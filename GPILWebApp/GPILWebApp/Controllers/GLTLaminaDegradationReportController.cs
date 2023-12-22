using GPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class GLTLaminaDegradationReportController : Controller
    {
        // GET: GLTLaminaDegradationReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CropCode()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "Select Distinct[Crop] from [dbo].[GPIL_RTQCR]";
                ds1 = cMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }
        [HttpGet]
        public ActionResult Grade()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "Select Distinct[Grade] from [dbo].[GPIL_RTQCR]";
                ds1 = cMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }
        [HttpGet]
        public ActionResult VarietyCode()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "Select Distinct[Variety] from [dbo].[GPIL_RTQCR]";
                ds1 = cMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }


        string lblMessage = string.Empty;
        public JsonResult GetLaminaDegradationReport(string fromDate, string toDate, string cropYear, string grade, string variety)
        {
            //Session["frmpurcdoc"] = poNumber;
            //string strsql;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            CommonManagement cMgt = new CommonManagement();

            if (fromDate == "")
            {
                data = "Error: From Date Should Not be Empty";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else if (toDate == "")
            {
                data = "Error: To Date Should Not be Empty";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else if (cropYear == "")
            {
                data = "Error: Crop Year Should Not be Empty";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else if (grade == "")
            {
                data = "Error: Grade Should Not be Empty";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else if (variety == "")
            {
                data = "Error: Variety Should Not be Empty";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            try
            {


                string query = "SELECT  [Crop],[Variety],[Grade],[Grade_Date],[Sample_Time] ,[RunNo],[CaseNo] ,[SampleWeight]";
                query += ",[Over11] ,[Over1] ,[Over1212] ,[Over12] ,[TOver1212]   ,[TOver12],[Over14],[Over14Second]  ,";
                query += "[TOver14],[Over18],[Over182] ,[OverPan] ,[PercntOnPan],[Over18P]";
                query += " ,[FirstPass],[PercentFirstPass] ,[SecondPass] ,[PercentSecondPass] ";
                query += ",[Obj3_32] ,[Obj3_32Second] ,[Slot07],[Slot07Second] ,[Slot12],[Slot12Second] ";
                query += ",[Mesh12] ,[Mesh12Second] ,[FiberHist] ,[FiberHistSecond]  ,[TsefHist] ,[TsefHistSecond] ,";
                query += "[NewFiber] ,[NewFiberSecond],[TsefNew] ,[New] ,[TotalStemInTips] ,[LC] ,[Stem],[PercentObjStem] ,";
                query += "[PercentStemTips]  ,[SystemFlagAnalysis] ,[PackedDensityDVR] ,[Remarks]  FROM [dbo].[GPIL_RTQCR] WHERE  ";

                if (fromDate != string.Empty && toDate != string.Empty)
                {
                    query += " [Grade_Date] between CONVERT(DATETIME,'" + fromDate + " 00:00:00',103) and CONVERT(DATETIME,'" + toDate + " 23:59:59',103) and";

                }

                if (grade != "")
                {
                    query += " [Grade]='" + grade + "'and";
                }
                if (variety != "")
                {
                    query += " [Variety]='" + variety + "'and";
                }
                if (cropYear != "")
                {
                    query += " [Crop]='" + cropYear + "'and";
                }

                query = query.Substring(0, query.Length - 3);
                ds1 = cMgt.GetQueryResult(query);
                ds1.TableName = "Table";

                if (ds1.Rows.Count == 0)
                {
                    // lblMessage.Text = "No Data Available";
                    data = "Error: No Data Available";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    json = JsonConvert.SerializeObject(ds1);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }

            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            // return Json(ds);
        }
    }
}