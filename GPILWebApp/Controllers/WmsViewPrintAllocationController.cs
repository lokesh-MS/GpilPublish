using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class WmsViewPrintAllocationController : Controller
    {
        // GET: WmsViewPrintAllocation
        private GREEN_LEAF_TRACEABILITYEntities _context;
        public WmsViewPrintAllocationController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}