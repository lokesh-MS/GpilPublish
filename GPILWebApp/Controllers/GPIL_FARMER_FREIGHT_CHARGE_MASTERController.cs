using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;


namespace GPILWebApp.Controllers
{
    public class GPIL_FARMER_FREIGHT_CHARGE_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: GPIL_FARMER_FREIGHT_CHARGE_MASTER


        [HttpPost]
        public ActionResult CheckVillageAvailability(string FreightID)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Where(x => x.FREIGHT_ID == FreightID).SingleOrDefault();
            if (usr != null)
            {
                if (FreightID == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,FREIGHT_ID,CROP,VARIETY,VILLAGE_CODE,ORGN_CODE,FREIGHT_CHARGE,REMARKS,LAST_FREIGHT_CHARGE,STATUS,FLAG,";
                query = query + "CREATED_BY ,CREATED_DATE ,LAST_UPDATED_BY,LAST_UPDATED_DATE,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_FARMER_FREIGHT_CHARGE_MASTER where FREIGHT_ID = '" + FreightID + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }

        }
        public ActionResult Index()
        {
            var gPIL_FARMER_FREIGHT_CHARGE_MASTER = db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Include(g => g.GPIL_CROP_MASTER).Include(g => g.GPIL_USER_MASTER).Include(g => g.GPIL_ORGN_MASTER).Include(g => g.GPIL_VARIETY_MASTER).Include(g => g.GPIL_VILLAGE_MASTER);
            return View(gPIL_FARMER_FREIGHT_CHARGE_MASTER.ToList());
        }

