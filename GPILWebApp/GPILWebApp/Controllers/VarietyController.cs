using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;

namespace GPILWebApp.Controllers
{
    public class VarietyController : Controller
    {

        GREEN_LEAF_TRACEABILITYEntities dbObj = new GREEN_LEAF_TRACEABILITYEntities();
        // GET: Variety
        public ActionResult Index()
        {
            var res = dbObj.GPIL_VARIETY_MASTER.ToList();
            ViewBag.VarietyDetails = res;
            return View();
        }

        [HttpPost]
        public ActionResult VarietyCreation(GPIL_VARIETY_MASTER VM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GPIL_VARIETY_MASTER Variety = new GPIL_VARIETY_MASTER();
                    Variety.VARIETY = VM.VARIETY;
                    Variety.VARIETY_NAME = VM.VARIETY_NAME;
                    Variety.VARIETY_TYPE = VM.VARIETY_TYPE;
                    Variety.VARIETY_DESC = VM.VARIETY_DESC;
                    Variety.STATUS = VM.STATUS;
                    Variety.CREATED_BY = Session["userID"].ToString();
                    Variety.CREATED_DATE = DateTime.Now;
                    dbObj.GPIL_VARIETY_MASTER.Add(Variety);
                    dbObj.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            catch (Exception ex)
            {

            }
            return View();
        }

    }
}