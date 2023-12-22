using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class FarmerController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: Farmer
        public ActionResult Index()
        {
            return View(db.GPIL_FARMER_MASTER.Take(1000).ToList());
        }

        // GET: Farmer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            if (gPIL_FARMER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_MASTER);
        }

        // GET: Farmer/Create
        public ActionResult Create()
        {
            var Crop = db.GPIL_CROP_MASTER
                .Select(i => i.CROP)
                .Distinct();
            ViewBag.Crop = new SelectList(Crop);
           // var va = (from v in db.GPIL_VARIETY_MASTER select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_DESC, v.VARIETY }).Distinct();

            var Variety = db.GPIL_VARIETY_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.VARIETY)
                .Distinct();
            ViewBag.Variety = new SelectList(Variety);

            var VillageCode = db.GPIL_VILLAGE_MASTER
              .Select(i => i.VILLAGE_CODE)
              .Distinct();
            ViewBag.VillageCode = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => (i.SOIL_TYPE))
                .Distinct();
            ViewBag.SoilType = new SelectList(SoilType);


            return View();
        }


        #region Farmer Authorized Quantity Details


        /// <summary>
        /// Farmer Authorized Quantity Details
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerAuthorizedQuantity()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();
        }


        [HttpGet]
        // GET: LD/PendingBales/a/a
        public JsonResult GetFarmerAuthorisedQuantityDetails(string FromDate, string ToDate, string Crop, string Variety)
        {
            DataSet ds = new DataSet();
            LDManagement ldMgt = new LDManagement();
            try
            {
                ds = ldMgt.GetFarmerAuthorisedQty (FromDate, ToDate, Crop, Variety);
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

        #endregion


        #region Farmer Purchase Verification Summary Reports


        /// <summary>
        /// Farmer Authorized Quantity Details
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerPurchaseVerifySummary()
        {
          return View();
        }


        [HttpGet]
        // GET: LD/PendingBales/a/a
        public JsonResult GetFarmerPurchaseVerifySummaryReport(string HeaderID, string ReportType)
        {
            DataSet ds = new DataSet();
            LDManagement ldMgt = new LDManagement();
            try
            {
                ds = ldMgt.GetFarmerPurchaseVerifySummaryReport(HeaderID, ReportType);
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

        #endregion



        #region Farmer Purchase Summary Details


        /// <summary>
        /// Farmer Purchase Summary Details
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerPurchaseSummaryDetails()
        {
            return View();
        }


        [HttpGet]
        
        public JsonResult GetFarmerPurchaseSummaryDetails(string HeaderID, string ReportType)
        {
            DataSet ds = new DataSet();
            LDManagement ldMgt = new LDManagement();
            try
            {
                ds = ldMgt.GetFarmerPurchaseSummaryDetails(HeaderID, ReportType);
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

        #endregion









        // POST: Farmer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,STATUS,LOAN_AMOUNT,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_FARMER_MASTER gPIL_FARMER_MASTER, FormCollection fc)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.GPIL_FARMER_MASTER.Add(gPIL_FARMER_MASTER);
                    db.SaveChanges();

                    GPIL_FARMER_CROP_HISTORY fch = new GPIL_FARMER_CROP_HISTORY();

                    string HisCode = Convert.ToString(fc["ATTRIBUTE1"]) + Convert.ToString(fc["Variety"]) + gPIL_FARMER_MASTER.FARM_CODE;
                    fch.HIS_CODE = HisCode;
                    fch.FARM_CODE = gPIL_FARMER_MASTER.FARM_CODE;
                    fch.CROP = Convert.ToString(fc["ATTRIBUTE1"]);
                    fch.VARIETY = Convert.ToString(fc["Variety"]);
                    fch.MOBILE_NO = gPIL_FARMER_MASTER.MOBILE_NO;
                    fch.EMAIL_ID = gPIL_FARMER_MASTER.EMAIL_ID;
                    fch.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.BANK_ACCOUNT_NO;
                    fch.BANK_NAME = gPIL_FARMER_MASTER.BANK_NAME;
                    fch.BRANCH_NAME = gPIL_FARMER_MASTER.BRANCH_NAME;
                    fch.IFSC_CODE = gPIL_FARMER_MASTER.IFSC_CODE;
                    fch.CREATED_BY = Session["userID"].ToString();
                    fch.CREATED_DATE = DateTime.Now;
                    fch.STATUS = "Y";
                    fch.LOAN_AMOUNT = 0.00;
                    fch.BALANCE_AMOUNT = 0.00;
                    db.GPIL_FARMER_CROP_HISTORY.Add(fch);

                    db.SaveChanges();
                    ModelState.Clear();

                    //return RedirectToAction("Index");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


            var Crop = db.GPIL_CROP_MASTER
                 .Select(i => i.CROP)
                 .Distinct();
            ViewBag.Crop = new SelectList(Crop);

            var Variety = db.GPIL_VARIETY_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.VARIETY)
                .Distinct();
            ViewBag.Variety = new SelectList(Variety);

            var VillageCode = db.GPIL_VILLAGE_MASTER
            .Select(i => i.VILLAGE_CODE)
            .Distinct();
            ViewBag.VillageCode = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.SOIL_TYPE)
                .Distinct();
            ViewBag.SoilType = new SelectList(SoilType);

            return View("Create");
        }

        // GET: Farmer/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Crop = db.GPIL_CROP_MASTER
                .Select(i => i.CROP)
                .Distinct();
            ViewBag.Crop = new SelectList(Crop);

            var Variety = db.GPIL_VARIETY_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.VARIETY)
                .Distinct();
            ViewBag.Variety = new SelectList(Variety);

            var VillageCode = db.GPIL_VILLAGE_MASTER
            .Select(i => i.VILLAGE_CODE)
            .Distinct();
            ViewBag.VillageCode = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.SOIL_TYPE)
                .Distinct();
            ViewBag.SoilType = new SelectList(SoilType);
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            if (gPIL_FARMER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_MASTER);
        }

        // POST: Farmer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE5")] GPIL_FARMER_MASTER gPIL_FARMER_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_FARMER_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Crop = db.GPIL_CROP_MASTER
                .Select(i => i.CROP)
                .Distinct();
            ViewBag.Crop = new SelectList(Crop);

            var Variety = db.GPIL_VARIETY_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.VARIETY)
                .Distinct();
            ViewBag.Variety = new SelectList(Variety);

            var VillageCode = db.GPIL_VILLAGE_MASTER
            .Select(i => i.VILLAGE_CODE)
            .Distinct();
            ViewBag.VillageCode = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER.Where((m => m.STATUS == "Y"))
                .Select(i => i.SOIL_TYPE)
                .Distinct();
            ViewBag.SoilType = new SelectList(SoilType);
            return View(gPIL_FARMER_MASTER);
        }

        // GET: Farmer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            if (gPIL_FARMER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_MASTER);
        }

        // POST: Farmer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            db.GPIL_FARMER_MASTER.Remove(gPIL_FARMER_MASTER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
