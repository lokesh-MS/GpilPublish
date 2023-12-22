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

    public class GPIL_SUPPLIER_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();


        [HttpPost]
        public ActionResult CheckSupplierAvailability(string Supplirerdata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_SUPPLIER_MASTER.Where(x => x.SUPP_CODE == Supplirerdata).SingleOrDefault();
            if (usr != null)
            {
                if (Supplirerdata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,";
                query = query + "CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,GPIL_SUPP_CODE";
                query = query + " from GPIL_SUPPLIER_MASTER where SUPP_CODE = '" + Supplirerdata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }

        // GET: GPIL_SUPPLIER_MASTER
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_SUPPLIER_MASTER.Where(s => s.STATUS == "Y").ToList();

            return View(res);
        }

        // GET: GPIL_SUPPLIER_MASTER/Details/5
        public ActionResult Details(string id)
        {

            var record = from r in db.GPIL_SUPPLIER_MASTER
                         select r;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            else
            {
                record = record.Where(r => r.SUPP_CODE == id);
            }
            GPIL_SUPPLIER_MASTER rec = record.ToList().First();
            return View(rec);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            //if (gPIL_SUPPLIER_MASTER == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(gPIL_SUPPLIER_MASTER);
        }

        // GET: GPIL_SUPPLIER_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_SUPPLIER_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,GPIL_SUPP_CODE")] GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,";
                    query = query + "CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,GPIL_SUPP_CODE from GPIL_SUPPLIER_MASTER where SUPP_CODE='" + gPIL_SUPPLIER_MASTER.SUPP_CODE + "' ";

                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_SUPPLIER_MASTER obj = new GPIL_SUPPLIER_MASTER();
                        obj.SUPP_CODE = gPIL_SUPPLIER_MASTER.SUPP_CODE;
                        obj.SUPP_NAME = gPIL_SUPPLIER_MASTER.SUPP_NAME;
                        obj.SITE_NAME = gPIL_SUPPLIER_MASTER.SITE_NAME;
                        obj.SUPP_ADDRESS1 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS1;
                        obj.SUPP_ADDRESS2 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS2;
                        obj.SUPP_ADDRESS3 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS3;
                        obj.SUPP_ADDRESS4 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS4;
                        obj.SUPP_ADDRESS5 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS5;
                        obj.SUPP_ADDRESS6 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS6;
                        obj.SUPP_ADDRESS7 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS7;
                        obj.SUPP_ADDRESS8 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS8;
                        obj.TEL_NO = gPIL_SUPPLIER_MASTER.TEL_NO;
                        obj.MOBILE_NO = gPIL_SUPPLIER_MASTER.MOBILE_NO;
                        obj.EMAIL_ID = gPIL_SUPPLIER_MASTER.EMAIL_ID;
                        obj.GPIL_SUPP_CODE = gPIL_SUPPLIER_MASTER.GPIL_SUPP_CODE;
                        obj.ATTRIBUTE1 = gPIL_SUPPLIER_MASTER.ATTRIBUTE1;
                        obj.ATTRIBUTE2 = gPIL_SUPPLIER_MASTER.ATTRIBUTE2;
                        obj.STATUS = gPIL_SUPPLIER_MASTER.STATUS;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_SUPPLIER_MASTER.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["SuccessMessage"] = "Successfully Created!!!";
                        ModelState.Clear();


                    }
                    else
                    {
                        GPIL_SUPPLIER_MASTER obj = (from C in db.GPIL_SUPPLIER_MASTER where C.SUPP_CODE == gPIL_SUPPLIER_MASTER.SUPP_CODE select C).Single();

                        obj.SUPP_NAME = gPIL_SUPPLIER_MASTER.SUPP_NAME;
                        obj.SITE_NAME = gPIL_SUPPLIER_MASTER.SITE_NAME;
                        obj.SUPP_ADDRESS1 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS1;
                        obj.SUPP_ADDRESS2 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS2;
                        obj.SUPP_ADDRESS3 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS3;
                        obj.SUPP_ADDRESS4 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS4;
                        obj.SUPP_ADDRESS5 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS5;
                        obj.SUPP_ADDRESS6 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS6;
                        obj.SUPP_ADDRESS7 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS7;
                        obj.SUPP_ADDRESS8 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS8;
                        obj.TEL_NO = gPIL_SUPPLIER_MASTER.TEL_NO;
                        obj.MOBILE_NO = gPIL_SUPPLIER_MASTER.MOBILE_NO;
                        obj.EMAIL_ID = gPIL_SUPPLIER_MASTER.EMAIL_ID;
                        obj.GPIL_SUPP_CODE = gPIL_SUPPLIER_MASTER.GPIL_SUPP_CODE;
                        obj.ATTRIBUTE1 = gPIL_SUPPLIER_MASTER.ATTRIBUTE1;
                        obj.ATTRIBUTE2 = gPIL_SUPPLIER_MASTER.ATTRIBUTE2;
                        obj.STATUS = gPIL_SUPPLIER_MASTER.STATUS;
                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
                        //db.Entry(gPIL_CLUSTER_MASTER).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Successfully Updated!!!";

                    }
                    // return RedirectToAction("Index");
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
                return View(gPIL_SUPPLIER_MASTER);
            }


            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_SUPPLIER_MASTER.CREATED_BY);

            return View(gPIL_SUPPLIER_MASTER);
        }

        // GET: GPIL_SUPPLIER_MASTER/Edit/5
        public ActionResult Edit(string id)
        {

            var record = from r in db.GPIL_SUPPLIER_MASTER
                         select r;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            else
            {
                record = record.Where(r => r.SUPP_CODE == id);
            }
            GPIL_SUPPLIER_MASTER rec = record.ToList().First();
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", rec.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", rec.CREATED_BY);
            //return View(gPIL_SUPPLIER_MASTER);
            return View(rec);


        }

        // POST: GPIL_SUPPLIER_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,GPIL_SUPP_CODE")] GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_SUPPLIER_MASTER obj = (from C in db.GPIL_SUPPLIER_MASTER where C.SUPP_CODE == gPIL_SUPPLIER_MASTER.SUPP_CODE select C).Single();

                    obj.SUPP_CODE = gPIL_SUPPLIER_MASTER.SUPP_CODE;
                    obj.SUPP_NAME = gPIL_SUPPLIER_MASTER.SUPP_NAME;
                    obj.SITE_NAME = gPIL_SUPPLIER_MASTER.SITE_NAME;

                    obj.SUPP_ADDRESS1 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS1;
                    obj.SUPP_ADDRESS2 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS2;
                    obj.TEL_NO = gPIL_SUPPLIER_MASTER.TEL_NO;
                    obj.MOBILE_NO = gPIL_SUPPLIER_MASTER.MOBILE_NO;
                    obj.EMAIL_ID = gPIL_SUPPLIER_MASTER.EMAIL_ID;

                    obj.STATUS = gPIL_SUPPLIER_MASTER.STATUS;
                    obj.CREATED_BY = gPIL_SUPPLIER_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_SUPPLIER_MASTER.CREATED_DATE;
                    obj.ATTRIBUTE1 = gPIL_SUPPLIER_MASTER.ATTRIBUTE1;
                    obj.ATTRIBUTE2 = gPIL_SUPPLIER_MASTER.ATTRIBUTE2;
                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfully Updated!!!";

                    //return RedirectToAction("Index");

                }
                return View(gPIL_SUPPLIER_MASTER);
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

        // GET: GPIL_SUPPLIER_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.SingleOrDefault(m => m.SUPP_CODE == id);
            //GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            if (gPIL_SUPPLIER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SUPPLIER_MASTER);
        }

        // POST: GPIL_SUPPLIER_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.Find(id);
            GPIL_SUPPLIER_MASTER gPIL_SUPPLIER_MASTER = db.GPIL_SUPPLIER_MASTER.SingleOrDefault(m => m.SUPP_CODE == id);

            GPIL_SUPPLIER_MASTER obj = (from C in db.GPIL_SUPPLIER_MASTER where C.SUPP_CODE == gPIL_SUPPLIER_MASTER.SUPP_CODE select C).Single();

            obj.SUPP_CODE = gPIL_SUPPLIER_MASTER.SUPP_CODE;
            obj.SUPP_NAME = gPIL_SUPPLIER_MASTER.SUPP_NAME;
            obj.SITE_NAME = gPIL_SUPPLIER_MASTER.SITE_NAME;

            obj.SUPP_ADDRESS1 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS1;
            obj.SUPP_ADDRESS2 = gPIL_SUPPLIER_MASTER.SUPP_ADDRESS2;
            obj.TEL_NO = gPIL_SUPPLIER_MASTER.TEL_NO;
            obj.MOBILE_NO = gPIL_SUPPLIER_MASTER.MOBILE_NO;
            obj.EMAIL_ID = gPIL_SUPPLIER_MASTER.EMAIL_ID;

            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_SUPPLIER_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_SUPPLIER_MASTER.CREATED_DATE;

            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_SUPPLIER_MASTER).State = EntityState.Modified;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Successfuly Deleted!!!";
            //return RedirectToAction("Index");
            return View(gPIL_SUPPLIER_MASTER);
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


        //[HttpPost]
        //public JsonResult SupplierMasterComplete(ListSupplierMaster LSM)
        //{
        //    SupplierMasterdata(LSM);
        //    return null;
        //}

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

        MasterManagement MstrMgt = new MasterManagement();
        [HttpPost]
        public JsonResult SupplierMasterComplete(ListSupplierMaster LSM)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LSM.SupplierMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string SUPP_CODE = dtGridLst.Rows[s]["SUPP_CODE"].ToString();
                    string SUPP_NAME = dtGridLst.Rows[s]["SUPP_NAME"].ToString();
                    string SITE_NAME = dtGridLst.Rows[s]["SITE_NAME"].ToString();
                    string SUPP_ADDRESS1 = dtGridLst.Rows[s]["SUPP_ADDRESS1"].ToString();
                    string SUPP_ADDRESS2 = dtGridLst.Rows[s]["SUPP_ADDRESS2"].ToString();

                    string SUPP_ADDRESS3 = dtGridLst.Rows[s]["SUPP_ADDRESS3"].ToString();
                    string SUPP_ADDRESS4 = dtGridLst.Rows[s]["SUPP_ADDRESS4"].ToString();
                    string SUPP_ADDRESS5 = dtGridLst.Rows[s]["SUPP_ADDRESS5"].ToString();
                    string SUPP_ADDRESS6 = dtGridLst.Rows[s]["SUPP_ADDRESS6"].ToString();
                    string SUPP_ADDRESS7 = dtGridLst.Rows[s]["SUPP_ADDRESS7"].ToString();
                    string SUPP_ADDRESS8 = dtGridLst.Rows[s]["SUPP_ADDRESS8"].ToString();
                    string TEL_NO = dtGridLst.Rows[s]["TEL_NO"].ToString();
                    string MOBILE_NO = dtGridLst.Rows[s]["MOBILE_NO"].ToString();
                    string EMAIL_ID = dtGridLst.Rows[s]["EMAIL_ID"].ToString();
                    string ATTRIBUTE1 = dtGridLst.Rows[s]["ATTRIBUTE1"].ToString();
                    string ATTRIBUTE2 = dtGridLst.Rows[s]["ATTRIBUTE2"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                    string GPIL_SUPP_CODE = dtGridLst.Rows[s]["GPIL_SUPP_CODE"].ToString();


                    if (SUPP_CODE == "")
                    {
                        data = "Error: Supplier code should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (SUPP_NAME == "")
                    {
                        data = "Error: Supplier name should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    if (SITE_NAME == "")
                    {
                        data = "Error: Site name should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    //else if (SUPP_ADDRESS1 == "")
                    //{
                    //    data = "Error: Supplier Address 1 should not be Empty";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;
                    //}
                    else if (STATUS == "")
                    {
                        data = "Error: Status should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }




                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,SUPP_CODE,SUPP_NAME,SITE_NAME,SUPP_ADDRESS1,SUPP_ADDRESS2,SUPP_ADDRESS3,SUPP_ADDRESS4,SUPP_ADDRESS5,SUPP_ADDRESS6,SUPP_ADDRESS7,SUPP_ADDRESS8,TEL_NO,MOBILE_NO,EMAIL_ID,";
                    query = query + "CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,GPIL_SUPP_CODE from GPIL_SUPPLIER_MASTER where SUPP_CODE='" + SUPP_CODE + "' ";
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
                        GPIL_SUPPLIER_MASTER obj = null;
                        obj = new GPIL_SUPPLIER_MASTER();

                        strQry = "INSERT INTO [dbo].[GPIL_SUPPLIER_MASTER] ([SUPP_CODE],[SUPP_NAME],[SITE_NAME],[SUPP_ADDRESS1],[SUPP_ADDRESS2],[MOBILE_NO],[EMAIL_ID],[ATTRIBUTE1],[ATTRIBUTE2],[CREATED_BY],[CREATED_DATE],[STATUS],[GPIL_SUPP_CODE]) ";
                        strQry = strQry + "VALUES('" + SUPP_CODE + "','" + SUPP_NAME + "','" + SITE_NAME + "','" + SUPP_ADDRESS1 + "','" + SUPP_ADDRESS2 + "','" + MOBILE_NO + "','" + EMAIL_ID + "','" + ATTRIBUTE1 + "','" + ATTRIBUTE2 + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "','" + GPIL_SUPP_CODE + "')";

                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: Supplier Code " + SUPP_CODE + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                }
                bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);

                if (b)
                {
                    data = "Succuss";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    return Json("Error: Please check the excel sheet", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }

    }
}
