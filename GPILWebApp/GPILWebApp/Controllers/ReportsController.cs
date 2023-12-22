using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [HttpGet]
        // GET: Reports/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}