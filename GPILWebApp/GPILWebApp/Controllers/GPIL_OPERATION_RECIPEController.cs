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
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using GPI;

namespace GPILWebApp.Controllers
{
    public class GPIL_OPERATION_RECIPEController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();


        [HttpPost]
        public ActionResult CheckRecipeAvailability(string Recipedata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_OPERATION_RECIPE.Where(x => x.RECIPE_CODE == Recipedata).SingleOrDefault();
            if (usr != null)
            {
                if (Recipedata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT RECIPE_CODE,OPERATION_RECIPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                query = query + "ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_OPERATION_RECIPE where RECIPE_CODE = '" + Recipedata + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }

        // GET: GPIL_OPERATION_RECIPE
        public ActionResult Index()
        {
            
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_OPERATION_RECIPE.Where(s => s.STATUS == "Y").ToList();

            return View(res);
        }

        // GET: GPIL_OPERATION_RECIPE/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_OPERATION_RECIPE gPIL_OPERATION_RECIPE = db.GPIL_OPERATION_RECIPE.Find(id);
            if (gPIL_OPERATION_RECIPE == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_OPERATION_RECIPE);
        }

        // GET: GPIL_OPERATION_RECIPE/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_OPERATION_RECIPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,RECIPE_CODE,OPERATION_RECIPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_OPERATION_RECIPE gPIL_OPERATION_RECIPE)
        {
            
            try
            {
                if (ModelState.IsValid)
                {

                    PPDManagement ppdMgt = new PPDManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT RECIPE_CODE,OPERATION_RECIPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + "ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                    query = query + " from GPIL_OPERATION_RECIPE where RECIPE_CODE='" + gPIL_OPERATION_RECIPE.RECIPE_CODE + "' ";
                    dtclstr = ppdMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_OPERATION_RECIPE obj = new GPIL_OPERATION_RECIPE();
                        obj.RECIPE_CODE = gPIL_OPERATION_RECIPE.RECIPE_CODE;
                        obj.OPERATION_RECIPE = gPIL_OPERATION_RECIPE.OPERATION_RECIPE;                        
                        obj.STATUS = gPIL_OPERATION_RECIPE.STATUS;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;

                        db.GPIL_OPERATION_RECIPE.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_OPERATION_RECIPE obj = (from C in db.GPIL_OPERATION_RECIPE where C.RECIPE_CODE == gPIL_OPERATION_RECIPE.RECIPE_CODE select C).Single();

                        obj.RECIPE_CODE = gPIL_OPERATION_RECIPE.RECIPE_CODE;
                        obj.OPERATION_RECIPE = gPIL_OPERATION_RECIPE.OPERATION_RECIPE;
                        obj.STATUS = gPIL_OPERATION_RECIPE.STATUS;
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
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return View(gPIL_OPERATION_RECIPE);
            }


            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_OPERATION_RECIPE.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_VILLAGE_MASTER, "USER_ID", "USER_NAME", gPIL_OPERATION_RECIPE.CREATED_BY);

            return View(gPIL_OPERATION_RECIPE);
        }

        // GET: GPIL_OPERATION_RECIPE/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_OPERATION_RECIPE gPIL_OPERATION_RECIPE = db.GPIL_OPERATION_RECIPE.Find(id);
            if (gPIL_OPERATION_RECIPE == null)
            {
                return HttpNotFound();
            }
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_OPERATION_RECIPE.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_OPERATION_RECIPE.CREATED_BY);
            return View(gPIL_OPERATION_RECIPE);
        }

        // POST: GPIL_OPERATION_RECIPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,RECIPE_CODE,OPERATION_RECIPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_OPERATION_RECIPE gPIL_OPERATION_RECIPE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_OPERATION_RECIPE obj = (from C in db.GPIL_OPERATION_RECIPE where C.RECIPE_CODE == gPIL_OPERATION_RECIPE.RECIPE_CODE select C).Single();

                    obj.RECIPE_CODE = gPIL_OPERATION_RECIPE.RECIPE_CODE;
                    obj.OPERATION_RECIPE = gPIL_OPERATION_RECIPE.OPERATION_RECIPE;
                    obj.STATUS = gPIL_OPERATION_RECIPE.STATUS;
                    obj.CREATED_BY = gPIL_OPERATION_RECIPE.CREATED_BY;
                    obj.CREATED_DATE = gPIL_OPERATION_RECIPE.CREATED_DATE;

                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(gPIL_OPERATION_RECIPE);
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

        // GET: GPIL_OPERATION_RECIPE/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_OPERATION_RECIPE gPIL_OPERATION_RECIPE = db.GPIL_OPERATION_RECIPE.Find(id);
            if (gPIL_OPERATION_RECIPE == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_OPERATION_RECIPE);
        }

        // POST: GPIL_OPERATION_RECIPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {            
            GPIL_OPERATION_RECIPE gPIL_OPERATION_RECIPE = db.GPIL_OPERATION_RECIPE.SingleOrDefault(m => m.RECIPE_CODE == id);

            GPIL_OPERATION_RECIPE obj = (from C in db.GPIL_OPERATION_RECIPE where C.RECIPE_CODE == gPIL_OPERATION_RECIPE.RECIPE_CODE select C).Single();

            obj.RECIPE_CODE = gPIL_OPERATION_RECIPE.RECIPE_CODE;
            obj.OPERATION_RECIPE = gPIL_OPERATION_RECIPE.OPERATION_RECIPE;
            obj.STATUS = "N";
            obj.CREATED_BY = gPIL_OPERATION_RECIPE.CREATED_BY;
            obj.CREATED_DATE = gPIL_OPERATION_RECIPE.CREATED_DATE;

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
                    
                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                }
            }
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

        MasterManagement MstrMgt = new MasterManagement();

        [HttpPost]
        public JsonResult OperationRecipeMasterComplete(ListOperationRecipe LOR)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LOR.OperationRecipe);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string RECIPE_CODE = dtGridLst.Rows[s]["RECIPE_CODE"].ToString();
                    string OPERATION_RECIPE = dtGridLst.Rows[s]["OPERATION_RECIPE"].ToString();                   
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                  
                    
                    DataTable dtclstr = new DataTable();

                    if (RECIPE_CODE.Trim() == string.Empty || RECIPE_CODE.Length > 50)
                    {
                        data = "Error: Receipe Code should not be empty for" + RECIPE_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (OPERATION_RECIPE.Trim() == string.Empty || OPERATION_RECIPE.Length > 50)
                    {
                        data = "Error: Operation Recipe should not be empty for" + RECIPE_CODE + "and Length must be 50 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    else if (STATUS.Trim() == string.Empty)
                    {
                        data = "Error: Status should not be empty for" + RECIPE_CODE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    string query = "";
                    query = " SELECT SNO,RECIPE_CODE,OPERATION_RECIPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,";
                    query = query + "ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_OPERATION_RECIPE where RECIPE_CODE='" + RECIPE_CODE + "' ";
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
                        GPIL_OPERATION_RECIPE obj = null;
                        obj = new GPIL_OPERATION_RECIPE();
                        strQry = "INSERT INTO [dbo].[GPIL_OPERATION_RECIPE] ([RECIPE_CODE],[OPERATION_RECIPE],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + RECIPE_CODE + "','" + OPERATION_RECIPE + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";

                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: Operation Recipe Code " + RECIPE_CODE + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                }
                //MasterManagement MstrMgt = new MasterManagement();
                //bool b = GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);
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
