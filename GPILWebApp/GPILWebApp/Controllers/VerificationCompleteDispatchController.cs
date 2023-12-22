using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;
using Newtonsoft.Json;
using System.ComponentModel;
using GPILWebApp.ViewModel.Verificationn;

namespace GPILWebApp.Controllers
{
    public class VerificationCompleteDispatchController : Controller
    {
        // GET: VerificationCompleteDispatch
        public ActionResult Index()
        {
            return View();
        }

        CommonManagement cMgt = new CommonManagement();
        JsonResult jsonResult;
        [HttpGet]
        public ActionResult GetCompleteDispatchDetails()
        {

           
            string data = String.Empty, json = String.Empty;
            string lblMessage = string.Empty;

            DataTable dtclstr = new DataTable();
            string sqlQuery = "";

            // and CompleteDate is null and convert(varchar,Created_Date,112)>='20200817 
            // and CompleteDate is null and convert(varchar,Created_Date,112)>='20200817'
            sqlQuery = "Select ROW_NUMBER() over(order by sno asc)as sno,SHIPMENT_NO ,SENDER_ORGN_CODE ,SENDER_TRUCK_NO ,SENDER_DATE,RECEIVED_DATE,FLAG  from  GPIL_SHIPMENT_HDR   where (FLAG ='Y' or FLAG='I')";

            dtclstr = cMgt.GetQueryResult(sqlQuery);

            if (dtclstr.Rows.Count == 0)
            {
                lblMessage = "Error: NO DATA FOUND";
            }

            if (lblMessage.Length >= 0 )
            {
                //data = lblMessage;
                json = JsonConvert.SerializeObject(dtclstr);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {

                json = JsonConvert.SerializeObject(dtclstr);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //data = "Success";
                //json = JsonConvert.SerializeObject(data);
                //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;

            }
            //json = JsonConvert.SerializeObject(ds1);
            //return Json(json, JsonRequestBehavior.AllowGet);




        }



        public static DataTable ToDataTable1<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        bool b;
        [HttpPost]
        public JsonResult CompleteDispatchComplete(ListCompleteDispatch LCD)
        {
            
            int z = 0;
            String strsql = string.Empty;
            String query = string.Empty;
            DataTable dtclstr = ToDataTable1(LCD.CompleteDispatchs);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {
                  

                        string strShipmentNo, strSenderOrgnCode, strSenderTruckNumber, strSenderDate, strFlag;

                        strShipmentNo = dtclstr.Rows[i]["SHIPMENT_NO"].ToString();
                        strSenderOrgnCode = dtclstr.Rows[i]["SENDER_ORGN_CODE"].ToString();

                        strSenderTruckNumber = dtclstr.Rows[i]["SENDER_TRUCK_NO"].ToString();
                        strSenderDate = dtclstr.Rows[i]["SENDER_DATE"].ToString();
                        strFlag = dtclstr.Rows[i]["FLAG"].ToString();


                        
                        
                  
                }
                string strSQLQuery = "Update GPIL_SHIPMENT_HDR set CompleteDate=getdate(),nFlag='YY'  where (FLAG ='Y' or FLAG='I') and CompleteDate is null and convert(varchar,Created_Date,112)>='20200817'";
                b = cMgt.UpdateUsingExecuteNonQuery(strSQLQuery);
                if (b == true)
                {
                    //tran.Rollback();
                    lblMessage = "Error: Error while Updating";



                }
                else
                {

                    //tran.Commit();
                    lblMessage = "Success: Sucessfully Updated";


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
            catch (Exception ex)
            {
                lblMessage = ex.ToString();

            }
            return null;
        }


    }
}