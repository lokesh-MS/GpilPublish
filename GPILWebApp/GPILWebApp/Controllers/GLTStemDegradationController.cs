using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class GLTStemDegradationController : Controller
    {
        // GET: GLTStemDegradation
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

                strsql = "select distinct[Crop] from [dbo].[GPIL_StemReports]";
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
        public ActionResult ScrapGrade()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "select distinct[Grade] from [dbo].[GPIL_StemReports]";
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

                strsql = "select distinct[Variety] from [dbo].[GPIL_StemReports]";
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
        public ActionResult LamiaGrade()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "select distinct[LamiaGrade] from [dbo].[GPIL_StemReports]";
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
        public JsonResult getStemDegradationReport(string fromDate, string toDate, string cropYear, string grade, string variety, string lamiaGrade)
        {
            //Session["frmpurcdoc"] = poNumber;
            //string strsql;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            CommonManagement cMgt = new CommonManagement();
            try
            {
                string query = "SELECT [Crop],[Variety],[LamiaGrade],[Grade],[Date]=(Convert(varchar,[Date],103)),[Time]=(Convert(varchar,[Time],108)),[CaseNo],[TotalLength]";
                query += ",[NoofStemPieces] ,[AvgStemLength],[StemWeight],[StemRotapG3_32],[StemRotapG3_32Percent]";
                query += " ,[StemRotapL3_32],[StemRotapL3_32Percent],[SandnDust]";
                query += " ,[SandnDustPercent],[NoofTotalStemInPieces],[NoofL1_2Stems],[L1_2StemsPercent]";
                query += " ,[[L1_2_4Stems],[NoofL1_2toG4StemsPercent],[NoofG4Stems],[NoofG4StemPercent]";
                query += "FROM [dbo].[GPIL_StemReports] where ";

                if (fromDate != string.Empty && toDate != string.Empty)
                {
                    query += " [Date] between CONVERT(DATETIME,'" + fromDate + " 00:00:00',103) and CONVERT(DATETIME,'" + toDate + " 23:59:59',103) and";
                }

                if (cropYear != "")
                {
                    query += " [Crop]='" + cropYear.ToString() + "' and";
                }

                if (variety != "")
                {
                    query += " [Variety]='" + variety.ToString() + "' and";
                }

                if (grade != "")
                {
                    query += " [Grade]='" + grade.ToString() + "' and";
                }
                if (lamiaGrade != "")
                {
                    query += " [LamiaGrade]='" + lamiaGrade.ToString() + "' and";
                }
                query = query.Substring(0, query.Length - 3);
                ds1 = cMgt.GetQueryResult(query);
                ds1.TableName = "Table";
                //var data = ds1;

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