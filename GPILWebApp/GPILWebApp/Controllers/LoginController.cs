using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.Models;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        GREEN_LEAF_TRACEABILITYEntities dbObj = new GREEN_LEAF_TRACEABILITYEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(GPIL_USER_MASTER user)//string userName, string passWord
        {
            string result = "success";
            try {
                // && x.ATTRIBUTE2 == "W"
                if (ModelState.IsValid)
                {
                    var userDetails = dbObj.GPIL_USER_MASTER.Where(x => x.USER_ID == user.USER_ID && x.PASSWORD == user.PASSWORD).FirstOrDefault();
                    if (userDetails == null)
                    {
                        // Guser.LoginErrorMessage = "Wrong User ID or password";
                        //return View("Login");
                    }
                    else
                    {
                        var userRights = dbObj.GPIL_USER_PERMISSION.Where(y => y.Employee_ID == user.USER_ID).ToList();
                        if (userRights != null)
                        {
                            Session["userName"] = userDetails.USER_NAME;
                            Session["userID"] = userDetails.EMP_CODE;
                            Session["UserForm"] = userRights;
                            Session["LocationCode"] = userDetails.ATTRIBUTE3;
                            Session["PrintLocationCode"] = userDetails.ATTRIBUTE3;
                            Session["LocationType"] = userDetails.ATTRIBUTE4;



                            // clsSettings. = varStrWMSLocation; //declaring user location code
                            //clsSettings. = varStrWMSLocation;
                            //clsSettings.strLocName = "";
                            // clsSettings. = varStrWMSLocationType;


                            result = "success";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                        }
                    }

                    //return RedirectToAction("Index", "Home");
                }


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex) {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
             //return View();
        }


        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;

        [HttpPost]
        public ActionResult LoginUser(string userName, string passWord)
        {
            string result = "success";
            try
            {
                // && x.ATTRIBUTE2 == "W"
                //if (ModelState.IsValid)
                //{
                    var userDetails = dbObj.GPIL_USER_MASTER.Where(x => x.USER_ID == userName && x.PASSWORD == passWord).FirstOrDefault();
                    if (userDetails == null)
                    {
                    data = "Error: Please Check User ID and Password!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                    else
                    {
                        var userRights = dbObj.GPIL_USER_PERMISSION.Where(y => y.Employee_ID == userName).ToList();
                        if (userRights != null)
                        {
                            Session["userName"] = userDetails.USER_NAME;
                            Session["userID"] = userDetails.EMP_CODE;
                            Session["UserForm"] = userRights;
                            Session["LocationCode"] = userDetails.ATTRIBUTE3;
                            Session["PrintLocationCode"] = userDetails.ATTRIBUTE3;
                            Session["LocationType"] = userDetails.ATTRIBUTE4;




                            data = "Success: ";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;


                            // clsSettings. = varStrWMSLocation; //declaring user location code
                            //clsSettings. = varStrWMSLocation;
                            //clsSettings.strLocName = "";
                            // clsSettings. = varStrWMSLocationType;


                            //result = "success";
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                            
                        }
                    }

                    //return RedirectToAction("Index", "Home");
                //}


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //return View();
        }



        public ActionResult AfterLogin()
        {
            if (Session["UserID"] != null)
            {
                 return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

    }
}