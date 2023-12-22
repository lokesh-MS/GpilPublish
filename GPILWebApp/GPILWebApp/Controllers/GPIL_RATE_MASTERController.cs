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

namespace GPILWebApp.Controllers
{
    public class GPIL_RATE_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();

        // GET: GPIL_RATE_MASTER
        public ActionResult Index()
        {
            return View(db.GPIL_RATE_MASTER.ToList());
        }

        // GET: GPIL_RATE_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_RATE_MASTER gPIL_RATE_MASTER = db.GPIL_RATE_MASTER.Find(id);
            if (gPIL_RATE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_RATE_MASTER);
        }

        // GET: GPIL_RATE_MASTER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GPIL_RATE_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Loc_Code,Item_Code,Rate,Last_Updated_Date")] GPIL_RATE_MASTER gPIL_RATE_MASTER)
        {

            //var roomDetails = db.Rooms.ToList();

            if (ModelState.IsValid)
            {
                GPIL_RATE_MASTER obj = new GPIL_RATE_MASTER();
                obj.Loc_Code = gPIL_RATE_MASTER.Loc_Code;
                obj.Item_Code = gPIL_RATE_MASTER.Item_Code;
                obj.Rate = gPIL_RATE_MASTER.Rate;

                obj.Last_Updated_Date = DateTime.Now;
                

                db.GPIL_RATE_MASTER.Add(obj);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
                //db.GPIL_RATE_MASTER.Add(gPIL_RATE_MASTER);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(gPIL_RATE_MASTER);
        }

        // GET: GPIL_RATE_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_RATE_MASTER gPIL_RATE_MASTER = db.GPIL_RATE_MASTER.Find(id);
            if (gPIL_RATE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_RATE_MASTER);
        }

        // POST: GPIL_RATE_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Loc_Code,Item_Code,Rate,Last_Updated_Date")] GPIL_RATE_MASTER gPIL_RATE_MASTER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPIL_RATE_MASTER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gPIL_RATE_MASTER);
        }

        // GET: GPIL_RATE_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_RATE_MASTER gPIL_RATE_MASTER = db.GPIL_RATE_MASTER.Find(id);
            if (gPIL_RATE_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_RATE_MASTER);
        }

        // POST: GPIL_RATE_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GPIL_RATE_MASTER gPIL_RATE_MASTER = db.GPIL_RATE_MASTER.Find(id);
            db.GPIL_RATE_MASTER.Remove(gPIL_RATE_MASTER);
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

        public ActionResult ExcelGrid()
        {
            return View();
        }

        public ActionResult ExcelIndex()
        {
            var Rate = db.GPIL_RATE_MASTER.ToList();
            return View(Rate.ToList());
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

                        conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name.  
                                sqlBulkCopy.DestinationTableName = "GPIL_RATE_MASTER";
                                sqlBulkCopy.ColumnMappings.Add("Loc_Code", "Loc_Code");
                                sqlBulkCopy.ColumnMappings.Add("Item_Code", "Item_Code");
                                sqlBulkCopy.ColumnMappings.Add("Rate", "Rate");
                                sqlBulkCopy.ColumnMappings.Add("Last_Updated_Date", "Last_Updated_Date");




                                con.Open();
                                sqlBulkCopy.WriteToServer(dt);
                                con.Close();

                                return RedirectToAction("Index");
                                //return Json("File uploaded successfully");
                                //return View("Index");

                            }
                        }
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

        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Please select a excel file";
                return View("ExcelIndex");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/ExcelUploads/" + excelfile.FileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    excelfile.SaveAs(path);
                    // Read Data from Excel File
                    Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(path);
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
                    List<GPIL_RATE_MASTER> lRate = new List<GPIL_RATE_MASTER>();
                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        GPIL_RATE_MASTER c = new GPIL_RATE_MASTER();
                        c.Loc_Code = ((Microsoft.Office.Interop.Excel.Range)range.Cells[row, 1]).Text;
                        c.Item_Code = ((Microsoft.Office.Interop.Excel.Range)range.Cells[row, 2]).Text;
                        c.Rate = ((Microsoft.Office.Interop.Excel.Range)range.Cells[row, 3]).Text;                        
                        lRate.Add(c);
                    }
                    ViewBag.LCrops = lRate;
                    workbook.Close(0);
                    return View("ExcelGrid");
                }
                else
                {
                    ViewBag.Error = "File Type is incorrect";
                    return View("ExcelIndex");
                }
            }
        }
    }
}
