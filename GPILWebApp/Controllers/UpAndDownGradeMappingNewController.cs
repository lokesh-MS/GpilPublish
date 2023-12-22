using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;
using Newtonsoft.Json;
using System.ComponentModel;
using GPILWebApp.ViewModel;

namespace GPILWebApp.Controllers
{
    public class UpAndDownGradeMappingNewController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities _context;


        public UpAndDownGradeMappingNewController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }

        [HttpPost]
        public  List<CountryList> GetCountriesName()
        {
            List<CountryList> lst = new List<CountryList>();
            lst.Add(new CountryList() { PairTypeId = 1, PairTypeName = "UP" });
            lst.Add(new CountryList() { PairTypeId = 2, PairTypeName = "DOWN" });
            lst.Add(new CountryList() { PairTypeId = 3, PairTypeName = "EQUAL" });
            return lst;
            //var output = JsonConvert.SerializeObject(lst);

        }

        // GET: UpAndDownGradeMappingNew
        public ActionResult UpAndDownGradeMappingNewIndex()
        {
            ViewBag.GPIL_CROP_MASTERs = (from c in _context.GPIL_CROP_MASTER select new { c.CROP, c.CROP_YEAR }).Distinct();
            ViewBag.GPIL_VARIETY_MASTERs = (from v in _context.GPIL_VARIETY_MASTER select new { v.VARIETY, v.VARIETY_NAME }).Distinct();
            return View();
        }
        DataTable dt = new DataTable();
        CommonManagement cMgt = new CommonManagement();
        string lblMessage = string.Empty;
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;

        [HttpGet]
        public JsonResult GetGradeMappingDetails(string cropYear, string variety)
        {
            try
            {
                string strQuery;
                string strCropVariety = "";
                string strCrop = "";
                string strVariety = "";
               
                if (cropYear != null && variety != null)
                {
                    strCropVariety = cropYear + variety;
                    strCrop = cropYear;
                    strVariety = variety;
                    strQuery = "((SELECT DISTINCT T1.CROP AS CROP,T1.VARIETY AS VARIETY,T1.ISSUED AS BUYER_GRADE_GRP ,T2.CLASSIFIED AS CLASSIFIER_GRADE_GRP,'None' AS PAIR_TYPE FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED,CROP,VARIETY FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE LIKE '" + strCropVariety + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0'  AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED,CROP,VARIETY FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE LIKE '" + strCropVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0'  AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2 " +
                                "WHERE T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND T1.CROP=T2.CROP AND T1.VARIETY=T2.VARIETY ) " +
                                "EXCEPT(SELECT '" + strCrop + "' AS CROP,'" + strVariety + "' AS VARIETY,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,'None' AS PAIR_TYPE FROM GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) WHERE ATTRIBUTE1='" + strCrop + "' AND ATTRIBUTE2='" + strVariety + "'))  " +
                                "ORDER BY T1.ISSUED,T2.CLASSIFIED ";
                  
                    dt = cMgt.GetQueryResult(strQuery);
                   
                    if (dt.Rows.Count > 0)
                    {
                        dt.TableName = "Table";
                        var data = dt;
                        string json = JsonConvert.SerializeObject(data);
                        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                       
                    }
                    else
                    {
                        lblMessage = "Error: Unpair Count : 0";
                    }
                }
                else
                {
                    //lblMessage.Text = "Please Select Crop & Variety";
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
            if (lblMessage.Length > 0)
            {
                data = lblMessage;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                data = "Success";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
        }


        public static DataTable ToDataTable1<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public ActionResult UpDownGradeMapping()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UpDownGradeMappingComplete(ListUpDownGradeMapping LUDGM)
        {
            int z = 0;
            String strsql = string.Empty;
            String query = string.Empty;
            DataTable dtclstr = ToDataTable1(LUDGM.UpDownGradeMappingNew);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {
                    try
                    {

                        string strCROP, strVARIETY, strBUYER_GRADE_GRP, strCLASSIFIER_GRADE_GRP, strPAIR_TYPE;
                        string strCropVariety;
                        strCROP = dtclstr.Rows[i]["CROP"].ToString();
                        strVARIETY = dtclstr.Rows[i]["VARIETY"].ToString();
                        strCropVariety = dtclstr.Rows[i]["CROP"].ToString() + dtclstr.Rows[i]["VARIETY"].ToString();
                        strBUYER_GRADE_GRP = dtclstr.Rows[i]["BUYER_GRADE_GRP"].ToString();
                        strCLASSIFIER_GRADE_GRP = dtclstr.Rows[i]["CLASSIFIER_GRADE_GRP"].ToString();
                        strPAIR_TYPE = dtclstr.Rows[i]["PAIR_TYPE"].ToString();
                        if (strPAIR_TYPE != "0")
                        {
                            string strQuery = "INSERT INTO GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER (PAIR_CODE,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,PAIR_TYPE,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2) VALUES ('" + strCropVariety + "-" + strBUYER_GRADE_GRP + "-" + strCLASSIFIER_GRADE_GRP + "', '" + strBUYER_GRADE_GRP + "', '" + strCLASSIFIER_GRADE_GRP + "', '" + strPAIR_TYPE + "', '" + Session["userID"].ToString() + "',GETDATE(),'Y', '" + strCROP + "', '" + strVARIETY + "')";
                            cMgt.UpdateUsingExecuteNonQuery(strQuery);
                        }

                       

                        //string strQuery = "INSERT INTO GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER (PAIR_CODE,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,PAIR_TYPE,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2) VALUES ('" + strCropVariety + "-" + strBUYER_GRADE_GRP + "-" + strCLASSIFIER_GRADE_GRP + "', '" + strBUYER_GRADE_GRP + "', '" + strCLASSIFIER_GRADE_GRP + "', '" + strPAIR_TYPE + "', '" + Session["userID"].ToString() + "',GETDATE(),'Y', '" + strCROP + "', '" + strVARIETY + "')";
                        //cMgt.UpdateUsingExecuteNonQuery(strQuery);
                        z = 0;
                    }

                    catch (Exception ex)
                    {
                        z = 1;
                    }
                }
                if (z == 1)
                {
                    //tran.Rollback();
                    lblMessage = "Error: Error while Inserting";



                }
                else
                {

                    //tran.Commit();
                    lblMessage = "Success: Sucessfully Inserted";


                }
                if (lblMessage.Length > 0)
                {
                    data = lblMessage;
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Success";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }

            }
            catch (Exception ex)
            {
                lblMessage = ex.ToString();

            }
            return null;
        }
    }
}