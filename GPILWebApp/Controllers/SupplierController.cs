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
    public class SupplierController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: Supplier
        public ActionResult Index()
        {
            var gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1);
            return View(gPIL_SUPPLIER_MASTER.ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            if (gPIL_SUPPLIER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SUPPLIER_MASTER);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SUPP_CODE,GPIL_SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2")] GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.GPIL_SUPPLIER_MASTER.Add(gPIL_SUPPLIER_MASTER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            return View(gPIL_SUPPLIER_MASTER);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] splitid = id.Split(',');
            GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(splitid[0].ToString(), (splitid[1].ToString()));
            //GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            if (gPIL_SUPPLIER_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            return View(gPIL_SUPPLIER_MASTER);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SUPP_CODE,GPIL_SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2")] GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_SUPPLIER_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            return View(gPIL_SUPPLIER_MASTER);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            if (gPIL_SUPPLIER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SUPPLIER_MASTER);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            db.GPIL_SUPPLIER_MASTER.Remove(gPIL_SUPPLIER_MASTER);
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
