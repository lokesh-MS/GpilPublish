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
    public class OrganizationController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: Organization
        public ActionResult Index()
        {
            var gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1).OrderByDescending(g=> g.CREATED_DATE);
            return View(gPIL_ORGN_MASTER.ToList());
        }

        // GET: Organization/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_ORGN_MASTER gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Find(id);
            if (gPIL_ORGN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_ORGN_MASTER);
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: Organization/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,CREATED_BY,CREATED_DATE,ORGN_COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,SYNC_ID,SYNC_PASSWORD,VARIETY")] GPIL_ORGN_MASTER gPIL_ORGN_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.GPIL_ORGN_MASTER.Add(gPIL_ORGN_MASTER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            return View(gPIL_ORGN_MASTER);
        }

        // GET: Organization/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_ORGN_MASTER gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Find(id);
            if (gPIL_ORGN_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            return View(gPIL_ORGN_MASTER);
        }

        // POST: Organization/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,CREATED_BY,CREATED_DATE,MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,SYNC_ID,SYNC_PASSWORD,VARIETY")] GPIL_ORGN_MASTER gPIL_ORGN_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_ORGN_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            return View(gPIL_ORGN_MASTER);
        }

        // GET: Organization/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_ORGN_MASTER gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Find(id);
            if (gPIL_ORGN_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_ORGN_MASTER);
        }

        // POST: Organization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_ORGN_MASTER gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Find(id);
            db.GPIL_ORGN_MASTER.Remove(gPIL_ORGN_MASTER);
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
