using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp.Models;
using System.Web.Mvc;
//using GPILWebApp.ViewModel;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data.Entity.SqlServer;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using GPILWebApp.ViewModel;

namespace GPILWebApp.Controllers
{
    public class GradingController : Controller
    {
        // GET: Grading
        private GREEN_LEAF_TRACEABILITYEntities _data;

        public GradingController()
        {
            _data = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _data.Dispose();
        }


        public ActionResult GradingTempDetails()
        {
            return View();
        }

        [HttpGet]
       
        public JsonResult GradingTempDetailsIssue(string BatchNumber, string ReportType)
        {
            DataSet ds = new DataSet();           
           
            PPDManagement ppdMgt = new PPDManagement();
            try
            {
                ds = ppdMgt.GetGradingTempDetails(BatchNumber, ReportType);
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