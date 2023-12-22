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

    public class GPIL_VILLAGE_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        [HttpPost]
        public ActionResult CheckVillageAvailability(string Villagedata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_VILLAGE_MASTER.Where(x => x.VILLAGE_CODE == Villagedata).SingleOrDefault();
            if (usr != null)
            {
                if (Villagedata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,VILLAGE_CODE,VILLAGE_NAME,CLUSTER_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                query = query + "ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_VILLAGE_MASTER where VILLAGE_CODE = '" + Villagedata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }


        // GET: GPIL_VILLAGE_MASTER
        public ActionResult Index()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            //var res = db.GPIL_VILLAGE_MASTER.ToList();

            //return View(res);

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_VILLAGE_MASTER.ToList();

            return View(res);
        }

        // GET: GPIL_VILLAGE_MASTER/Details/5
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

        // GET: GPIL_VILLAGE_MASTER/Create
        public ActionResult Create()
        {


            var ClusterCode = db.GPIL_CLUSTER_MASTER
                 .Select(i => i.CLUSTER_CODE)
                 .Distinct();
            ViewBag.GPIL_CLUSTER_MASTER = new SelectList(ClusterCode);
            return View();
        }

        // POST: GPIL_VILLAGE_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,VILLAGE_CODE,VILLAGE_NAME,CLUSTER_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER)
        {
            var ClusterCode = db.GPIL_CLUSTER_MASTER
                 .Select(i => i.CLUSTER_CODE)
                 .Distinct();
            ViewBag.GPIL_CLUSTER_MASTER = new SelectList(ClusterCode);
            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,VILLAGE_CODE,VILLAGE_NAME,CLUSTER_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + "ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_VILLAGE_MASTER where VILLAGE_CODE='" + gPIL_VILLAGE_MASTER.VILLAGE_CODE + "' ";

                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_VILLAGE_MASTER obj = new GPIL_VILLAGE_MASTER();
                        obj.VILLAGE_CODE = gPIL_VILLAGE_MASTER.VILLAGE_CODE;
                        obj.VILLAGE_NAME = gPIL_VILLAGE_MASTER.VILLAGE_NAME;
                        obj.CLUSTER_CODE = gPIL_VILLAGE_MASTER.CLUSTER_CODE;
                        obj.STATUS = gPIL_VILLAGE_MASTER.STATUS;

                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_VILLAGE_MASTER.Add(obj);
                        db.SaveChanges();

                        TempData["SuccessMessage"] = "Successfully Created!!!";
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_VILLAGE_MASTER obj = (from C in db.GPIL_VILLAGE_MASTER where C.VILLAGE_CODE == gPIL_VILLAGE_MASTER.VILLAGE_CODE select C).Single();

                        obj.VILLAGE_NAME = gPIL_VILLAGE_MASTER.VILLAGE_NAME;
                        obj.CLUSTER_CODE = gPIL_VILLAGE_MASTER.CLUSTER_CODE;

                        obj.STATUS = gPIL_VILLAGE_MASTER.STATUS;

                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;


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
                return View(gPIL_VILLAGE_MASTER);
            }


            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_VILLAGE_MASTER.CREATED_BY);

            return View(gPIL_VILLAGE_MASTER);
        }

        // GET: GPIL_VILLAGE_MASTER/Edit/5
        public ActionResult Edit(string id)
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



            var ClusterCode = db.GPIL_CLUSTER_MASTER
                 .Select(i => i.CLUSTER_CODE)
                 .Distinct();
            ViewBag.GPIL_CLUSTER_MASTER = new SelectList(ClusterCode);
            return View(gPIL_VILLAGE_MASTER);
        }

        // POST: GPIL_VILLAGE_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,VILLAGE_CODE,VILLAGE_NAME,CLUSTER_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER)
        {
            var ClusterCode = db.GPIL_CLUSTER_MASTER
                .Select(i => i.CLUSTER_CODE)
                .Distinct();
            ViewBag.GPIL_CLUSTER_MASTER = new SelectList(ClusterCode);
            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_VILLAGE_MASTER obj = (from C in db.GPIL_VILLAGE_MASTER where C.VILLAGE_CODE == gPIL_VILLAGE_MASTER.VILLAGE_CODE select C).Single();
                    //GPIL_VILLAGE_MASTER obj = new GPIL_VILLAGE_MASTER();
                    //obj.VILLAGE_CODE = gPIL_VILLAGE_MASTER.VILLAGE_CODE;
                    obj.VILLAGE_NAME = gPIL_VILLAGE_MASTER.VILLAGE_NAME;
                    obj.CLUSTER_CODE = gPIL_VILLAGE_MASTER.CLUSTER_CODE;
                    obj.STATUS = gPIL_VILLAGE_MASTER.STATUS;
                    obj.CREATED_BY = gPIL_VILLAGE_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_VILLAGE_MASTER.CREATED_DATE;
                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfully Updated!!!";
                    //return RedirectToAction("Index");

                }
                return View(gPIL_VILLAGE_MASTER);
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

        // GET: GPIL_VILLAGE_MASTER/Delete/5
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

        // POST: GPIL_VILLAGE_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_VILLAGE_MASTER gPIL_VILLAGE_MASTER = db.GPIL_VILLAGE_MASTER.Find(id);
            GPIL_VILLAGE_MASTER obj = (from C in db.GPIL_VILLAGE_MASTER where C.VILLAGE_CODE == gPIL_VILLAGE_MASTER.VILLAGE_CODE select C).Single();
            obj.VILLAGE_CODE = gPIL_VILLAGE_MASTER.VILLAGE_CODE;
            obj.VILLAGE_NAME = gPIL_VILLAGE_MASTER.VILLAGE_NAME;
            obj.CLUSTER_CODE = gPIL_VILLAGE_MASTER.CLUSTER_CODE;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_VILLAGE_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_VILLAGE_MASTER.CREATED_DATE;
            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_VILLAGE_MASTER).State = EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Successfully Deleted!!!";
            return View(gPIL_VILLAGE_MASTER);
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
        //public JsonResult VillageMasterComplete(ListVillageMaster LVLM)
        //{
        //    VillageMasterdata(LVLM);

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
        [HttpPost]
        public JsonResult VillageMasterComplete(ListVillageMaster LVLM)
        {
            MasterManagement MstrMgt = new MasterManagement();

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();

                DataTable dtGridLst = ToDataTable(LVLM.VillageMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string VILLAGE_CODE = dtGridLst.Rows[s]["VILLAGE_CODE"].ToString();
                    string VILLAGE_NAME = dtGridLst.Rows[s]["VILLAGE_NAME"].ToString();
                    string CLUSTER_CODE = dtGridLst.Rows[s]["CLUSTER_CODE"].ToString();
                    //string CREATED_BY = Session["userID"].ToString();
                    //obj.CREATED_DATE = DateTime.Now;
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();

                    DataTable dtclstr = new DataTable();
                    string query = "";

                    if (VILLAGE_CODE == "" || VILLAGE_CODE.Length > 10)
                    {
                        data = "Error: Village code is not Empty and length less than 10";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (VILLAGE_NAME == "" || VILLAGE_NAME.Length > 50)
                    {
                        data = "Error: Village name is not Empty and length less than 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (CLUSTER_CODE == "" || CLUSTER_CODE.Length > 10)
                    {
                        data = "Error: cluster code is not Empty and length less than 10";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (STATUS == "")
                    {
                        data = "Error: Village status is not Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    query = " SELECT VILLAGE_CODE from GPIL_VILLAGE_MASTER where VILLAGE_CODE='" + VILLAGE_CODE + "' ";
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
                        GPIL_VILLAGE_MASTER obj = null;
                        obj = new GPIL_VILLAGE_MASTER();
                        strQry = "INSERT INTO [dbo].[GPIL_VILLAGE_MASTER] ([VILLAGE_CODE],[VILLAGE_NAME],[CLUSTER_CODE],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + VILLAGE_CODE + "','" + VILLAGE_NAME + "','" + CLUSTER_CODE + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";
                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: Village Code " + VILLAGE_CODE + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                }
                bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);// GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);
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
                    return Json("Error: Please check the Excel sheet ", JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }


    }

}