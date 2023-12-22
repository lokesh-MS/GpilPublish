using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using System.Data.Entity.Validation;
using GPILWebApp.ViewModel;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Text;
using CrystalDecisions.Web.Services;
//using GPILWebApp.ViewModel.Masters;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{

    public class GPIL_CLUSTER_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        List<GPIL_CLUSTER_MASTER> lCLuster = new List<GPIL_CLUSTER_MASTER>();



        [HttpPost]
        public ActionResult CheckClusterAvailability(string Clusdata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_CLUSTER_MASTER.Where(x => x.CLUSTER_CODE == Clusdata).SingleOrDefault();
            if (usr != null)
            {
                if (Clusdata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2,";
                query = query + " ATTRIBUTE3,ATTRIBUTE4 from GPIL_CLUSTER_MASTER where CLUSTER_CODE='" + Clusdata + "' ";

                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }

        // GET: GPIL_CLUSTER_MASTER
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_CLUSTER_MASTER.Where(s => s.STATUS == "Y").ToList();

            return View(res);
            //var gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER;/*.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1).Include(g => g.GPIL_USER_MASTER2).Include(g => g.GPIL_USER_MASTER3);*/
            //return View(gPIL_CLUSTER_MASTER.ToList());
        }

        // GET: GPIL_CLUSTER_MASTER/Details/5
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

        // GET: GPIL_CLUSTER_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }




        // POST: GPIL_CLUSTER_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2,";
                    query = query + " ATTRIBUTE3,ATTRIBUTE4 from GPIL_CLUSTER_MASTER where CLUSTER_CODE='" + gPIL_CLUSTER_MASTER.CLUSTER_CODE + "' ";

                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_CLUSTER_MASTER obj = new GPIL_CLUSTER_MASTER();
                        obj.CLUSTER_CODE = gPIL_CLUSTER_MASTER.CLUSTER_CODE;
                        obj.CLUSTER_NAME = gPIL_CLUSTER_MASTER.CLUSTER_NAME;
                        obj.STATUS = gPIL_CLUSTER_MASTER.STATUS;

                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_CLUSTER_MASTER.Add(obj);
                        db.SaveChanges();

                        TempData["SuccessMessage"] = "Successfuly Created!!!";
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_CLUSTER_MASTER obj = (from C in db.GPIL_CLUSTER_MASTER where C.CLUSTER_CODE == gPIL_CLUSTER_MASTER.CLUSTER_CODE select C).Single();
                        obj.CLUSTER_NAME = gPIL_CLUSTER_MASTER.CLUSTER_NAME;
                        obj.STATUS = gPIL_CLUSTER_MASTER.STATUS;
                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
                        //obj.CREATED_BY = Session["userID"].ToString();
                        //obj.CREATED_DATE = DateTime.Now;
                        //db.Entry(gPIL_CLUSTER_MASTER).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Successfuly Updated!!!";

                    }
                    return View(gPIL_CLUSTER_MASTER);
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
                return View(gPIL_CLUSTER_MASTER);
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            return View(gPIL_CLUSTER_MASTER);
        }

        // GET: GPIL_CLUSTER_MASTER/Edit/5
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
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_CLUSTER_MASTER.CREATED_BY);
            return View(gPIL_CLUSTER_MASTER);
        }

        // POST: GPIL_CLUSTER_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    GPIL_CLUSTER_MASTER obj = (from C in db.GPIL_CLUSTER_MASTER where C.CLUSTER_CODE == gPIL_CLUSTER_MASTER.CLUSTER_CODE select C).Single();
                    obj.CLUSTER_CODE = gPIL_CLUSTER_MASTER.CLUSTER_CODE;
                    obj.CLUSTER_NAME = gPIL_CLUSTER_MASTER.CLUSTER_NAME;
                    obj.STATUS = gPIL_CLUSTER_MASTER.STATUS;
                    obj.CREATED_BY = gPIL_CLUSTER_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_CLUSTER_MASTER.CREATED_DATE;

                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfully Updated!!!";
                    // return RedirectToAction("Index");


                }

                return View(gPIL_CLUSTER_MASTER);
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

        // GET: GPIL_CLUSTER_MASTER/Delete/5
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

        // POST: GPIL_CLUSTER_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_CLUSTER_MASTER gPIL_CLUSTER_MASTER = db.GPIL_CLUSTER_MASTER.Find(id);

            GPIL_CLUSTER_MASTER obj = (from C in db.GPIL_CLUSTER_MASTER where C.CLUSTER_CODE == gPIL_CLUSTER_MASTER.CLUSTER_CODE select C).Single();
            obj.CLUSTER_CODE = gPIL_CLUSTER_MASTER.CLUSTER_CODE;
            obj.CLUSTER_NAME = gPIL_CLUSTER_MASTER.CLUSTER_NAME;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_CLUSTER_MASTER.CREATED_BY;
            obj.CREATED_DATE = gPIL_CLUSTER_MASTER.CREATED_DATE;

            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_CLUSTER_MASTER).State = EntityState.Modified;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Successfully Deleted!!!";
            //return RedirectToAction("Index");
            return View(gPIL_CLUSTER_MASTER);
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

            //var Cluster = db.GPIL_CLUSTER_MASTER.ToList();
            //return View(Cluster.ToList());
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


        public void ClusterMasterdata(Models.ListClusterMaster LCM)
        {

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
        MasterManagement MstrMgt = new MasterManagement();
        [HttpPost]
        public JsonResult ClusterMasterComplete(Models.ListClusterMaster LCM)
        {

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LCM.ClusterMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string CLUSTER_CODE = dtGridLst.Rows[s]["CLUSTER_CODE"].ToString();
                    string CLUSTER_NAME = dtGridLst.Rows[s]["CLUSTER_NAME"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();


                    if (CLUSTER_CODE == "")
                    {
                        data = "Error: Cluster code should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (CLUSTER_NAME == "")
                    {
                        data = "Error: Cluster name should not be Empty";
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
                    query = " SELECT SNO,CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2,";
                    query = query + " ATTRIBUTE3,ATTRIBUTE4 from GPIL_CLUSTER_MASTER where CLUSTER_CODE='" + CLUSTER_CODE + "' ";
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
                        GPIL_CLUSTER_MASTER obj = null;
                        obj = new GPIL_CLUSTER_MASTER();

                        strQry = "INSERT INTO [dbo].[GPIL_CLUSTER_MASTER] ([CLUSTER_CODE],[CLUSTER_NAME],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + CLUSTER_CODE + "','" + CLUSTER_NAME + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";
                        lstQry.Add(strQry);


                    }
                    else
                    {

                        data = "Error: Cluster Code " + CLUSTER_CODE + " is already exist So please check and import";
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
