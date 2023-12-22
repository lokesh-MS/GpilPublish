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
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System.Text;
using System.Data.Entity.Validation;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class GPIL_ITEM_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();



        [HttpPost]
        public ActionResult CheckItemAvailability(string Itemdata)
        {
            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_ITEM_MASTER.Where(x => x.ITEM_CODE == Itemdata).SingleOrDefault();
            if (usr == null)
            {
                var Variety = Itemdata.Substring(0, 2);
                var Crop = Itemdata.Substring(2, 2);
                var response = new
                {
                    VarietyCode = Variety,
                    CropCode = Crop
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else if (usr != null)
            {
                if (Itemdata == null)
                {
                    return Json(0);
                }
                MasterManagement MstrMgt = new MasterManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP,VARIETY,COST_CATEGORY,ORGN_TYPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                query = query + " ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_ITEM_MASTER where ITEM_CODE='" + Itemdata + "' ";
                dtclstr = MstrMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }



        // GET: GPIL_ITEM_MASTER
        //public ActionResult Index()
        //{

        //    PPDManagement ppdMgt = new PPDManagement();
        //    DataTable dtclstr = new DataTable();
        //    string query = "";
        //    query = "  SELECT SUBSTRING(FINANCIAL_YEAR,3,2) AS YEAR  FROM [dbo].[GPIL_FINANCIAL_YEAR] ";
        //    dtclstr = ppdMgt.GetQueryResult(query);
        //    string strYear = "";

        //    if (dtclstr.Rows.Count > 0)
        //    {
        //        strYear = dtclstr.Rows[0]["YEAR"].ToString();
        //    }

        //    db.Configuration.ProxyCreationEnabled = false;
        //    var res = db.GPIL_ITEM_MASTER.Where(s => s.CROP == strYear && s.STATUS == "Y").ToList();

        //    return View(res);
        //}

        // GET: GPIL_ITEM_MASTER/Details/5
        public ActionResult Index()
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "SELECT SUBSTRING(FINANCIAL_YEAR, 3, 2) AS YEAR FROM [dbo].[GPIL_FINANCIAL_YEAR]";
            dtclstr = ppdMgt.GetQueryResult(query);
            List<string> cropYears = new List<string>();
            if (dtclstr.Rows.Count > 0)
            {
                foreach (DataRow row in dtclstr.Rows)
                {
                    string year = row["YEAR"].ToString();
                    cropYears.Add(year);
                }
            }
            db.Configuration.ProxyCreationEnabled = false;
            // Use the Any() method to check if there are any crop years
            var res = cropYears.Any()
                ? db.GPIL_ITEM_MASTER.Where(s => cropYears.Contains(s.CROP) && s.STATUS == "Y").ToList()
                : new List<GPIL_ITEM_MASTER>(); // Handle the case where there are no crop years
            return View(res);
        }








        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_ITEM_MASTER gPIL_ITEM_MASTER = db.GPIL_ITEM_MASTER.Find(id);
            if (gPIL_ITEM_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_ITEM_MASTER);
        }

        // GET: GPIL_ITEM_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_ITEM_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP,VARIETY,COST_CATEGORY,ORGN_TYPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_ITEM_MASTER gPIL_ITEM_MASTER)
        {

            try
            {
                ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
                ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
                if (ModelState.IsValid)
                {
                    MasterManagement MstrMgt = new MasterManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP,VARIETY,COST_CATEGORY,ORGN_TYPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                    query = query + " FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_ITEM_MASTER where ITEM_CODE='" + gPIL_ITEM_MASTER.ITEM_CODE + "' ";
                    dtclstr = MstrMgt.GetQueryResult(query);
                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_ITEM_MASTER obj = new GPIL_ITEM_MASTER();
                        obj.ITEM_CODE = gPIL_ITEM_MASTER.ITEM_CODE;
                        obj.ITEM_CODE_GROUP = gPIL_ITEM_MASTER.ITEM_CODE_GROUP;
                        obj.ITEM_GROUP = gPIL_ITEM_MASTER.ITEM_GROUP;
                        obj.ITEM_TYPE = gPIL_ITEM_MASTER.ITEM_TYPE;
                        obj.ITEM_DESC = gPIL_ITEM_MASTER.ITEM_DESC;
                        obj.CROP = gPIL_ITEM_MASTER.CROP;
                        obj.VARIETY = gPIL_ITEM_MASTER.VARIETY;
                        obj.COST_CATEGORY = gPIL_ITEM_MASTER.COST_CATEGORY;
                        obj.ORGN_TYPE = gPIL_ITEM_MASTER.ORGN_TYPE;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;
                        obj.STATUS = gPIL_ITEM_MASTER.STATUS;
                        obj.ATTRIBUTE2 = gPIL_ITEM_MASTER.ATTRIBUTE2;
                        obj.ATTRIBUTE3 = gPIL_ITEM_MASTER.ATTRIBUTE3.Substring(0, 1);
                        obj.ATTRIBUTE4 = gPIL_ITEM_MASTER.ATTRIBUTE4;
                        db.GPIL_ITEM_MASTER.Add(obj);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Successfully Created!!!";
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_ITEM_MASTER obj = (from C in db.GPIL_ITEM_MASTER where C.ITEM_CODE == gPIL_ITEM_MASTER.ITEM_CODE select C).Single();
                        obj.ITEM_CODE = gPIL_ITEM_MASTER.ITEM_CODE;
                        obj.ITEM_CODE_GROUP = gPIL_ITEM_MASTER.ITEM_CODE_GROUP;
                        obj.ITEM_GROUP = gPIL_ITEM_MASTER.ITEM_GROUP;
                        obj.ITEM_TYPE = gPIL_ITEM_MASTER.ITEM_TYPE;
                        obj.ITEM_DESC = gPIL_ITEM_MASTER.ITEM_DESC;
                        obj.CROP = gPIL_ITEM_MASTER.CROP;
                        obj.VARIETY = gPIL_ITEM_MASTER.VARIETY;
                        obj.COST_CATEGORY = gPIL_ITEM_MASTER.COST_CATEGORY;
                        obj.ORGN_TYPE = gPIL_ITEM_MASTER.ORGN_TYPE;
                        obj.LAST_UPDATED_BY = Session["userID"].ToString();
                        obj.LAST_UPDATED_DATE = DateTime.Now;
                        obj.STATUS = gPIL_ITEM_MASTER.STATUS;
                        obj.ATTRIBUTE2 = gPIL_ITEM_MASTER.ATTRIBUTE2;
                        obj.ATTRIBUTE3 = gPIL_ITEM_MASTER.ATTRIBUTE3.Substring(0, 1);
                        obj.ATTRIBUTE4 = gPIL_ITEM_MASTER.ATTRIBUTE4;
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
                return View(gPIL_ITEM_MASTER);
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ITEM_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ITEM_MASTER.CREATED_BY);
            return View(gPIL_ITEM_MASTER);
        }

        // GET: GPIL_ITEM_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_ITEM_MASTER gPIL_ITEM_MASTER = db.GPIL_ITEM_MASTER.Find(id);
            if (gPIL_ITEM_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ITEM_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_ITEM_MASTER.CREATED_BY);
            return View(gPIL_ITEM_MASTER);
        }

        // POST: GPIL_ITEM_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP,VARIETY,COST_CATEGORY,ORGN_TYPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_ITEM_MASTER gPIL_ITEM_MASTER)
        {
            //obj.ATTRIBUTE3 = gPIL_ITEM_MASTER.ATTRIBUTE3.Substring(0, 1);
            ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            try
            {
                if (ModelState.IsValid)
                {


                    GPIL_ITEM_MASTER obj = (from C in db.GPIL_ITEM_MASTER where C.ITEM_CODE == gPIL_ITEM_MASTER.ITEM_CODE select C).Single();
                    obj.ITEM_CODE = gPIL_ITEM_MASTER.ITEM_CODE;
                    obj.ITEM_CODE_GROUP = gPIL_ITEM_MASTER.ITEM_CODE_GROUP;
                    obj.ITEM_GROUP = gPIL_ITEM_MASTER.ITEM_GROUP;
                    obj.ITEM_TYPE = gPIL_ITEM_MASTER.ITEM_TYPE;
                    obj.ITEM_DESC = gPIL_ITEM_MASTER.ITEM_DESC;
                    obj.CROP = gPIL_ITEM_MASTER.CROP;
                    obj.VARIETY = gPIL_ITEM_MASTER.VARIETY;
                    obj.COST_CATEGORY = gPIL_ITEM_MASTER.COST_CATEGORY;
                    obj.ORGN_TYPE = gPIL_ITEM_MASTER.ORGN_TYPE;
                    obj.CREATED_BY = gPIL_ITEM_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_ITEM_MASTER.CREATED_DATE;
                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;
                    obj.STATUS = gPIL_ITEM_MASTER.STATUS;
                    obj.ATTRIBUTE2 = gPIL_ITEM_MASTER.ATTRIBUTE2;
                    obj.ATTRIBUTE3 = gPIL_ITEM_MASTER.ATTRIBUTE3.Substring(0, 1);
                    obj.ATTRIBUTE4 = gPIL_ITEM_MASTER.ATTRIBUTE4;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfuly Updated!!!";
                    ModelState.Clear();
                    //return RedirectToAction("Index");

                }

                return View(gPIL_ITEM_MASTER);
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

        // GET: GPIL_ITEM_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_ITEM_MASTER gPIL_ITEM_MASTER = db.GPIL_ITEM_MASTER.Find(id);
            if (gPIL_ITEM_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_ITEM_MASTER);
        }

        // POST: GPIL_ITEM_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {



            try
            {
                GPIL_ITEM_MASTER gPIL_ITEM_MASTER = db.GPIL_ITEM_MASTER.Find(id);

                GPIL_ITEM_MASTER obj = (from C in db.GPIL_ITEM_MASTER where C.ITEM_CODE == gPIL_ITEM_MASTER.ITEM_CODE select C).Single();
                obj.ITEM_CODE = gPIL_ITEM_MASTER.ITEM_CODE;
                obj.ITEM_CODE_GROUP = gPIL_ITEM_MASTER.ITEM_CODE_GROUP;
                obj.ITEM_GROUP = gPIL_ITEM_MASTER.ITEM_GROUP;
                obj.ITEM_TYPE = gPIL_ITEM_MASTER.ITEM_TYPE;
                obj.ITEM_DESC = gPIL_ITEM_MASTER.ITEM_DESC;
                obj.CROP = gPIL_ITEM_MASTER.CROP;
                obj.VARIETY = gPIL_ITEM_MASTER.VARIETY;
                obj.COST_CATEGORY = gPIL_ITEM_MASTER.COST_CATEGORY;
                obj.ORGN_TYPE = gPIL_ITEM_MASTER.ORGN_TYPE;
                obj.CREATED_BY = gPIL_ITEM_MASTER.CREATED_BY;
                obj.CREATED_DATE = gPIL_ITEM_MASTER.CREATED_DATE;
                obj.LAST_UPDATED_BY = Session["userID"].ToString();
                obj.LAST_UPDATED_DATE = DateTime.Now;
                obj.STATUS = "N";
                obj.ATTRIBUTE2 = gPIL_ITEM_MASTER.ATTRIBUTE2;
                obj.ATTRIBUTE3 = gPIL_ITEM_MASTER.ATTRIBUTE3.Substring(0, 1);
                obj.ATTRIBUTE4 = gPIL_ITEM_MASTER.ATTRIBUTE4;
                //db.Entry(gPIL_ITEM_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Successfully Deleted!!!";
                //return RedirectToAction("Index");

                // Set delay in milliseconds (e.g., 3000 for 3 seconds)
                //int delay = 100;

                //Timer timer = null;
                //timer = new Timer(_ =>
                //{
                //    // Your server-side code here
                //    Console.WriteLine("Delayed task executed!");

                //    // Dispose the timer after it's done
                //    timer.Dispose();
                //   RedirectToAction("Index");
                //}, null, delay, Timeout.Infinite);
                return View(gPIL_ITEM_MASTER);
                //return RedirectToAction("Index");
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
            //var Item = db.GPIL_ITEM_MASTER.ToList();
            //return View(Item.ToList());
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
        //public JsonResult ItemMasterComplete(ListItemMaster LIM)
        //{
        //    ItemMasterdata(LIM);
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
        public JsonResult ItemMasterComplete(ListItemMaster LIM)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LIM.ItemMasters);
                string strQry = "";
                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string ITEM_CODE = dtGridLst.Rows[s]["ITEM_CODE"].ToString();
                    string ITEM_CODE_GROUP = dtGridLst.Rows[s]["ITEM_CODE_GROUP"].ToString();
                    string ITEM_GROUP = dtGridLst.Rows[s]["ITEM_GROUP"].ToString();
                    string ITEM_TYPE = dtGridLst.Rows[s]["ITEM_TYPE"].ToString();
                    string ITEM_DESC = dtGridLst.Rows[s]["ITEM_DESC"].ToString();
                    string CROP = dtGridLst.Rows[s]["CROP"].ToString();
                    string VARIETY = dtGridLst.Rows[s]["VARIETY"].ToString();
                    string COST_CATEGORY = dtGridLst.Rows[s]["COST_CATEGORY"].ToString();
                    string ORGN_TYPE = dtGridLst.Rows[s]["ORGN_TYPE"].ToString();
                    string ATTRIBUTE2 = dtGridLst.Rows[s]["ATTRIBUTE2"].ToString();
                    string ATTRIBUTE3 = dtGridLst.Rows[s]["ATTRIBUTE3"].ToString();
                    string ATTRIBUTE4 = dtGridLst.Rows[s]["ATTRIBUTE4"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();


                    // if(ITEM_CODE.Substring )





                    if (ITEM_CODE.Substring(0, 2) != CROP || ITEM_CODE.Length > 20)
                    {
                        data = "Error: Item Code Not match with Crop" + ITEM_CODE + " and length must be less or equal to 20";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ITEM_CODE.Substring(2, 2) != VARIETY)
                    {
                        data = "Error: Item Code Not match with Variety" + ITEM_CODE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ITEM_CODE_GROUP.Trim() == string.Empty || ITEM_CODE_GROUP.Length > 20)
                    {
                        data = "Error: Item Code Group should not be empty for" + ITEM_CODE + "and Length must be 20 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (CROP.Trim() == string.Empty || CROP.Length > 2)
                    {
                        data = "Error: Crop should not be empty for" + ITEM_CODE + "and Length must be 2 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (VARIETY.Trim() == string.Empty || VARIETY.Length > 2)
                    {
                        data = "Error: Variety should not be empty for" + ITEM_CODE + "and Length must be 2 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ITEM_TYPE.Trim() == string.Empty || ITEM_TYPE.Length > 50)
                    {
                        data = "Error: Item Type should not be empty for" + ITEM_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (ITEM_DESC.Trim() == string.Empty || ITEM_DESC.Length > 50)
                    {
                        data = "Error: Item Description should not be empty for" + ITEM_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (COST_CATEGORY == string.Empty || COST_CATEGORY.Length > 50)
                    {
                        data = "Error: Cost Category should not be empty for" + ITEM_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ORGN_TYPE == string.Empty || ORGN_TYPE.Length > 50)
                    {
                        data = "Error: Organization Type should not be empty for" + ITEM_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ITEM_GROUP != "TBG" && ITEM_GROUP != "BCG" && ITEM_GROUP != "VFG" && ITEM_GROUP != "CLG" && ITEM_GROUP != "ICG" && ITEM_GROUP != "GOG" && ITEM_GROUP != "BYP" && ITEM_GROUP != "PCG" && ITEM_GROUP != "LOSS" || ITEM_GROUP.Length > 3)
                    {
                        data = "Error: Item Group Should be specific" + ITEM_CODE + "and Length must be 3 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (ATTRIBUTE2 != "BT" && ATTRIBUTE2 != "BYP" && ATTRIBUTE2 != "CL" && ATTRIBUTE2 != "FGD" && ATTRIBUTE2 != "FGE" && ATTRIBUTE2 != "FW" && ATTRIBUTE2 != "GRD" && ATTRIBUTE2 != "LOSS" && ATTRIBUTE2 != "W" || ATTRIBUTE2.Length > 50)
                    {
                        data = "Error: SUBINVENTORY_CODE should be Specific" + ITEM_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (ATTRIBUTE3 != "B" && ATTRIBUTE3 != "M" && ATTRIBUTE3 != "L" && ATTRIBUTE3 != "G" && ATTRIBUTE3 != "" || ATTRIBUTE3.Length > 50)
                    {
                        data = "Error: BMGL_Grade should be empty for" + ITEM_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (ATTRIBUTE4.Trim() == string.Empty)
                    {
                        data = "Error: HSN CODE should be empty for" + ITEM_CODE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP,VARIETY,COST_CATEGORY,ORGN_TYPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                    query = query + " FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_ITEM_MASTER where ITEM_CODE='" + ITEM_CODE + "'  ";
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

                        if (ITEM_CODE_GROUP.ToUpper() == "PCG")
                        {
                            ITEM_CODE = ITEM_CODE.ToUpper().Substring(4, ITEM_CODE.Length - 4);
                        }
                        GPIL_ITEM_MASTER obj = null;
                        obj = new GPIL_ITEM_MASTER();

                        strQry = "INSERT INTO [dbo].[GPIL_ITEM_MASTER] ([ITEM_CODE],[ITEM_CODE_GROUP],[ITEM_GROUP],[ITEM_TYPE],[ITEM_DESC],[CROP],[VARIETY],[COST_CATEGORY],[ORGN_TYPE],[CREATED_BY],[CREATED_DATE],[ATTRIBUTE2],[ATTRIBUTE3],[ATTRIBUTE4],[STATUS]) ";
                        strQry = strQry + "VALUES('" + ITEM_CODE + "','" + ITEM_CODE_GROUP + "','" + ITEM_GROUP + "','" + ITEM_TYPE + "','" + ITEM_DESC + "','" + CROP + "','" + VARIETY + "','" + COST_CATEGORY + "','" + ORGN_TYPE + "','" + Session["userID"].ToString() + "',getdate(),'" + ATTRIBUTE2 + "','" + ATTRIBUTE3 + "','" + ATTRIBUTE4 + "','" + STATUS + "')";
                        lstQry.Add(strQry);

                    }
                    //else
                    //{

                    //    data = "Error: Item Code "+ ITEM_CODE + " is already exist So please check and import";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;
                    //}

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
