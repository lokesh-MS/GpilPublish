using GPI;
using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class UpAndDownGradeMappingController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;

         
        public UpAndDownGradeMappingController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        // GET: UpAndDownGradeMapping
        public ActionResult UpAndDownGradeMappingIndex()
        {
           
            ViewBag.GPIL_CROP_MASTERs = (from c in _context.GPIL_CROP_MASTER select new { c.CROP, c.CROP_YEAR }).Distinct();
            ViewBag.GPIL_VARIETY_MASTERs = (from v in _context.GPIL_VARIETY_MASTER select new { v.VARIETY, v.VARIETY_NAME }).Distinct();
            return View();
        }


        [HttpGet]
        public ActionResult GetUpAndDownMappingDetails(string crop, string variety)
        {
            
            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string lblMessage = string.Empty;
            JsonResult jsonResult;
            string data = String.Empty;
            string json = "";
            try
            {
                string strQuery;
                

                //string lblMessage = "";
                //lblRecordCount.Text = "";



               

                if (crop.Length > 0 && variety.Length > 0)
                {
                   string strCropVariety = crop + variety;
                    


                    

                    strQuery = "((SELECT DISTINCT T1.CROP AS CROP,T1.VARIETY AS VARIETY,T1.ISSUED AS BUYER_GRADE_GRP ,T2.CLASSIFIED AS CLASSIFIER_GRADE_GRP,'None' AS PAIR_TYPE FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED,CROP,VARIETY FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE LIKE '" + strCropVariety + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0'  AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED,CROP,VARIETY FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE LIKE '" + strCropVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0'  AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2 " +
                                "WHERE T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND T1.CROP=T2.CROP AND T1.VARIETY=T2.VARIETY ) " +
                                "EXCEPT(SELECT '" + crop + "' AS CROP,'" + variety + "' AS VARIETY,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,'None' AS PAIR_TYPE FROM GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) WHERE ATTRIBUTE1='" + crop + "' AND ATTRIBUTE2='" + variety + "'))  " +
                                "ORDER BY T1.ISSUED,T2.CLASSIFIED ";
                    dtclstr = cMgt.GetQueryResult(strQuery);
                    if (dtclstr.Rows.Count > 0)
                    {

                        lblMessage = "Success: Unpair Count : " + dtclstr.Rows.Count.ToString();
                        json = JsonConvert.SerializeObject(dtclstr);
                        return Json(json, JsonRequestBehavior.AllowGet);
                    }
                    
                    else
                    {
                        lblMessage = "Error: Unpair Count : 0";
                    }
                   
                }
                else
                {
                    lblMessage = "Error: Please Select Crop & Variety";
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
                lblMessage = ex.Message;
            }
            
            return Json(dtclstr);


        }
        //[HttpPost]
        //public JsonResult UpAndDownGradeMappingView(ListUpAndDownGradeMappingUpload UADGM)
        //{
        //    UpAndDownGradeMappingdata(UADGM);
        //    return null;
        //}

        [HttpPost]
        public ActionResult AddToMasterDetails(string crop, string variety, string buyerGrade, string classifierGrade, string pairType)
        {
            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string lblMessage = string.Empty;
            JsonResult jsonResult;
            string data = String.Empty;
            string json = "";
            try
            {


                if (pairType.Length >= 0)
                {
                    
                    string strQuery, strPairCode="", strCropVariety;

                    
                    strCropVariety = crop + variety;


                    if (pairType == "Up")
                    {
                        strPairCode = "U";
                    }
                    else if (pairType == "Down")
                    {
                        strPairCode = "D";
                    }
                    else if (pairType == "Equal")
                    {
                        strPairCode = "E";
                    }
                    else
                    {
                        //return;
                    }
                                                                                                                                                                                                                                                                

                    strQuery = "INSERT INTO GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER (PAIR_CODE,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,PAIR_TYPE,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2) VALUES ('" + strCropVariety + "-" + buyerGrade + "-" + classifierGrade + "','" + buyerGrade + "','" + classifierGrade + "','" + strPairCode + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','" + crop + "','" + variety + "')";
                    cMgt.UpdateUsingExecuteNonQuery(strQuery);


                    //lblMessage = "Success: Unpair Count : " + dtclstr.Rows.Count.ToString();
                    //json = JsonConvert.SerializeObject(dtclstr);
                    //return Json(json, JsonRequestBehavior.AllowGet);

                }
                GetUpAndDownMappingDetails(crop, variety);

            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error While Updating data please try again');", true);
            }
            finally
            {
                //ClsConnection.closeDB();
            }
            return View();
        }
    }
}