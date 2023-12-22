using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;

namespace GPILWebApp.Controllers
{
    public class VerificationFreightChargeReportDCNumberController : Controller
    {
        // GET: VerificationFreightChargeReportDCNumber
        public ActionResult Index()
        {
            return View();
        }
        CommonManagement cMgt = new CommonManagement();

        [HttpGet]
        public ActionResult GetFreightChargeWithDCNo(string fromDate, string toDate)
        {
            
            JsonResult jsonResult;
            string data = String.Empty, json = String.Empty;
            string lblMessage = string.Empty;

            DataTable dtclstr = new DataTable();
            DateTime dt1, dt2;
            string sSql;
            string sFrom, sTo;
            dt1 = Convert.ToDateTime(fromDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            dt2 = Convert.ToDateTime(toDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            sFrom = String.Format("{0:yyyyMMdd}", dt1);
            sTo = String.Format("{0:yyyyMMdd}", dt2);

            sSql = " SELECT ROW_NUMBER() oveR(order by HDR.SHIPMENT_NO desc)as sno, HDR.SHIPMENT_NO,dc. DC_No,HDR.SENDER_DATE,SENDER_ORGN_CODE ,RECEIVER_ORGN_CODE,HDR.SENDER_TRUCK_NO, ";
            sSql = sSql + " COUNT(DLT.GPIL_BALE_NUMBER)AS NoOfBales,Convert(numeric(18,2),SUM(DLT.MARKED_WT))Qty,Convert(numeric(18,2),HDR.FRIEGHT_CHARGES) AS FREIGHT_CHARGES, Convert(numeric(18,2),(SUM(DLT.MARKED_WT)*HDR.FRIEGHT_CHARGES))AS Value  ";
            sSql = sSql + " FROM GPIL_SHIPMENT_HDR HDR LEFT JOIN GPIL_SHIPMENT_DTLS DLT ON HDR.SHIPMENT_NO =DLT.SHIPMENT_NO  ";
            sSql = sSql + " left outeR join GPIL_GST_INVOICE_NO dc on HDR.SHIPMENT_NO=dc.SHIPMENT_NO ";
            sSql = sSql + " where convert(varchar(15),HDR.SENDER_DATE,112)>='" + sFrom.Trim() + "' and convert(varchar(15),HDR.SENDER_DATE,112)<='" + sTo.Trim() + "'";
            sSql = sSql + " GROUP BY HDR.SHIPMENT_NO,HDR.SENDER_DATE,SENDER_ORGN_CODE,FRIEGHT_CHARGES,RECEIVER_ORGN_CODE,HDR.SENDER_TRUCK_NO,dc.DC_No ";

            dtclstr = cMgt.GetQueryResult(sSql);

            if(dtclstr.Rows.Count == 0)
            {
                lblMessage = "Error: NO DATA FOUND";
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

            //json = JsonConvert.SerializeObject(dtclstr);
            //return Json(json, JsonRequestBehavior.AllowGet);


        }
    }
}