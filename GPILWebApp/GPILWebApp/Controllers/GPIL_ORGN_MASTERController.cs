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
using System.ComponentModel;
using System.Text;
using System.Data.Entity.Validation;

namespace GPILWebApp.Controllers
{
    public class GPIL_ORGN_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        public const string SessionKeyName = "userID";
        public DateTime SessionKeyAge = DateTime.Now;



        [HttpPost]
        public ActionResult CheckOrgnAvailability(string Orgndata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_ORGN_MASTER.Where(x => x.ORGN_CODE == Orgndata).SingleOrDefault();
            if (usr != null)
            {
                if (Orgndata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " select SNO,ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,";
                query = query + " MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,ATTRIBUTE6,ATTRIBUTE7,ATTRIBUTE8,CREATED_BY,";
                query = query + " CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,SYNC_ID,SYNC_PASSWORD,VARIETY ";
                query = query + " from GPIL_ORGN_MASTER where ORGN_CODE = '" + Orgndata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }

        // GET: GPIL_ORGN_MASTER
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_ORGN_MASTER.Where(s => s.STATUS == "Y").ToList();

            return View(res);
            //var gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1);
            //return View(gPIL_ORGN_MASTER.ToList());
        }

        // GET: GPIL_ORGN_MASTER/Details/5
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

        // GET: GPIL_ORGN_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_ORGN_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,ATTRIBUTE6,ATTRIBUTE7,ATTRIBUTE8,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,SYNC_ID,SYNC_PASSWORD,VARIETY")] GPIL_ORGN_MASTER gPIL_ORGN_MASTER)
        {
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " select SNO,ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,";
                    query = query + " MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,ATTRIBUTE6,ATTRIBUTE7,ATTRIBUTE8,CREATED_BY,";
                    query = query + " CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,SYNC_ID,SYNC_PASSWORD,VARIETY ";
                    query = query + " from GPIL_ORGN_MASTER where ORGN_CODE = '" + gPIL_ORGN_MASTER.ORGN_CODE + "' ";
                    dtclstr = ppdMgt.GetQueryResult(query);



                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_ORGN_MASTER obj = new GPIL_ORGN_MASTER();
                        obj.ORGN_CODE = gPIL_ORGN_MASTER.ORGN_CODE;
                        obj.ORGN_NAME = gPIL_ORGN_MASTER.ORGN_NAME;
                        obj.ORGN_TYPE = gPIL_ORGN_MASTER.ORGN_TYPE;
                        obj.ORGN_ADDRESS1 = gPIL_ORGN_MASTER.ORGN_ADDRESS1;
                        obj.ORGN_ADDRESS2 = gPIL_ORGN_MASTER.ORGN_ADDRESS2;
                        obj.ORGN_ADDRESS3 = gPIL_ORGN_MASTER.ORGN_ADDRESS3;
                        obj.ORGN_ADDRESS4 = gPIL_ORGN_MASTER.ORGN_ADDRESS4;
                        obj.ORGN_ADDRESS5 = gPIL_ORGN_MASTER.ORGN_ADDRESS5;
                        obj.ORGN_ADDRESS6 = gPIL_ORGN_MASTER.ORGN_ADDRESS6;

                        obj.ORGN_COUNTRY = gPIL_ORGN_MASTER.ORGN_COUNTRY;
                        obj.PIN_CODE = gPIL_ORGN_MASTER.PIN_CODE;
                        obj.TEL_NO = gPIL_ORGN_MASTER.TEL_NO;
                        obj.MOBILE_NO = gPIL_ORGN_MASTER.MOBILE_NO;
                        obj.EMAIL_ID = gPIL_ORGN_MASTER.EMAIL_ID;
                        obj.INSURANCE_VAL = gPIL_ORGN_MASTER.INSURANCE_VAL;
                        obj.SYNC_ID = "1";
                        obj.SYNC_PASSWORD = "1";
                        obj.STATUS = gPIL_ORGN_MASTER.STATUS;
                        obj.VARIETY = gPIL_ORGN_MASTER.VARIETY;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_ORGN_MASTER.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_ORGN_MASTER obj = (from C in db.GPIL_ORGN_MASTER where C.ORGN_CODE == gPIL_ORGN_MASTER.ORGN_CODE select C).Single();
                        obj.ORGN_NAME = gPIL_ORGN_MASTER.ORGN_NAME;
                        obj.ORGN_TYPE = gPIL_ORGN_MASTER.ORGN_TYPE;
                        obj.ORGN_ADDRESS1 = gPIL_ORGN_MASTER.ORGN_ADDRESS1;
                        obj.ORGN_ADDRESS2 = gPIL_ORGN_MASTER.ORGN_ADDRESS2;
                        obj.ORGN_ADDRESS3 = gPIL_ORGN_MASTER.ORGN_ADDRESS3;
                        obj.ORGN_ADDRESS4 = gPIL_ORGN_MASTER.ORGN_ADDRESS4;
                        obj.ORGN_ADDRESS5 = gPIL_ORGN_MASTER.ORGN_ADDRESS5;
                        obj.ORGN_ADDRESS6 = gPIL_ORGN_MASTER.ORGN_ADDRESS6;

                        obj.ORGN_COUNTRY = gPIL_ORGN_MASTER.ORGN_COUNTRY;
                        obj.PIN_CODE = gPIL_ORGN_MASTER.PIN_CODE;
                        obj.TEL_NO = gPIL_ORGN_MASTER.TEL_NO;
                        obj.MOBILE_NO = gPIL_ORGN_MASTER.MOBILE_NO;
                        obj.EMAIL_ID = gPIL_ORGN_MASTER.EMAIL_ID;
                        obj.INSURANCE_VAL = gPIL_ORGN_MASTER.INSURANCE_VAL;

                        obj.SYNC_ID = "1";
                        obj.SYNC_PASSWORD = "1";

                        obj.STATUS = gPIL_ORGN_MASTER.STATUS;
                        obj.VARIETY = gPIL_ORGN_MASTER.VARIETY;

                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
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
                return View(gPIL_ORGN_MASTER);
            }


            return View(gPIL_ORGN_MASTER);
        }

