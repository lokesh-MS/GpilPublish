using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp.Models;
using System.Web.Mvc;
//using GPILWebApp.ViewModel;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data.Entity.SqlServer;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using GPILWebApp.ViewModel;

namespace GPILWebApp.Controllers
{
    public class LDController : Controller
    {
        RDLCReport rdlcReport = new RDLCReport();
        // GET: LD
        private GREEN_LEAF_TRACEABILITYEntities _context;

        
        public LDController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }                                                       
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }

        public ActionResult FPCRRRBalesDetails()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();
        }



        [HttpGet]
        // GET: LD/Crop
        public ActionResult OrgnCode()
        {
            return View();
        }

       

        [HttpGet]
        // GET: LD/Variety
        public ActionResult Variety()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER  where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            string json = JsonConvert.SerializeObject(ViewBag.GPIL_ORGN_MASTER.ToArray());
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        // GET: LD/Variety
        public ActionResult FarmerCode(string headerid)
        {
            string json = "";
            try
            {
                var result = (from s in _context.GPIL_TAP_FARM_PURCHS_DTLS where s.HEADER_ID == headerid orderby int.Parse(s.TB_LOT_NO) select new { FARMER_LOT = s.TB_LOT_NO + " - " + s.FARMER_CODE, s.FARMER_CODE }).Distinct().ToList();
                json = JsonConvert.SerializeObject(result.ToArray());
            }
            catch(Exception ex)
            { }
           
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        DataSet ds = new DataSet();
        public ActionResult ReportBales(string orgnCode, string farmerCode)
        {
            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
                reportViewer.ShowPrintButton = true;
                reportViewer.Width = Unit.Percentage(900);
                reportViewer.Height = Unit.Percentage(900);
                ds = rdlcReport.GetPurchaseSlip(orgnCode, farmerCode, "CURDATE");//("P1520170609", "HDBU10050004", "CURDATE"); //
                reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"CrystalReport\PurchaseSlip.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("FARMER_PURCHASESLIP", ds.Tables[0]));
                reportViewer.LocalReport.Refresh();
                ViewBag.ReportViewer = reportViewer;
               
            }
            catch(Exception ex)
            { }
            //return PartialView("ReportBales");
            return View();
        }

        [HttpGet]
        // GET: LD/PendingBales/a/a
        public ActionResult PendingBales(string headerid, string ReportType)
        {

            string RejectionType = "OK";
            //if (ReportType == "Un_Weighment")
            //    ReportType = "a.NET_WT";
            //else if ((ReportType == "Non_Purchaseable"))
            //    ReportType = "a.RATE";
            //else
                
            _context.Configuration.ProxyCreationEnabled = false;

            if (ReportType == "Un_Weighment")
            {
                var result = _context.GPIL_TAP_FARM_PURCHS_DTLS.Select(
                         a => new
                         {
                             a.GPIL_BALE_NUMBER,
                             a.REJE_STATUS,
                             a.TB_LOT_NO,
                             a.ATTRIBUTE3,
                             a.FARMER_CODE,
                             a.NET_WT,
                             a.ATTRIBUTE4,
                             a.RATE,
                             a.BUYER_GRADE,
                             a.ATTRIBUTE2,
                             a.HEADER_ID
                         }).OrderBy(a => a.TB_LOT_NO).ThenBy(a => a.ATTRIBUTE3).Where(a => a.HEADER_ID == headerid && a.REJE_STATUS == RejectionType && a.NET_WT == null);
                ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
                string json = JsonConvert.SerializeObject(result.ToArray());
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else if (ReportType == "Non_Purchaseable")
            {
                var result = _context.GPIL_TAP_FARM_PURCHS_DTLS.Select(
                         a => new
                         {
                             a.GPIL_BALE_NUMBER,
                             a.REJE_STATUS,
                             a.TB_LOT_NO,
                             a.ATTRIBUTE3,
                             a.FARMER_CODE,
                             a.NET_WT,
                             a.ATTRIBUTE4,
                             a.RATE,
                             a.BUYER_GRADE,
                             a.ATTRIBUTE2,
                             a.HEADER_ID
                         }).OrderBy(a => a.TB_LOT_NO).ThenBy(a => a.ATTRIBUTE3).Where(a => a.HEADER_ID == headerid && a.REJE_STATUS == RejectionType && a.RATE == null);
                ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
                string json = JsonConvert.SerializeObject(result.ToArray());
                return Json(json, JsonRequestBehavior.AllowGet);
            }
           else
            {
                RejectionType = "RJ";
                var result = _context.GPIL_TAP_FARM_PURCHS_DTLS.Select(
                         a => new
                         {
                             a.GPIL_BALE_NUMBER,
                             a.REJE_STATUS,
                             a.TB_LOT_NO,
                             a.ATTRIBUTE3,
                             a.FARMER_CODE,
                             a.NET_WT,
                             a.ATTRIBUTE4,
                             a.RATE,
                             a.BUYER_GRADE,
                             a.ATTRIBUTE2,
                             a.HEADER_ID
                         }).OrderBy(a => a.TB_LOT_NO).ThenBy(a => a.ATTRIBUTE3).Where(a => a.HEADER_ID == headerid && a.REJE_STATUS == RejectionType);
                ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
                string json = JsonConvert.SerializeObject(result.ToArray());
                return Json(json, JsonRequestBehavior.AllowGet);
            }

            return View();

        }

        public ActionResult FarmerwisePurchaseSummary()
        {
            return View();
        }


        [HttpGet]
        // GET: LD/Crop
        public ActionResult Crop()
        {
            ViewBag.GPIL_CROP_MASTER = (from s in _context.GPIL_CROP_MASTER where s.STATUS == "Y" select new { CROPS = s.CROP + " - " + s.CROP_YEAR, s.CROP }).ToList();
            string json = JsonConvert.SerializeObject(ViewBag.GPIL_CROP_MASTER.ToArray());
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        // GET: LD/Vareities
        public ActionResult Vareities()
        {
            ViewBag.GPIL_CROP_MASTER = (from s in _context.GPIL_VARIETY_MASTER where s.STATUS == "Y" select new { VARIETIES = s.VARIETY + " - " + s.VARIETY_NAME, s.VARIETY }).ToList();
            string json = JsonConvert.SerializeObject(ViewBag.GPIL_CROP_MASTER.ToArray());
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FarmerwisePurchaseSummaryDetails(string strCrop, string strVariety)
        {
            try
            {
                ds = rdlcReport.FarmerPurchaseSummary(strCrop, strVariety);
                var data = ds;
                var json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch
            {

            }
            return Json(ds);

        }


        [HttpGet]

        public JsonResult FarmerPurchaseCRRRBalesDetails(string FromDate, string ToDate, string Crop, string Variety)
        {
            DataSet ds = new DataSet();
            LDManagement ldMgt = new LDManagement();
            try
            {
                ds = ldMgt.GetFarmerCRRRBaleDetails(FromDate, ToDate, Crop, Variety);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


            }
            catch (Exception ex)
            { }
            return Json(ds);
        }





        public ActionResult TTDCRRRDetails()
        {
            //ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();

        }

        public JsonResult CRRRDetails(string fromdate, string crop, string variety)
        {
            DataSet ds = new DataSet();
            LDManagement ldMgt = new LDManagement();
            try
            {
                ds = ldMgt.GetTTDCRRRDetails(fromdate, crop, variety);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


            }
            catch (Exception ex)
            { }
            return Json(ds);
        }


        [HttpGet]
        // GET: LD/Crop
        public ActionResult FarmerPurchaseSlipwithDate()
        {
            return View();
        }



        [HttpGet]
        // GET: LD/Crop
       
        



        public ActionResult FarmerPurchaseSlip()
        {
            return View();
        }
        [HttpGet]
        public JsonResult FarmerPurchaseSlipTest(string strCrop, string strVariety)
        {
            try
            {
                ds = rdlcReport.FarmerPurchaseSummary(strCrop, strVariety);
                var data = ds;
                var json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch
            {

            }
            return Json(ds);
        }


        [HttpGet]
        // GET: LD/Crop





        public ActionResult FarmerPurchaseSlipFinal()
        {
            return View();
        }


    }
}