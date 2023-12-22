using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using GPILWebApp.ViewModel.ExcelUpload;


namespace GPILWebApp.Controllers
{
    
    public class VillageController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        
        // GET: Village
        public ActionResult Index()
        {
            var gPIL_VILLAGE_MASTER = db.GPIL_VILLAGE_MASTER.Where(g => g.STATUS == "Y" && g.VILLAGE_CODE != null).Include(g => g.GPIL_CLUSTER_MASTER).Include(g => g.GPIL_USER_MASTER);
            return View(gPIL_VILLAGE_MASTER.ToList());
        }

        // GET: Village/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER = db.GPIL_VILLAGE_MASTER.Find(id);
            if (gPIL_VILLAGE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_VILLAGE_MASTER);
        }

        // GET: Village/Create
        public ActionResult Create()
        {
            ViewBag.CLUSTER_CODE = new SelectList(db.GPIL_CLUSTER_MASTER, "CLUSTER_CODE", "CLUSTER_CODE");
            return View();
        }

        // POST: Village/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,VILLAGE_CODE,VILLAGE_NAME,CLUSTER_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.GPIL_VILLAGE_MASTER.Add(gPIL_VILLAGE_MASTER);
                    db.SaveChanges();
                    ModelState.Clear();
                    // return RedirectToAction("Index");
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

            ViewBag.CLUSTER_CODE = new SelectList(db.GPIL_CLUSTER_MASTER, "CLUSTER_CODE", "CLUSTER_CODE", gPIL_VILLAGE_MASTER.CLUSTER_CODE);
            return View("Create");
        }

        // GET: Village/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER = db.GPIL_VILLAGE_MASTER.Find(id);
            ViewBag.CLUSTER_CODE = new SelectList(db.GPIL_CLUSTER_MASTER, "CLUSTER_CODE", "CLUSTER_CODE", gPIL_VILLAGE_MASTER.CLUSTER_CODE);
            try
            {

                if (gPIL_VILLAGE_MASTER == null)
                {
                    return HttpNotFound();
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
            return View(gPIL_VILLAGE_MASTER);
        }

        // POST: Village/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,VILLAGE_CODE,VILLAGE_NAME,CLUSTER_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(gPIL_VILLAGE_MASTER).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
            ViewBag.CLUSTER_CODE = new SelectList(db.GPIL_CLUSTER_MASTER, "CLUSTER_CODE", "CLUSTER_CODE");
            return View(gPIL_VILLAGE_MASTER);
        }

        // GET: Village/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER = db.GPIL_VILLAGE_MASTER.Find(id);
            if (gPIL_VILLAGE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_VILLAGE_MASTER);
        }

        // POST: Village/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER = db.GPIL_VILLAGE_MASTER.Find(id);
            db.GPIL_VILLAGE_MASTER.Remove(gPIL_VILLAGE_MASTER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        static DataSet ds = new DataSet();

        [HttpPost]
        public ActionResult ImportExcelData(HttpPostedFileBase postedFile)
        {
            var villageList = new List<VillageExcel>();
          
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/ExcelUploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, filePath);
                try
                {
                   
                    string query = "select '0' as ID, VillageCode,VillageName,ClusterCode,Status from [Sheet1$]";
                    OleDbDataAdapter odaExcel = new OleDbDataAdapter(query, conString);
                    odaExcel.Fill(ds);



                   



                    //if (Request != null)
                    //{
                    //    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    //    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    //    {
                    //        string fileName = file.FileName;
                    //        string fileContentType = file.ContentType;
                    //        byte[] fileBytes = new byte[file.ContentLength];
                    //        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    //        using (var package = new ExcelPackage(file.InputStream))
                    //        {
                    //            var currentSheet = package.Workbook.Worksheets;
                    //            var workSheet = currentSheet.First();
                    //            var noOfCol = workSheet.Dimension.End.Column;
                    //            var noOfRow = workSheet.Dimension.End.Row;

                    //            for (int rowIterator = 2; rowIterator <= ds.Tables[0].Rows.Count; rowIterator++)
                    //            {
                    //                var village = new VillageExcel();
                    //                village.VillageCode = ds.Tables[0].Rows[rowIterator][rowIterator, 0].ToString();
                    //                village.VillageName = ds.Tables[0].Rows[rowIterator]["VillageName"].ToString();
                    //                village.ClusterCode = ds.Tables[0].Rows[rowIterator][rowIterator, 2].ToString();
                    //                villageList.Add(village);
                    //            }
                    //        }
                    //        return View("Index", villageList);
                    //    }


                    //}
                    //return View("Index" ,ds);

                }
                catch (Exception ex)
                {
                  
                }

               
            }

            return View(ds);
        }

        public ActionResult GetExcelData()
        {
            return View(ds);
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

