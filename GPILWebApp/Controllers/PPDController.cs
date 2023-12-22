using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp.Models;
using System.Web.Mvc;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System.Data;
using ClosedXML.Excel;

namespace GPILWebApp.Controllers
{
    public class PPDController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _data;
        public PPDController()
        {
            _data = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _data.Dispose();
        }
        //// GET: PPD
        //public ActionResult GradingDetails()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public JsonResult GradingDetailsIssue(string BatchNumber, string ReportType)
        //{
        //    DataSet ds = new DataSet();
        //    PPDManagement PPDMGT = new PPDManagement();
        //    try
        //    {
        //        ds = PPDMGT.GetGradingDetailsIssue(BatchNumber, ReportType);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}

        //[HttpGet]
        //public JsonResult GradingDetailsOutProduct(string BatchNumber, string ReportType)
        //{
        //    DataSet ds = new DataSet();
        //    PPDManagement PPDMGT = new PPDManagement();
        //    try
        //    {
        //        ds = PPDMGT.GetGradingDetailsOutProduct(BatchNumber, ReportType);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}

        //[HttpGet]
        //public JsonResult GradingDetailsByProduct(string BatchNumber, string ReportType)
        //{
        //    DataSet ds = new DataSet();
        //    PPDManagement PPDMGT = new PPDManagement();
        //    try
        //    {
        //        ds = PPDMGT.GetGradingDetailsByProduct(BatchNumber, ReportType);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}



        //public ActionResult GradingTempDetails()
        //{
        //    return View();
        //}


        //[HttpGet]
        //public JsonResult GradingTempDetailsIssue(string BatchNumber, string ReportType)
        //{
        //    DataSet ds = new DataSet();
        //    PPDManagement ppdMgt = new PPDManagement();
        //    try
        //    {
        //        ds = ppdMgt.GetGradingTempDetailsIssue(BatchNumber, ReportType);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}

        //[HttpGet]
        //public JsonResult GradingTempDetailsOutProduct(string BatchNumber, string ReportType)
        //{
        //    DataSet ds = new DataSet();
        //    PPDManagement ppdMgt = new PPDManagement();
        //    try
        //    {
        //        ds = ppdMgt.GetGradingTempDetailsOutProduct(BatchNumber, ReportType);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}

        //[HttpGet]
        //public JsonResult GradingTempDetailsByProduct(string BatchNumber, string ReportType)
        //{
        //    DataSet ds = new DataSet();
        //    PPDManagement ppdMgt = new PPDManagement();
        //    try
        //    {
        //        ds = ppdMgt.GetGradingTempDetailsByProduct(BatchNumber, ReportType);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}



