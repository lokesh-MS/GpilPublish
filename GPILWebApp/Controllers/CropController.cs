using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;

namespace GPILWebApp.Controllers
{
    public class CropController : Controller
    {
        GREEN_LEAF_TRACEABILITYEntities dbObj = new GREEN_LEAF_TRACEABILITYEntities();
        // GET: Crop
        public ActionResult Index()
        {
            dbObj.Configuration.ProxyCreationEnabled = false;
            var res = dbObj.GPIL_CROP_MASTER.Take(100).ToList();

            return View(res);
        }
    }
}