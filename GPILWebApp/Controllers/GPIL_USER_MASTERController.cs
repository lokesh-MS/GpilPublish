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
using GPILWebApp.REPOSITORY;
using Newtonsoft.Json;
using GPILWebApp.ViewModel;
using System.Text;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{

    public class GPIL_USER_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        [HttpPost]
        public ActionResult CheckUserAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_USER_MASTER.Where(x => x.USER_ID == userdata).SingleOrDefault();
            if (usr != null)
            {
                if (userdata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,[USER_ID],[USER_NAME],[PASSWORD],USER_ERP_NAME,EMP_CODE,DESIGNATION,DEPARTMENT,";
                query = query + " USER_RIGHTS,SYNC_ID,SYNC_PASSWORD,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,";
                query = query + " LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_USER_MASTER where user_id = '" + userdata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }

        //public ActionResult OnTextChange(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    GPIL_USER_MASTER gPIL_USER_MASTER = db.GPIL_USER_MASTER.Find(id);
        //    if (gPIL_USER_MASTER == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gPIL_USER_MASTER);


        //}

        // GET: GPIL_USER_MASTER
        public ActionResult Index()
        {
            //return View(db.GPIL_USER_MASTER.ToList());

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_USER_MASTER.Where(s => s.STATUS == "Y").ToList();
            return View(res);
        }

        // GET: GPIL_USER_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_USER_MASTER gPIL_USER_MASTER = db.GPIL_USER_MASTER.Find(id);
            if (gPIL_USER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_USER_MASTER);
        }

        // GET: GPIL_USER_MASTER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GPIL_USER_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,USER_ID,USER_NAME,PASSWORD,USER_ERP_NAME,EMP_CODE,DESIGNATION,DEPARTMENT,USER_RIGHTS,SYNC_ID,SYNC_PASSWORD,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_USER_MASTER gPIL_USER_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,USER_ID,USER_NAME,PASSWORD,USER_ERP_NAME,EMP_CODE,DESIGNATION,DEPARTMENT,USER_RIGHTS,SYNC_ID,SYNC_PASSWORD,MOBILE_NO,EMAIL_ID,CREATED_BY,";
                    query = query + " CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_USER_MASTER where USER_ID='" + gPIL_USER_MASTER.USER_ID + "' ";
                    dtclstr = ppdMgt.GetQueryResult(query);
                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_USER_MASTER obj = new GPIL_USER_MASTER();
                        obj.USER_ID = gPIL_USER_MASTER.USER_ID;
                        obj.USER_NAME = gPIL_USER_MASTER.USER_NAME;
                        obj.PASSWORD = gPIL_USER_MASTER.PASSWORD;
                        obj.USER_ERP_NAME = gPIL_USER_MASTER.USER_ERP_NAME;
                        obj.EMP_CODE = gPIL_USER_MASTER.EMP_CODE;
                        obj.DESIGNATION = gPIL_USER_MASTER.DESIGNATION;
                        obj.DEPARTMENT = gPIL_USER_MASTER.DEPARTMENT;
                        obj.USER_RIGHTS = gPIL_USER_MASTER.USER_RIGHTS;
                        obj.MOBILE_NO = gPIL_USER_MASTER.MOBILE_NO;
                        obj.EMAIL_ID = gPIL_USER_MASTER.EMAIL_ID;
                        obj.SYNC_ID = "1";
                        obj.SYNC_PASSWORD = "1";
                        obj.STATUS = gPIL_USER_MASTER.STATUS;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;
                        db.GPIL_USER_MASTER.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["SuccessMessage"] = "Successfully Created!!!";
                    }
                    else
                    {
                        GPIL_USER_MASTER obj = (from C in db.GPIL_USER_MASTER where C.USER_ID == gPIL_USER_MASTER.USER_ID select C).Single();
                        obj.USER_ID = gPIL_USER_MASTER.USER_ID;
                        obj.USER_NAME = gPIL_USER_MASTER.USER_NAME;
                        obj.PASSWORD = gPIL_USER_MASTER.PASSWORD;
                        obj.USER_ERP_NAME = gPIL_USER_MASTER.USER_ERP_NAME;
                        obj.EMP_CODE = gPIL_USER_MASTER.EMP_CODE;
                        obj.DESIGNATION = gPIL_USER_MASTER.DESIGNATION;
                        obj.DEPARTMENT = gPIL_USER_MASTER.DEPARTMENT;
                        obj.USER_RIGHTS = gPIL_USER_MASTER.USER_RIGHTS;
                        obj.MOBILE_NO = gPIL_USER_MASTER.MOBILE_NO;
                        obj.EMAIL_ID = gPIL_USER_MASTER.EMAIL_ID;
                        obj.SYNC_ID = "1";
                        obj.SYNC_PASSWORD = "1";
                        obj.STATUS = gPIL_USER_MASTER.STATUS;
                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
                        //db.Entry(gPIL_CLUSTER_MASTER).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Successfully Updated!!!";

                    }
                    //return RedirectToAction("Index");
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
                return View(gPIL_USER_MASTER);
            }
            //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            return View(gPIL_USER_MASTER);
        }

        // GET: GPIL_USER_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_USER_MASTER gPIL_USER_MASTER = db.GPIL_USER_MASTER.Find(id);
            if (gPIL_USER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_USER_MASTER);
        }

        // POST: GPIL_USER_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,USER_ID,USER_NAME,PASSWORD,USER_ERP_NAME,EMP_CODE,DESIGNATION,DEPARTMENT,USER_RIGHTS,SYNC_ID,SYNC_PASSWORD,MOBILE_NO,EMAIL_ID,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_USER_MASTER gPIL_USER_MASTER)
        {
            if (ModelState.IsValid)
            {
                GPIL_USER_MASTER obj = (from C in db.GPIL_USER_MASTER where C.USER_ID == gPIL_USER_MASTER.USER_ID select C).Single();
                //GPIL_USER_MASTER obj = new GPIL_USER_MASTER();
                obj.USER_ID = gPIL_USER_MASTER.USER_ID;
                obj.USER_NAME = gPIL_USER_MASTER.USER_NAME;
                obj.USER_ERP_NAME = gPIL_USER_MASTER.USER_ERP_NAME;

                obj.PASSWORD = gPIL_USER_MASTER.PASSWORD;
                obj.DESIGNATION = gPIL_USER_MASTER.DESIGNATION;
                obj.USER_RIGHTS = gPIL_USER_MASTER.USER_RIGHTS;

                obj.EMP_CODE = gPIL_USER_MASTER.EMP_CODE;
                obj.EMAIL_ID = gPIL_USER_MASTER.EMAIL_ID;
                obj.MOBILE_NO = gPIL_USER_MASTER.MOBILE_NO;
                obj.DEPARTMENT = gPIL_USER_MASTER.DEPARTMENT;
                obj.STATUS = gPIL_USER_MASTER.STATUS;
                obj.CREATED_BY = gPIL_USER_MASTER.CREATED_BY;
                obj.CREATED_DATE = gPIL_USER_MASTER.CREATED_DATE;
                obj.SYNC_ID = "1";
                obj.SYNC_PASSWORD = "1";
                obj.LAST_UPDATED_BY = Session["userID"].ToString();
                obj.LAST_UPDATED_DATE = DateTime.Now;
                // db.Entry(gPIL_USER_MASTER).State = EntityState.Modified;

                db.SaveChanges();
                TempData["SuccessMessage"] = "Successfully Updated!!!";
                // return RedirectToAction("Index");
                //db.Entry(gPIL_USER_MASTER).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(gPIL_USER_MASTER);
        }

        // GET: GPIL_USER_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_USER_MASTER gPIL_USER_MASTER = db.GPIL_USER_MASTER.Find(id);
            if (gPIL_USER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_USER_MASTER);
        }

        // POST: GPIL_USER_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_USER_MASTER gPIL_USER_MASTER = db.GPIL_USER_MASTER.Find(id);
            GPIL_USER_MASTER obj = (from C in db.GPIL_USER_MASTER where C.USER_ID == gPIL_USER_MASTER.USER_ID select C).Single();
            obj.USER_ID = gPIL_USER_MASTER.USER_ID;
            obj.USER_NAME = gPIL_USER_MASTER.USER_NAME;
            obj.USER_ERP_NAME = gPIL_USER_MASTER.USER_ERP_NAME;

            obj.PASSWORD = gPIL_USER_MASTER.PASSWORD;
            obj.DESIGNATION = gPIL_USER_MASTER.DESIGNATION;
            obj.USER_RIGHTS = gPIL_USER_MASTER.USER_RIGHTS;

            obj.EMP_CODE = gPIL_USER_MASTER.EMP_CODE;
            obj.EMAIL_ID = gPIL_USER_MASTER.EMAIL_ID;
            obj.MOBILE_NO = gPIL_USER_MASTER.MOBILE_NO;
            obj.DEPARTMENT = gPIL_USER_MASTER.DEPARTMENT;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_USER_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_USER_MASTER.CREATED_DATE;
            obj.SYNC_ID = gPIL_USER_MASTER.SYNC_ID;
            obj.SYNC_PASSWORD = gPIL_USER_MASTER.SYNC_PASSWORD;
            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            // db.Entry(gPIL_USER_MASTER).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            TempData["SuccessMessage"] = "Successfully Deleted!!!";
            return View(gPIL_USER_MASTER);
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


        //[HttpPost]
        //public JsonResult UserMasterComplete(ListUserMaster LUM)
        //{
        //    UserMasterdata(LUM);
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
        public JsonResult UserMasterComplete(ListUserMaster LUM)
        {

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();

                DataTable dtGridLst = ToDataTable(LUM.UserMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string USER_ID = dtGridLst.Rows[s]["USER_ID"].ToString();
                    string USER_NAME = dtGridLst.Rows[s]["USER_NAME"].ToString();
                    string USER_ERP_NAME = dtGridLst.Rows[s]["USER_ERP_NAME"].ToString();
                    string PASSWORD = dtGridLst.Rows[s]["PASSWORD"].ToString();
                    string DESIGNATION = dtGridLst.Rows[s]["DESIGNATION"].ToString();
                    string USER_RIGHTS = dtGridLst.Rows[s]["USER_RIGHTS"].ToString();
                    string EMP_CODE = dtGridLst.Rows[s]["EMP_CODE"].ToString();
                    string EMAIL_ID = dtGridLst.Rows[s]["EMAIL_ID"].ToString();
                    string MOBILE_NO = dtGridLst.Rows[s]["MOBILE_NO"].ToString();
                    string DEPARTMENT = dtGridLst.Rows[s]["DEPARTMENT"].ToString();
                    string SYNC_ID;
                    string SYNC_PASSWORD;
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();



                    SYNC_ID = "1";
                    SYNC_PASSWORD = "1";

                    if (USER_ID == "" || USER_ID.Length > 20)
                    {
                        data = "Error: User ID Should Not be Empty and max length 20 for this User Name   " + USER_NAME;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (USER_NAME == "" || USER_NAME.Length > 50)
                    {
                        data = "Error: User Name Should Not be Empty   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (USER_ERP_NAME == "" || USER_ERP_NAME.Length > 50)
                    {
                        data = "Error: User ERP Name Should Not be Empty   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (PASSWORD == "" || PASSWORD.Length > 10)
                    {
                        data = "Error: User Password Should Not be Empty and max length should be 10   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (DESIGNATION == "" || DESIGNATION.Length > 50)
                    {
                        data = "Error: Designation Should Not be Empty and max length should be 50   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (DEPARTMENT == "" || DEPARTMENT.Length > 50)
                    {
                        data = "Error: Department Should Not be Empty and max length should be 50   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (USER_RIGHTS == "" || USER_RIGHTS.Length > 10)
                    {
                        data = "Error: User Rights Should Not be Empty and max length should be 10   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (EMAIL_ID == "" || EMAIL_ID.Length > 50)
                    {
                        data = "Error: Email ID Should Not be Empty and max length should be 50   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (STATUS == "" || STATUS.Length > 5)
                    {
                        data = "Error: Status Should Not be Empty and max length should be 5   " + USER_ID;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }



                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,USER_ID,USER_NAME,PASSWORD,USER_ERP_NAME,EMP_CODE,DESIGNATION,DEPARTMENT,USER_RIGHTS,SYNC_ID,SYNC_PASSWORD,MOBILE_NO,EMAIL_ID,CREATED_BY,";
                    query = query + " CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_USER_MASTER where USER_ID='" + USER_ID + "' ";
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
                        GPIL_USER_MASTER obj = null;
                        obj = new GPIL_USER_MASTER();
                        strQry = "INSERT INTO [dbo].[GPIL_USER_MASTER] ([USER_ID],[USER_NAME],[USER_ERP_NAME],[PASSWORD],[DESIGNATION],[USER_RIGHTS],[EMP_CODE],[EMAIL_ID],[MOBILE_NO],[DEPARTMENT],[CREATED_BY],[CREATED_DATE],[SYNC_ID],[SYNC_PASSWORD],[STATUS]) ";
                        strQry = strQry + "VALUES('" + USER_ID + "','" + USER_NAME + "','" + USER_ERP_NAME + "','" + PASSWORD + "','" + DESIGNATION + "','" + USER_RIGHTS + "','" + EMP_CODE + "','" + EMAIL_ID + "','" + MOBILE_NO + "','" + DEPARTMENT + "','" + Session["userID"].ToString() + "',getdate(),'" + SYNC_ID + "','" + SYNC_PASSWORD + "','" + STATUS + "')";

                        lstQry.Add(strQry);
                    }
                    else
                    {
                        data = "Error: User ID " + USER_ID + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                }
                bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);
                //bool b = GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);
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
                //data = "Succuss";
                //json = JsonConvert.SerializeObject(data);
                //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;


            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }


        }

    }
}