        /// <summary>
        /// Classification Feed BackReport
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationFeedBackReport()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _data.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _data.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }


        [HttpGet]
        // GET: LD/PendingBales/a/a
        public ActionResult GetClassificationFeedBackReport(string strFromDate, string strToDate, string strCrop, string strVariety, string strOrgnCode)
        {
            PPDManagement pmgt = new PPDManagement();
            DataTable ds1 = new DataTable();
            string query = "";
            DateTime dt1 = Convert.ToDateTime(strFromDate);
            strFromDate = dt1.ToString("dd-MM-yyyy");
            DateTime dt2 = Convert.ToDateTime(strToDate);
            strToDate = dt2.ToString("dd-MM-yyyy");
            try
            {
                if (strOrgnCode == "")
                {
                    query = "SELECT H.ORGN_CODE,PH.ORGN_CODE AS TAP,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,PD.RATE,D.MARKED_WT,D.CLASSIFICATION_GRADE,D.REMARKS FROM GPIL_CLASSIFICATION_DTLS D,GPIL_CLASSIFICATION_HDR H,GPIL_TAP_FARM_PURCHS_DTLS PD,GPIL_TAP_FARM_PURCHS_HDR PH WHERE PH.HEADER_ID=PD.HEADER_ID AND D.GPIL_BALE_NUMBER=PD.GPIL_BALE_NUMBER AND  H.BATCH_NO=D.BATCH_NO AND H.REASONING_CODE='0' AND D.REMARKS IS NOT NULL AND  D.ISSUED_GRADE LIKE '" + strCrop + strVariety + "%' AND H.CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND  CONVERT(DATETIME,'" + strToDate + " 23:59:59',105) ORDER BY H.ORGN_CODE,PH.ORGN_CODE,D.GPIL_BALE_NUMBER";
                }
                else
                {
                    query = "SELECT H.ORGN_CODE,PH.ORGN_CODE AS TAP,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,PD.RATE,D.MARKED_WT,D.CLASSIFICATION_GRADE,D.REMARKS FROM GPIL_CLASSIFICATION_DTLS D,GPIL_CLASSIFICATION_HDR H,GPIL_TAP_FARM_PURCHS_DTLS PD,GPIL_TAP_FARM_PURCHS_HDR PH WHERE PH.HEADER_ID=PD.HEADER_ID AND D.GPIL_BALE_NUMBER=PD.GPIL_BALE_NUMBER AND  H.BATCH_NO=D.BATCH_NO AND H.REASONING_CODE='0' AND D.REMARKS IS NOT NULL AND  D.ISSUED_GRADE LIKE '" + strCrop + strVariety + "%' AND H.ORGN_CODE='" + strOrgnCode + "' AND H.CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND  CONVERT(DATETIME,'" + strToDate + " 23:59:59',105)  ORDER BY H.ORGN_CODE,PH.ORGN_CODE,D.GPIL_BALE_NUMBER";
                }

                ds1 = pmgt.GetQueryResult(query);


            }
            catch (Exception ex)
            {
                ds1 = null;

            }
            string json = JsonConvert.SerializeObject(ds1);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// GRADING BATCH DETAILS
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingBatchDetailsIndex()
        {
            lstDatatableDtls.Clear();
            return View();
        }

        public static List<DataTable> lstDatatableDtls = new List<DataTable>();

        public void ExportListDetails()
        {
            if (lstDatatableDtls.Count > 0)
            {
                ExportToExcel(lstDatatableDtls, "GradingBatchDetails");
                lstDatatableDtls.Clear();
            }
            else
            {
                Response.Redirect("GradingBatchTempDetailsIndex");
            }


        }



        [HttpGet]
        public ActionResult GradingBatchIssueDetails(string strBatchNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_GRADING_DTLS  WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='IPB'";            
            dtclstr = ppdMgt.GetQueryResult(query);            
            string json = JsonConvert.SerializeObject(dtclstr);
            lstDatatableDtls.Add(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);
            

        }

        [HttpGet]
        public ActionResult GradingBatchOutTurnProductDetails(string strBatchNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_GRADING_DTLS  WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE NOT IN ('BP','LOSS')";
            dtclstr = ppdMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            lstDatatableDtls.Add(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult GradingBatchOutTurnByProductDetails(string strBatchNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_GRADING_DTLS  WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS')";
            dtclstr = ppdMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            lstDatatableDtls.Add(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }



        /// <summary>
        /// GRADING BATCH TEMP DETAILS
        /// </summary>
        /// <returns></returns>
        public static List<DataTable> lstDatatableTemp = new List<DataTable>();

        public void ExportListTempDetails()
        {
            if (lstDatatableTemp.Count > 0)
            {
                ExportToExcel(lstDatatableTemp, "GradingBatchTempDetails");
                lstDatatableTemp.Clear();
            }
            else
            {
                Response.Redirect("GradingBatchTempDetailsIndex");
            }


        }
        public ActionResult ExportToExcel(List<DataTable> datas,string FileName)
        {
            
            using (var workbook = new XLWorkbook())
            {
                int sheet = 1;

                foreach (DataTable items in datas)
                {
                    var worksheet = workbook.Worksheets.Add($"Sheet{sheet}");

                    // Add DataTable headers
                    for (var i = 0; i < items.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = items.Columns[i].ColumnName;
                    }

                    // Add DataTable data
                    for (var i = 0; i < items.Rows.Count; i++)
                    {
                        for (var j = 0; j < items.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = items.Rows[i][j];
                        }
                    }

                    sheet++;
                }

                // Set content type and return the Excel file
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    memoryStream.Close();
                }

                Response.End();
            }

            return null;
        }

        public ActionResult GradingBatchTempDetailsIndex()
        {
            lstDatatableTemp.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult GradingBatchTempIssueDetails(string strBatchNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_GRADING_DTLS_TEMP   WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='IPB'";
            dtclstr = ppdMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            lstDatatableTemp.Add(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult GradingBatchTempOutTurnProductDetails(string strBatchNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_GRADING_DTLS_TEMP   WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE NOT IN ('BP','LOSS')";
            dtclstr = ppdMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            lstDatatableTemp.Add(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult GradingBatchTempOutTurnByProductDetails(string strBatchNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_GRADING_DTLS_TEMP   WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS')";
            dtclstr = ppdMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            lstDatatableTemp.Add(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }


        /// <summary>
        /// Un-Classified Details
        /// </summary>
        /// <returns></returns>
        public ActionResult UnClassifiedDetailsIndex()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            ViewBag.GPIL_CROP_MASTER = (from c in _data.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _data.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();
        }


        [HttpGet]
        public ActionResult UnClassifiedDetails(string strOrgnCode, string strCrop, string strVariety)
        {
            string orgnCode  = strOrgnCode.ToString().Trim();
            string strAdditionalCondition = " ";


            if (orgnCode.Length != 0 && orgnCode.Length != -1)
            {
                strAdditionalCondition += " AND CURR_ORGN_CODE='" + orgnCode + "' ";
            }
            if (strCrop.Length != 0 && strCrop.Length != -1)
            {
                strAdditionalCondition += " AND CROP='" + strCrop + "' ";
            }
            if (strVariety.Length != 0 && strVariety.Length != -1)
            {
                strAdditionalCondition += " AND VARIETY='" + strVariety + "' ";
            }


            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            if(orgnCode.Length != 0 || strCrop.Length != 0 || strVariety.Length != 0)
            {
               // query = "SELECT ROW_NUMBER() OVER(ORDER BY CROP,VARIETY,CURR_ORGN_CODE,GPIL_BALE_NUMBER) AS SNO,CROP,VARIETY,CURR_ORGN_CODE,GPIL_BALE_NUMBER,TB_LOT_NO,BUYER_GRADE,MARKED_WT,PRICE,BATCH_NO FROM GPIL_STOCK WHERE CURR_ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE<>'TAP') AND STATUS='Y' AND PROCESS_STATUS='N' AND GRADE IS NULL " + strAdditionalCondition;
                query = "SELECT ROW_NUMBER() OVER(ORDER BY CROP,VARIETY,CURR_ORGN_CODE,GPIL_BALE_NUMBER) AS SNO,CROP,VARIETY,CURR_ORGN_CODE,GPIL_BALE_NUMBER,TB_LOT_NO,BUYER_GRADE,MARKED_WT,PRICE,BATCH_NO FROM GPIL_STOCK WHERE  STATUS='Y' AND PROCESS_STATUS='N' AND (GRADE IS NULL OR GRADE ='')" + strAdditionalCondition;
            }
            else
            {
                query = " select * from GPIL_STOCK WHERE STATUS='Y' AND PROCESS_STATUS='N' AND (GRADE IS NULL  OR GRADE ='')";
            }
            
            dtclstr = ppdMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }



        /// <summary>
        /// ClassificationReport(T/G)
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationReportForTAPIndex()
        {
            return View();
        }

        /// <summary>
        /// Classification Cumulative Report
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationCumuReportIndex()
        {
            return View();
        }

        /// <summary>
        /// VKBU Buyer Classification Cumulative Report
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationCumuReportVKBUIndex()
        {
            return View();
        }
        /// <summary>
        /// Classification Cumulative Today VS Todate Report
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationCumuReportTodayTodateIndex()
        {
            return View();
        }
        /// <summary>
        /// Classification Cumulative With Grade Transfer Report

        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationCumulativeWithGradeTransferReportIndex()
        {
            return View();
        }
        /// <summary>
        /// Classification Cumulative With Grade Transfer Report With Freight
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassCumuWithGradeTransferFrightReportIndex()
        {
            return View();
        }    
        
        /// <summary>
        /// Up & Down Report
        /// </summary>
        /// <returns></returns>
        public ActionResult UpAndDownClassificationReportIndex()
        {
            return View();
        }
        /// <summary>
        /// Grading issue /OutTurn Sheet
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingIssueIndex()
        {
            return View();
        }
        /// <summary>
        /// Grading /Daily Operation Report
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingReportIndex()
        {
            return View();
        }
        /// <summary>
        /// Grading Operation APH Report
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingOperationReportIndex()
        {
            return View();
        }
        /// <summary>
        /// Green Stock Report
        /// </summary>
        /// <returns></returns>
        public ActionResult GreenStockReportIndex()
        {
            return View();
        }
        /// <summary>
        /// CL Org Wise Classification Report
        /// </summary>
        /// <returns></returns>
        public ActionResult ClOrgWiseClassificationReportIndex()
        {
            return View();
        }


        /// <summary>
        /// Batch Wise Classification Report
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchwiseClassificationRptIndex()
        {
            return View();
        }
        /// <summary>
        /// Supplier Purchase Print
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierPurchaseInfoReportsIndex()
        {
            return View();
        }
        /// <summary>
        /// Buyer Vs Classifier Grade  Report
        /// </summary>
        /// <returns></returns>
        public ActionResult BuyerClassGradeReportIndex()
        {
            return View();
        }












    }
}