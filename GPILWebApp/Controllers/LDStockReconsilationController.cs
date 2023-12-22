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
    public class LDStockReconsilationController : Controller
    {
        // GET: LDStockReconsilation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetLDStockReconDetails(string strfromDate, string strtoDate)
        {
            CommonManagement cMgt = new CommonManagement();
            DataTable dtclstr = new DataTable();
            string data = String.Empty, json = String.Empty;
            //JsonResult jsonResult;
            string lblMessage = string.Empty;

            if (strfromDate == "" || strtoDate == "")
            {
                lblMessage = "Error : SELECT FROM AND TO DATES...";
               
            }
           

            string sql = "select a.PoNo PoNo, a.itemCode ItemCode,(Select e.ItemName from GPil_Leaf_item_master e where e.Itemcode=a.itemCode) ItemName,(Select b.Supplier from GPIL_Leaf_PO_Header b where b.PoNo=a.PoNo ) Supplier,ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.PoNo=a.PoNo  and c.DatePo >=Convert(dateTime,'" + strfromDate + "',103) AND c.DatePo <=Convert(DAteTime,'" + strtoDate + "',103)),0) PoQty,ISNUll((Select Sum(c.Total)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.PoNo=a.PoNo  and c.DatePo >=Convert(dateTime,'" + strfromDate + "',103) AND c.DatePo <=Convert(DAteTime,'" + strtoDate + "',103) ),0) PoValue,ISNUll((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and  d.PoNo=a.PoNo and  d.Date >=Convert(dateTime,'" + strfromDate + "',103) AND d.Date <=Convert(DAteTime,'" + strtoDate + "',103)),0) SubsidyQty ,ISnull((Select Sum(CAST(d.Attribute2 As FLoat)) from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.PoNo=a.PoNo  and d.Date >=Convert(dateTime,'" + strfromDate + "',103) AND d.Date <=Convert(DAteTime,'" + strtoDate + "',103)),0) SubsidyValue,(ISNULL((select Sum(c.Qty)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.PoNo=a.PoNo  and c.DatePo >=Convert(dateTime,'" + strfromDate + "',103) AND c.DatePo <=Convert(DAteTime,'" + strtoDate + "',103) ),0)-iSnull((select Sum(CAST(d.ATTRIBUTE1 AS INT))  from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.PoNo=a.PoNo  and d.Date >=Convert(dateTime,'" + strfromDate + "',103) AND d.Date <=Convert(DAteTime,'" + strtoDate + "',103)),0)) ExStock,(ISNULL((Select Sum(c.Total)  from GPIL_Leaf_PO_Details c where c.ItemCode=a.ItemCode and c.PoNo=a.PoNo  and c.DatePo >=Convert(dateTime,'" + strfromDate + "',103) AND c.DatePo <=Convert(DAteTime,'" + strtoDate + "',103) ),0)-ISNULL((Select Sum(CAST(d.Attribute2 As FLoat)) from GPIL_Leaf_Subsidy_Details d where d.itemcode=a.ItemCode and d.PoNo=a.PoNo  and d.Date >=Convert(dateTime,'" + strfromDate + "',103) AND d.Date <=Convert(DAteTime,'" + strtoDate + "',103)),0)) ExAmount from  GPIL_Leaf_PO_Details a   Group by ItemCode ,PoNo";
            dtclstr = cMgt.GetQueryResult(sql);
            json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }
    }
}