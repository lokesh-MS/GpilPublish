using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using GPI;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class VerificationFactoryDispatchDataCheckController : Controller
    {
        // GET: VerificationFactoryDispatchDataCheck
        public ActionResult Index()
        {
            return View();
        }
        CommonManagement cMgt = new CommonManagement();
        DataTable dtt = new DataTable();
        [HttpGet]
        public ActionResult GetLPNumber(string fromDate, string toDate)
        {
            
            DateTime dtime = Convert.ToDateTime(fromDate);
            fromDate = dtime.ToString("dd-MM-yyyy");
            DateTime dtim = Convert.ToDateTime(toDate);
            toDate = dtim.ToString("dd-MM-yyyy");

            string strsql = "";
            if (fromDate != "" && toDate != "")
            {
                strsql = "SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR (NOLOCK) WHERE RECEIVER_ORGN_CODE IN ('M81','M82') AND SENDER_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
            }
            else
            {
                strsql = "SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR (NOLOCK) WHERE RECEIVER_ORGN_CODE IN ('M81','M82')";

            }
            dtt = cMgt.GetQueryResult(strsql);            
            string json = JsonConvert.SerializeObject(dtt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        string data = String.Empty;
        JsonResult jsonResult;
        string json = String.Empty;
        [HttpGet]
        public ActionResult GetFactoryDispatchDetails(string fromDate, string toDate, string lpNumber)
        {
            DataTable dt1 = new DataTable();
            try
            {

                if (fromDate.Trim().Length == 0)
                {

                    data = "Error: Please select the From Date";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the From Date');", true);
                    //return;
                }

                else if (toDate.Length == 0)
                {
                    data = "Error: Please select the To Date";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the To Date');", true);
                    //return;
                }


                else if (lpNumber == "")
                {
                    data = "Error: Please select the LP5 Number";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the LP5 Number');", true);
                    //return;
                }

                string strSqlQuery = "";

                if (fromDate != "" && toDate != "")
                {
                    if (lpNumber.Length != 0)
                    {
                        strSqlQuery = "SELECT LocCode,CaseBarCode,ToLocCode,PickListNo,LP5No,NIC,TRS,CL,convert(nvarchar(21),FumStartOn,105),convert(nvarchar(21),FumEndOn,105),convert(nvarchar(21),FumExpiredOn,105) FROM tCaseDetails (NOLOCK) WHERE CaseBarCode IN (SELECT GPIL_BALE_NUMBER FROM GPIL_SHIPMENT_DTLS (NOLOCK) WHERE SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR (NOLOCK) WHERE RECEIVER_ORGN_CODE IN ('M81','M82') AND SHIPMENT_NO ='" + lpNumber + "' AND SENDER_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)))";
                    }
                    else
                    {
                        strSqlQuery = "SELECT LocCode,CaseBarCode,ToLocCode,PickListNo,LP5No,NIC,TRS,CL,convert(nvarchar(21),FumStartOn,105),convert(nvarchar(21),FumEndOn,105),convert(nvarchar(21),FumExpiredOn,105) FROM tCaseDetails (NOLOCK) WHERE CaseBarCode IN (SELECT GPIL_BALE_NUMBER FROM GPIL_SHIPMENT_DTLS (NOLOCK) WHERE SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR (NOLOCK) WHERE RECEIVER_ORGN_CODE IN ('M81','M82') AND SENDER_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)))";

                    }

                }
                else
                {
                    if (lpNumber.Length != 0)
                    {
                        strSqlQuery = "SELECT LocCode,CaseBarCode,ToLocCode,PickListNo,LP5No,NIC,TRS,CL,convert(nvarchar(21),FumStartOn,105),convert(nvarchar(21),FumEndOn,105),convert(nvarchar(21),FumExpiredOn,105) FROM tCaseDetails (NOLOCK) WHERE CaseBarCode IN (SELECT GPIL_BALE_NUMBER FROM GPIL_SHIPMENT_DTLS (NOLOCK) WHERE SHIPMENT_NO ='" + lpNumber + "')";
                    }
                    else
                    {
                        strSqlQuery = "SELECT LocCode,CaseBarCode,ToLocCode,PickListNo,LP5No,NIC,TRS,CL,convert(nvarchar(21),FumStartOn,105),convert(nvarchar(21),FumEndOn,105),convert(nvarchar(21),FumExpiredOn,105) FROM tCaseDetails (NOLOCK) WHERE CaseBarCode IN (SELECT GPIL_BALE_NUMBER FROM GPIL_SHIPMENT_DTLS (NOLOCK) WHERE SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR (NOLOCK) WHERE RECEIVER_ORGN_CODE IN ('M81','M82') ))";

                    }
                }

                strSqlQuery = strSqlQuery + " order by LocCode,ToLocCode";

                

                dt1 = cMgt.GetQueryResult(strSqlQuery);

                if(dt1.Rows.Count>0)
                {
                    string json = JsonConvert.SerializeObject(dt1);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    data = "Error: NO DATA FOUND";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }

                


            }
            catch (Exception ex)
            {

            }
            return Json(dt1);
        }
    }
}