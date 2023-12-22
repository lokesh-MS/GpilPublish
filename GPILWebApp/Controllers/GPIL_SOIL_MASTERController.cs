using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System.Text;
using System.Data.Entity.Validation;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class GPIL_SOIL_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();


        [HttpPost]
        public ActionResult CheckSoilAvailability(string Soildata)
        {
            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_SOIL_MASTER.Where(x => x.SOIL_TYPE == Soildata).SingleOrDefault();
            if (usr != null)
            {
                if (Soildata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,SOIL_TYPE,SOIL_NAME,SOIL_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                query = query + " ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_SOIL_MASTER where SOIL_TYPE='" + Soildata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }
        // GET: GPIL_SOIL_MASTER
        public ActionResult Index()
        {
            //var gPIL_SOIL_MASTER = db.GPIL_SOIL_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1).Include(g => g.GPIL_USER_MASTER2).Include(g => g.GPIL_USER_MASTER3);
            //return View(gPIL_SOIL_MASTER.ToList());

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_SOIL_MASTER.Where(s => s.STATUS == "Y").ToList();

            return View(res);
        }

        // GET: GPIL_SOIL_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SOIL_MASTER gPIL_SOIL_MASTER = db.GPIL_SOIL_MASTER.Find(id);
            if (gPIL_SOIL_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SOIL_MASTER);
        }

        // GET: GPIL_SOIL_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_SOIL_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,SOIL_TYPE,SOIL_NAME,SOIL_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_SOIL_MASTER gPIL_SOIL_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,SOIL_TYPE,SOIL_NAME,SOIL_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + " ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_SOIL_MASTER where SOIL_TYPE='" + gPIL_SOIL_MASTER.SOIL_TYPE + "' ";
                    dtclstr = ppdMgt.GetQueryResult(query);
                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_SOIL_MASTER obj = new GPIL_SOIL_MASTER();
                        obj.SOIL_TYPE = gPIL_SOIL_MASTER.SOIL_TYPE;
                        obj.SOIL_NAME = gPIL_SOIL_MASTER.SOIL_NAME;
                        obj.SOIL_DESC = gPIL_SOIL_MASTER.SOIL_DESC;
                        obj.STATUS = gPIL_SOIL_MASTER.STATUS;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;
                        db.GPIL_SOIL_MASTER.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_SOIL_MASTER obj = (from C in db.GPIL_SOIL_MASTER where C.SOIL_TYPE == gPIL_SOIL_MASTER.SOIL_TYPE select C).Single();
                        obj.SOIL_NAME = gPIL_SOIL_MASTER.SOIL_NAME;
                        obj.SOIL_DESC = gPIL_SOIL_MASTER.SOIL_DESC;
                        obj.STATUS = gPIL_SOIL_MASTER.STATUS;
                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
                        // db.Entry(gPIL_SOIL_MASTER).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                //Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return View(gPIL_SOIL_MASTER);
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            return View(gPIL_SOIL_MASTER);
        }
        // GET: GPIL_SOIL_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SOIL_MASTER gPIL_SOIL_MASTER = db.GPIL_SOIL_MASTER.Find(id);
            if (gPIL_SOIL_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SOIL_MASTER.CREATED_BY);
            return View(gPIL_SOIL_MASTER);
        }

        // POST: GPIL_SOIL_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,SOIL_TYPE,SOIL_NAME,SOIL_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_SOIL_MASTER gPIL_SOIL_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //GPIL_SOIL_MASTER obj = new GPIL_SOIL_MASTER();
                    GPIL_SOIL_MASTER obj = (from C in db.GPIL_SOIL_MASTER where C.SOIL_TYPE == gPIL_SOIL_MASTER.SOIL_TYPE select C).Single();
                    //obj.SOIL_TYPE = gPIL_SOIL_MASTER.SOIL_TYPE;
                    obj.SOIL_NAME = gPIL_SOIL_MASTER.SOIL_NAME;
                    obj.SOIL_DESC = gPIL_SOIL_MASTER.SOIL_DESC;
                    obj.STATUS = gPIL_SOIL_MASTER.STATUS;
                    obj.CREATED_BY = gPIL_SOIL_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_SOIL_MASTER.CREATED_DATE;
                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    //db.Entry(gPIL_SOIL_MASTER).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                return View(gPIL_SOIL_MASTER);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        // GET: GPIL_SOIL_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SOIL_MASTER gPIL_SOIL_MASTER = db.GPIL_SOIL_MASTER.Find(id);
            if (gPIL_SOIL_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SOIL_MASTER);
        }

        // POST: GPIL_SOIL_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_SOIL_MASTER gPIL_SOIL_MASTER = db.GPIL_SOIL_MASTER.Find(id);
            GPIL_SOIL_MASTER obj = (from C in db.GPIL_SOIL_MASTER where C.SOIL_TYPE == gPIL_SOIL_MASTER.SOIL_TYPE select C).Single();
            obj.SOIL_TYPE = gPIL_SOIL_MASTER.SOIL_TYPE;
            obj.SOIL_NAME = gPIL_SOIL_MASTER.SOIL_NAME;
            obj.SOIL_DESC = gPIL_SOIL_MASTER.SOIL_DESC;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_SOIL_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_SOIL_MASTER.CREATED_DATE;
            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_SOIL_MASTER).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //GPIL_SOIL_MASTER gPIL_SOIL_MASTER = db.GPIL_SOIL_MASTER.Find(id);
            //db.GPIL_SOIL_MASTER.Remove(gPIL_SOIL_MASTER);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
        public ActionResult ExcelIndex()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null && postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
                {
                    ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
                }

                else
                {
                    string filePath = string.Empty;
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
                        case ".xls": //For Excel 97-03.  
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //For Excel 07 and above.  
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    try
                    {
                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.  
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.  
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }

                        string json = JsonConvert.SerializeObject(dt);
                        return Json(json, JsonRequestBehavior.AllowGet);
                    }

                    //catch (Exception ex)  
                    //{  
                    //    throw ex;  
                    //}  
                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                    //return RedirectToAction("Index");  
                }
            }
            //return View(postedFile);  
            return View("Index");
        }
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        //[HttpPost]
        //public JsonResult SoilMasterComplete(ListSoilMaster LSM)
        //{
        //    SoilMasterdata(LSM);
        //    return null;
        //}

        [HttpPost]
        public JsonResult SoilMasterComplete(ListSoilMaster LSM)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LSM.SoilMasters);
                string strQry = "";
                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string SOIL_TYPE = dtGridLst.Rows[s]["SOIL_TYPE"].ToString();
                    string SOIL_NAME = dtGridLst.Rows[s]["SOIL_NAME"].ToString();
                    string SOIL_DESC = dtGridLst.Rows[s]["SOIL_DESC"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                    MasterManagement MstrMgt = new MasterManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,SOIL_TYPE,SOIL_NAME,SOIL_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + " ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_SOIL_MASTER where SOIL_TYPE='" + SOIL_TYPE + "' ";
                    dtclstr = MstrMgt.GetQueryResult(query);
                    if (dtclstr.Columns.Contains("ErrorMessage"))
                    {
                        data = "Error: " + dtclstr.Rows[0]["ErrorMessage"].ToString();
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_SOIL_MASTER obj = null;
                        obj = new GPIL_SOIL_MASTER();
                       
                        strQry = "INSERT INTO [dbo].[GPIL_SOIL_MASTER] ([SOIL_TYPE],[SOIL_NAME],[SOIL_DESC],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + SOIL_TYPE + "','" + SOIL_NAME + "','" + SOIL_DESC + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";

                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: Soil Type " + SOIL_TYPE + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
                bool b = GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);
                if (b)
                {
                }
                else
                {
                }
                data = "Succuss";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