        // GET: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_FREIGHT_CHARGE_MASTER gPIL_FARMER_FREIGHT_CHARGE_MASTER = db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Find(id);
            if (gPIL_FARMER_FREIGHT_CHARGE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_FREIGHT_CHARGE_MASTER);
        }

        // GET: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Create
        public ActionResult Create()
        {
            var GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            var GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            var gPIL_ORGN_MASTER = (from O in db.GPIL_ORGN_MASTER where O.STATUS == "Y" select new { OrgnCode1 = O.ORGN_CODE + " - " + O.ORGN_NAME, O.ORGN_CODE }).ToList();
            var GPIL_VILLAGE_MASTER = (from Village in db.GPIL_VILLAGE_MASTER select new { VillageCode1 = Village.VILLAGE_CODE + " - " + Village.VILLAGE_NAME, Village.VILLAGE_CODE }).ToList();
            ViewBag.CROP = new SelectList(GPIL_CROP_MASTER, "CROP1", "CROP1");
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME");
            ViewBag.ORGN_CODE = new SelectList(gPIL_ORGN_MASTER, "OrgnCode1", "OrgnCode1");
            ViewBag.VARIETY = new SelectList(GPIL_VARIETY_MASTER, "VARIETY1", "VARIETY1");
            ViewBag.VILLAGE_CODE = new SelectList(GPIL_VILLAGE_MASTER, "VillageCode1", "VillageCode1");
            return View();
        }

        // POST: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNO,FREIGHT_ID,CROP,VARIETY,VILLAGE_CODE,ORGN_CODE,FREIGHT_CHARGE,REMARKS,LAST_FREIGHT_CHARGE,STATUS,FLAG,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_FARMER_FREIGHT_CHARGE_MASTER gPIL_FARMER_FREIGHT_CHARGE_MASTER)
        {
            if (ModelState.IsValid)
            {
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                query = " SELECT SNO,FREIGHT_ID,CROP,VARIETY,VILLAGE_CODE,ORGN_CODE,FREIGHT_CHARGE,REMARKS,LAST_FREIGHT_CHARGE,STATUS,FLAG,";
                query = query + "CREATED_BY ,CREATED_DATE ,LAST_UPDATED_BY,LAST_UPDATED_DATE,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5";
                query = query + " from GPIL_FARMER_FREIGHT_CHARGE_MASTER where FREIGHT_ID = '" + gPIL_FARMER_FREIGHT_CHARGE_MASTER.FREIGHT_ID + "' ";
                dtclstr = ppdMgt.GetQueryResult(query);
                if (dtclstr.Rows.Count == 0)
                {
                    GPIL_FARMER_FREIGHT_CHARGE_MASTER Obj = new GPIL_FARMER_FREIGHT_CHARGE_MASTER();
                    Obj.FREIGHT_ID = gPIL_FARMER_FREIGHT_CHARGE_MASTER.FREIGHT_ID;
                    Obj.CROP = gPIL_FARMER_FREIGHT_CHARGE_MASTER.CROP.ToString().Substring(0, 2);
                    Obj.VARIETY = gPIL_FARMER_FREIGHT_CHARGE_MASTER.VARIETY.ToString().Substring(0, 2);
                    Obj.VILLAGE_CODE = gPIL_FARMER_FREIGHT_CHARGE_MASTER.VILLAGE_CODE.ToString().Substring(0, 8);
                    Obj.ORGN_CODE = gPIL_FARMER_FREIGHT_CHARGE_MASTER.ORGN_CODE.ToString().Substring(0, 3);
                    Obj.FREIGHT_CHARGE = gPIL_FARMER_FREIGHT_CHARGE_MASTER.FREIGHT_CHARGE;
                    Obj.STATUS = gPIL_FARMER_FREIGHT_CHARGE_MASTER.STATUS;
                    Obj.FLAG = gPIL_FARMER_FREIGHT_CHARGE_MASTER.FLAG;
                    Obj.LAST_FREIGHT_CHARGE = gPIL_FARMER_FREIGHT_CHARGE_MASTER.LAST_FREIGHT_CHARGE;
                    Obj.CREATED_DATE = DateTime.Now;
                    Obj.LAST_UPDATED_DATE = gPIL_FARMER_FREIGHT_CHARGE_MASTER.LAST_UPDATED_DATE;
                    Obj.LAST_UPDATED_BY = gPIL_FARMER_FREIGHT_CHARGE_MASTER.LAST_UPDATED_BY;
                    Obj.LASTUPDATE = gPIL_FARMER_FREIGHT_CHARGE_MASTER.LASTUPDATE;
                    Obj.ATTRIBUTE1 = gPIL_FARMER_FREIGHT_CHARGE_MASTER.ATTRIBUTE1;
                    Obj.ATTRIBUTE2 = gPIL_FARMER_FREIGHT_CHARGE_MASTER.ATTRIBUTE2;
                    Obj.ATTRIBUTE3 = gPIL_FARMER_FREIGHT_CHARGE_MASTER.ATTRIBUTE3;
                    Obj.ATTRIBUTE4 = gPIL_FARMER_FREIGHT_CHARGE_MASTER.ATTRIBUTE4;
                    Obj.ATTRIBUTE5 = gPIL_FARMER_FREIGHT_CHARGE_MASTER.ATTRIBUTE5;
                    Obj.CREATED_BY = Session["userID"].ToString();
                    db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Add(Obj);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfully Created!!!";
                    ModelState.Clear();
                    //return RedirectToAction("Index");
                }
                else
                {
                    GPIL_FARMER_FREIGHT_CHARGE_MASTER Obj = (from C in db.GPIL_FARMER_FREIGHT_CHARGE_MASTER where C.FREIGHT_ID == gPIL_FARMER_FREIGHT_CHARGE_MASTER.FREIGHT_ID select C).Single();
                    Obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    Obj.LAST_UPDATED_DATE = DateTime.Now;
                    TempData["SuccessMessage"] = "Successfully Updated!!!";
                    db.SaveChanges();
                }
            }

            ViewBag.CROP = new SelectList(db.GPIL_CROP_MASTER, "CROP", "CROP_YEAR", gPIL_FARMER_FREIGHT_CHARGE_MASTER.CROP);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.CREATED_BY);
            ViewBag.ORGN_CODE = new SelectList(db.GPIL_ORGN_MASTER, "ORGN_CODE", "ORGN_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.ORGN_CODE);
            ViewBag.VARIETY = new SelectList(db.GPIL_VARIETY_MASTER, "VARIETY", "VARIETY_TYPE", gPIL_FARMER_FREIGHT_CHARGE_MASTER.VARIETY);
            ViewBag.VILLAGE_CODE = new SelectList(db.GPIL_VILLAGE_MASTER, "VILLAGE_CODE", "VILLAGE_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.VILLAGE_CODE);
            return View(gPIL_FARMER_FREIGHT_CHARGE_MASTER);
        }

        // GET: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_FREIGHT_CHARGE_MASTER gPIL_FARMER_FREIGHT_CHARGE_MASTER = db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Find(id);
            if (gPIL_FARMER_FREIGHT_CHARGE_MASTER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CROP = new SelectList(db.GPIL_CROP_MASTER, "CROP", "CROP_YEAR", gPIL_FARMER_FREIGHT_CHARGE_MASTER.CROP);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.CREATED_BY);
            ViewBag.ORGN_CODE = new SelectList(db.GPIL_ORGN_MASTER, "ORGN_CODE", "ORGN_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.ORGN_CODE);
            ViewBag.VARIETY = new SelectList(db.GPIL_VARIETY_MASTER, "VARIETY", "VARIETY_TYPE", gPIL_FARMER_FREIGHT_CHARGE_MASTER.VARIETY);
            ViewBag.VILLAGE_CODE = new SelectList(db.GPIL_VILLAGE_MASTER, "VILLAGE_CODE", "VILLAGE_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.VILLAGE_CODE);
            return View(gPIL_FARMER_FREIGHT_CHARGE_MASTER);
        }

        // POST: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,FREIGHT_ID,CROP,VARIETY,VILLAGE_CODE,ORGN_CODE,FREIGHT_CHARGE,REMARKS,LAST_FREIGHT_CHARGE,STATUS,FLAG,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,LASTUPDATE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_FARMER_FREIGHT_CHARGE_MASTER gPIL_FARMER_FREIGHT_CHARGE_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_FARMER_FREIGHT_CHARGE_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Successfully Updated!!!";
                //return RedirectToAction("Index");
            }
            ViewBag.CROP = new SelectList(db.GPIL_CROP_MASTER, "CROP", "CROP_YEAR", gPIL_FARMER_FREIGHT_CHARGE_MASTER.CROP);
            ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.CREATED_BY);
            ViewBag.ORGN_CODE = new SelectList(db.GPIL_ORGN_MASTER, "ORGN_CODE", "ORGN_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.ORGN_CODE);
            ViewBag.VARIETY = new SelectList(db.GPIL_VARIETY_MASTER, "VARIETY", "VARIETY_TYPE", gPIL_FARMER_FREIGHT_CHARGE_MASTER.VARIETY);
            ViewBag.VILLAGE_CODE = new SelectList(db.GPIL_VILLAGE_MASTER, "VILLAGE_CODE", "VILLAGE_NAME", gPIL_FARMER_FREIGHT_CHARGE_MASTER.VILLAGE_CODE);
            return View(gPIL_FARMER_FREIGHT_CHARGE_MASTER);
        }

        // GET: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_FREIGHT_CHARGE_MASTER gPIL_FARMER_FREIGHT_CHARGE_MASTER = db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Find(id);
            if (gPIL_FARMER_FREIGHT_CHARGE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_FREIGHT_CHARGE_MASTER);
        }

        // POST: GPIL_FARMER_FREIGHT_CHARGE_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_FARMER_FREIGHT_CHARGE_MASTER gPIL_FARMER_FREIGHT_CHARGE_MASTER = db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Find(id);
            db.GPIL_FARMER_FREIGHT_CHARGE_MASTER.Remove(gPIL_FARMER_FREIGHT_CHARGE_MASTER);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        public JsonResult FarmerFreightChargeMasterComplete(ListFarmerFreightChargeMaster LVLM)
        {
            MasterManagement MstrMgt = new MasterManagement();

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();

                DataTable dtGridLst = ToDataTable(LVLM.FarmerFreightChargeMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string FREIGHT_ID = dtGridLst.Rows[s]["FREIGHT_ID"].ToString();
                    string CROP = dtGridLst.Rows[s]["CROP"].ToString();
                    string VARIETY = dtGridLst.Rows[s]["VARIETY"].ToString();
                    string ORGN_CODE = dtGridLst.Rows[s]["ORGN_CODE"].ToString();
                    string LAST_FREIGHT_CHARGE = dtGridLst.Rows[s]["LAST_FREIGHT_CHARGE"].ToString();
                    string FREIGHT_CHARGE = dtGridLst.Rows[s]["FREIGHT_CHARGE"].ToString();
                    string VILLAGE_CODE = dtGridLst.Rows[s]["VILLAGE_CODE"].ToString();
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
                    else if (ORGN_CODE == "")
                    {
                        data = "Error: ORGN_CODE Is Empty!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (FREIGHT_CHARGE == "")
                    {
                        data = "Error: FREIGHT_CHARGE Is Empty!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (CROP == "")
                    {
                        data = "Error: CROP Is Empty!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (FREIGHT_ID == "" || FREIGHT_ID.Length > 15)
                    {
                        data = "Error: FREIGHT_ID code is not Empty and length less than 10";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else if (STATUS == "")
                    {
                        data = "Error:  status is not Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    query = " SELECT FREIGHT_ID from GPIL_FARMER_FREIGHT_CHARGE_MASTER where FREIGHT_ID='" + FREIGHT_ID + "' ";
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
                        GPIL_FARMER_FREIGHT_CHARGE_MASTER obj = null;
                        obj = new GPIL_FARMER_FREIGHT_CHARGE_MASTER();
                        strQry = "INSERT INTO [dbo].[GPIL_FARMER_FREIGHT_CHARGE_MASTER] ([FREIGHT_ID],[CROP],[VARIETY],[CREATED_BY],[CREATED_DATE],[ORGN_CODE],[FREIGHT_CHARGE],[VILLAGE_CODE],[STATUS]) ";
                        strQry = strQry + "VALUES('" + FREIGHT_ID + "','" + CROP + "','" + VARIETY + "','" + Session["userID"].ToString() + "',getdate(),'" + ORGN_CODE + "','" + FREIGHT_CHARGE + "','" + VILLAGE_CODE + "','" + STATUS + "')";
                        lstQry.Add(strQry);

                    }
                    else
                    {
                        data = "Error: FREIGHT_ID  " + FREIGHT_ID + " is already exist So please check and import";
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}