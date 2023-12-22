using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class ProfilePrintAllocationUpdateController : Controller
    {
        // GET: ProfilePrintAllocationUpdate
        public ActionResult Index()
        {
            return View();
        }
        DataTable dt = new DataTable();
        CommonManagement cMgt = new CommonManagement();
        string lblMessage = string.Empty;
        string data = String.Empty;
        JsonResult jsonResult;
        string json = String.Empty;

        //Crop, RunNo, LocCode, Grade, CasePrint
        [HttpPost]
        public ActionResult UpdatePrintAllocation(string crop, string runNo, string locCode, string grade, string casePrint)
        {
            string json = String.Empty;

            bool b;
            string sql = "UPDATE tPrintAllocation SET CasesToPrint='" + casePrint + "' where CropYearCode='" + crop + "' AND PMRunNo='" + runNo + "' AND LocCode='" + locCode + "' AND GradeCode='" + grade + "'";
            b = cMgt.UpdateUsingExecuteNonQuery(sql);
            if(b == false)
            {
                lblMessage = "Success: Updated SucessFully!!";
            }
            else
            {
                lblMessage = "Error: Not Updated!";
            }
            
            if (lblMessage.Length > 0)
            {
                data = lblMessage;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                data = "Success";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
        }
    }
}