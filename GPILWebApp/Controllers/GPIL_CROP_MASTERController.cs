using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using Newtonsoft.Json;
using GPILWebApp.ViewModel;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel;
using System.Data.Entity.Validation;

namespace GPILWebApp.Controllers
{

    public class GPIL_CROP_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        System.Data.DataTable dt = new System.Data.DataTable();


        [HttpPost]
        public ActionResult CheckCropAvailability(string Cropdata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_CROP_MASTER.Where(x => x.CROP == Cropdata).SingleOrDefault();
            if (usr != null)
            {
                if (Cropdata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,CROP,CROP_YEAR,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,";
                query = query + "STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_CROP_MASTER where CROP = '" + Cropdata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }

        // GET: GPIL_CROP_MASTER
        public ActionResult Index()
        {
            //var gPIL_CROP_MASTER = db.GPIL_CROP_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1);
            //return View(gPIL_CROP_MASTER.ToList());


            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_CROP_MASTER.Where(s => s.STATUS == "Y").ToList();

            ViewBag.menu = new MenuModel();

            return View(res);
        }

        // GET: GPIL_CROP_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_CROP_MASTER gPIL_CROP_MASTER = db.GPIL_CROP_MASTER.Find(id);
            if (gPIL_CROP_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_CROP_MASTER);
        }

        // GET: GPIL_CROP_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");

            return View();
        }

        // POST: GPIL_CROP_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,CROP,CROP_YEAR,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_CROP_MASTER gPIL_CROP_MASTER)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,CROP,CROP_YEAR,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,";
                    query = query + "STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_CROP_MASTER where CROP='" + gPIL_CROP_MASTER.CROP + "' ";

                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_CROP_MASTER obj = new GPIL_CROP_MASTER();
                        obj.CROP = gPIL_CROP_MASTER.CROP;
                        obj.CROP_YEAR = gPIL_CROP_MASTER.CROP_YEAR;
                        obj.STATUS = gPIL_CROP_MASTER.STATUS;

                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_CROP_MASTER.Add(obj);
                        db.SaveChanges();

                        TempData["SuccessMessage"] = "Successfully Created!!!";
                        //ModelState.Clear();
                    }
                    else
                    {
                        GPIL_CROP_MASTER obj = (from C in db.GPIL_CROP_MASTER where C.CROP == gPIL_CROP_MASTER.CROP select C).Single();

                        obj.CROP_YEAR = gPIL_CROP_MASTER.CROP_YEAR;
                        obj.STATUS = gPIL_CROP_MASTER.STATUS;

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
                return View(gPIL_CROP_MASTER);
            }


            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CROP_MASTER.CREATED_BY);
            return View(gPIL_CROP_MASTER);
        }

        // GET: GPIL_CROP_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_CROP_MASTER gPIL_CROP_MASTER = db.GPIL_CROP_MASTER.Find(id);
            if (gPIL_CROP_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CROP_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CROP_MASTER.CREATED_BY);
            return View(gPIL_CROP_MASTER);
        }

        // POST: GPIL_CROP_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,CROP,CROP_YEAR,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_CROP_MASTER gPIL_CROP_MASTER)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_CROP_MASTER obj = (from C in db.GPIL_CROP_MASTER where C.CROP == gPIL_CROP_MASTER.CROP select C).Single();
                    obj.CROP = gPIL_CROP_MASTER.CROP;
                    obj.CROP_YEAR = gPIL_CROP_MASTER.CROP_YEAR;
                    obj.STATUS = gPIL_CROP_MASTER.STATUS;
                    obj.CREATED_BY = gPIL_CROP_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_CROP_MASTER.CREATED_DATE;

                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfuly Updated!!!";

                    // return RedirectToAction("Index");
                }
                //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CROP_MASTER.CREATED_BY);
                //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CROP_MASTER.CREATED_BY);
                return View(gPIL_CROP_MASTER);
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

        // GET: GPIL_CROP_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_CROP_MASTER gPIL_CROP_MASTER = db.GPIL_CROP_MASTER.Find(id);
            if (gPIL_CROP_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_CROP_MASTER);
        }

        // POST: GPIL_CROP_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {


            GPIL_CROP_MASTER gPIL_CROP_MASTER = db.GPIL_CROP_MASTER.Find(id);

            GPIL_CROP_MASTER obj = (from C in db.GPIL_CROP_MASTER where C.CROP == gPIL_CROP_MASTER.CROP select C).Single();

            obj.CROP = gPIL_CROP_MASTER.CROP;
            obj.CROP_YEAR = gPIL_CROP_MASTER.CROP_YEAR;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_CROP_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_CROP_MASTER.CREATED_DATE;

            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_CROP_MASTER).State = EntityState.Modified;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Successfully Deleted!!!";

            return View(gPIL_CROP_MASTER);
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
        MasterManagement MstrMgt = new MasterManagement();
        [HttpPost]
        public JsonResult CropMasterComplete(ListCropMaster LCRM)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;



            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LCRM.CropMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string CROP = dtGridLst.Rows[s]["CROP"].ToString();
                    string CROP_YEAR = dtGridLst.Rows[s]["CROP_YEAR"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();


                    DataTable dtclstr = new DataTable();
                    string query = "";

                    if (CROP.Trim() == string.Empty || CROP.Length > 2)
                    {
                        data = "Error: Crop Must Not Be Empty and Length should be 2" + CROP;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (CROP_YEAR.Trim() == string.Empty || CROP_YEAR.Length > 4)
                    {
                        data = "Error: Crop Year Must Not Be Empty and Length should be 4       " + CROP;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    //else if (STATUS.Trim() == string.Empty || STATUS.Length > 1)
                    //{
                    //    data = "Error: Status Must Not Be Empty and Length should be 1" + CROP;
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;
                    //}


                    query = " SELECT SNO,CROP,CROP_YEAR,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                    query = query + " FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_CROP_MASTER where CROP='" + CROP + "' ";


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
                        GPIL_CROP_MASTER obj = null;


                        obj = new GPIL_CROP_MASTER();
                        //obj.CROP = dtGridLst.Rows[s]["CROP"].ToString();
                        //obj.CROP_YEAR = dtGridLst.Rows[s]["CROP_YEAR"].ToString();
                        //obj.CREATED_BY = Session["userID"].ToString();
                        //obj.CREATED_DATE = DateTime.Now;
                        //obj.STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                        //db.GPIL_CROP_MASTER.Add(obj);
                        //db.SaveChanges();
                        strQry = "INSERT INTO [dbo].[GPIL_CROP_MASTER] ([CROP],[CROP_YEAR],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + CROP + "','" + CROP_YEAR + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";

                        lstQry.Add(strQry);


                    }

                    else
                    {

                        data = "Error: Crop Code " + CROP + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
                //bool b = false;

                bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);
                //GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);

                if (b)
                {
                    data = "Succuss: Crop Code is Inserted SucessFully";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Error: PLEASE CHECK EXCEL SHEET";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;// ("Error: PLEASE CHECK EXCEL SHEET" + JsonRequestBehavior.AllowGet);
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

            //}

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
        public void CropMasterdata(ListCropMaster LCRM)
        {

            //return RedirectToAction("Index");

        }



    }
}
