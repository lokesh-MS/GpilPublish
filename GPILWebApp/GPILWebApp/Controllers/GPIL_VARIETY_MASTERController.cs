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
    public class GPIL_VARIETY_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        List<GPIL_VARIETY_MASTER> lVariety = new List<GPIL_VARIETY_MASTER>();

        [HttpPost]
        public ActionResult CheckVarietyAvailability(string Varietydata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_VARIETY_MASTER.Where(x => x.VARIETY == Varietydata).SingleOrDefault();
            if (usr != null)
            {
                if (Varietydata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,VARIETY,VARIETY_TYPE,VARIETY_NAME,VARIETY_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,";
                query = query + "STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_VARIETY_MASTER where Variety = '" + Varietydata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }


        // GET: GPIL_VARIETY_MASTER
        public ActionResult Index()
        {

            //var gPIL_VARIETY_MASTER = db.GPIL_VARIETY_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1).Include(g => g.GPIL_VARIETY_SEASON_MASTER);
            //return View(gPIL_VARIETY_MASTER.ToList());
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_VARIETY_MASTER.Where(s => s.STATUS == "Y").ToList();

            return View(res);
        }

        // GET: GPIL_VARIETY_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_VARIETY_MASTER gPIL_VARIETY_MASTER = db.GPIL_VARIETY_MASTER.Find(id);
            if (gPIL_VARIETY_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_VARIETY_MASTER);
        }

        // GET: GPIL_VARIETY_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.VARIETY = new SelectList(db.GPIL_VARIETY_SEASON_MASTER, "VARIETY", "CROP");
            return View();
        }

        // POST: GPIL_VARIETY_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,VARIETY,VARIETY_TYPE,VARIETY_NAME,VARIETY_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_VARIETY_MASTER gPIL_VARIETY_MASTER)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,VARIETY,VARIETY_TYPE,VARIETY_NAME,VARIETY_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + " ATTRIBUTE3,ATTRIBUTE4 from GPIL_VARIETY_MASTER where VARIETY='" + gPIL_VARIETY_MASTER.VARIETY + "' ";

                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_VARIETY_MASTER obj = new GPIL_VARIETY_MASTER();
                        obj.VARIETY = gPIL_VARIETY_MASTER.VARIETY;
                        obj.VARIETY_TYPE = gPIL_VARIETY_MASTER.VARIETY_TYPE;
                        obj.VARIETY_NAME = gPIL_VARIETY_MASTER.VARIETY_NAME;
                        obj.VARIETY_DESC = gPIL_VARIETY_MASTER.VARIETY_DESC;
                        obj.STATUS = gPIL_VARIETY_MASTER.STATUS;

                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_VARIETY_MASTER.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_VARIETY_MASTER obj = (from C in db.GPIL_VARIETY_MASTER where C.VARIETY == gPIL_VARIETY_MASTER.VARIETY select C).Single();
                        obj.VARIETY_TYPE = gPIL_VARIETY_MASTER.VARIETY_TYPE;
                        obj.VARIETY_NAME = gPIL_VARIETY_MASTER.VARIETY_NAME;
                        obj.VARIETY_DESC = gPIL_VARIETY_MASTER.VARIETY_DESC;
                        obj.STATUS = gPIL_VARIETY_MASTER.STATUS;

                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
                        //db.Entry(gPIL_CLUSTER_MASTER).State = EntityState.Modified;
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
                return View(gPIL_VARIETY_MASTER);
            }


            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_VARIETY_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_VARIETY_MASTER.CREATED_BY);
            ViewBag.VARIETY = new SelectList(db.GPIL_VARIETY_SEASON_MASTER, "VARIETY", "CROP", gPIL_VARIETY_MASTER.VARIETY);

            return View(gPIL_VARIETY_MASTER);
        }

        // GET: GPIL_VARIETY_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_VARIETY_MASTER gPIL_VARIETY_MASTER = db.GPIL_VARIETY_MASTER.Find(id);
            if (gPIL_VARIETY_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_VARIETY_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_VARIETY_MASTER.CREATED_BY);
            ViewBag.VARIETY = new SelectList(db.GPIL_VARIETY_SEASON_MASTER, "VARIETY", "CROP", gPIL_VARIETY_MASTER.VARIETY);
            return View(gPIL_VARIETY_MASTER);
        }

        // POST: GPIL_VARIETY_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Edit([Bind(Include = "SNO,VARIETY,VARIETY_TYPE,VARIETY_NAME,VARIETY_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_VARIETY_MASTER gPIL_VARIETY_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_VARIETY_MASTER obj = (from C in db.GPIL_VARIETY_MASTER where C.VARIETY == gPIL_VARIETY_MASTER.VARIETY select C).Single();

                    obj.VARIETY_NAME = gPIL_VARIETY_MASTER.VARIETY_NAME;
                    obj.VARIETY_TYPE = gPIL_VARIETY_MASTER.VARIETY_TYPE;
                    obj.VARIETY_DESC = gPIL_VARIETY_MASTER.VARIETY_DESC;

                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    obj.STATUS = gPIL_VARIETY_MASTER.STATUS;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(gPIL_VARIETY_MASTER);
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

        // GET: GPIL_VARIETY_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_VARIETY_MASTER gPIL_VARIETY_MASTER = db.GPIL_VARIETY_MASTER.Find(id);
            if (gPIL_VARIETY_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_VARIETY_MASTER);
        }

        // POST: GPIL_VARIETY_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_VARIETY_MASTER gPIL_VARIETY_MASTER = db.GPIL_VARIETY_MASTER.Find(id);
           

            GPIL_VARIETY_MASTER obj = (from C in db.GPIL_VARIETY_MASTER where C.VARIETY == gPIL_VARIETY_MASTER.VARIETY select C).Single();

            obj.VARIETY = gPIL_VARIETY_MASTER.VARIETY;            
            obj.VARIETY_NAME = gPIL_VARIETY_MASTER.VARIETY_NAME;
            obj.VARIETY_TYPE = gPIL_VARIETY_MASTER.VARIETY_TYPE;
            obj.VARIETY_DESC = gPIL_VARIETY_MASTER.VARIETY_DESC;

            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            obj.STATUS = "N";
            db.Entry(gPIL_VARIETY_MASTER).State = EntityState.Modified;
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
        //// JsonResult
        //public JsonResult VarietyMasterComplete(ListVarietyMaster LVM)
        //{
        //    VarietyMasterdata(LVM);
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
        // JsonResult
        public JsonResult VarietyMasterComplete(ListVarietyMaster LVM)
        {

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {

                List<string> lstQry = new List<string>();
                

                DataTable dtGridLst = ToDataTable(LVM.VarietyMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string VARIETY = dtGridLst.Rows[s]["VARIETY"].ToString();
                    string VARIETY_TYPE = dtGridLst.Rows[s]["VARIETY_TYPE"].ToString();
                    string VARIETY_NAME = dtGridLst.Rows[s]["VARIETY_NAME"].ToString();
                    string VARIETY_DESC = dtGridLst.Rows[s]["VARIETY_DESC"].ToString();
                    //string CREATED_BY = Session["userID"].ToString();
                    //string CREATED_DATE = DateTime.Now;
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();

                    if (VARIETY == "")
                    {
                        data = "Error: Variety should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (VARIETY_TYPE == "")
                    {
                        data = "Error: Variety should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    if (VARIETY_NAME == "")
                    {
                        data = "Error: Variety Name should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (VARIETY_DESC == "")
                    {
                        data = "Error: Variety Desc should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
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
                    query = " SELECT SNO,VARIETY,VARIETY_TYPE,VARIETY_NAME,VARIETY_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + " ATTRIBUTE3,ATTRIBUTE4 from GPIL_VARIETY_MASTER where VARIETY='" + VARIETY + "' ";
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
                        GPIL_VARIETY_MASTER obj =null;
                        obj = new GPIL_VARIETY_MASTER();
                        //obj.VARIETY = dtGridLst.Rows[s]["VARIETY"].ToString();
                        //obj.VARIETY_TYPE = dtGridLst.Rows[s]["VARIETY_TYPE"].ToString();
                        //obj.VARIETY_NAME = dtGridLst.Rows[s]["VARIETY_NAME"].ToString();
                        //obj.VARIETY_DESC = dtGridLst.Rows[s]["VARIETY_DESC"].ToString();
                        //obj.CREATED_BY = Session["userID"].ToString();
                        //obj.CREATED_DATE = DateTime.Now;                        
                        //obj.STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                        strQry = "INSERT INTO [dbo].[GPIL_VARIETY_MASTER] ([VARIETY],[VARIETY_TYPE],[VARIETY_NAME],[VARIETY_DESC],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + VARIETY + "','" + VARIETY_TYPE + "','" + VARIETY_NAME + "','" + VARIETY_DESC + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";

                        lstQry.Add(strQry);
                    }
                    else
                    {
                        data = "Error: Variety " + VARIETY + " is already exist So please check and import";
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
