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
using Newtonsoft.Json;
using GPILWebApp.ViewModel;
using System.Text;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class GPIL_COMPANY_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: GPIL_COMPANY_MASTER
        public ActionResult Index()
        {

            db.Configuration.ProxyCreationEnabled = false;
            var res = db.GPIL_COMPANY_MASTER.Where(s => s.STATUS == "Y").ToList();
            return View(res);
            //var gPIL_COMPANY_MASTER = db.GPIL_COMPANY_MASTER.Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_USER_MASTER1);
            //return View(gPIL_COMPANY_MASTER.ToList());
        }

        // GET: GPIL_COMPANY_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_COMPANY_MASTER gPIL_COMPANY_MASTER = db.GPIL_COMPANY_MASTER.Find(id);
            if (gPIL_COMPANY_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_COMPANY_MASTER);
        }

        //Auto Populate Method

        [HttpPost]
        public ActionResult CheckCompanyAvailability(string Companydata)
        {
            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_COMPANY_MASTER.Where(x => x.COMPANY_CODE == Companydata).SingleOrDefault();
            if (usr != null)
            {
                if (Companydata == null)
                {
                    return Json(0);
                }
                MasterManagement MstrMgt = new MasterManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,COMPANY_CODE,COMPANY_NAME,SUPPLIER_FLAG,SUPPLIED_TO,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,";
                query = query + " ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,COMP_SHORT_NAME,COMP_GROUP_CODE from GPIL_COMPANY_MASTER where COMPANY_CODE='" + Companydata + "' ";
                dtclstr = MstrMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }


        // GET: GPIL_COMPANY_MASTER/Create
        public ActionResult Create()
        {
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            return View();
        }

        // POST: GPIL_COMPANY_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,COMPANY_CODE,COMPANY_NAME,SUPPLIER_FLAG,SUPPLIED_TO,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,COMP_SHORT_NAME,COMP_GROUP_CODE")] GPIL_COMPANY_MASTER gPIL_COMPANY_MASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MasterManagement MstrMgt = new MasterManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    query = " SELECT SNO,COMPANY_CODE,COMPANY_NAME,SUPPLIER_FLAG,SUPPLIED_TO,CREATED_BY,CREATED_DATE,";
                    query = query + " LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,COMP_SHORT_NAME,COMP_GROUP_CODE from GPIL_COMPANY_MASTER where COMPANY_CODE='" + gPIL_COMPANY_MASTER.COMPANY_CODE + "' ";
                    dtclstr = MstrMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        GPIL_COMPANY_MASTER obj = new GPIL_COMPANY_MASTER();
                        obj.COMPANY_CODE = gPIL_COMPANY_MASTER.COMPANY_CODE;
                        obj.COMPANY_NAME = gPIL_COMPANY_MASTER.COMPANY_NAME;
                        obj.SUPPLIER_FLAG = gPIL_COMPANY_MASTER.SUPPLIER_FLAG;
                        obj.SUPPLIED_TO = gPIL_COMPANY_MASTER.SUPPLIED_TO;
                        obj.STATUS = gPIL_COMPANY_MASTER.STATUS;
                        obj.COMP_SHORT_NAME = gPIL_COMPANY_MASTER.COMP_SHORT_NAME;
                        obj.COMP_GROUP_CODE = gPIL_COMPANY_MASTER.COMP_GROUP_CODE;
                        obj.CREATED_BY = Session["userID"].ToString();
                        obj.CREATED_DATE = DateTime.Now;
                        db.GPIL_COMPANY_MASTER.Add(obj);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        GPIL_COMPANY_MASTER obj = (from C in db.GPIL_COMPANY_MASTER where C.COMPANY_CODE == gPIL_COMPANY_MASTER.COMPANY_CODE select C).Single();
                        obj.COMPANY_CODE = gPIL_COMPANY_MASTER.COMPANY_CODE;
                        obj.COMPANY_NAME = gPIL_COMPANY_MASTER.COMPANY_NAME;
                        obj.SUPPLIER_FLAG = gPIL_COMPANY_MASTER.SUPPLIER_FLAG;
                        obj.SUPPLIED_TO = gPIL_COMPANY_MASTER.SUPPLIED_TO;
                        obj.STATUS = gPIL_COMPANY_MASTER.STATUS;
                        obj.COMP_SHORT_NAME = gPIL_COMPANY_MASTER.COMP_SHORT_NAME;
                        obj.COMP_GROUP_CODE = gPIL_COMPANY_MASTER.COMP_GROUP_CODE;
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

                return View(gPIL_COMPANY_MASTER);

            }

            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_COMPANY_MASTER.CREATED_BY);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_COMPANY_MASTER.CREATED_BY);
            return View("gPIL_COMPANY_MASTER");
        }

        // GET: GPIL_COMPANY_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_COMPANY_MASTER gPIL_COMPANY_MASTER = db.GPIL_COMPANY_MASTER.Find(id);
            if (gPIL_COMPANY_MASTER == null)
            {
                return HttpNotFound();
            }


            //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_COMPANY_MASTER.CREATED_BY);
            //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_COMPANY_MASTER.CREATED_BY);

            ViewBag.LAST_UPDATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_COMPANY_MASTER.LAST_UPDATED_BY);
            ViewBag.LAST_UPDATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_COMPANY_MASTER.LAST_UPDATED_BY);
            return View(gPIL_COMPANY_MASTER);
        }

        // POST: GPIL_COMPANY_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,COMPANY_CODE,COMPANY_NAME,SUPPLIER_FLAG,SUPPLIED_TO,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,COMP_SHORT_NAME,COMP_GROUP_CODE")] GPIL_COMPANY_MASTER gPIL_COMPANY_MASTER)
        {
            if (ModelState.IsValid)
            {

                MasterManagement MstrMgt = new MasterManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,COMPANY_CODE,COMPANY_NAME,SUPPLIER_FLAG,SUPPLIED_TO,CREATED_BY,CREATED_DATE,";
                query = query + " LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,COMP_SHORT_NAME,COMP_GROUP_CODE from GPIL_COMPANY_MASTER where COMPANY_CODE='" + gPIL_COMPANY_MASTER.COMPANY_CODE + "' ";
                dtclstr = MstrMgt.GetQueryResult(query);

                if (dtclstr.Rows.Count != 0)
                {

                    GPIL_COMPANY_MASTER obj = (from C in db.GPIL_COMPANY_MASTER where C.COMPANY_CODE == gPIL_COMPANY_MASTER.COMPANY_CODE select C).Single();
                    obj.COMPANY_CODE = gPIL_COMPANY_MASTER.COMPANY_CODE;
                    obj.COMPANY_NAME = gPIL_COMPANY_MASTER.COMPANY_NAME;
                    obj.SUPPLIER_FLAG = gPIL_COMPANY_MASTER.SUPPLIER_FLAG;
                    obj.SUPPLIED_TO = gPIL_COMPANY_MASTER.SUPPLIED_TO;
                    obj.STATUS = gPIL_COMPANY_MASTER.STATUS;
                    obj.COMP_SHORT_NAME = gPIL_COMPANY_MASTER.COMP_SHORT_NAME;
                    obj.COMP_GROUP_CODE = gPIL_COMPANY_MASTER.COMP_GROUP_CODE;
                    obj.CREATED_BY = gPIL_COMPANY_MASTER.CREATED_BY;
                    obj.CREATED_DATE = gPIL_COMPANY_MASTER.CREATED_DATE;
                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;

                    db.SaveChanges();
                    ModelState.Clear();
                }


            }
            return RedirectToAction("Index");

        }

        // GET: GPIL_COMPANY_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_COMPANY_MASTER gPIL_COMPANY_MASTER = db.GPIL_COMPANY_MASTER.Find(id);
            if (gPIL_COMPANY_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_COMPANY_MASTER);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {

                GPIL_COMPANY_MASTER gPIL_COMPANY_MASTER = db.GPIL_COMPANY_MASTER.Find(id);

                GPIL_COMPANY_MASTER obj = (from C in db.GPIL_COMPANY_MASTER where C.COMPANY_CODE == gPIL_COMPANY_MASTER.COMPANY_CODE select C).Single();



                obj.COMPANY_CODE = gPIL_COMPANY_MASTER.COMPANY_CODE;
                obj.COMPANY_NAME = gPIL_COMPANY_MASTER.COMPANY_NAME;
                obj.SUPPLIER_FLAG = gPIL_COMPANY_MASTER.SUPPLIER_FLAG;
                obj.SUPPLIED_TO = gPIL_COMPANY_MASTER.SUPPLIED_TO;
                obj.STATUS = "N";
                obj.COMP_SHORT_NAME = gPIL_COMPANY_MASTER.COMP_SHORT_NAME;
                obj.COMP_GROUP_CODE = gPIL_COMPANY_MASTER.COMP_GROUP_CODE;
                obj.CREATED_BY = gPIL_COMPANY_MASTER.CREATED_BY;
                obj.CREATED_DATE = gPIL_COMPANY_MASTER.CREATED_DATE;
                obj.LAST_UPDATED_BY = Session["userID"].ToString();
                obj.LAST_UPDATED_DATE = DateTime.Now;
                //db.GPIL_COMPANY_MASTER.Add(obj);
                db.SaveChanges();
                ModelState.Clear();


                return RedirectToAction("Index");



            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);

                return View("Index");

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
        //public JsonResult CompanyMasterComplete(ListCompanyMaster LCM)
        //{
        //    CompanyMasterdata(LCM);
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
        public JsonResult CompanyMasterComplete(ListCompanyMaster LCM)
        {

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {

                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LCM.CompanyMasters);
                string strQry = "";
                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string COMPANY_CODE = dtGridLst.Rows[s]["COMPANY_CODE"].ToString();
                    string COMPANY_NAME = dtGridLst.Rows[s]["COMPANY_NAME"].ToString();
                    string SUPPLIER_FLAG = dtGridLst.Rows[s]["SUPPLIER_FLAG"].ToString();
                    string SUPPLIED_TO = dtGridLst.Rows[s]["SUPPLIED_TO"].ToString();
                    string COMP_SHORT_NAME = dtGridLst.Rows[s]["COMP_SHORT_NAME"].ToString();
                    string COMP_GROUP_CODE = dtGridLst.Rows[s]["COMP_GROUP_CODE"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                    //string CREATED_BY = Session["userID"].ToString();
                    //string CREATED_DATE = DateTime.Now;
                   if(COMPANY_CODE == "" )
                    {
                        data = "Error: Company code should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                   else if (COMPANY_NAME == "")
                    {
                        data = "Error: Company name should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    if (SUPPLIER_FLAG == "")
                    {
                        data = "Error: Supplier flag should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                   else  if (SUPPLIED_TO == "")
                    {
                        data = "Error: Supplier TO should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (STATUS == "")
                    {
                        data = "Error: Supplier TO should not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }



                    // MasterManagement MstrMgt = new MasterManagement();
                    DataTable dtclstr = new DataTable();
                    string query = "";
                    //query = " SELECT SNO,ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP,VARIETY,COST_CATEGORY,ORGN_TYPE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,";
                    //query = query + " FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_ITEM_MASTER where ITEM_CODE='" + ITEM_CODE + "' ";

                    query = " SELECT SNO,COMPANY_CODE,COMPANY_NAME,SUPPLIER_FLAG,SUPPLIED_TO,CREATED_BY,CREATED_DATE,";
                    query = query + " LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,COMP_SHORT_NAME,COMP_GROUP_CODE from GPIL_COMPANY_MASTER where COMPANY_CODE='" + COMPANY_CODE + "' ";
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

                        GPIL_COMPANY_MASTER obj = null;
                        obj = new GPIL_COMPANY_MASTER();
                        strQry = "INSERT INTO [dbo].[GPIL_COMPANY_MASTER] ([COMPANY_CODE],[COMPANY_NAME],[SUPPLIER_FLAG],[SUPPLIED_TO],[COMP_SHORT_NAME],[COMP_GROUP_CODE],[CREATED_BY],[CREATED_DATE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + COMPANY_CODE + "','" + COMPANY_NAME + "','" + SUPPLIER_FLAG + "','" + SUPPLIED_TO + "','" + COMP_SHORT_NAME + "','" + COMP_GROUP_CODE + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "')";
                        lstQry.Add(strQry);
                    }
                    else
                    {

                        data = "Error: Company Code " + COMPANY_CODE + " is already exist So please check and import";
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
