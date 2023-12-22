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
    public class VerificationFreightUpdateController : Controller
    {
        // GET: VerificationFreightUpdate
        public ActionResult Index()
        {
            return View();
        }

        DataTable dt = new DataTable();
        CommonManagement cMgt = new CommonManagement();
        public ActionResult GetFreightUpdationDetails()
        {
            
            DataTable dt = new DataTable();
            string sqlQuery = "";
            sqlQuery = "SELECT Row_Number() over(order by A.SHIPMENT_NO desc) as SNO, A.SHIPMENT_NO, WAYBILL, SENDER_ORGN_CODE, RECEIVER_ORGN_CODE, SENDER_NO, SENDER_DATE, SENT_BY, SENDER_TRUCK_NO, RECEIVER_TRUCK_NO, RC_NO, DRIVER_NAME, DRIVING_LICENCE_NO, TRANSPORT_NAME, REDIRECT_STATUS, REDIRECT_SHIPMENT_NO, FRIEGHT_CHARGES, TOT_NO_OF_BALES, TOT_WEIGHT, convert(numeric(10, 2), SUM(B.MARKED_WT)) QTY, convert(numeric(10, 2), FRIEGHT_CHARGES * (SUM(B.MARKED_WT)))  AS FreightValue, UOM  FROM GPIL_SHIPMENT_HDR A, GPIL_SHIPMENT_DTLS B  WHERE  A.FLAG IS NULL AND  A.SHIPMENT_NO = B.SHIPMENT_NO GROUP BY A.SNO, A.SHIPMENT_NO, WAYBILL, SENDER_ORGN_CODE, RECEIVER_ORGN_CODE, SENDER_NO, SENDER_DATE, SENT_BY, SENDER_TRUCK_NO, RECEIVER_TRUCK_NO, RC_NO, DRIVER_NAME, DRIVING_LICENCE_NO, TRANSPORT_NAME, REDIRECT_STATUS, REDIRECT_SHIPMENT_NO, FRIEGHT_CHARGES, TOT_NO_OF_BALES, TOT_WEIGHT, UOM   order by  a.SHIPMENT_NO desc";
            dt = cMgt.GetQueryResult(sqlQuery);
            string json = JsonConvert.SerializeObject(dt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        JsonResult jsonResult;
        string data = String.Empty, json = String.Empty;
        string lblMessage = string.Empty;
        
        [HttpPost]      
        public ActionResult FreightUpdation(string shipmentNumber, string freighrCharge)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string sqlQuery = "";
            sqlQuery = "UPDATE GPIL_SHIPMENT_HDR SET FRIEGHT_CHARGES='" + freighrCharge + "' WHERE SHIPMENT_NO='" + shipmentNumber + "' and FLAG IS NULL ";
            bool b = cMgt.UpdateUsingExecuteNonQuery(sqlQuery);
            if(b)
            {

                lblMessage = "Success: Updated!! Sucessfully";
            }
            else
            {

                lblMessage = "Error: Not Updated!!";
               
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