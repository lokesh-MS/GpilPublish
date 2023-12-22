using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class BaleTrackWithUpdationController : Controller
    {
        // GET: BaleTrackWithUpdation
        public ActionResult BaleTrackWithUpdationIndex()
        {
            return View();
        }


        string lblMessage = string.Empty;
        [HttpGet]
        public ActionResult getGradeAndSubInv()
        {
            VerificationManagement ldMgt = new VerificationManagement();
            DataSet ds = new DataSet();
            DataTable dsTemp = new DataTable();
            string sSql = string.Empty; 
            ds = ldMgt.GetdsQueryResult("SELECT ITEM_CODE AS FROM_GRADE FROM GPIL_ITEM_MASTER (NOLOCK);");
             
            dsTemp = ldMgt.GetQueryResult("SELECT ITEM_CODE AS TO_GRADE FROM GPIL_ITEM_MASTER (NOLOCK);");
            if (dsTemp.Rows.Count > 0)
            {
                ds.Tables.Add(dsTemp);
            } 
            dsTemp = ldMgt.GetQueryResult("SELECT SUB_INV_CODE AS FROM_SUBINV FROM GPIL_SUBINVENTORY (NOLOCK);");
            if (dsTemp.Rows.Count > 0)
            {
                ds.Tables.Add(dsTemp);
            } 
            dsTemp = ldMgt.GetQueryResult("SELECT SUB_INV_CODE AS TO_SUBINV FROM GPIL_SUBINVENTORY (NOLOCK);");
            if (dsTemp.Rows.Count > 0)
            {
                ds.Tables.Add(dsTemp);
            } 
            string json = JsonConvert.SerializeObject(ds);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public static DataSet ds = new DataSet();
        public static string errfile;

        [HttpGet]
        public ActionResult BaleTrackingDetails(string strCaseNumber)



        {

            //lblMessage.Text = string.Empty;
            string sBaleNo = string.Empty;
            sBaleNo = strCaseNumber.Trim();
            errfile = string.Empty;
            string sSql = string.Empty;
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();

            string json = string.Empty; 

            //string query = "";   
            ds.Clear();

            sSql = "SELECT D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN H.PURCHASE_TYPE='TAP PURCHASE' THEN 'TAP PURCHASE' ELSE CASE WHEN H.PURCHASE_TYPE='SUNDRY PURCHASE' THEN 'FARMER PURCHASE' END END PROCESS,H.HEADER_ID AS REPORT_REF,H.HEADER_ID AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.BUYER_GRADE AS FROM_GRADE,D.BUYER_GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV, D.NET_WT AS MARKED_WT,D.NET_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            ds = ldMgt.GetdsQueryResult(sSql);

            sSql = "SELECT D.GPIL_BALE_NUMBER,(H.RECEV_ORGN_CODE + ' (' + H.SUPP_CODE + ')') AS ORGN_CODE,H.ATTRIBUTE1  AS PROCESS,H.HEADER_ID AS REPORT_REF,H.HEADER_ID AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS, D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.NET_WEIGHT AS MARKED_WT,D.NET_WEIGHT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_SUPP_PURCHS_DTLS(NOLOCK) D,GPIL_SUPP_PURCHS_HDR(NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND D.GPIL_BALE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT  D.GPIL_BALE_NUMBER,(H.SENDER_ORGN_CODE + ' - ' + H.RECEIVER_ORGN_CODE) AS ORGN_CODE, (CASE WHEN REDIRECT_STATUS='Y' THEN 'RE-DIRECT SHIPMENT' ELSE 'SHIPMENT' END) AS PROCESS,H.SHIPMENT_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.DISPATCH_WEIGHT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND D.GPIL_BALE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT  D.GPIL_BALE_NUMBER,(H.SENDER_ORGN_CODE + ' - ' + H.RECEIVER_ORGN_CODE) AS ORGN_CODE, 'SALES' AS PROCESS,H.SHIPMENT_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.DISPATCH_WEIGHT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_SO_RESERVATION_DTLS(NOLOCK) D,GPIL_SO_RESERVATION_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND D.GPIL_BALE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT  D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,H.RECIPE_CODE  AS PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.ISSUED_GRADE AS FROM_GRADE,D.CLASSIFICATION_GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.WEIGHT_BEFORE_CLASSIFY AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_CLASSIFICATION_DTLS(NOLOCK) D,GPIL_CLASSIFICATION_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT CASE WHEN D.OLD_BALE_NUMBER='" + sBaleNo + "' THEN D.OLD_BALE_NUMBER ELSE CASE WHEN D.NEW_BALE_NUMBER='" + sBaleNo + "' THEN D.NEW_BALE_NUMBER END END GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN D.OLD_BALE_NUMBER='" + sBaleNo + "' THEN 'CROP-TRANSFER ISSUE' ELSE CASE WHEN D.NEW_BALE_NUMBER='" + sBaleNo + "' THEN 'CROP-TRANSFER OUTTURN' END END PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.OLD_GRADE AS FROM_GRADE,D.NEW_GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.MARKED_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_CROP_TRANS_DTLS(NOLOCK) D,GPIL_CROP_TRANS_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND (D.OLD_BALE_NUMBER='" + sBaleNo + "' or D.NEW_BALE_NUMBER='" + sBaleNo + "') ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT  D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN D.BALE_TYPE='IPB' THEN 'GRADING-ISSUE' ELSE CASE WHEN D.BALE_TYPE='OPB' THEN 'GRADING-OUTTURN' END END PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.ASCERTAIN_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_GRADING_DTLS(NOLOCK) D,GPIL_GRADING_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN D.BALE_TYPE='IPB' THEN 'THRESHING-ISSUE' ELSE CASE WHEN D.BALE_TYPE='OPB' THEN 'THRESHING-BY-PRODUCT' END END PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.ASCERTAIN_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_THRESH_RECON_DTLS_1(NOLOCK) D,GPIL_THRESH_RECON_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }
            sSql = "SELECT D.CASE_NUMBER AS GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,'THRESHING-PRODUCT'  AS PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS ,H.FLAG AS ERP_STATUS,D.PACKED_GRADE AS FROM_GRADE,D.PACKED_GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.NET_WT AS MARKED_WT,D.NET_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_THRESH_RECON_DTLS_2(NOLOCK) D,GPIL_THRESH_RECON_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.CASE_NUMBER='" + sBaleNo + "' ORDER BY D.CREATED_DATE";
            dsTemp = ldMgt.GetdsQueryResult(sSql);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsTemp);
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].DefaultView.Sort = "CREATED_DATE";
                json = JsonConvert.SerializeObject(ds.Tables[0]);
            }
           
            var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           

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

        [HttpPost]
        public JsonResult UpdateBaleTrack(ListBale_Track gPIL_Bale_Track_DTLS)
        {
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            VerificationManagement ldMgt = new VerificationManagement();
            DataTable dtclstr = ToDataTable1(gPIL_Bale_Track_DTLS.BaleTracks);
            try
            {
                

                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {

                    

                    string GPIL_BALE_NUMBER = dtclstr.Rows[i]["GPIL_BALE_NUMBER"].ToString();
                    string ORGN_CODE = dtclstr.Rows[i]["ORGN_CODE"].ToString();
                    string PROCESS = dtclstr.Rows[i]["PROCESS"].ToString();
                    string REPORT_REF = dtclstr.Rows[i]["REPORT_REF"].ToString();
                    string USER_REF = dtclstr.Rows[i]["USER_REF"].ToString();
                    string SQL_STATUS = dtclstr.Rows[i]["SQL_STATUS"].ToString();
                    string ERP_STATUS = dtclstr.Rows[i]["ERP_STATUS"].ToString();
                    string FROM_GRADE = dtclstr.Rows[i]["FROM_GRADE"].ToString();
                    string TO_GRADE = dtclstr.Rows[i]["TO_GRADE"].ToString();
                    string FROM_SUBINV = dtclstr.Rows[i]["FROM_SUBINV"].ToString();
                    string TO_SUBINV = dtclstr.Rows[i]["TO_SUBINV"].ToString();

                    string MARKED_WT = dtclstr.Rows[i]["MARKED_WT"].ToString();
                    string ASCERTAIN_WT = dtclstr.Rows[i]["ASCERTAIN_WT"].ToString();
                    //        public string GPIL_BALE_NUMBER { get; set; }        public string ORGN_CODE { get; set; }        public string PROCESS { get; set; }
                    //public string REPORT_REF { get; set; }        public string USER_REF { get; set; }        public string SQL_STATUS { get; set; }
                    //public string ERP_STATUS { get; set; }        public string FROM_GRADE { get; set; }        public string TO_GRADE { get; set; }
                    //public string FROM_SUBINV { get; set; }        public string TO_SUBINV { get; set; }        public Nullable<double> MARKED_WT { get; set; }
                    //public Nullable<double> ASCERTAIN_WT { get; set; }        public System.DateTime CREATED_DATE { get; set; }


                    //GPIL_BALE_NUMBER = gPIL_Bale_Track_DTLS.GPIL_BALE_NUMBER;
                    // ORGN_CODE = (gPIL_Bale_Track_DTLS.ORGN_CODE == null) ? "" : gPIL_Bale_Track_DTLS.ORGN_CODE;
                    //string orgcd = ((string)Session["orgnCode"] == null) ? "" : (string)Session["orgnCode"];
                    //string PROCESS = (gPIL_Bale_Track_DTLS.PROCESS == null) ? "" : gPIL_Bale_Track_DTLS.PROCESS;
                    //string REPORT_REF = (gPIL_Bale_Track_DTLS.REPORT_REF == null) ? "" : gPIL_Bale_Track_DTLS.REPORT_REF;
                    //string USER_REF = (gPIL_Bale_Track_DTLS.USER_REF == null) ? "" : gPIL_Bale_Track_DTLS.USER_REF;

                    //string SQL_STATUS = (gPIL_Bale_Track_DTLS.SQL_STATUS == null) ? "" : gPIL_Bale_Track_DTLS.SQL_STATUS;

                    //string ERP_STATUS = (gPIL_Bale_Track_DTLS.ERP_STATUS == null) ? "" : gPIL_Bale_Track_DTLS.ERP_STATUS;
                    //string FROM_GRADE = (gPIL_Bale_Track_DTLS.FROM_GRADE == null) ? "" : gPIL_Bale_Track_DTLS.FROM_GRADE;
                    //string TO_GRADE = (gPIL_Bale_Track_DTLS.TO_GRADE == null) ? "" : gPIL_Bale_Track_DTLS.TO_GRADE;
                    //string FROM_SUBINV = (gPIL_Bale_Track_DTLS.FROM_SUBINV == null) ? "" : gPIL_Bale_Track_DTLS.FROM_SUBINV;
                    //string TO_SUBINV = (gPIL_Bale_Track_DTLS.TO_SUBINV == null) ? "" : gPIL_Bale_Track_DTLS.TO_SUBINV;

                    //double MARKED_WT = Convert.ToDouble(gPIL_Bale_Track_DTLS.MARKED_WT);
                    //double ASCERTAIN_WT = Convert.ToDouble(gPIL_Bale_Track_DTLS.ASCERTAIN_WT);






                    if (ERP_STATUS != "Y")
                    {

                        string strQuery = "";

                        if (PROCESS == "TAP PURCHASE" || PROCESS == "FARMER PURCHASE")
                        {
                            strQuery = "UPDATE GPIL_TAP_FARM_PURCHS_DTLS SET BUYER_GRADE='" + FROM_GRADE + "',SUBINVENTORY_CODE='" + FROM_SUBINV + "' WHERE HEADER_ID='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "RE-DIRECT SHIPMENT" || PROCESS == "SHIPMENT")
                        {
                            strQuery = "UPDATE GPIL_SHIPMENT_DTLS SET GRADE='" + FROM_GRADE + "',FROM_SUBINVENTORY_CODE='" + FROM_SUBINV + "',TO_SUBINVENTORY_CODE='" + TO_SUBINV + "' WHERE SHIPMENT_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "CLASSIFICATION" || PROCESS == "RE-CLASSIFICATION")
                        {
                            strQuery = "UPDATE GPIL_CLASSIFICATION_DTLS SET ISSUED_GRADE='" + FROM_GRADE + "',CLASSIFICATION_GRADE='" + TO_GRADE + "',FROM_SUBINVENTORY_CODE='" + FROM_SUBINV + "',TO_SUBINVENTORY_CODE='" + TO_SUBINV + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "GRADING-ISSUE" || PROCESS == "GRADING-OUTTURN")
                        {
                            strQuery = "UPDATE GPIL_GRADING_DTLS SET GRADE='" + FROM_GRADE + "',SUBINVENTORY_CODE='" + FROM_SUBINV + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "THRESHING-ISSUE" || PROCESS == "THRESHING-BY-PRODUCT")
                        {
                            strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_1 SET GRADE='" + FROM_GRADE + "',SUBINVENTORY_CODE='" + FROM_SUBINV + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "THRESHING-PRODUCT")
                        {
                            strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_2 SET PACKED_GRADE='" + FROM_GRADE + "',SUBINVENTORY_CODE='" + FROM_SUBINV + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "SALES")
                        {
                            strQuery = "UPDATE GPIL_SO_RESERVATION_DTLS SET GRADE='" + FROM_GRADE + "',FROM_SUBINVENTORY_CODE='" + FROM_SUBINV + "',TO_SUBINVENTORY_CODE='" + TO_SUBINV + "' WHERE SHIPMENT_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else if (PROCESS == "SUPPLIER RECEIPT" || PROCESS == "SUPPLIER PURCHASE")
                        {
                            strQuery = "UPDATE GPIL_SUPP_PURCHS_DTLS SET GRADE='" + FROM_GRADE + "',SUBINVENTORY_CODE='" + FROM_SUBINV + "' WHERE HEADER_ID='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }

                        //03-10-2016 Modifiaction Saba
                        else if (PROCESS == "CROP-TRANSFER ISSUE")
                        {
                            strQuery = "UPDATE GPIL_CROP_TRANS_DTLS SET OLD_GRADE='" + FROM_GRADE + "',NEW_GRADE='" + TO_GRADE + "',FROM_SUBINVENTORY_CODE='" + FROM_SUBINV + "',TO_SUBINVENTORY_CODE='" + TO_SUBINV + "' WHERE BATCH_NO='" + REPORT_REF + "' AND OLD_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        }
                        else
                        {

                            data = "Error: Not yet implemented for " + PROCESS;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                            //lblMessage = "Error: Not yet implemented for " + PROCESS;
                            //return;
                        }
                        ldMgt.UpdateUsingExecuteNonQuery(strQuery);



                        BaleTrackingDetails(GPIL_BALE_NUMBER); //ref
                        //lblMessage = "Success: Record Updated Successfully";

                        data = "Success: Record Updated Successfully";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        //jsonResult.MaxJsonLength = int.MaxValue;
                        //return jsonResult;
                    }
                    else
                    {
                        //lblMessage = "Error: We can't update, because ERP is picked already ";
                        data = "Error: We can't update, because ERP is picked already ";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        //jsonResult.MaxJsonLength = int.MaxValue;
                        //return jsonResult;
                    }

                    //string CREATED_DATE = gPIL_Bale_Track_DTLS.CREATED_DATE;
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
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


           

        }

        [HttpPost]
        public ActionResult DeleteFarmerPurchase(string GPIL_BALE_NUMBER)
        {
            //string sttrsqrt;
            //VerificationManagement ldMgt = new VerificationManagement();
            //sttrsqrt = "delete FROM GPIL_TAP_FARM_PURCHS_DTLS where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            //ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            //sttrsqrt = "delete FROM gpil_stock where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            //ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            return new EmptyResult();
        }
    }


    public class Bale_Track
    {


        public int SNO { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string ORGN_CODE { get; set; }
        public string PROCESS { get; set; }
        public string REPORT_REF { get; set; }
        public string USER_REF { get; set; }
        public string SQL_STATUS { get; set; }
        public string ERP_STATUS { get; set; }
        public string FROM_GRADE { get; set; }
        public string TO_GRADE { get; set; }
        public string FROM_SUBINV { get; set; }
        public string TO_SUBINV { get; set; }
        public Nullable<double> MARKED_WT { get; set; }
        public Nullable<double> ASCERTAIN_WT { get; set; }

        public System.DateTime CREATED_DATE { get; set; }

    }
    public class ListBale_Track
    {
        public List<Bale_Track> BaleTracks { get; set; }
    }
}