        // GET: GPIL_ORGN_MASTER/Edit/5
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
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ORGN_MASTER.CREATED_BY);
            return View(gPIL_ORGN_MASTER);
        }

        // POST: GPIL_ORGN_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,ATTRIBUTE6,ATTRIBUTE7,ATTRIBUTE8,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,SYNC_ID,SYNC_PASSWORD,VARIETY")] GPIL_ORGN_MASTER gPIL_ORGN_MASTER)
        {
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            try
            {
                if (ModelState.IsValid)
                {

                    //GPIL_ORGN_MASTER obj = new GPIL_ORGN_MASTER();
                    GPIL_ORGN_MASTER obj = (from C in db.GPIL_ORGN_MASTER where C.ORGN_CODE == gPIL_ORGN_MASTER.ORGN_CODE select C).Single();
                    obj.ORGN_TYPE = gPIL_ORGN_MASTER.ORGN_TYPE;
                    obj.ORGN_NAME = gPIL_ORGN_MASTER.ORGN_NAME;

                    obj.ORGN_ADDRESS1 = gPIL_ORGN_MASTER.ORGN_ADDRESS1;
                    obj.ORGN_ADDRESS2 = gPIL_ORGN_MASTER.ORGN_ADDRESS2;
                    obj.ORGN_ADDRESS3 = gPIL_ORGN_MASTER.ORGN_ADDRESS3;
                    obj.ORGN_ADDRESS4 = gPIL_ORGN_MASTER.ORGN_ADDRESS4;

                    obj.ORGN_COUNTRY = gPIL_ORGN_MASTER.ORGN_COUNTRY;
                    obj.PIN_CODE = gPIL_ORGN_MASTER.PIN_CODE;
                    obj.TEL_NO = gPIL_ORGN_MASTER.TEL_NO;
                    obj.MOBILE_NO = gPIL_ORGN_MASTER.MOBILE_NO;
                    obj.EMAIL_ID = gPIL_ORGN_MASTER.EMAIL_ID;
                    obj.INSURANCE_VAL = gPIL_ORGN_MASTER.INSURANCE_VAL;

                    obj.STATUS = gPIL_ORGN_MASTER.STATUS;
                    obj.CREATED_BY = gPIL_ORGN_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_ORGN_MASTER.CREATED_DATE;
                    obj.SYNC_ID = "1";
                    obj.SYNC_PASSWORD = "1";
                    obj.VARIETY = gPIL_ORGN_MASTER.VARIETY;

                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    //db.Entry(gPIL_ORGN_MASTER).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(gPIL_ORGN_MASTER);
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

        // GET: GPIL_ORGN_MASTER/Delete/5
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

        // POST: GPIL_ORGN_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            GPIL_ORGN_MASTER gPIL_ORGN_MASTER = db.GPIL_ORGN_MASTER.Find(id);

            GPIL_ORGN_MASTER obj = (from C in db.GPIL_ORGN_MASTER where C.ORGN_CODE == gPIL_ORGN_MASTER.ORGN_CODE select C).Single();
            obj.ORGN_CODE = gPIL_ORGN_MASTER.ORGN_CODE;
            obj.ORGN_TYPE = gPIL_ORGN_MASTER.ORGN_TYPE;
            obj.ORGN_NAME = gPIL_ORGN_MASTER.ORGN_NAME;

            obj.ORGN_ADDRESS1 = gPIL_ORGN_MASTER.ORGN_ADDRESS1;
            obj.ORGN_ADDRESS2 = gPIL_ORGN_MASTER.ORGN_ADDRESS2;
            obj.ORGN_ADDRESS3 = gPIL_ORGN_MASTER.ORGN_ADDRESS3;
            obj.ORGN_ADDRESS4 = gPIL_ORGN_MASTER.ORGN_ADDRESS4;

            obj.ORGN_COUNTRY = gPIL_ORGN_MASTER.ORGN_COUNTRY;
            obj.PIN_CODE = gPIL_ORGN_MASTER.PIN_CODE;
            obj.TEL_NO = gPIL_ORGN_MASTER.TEL_NO;
            obj.MOBILE_NO = gPIL_ORGN_MASTER.MOBILE_NO;
            obj.EMAIL_ID = gPIL_ORGN_MASTER.EMAIL_ID;
            obj.INSURANCE_VAL = (gPIL_ORGN_MASTER.INSURANCE_VAL);

            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_ORGN_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_ORGN_MASTER.CREATED_DATE;
            obj.SYNC_ID = "1";
            obj.SYNC_PASSWORD = "1";
            obj.VARIETY = gPIL_ORGN_MASTER.VARIETY;

            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_ORGN_MASTER).State = EntityState.Modified;
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
        //public JsonResult OrganizationMasterComplete(ListOrganizationMaster LOM)
        //{
        //    OrganizationMasterdata(LOM);
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
        public JsonResult OrganizationMasterComplete(ListOrganizationMaster LOM)
        {

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LOM.OrganizationMasters);
                string strQry = "";


                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string ORGN_CODE = dtGridLst.Rows[s]["ORGN_CODE"].ToString();

                    string ORGN_NAME = dtGridLst.Rows[s]["ORGN_NAME"].ToString();
                    string ORGN_TYPE = dtGridLst.Rows[s]["ORGN_TYPE"].ToString();
                    string ORGN_ADDRESS1 = dtGridLst.Rows[s]["ORGN_ADDRESS1"].ToString();
                    string ORGN_ADDRESS2 = dtGridLst.Rows[s]["ORGN_ADDRESS2"].ToString();
                    string ORGN_ADDRESS3 = dtGridLst.Rows[s]["ORGN_ADDRESS3"].ToString();
                    string ORGN_COUNTRY = dtGridLst.Rows[s]["ORGN_COUNTRY"].ToString();
                    string PIN_CODE = dtGridLst.Rows[s]["PIN_CODE"].ToString();
                    string TEL_NO = dtGridLst.Rows[s]["TEL_NO"].ToString();
                    string MOBILE_NO = dtGridLst.Rows[s]["MOBILE_NO"].ToString();
                    string EMAIL_ID = dtGridLst.Rows[s]["EMAIL_ID"].ToString();
                    string INSURANCE_VAL = dtGridLst.Rows[s]["INSURANCE_VAL"].ToString();
                    string SYNC_ID = dtGridLst.Rows[s]["SYNC_ID"].ToString();
                    string SYNC_PASSWORD = dtGridLst.Rows[s]["SYNC_PASSWORD"].ToString();
                    string VARIETY = dtGridLst.Rows[s]["VARIETY"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                    
                    DataTable dtclstr = new DataTable();

                    if (ORGN_NAME.Trim() == string.Empty || ORGN_NAME.Length > 50)
                    {
                        data = "Error: Organization Name should not be empty for" + ORGN_CODE + " and length must be less or equal to 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (VARIETY.Trim() == string.Empty || VARIETY.Length > 2)
                    {
                        data = "Error: Variety should not be empty for" + ORGN_CODE + " and length must be less or equal to 2";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ORGN_TYPE.Trim() == string.Empty || ORGN_TYPE.Length > 50)
                    {
                        data = "Error: Organization Type should not be empty for" + ORGN_CODE + " and length must be less or equal to 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ORGN_ADDRESS1.Trim() == string.Empty || ORGN_ADDRESS1.Length > 50)
                    {
                        data = "Error: Orgn Address1 should not be empty for" + ORGN_CODE + " and length must be less or equal to 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ORGN_ADDRESS2.Trim() == string.Empty || ORGN_ADDRESS2.Length > 50)
                    {
                        data = "Error: Orgn Address2 should not be empty for" + ORGN_CODE + " and length must be less or equal to 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ORGN_COUNTRY.Trim() == string.Empty || ORGN_COUNTRY.Length > 50)
                    {
                        data = "Error:  Orgn Country should not be empty for" + ORGN_CODE + " and length must be less or equal to 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (PIN_CODE.Trim() == string.Empty || PIN_CODE.Length > 6)
                    {
                        data = "Error:  Pincode should not be empty for" + ORGN_CODE + " and length must be less or equal to 6";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (TEL_NO.Trim() == string.Empty || TEL_NO.Length > 15)
                    {
                        data = "Error: Telephone Number should not be empty for" + ORGN_CODE + " and length must be less or equal to 15";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (EMAIL_ID.Trim() == string.Empty || EMAIL_ID.Length > 50)
                    {
                        data = "Error: Email ID should not be empty for" + ORGN_CODE + " and length must be less or equal to 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (INSURANCE_VAL.Trim() == string.Empty)
                    {
                        data = "Error: Insurance Value should not be empty for" + ORGN_CODE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    string query = "";
                    query = " select SNO,ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,";
                    query = query + " MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,ATTRIBUTE6,ATTRIBUTE7,ATTRIBUTE8,CREATED_BY,";
                    query = query + " CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,SYNC_ID,SYNC_PASSWORD,VARIETY ";
                    query = query + " from GPIL_ORGN_MASTER where ORGN_CODE = '" + ORGN_CODE + "' ";
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
                        GPIL_ORGN_MASTER obj = null;
                        obj = new GPIL_ORGN_MASTER();
                        strQry = "INSERT INTO [dbo].[GPIL_ORGN_MASTER] ([ORGN_CODE],[ORGN_NAME],[ORGN_TYPE],[ORGN_ADDRESS1],[ORGN_ADDRESS2],[ORGN_ADDRESS3],[ORGN_COUNTRY],[PIN_CODE],[TEL_NO],[MOBILE_NO],[EMAIL_ID],[INSURANCE_VAL],[CREATED_BY],[CREATED_DATE],[SYNC_ID],[SYNC_PASSWORD],[VARIETY],[STATUS]) ";
                        strQry = strQry + "VALUES('" + ORGN_CODE + "','" + ORGN_NAME + "','" + ORGN_TYPE + "','" + ORGN_ADDRESS1 + "','" + ORGN_ADDRESS2 + "','" + ORGN_ADDRESS3 + "','" + ORGN_COUNTRY + "','" + PIN_CODE + "','" + TEL_NO + "','" + MOBILE_NO + "','" + EMAIL_ID + "','" + INSURANCE_VAL + "','" + Session["userID"].ToString() + "',getdate(),'" + SYNC_ID + "','" + SYNC_PASSWORD + "','" + VARIETY + "','" + STATUS + "')";

                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: Organization Code " + ORGN_CODE + " is already exist So please check and import";
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
                    return Json("Error: Please Check Your Excel and Upload!!!!" + JsonRequestBehavior.AllowGet);
                }
                


            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }


        }


    }
}
