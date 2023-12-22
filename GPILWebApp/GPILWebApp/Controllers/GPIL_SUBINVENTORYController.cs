using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System.Text;
using System.Data.Entity.Validation;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class GPIL_SUBINVENTORYController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        [HttpPost]
        public ActionResult CheckSubInventoryAvailability(string SubInventorydata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_SUBINVENTORY.Where(x => x.SUB_INV_CODE == SubInventorydata).SingleOrDefault();
            if (usr != null)
            {
                if (SubInventorydata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SUB_INV_CODE,SUB_INV_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                query = query + "FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_SUBINVENTORY where SUB_INV_CODE = '" + SubInventorydata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }


        // GET: GPIL_SUBINVENTORY
        public ActionResult Index()
        {
           

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_SUBINVENTORY.Where(s => s.STATUS == "Y").ToList();

            return View(res);
        }

        // GET: GPIL_SUBINVENTORY/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SUBINVENTORY gPIL_SUBINVENTORY = db.GPIL_SUBINVENTORY.Find(id);
            if (gPIL_SUBINVENTORY == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SUBINVENTORY);
        }

        // GET: GPIL_SUBINVENTORY/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_SUBINVENTORY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,SUB_INV_CODE,SUB_INV_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_SUBINVENTORY gPIL_SUBINVENTORY)
        {
            

            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SUB_INV_CODE,SUB_INV_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                    query = query + "FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                    query = query + " from GPIL_SUBINVENTORY where SUB_INV_CODE = '" + gPIL_SUBINVENTORY.SUB_INV_CODE + "' ";
                    
                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_SUBINVENTORY obj = new GPIL_SUBINVENTORY();
                        obj.SUB_INV_CODE = gPIL_SUBINVENTORY.SUB_INV_CODE;
                        obj.SUB_INV_DESC = gPIL_SUBINVENTORY.SUB_INV_DESC;
                        obj.STATUS = gPIL_SUBINVENTORY.STATUS;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_SUBINVENTORY.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_SUBINVENTORY obj = (from C in db.GPIL_SUBINVENTORY where C.SUB_INV_CODE == gPIL_SUBINVENTORY.SUB_INV_CODE select C).Single();

                        obj.SUB_INV_CODE = gPIL_SUBINVENTORY.SUB_INV_CODE;
                        obj.SUB_INV_DESC = gPIL_SUBINVENTORY.SUB_INV_DESC;
                        obj.STATUS = gPIL_SUBINVENTORY.STATUS;
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
                return View(gPIL_SUBINVENTORY);
            }


            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_SUBINVENTORY.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_SUBINVENTORY.CREATED_BY);

            return View(gPIL_SUBINVENTORY);
        }

        // GET: GPIL_SUBINVENTORY/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SUBINVENTORY gPIL_SUBINVENTORY = db.GPIL_SUBINVENTORY.Find(id);
            if (gPIL_SUBINVENTORY == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUBINVENTORY.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_SUBINVENTORY.CREATED_BY);
            return View(gPIL_SUBINVENTORY);
        }

        // POST: GPIL_SUBINVENTORY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,SUB_INV_CODE,SUB_INV_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_SUBINVENTORY gPIL_SUBINVENTORY)
        {
           


            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_SUBINVENTORY obj = (from C in db.GPIL_SUBINVENTORY where C.SUB_INV_CODE == gPIL_SUBINVENTORY.SUB_INV_CODE select C).Single();

                    obj.SUB_INV_CODE = gPIL_SUBINVENTORY.SUB_INV_CODE;
                    obj.SUB_INV_DESC = gPIL_SUBINVENTORY.SUB_INV_DESC;
                    obj.STATUS = gPIL_SUBINVENTORY.STATUS;
                    obj.CREATED_BY = gPIL_SUBINVENTORY.CREATED_BY;
                    obj.CREATED_DATE = gPIL_SUBINVENTORY.CREATED_DATE;
                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(gPIL_SUBINVENTORY);
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

        // GET: GPIL_SUBINVENTORY/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_SUBINVENTORY gPIL_SUBINVENTORY = db.GPIL_SUBINVENTORY.Find(id);
            if (gPIL_SUBINVENTORY == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_SUBINVENTORY);
        }

        // POST: GPIL_SUBINVENTORY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //GPIL_SUBINVENTORY gPIL_SUBINVENTORY = db.GPIL_SUBINVENTORY.Find(id);
            //db.GPIL_SUBINVENTORY.Remove(gPIL_SUBINVENTORY);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            GPIL_SUBINVENTORY gPIL_SUBINVENTORY = db.GPIL_SUBINVENTORY.SingleOrDefault(m => m.SUB_INV_CODE == id);

            GPIL_SUBINVENTORY obj = (from C in db.GPIL_SUBINVENTORY where C.SUB_INV_CODE == gPIL_SUBINVENTORY.SUB_INV_CODE select C).Single();

            obj.SUB_INV_CODE = gPIL_SUBINVENTORY.SUB_INV_CODE;
            obj.SUB_INV_DESC = gPIL_SUBINVENTORY.SUB_INV_DESC;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_SUBINVENTORY.CREATED_BY;
            obj.CREATED_DATE = gPIL_SUBINVENTORY.CREATED_DATE;

            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;

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
        public JsonResult SubInventoryMasterComplete(ListSubInventory LSI)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LSI.SubInventoryMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string SUB_INV_CODE = dtGridLst.Rows[s]["SUB_INV_CODE"].ToString();
                    string SUB_INV_DESC = dtGridLst.Rows[s]["SUB_INV_DESC"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();

                    MasterManagement MstrMgt = new MasterManagement();
                    DataTable dtclstr = new DataTable();

                    if (SUB_INV_CODE.Trim() == string.Empty || SUB_INV_CODE.Length > 5)
                    {
                        data = "Error: Sub_Inv_Code should not be empty for" + SUB_INV_CODE + "and Length must be 5 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (SUB_INV_DESC.Trim() == string.Empty || SUB_INV_DESC.Length > 50)
                    {
                        data = "Error: Sub Inventory Description should not be empty for" + SUB_INV_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (STATUS.Trim() == string.Empty)
                    {
                        data = "Error: Status should not be empty for" + SUB_INV_CODE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }



                    string query = "";
                    query = " SELECT SUB_INV_CODE,SUB_INV_DESC,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                    query = query + "FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_SUBINVENTORY where SUB_INV_CODE='" + SUB_INV_CODE + "' ";
                    
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
                        GPIL_SUBINVENTORY obj = null;
                        obj = new GPIL_SUBINVENTORY();

                        strQry = "INSERT INTO [dbo].[GPIL_SUBINVENTORY] ([SUB_INV_CODE],[SUB_INV_DESC],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + SUB_INV_CODE + "','" + SUB_INV_DESC + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";

                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: Operation Recipe Code " + SUB_INV_CODE + " is already exist So please check and import";
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
