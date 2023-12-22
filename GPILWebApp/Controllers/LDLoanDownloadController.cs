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
    public class LDLoanDownloadController : Controller
    {
        // GET: LDLoanDownload
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetLdLoanDetailsDetails(string strfromDate, string strtoDate)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                string lblMessage = string.Empty;
                CommonManagement cMgt = new CommonManagement();
                DataTable dtclstr = new DataTable();

               

                string sqlRecon = " SELECT SUBSTRING(CROP,3,2) CROP,(CASE  WHEN SUBSTRING(FARMERCODE,1,3)='HDB' THEN '15'  WHEN SUBSTRING(FARMERCODE,1,3)='VKB' THEN '11' ELSE '19' END) VARIETY ,Farmercode FARMER_CODE,SUM(FARMERVALUE) LOAN_AMOUNT  FROM GPIL_Leaf_Subsidy_Header WHERE [Date] >=Convert(varchar,'" + strfromDate + " 00:00:00',103) AND [Date] <=Convert(varchar,'" + strtoDate + " 23:59:59',103) GROUP BY CROP,Farmercode";

               

                dtclstr = cMgt.GetQueryResult(sqlRecon);
                json = JsonConvert.SerializeObject(dtclstr);
                return Json(json, JsonRequestBehavior.AllowGet);
                

            }
            catch (Exception ex)
            {
                data = ex.ToString();
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            return View();
        }
    }
}