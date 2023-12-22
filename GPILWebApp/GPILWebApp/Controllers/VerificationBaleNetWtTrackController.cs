using GPI;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class VerificationBaleNetWtTrackController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities _context;
        public VerificationBaleNetWtTrackController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        // GET: VerificationBaleNetWtTrack
        public ActionResult Index()
        {
            ViewBag.GPIL_CROP_MASTER = (from s in _context.GPIL_CROP_MASTER select new { s.CROP_YEAR }).Distinct();
            return View();
        }

        string lblMessage = string.Empty;
        string data = String.Empty;
        JsonResult jsonResult;
        string json = String.Empty;

        [HttpGet]
        // GET: LD/PendingBales/a/a
        public ActionResult BaleTrackNTWeightSubInvDetails(string crop, string ReportType)
        {
            CommonManagement cMgt = new CommonManagement();
          
            DataTable dt = new DataTable();
            DataTable dtClr = new DataTable();
            string sSql = string.Empty;
            DataSet dsTemp = new DataSet();
            string sBaleNo = string.Empty;
            string json = String.Empty;

            if (crop.Trim() != string.Empty)
            {
                if (ReportType == "Marked_Weight_Cross_Check")
                {
                    sSql = "SELECT SD.SHIPMENT_NO AS REPORT_REF,(HD.SENDER_ORGN_CODE + ' - ' + HD.RECEIVER_ORGN_CODE) AS ORGN_CODE,(CASE WHEN HD.REDIRECT_STATUS='Y' THEN 'RE-DIRECT SHIPMENT' ELSE 'SHIPMENT' END) AS PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.MARKED_WT SHIPMENT_WT, ST.MARKED_WT AS STOCK_WT FROM GPIL_SHIPMENT_DTLS SD ,GPIL_STOCK ST ,GPIL_SHIPMENT_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.SHIPMENT_NO = HD.SHIPMENT_NO   AND HD.FLAG IS NULL AND SD.GPIL_BALE_NUMBER = ST.GPIL_BALE_NUMBER AND (SD.MARKED_WT!=ST.MARKED_WT)--GROUP BY SD.SHIPMENT_NO,HD.REDIRECT_STATUS,SD.GPIL_BALE_NUMBER , SD.MARKED_WT , ST.MARKED_WT";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }

                    sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,(HD.RECIPE_CODE) AS PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.MARKED_WT SHIPMENT_WT, ST.MARKED_WT AS STOCK_WT FROM GPIL_CLASSIFICATION_DTLS SD ,GPIL_STOCK ST ,GPIL_CLASSIFICATION_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND HD.FLAG IS NULL AND SD.GPIL_BALE_NUMBER = ST.GPIL_BALE_NUMBER AND (SD.MARKED_WT!=ST.MARKED_WT)";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }

                    sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,CASE WHEN SD.BALE_TYPE='IPB' THEN 'GRADING-ISSUE' ELSE CASE WHEN SD.BALE_TYPE='OPB' THEN 'GRADING-OUTTURN' END END PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.MARKED_WT SHIPMENT_WT, ST.MARKED_WT AS STOCK_WT FROM GPIL_GRADING_DTLS SD ,GPIL_STOCK ST ,GPIL_GRADING_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO  AND HD.FLAG IS NULL AND SD.GPIL_BALE_NUMBER = ST.GPIL_BALE_NUMBER AND (SD.MARKED_WT!=ST.MARKED_WT) ORDER BY SD.CREATED_DATE DESC";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }
                    sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,CASE WHEN SD.BALE_TYPE='IPB' THEN 'THRESHING-ISSUE' ELSE CASE WHEN SD.BALE_TYPE='OPB' THEN 'THRESHING-BY-PRODUCT' END END PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.MARKED_WT SHIPMENT_WT, ST.MARKED_WT AS STOCK_WT FROM GPIL_THRESH_RECON_DTLS_1 SD ,GPIL_STOCK ST ,GPIL_THRESH_RECON_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND HD.FLAG IS NULL AND SD.GPIL_BALE_NUMBER = ST.GPIL_BALE_NUMBER AND (SD.MARKED_WT!=ST.MARKED_WT) ORDER BY SD.CREATED_DATE DESC";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }
                    sSql = "SELECT  SD.CASE_NUMBER AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,'THRESHING-PRODUCT'  AS PROCESS,HD.CREATED_DATE, SD.CASE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.NET_WT SHIPMENT_WT, ST.MARKED_WT AS STOCK_WT FROM GPIL_THRESH_RECON_DTLS_2 SD ,GPIL_STOCK ST ,GPIL_THRESH_RECON_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND HD.FLAG IS NULL AND SD.CASE_NUMBER = ST.GPIL_BALE_NUMBER AND (SD.NET_WT!=ST.MARKED_WT) ORDER BY SD.CREATED_DATE DESC ";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        dt.DefaultView.Sort = "CREATED_DATE";
                        json = JsonConvert.SerializeObject(dt);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else
                    {
                        lblMessage = "Error: No Records found";
                        data = lblMessage;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
                else if (ReportType == "SubInventory_Cross_Check")
                {
                    sSql = "SELECT SD.SHIPMENT_NO AS REPORT_REF,(HD.SENDER_ORGN_CODE + ' - ' + HD.RECEIVER_ORGN_CODE) AS ORGN_CODE,(CASE WHEN HD.REDIRECT_STATUS='Y' THEN 'RE-DIRECT SHIPMENT' ELSE 'SHIPMENT' END) AS PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,SD.Grade AS ERP_STATUS ,SD.To_subInventory_Code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_SHIPMENT_DTLS SD ,GPIL_ITEM_MASTER ST ,GPIL_SHIPMENT_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.SHIPMENT_NO = HD.SHIPMENT_NO  AND HD.FLAG IS NULL AND SD.GRADE = ST.ITEM_CODE and HD.ATTRIBUTE2 !='TAP' AND (SD.To_subInventory_Code!=ST.ATTRIBUTE2)";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }

                    sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,(HD.RECIPE_CODE) AS PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.To_subInventory_Code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_CLASSIFICATION_DTLS SD ,GPIL_ITEM_MASTER ST ,GPIL_CLASSIFICATION_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO AND (SUBSTRING(HD.BATCH_NO ,9,2)  in ('51'))  AND HD.FLAG IS NULL AND SD.To_subInventory_Code = ST.ITEM_CODE AND  (SD.To_subInventory_Code!=ST.ATTRIBUTE2)";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }
                    sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,CASE WHEN SD.BALE_TYPE='IPB' THEN 'GRADING-ISSUE' ELSE CASE WHEN SD.BALE_TYPE='OPB' THEN 'GRADING-OUTTURN' END END PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.subinventory_code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_GRADING_DTLS SD ,GPIL_ITEM_MASTER ST ,GPIL_GRADING_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO  AND  SD.GRADE = ST.ITEM_CODE AND  (SUBSTRING(HD.BATCH_NO ,9,2) Not in ('08','14'))  and HD.FLAG IS NULL AND  (SD.subInventory_Code!=ST.ATTRIBUTE2) ORDER BY SD.CREATED_DATE DESC";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }
                    sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,CASE WHEN SD.BALE_TYPE='IPB' THEN 'THRESHING-ISSUE' ELSE CASE WHEN SD.BALE_TYPE='OPB' THEN 'THRESHING-BY-PRODUCT' END END PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.subinventory_code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_THRESH_RECON_DTLS_1 SD ,GPIL_ITEM_MASTER ST ,GPIL_THRESH_RECON_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND SD.GRADE = ST.ITEM_CODE AND  HD.FLAG IS NULL AND  (SD.subInventory_Code!=ST.ATTRIBUTE2) ORDER BY SD.CREATED_DATE DESC";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }
                    sSql = "SELECT  SD.CASE_NUMBER AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,'THRESHING-PRODUCT'  AS PROCESS,HD.CREATED_DATE, SD.CASE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.subinventory_code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_THRESH_RECON_DTLS_2 SD ,GPIL_ITEM_MASTER ST ,GPIL_THRESH_RECON_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND HD.FLAG IS NULL AND SD.Packed_Grade = ST.Item_code AND (SD.subInventory_Code!=ST.ATTRIBUTE2) ORDER BY SD.CREATED_DATE DESC ";
                    dtClr = cMgt.GetQueryResult(sSql);
                    if (dtClr.Rows.Count > 0)
                    {
                        dt.Merge(dtClr);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        dt.DefaultView.Sort = "CREATED_DATE";
                        json = JsonConvert.SerializeObject(dt);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        
                    }
                    else
                    {
                        lblMessage = "Error: No Records found";
                        data = lblMessage;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //GridViewSample.Visible = false;
                    }

                }
               
                    lblMessage = "Error: Please select Any one option";
                    data = lblMessage;
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                
            }

            lblMessage = "Error: Please Select Crop Year";
            data = lblMessage;
            json = JsonConvert.SerializeObject(data);
            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           
        }
        

        public static DataTable ToDataTable<T>(IList<T> data)
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

       // public JsonResult UpdateBaleTrack(ListBaleNetWtTrackDetails BNT, string isMarkedWeight)
        [HttpPost]
        public JsonResult UpdateBaleTrack(ListBaleNetWtTrackDetails BNT, string isMarkedWeight)
        {
            
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            VerificationManagement ldMgt = new VerificationManagement();
            DataTable dtclstr = ToDataTable(BNT.BaleNetWtTracks);
            try
            {

                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {
                    string ORGN_CODE = dtclstr.Rows[i]["ORGN_CODE"].ToString();
                    string PROCESS = dtclstr.Rows[i]["PROCESS"].ToString();
                    string CREATED_DATE = dtclstr.Rows[i]["CREATED_DATE"].ToString();
                    string REPORT_REF = dtclstr.Rows[i]["REPORT_REF"].ToString();
                    string GPIL_BALE_NUMBER = dtclstr.Rows[i]["GPIL_BALE_NUMBER"].ToString();
                    string ERP_STATUS = dtclstr.Rows[i]["ERP_STATUS"].ToString();
                    string SHIPMENT_WT = dtclstr.Rows[i]["SHIPMENT_WT"].ToString();
                    string STOCK_WT = dtclstr.Rows[i]["STOCK_WT"].ToString();
                   // string isMarkedWeight;



                    if (ERP_STATUS != "Y")
                    {

                        string strQuery = "";

                        if(isMarkedWeight == "Marked_Weight_Cross_Check")
                        {
                            if (PROCESS == "TAP PURCHASE" || PROCESS == "FARMER PURCHASE")
                            {
                                strQuery = "UPDATE GPIL_TAP_FARM_PURCHS_DTLS SET NET_WT='" + SHIPMENT_WT + "' WHERE HEADER_ID='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";

                            }
                            else if (PROCESS == "RE-DIRECT SHIPMENT" || PROCESS == "SHIPMENT")
                            {
                                strQuery = "UPDATE GPIL_SHIPMENT_DTLS SET MARKED_WT='" + SHIPMENT_WT + "' WHERE SHIPMENT_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "CLASSIFICATION" || PROCESS == "RE-CLASSIFICATION")
                            {
                                strQuery = "UPDATE GPIL_CLASSIFICATION_DTLS SET MARKED_WT='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "GRADING-ISSUE" || PROCESS == "GRADING-OUTTURN")
                            {
                                strQuery = "UPDATE GPIL_GRADING_DTLS SET MARKED_WT='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "THRESHING-ISSUE" || PROCESS == "THRESHING-BY-PRODUCT")
                            {
                                strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_1 SET MARKED_WT='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "THRESHING-PRODUCT")
                            {
                                strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_2 SET NET_WT='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "SALES")
                            {
                                strQuery = "UPDATE GPIL_SO_RESERVATION_DTLS SET MARKED_WT='" + SHIPMENT_WT + "' WHERE SHIPMENT_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "SUPPLIER RECEIPT" || PROCESS == "SUPPLIER PURCHASE")
                            {
                                strQuery = "UPDATE GPIL_SUPP_PURCHS_DTLS SET NET_WEIGHT='" + SHIPMENT_WT + "' WHERE HEADER_ID='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }

                            //03-10-2016 Modifiaction Saba
                            else if (PROCESS == "CROP-TRANSFER ISSUE")
                            {
                                strQuery = "UPDATE GPIL_CROP_TRANS_DTLS SET MARKED_WT='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND OLD_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else
                            {
                                lblMessage = " Error :Not yet implemented for " + PROCESS;
                                data = lblMessage;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                                // return;
                            }
                        }
                        else
                        {
                            if (PROCESS == "TAP PURCHASE" || PROCESS == "FARMER PURCHASE")
                            {
                                strQuery = "UPDATE GPIL_TAP_FARM_PURCHS_DTLS SET subInventory_Code='" + SHIPMENT_WT + "' WHERE HEADER_ID='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";

                            }
                            else if (PROCESS == "RE-DIRECT SHIPMENT" || PROCESS == "SHIPMENT")
                            {
                                strQuery = "UPDATE GPIL_SHIPMENT_DTLS SET To_subInventory_Code='" + SHIPMENT_WT + "' WHERE SHIPMENT_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "CLASSIFICATION" || PROCESS == "RE-CLASSIFICATION")
                            {
                                strQuery = "UPDATE GPIL_CLASSIFICATION_DTLS SET To_subInventory_Code='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "GRADING-ISSUE" || PROCESS == "GRADING-OUTTURN")
                            {
                                strQuery = "UPDATE GPIL_GRADING_DTLS SET subInventory_Code='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "THRESHING-ISSUE" || PROCESS == "THRESHING-BY-PRODUCT")
                            {
                                strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_1 SET subInventory_Code='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "THRESHING-PRODUCT")
                            {
                                strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_2 SET subInventory_Code='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "SALES")
                            {
                                strQuery = "UPDATE GPIL_SO_RESERVATION_DTLS SET subInventory_Code='" + SHIPMENT_WT + "' WHERE SHIPMENT_NO='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else if (PROCESS == "SUPPLIER RECEIPT" || PROCESS == "SUPPLIER PURCHASE")
                            {
                                strQuery = "UPDATE GPIL_SUPP_PURCHS_DTLS SET subInventory_Code='" + SHIPMENT_WT + "' WHERE HEADER_ID='" + REPORT_REF + "' AND GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }

                            //03-10-2016 Modifiaction Saba
                            else if (PROCESS == "CROP-TRANSFER ISSUE")
                            {
                                strQuery = "UPDATE GPIL_CROP_TRANS_DTLS SET To_subInventory_Code='" + SHIPMENT_WT + "' WHERE BATCH_NO='" + REPORT_REF + "' AND OLD_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            }
                            else
                            {
                                lblMessage = "Error :Not yet implemented for " + PROCESS;
                                data = lblMessage;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                                //return;
                            }
                        }

                        // bool B = GPIWEB.DataServer.Instance.ExecuteNonQuery(strQuery);
                        ldMgt.UpdateUsingExecuteNonQuery(strQuery);

                       // GridViewSample.EditIndex = -1;

                        //if (rdbsubInventoryCode.Checked == true)
                        //{
                        //    SubInventoryDetails();
                        //}
                        //else
                        //{
                        //    BaleTrackDetails();
                        //}
                        lblMessage = "Success :Record Updated Successfully";
                        data = lblMessage;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else
                    {
                        lblMessage = "Error :We can't update, because ERP is picked already";
                        data = lblMessage;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    
                }
                
                //return null;
                
            }
            catch (Exception e)

            {


            }
            

             return null;
        }

        //[HttpPost]
        //public JsonResult MarkedWeight(string rdbMarkedWt)
        //{
        //   // int i = 0;
        //    if(rdbMarkedWt == "Marked_Weight_Cross_Check")

        //    {
        //        lblMessage = "Error: NO ERROR";
                
        //       // return true;
        //    }
        //    else
        //    {
               
        //        //return false;
        //    }

        //    data = lblMessage;
        //    json = JsonConvert.SerializeObject(data);
        //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //   // return null;
        //}


            //[HttpGet]
            //public ActionResult BaleTrackSubInvDetails(string crop, string ReportType)
            //{
            //    CommonManagement cMgt = new CommonManagement();
            //    string lblMessage = string.Empty;
            //    DataTable dt = new DataTable();
            //    DataTable dtClr = new DataTable();
            //    string sSql = string.Empty;
            //    DataSet dsTemp = new DataSet();
            //    string sBaleNo = string.Empty;
            //    string json = String.Empty;


            //    if (ReportType == "SubInventory_Cross_Check")
            //    {
            //        sSql = "SELECT SD.SHIPMENT_NO AS REPORT_REF,(HD.SENDER_ORGN_CODE + ' - ' + HD.RECEIVER_ORGN_CODE) AS ORGN_CODE,(CASE WHEN HD.REDIRECT_STATUS='Y' THEN 'RE-DIRECT SHIPMENT' ELSE 'SHIPMENT' END) AS PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,SD.Grade AS ERP_STATUS ,SD.To_subInventory_Code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_SHIPMENT_DTLS SD ,GPIL_ITEM_MASTER ST ,GPIL_SHIPMENT_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.SHIPMENT_NO = HD.SHIPMENT_NO  AND HD.FLAG IS NULL AND SD.GRADE = ST.ITEM_CODE and HD.ATTRIBUTE2 !='TAP' AND (SD.To_subInventory_Code!=ST.ATTRIBUTE2)";
            //        dtClr = cMgt.GetQueryResult(sSql);
            //        if (dtClr.Rows.Count > 0)
            //        {
            //            dt.Merge(dtClr);
            //        }

            //        sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,(HD.RECIPE_CODE) AS PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.To_subInventory_Code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_CLASSIFICATION_DTLS SD ,GPIL_ITEM_MASTER ST ,GPIL_CLASSIFICATION_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO AND (SUBSTRING(HD.BATCH_NO ,9,2)  in ('51'))  AND HD.FLAG IS NULL AND SD.To_subInventory_Code = ST.ITEM_CODE AND  (SD.To_subInventory_Code!=ST.ATTRIBUTE2)";
            //        dtClr = cMgt.GetQueryResult(sSql);
            //        if (dtClr.Rows.Count > 0)
            //        {
            //            dt.Merge(dtClr);
            //        }
            //        sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,CASE WHEN SD.BALE_TYPE='IPB' THEN 'GRADING-ISSUE' ELSE CASE WHEN SD.BALE_TYPE='OPB' THEN 'GRADING-OUTTURN' END END PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.subinventory_code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_GRADING_DTLS SD ,GPIL_ITEM_MASTER ST ,GPIL_GRADING_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO  AND  SD.GRADE = ST.ITEM_CODE AND  (SUBSTRING(HD.BATCH_NO ,9,2) Not in ('08','14'))  and HD.FLAG IS NULL AND  (SD.subInventory_Code!=ST.ATTRIBUTE2) ORDER BY SD.CREATED_DATE DESC";
            //        dtClr = cMgt.GetQueryResult(sSql);
            //        if (dtClr.Rows.Count > 0)
            //        {
            //            dt.Merge(dtClr);
            //        }
            //        sSql = "SELECT SD.BATCH_NO AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,CASE WHEN SD.BALE_TYPE='IPB' THEN 'THRESHING-ISSUE' ELSE CASE WHEN SD.BALE_TYPE='OPB' THEN 'THRESHING-BY-PRODUCT' END END PROCESS,HD.CREATED_DATE, SD.GPIL_BALE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.subinventory_code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_THRESH_RECON_DTLS_1 SD ,GPIL_ITEM_MASTER ST ,GPIL_THRESH_RECON_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND SD.GRADE = ST.ITEM_CODE AND  HD.FLAG IS NULL AND  (SD.subInventory_Code!=ST.ATTRIBUTE2) ORDER BY SD.CREATED_DATE DESC";
            //        dtClr = cMgt.GetQueryResult(sSql);
            //        if (dtClr.Rows.Count > 0)
            //        {
            //            dt.Merge(dtClr);
            //        }
            //        sSql = "SELECT  SD.CASE_NUMBER AS REPORT_REF,(HD.ORGN_CODE) AS ORGN_CODE,'THRESHING-PRODUCT'  AS PROCESS,HD.CREATED_DATE, SD.CASE_NUMBER,HD.FLAG AS ERP_STATUS ,SD.subinventory_code SHIPMENT_WT, ST.ATTRIBUTE2 AS STOCK_WT FROM GPIL_THRESH_RECON_DTLS_2 SD ,GPIL_ITEM_MASTER ST ,GPIL_THRESH_RECON_HDR HD WHERE YEAR(SD.CREATED_DATE)='" + crop + "' AND SD.BATCH_NO = HD.BATCH_NO   AND HD.FLAG IS NULL AND SD.Packed_Grade = ST.Item_code AND (SD.subInventory_Code!=ST.ATTRIBUTE2) ORDER BY SD.CREATED_DATE DESC ";
            //        dtClr = cMgt.GetQueryResult(sSql);
            //        if (dtClr.Rows.Count > 0)
            //        {
            //            dt.Merge(dtClr);
            //        }
            //        if (dt.Rows.Count > 0)
            //        {
            //            dt.DefaultView.Sort = "CREATED_DATE";
            //            json = JsonConvert.SerializeObject(dt);
            //            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //            jsonResult.MaxJsonLength = int.MaxValue;
            //            return jsonResult;


            //        }
            //        else
            //        {
            //            lblMessage = "Error: No Records found";
            //            //GridViewSample.Visible = false;
            //        }

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

            //    //json = JsonConvert.SerializeObject(dt);
            //    //return Json(json, JsonRequestBehavior.AllowGet);


            //}
        }
}