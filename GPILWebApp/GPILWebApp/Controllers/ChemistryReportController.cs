using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class ChemistryReportController : Controller
    {
        // GET: ChemistryReport
        public ActionResult Index()
        {
            return View("ChemistryReportsIndex");
        }
    }
}