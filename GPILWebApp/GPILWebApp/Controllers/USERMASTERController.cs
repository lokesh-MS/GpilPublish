using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace GPILWebApp.Controllers
{
    public class USERMASTERController : Controller
    {



        string connectionString = @"Data Source = 123.136.195.87; Initial Catalog = DBGPIL; user Id = team; Password=admin@123;MultipleActiveResultSets=True"; 

        // GET: USERMASTER
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            //using (SqlConnection
            return View();
        }

        // GET: USERMASTER/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: USERMASTER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERMASTER/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: USERMASTER/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: USERMASTER/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: USERMASTER/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: USERMASTER/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
