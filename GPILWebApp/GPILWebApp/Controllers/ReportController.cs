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
    public class ReportController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;
        public ReportController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Report

        /// <summary>
        /// LP5 PRINT
        /// </summary>
        /// <returns></returns>
        public ActionResult LP5Print()
        {
            return View();
        }

        public ActionResult LP4Print()
        {
            return View();
        }


        public ActionResult GradewiseStock()
        {
            return View();
        }


        public ActionResult LotWiseStockReport()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.ORGN_TYPE == "TAP" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }



        public ActionResult LP5AuctionDocument()
        {
            return View();
        }

        /// <summary>
        /// FACTORY LP5 PRINT
        /// </summary>
        /// <returns></returns>
        public ActionResult FactoryLP5PrintIndex()
        {
            return View();
        }

        /// <summary>
        /// Dispatch Report
        /// </summary>
        /// <returns></returns>
        public ActionResult DispatchReport()
        {
            return View();
        }

        /// <summary>
        /// RECEIPT REPORT
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptReport()
        {
            return View();
        }

        //public ActionResult LP5Print()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// FACTORY LP5 PRINT
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult FactoryLP5PrintIndex()
        //{
        //    return View();
        //}




        /// <summary>
        /// LP4 PRINT
        /// </summary>
        /// <returns></returns>
        public ActionResult LP4PrintIndex()
        {
            return View();
        }

        /// <summary>
        /// INTRANSIT REPORT
        /// </summary>
        /// <returns></returns>
        public ActionResult IntransitReportIndex()
        {
            return View();
        }

        /// <summary>
        /// SALES ORDER PRINT
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesOrderPrintIndex()
        {
            return View();
        }


        public ActionResult GradeTransferReport()
        {
            return View();
        }

        public ActionResult CropTransferReport()
        {
            return View();
        }





        /// <summary>
        /// BALE TRACK
        /// </summary>
        /// <returns></returns>
        public ActionResult BaleTrackIndex()
        {
            return View();
        }



        /// <summary>
        ///LOT WISE STOCK
        /// </summary>
        /// <returns></returns>
        public ActionResult LotWiseStockIndex()
        {
            return View();
        }


        /// <summary>
        ///WEEKLY RECONCILATION
        /// </summary>
        /// <returns></returns>
        public ActionResult WeeklyReconcilationIndex()
        {
            return View();
        }



        public ActionResult WeightLossReport()
        {
            return View();
        }










        /// <summary>
        /// GRADING CHART REPORT
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingChartReport()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            ViewBag.GPIL_OPERATION_RECIPE = (from r in _context.GPIL_OPERATION_RECIPE select new { r.RECIPE_CODE, r.OPERATION_RECIPE }).ToList();

            return View();
        }
        /// <summary>
        /// FREIGHT CHARGE REPORT
        /// </summary>
        /// <returns></returns>
        public ActionResult FreightChargeReport()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();

            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            ViewBag.GPIL_ORGN_MASTERs = (from s in _context.GPIL_ORGN_MASTER select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            ViewBag.GPIL_SHIPMENT_HDR = (from v in _context.GPIL_SHIPMENT_HDR select new { v.ATTRIBUTE2 }).ToList();
            ViewBag.GPIL_SHIPMENT_HDRs = (from v in _context.GPIL_SHIPMENT_HDR select new { v.ATTRIBUTE3 }).ToList();
            return View();
        }

        //FromDate, ToDate, Crop, Variety, FromDept, ToDept, FromOrgn, ToOrgn
        [HttpGet]
        public JsonResult FreightChargeReportDetails(string fromDate, string toDate, string crop, string variety, string fromDept, string toDept, string fromOrgn, string toOrgn)
        {
            DataTable DataTable = new DataTable();
            string lblMessage = string.Empty;
            CommonManagement cMgt = new CommonManagement();

            if (string.IsNullOrEmpty(crop) || crop == "")
            {
                lblMessage = "Error: Please Select Crop..";

            }
            if (string.IsNullOrEmpty(variety) || variety == "")
            {
                lblMessage = "Error: Please Select Variety..";

            }

            DateTime dt1, dt2;
            string sFrom, sTo;
            dt1 = Convert.ToDateTime(fromDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            dt2 = Convert.ToDateTime(toDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            sFrom = String.Format("{0:yyyyMMdd}", dt1);
            sTo = String.Format("{0:yyyyMMdd}", dt2);
            string sSql = "";

            //For Summary report
            sSql = "select count(DISTINCT H.SENDER_TRUCK_NO)NoOfTrucks,COUNT(D.GPIL_BALE_NUMBER)NoOfBales,convert(numeric(18,2),SUM(D.MARKED_WT))Qty,";
            sSql = sSql + "(SUM(D.MARKED_WT*H.FRIEGHT_CHARGES)/SUM(D.MARKED_WT))FREIGHT_CHARGE,SUM(CONVERT(NUMERIC(18,2),D.MARKED_WT*H.FRIEGHT_CHARGES))Value";
            sSql = sSql + " from GPIL_SHIPMENT_HDR(NOLOCK) H,GPIL_SHIPMENT_dtls(NOLOCK) D where H.SHIPMENT_NO=D.SHIPMENT_NO and";
            sSql = sSql + " convert(varchar(15),H.SENDER_DATE,112)>='" + sFrom.Trim() + "' and convert(varchar(15),H.SENDER_DATE,112)<='" + sTo.Trim() + "'";
            sSql = sSql + " and substring(D.GPIL_BALE_NUMBER,1,2)='" + crop + "' and substring(D.GPIL_BALE_NUMBER,3,2)='" + variety + "'";
            if (!string.IsNullOrEmpty(fromOrgn) & fromOrgn != "")
            {
                sSql = sSql + " and H.SENDER_ORGN_CODE='" + fromOrgn + "'";
            }
            if (!string.IsNullOrEmpty(toOrgn) & toOrgn != "")
            {
                sSql = sSql + " and H.RECEIVER_ORGN_CODE='" + toOrgn + "'";
            }
            if (!string.IsNullOrEmpty(fromDept) & fromDept != "")
            {
                sSql = sSql + " and H.ATTRIBUTE2='" + fromDept + "'";
            }
            if (!string.IsNullOrEmpty(toDept) & toDept != "")
            {
                sSql = sSql + " and H.ATTRIBUTE3='" + toDept + "'";
            }
            //sSql = sSql + " group by H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SHIPMENT_NO,H.FRIEGHT_CHARGES order by H.SHIPMENT_NO";
            DataTable = cMgt.GetQueryResult(sSql);


            if (DataTable.Rows.Count > 0)
            {
                string json = JsonConvert.SerializeObject(DataTable);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //grdHeader.Visible = false;
                //grdHeader.DataSource = null;
                //grdHeader.DataBind();
            }

            return Json(DataTable);
        }


        [HttpGet]
        public JsonResult FreightChargeReportDetailss(string fromDate, string toDate, string crop, string variety, string fromDept, string toDept, string fromOrgn, string toOrgn)
        {
            DataTable DataTable = new DataTable();
            string lblMessage = string.Empty;
            CommonManagement cMgt = new CommonManagement();


            if (string.IsNullOrEmpty(crop) || crop == "")
            {
                lblMessage = "Error: Please Select Crop..";

            }
            if (string.IsNullOrEmpty(variety) || variety == "")
            {
                lblMessage = "Error: Please Select Variety..";

            }

            DateTime dt1, dt2;
            string sFrom, sTo;
            dt1 = Convert.ToDateTime(fromDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            dt2 = Convert.ToDateTime(toDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            sFrom = String.Format("{0:yyyyMMdd}", dt1);
            sTo = String.Format("{0:yyyyMMdd}", dt2);
            string sSql = "";

            //For detail report
            sSql = "select H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SHIPMENT_NO,COUNT(D.GPIL_BALE_NUMBER)NoOfBales,SUM(D.MARKED_WT)Qty,";
            sSql = sSql + " H.FRIEGHT_CHARGES as FREIGHT_CHARGES,CONVERT(NUMERIC(18,2),(SUM(D.MARKED_WT)*H.FRIEGHT_CHARGES))Value";
            sSql = sSql + " from GPIL_SHIPMENT_HDR(NOLOCK) H,GPIL_SHIPMENT_dtls(NOLOCK) D where H.SHIPMENT_NO=D.SHIPMENT_NO and";
            sSql = sSql + " convert(varchar(15),H.SENDER_DATE,112)>='" + sFrom.Trim() + "' and convert(varchar(15),H.SENDER_DATE,112)<='" + sTo.Trim() + "'";
            sSql = sSql + " and substring(D.GPIL_BALE_NUMBER,1,2)='" + crop + "' and substring(D.GPIL_BALE_NUMBER,3,2)='" + variety + "'";
            if (!string.IsNullOrEmpty(fromOrgn) & fromOrgn != "")
            {
                sSql = sSql + " and H.SENDER_ORGN_CODE='" + fromOrgn + "'";
            }
            if (!string.IsNullOrEmpty(toOrgn) & toOrgn != "")
            {
                sSql = sSql + " and H.RECEIVER_ORGN_CODE='" + toOrgn + "'";
            }
            if (!string.IsNullOrEmpty(fromDept) & fromDept != "")
            {
                sSql = sSql + " and H.ATTRIBUTE2='" + fromDept + "'";
            }
            if (!string.IsNullOrEmpty(toDept) & toDept != "")
            {
                sSql = sSql + " and H.ATTRIBUTE3='" + toDept + "'";
            }
            sSql = sSql + " group by H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SHIPMENT_NO,H.FRIEGHT_CHARGES order by H.SHIPMENT_NO";

            DataTable = cMgt.GetQueryResult(sSql);
           

            if (DataTable.Rows.Count > 0)
            {
                string json = JsonConvert.SerializeObject(DataTable);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                lblMessage = "Error: No Data Found..";
            }
            return Json(DataTable);
        }
    }
}