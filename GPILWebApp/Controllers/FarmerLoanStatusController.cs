using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GPILWebApp.Models;
using System.Web.Mvc;
using System.Data;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class FarmerLoanStatusController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;

        public FarmerLoanStatusController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }

        // GET: FarmerLoanStatus
        public ActionResult Index()
        {
            //ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();

        }

        public JsonResult Loanstatus(string crop, string variety)
        {
            DataSet ds = new DataSet();
            LDManagement ldMgt = new LDManagement();
            try
            {
                ds = ldMgt.GetFarmerLoanStatus(crop, variety);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


            }
            catch (Exception ex)
            { }
            return Json(ds);
        }
    }
}