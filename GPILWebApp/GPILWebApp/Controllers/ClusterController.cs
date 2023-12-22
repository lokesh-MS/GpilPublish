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
    public class ClusterController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: Cluster
        public ActionResult Index()
        {
            var gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1).Include(g => g.GPIL_USER_MASTER2).Include(g => g.GPIL_USER_MASTER3);
            return View(gPIL_CLUSTER_MASTER.ToList());
        }

        // GET: Cluster/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER.Find(id);
            if (gPIL_CLUSTER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_CLUSTER_MASTER);
        }

        // GET: Cluster/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
           
            return View();
        }

        // POST: Cluster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,STATUS,")] GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.GPIL_CLUSTER_MASTER.Add(gPIL_CLUSTER_MASTER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
           
            return View(gPIL_CLUSTER_MASTER);
        }

        // GET: Cluster/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER.Find(id);
            if (gPIL_CLUSTER_MASTER == null)
            {
                return HttpNotFound();
            }
           
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            return View(gPIL_CLUSTER_MASTER);
        }

        // POST: Cluster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS")] GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_CLUSTER_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
           
            return View(gPIL_CLUSTER_MASTER);
        }

        // GET: Cluster/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER.Find(id);
            if (gPIL_CLUSTER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_CLUSTER_MASTER);
        }

        // POST: Cluster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER.Find(id);
            db.GPIL_CLUSTER_MASTER.Remove(gPIL_CLUSTER_MASTER);
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
