using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using System.Data.Entity;
using System.Data;

namespace GPILWebApp.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        GREEN_LEAF_TRACEABILITYEntities dbObj = new GREEN_LEAF_TRACEABILITYEntities();
        public ActionResult Index()
        {
            var res = dbObj.GPIL_COMPANY_MASTER.OrderBy(s => s.COMPANY_NAME).ToList();
            ViewBag.CompanyDetails = res;
            return View();
           
        }
        [HttpPost]
        public ActionResult CompanyCreation(GPIL_COMPANY_MASTER com)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var companyDetails = dbObj.GPIL_COMPANY_MASTER.Where(x => x.COMPANY_CODE == com.COMPANY_CODE).FirstOrDefault();
                    GPIL_COMPANY_MASTER obj = new GPIL_COMPANY_MASTER();
                    obj.COMPANY_CODE = com.COMPANY_CODE;
                    obj.COMPANY_NAME = com.COMPANY_NAME;
                    obj.STATUS = com.STATUS;
                    obj.SUPPLIER_FLAG = com.SUPPLIER_FLAG;
                    obj.COMP_SHORT_NAME = com.COMP_SHORT_NAME;
                    obj.COMP_GROUP_CODE = com.COMP_GROUP_CODE;
                    obj.SUPPLIED_TO = "NONE";
                    obj.CREATED_BY = Session["UserID"].ToString();
                    //obj.CREATED_DATE = DateTime.Now;
                    if (companyDetails == null)
                    {
                      
                        dbObj.GPIL_COMPANY_MASTER.Add(obj);
                        dbObj.SaveChanges();
                    }
                    else
                    {
                        dbObj.Entry(obj).State= EntityState.Modified;
                        dbObj.SaveChanges();
                    }
                   
                    ModelState.Clear();
                    return View("Index");

                }
                return View("Index");
            }
            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", ex);
                return View("Index");
            }

        }
    }
}