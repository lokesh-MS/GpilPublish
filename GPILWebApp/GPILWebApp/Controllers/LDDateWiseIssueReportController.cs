using GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class LDDateWiseIssueReportController : Controller
    {
        // GET: LDDateWiseIssueReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetLdLoanDetails(string entryDate)
        {

            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "select a.farmercode,a.crop,a.farmername,(select itemname from GPIL_Leaf_Item_Master where ItemCode=b.ItemCode) Item,b.[date] IssueDate ,b.Attribute1 Qty,b.PoNo,b.Reciptno RecieptNo from GPIL_Leaf_Subsidy_Header a,GPIL_Leaf_Subsidy_Details b where a.ID=b.ID and b.entryDate >= CONVERT(datetime,'" + entryDate + " 00:00:00',103) and b.entrydate < CONVERT(datetime,'" + entryDate + " 23:59:59',103)";
            dtclstr = cMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);
            //string data = String.Empty, json = String.Empty;
            //JsonResult jsonResult;


            //string lblMessage = string.Empty;
            //CommonManagement cMgt = new CommonManagement();
            //DataTable dtclstr = new DataTable();

            //try
            //{
            //    if (entryDate == "")
            //    {

            //        lblMessage = "Error: Please select Date...";

            //    }


            //    string sql = "select a.farmercode,a.crop,a.farmername,(select itemname from GPIL_Leaf_Item_Master where ItemCode=b.ItemCode) Item,b.[date] IssueDate ,b.Attribute1 Qty,b.PoNo,b.Reciptno RecieptNo from GPIL_Leaf_Subsidy_Header a,GPIL_Leaf_Subsidy_Details b where a.ID=b.ID and b.entryDate >= CONVERT(datetime,'" + entryDate + " 00:00:00',103) and b.entrydate < CONVERT(datetime,'" + entryDate + " 23:59:59',103)";
            //    dtclstr = cMgt.GetQueryResult(sql);
            //    if (dtclstr.Rows.Count > 0)
            //    {
            //        json = JsonConvert.SerializeObject(dtclstr);
            //        return Json(json, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        lblMessage = "Error: No data available...";

            //    }
            //    if (lblMessage.Length > 0)
            //    {
            //        data = lblMessage;
            //        json = JsonConvert.SerializeObject(data);
            //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //        jsonResult.MaxJsonLength = int.MaxValue;
            //        return jsonResult;
            //    }
            //    else
            //    {
            //        data = "Success";
            //        json = JsonConvert.SerializeObject(data);
            //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //        jsonResult.MaxJsonLength = int.MaxValue;
            //        return jsonResult;

            //    }


            //}
            //catch (Exception ex)
            //{
            //    lblMessage = ex.Message;

            //}
            //return View();
        }
    }
}