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
    public class GPIL_FARMER_MASTERController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();


        [HttpPost]
        public ActionResult CheckFarmerAvailability(string Farmerdata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_FARMER_MASTER.Where(x => x.FARM_CODE == Farmerdata).SingleOrDefault();
            if (usr == null)
            {
                var villageCode = Farmerdata.Substring(0, 8);
                var response = new
                {
                    VillageCode = villageCode
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else if (usr != null)
            {
                if (Farmerdata == null)
                {
                    return Json(0);
                }
                PPDManagement ppdMgt = new PPDManagement();
                DataTable dtclstr = new DataTable();
                string query = "";
                //query = " SELECT SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,";
                //query = query + " COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_FARMER_MASTER where FARM_CODE='" + Farmerdata + "' ";


                query = "  SELECT FM.SNO,FM.FARM_CODE,FM.FARM_CATEGORY,FM.FARM_NAME,FM.FARM_FATHER_NAME,FM.VILLAGE_CODE,FM.SOIL_TYPE,";
                query = query + "FM.FARM_ADDRESS1,FM.FARM_ADDRESS2,FM.FARM_ADDRESS3,FM.FARM_ADDRESS4,FM.FARM_ADDRESS5,FM.FARM_ADDRESS6,";
                query = query + " FM.COUNTRY,FM.PIN_CODE,FM.TEL_NO,FM.MOBILE_NO,FM.EMAIL_ID,FM.BANK_ACCOUNT_NO,FM.BANK_NAME,FM.BRANCH_NAME,";
                query = query + "FM.IFSC_CODE,FM.CREATED_BY,FM.CREATED_DATE,FM.LAST_UPDATED_BY,FM.LAST_UPDATED_DATE,FM.STATUS,FM.FLAG,FM.LASTUPDATE,";
                query = query + "FM.LOAN_AMOUNT,FM.ALERT_FLAG,FM.ALERT_MSG,FM.ATTRIBUTE1,FM.ATTRIBUTE2,FM.ATTRIBUTE3 as A3,FM.ATTRIBUTE4,FM.ATTRIBUTE5,";
                query = query + "FCH.CROP,FCH.VARIETY,FCH.ATTRIBUTE1 as A1,FCH.ATTRIBUTE2 as A2,FCH.ATTRIBUTE3,FCH.ATTRIBUTE4 as A4,FCH.ATTRIBUTE5 as A5";
                query = query + " from GPIL_FARMER_MASTER FM Join[dbo].[GPIL_FARMER_CROP_HISTORY]  FCH on FM.FARM_CODE = FCH.FARM_CODE";
                query = query + " where FM.FARM_CODE = '" + Farmerdata + "'";



                dtclstr = ppdMgt.GetQueryResult(query);
                string json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(0);
            }


            //else
            //{
            //    // Farmer code is not available in the database
            //    // Assign village code text field = first 8 characters in farmer code

            //    // Assuming you have a text field for the Village Code with ID: txtVillageCode
            //    var villageCode = Farmerdata.Substring(0, 8);
            //    var response = new
            //    {
            //        VillageCode = villageCode
            //    };

            //    return Json(response, JsonRequestBehavior.AllowGet);
            //}

        }


        // GET: GPIL_FARMER_MASTER
        public ActionResult Index()
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "  SELECT SUBSTRING(FINANCIAL_YEAR,3,2) AS YEAR  FROM [dbo].[GPIL_FINANCIAL_YEAR] ";
            dtclstr = ppdMgt.GetQueryResult(query);
            string strYear = "";

            if (dtclstr.Rows.Count > 0)
            {
                strYear = dtclstr.Rows[0]["YEAR"].ToString();
            }

            db.Configuration.ProxyCreationEnabled = false;

            //var res = db.GPIL_FARMER_CROP_HISTORY.Where(s => s.CROP == strYear).ToList();           
            //HashSet<string> sentIDs = new HashSet<string>(db.GPIL_FARMER_CROP_HISTORY.Select (s => s.FARM_CODE ));           
            //var res1 = db.GPIL_FARMER_MASTER.Where(s=> !sentIDs.Contains(s.FARM_CODE));

            var td =   from s in db.GPIL_FARMER_MASTER
                       join r in db.GPIL_FARMER_CROP_HISTORY on s.FARM_CODE equals r.FARM_CODE
                       where r.CROP == strYear && s.STATUS=="Y"   select s;


            return View(td);
        }

        // GET: GPIL_FARMER_MASTER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            if (gPIL_FARMER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_MASTER);
        }

        // GET: GPIL_FARMER_MASTER/Create
        public ActionResult Create()
        {

            ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();


            var VillageCode = db.GPIL_VILLAGE_MASTER
                 .Select(i => i.VILLAGE_CODE)
                 .Distinct();
            ViewBag.GPIL_VILLAGE_MASTER = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER
                 .Select(i => i.SOIL_TYPE)
                 .Distinct();
            ViewBag.GPIL_SOIL_MASTER = new SelectList(SoilType);

            return View();
        }



        // POST: GPIL_FARMER_MASTER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        //public ActionResult Create(GPIL_FARMER_MASTER gPIL_FARMER_MASTER)
        //{
        //    var VillageCode = db.GPIL_VILLAGE_MASTER
        //         .Select(i => i.VILLAGE_CODE)
        //         .Distinct();
        //    ViewBag.GPIL_VILLAGE_MASTER = new SelectList(VillageCode);

        //    var SoilType = db.GPIL_SOIL_MASTER
        //         .Select(i => i.SOIL_TYPE)
        //         .Distinct();
        //    ViewBag.GPIL_SOIL_MASTER = new SelectList(SoilType);


        //}


        //[Bind(Include = "SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FarmerMaster_FarmerCropHistory gPIL_FARMER_MASTER)
        {
            ViewBag.GPIL_CROP_MASTER = (from c in db.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in db.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            var VillageCode = db.GPIL_VILLAGE_MASTER
                 .Select(i => i.VILLAGE_CODE)
                 .Distinct();
            ViewBag.GPIL_VILLAGE_MASTER = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER
                 .Select(i => i.SOIL_TYPE)
                 .Distinct();
            ViewBag.GPIL_SOIL_MASTER = new SelectList(SoilType);

            if (string.IsNullOrEmpty(gPIL_FARMER_MASTER.User.COUNTRY))
            {
                // Set "India" as the default value
                gPIL_FARMER_MASTER.User.COUNTRY = "INDIA";
            }
            if (string.IsNullOrEmpty(gPIL_FARMER_MASTER.User.FARM_CATEGORY))
            {
                // Set "India" as the default value
                gPIL_FARMER_MASTER.User.FARM_CATEGORY = "REGISTERED";
            }

            try
            {
                if (ModelState.IsValid)
                {

                    if (IsAadhaarExists(gPIL_FARMER_MASTER.User.ATTRIBUTE3))
                    {
                        ModelState.AddModelError("AadhaarNumber", "Aadhaar number already exists for another farmer.");
                        return View(gPIL_FARMER_MASTER);
                    }

                    using (GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities())
                    {

                        PPDManagement ppdMgt = new PPDManagement();
                        DataTable dtclstr = new DataTable();
                        //string query = "";
                        // where FARM_CODE='" + gPIL_FARMER_MASTER.User.FARM_CODE + "' 
                        var v = db.GPIL_FARMER_MASTER.Where(a => a.FARM_CODE.Equals(gPIL_FARMER_MASTER.User.FARM_CODE)).FirstOrDefault();
                        //query = " SELECT SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,";
                        //query = query + " COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_FARMER_MASTER";

                        //dtclstr = ppdMgt.GetQueryResult(query);

                        
                        if (v  == null )
                        {
                            //FarmerMaster_FarmerCropHistory mymodel = new FarmerMaster_FarmerCropHistory();
                            GPIL_FARMER_MASTER obj = new GPIL_FARMER_MASTER();
                            obj.FARM_CODE = gPIL_FARMER_MASTER.User.FARM_CODE;
                            obj.FARM_CATEGORY = gPIL_FARMER_MASTER.User.FARM_CATEGORY;
                            obj.FARM_NAME = gPIL_FARMER_MASTER.User.FARM_NAME;
                            obj.FARM_FATHER_NAME = gPIL_FARMER_MASTER.User.FARM_FATHER_NAME;
                            obj.VILLAGE_CODE = gPIL_FARMER_MASTER.User.VILLAGE_CODE;
                            obj.ALERT_MSG = gPIL_FARMER_MASTER.User.ALERT_MSG;
                            obj.SOIL_TYPE = gPIL_FARMER_MASTER.User.SOIL_TYPE;
                            obj.FARM_ADDRESS1 = gPIL_FARMER_MASTER.User.FARM_ADDRESS1;
                            obj.FARM_ADDRESS2 = gPIL_FARMER_MASTER.User.FARM_ADDRESS2;
                            obj.FARM_ADDRESS3 = gPIL_FARMER_MASTER.User.FARM_ADDRESS3;
                            obj.FARM_ADDRESS4 = gPIL_FARMER_MASTER.User.FARM_ADDRESS4;
                            obj.FARM_ADDRESS5 = gPIL_FARMER_MASTER.User.FARM_ADDRESS5;
                            obj.FARM_ADDRESS6 = gPIL_FARMER_MASTER.User.FARM_ADDRESS6;
                            obj.COUNTRY = gPIL_FARMER_MASTER.User.COUNTRY;
                            obj.PIN_CODE = gPIL_FARMER_MASTER.User.PIN_CODE;
                            obj.TEL_NO = gPIL_FARMER_MASTER.User.TEL_NO;
                            obj.MOBILE_NO = gPIL_FARMER_MASTER.User.MOBILE_NO;
                            obj.EMAIL_ID = gPIL_FARMER_MASTER.User.EMAIL_ID;
                            obj.CREATED_BY = Session["userID"].ToString();
                            obj.CREATED_DATE = DateTime.Now;
                            obj.STATUS = gPIL_FARMER_MASTER.User.STATUS;

                            obj.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.User.BANK_ACCOUNT_NO;
                            obj.BANK_NAME = gPIL_FARMER_MASTER.User.BANK_NAME;
                            obj.BRANCH_NAME = gPIL_FARMER_MASTER.User.BRANCH_NAME;
                            obj.IFSC_CODE = gPIL_FARMER_MASTER.User.IFSC_CODE;
                            obj.ATTRIBUTE3 = gPIL_FARMER_MASTER.User.ATTRIBUTE3;


                            //db.SaveChanges();

                            //                        INSERT INTO GPIL_FARMER_CROP_HISTORY(HIS_CODE, FARM_CODE, CROP,
                            //                            VARIETY, MOBILE_NO, EMAIL_ID, BANK_ACCOUNT_NO, BANK_NAME, BRANCH_NAME, 
                            //IFSC_CODE, CREATED_BY, CREATED_DATE, STATUS, LOAN_AMOUNT, BALANCE_AMOUNT,
                            //ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3, ATTRIBUTE4, ATTRIBUTE5
                            var His_Code = gPIL_FARMER_MASTER.Login.CROP + gPIL_FARMER_MASTER.Login.VARIETY + gPIL_FARMER_MASTER.User.FARM_CODE;
                            //GPIL_FARMER_CROP_HISTORY obj1 = new GPIL_FARMER_CROP_HISTORY();




                            GPIL_FARMER_CROP_HISTORY obj1 = new GPIL_FARMER_CROP_HISTORY();
                            obj1.HIS_CODE = His_Code;
                            obj1.FARM_CODE = gPIL_FARMER_MASTER.User.FARM_CODE;
                            obj1.CROP = gPIL_FARMER_MASTER.Login.CROP;
                            obj1.VARIETY = gPIL_FARMER_MASTER.Login.VARIETY;
                            obj1.MOBILE_NO = gPIL_FARMER_MASTER.User.MOBILE_NO;
                            obj1.EMAIL_ID = gPIL_FARMER_MASTER.User.EMAIL_ID;
                            obj1.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.User.BANK_ACCOUNT_NO;
                            obj1.BANK_NAME = gPIL_FARMER_MASTER.User.BANK_NAME;
                            obj1.BRANCH_NAME = gPIL_FARMER_MASTER.User.BRANCH_NAME;
                            obj1.IFSC_CODE = gPIL_FARMER_MASTER.User.IFSC_CODE;
                            obj1.CREATED_BY = Session["userID"].ToString();
                            obj1.CREATED_DATE = DateTime.Now;
                            obj1.STATUS = gPIL_FARMER_MASTER.User.STATUS;
                            obj1.LOAN_AMOUNT = gPIL_FARMER_MASTER.User.LOAN_AMOUNT;
                            obj1.BALANCE_AMOUNT = 0.00;
                            obj1.ATTRIBUTE1 = gPIL_FARMER_MASTER.Login.ATTRIBUTE1;
                            obj1.ATTRIBUTE2 = gPIL_FARMER_MASTER.Login.ATTRIBUTE2;
                            obj1.ATTRIBUTE3 = gPIL_FARMER_MASTER.Login.ATTRIBUTE3;
                            obj1.ATTRIBUTE4 = gPIL_FARMER_MASTER.Login.ATTRIBUTE4;
                            obj1.ATTRIBUTE5 = gPIL_FARMER_MASTER.Login.ATTRIBUTE5;
                            db.GPIL_FARMER_MASTER.Add(obj);
                            db.GPIL_FARMER_CROP_HISTORY.Add(obj1);
                            db.SaveChanges();
                            ModelState.Clear();
                             ModelState.AddModelError(obj.FARM_CODE, "Successfully Farmer Code created");
                            TempData["SuccessMessage"] = "Successfully Created!!!";
                            
                        }
                        else
                        {
                            GPIL_FARMER_MASTER obj = (from C in db.GPIL_FARMER_MASTER where C.FARM_CODE == gPIL_FARMER_MASTER.User.FARM_CODE select C).Single();

                            //GPIL_FARMER_MASTER obj = new GPIL_FARMER_MASTER();
                            obj.FARM_CODE = gPIL_FARMER_MASTER.User.FARM_CODE;
                            obj.FARM_CATEGORY = gPIL_FARMER_MASTER.User.FARM_CATEGORY;
                            obj.FARM_NAME = gPIL_FARMER_MASTER.User.FARM_NAME;
                            obj.FARM_FATHER_NAME = gPIL_FARMER_MASTER.User.FARM_FATHER_NAME;
                            obj.VILLAGE_CODE = gPIL_FARMER_MASTER.User.VILLAGE_CODE;
                            obj.SOIL_TYPE = gPIL_FARMER_MASTER.User.SOIL_TYPE;
                            obj.FARM_ADDRESS1 = gPIL_FARMER_MASTER.User.FARM_ADDRESS1;
                            obj.FARM_ADDRESS2 = gPIL_FARMER_MASTER.User.FARM_ADDRESS2;
                            obj.FARM_ADDRESS3 = gPIL_FARMER_MASTER.User.FARM_ADDRESS3;
                            obj.FARM_ADDRESS4 = gPIL_FARMER_MASTER.User.FARM_ADDRESS4;
                            obj.FARM_ADDRESS5 = gPIL_FARMER_MASTER.User.FARM_ADDRESS5;
                            obj.FARM_ADDRESS6 = gPIL_FARMER_MASTER.User.FARM_ADDRESS6;
                            obj.COUNTRY = gPIL_FARMER_MASTER.User.COUNTRY;
                            obj.PIN_CODE = gPIL_FARMER_MASTER.User.PIN_CODE;
                            obj.TEL_NO = gPIL_FARMER_MASTER.User.TEL_NO;
                            obj.MOBILE_NO = gPIL_FARMER_MASTER.User.MOBILE_NO;
                            obj.EMAIL_ID = gPIL_FARMER_MASTER.User.EMAIL_ID;
                            obj.LAST_UPDATED_BY = Session["userID"].ToString();
                            obj.LAST_UPDATED_DATE = DateTime.Now;
                            obj.STATUS = gPIL_FARMER_MASTER.User.STATUS;

                            obj.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.User.BANK_ACCOUNT_NO;
                            obj.BANK_NAME = gPIL_FARMER_MASTER.User.BANK_NAME;
                            obj.BRANCH_NAME = gPIL_FARMER_MASTER.User.BRANCH_NAME;
                            obj.IFSC_CODE = gPIL_FARMER_MASTER.User.IFSC_CODE;
                            obj.ATTRIBUTE3 = gPIL_FARMER_MASTER.User.ATTRIBUTE3;

                            var His_Code = gPIL_FARMER_MASTER.Login.CROP + gPIL_FARMER_MASTER.Login.VARIETY + gPIL_FARMER_MASTER.User.FARM_CODE;
                            

                            GPIL_FARMER_CROP_HISTORY obj1 = db.GPIL_FARMER_CROP_HISTORY.Where(a => a.FARM_CODE.Equals(gPIL_FARMER_MASTER.User.FARM_CODE)).FirstOrDefault();
                            //GPIL_FARMER_CROP_HISTORY obj1 = new GPIL_FARMER_CROP_HISTORY();
                            obj1.HIS_CODE = His_Code;
                            obj1.FARM_CODE = gPIL_FARMER_MASTER.User.FARM_CODE;
                            obj1.CROP = gPIL_FARMER_MASTER.Login.CROP;
                            obj1.VARIETY = gPIL_FARMER_MASTER.Login.VARIETY;
                            obj1.MOBILE_NO = gPIL_FARMER_MASTER.User.MOBILE_NO;
                            obj1.EMAIL_ID = gPIL_FARMER_MASTER.User.EMAIL_ID;
                            obj1.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.User.BANK_ACCOUNT_NO;
                            obj1.BANK_NAME = gPIL_FARMER_MASTER.User.BANK_NAME;
                            obj1.BRANCH_NAME = gPIL_FARMER_MASTER.User.BRANCH_NAME;
                            obj1.IFSC_CODE = gPIL_FARMER_MASTER.User.IFSC_CODE;
                            obj1.LAST_UPDATED_BY = Session["userID"].ToString();
                            obj1.LAST_UPDATED_DATE = DateTime.Now;
                            obj1.STATUS = gPIL_FARMER_MASTER.User.STATUS;
                            obj1.LOAN_AMOUNT = gPIL_FARMER_MASTER.User.LOAN_AMOUNT;
                            obj1.BALANCE_AMOUNT = 0.00;
                            obj1.ATTRIBUTE1 = gPIL_FARMER_MASTER.Login.ATTRIBUTE1;
                            obj1.ATTRIBUTE2 = gPIL_FARMER_MASTER.Login.ATTRIBUTE2;
                            obj1.ATTRIBUTE3 = gPIL_FARMER_MASTER.Login.ATTRIBUTE3;
                            obj1.ATTRIBUTE4 = gPIL_FARMER_MASTER.Login.ATTRIBUTE4;
                            obj1.ATTRIBUTE5 = gPIL_FARMER_MASTER.Login.ATTRIBUTE5;
                         
                            db.SaveChanges();

                            TempData["SuccessMessage"] = "Successfully Updated!!!";

                        }
                    //return RedirectToAction("Create");

                        //var response = new
                        //{
                        //    AlertMessage = "Created"
                        //};
                        //return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    ModelState.AddModelError(gPIL_FARMER_MASTER.User.FARM_CODE, " Farmer Code Creation Failed.");
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
                return View(gPIL_FARMER_MASTER);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_FARMER_MASTER.CREATED_BY);
            //ViewBag.CREATED_BY = new SelectList(db.GPIL_USER_MASTER, "USER_ID", "USER_NAME", gPIL_FARMER_MASTER.CREATED_BY);

            return View(gPIL_FARMER_MASTER);
        }

        // GET: GPIL_FARMER_MASTER/Edit/5
        public ActionResult Edit(string id)
        {
            var VillageCode = db.GPIL_VILLAGE_MASTER
                .Select(i => i.VILLAGE_CODE)
                .Distinct();
            ViewBag.GPIL_VILLAGE_MASTER = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER
                 .Select(i => i.SOIL_TYPE)
                 .Distinct();
            ViewBag.GPIL_SOIL_MASTER = new SelectList(SoilType);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            //ViewBag.VILLAGE_CODE = new SelectList(db.GPIL_VILLAGE_MASTER, "VILLAGE_CODE", "VILLAGE_CODE", gPIL_FARMER_MASTER.VILLAGE_CODE);
            //ViewBag.SOIL_TYPE = new SelectList(db.GPIL_SOIL_MASTER, "SOIL_TYPE", "SOIL_TYPE", gPIL_FARMER_MASTER.SOIL_TYPE);
            if (gPIL_FARMER_MASTER == null)
            {
                return HttpNotFound();
            }


           

            return View(gPIL_FARMER_MASTER);
            //return View(gPIL_FARMER_MASTER);
        }

        // POST: GPIL_FARMER_MASTER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5")] GPIL_FARMER_MASTER gPIL_FARMER_MASTER)
        {
            var VillageCode = db.GPIL_VILLAGE_MASTER
                 .Select(i => i.VILLAGE_CODE)
                 .Distinct();
            ViewBag.GPIL_VILLAGE_MASTER = new SelectList(VillageCode);

            var SoilType = db.GPIL_SOIL_MASTER
                 .Select(i => i.SOIL_TYPE)
                 .Distinct();
            ViewBag.GPIL_SOIL_MASTER = new SelectList(SoilType);
            // lokesh this change this code

            if (string.IsNullOrEmpty(gPIL_FARMER_MASTER.COUNTRY))
            {
                // Set "India" as the default value
                gPIL_FARMER_MASTER.COUNTRY = "INDIA";
            }
            if (string.IsNullOrEmpty(gPIL_FARMER_MASTER.FARM_CATEGORY))
            {
                // Set "India" as the default value
                gPIL_FARMER_MASTER.FARM_CATEGORY = "REGISTERED";
            }
            try
            {
                if (ModelState.IsValid)
                {


                    GPIL_FARMER_MASTER obj = (from C in db.GPIL_FARMER_MASTER where C.FARM_CODE == gPIL_FARMER_MASTER.FARM_CODE select C).Single();
                    obj.FARM_CODE = gPIL_FARMER_MASTER.FARM_CODE;
                    obj.FARM_CATEGORY = gPIL_FARMER_MASTER.FARM_CATEGORY;
                    obj.FARM_NAME = gPIL_FARMER_MASTER.FARM_NAME;

                    obj.FARM_FATHER_NAME = gPIL_FARMER_MASTER.FARM_FATHER_NAME;
                    obj.VILLAGE_CODE = gPIL_FARMER_MASTER.VILLAGE_CODE;
                    obj.SOIL_TYPE = gPIL_FARMER_MASTER.SOIL_TYPE;
                    obj.FARM_ADDRESS1 = gPIL_FARMER_MASTER.FARM_ADDRESS1;
                    obj.FARM_ADDRESS2 = gPIL_FARMER_MASTER.FARM_ADDRESS2;

                    obj.TEL_NO = gPIL_FARMER_MASTER.TEL_NO;
                    obj.MOBILE_NO = gPIL_FARMER_MASTER.MOBILE_NO;
                    obj.EMAIL_ID = gPIL_FARMER_MASTER.EMAIL_ID;
                    obj.STATUS = gPIL_FARMER_MASTER.STATUS;
                    obj.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.BANK_ACCOUNT_NO;
                    obj.BANK_NAME = gPIL_FARMER_MASTER.BANK_NAME;
                    obj.BRANCH_NAME = gPIL_FARMER_MASTER.BRANCH_NAME;
                    obj.IFSC_CODE = gPIL_FARMER_MASTER.IFSC_CODE;

                    obj.LAST_UPDATED_BY = Session["userID"].ToString();
                    obj.LAST_UPDATED_DATE = DateTime.Now;

                    
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    TempData["SuccessMessage"] = "Successfully Updated!!!";


                }

                return View(gPIL_FARMER_MASTER);
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




        private bool IsAadhaarExists(string aadhaarNumber)
        {
            using (GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities())
            {
                return db.GPIL_FARMER_MASTER.Any(f => f.ATTRIBUTE3 == aadhaarNumber);
            }
        }

        // GET: GPIL_FARMER_MASTER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);
            if (gPIL_FARMER_MASTER == null)
            {
                return HttpNotFound();
            }
            return View(gPIL_FARMER_MASTER);
        }

        // POST: GPIL_FARMER_MASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            

            GPIL_FARMER_MASTER gPIL_FARMER_MASTER = db.GPIL_FARMER_MASTER.Find(id);

            GPIL_FARMER_MASTER obj = (from C in db.GPIL_FARMER_MASTER where C.FARM_CODE == gPIL_FARMER_MASTER.FARM_CODE select C).Single();
            obj.FARM_CODE = gPIL_FARMER_MASTER.FARM_CODE;
            obj.FARM_CATEGORY = gPIL_FARMER_MASTER.FARM_CATEGORY;
            obj.FARM_NAME = gPIL_FARMER_MASTER.FARM_NAME;

            obj.FARM_FATHER_NAME = gPIL_FARMER_MASTER.FARM_FATHER_NAME;
            obj.VILLAGE_CODE = gPIL_FARMER_MASTER.VILLAGE_CODE;
            obj.SOIL_TYPE = gPIL_FARMER_MASTER.SOIL_TYPE;
            obj.FARM_ADDRESS1 = gPIL_FARMER_MASTER.FARM_ADDRESS1;
            obj.FARM_ADDRESS2 = gPIL_FARMER_MASTER.FARM_ADDRESS2;

            obj.TEL_NO = gPIL_FARMER_MASTER.TEL_NO;
            obj.MOBILE_NO = gPIL_FARMER_MASTER.MOBILE_NO;
            obj.EMAIL_ID = gPIL_FARMER_MASTER.EMAIL_ID;
            obj.STATUS = "N";
            obj.BANK_ACCOUNT_NO = gPIL_FARMER_MASTER.BANK_ACCOUNT_NO;
            obj.BANK_NAME = gPIL_FARMER_MASTER.BANK_NAME;
            obj.BRANCH_NAME = gPIL_FARMER_MASTER.BRANCH_NAME;
            obj.IFSC_CODE = gPIL_FARMER_MASTER.IFSC_CODE;
            obj.LAST_UPDATED_BY = Session["userID"].ToString();
            obj.LAST_UPDATED_DATE = DateTime.Now;
            db.Entry(gPIL_FARMER_MASTER).State = EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Successfully Deleted!!!";
            // return RedirectToAction("Index");
            return View(gPIL_FARMER_MASTER);
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
        //public JsonResult FarmerMasterComplete(ListFarmerMaster LFM)
        //{
        //    FarmerMasterdata(LFM);

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
        public JsonResult FarmerMasterComplete(ListFarmerMaster LFM)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LFM.FarmerMasters);
                string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string CROP = dtGridLst.Rows[s]["CROP"].ToString();
                    string VARIETY = dtGridLst.Rows[s]["VARIETY"].ToString();
                    string FARM_CODE = dtGridLst.Rows[s]["FARM_CODE"].ToString();
                    string FARM_CATEGORY = dtGridLst.Rows[s]["FARM_CATEGORY"].ToString();
                    string FARM_NAME = dtGridLst.Rows[s]["FARM_NAME"].ToString();
                    string FARM_FATHER_NAME = dtGridLst.Rows[s]["FARM_FATHER_NAME"].ToString();
                    string VILLAGE_CODE = dtGridLst.Rows[s]["VILLAGE_CODE"].ToString();
                    string SOIL_TYPE = dtGridLst.Rows[s]["SOIL_TYPE"].ToString();
                    string FARM_ADDRESS1 = dtGridLst.Rows[s]["FARM_ADDRESS1"].ToString();
                    string FARM_ADDRESS2 = dtGridLst.Rows[s]["FARM_ADDRESS2"].ToString();
                    string FARM_ADDRESS3 = dtGridLst.Rows[s]["FARM_ADDRESS3"].ToString();
                    string FARM_ADDRESS4 = dtGridLst.Rows[s]["FARM_ADDRESS4"].ToString();
                    string FARM_ADDRESS5 = dtGridLst.Rows[s]["FARM_ADDRESS5"].ToString();
                    string FARM_ADDRESS6 = dtGridLst.Rows[s]["FARM_ADDRESS6"].ToString();
                    string COUNTRY = dtGridLst.Rows[s]["COUNTRY"].ToString();
                    string PIN_CODE = dtGridLst.Rows[s]["PIN_CODE"].ToString();
                    string TEL_NO = dtGridLst.Rows[s]["TEL_NO"].ToString();

                    string MOBILE_NO = dtGridLst.Rows[s]["MOBILE_NO"].ToString();
                    string EMAIL_ID = dtGridLst.Rows[s]["EMAIL_ID"].ToString();
                    string BANK_ACCOUNT_NO = dtGridLst.Rows[s]["BANK_ACCOUNT_NO"].ToString();
                    string BANK_NAME = dtGridLst.Rows[s]["BANK_NAME"].ToString();
                    string BRANCH_NAME = dtGridLst.Rows[s]["BRANCH_NAME"].ToString();
                    string IFSC_CODE = dtGridLst.Rows[s]["IFSC_CODE"].ToString();
                    string STATUS = dtGridLst.Rows[s]["STATUS"].ToString();
                    string ATTRIBUTE3 = dtGridLst.Rows[s]["ATTRIBUTE3"].ToString();

                    string A1 = dtGridLst.Rows[s]["A1"].ToString();
                    string A2 = dtGridLst.Rows[s]["A2"].ToString();
                    string A3 = dtGridLst.Rows[s]["A3"].ToString();
                    string A4 = dtGridLst.Rows[s]["A4"].ToString();
                    string A5 = dtGridLst.Rows[s]["A5"].ToString();
                    DataTable dtclstr = new DataTable();
                    string query = "";

                    //double TotAuthorizedQty = 0;
                    //double AuthorizedQty = 0;
                    //AuthorizedQty = Convert.ToDouble(A1);
                    //TotAuthorizedQty = Convert.ToDouble(A2) + Convert.ToDouble(A3) + Convert.ToDouble(A4) + Convert.ToDouble(A5);


                   


                    if (FARM_CODE == "" )
                    {
                        data = "Error: Farmer Code Should Not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if(FARM_CATEGORY == "" || FARM_CATEGORY.Length >15 )
                    {
                        data = "Error: Farmer Category Should Not be Empty and less than 15 ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (FARM_NAME == "" || FARM_NAME.Length >50)
                    {
                        data = "Error: Farmer Name Should Not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (FARM_FATHER_NAME == "" || FARM_FATHER_NAME.Length>50)
                    {
                        data = "Error: Farmer Father Name Should Not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (VILLAGE_CODE == "" || VILLAGE_CODE.Length>10)
                    {
                        data = "Error: Village Code Should Not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (SOIL_TYPE == "" || SOIL_TYPE.Length > 5)
                    {
                        data = "Error: Soil Type Should Not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (FARM_ADDRESS1 == "" || FARM_ADDRESS1.Length >50)
                    {
                        data = "Error: Farmer Address1 Should Not be Empty and less then 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (FARM_ADDRESS2 == "" || FARM_ADDRESS1.Length > 50)
                    {
                        data = "Error: Farmer Address2 Should Not be Empty and less then 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }


                    else if (COUNTRY == "")
                    {
                        data = "Error: Country Should Not be Empty";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (BANK_ACCOUNT_NO == "" || BANK_ACCOUNT_NO.Length >25)
                    {
                        data = "Error: Farmer Account Number Should Not be Empty and less then 25";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (BANK_NAME == "" || BANK_NAME.Length >50)
                    {
                        data = "Error: Bank Name Should Not be Empty and less then 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else if (IFSC_CODE == "" || IFSC_CODE.Length >11)
                    {
                        data = "Error: IFSC Code Should Not be Empty and less then 11";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (ATTRIBUTE3 == "" || ATTRIBUTE3.Length > 50)
                    {
                        data = "Error: adhaar number Should Not be Empty and less then 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    else if (BRANCH_NAME == "" || BRANCH_NAME.Length >50)
                    {
                        data = "Error: Branch Name Should Not be Empty and less then 50";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }


                    //else if (A1 == "")
                    //{
                    //    data = "Error: Authorized Quantity Should Not be Empty";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;

                    //}
                    //else if (A2 == "")
                    //{
                    //    data = "Error: Bright Should Not be Empty";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;

                    //}
                    //else if (A3 == "")
                    //{
                    //    data = "Error: Medium Should Not be Empty";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;

                    //}
                    //else if (A4 == "")
                    //{
                    //    data = "Error: Low Should Not be Empty";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;

                    //}
                    //else if (A5 == "")
                    //{
                    //    data = "Error: Others Should Not be Empty";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;

                    //}
                    //else if (AuthorizedQty != TotAuthorizedQty)
                    //{
                    //    data = "Error: Authorised Quantity MisMatch. Please Enter Valid Authorised Quantity ";
                    //    json = JsonConvert.SerializeObject(data);
                    //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //    jsonResult.MaxJsonLength = int.MaxValue;
                    //    return jsonResult;

                    //}


                    query = " SELECT VILLAGE_CODE FROM GPIL_VILLAGE_MASTER(NOLOCK) WHERE VILLAGE_CODE='" + VILLAGE_CODE + "'";                  
                    dtclstr = MstrMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        data = "Error:  Please check the village code" + VILLAGE_CODE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }




                    query = " SELECT SOIL_TYPE FROM GPIL_SOIL_MASTER(NOLOCK) WHERE SOIL_TYPE='" + SOIL_TYPE + "'";              
                    dtclstr = MstrMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        data = "Error: Please check the soil type" + SOIL_TYPE;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }



                    query = "SELECT VARIETY FROM GPIL_VARIETY_SEASON_MASTER(NOLOCK) WHERE VARIETY='" + VARIETY + "'";
               
                    dtclstr = MstrMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        data = "Error: The given varierty not available" + VARIETY;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    query = "SELECT * FROM GPIL_FARMER_MASTER WHERE ATTRIBUTE3='" + ATTRIBUTE3 + "'";

                    dtclstr = MstrMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count > 0)
                    {
                        data = "Error: Adhaar number already exists " + ATTRIBUTE3;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }


                    query = "SELECT CROP FROM GPIL_crop_MASTER(NOLOCK) WHERE Crop='" + CROP + "'";

                    dtclstr = MstrMgt.GetQueryResult(query);

                    if (dtclstr.Rows.Count == 0)
                    {
                        data = "Error: The given crop not available" + CROP ;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }




                    //query = " SELECT SNO,CLUSTER_CODE,CLUSTER_NAME,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2,";
                    //query = query + " ATTRIBUTE3,ATTRIBUTE4 from GPIL_CLUSTER_MASTER where CLUSTER_CODE='" + CLUSTER_CODE + "' ";

                    query = " SELECT SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,";
                    query = query + " COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_FARMER_MASTER where FARM_CODE='" + FARM_CODE + "' ";
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
                        GPIL_FARMER_MASTER obj = null;
                        obj = new GPIL_FARMER_MASTER();
                      
                        strQry = "INSERT INTO [dbo].[GPIL_FARMER_MASTER] ([FARM_CODE],[FARM_CATEGORY],[FARM_NAME],[FARM_FATHER_NAME],[VILLAGE_CODE],[SOIL_TYPE],[FARM_ADDRESS1],[FARM_ADDRESS2],[MOBILE_NO],[EMAIL_ID],[BANK_ACCOUNT_NO],[BANK_NAME],[BRANCH_NAME],[IFSC_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[ATTRIBUTE3]) ";
                        strQry = strQry + "VALUES('" + FARM_CODE + "','" + FARM_CATEGORY + "','" + FARM_NAME + "','" + FARM_FATHER_NAME + "','" + VILLAGE_CODE + "','" + SOIL_TYPE + "','" + FARM_ADDRESS1 + "','" + FARM_ADDRESS2 + "','" + MOBILE_NO + "','" + EMAIL_ID + "','" + BANK_ACCOUNT_NO + "','" + BANK_NAME + "','" + BRANCH_NAME + "','" + IFSC_CODE + "','" + Session["userID"].ToString() + "',getdate(),'" + STATUS + "','" + ATTRIBUTE3 + "')";
                        lstQry.Add(strQry);



                        strQry = "INSERT INTO GPIL_FARMER_CROP_HISTORY(HIS_CODE, FARM_CODE, CROP, VARIETY, MOBILE_NO, EMAIL_ID, BANK_ACCOUNT_NO, BANK_NAME, ";
                        strQry = strQry + "BRANCH_NAME, IFSC_CODE, CREATED_BY, CREATED_DATE, STATUS, LOAN_AMOUNT, BALANCE_AMOUNT,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5)";
                        strQry = strQry + "VALUES('" + CROP + VARIETY + FARM_CODE + "','" + FARM_CODE + "','" + CROP + "','" + VARIETY + "','" + MOBILE_NO + "','" + EMAIL_ID + "',";
                        strQry = strQry + " '" + BANK_ACCOUNT_NO + "','" + BANK_NAME + "','" + BRANCH_NAME + "','" + IFSC_CODE + "','" + Session["userID"].ToString() + "',getdate(),'Y', '0', '0','" + A1 + "','" + A2 + "','" + A3 + "','" + A4 + "','" + A5 + "')";
                        lstQry.Add(strQry);

                        
                    }
                    else
                    {


                        strQry = " UPDATE GPIL_FARMER_MASTER SET FARM_NAME = '" + FARM_NAME + "', FARM_FATHER_NAME = '" + FARM_FATHER_NAME + "', VILLAGE_CODE = '" + VILLAGE_CODE + "',";
                        strQry = strQry + "SOIL_TYPE = '" + SOIL_TYPE + "', FARM_ADDRESS1 = '"+ FARM_ADDRESS1 + "', FARM_ADDRESS2 = '" + FARM_ADDRESS2 + "', FARM_ADDRESS3 = '" + FARM_ADDRESS3 + "', FARM_ADDRESS4 = '" + FARM_ADDRESS4 + "',";
                        strQry = strQry + " FARM_ADDRESS5 = '" + FARM_ADDRESS5 + "', FARM_ADDRESS6 = '" + FARM_ADDRESS6 + "', COUNTRY ='" + COUNTRY + "', PIN_CODE ='" + PIN_CODE + "', TEL_NO = '" + TEL_NO + "', MOBILE_NO = '" + MOBILE_NO + "',";
                        strQry = strQry + " EMAIL_ID ='" + EMAIL_ID + "', BANK_ACCOUNT_NO = '" + BANK_ACCOUNT_NO + "', BANK_NAME = '" + BANK_NAME + "', BRANCH_NAME = '" + BRANCH_NAME + "', IFSC_CODE = '" + IFSC_CODE + "',ATTRIBUTE3 = '" + ATTRIBUTE3 + "',";
                        strQry = strQry + "LAST_UPDATED_BY = '" + Session["userID"].ToString() + "', LAST_UPDATED_DATE = GETDATE(), STATUS = 'Y' WHERE FARM_CODE = '" + FARM_CODE + "'";
                        lstQry.Add(strQry);


                        strQry = "UPDATE GPIL_FARMER_CROP_HISTORY SET HIS_CODE = '"+CROP + VARIETY + FARM_CODE+ "', CROP = '" + CROP + "', VARIETY = '" + VARIETY + "', MOBILE_NO = '" + MOBILE_NO + "', EMAIL_ID = '" + EMAIL_ID + "',";
                        strQry = strQry + " BANK_ACCOUNT_NO = '" + BANK_ACCOUNT_NO + "', BANK_NAME = '" + BANK_NAME + "', BRANCH_NAME = '" + BRANCH_NAME + "', IFSC_CODE = '" + IFSC_CODE + "', LAST_UPDATED_BY = '" + Session["userID"].ToString() + "',";
                        strQry = strQry + "LAST_UPDATED_DATE = GETDATE(), STATUS = 'Y', LOAN_AMOUNT = '0', BALANCE_AMOUNT = '0', ATTRIBUTE1='"+ A1 + "', ATTRIBUTE2='" + A2 + "', ATTRIBUTE3='" + A3 + "', ATTRIBUTE4='" + A4 + "', ATTRIBUTE5='" + A5 + "',";
                        strQry = strQry + "WHERE FARM_CODE = '" + FARM_CODE + "' AND CROP = '" + CROP + "' AND VARIETY = '" + VARIETY + "'";
                        lstQry.Add(strQry);
                        //data = "Error: Farmer Code " + FARM_CODE + " is already exist So please check and import";
                        //json = JsonConvert.SerializeObject(data);
                        //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        //jsonResult.MaxJsonLength = int.MaxValue;
                        //return jsonResult;

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
                    return Json("Error: Please Check Your Excel and Upload!!!!" +  JsonRequestBehavior.AllowGet);
                }
                

            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
           

        }

    }
}
