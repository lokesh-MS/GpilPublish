using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;

namespace GPILWebApp.Controllers
{
    public class GPIL_Chemistry_ReportsController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: GPIL_Chemistry_Reports
        public ActionResult Index()
        {
            return View(db.GPIL_Chemistry_Reports.ToList());
        }

        // GET: GPIL_Chemistry_Reports/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_Chemistry_Reports gPIL_Chemistry_Reports = db.GPIL_Chemistry_Reports.Find(id);
            if (gPIL_Chemistry_Reports == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_Chemistry_Reports);
        }

        // GET: GPIL_Chemistry_Reports/Create
        public ActionResult Create()
        {
            ViewBag.GPIL_Chemical_Target = (from s in db.GPIL_Chemical_Targets select new { s.Crop }).Distinct();
            ViewBag.GPIL_Chemical_Targets = (from s in db.GPIL_Chemical_Targets select new { s.Variety }).Distinct();
            ViewBag.GPIL_Chemical_Targetss = (from s in db.GPIL_Chemical_Targets select new { s.Grade }).Distinct();
            return View();
        }

        // POST: GPIL_Chemistry_Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DOP,Crop,Variety,Grade,Mark,SourceOrganisation,Product,Dom_Exp,Type,From_Run_No,To_Run_No,NIC,TRS,CL,MoisturePercent,Attrbute1,Attrbute2,Attribute3,Attribute4,Attribute5")] GPIL_Chemistry_Reports gPIL_Chemistry_Reports)
        {
            if (ModelState.IsValid)
            {
                db.GPIL_Chemistry_Reports.Add(gPIL_Chemistry_Reports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gPIL_Chemistry_Reports);
        }

        // GET: GPIL_Chemistry_Reports/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_Chemistry_Reports gPIL_Chemistry_Reports = db.GPIL_Chemistry_Reports.Find(id);
            if (gPIL_Chemistry_Reports == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_Chemistry_Reports);
        }

        // POST: GPIL_Chemistry_Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DOP,Crop,Variety,Grade,Mark,SourceOrganisation,Product,Dom_Exp,Type,From_Run_No,To_Run_No,NIC,TRS,CL,MoisturePercent,Attrbute1,Attrbute2,Attribute3,Attribute4,Attribute5")] GPIL_Chemistry_Reports gPIL_Chemistry_Reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_Chemistry_Reports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gPIL_Chemistry_Reports);
        }

        // GET: GPIL_Chemistry_Reports/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_Chemistry_Reports gPIL_Chemistry_Reports = db.GPIL_Chemistry_Reports.Find(id);
            if (gPIL_Chemistry_Reports == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_Chemistry_Reports);
        }

        // POST: GPIL_Chemistry_Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            GPIL_Chemistry_Reports gPIL_Chemistry_Reports = db.GPIL_Chemistry_Reports.Find(id);
            db.GPIL_Chemistry_Reports.Remove(gPIL_Chemistry_Reports);
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
