using GPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class LDMonthlyReconsilationController : Controller
    {
        // GET: LDMonthlyReconsilation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCropYear()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "SELECT Distinct[Crop_year] FROM [dbo].[GPIL_CROP_MASTER]";
                ds1 = cMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }

        [HttpGet]
        public ActionResult GetLDMonthReconDetails(string strMonth, string strYear)
        {
            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            string lblMessage = string.Empty;


            if (strMonth == string.Empty || strYear == string.Empty)
            {
                //lblMessage = "Error: Please select Month and Year...";
                data = "Error: Please select Month and Year...";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            int days = DateTime.DaysInMonth(Convert.ToInt32(strYear.ToString()), Convert.ToInt32(strMonth.ToString()));

            string sql = " Select a.itemCode,(Select e.ItemName from GPil_Leaf_item_master e where e.Itemcode=a.itemCode) ItemName,  (ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.DatePo < Convert(dateTime,'01-" + strMonth + "-" + strYear + " 00:00:00',103)),0)-iSnull((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.ItemCode=a.ItemCode  and d.Date < Convert(dateTime,'01-" + strMonth + "-" + strYear + " 00:00:00',103)),0)) OpStock,ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and  c.DatePo >=Convert(dateTime,'01-" + strMonth + "-" + strYear + " 00:00:00',103) AND c.DatePo <=Convert(DAteTime,'" + days.ToString() + "-" + strMonth + "-" + strYear + " 23:59:59',103)),0) PoQty,((ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.DatePo < Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103)),0)-iSnull((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.ItemCode=a.ItemCode  and d.Date < Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103)),0))+ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and  c.DatePo >=Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103) AND c.DatePo <=Convert(DAteTime,'" + days.ToString() + "-" + strMonth + "-" + strYear + " 23:59:59',103)),0) ) poplusop,ISNUll((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.ItemCode=a.ItemCode  and d.Date >=Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103) AND d.Date <=Convert(DAteTime,'" + days.ToString() + "-" + strMonth + "-" + strYear + "  23:59:59',103)),0) SubsidyQty,(((ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.DatePo < Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103)),0)-iSnull((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.ItemCode=a.ItemCode  and d.Date < Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103)),0))+ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and  c.DatePo >=Convert(dateTime,'01-" + strMonth + "-" + strYear + " 00:00:00',103) AND c.DatePo <=Convert(DAteTime,'" + days.ToString() + "-" + strMonth + "-" + strYear + "  23:59:59',103)),0) ) -ISNUll((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.ItemCode=a.ItemCode  and d.Date >=Convert(dateTime,'01-" + strMonth + "-" + strYear + "  00:00:00',103) AND d.Date <=Convert(DAteTime,'" + days.ToString() + "-" + strMonth + "-" + strYear + " 23:59:59',103)),0)) QtyLeft  from GPIL_Leaf_PO_Details a Group by ItemCode";
            dtclstr = cMgt.GetQueryResult(sql);           
            json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

    }
}