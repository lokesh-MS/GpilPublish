using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using Newtonsoft.Json;
using DataTables;
using System.Net;

namespace GPILWebApp.Controllers
{

    public class ItemController : Controller
    {
        // GET: Item
        GREEN_LEAF_TRACEABILITYEntities dbObj = new GREEN_LEAF_TRACEABILITYEntities();
        public ActionResult Index()
        {
            var OrgnType = dbObj.GPIL_ORGN_MASTER
                 .Select(i => i.ORGN_TYPE)
                 .Distinct();

            //TempData["GPIL_ORGN_MASTER"] = OrgnType;
            ViewBag.GPIL_ORGN_MASTER = new SelectList(OrgnType);

            var SubInv = dbObj.GPIL_SUBINVENTORY.Where((m => m.STATUS == "Y"))
                .Select(i => i.SUB_INV_CODE)
                .Distinct();
            //TempData["SubInventoryType"] = SubInv;
            ViewBag.SubInventoryType = new SelectList(SubInv);




            return View();



        }

        public ActionResult ExcelIndex()
        {
            return View();
        }

        public ActionResult GetItems(string sord = "DESC", int page = 10, int rows = 100, string searchString = "")
        {
            //#1 Create Instance of DatabaseContext class for Accessing Database.  
            //GREEN_LEAF_TRACEABILITYEntities dbObj = new GREEN_LEAF_TRACEABILITYEntities();

            //#2 Setting Paging  

            //#3 Linq Query to Get Customer   
            //var Results = dbObj.GPIL_ITEM_MASTER.Select(
            //    a => new
            //    {
            //        a.ITEM_CODE,
            //        a.ITEM_GROUP,
            //        a.ITEM_CODE_GROUP,
            //        a.CROP,
            //        a.VARIETY,
            //        a.ORGN_TYPE,
            //        a.ITEM_TYPE,
            //        a.COST_CATEGORY,
            //        a.ATTRIBUTE1,
            //        a.ATTRIBUTE3,
            //        a.ATTRIBUTE4,
            //    });

            //return View(Results.ToList());

            var res = dbObj.GPIL_ITEM_MASTER.Where(s => s.CROP == "21").Take(1500).ToList();
            return View(res);

        }


        [HttpPost]
        public ActionResult ItemMaster(GPIL_ITEM_MASTER item)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    GPIL_ITEM_MASTER obj = new GPIL_ITEM_MASTER();
                    obj.ITEM_CODE = item.ITEM_CODE;
                    obj.ITEM_CODE_GROUP = item.ITEM_CODE_GROUP;
                    obj.ITEM_GROUP = item.ITEM_GROUP;
                    obj.ITEM_TYPE = item.ITEM_TYPE;
                    obj.ITEM_DESC = item.ITEM_DESC;
                    obj.CROP = item.CROP;
                    obj.VARIETY = item.VARIETY;
                    obj.COST_CATEGORY = item.COST_CATEGORY;
                    obj.ORGN_TYPE = item.ORGN_TYPE;
                    obj.CREATED_BY = Session["userID"].ToString();
                    obj.CREATED_DATE = DateTime.Now;
                    obj.STATUS = item.STATUS;
                    obj.ATTRIBUTE2 = item.ATTRIBUTE2;
                    obj.ATTRIBUTE3 = item.ATTRIBUTE3.Substring(0, 1);
                    dbObj.GPIL_ITEM_MASTER.Add(obj);
                    dbObj.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                    //return View("Index","Item");

                }
                var OrgnType = dbObj.GPIL_ORGN_MASTER
                .Select(i => i.ORGN_TYPE)
                .Distinct();

                //TempData["GPIL_ORGN_MASTER"] = OrgnType;
                ViewBag.GPIL_ORGN_MASTER = new SelectList(OrgnType);

                var SubInv = dbObj.GPIL_SUBINVENTORY.Where((m => m.STATUS == "Y"))
                    .Select(i => i.SUB_INV_CODE)
                    .Distinct();
                //TempData["SubInventoryType"] = SubInv;
                ViewBag.SubInventoryType = new SelectList(SubInv);


                return View("Index");
            }
            catch { return View(); }

        }

        public ActionResult ItemList()
        {
            dbObj.Configuration.ProxyCreationEnabled = false;
            var res = dbObj.GPIL_ITEM_MASTER.Where(s => s.CROP == "21").OrderByDescending(s => s.CREATED_DATE).Take(500).ToList();

            return View(res);

            //var Results = dbObj.GPIL_ITEM_MASTER.Select(
            //    a => new
            //    {
            //        a.SNO,
            //        a.ITEM_CODE,
            //        a.ITEM_GROUP,
            //        a.ITEM_CODE_GROUP,
            //        a.CROP,
            //        a.VARIETY,
            //        a.ORGN_TYPE,
            //        a.ITEM_TYPE,
            //        a.COST_CATEGORY,
            //        a.ATTRIBUTE1,
            //        a.ATTRIBUTE3,
            //        a.ATTRIBUTE4,
            //    });

            // return View(Results.ToList());

            //var jsonResult = Json(new { data = Results }, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        DataSet ds = new DataSet();
        [HttpPost]
        public JsonResult ImportExcelData()
        {
           
            HttpPostedFileBase postedFile = null;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                postedFile = Request.Files[0];
            }
            string filePath = string.Empty;
            if (postedFile != null)
            {
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
                    case ".xls": //Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conString = string.Format(conString, filePath);
                try
                {
                    string query = "select ITEM_CODE,ITEM_CODE_GROUP,ITEM_GROUP,ITEM_TYPE,ITEM_DESC,CROP_YEAR,VARIETY_CODE, COST_CATEGORY, ORG_TYPE ,SUBINVENTORY_CODE ,BMLG_GRADE,HSNCode,INS_STS from [Sheet1$]";
                    OleDbDataAdapter odaExcel = new OleDbDataAdapter(query, conString);
                    odaExcel.Fill(ds);
                }
                catch (Exception ex)
                {
                }
            }
            string json = JsonConvert.SerializeObject(ds);
            //json = json.Replace(@"\", " ");
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetExcelData()
        {
            return View();
        }

        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    GPIL_ITEM_MASTER gpil_itemMaster = dbObj.GPIL_ITEM_MASTER.Find(id);
        //    if (gpil_itemMaster == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gpil_itemMaster);
        //}


    }
}