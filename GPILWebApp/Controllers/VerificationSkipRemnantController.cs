using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;
using Newtonsoft.Json;
using GPILWebApp.ViewModel;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class VerificationSkipRemnantController : Controller
    {
        // GET: VerificationSkipRemnant
        public ActionResult Index()
        {
            return View();
        }
        CommonManagement cMgt = new CommonManagement();
        [HttpGet]
        public ActionResult GetActualRemnantDetails()
        {
            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) WHERE  BALE_TYPE='IPB' AND GRADE LIKE 'L%' AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR (NOLOCK) WHERE STATUS='N' AND FLAG IS NULL)";
            dtclstr = cMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public ActionResult GetSkippedRemnantDetails()
        {
            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,REMARKS FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) WHERE BATCH_NO='2015/TH/094/P27/0142/1' AND BALE_TYPE='IPB' AND GRADE LIKE 'L%' ";
            dtclstr = cMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


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


        [HttpPost]
        public JsonResult SkipThreshingIssueRemnant(ListSkipRemnantThreshingIssue LSRTI)
        {
            String strsql = string.Empty;
            String query = string.Empty;
            DataTable dtclstr = ToDataTable1(LSRTI.SkipRemnantThreshingIssues);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;


            try
            {
                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {

                    string strBatchNo, strBaleNumber, strBaleType, strProductType, strGrade, strMarkedWt, strAscertainWt, strSubInvCode;


                    strBatchNo = dtclstr.Rows[i]["BATCH_NO"].ToString();
                    strBaleNumber = dtclstr.Rows[i]["GPIL_BALE_NUMBER"].ToString();
                    strBaleType = dtclstr.Rows[i]["BALE_TYPE"].ToString();
                    strProductType = dtclstr.Rows[i]["PRODUCT_TYPE"].ToString();
                    strGrade = dtclstr.Rows[i]["GRADE"].ToString();
                    strMarkedWt = dtclstr.Rows[i]["MARKED_WT"].ToString();
                    strAscertainWt = dtclstr.Rows[i]["ASCERTAIN_WT"].ToString();
                    strSubInvCode = dtclstr.Rows[i]["SUBINVENTORY_CODE"].ToString();

                    strsql = " UPDATE GPIL_THRESH_RECON_DTLS_1 SET REMARKS = BATCH_NO, BATCH_NO = '2015/TH/094/P27/0142/1' WHERE BALE_TYPE = 'IPB' AND GRADE LIKE 'L%' AND BATCH_NO IN(SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR(NOLOCK) WHERE STATUS = 'N' AND FLAG IS NULL)";


                    GetActualRemnantDetails();
                    GetSkippedRemnantDetails();
                   
                }
                bool b = cMgt.UpdateUsingExecuteNonQuery(query);

                if (b)
                {
                    data = "Succuss: Updated Sucessfully!!!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }
                else
                {
                    data = "Error: Error While Uploading";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult SkipThreshingIssueRemnantSkipped(ListSkipRemnantThreshingIssue LSRTI)
        {
            List<string> lstQry = new List<string>();
            String strsql = string.Empty;
            String query = string.Empty;
            DataTable dtclstr = ToDataTable1(LSRTI.SkipRemnantThreshingIssues);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;


            try
            {
                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {

                    string strBatchNo, strBaleNumber, strBaleType, strProductType, strGrade, strMarkedWt, strAscertainWt, strRemarks;


                    strBatchNo = dtclstr.Rows[i]["BATCH_NO"].ToString();
                    strBaleNumber = dtclstr.Rows[i]["GPIL_BALE_NUMBER"].ToString();
                    strBaleType = dtclstr.Rows[i]["BALE_TYPE"].ToString();
                    strProductType = dtclstr.Rows[i]["PRODUCT_TYPE"].ToString();
                    strGrade = dtclstr.Rows[i]["GRADE"].ToString();
                    strMarkedWt = dtclstr.Rows[i]["MARKED_WT"].ToString();
                    strAscertainWt = dtclstr.Rows[i]["ASCERTAIN_WT"].ToString();
                    strRemarks = dtclstr.Rows[i]["REMARKS"].ToString();

                    strsql = "UPDATE GPIL_THRESH_RECON_DTLS_1 SET BATCH_NO='"+ strRemarks + "', LAST_UPDATED_DATE=getdate() WHERE BATCH_NO='2015/TH/094/P27/0142/1' AND BALE_TYPE='IPB' AND GRADE LIKE 'L%' AND REMARKS IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR (NOLOCK) WHERE BATCH_NO IN (SELECT REMARKS FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) WHERE BATCH_NO='2015/TH/094/P27/0142/1' AND BALE_TYPE='IPB' AND GRADE LIKE 'L%') and STATUS='N' and FLAG='Y')";
                    lstQry.Add(strsql);

                    GetActualRemnantDetails();
                    GetSkippedRemnantDetails();

                }
                bool b = cMgt.UpdateUsingExecuteNonQuery(strsql);

                if (b)
                {
                    data = "Succuss: Updated Sucessfully!!!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }
                else
                {
                    data = "Error: Error While Uploading";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }




    }
}