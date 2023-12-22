using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class AuthorisedQtyMasterController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;


        public AuthorisedQtyMasterController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        // GET: AuthorisedQtyMaster
        public ActionResult AuthorisedQtyMasterIndex()
        {
            ViewBag.GPIL_CROP_MASTERs = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP_YEAR = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            return View();
        }
    }
}