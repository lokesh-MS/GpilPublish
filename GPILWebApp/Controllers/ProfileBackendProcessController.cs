using GPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class ProfileBackendProcessController : Controller
    {
        // GET: ProfileBackendProcess
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

        public ActionResult BackendProcessExecute(string shipmentNumber, string ReportType)
        {
            string retVal = "";
            
            if (ReportType == "WMSDISPATCH_LOCAL")
            {
                string SPName = "WMSDISPATCH_LOCAL";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[2];
                pram[0] = (new SqlParameter("@SHIPMENTNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@ERR", SqlDbType.NVarChar, 500));               
                pram[0].Value = shipmentNumber;
                pram[1].Direction = ParameterDirection.Output;                
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                retVal = cMgt.SP_ExecuteNonQuery(parameters, SPName);
                lblMessage = "MESSAGE:" + retVal;
            }
            else if (ReportType == "WMSChemicalDetailsUpdation")
            {
                string SPName = "WMSChemicalDetailsUpdation";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[2];
                pram[0] = (new SqlParameter("@SHIPMENTNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@ERR", SqlDbType.NVarChar, 500));
                pram[0].Value = shipmentNumber;
                pram[1].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                retVal = cMgt.SP_ExecuteNonQuery(parameters, SPName);
                lblMessage = "MESSAGE:" + retVal;
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