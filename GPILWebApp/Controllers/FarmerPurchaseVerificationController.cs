using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class FarmerPurchaseVerificationController : Controller
    {



        private GREEN_LEAF_TRACEABILITYEntities _context;


        public FarmerPurchaseVerificationController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: FarmerPurchaseVerification
        public ActionResult FarmerPurchaseIndex()
        {
            //from s in test.GPIL_TAP_FARM_PURCHS_HDRs where s.STATUS == "P" && s.PURCHASE_TYPE == "SUNDRY PURCHASE" select new { s.PURCH_DOC_NO }).Distinct();
            ViewBag.GPIL_TAP_FARM_PURCHS_HDR = (from s in _context.GPIL_TAP_FARM_PURCHS_HDR where s.STATUS == "P" && s.PURCHASE_TYPE == "SUNDRY PURCHASE" select new { s.PURCH_DOC_NO }).Distinct();

            return View();
        }



        [HttpPost]
        public JsonResult FarmerPurchaceBW(string orgnCodeList, string poNumberList)
        {
            Session["poNumber"] = poNumberList;
            Session["orgnCode"] = orgnCodeList;

            return Json(new { result = "Redirect", url = Url.Action("FarmerPurchaseBaleWiseIndex", "Verification") });
        }

        public ActionResult FarmerPurchaseBaleWiseIndex()
        {
            ViewBag.Title = "FarmerPurchaseBaleWiseIndex";
            ViewBag.poNumber = (string)Session["poNumber"];
            ViewBag.orgnCode = (string)Session["orgnCode"];

            DataSet ds = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                ds = ldMgt.GetFarmerPurchaseBaleWise(ViewBag.poNumber, ViewBag.orgnCode);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return View(jsonResult);
            }
            catch (Exception ex)
            { }
            return View("FarmerPurchaseBaleWiseIndex");
        }


        public JsonResult FarmerPurchase(string poNumber)
        {
            DataSet ds = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                ds = ldMgt.GetFarmerPurchase(poNumber);
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



    }
}