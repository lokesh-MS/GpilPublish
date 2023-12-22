﻿using GPILWebApp.Models;
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
    public class VerificationExportDataController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;
        public VerificationExportDataController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }

        // GET: VerificationExportData
        public ActionResult Index()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            //ViewBag.GPIL_CROP_MASTERs = (from c in _context.GPIL_CROP_MASTER select new { c.CROP, c.CROP_YEAR }).Distinct();
            ViewBag.GPIL_VARIETY_MASTERs = (from v in _context.GPIL_VARIETY_MASTER select new { v.VARIETY, VARIETYNAME = v.VARIETY + " - " + v.VARIETY_NAME }).Distinct();
            ViewBag.GPIL_ORGN_MASTERs = (from s in _context.GPIL_ORGN_MASTER select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).Distinct();
            ViewBag.GPIL_OPERATION_RECIPEs = (from r in _context.GPIL_OPERATION_RECIPE select new { r.RECIPE_CODE, r.OPERATION_RECIPE }).Distinct();
            return View();
        }
        string fromDate = "";
        string toDate = "";
        string crop = "";
        string variety = "";
        string orgnCode = "";
        string operationRec = "";
        string exportData = "";
        string data = String.Empty;
        JsonResult jsonResult;
        string json = String.Empty;
        public ActionResult ExportToExcel(string fromDate, string toDate, string crop, string variety, string orgnCode, string operationRec, string exportData)
        {

            if (fromDate.Trim().Length == 0)
            {

                data = "Error: Please select the From Date";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
               
            }

            if (toDate.Trim().Length == 0)
            {

                data = "Error: Please select the To Date";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }

            if (crop.Trim().Length == 0)
            {
                if (exportData != "Dispatch" && exportData != "Dispatch Summary" && exportData != "Receipt" && exportData == "Receipt Summary" && exportData == "Sales" && exportData == "Crop Transfer")
                {

                    data = "Error: Please select the Crop";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }
            }
            if (variety.Trim().Length == 0)
            {
                if (exportData != "Dispatch" && exportData != "Dispatch Summary" && exportData != "Receipt" && exportData == "Receipt Summary" && exportData == "Sales")
                {

                    if (exportData != "Dispatch" && exportData != "Dispatch Summary")
                    {
                        data = "Error: Please select the Variety";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
               

            }
            //if (orgnCode.Trim().Length == 0)
            //{

            //    data = "Error: Please select the Organization Code";
            //    json = JsonConvert.SerializeObject(data);
            //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //    jsonResult.MaxJsonLength = int.MaxValue;
            //    return jsonResult;

            //}
            //if (operationRec.Trim().Length == 0)
            //{

            //    data = "Error: Please select the Operation Receipe";
            //    json = JsonConvert.SerializeObject(data);
            //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //    jsonResult.MaxJsonLength = int.MaxValue;
            //    return jsonResult;

            //}
            if (exportData.Trim().Length == 0)
            {

                data = "Error: Please select the Process Name";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }

            Session["FromDate"] = fromDate;
            Session["ToDate"] = toDate;

            Session["Crop"] = crop;

            Session["Variety"] = variety;

            Session["OrgnCode"] = orgnCode;
            Session["OperationRec"] = operationRec;


            Session["ExportData"] = exportData;


            DataTable dt1 = new DataTable();
            dt1 = GetExcelDataDetails();
            if (dt1.Rows.Count > 0)
            {
                var grdReport = new System.Web.UI.WebControls.GridView();
                grdReport.DataSource = dt1;
                grdReport.DataBind();

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                grdReport.RenderControl(htw);
                byte[] binddata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
                string fileName = "DynamicFileName.xls";

                return File(binddata, "application/ms-excel", "fileName");
            }
            else
            {
                return null;
            }
            //return null;
        }

        public JsonResult FarmerPurchaceBW(string fromDate, string toDate, string crop, string variety, string orgnCode, string operationRec, string exportData)
        {
            Session["FromDate"] = fromDate;
            Session["ToDate"] = toDate;

            Session["Crop"] = crop;
           
            Session["Variety"] = variety;

            Session["OrgnCode"] = orgnCode;
            Session["OperationRec"] = operationRec;

            
            Session["ExportData"] = exportData;

            return null;
        }



        DataTable dt = new DataTable();
        CommonManagement cMgt = new CommonManagement();
        [NonAction]
        public DataTable GetExcelDataDetails()
        {

            fromDate = (string)Session["FromDate"];
            toDate = (string)Session["ToDate"];
            crop = (string)Session["Crop"];
            //crop = "15";
            variety = (string)Session["Variety"];
            orgnCode = (string)Session["OrgnCode"];
            operationRec = (string)Session["OperationRec"];
            exportData = (string)Session["ExportData"];
            string strQuery = "";
            string strQueryWhere = "";
            string strQueryOrderBy = "";

            if (exportData == "TAP Purchase")
            {
                //strQuery = "SELECT H.HEADER_ID,H.ORGN_CODE,H.DATE_OF_PURCH,H.BUYER_CODE,H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER,D.TB_LOT_NO,D.TBGR_NO,D.TB_GRADE,D.BUYER_GRADE,D.NET_WT AS QUANTITY,D.RATE,D.SUBINVENTORY_CODE,D.PATTA_CHARGE,D.SERVICE_CHARGE,D.SERVICE_CHARGE_AMT,D.SERVICE_TAX,D.SERVICE_TAX_AMT,D.CREATED_DATE,D.SH_ED_TAX,D.ED_CESS_TAX FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.PURCHASE_TYPE='TAP PURCHASE' AND D.REJE_STATUS='OK' AND H.STATUS='N' AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                //strQuery = "SELECT H.HEADER_ID,H.ORGN_CODE,H.DATE_OF_PURCH,H.BUYER_CODE,H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER,D.TB_LOT_NO,D.TBGR_NO,D.TB_GRADE,D.BUYER_GRADE,D.NET_WT AS QUANTITY,D.RATE,D.SUBINVENTORY_CODE,D.PATTA_CHARGE,D.SERVICE_CHARGE,D.SERVICE_CHARGE_AMT,D.SERVICE_TAX,D.SERVICE_TAX_AMT,D.CREATED_DATE,D.SH_ED_TAX,D.ED_CESS_TAX,I.ITEM_CODE_GROUP FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.BUYER_GRADE=I.ITEM_CODE AND H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.PURCHASE_TYPE='TAP PURCHASE' AND D.REJE_STATUS='OK' AND H.STATUS='N' AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.HEADER_ID,H.ORGN_CODE,H.DATE_OF_PURCH,H.BUYER_CODE,H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER,D.TB_LOT_NO,D.TBGR_NO,D.TB_GRADE,D.BUYER_GRADE,D.NET_WT AS QUANTITY,D.RATE,D.SUBINVENTORY_CODE,D.PATTA_CHARGE,D.SERVICE_CHARGE,D.SERVICE_CHARGE_AMT,D.SERVICE_TAX,D.SERVICE_TAX_AMT,D.CREATED_DATE,D.SH_ED_TAX,D.ED_CESS_TAX,I.ITEM_CODE_GROUP FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.BUYER_GRADE=I.ITEM_CODE AND H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.PURCHASE_TYPE='TAP PURCHASE' AND D.REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                //strQuery = "SELECT H.HEADER_ID,H.ORGN_CODE,H.DATE_OF_PURCH,H.BUYER_CODE,H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER,D.TB_LOT_NO,D.TBGR_NO,D.TB_GRADE,D.BUYER_GRADE,D.NET_WT AS QUANTITY,D.RATE,D.SUBINVENTORY_CODE,D.PATTA_CHARGE,D.SERVICE_CHARGE,D.SERVICE_CHARGE_AMT,D.SERVICE_TAX,D.SERVICE_TAX_AMT,D.CREATED_DATE,D.SH_ED_TAX,D.ED_CESS_TAX,I.ITEM_CODE_GROUP FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H,GPIL_ITEM_MASTER(NOLOCK) I,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.BUYER_GRADE=I.ITEM_CODE AND H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.PURCHASE_TYPE='TAP PURCHASE' AND D.REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID AND H.DATE_OF_PURCH BETWEEN SC.STARTING_DATE AND ISNULL(SC.ENDING_DATE,GETDATE()) ";
                if (orgnCode != null && orgnCode !="")
                {
                    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                    strQueryOrderBy = " ORDER BY H.HEADER_ID,D.GPIL_BALE_NUMBER";
                    //strQuery = strQuery + strQueryWhere + strQueryOrderBy;
                }
                else
                {
                    strQueryOrderBy = " ORDER BY H.HEADER_ID,D.GPIL_BALE_NUMBER";
                    strQuery = strQuery + strQueryOrderBy;
                }
               
            }
            else if (exportData == "Farmer Purchase")
            {
                //string fromDate = "";
                //string toDate = "";
                //string crop = "";
                //string variety = "";
                //string orgnCode = "";
                //string operationRec = "";
                //string exportData = "";
                //string data = String.Empty;
                //JsonResult jsonResult;
                //string json = String.Empty;
                //if (operationRec.Trim().Length == 0)
                //{
                    
                //    data = "Error: Please select the Operation Receipe";
                //    json = JsonConvert.SerializeObject(data);
                //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //    jsonResult.MaxJsonLength = int.MaxValue;
                    

                //}
                // else
                //{

                    //strQuery = "SELECT H.HEADER_ID,H.ORGN_CODE,H.DATE_OF_PURCH,H.BUYER_CODE,H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER,D.TB_LOT_NO,D.FARMER_CODE,D.TB_GRADE,D.BUYER_GRADE,D.ATTRIBUTE2 AS CLASSIFICATION_GADE,D.NET_WT AS QUANTITY,D.RATE,D.ATTRIBUTE4 AS RATE_WOF,D.SUBINVENTORY_CODE,D.CREATED_DATE FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND D.REJE_STATUS='OK' AND H.STATUS='N' AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                    strQuery = "SELECT H.HEADER_ID,H.ORGN_CODE,H.DATE_OF_PURCH,H.BUYER_CODE,H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER,D.TB_LOT_NO,D.FARMER_CODE,D.TB_GRADE,D.BUYER_GRADE,D.ATTRIBUTE2 AS CLASSIFICATION_GADE,D.NET_WT AS QUANTITY,D.RATE,D.ATTRIBUTE4 AS RATE_WOF,D.SUBINVENTORY_CODE,D.CREATED_DATE,I.ITEM_CODE_GROUP FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.BUYER_GRADE=I.ITEM_CODE AND H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND D.REJE_STATUS='OK' AND H.STATUS='N' AND H.DATE_OF_PURCH BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                    if (orgnCode != null && orgnCode != "")
                    {
                        strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                        strQueryOrderBy = " ORDER BY H.HEADER_ID,D.GPIL_BALE_NUMBER";
                       // strQuery = strQuery + strQueryWhere + strQueryOrderBy;
                    }
                    else
                    {
                        strQueryOrderBy = " ORDER BY H.HEADER_ID,D.GPIL_BALE_NUMBER";
                        //strQuery = strQuery + strQueryOrderBy;
                    }
               


               
            }
            else if (exportData == "Supplier Purchase")
            {
                //strQuery = "SELECT H.HEADER_ID,H.STATUS,H.RECEV_ORGN_CODE,H.LP4_DATE,H.LP4_NUMBER,H.SUPP_CODE,H.SITE_NAME,H.BUYER_CODE,D.GPIL_BALE_NUMBER,D.GRADE,D.NET_WEIGHT AS QUANTITY,D.SUBINVENTORY_CODE FROM GPIL_SUPP_PURCHS_DTLS (NOLOCK) D,GPIL_SUPP_PURCHS_HDR (NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N','P') AND H.LP4_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.HEADER_ID,H.STATUS,H.RECEV_ORGN_CODE,H.LP4_DATE,H.LP4_NUMBER,H.SUPP_CODE,H.SITE_NAME,H.BUYER_CODE,D.GPIL_BALE_NUMBER,D.GRADE,D.NET_WEIGHT AS QUANTITY,S.PRICE AS RATE,D.SUBINVENTORY_CODE,I.ITEM_CODE_GROUP FROM GPIL_SUPP_PURCHS_DTLS (NOLOCK) D,GPIL_SUPP_PURCHS_HDR (NOLOCK) H,GPIL_STOCK(NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND D.GRADE=I.ITEM_CODE AND H.HEADER_ID=D.HEADER_ID AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N','P') AND H.LP4_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (orgnCode != null && orgnCode != "")
                {
                    strQueryWhere = " AND H.RECEV_ORGN_CODE='" + orgnCode + "' ";
                    strQueryOrderBy = " ORDER BY H.HEADER_ID,D.GPIL_BALE_NUMBER";
                    //strQuery = strQuery + strQueryWhere + strQueryOrderBy;
                }
                else
                {
                    strQueryOrderBy = " ORDER BY H.HEADER_ID,D.GPIL_BALE_NUMBER";
                    //strQuery = strQuery + strQueryOrderBy;
                }
               
            }


           //need to change below code in future
            else if (exportData == "Dispatch")
            {
                //strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_NO,H.SENT_BY,H.SENDER_TRUCK_NO,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,H.REDIRECT_STATUS,H.REDIRECT_SHIPMENT_NO,H.FRIEGHT_CHARGES,ISNULL(H.TOT_NO_OF_BALES,0) AS TOT_NO_OF_BALES,H.UOM,H.TEMP_REF,H.RECEV_WEIGH_TYPE,H.IS_WMS_SHIPMENT,H.PICKLIST_NO,H.LP5_NO,H.STATUS,H.FLAG,H.WMS_STATUS,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,D.DISPATCH_WEIGHT,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.LOADING_DATETIME,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.WEIGHT_STATUS FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('INT','P','N') AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_NO,H.SENT_BY,H.SENDER_TRUCK_NO,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,H.REDIRECT_STATUS,H.REDIRECT_SHIPMENT_NO,H.FRIEGHT_CHARGES,ISNULL(H.TOT_NO_OF_BALES,0) AS TOT_NO_OF_BALES,H.UOM,H.TEMP_REF,H.RECEV_WEIGH_TYPE,H.IS_WMS_SHIPMENT,H.PICKLIST_NO,H.LP5_NO,H.STATUS,H.FLAG,H.WMS_STATUS,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,S.PRICE AS RATE,D.DISPATCH_WEIGHT,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.LOADING_DATETIME,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.WEIGHT_STATUS,I.ITEM_CODE_GROUP FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GRADE=I.ITEM_CODE AND H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('INT','P','N') AND H.SENDER_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (orgnCode != null && orgnCode != "")
                {
                    strQueryWhere = " AND H.SENDER_ORGN_CODE='" + orgnCode + "' ";
                }

                if (crop != null && crop != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.CROP='" + crop + "' ";
                }

                if (variety != null && variety != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.VARIETY='" + variety + "' ";
                }

                strQueryOrderBy = " ORDER BY H.SHIPMENT_NO,D.GPIL_BALE_NUMBER";
            }
            else if (exportData == "Receipt")
            {
                //strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.RECEIVED_DATE,H.SENDER_NO,H.RECEIVER_NO,H.SENT_BY,H.RECEIVED_BY,H.SENDER_TRUCK_NO,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,H.REDIRECT_STATUS,H.REDIRECT_SHIPMENT_NO,H.FRIEGHT_CHARGES,ISNULL(H.TOT_NO_OF_BALES,0) AS TOT_NO_OF_BALES,H.UOM,H.TEMP_REF,H.RECEV_WEIGH_TYPE,H.IS_WMS_SHIPMENT,H.PICKLIST_NO,H.LP5_NO,H.STATUS,H.FLAG,H.WMS_STATUS,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,D.DISPATCH_WEIGHT,D.RECEIPT_WEIGHT,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.WEIGHT_STATUS FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('N') AND H.RECEIVED_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.RECEIVED_DATE,H.SENDER_NO,H.RECEIVER_NO,H.SENT_BY,H.RECEIVED_BY,H.SENDER_TRUCK_NO,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,H.REDIRECT_STATUS,H.REDIRECT_SHIPMENT_NO,H.FRIEGHT_CHARGES,ISNULL(H.TOT_NO_OF_BALES,0) AS TOT_NO_OF_BALES,H.UOM,H.TEMP_REF,H.RECEV_WEIGH_TYPE,H.IS_WMS_SHIPMENT,H.PICKLIST_NO,H.LP5_NO,H.STATUS,H.FLAG,H.WMS_STATUS,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,S.PRICE AS RATE,D.DISPATCH_WEIGHT,D.RECEIPT_WEIGHT,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.WEIGHT_STATUS,I.ITEM_CODE_GROUP FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GRADE=I.ITEM_CODE AND H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('N') AND H.RECEIVED_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (orgnCode != null && orgnCode != "")
                {
                    strQueryWhere = " AND H.RECEIVER_ORGN_CODE='" + orgnCode + "' ";
                }

                if (crop != null &&  crop != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.CROP='" + crop + "' ";
                }

                if (variety != null && variety != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.VARIETY='" + variety + "' ";
                }


                strQueryOrderBy = " ORDER BY H.SHIPMENT_NO,D.GPIL_BALE_NUMBER";
            }
            //changed condition date 30-09-2023 without org and variety
            else if (exportData == "Dispatch Summary")
            {
                //strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_TRUCK_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QUANTITY,SUM(D.DISPATCH_WEIGHT) AS DISPATCH_QUANTITY  FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('INT','P','N') AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_TRUCK_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QUANTITY,S.PRICE AS RATE,SUM(D.DISPATCH_WEIGHT) AS DISPATCH_QUANTITY,I.ITEM_CODE_GROUP FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GRADE=I.ITEM_CODE AND H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('INT','P','N') AND H.SENDER_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";

                if (orgnCode != null && orgnCode != "")
                {
                    strQueryWhere = " AND H.SENDER_ORGN_CODE='" + orgnCode + "' ";
                }

                if (crop != null && crop != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.CROP='" + crop + "' ";
                }

                if (variety != null && variety !="")
                {
                    strQueryWhere = strQueryWhere + " AND S.VARIETY='" + variety + "' ";
                }


                strQueryWhere = strQueryWhere + " GROUP BY H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_TRUCK_NO,D.GRADE,S.PRICE,I.ITEM_CODE_GROUP ";

                strQueryOrderBy = " ORDER BY H.SHIPMENT_NO,D.GRADE";
            }
            else if (exportData == "Receipt Summary")
            {
                //strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.RECEIVED_DATE,H.SENDER_TRUCK_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QUANTITY,SUM(D.DISPATCH_WEIGHT) AS DISPATCH_QUANTITY,SUM(ISNULL(D.RECEIPT_WEIGHT,D.DISPATCH_WEIGHT)) AS RECEIPT_QUANTITY FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('N') AND H.RECEIVED_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.RECEIVED_DATE,H.SENDER_TRUCK_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QUANTITY,S.PRICE AS RATE,SUM(D.DISPATCH_WEIGHT) AS DISPATCH_QUANTITY,SUM(ISNULL(D.RECEIPT_WEIGHT,D.DISPATCH_WEIGHT)) AS RECEIPT_QUANTITY,I.ITEM_CODE_GROUP FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GRADE=I.ITEM_CODE AND H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('N') AND H.RECEIVED_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (orgnCode != null && orgnCode !="")
                {
                    strQueryWhere = " AND H.RECEIVER_ORGN_CODE='" + orgnCode + "' ";
                }

                if (crop != null && crop !="")
                {
                    strQueryWhere = strQueryWhere + " AND S.CROP='" + crop + "' ";
                }

                if (variety != null && variety != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.VARIETY='" + variety + "' ";
                }

                strQueryWhere = strQueryWhere + " GROUP BY H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.RECEIVED_DATE,H.SENDER_TRUCK_NO,D.GRADE,S.PRICE,I.ITEM_CODE_GROUP ";

                strQueryOrderBy = " ORDER BY H.SHIPMENT_NO,D.GRADE";
            }
            else if (exportData == "Sales")
            {
                //strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_NO,H.SENT_BY,H.SENDER_TRUCK_NO,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,ISNULL(H.TOT_NO_OF_BALES,0) AS TOT_NO_OF_BALES,H.UOM,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,D.DISPATCH_WEIGHT,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE FROM GPIL_SO_RESERVATION_DTLS  (NOLOCK) D,GPIL_SO_RESERVATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('N') AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_DATE,H.SENDER_NO,H.SENT_BY,H.SENDER_TRUCK_NO,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,ISNULL(H.TOT_NO_OF_BALES,0) AS TOT_NO_OF_BALES,H.UOM,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,S.PRICE AS RATE,D.DISPATCH_WEIGHT,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,I.ITEM_CODE_GROUP FROM GPIL_SO_RESERVATION_DTLS  (NOLOCK) D,GPIL_SO_RESERVATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GRADE=I.ITEM_CODE AND H.SHIPMENT_NO=D.SHIPMENT_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.STATUS IN ('N') AND H.SENDER_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (orgnCode != null && orgnCode != "")
                {
                    strQueryWhere = " AND H.SENDER_ORGN_CODE='" + orgnCode + "' ";
                }

                if (crop != null && crop != "")
                {
                    strQueryWhere = strQueryWhere + " AND S.CROP='" + crop + "' ";
                }

                if (variety != null && variety !="")
                {
                    strQueryWhere = strQueryWhere + " AND S.VARIETY='" + variety + "' ";
                }

                strQueryOrderBy = " ORDER BY H.SHIPMENT_NO,D.GPIL_BALE_NUMBER";
            }
            else if (exportData == "Classification")
            {
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.CLASSIFICATION_DATE,H.REASONING_CODE,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,D.CLASSIFICATION_GRADE,D.MARKED_WT,P.RATE,D.WEIGHT_AFTER_CLASSIFICATION,D.WEIGHT_BEFORE_CLASSIFY,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS FROM GPIL_CLASSIFICATION_DTLS (NOLOCK) D,GPIL_CLASSIFICATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) P WHERE D.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER AND H.BATCH_NO=D.BATCH_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.REASONING_CODE='0' AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND S.CROP='" + crop + "' AND S.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.CLASSIFICATION_DATE,H.REASONING_CODE,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,D.CLASSIFICATION_GRADE,D.MARKED_WT,P.RATE,D.WEIGHT_AFTER_CLASSIFICATION,D.WEIGHT_BEFORE_CLASSIFY,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_CLASSIFICATION_DTLS (NOLOCK) D,GPIL_CLASSIFICATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) P,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.CLASSIFICATION_GRADE=I.ITEM_CODE AND D.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER AND H.BATCH_NO=D.BATCH_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.REASONING_CODE='0' AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND S.CROP='" + crop + "' AND S.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.CLASSIFICATION_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";

                if (orgnCode != null)
                {
                    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                }

                strQueryOrderBy = " ORDER BY H.BATCH_NO,D.GPIL_BALE_NUMBER";
            }
            else if (exportData == "Grade Transfer")
            {
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.CLASSIFICATION_DATE,H.REASONING_CODE,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,D.CLASSIFICATION_GRADE,D.MARKED_WT,D.WEIGHT_AFTER_CLASSIFICATION,D.WEIGHT_BEFORE_CLASSIFY,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS FROM GPIL_CLASSIFICATION_DTLS (NOLOCK) D,GPIL_CLASSIFICATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S WHERE H.BATCH_NO=D.BATCH_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.REASONING_CODE='1' AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND S.CROP='" + crop + "' AND S.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.CLASSIFICATION_DATE,H.REASONING_CODE,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,D.CLASSIFICATION_GRADE,D.MARKED_WT,S.PRICE AS RATE,D.WEIGHT_AFTER_CLASSIFICATION,D.WEIGHT_BEFORE_CLASSIFY,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_CLASSIFICATION_DTLS (NOLOCK) D,GPIL_CLASSIFICATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.CLASSIFICATION_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.REASONING_CODE='1' AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND S.CROP='" + crop + "' AND S.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.CLASSIFICATION_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.CLASSIFICATION_DATE,H.REASONING_CODE,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TEMP_REF,H.STATUS,H.FLAG,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE,D.CLASSIFICATION_GRADE,D.MARKED_WT,S.PRICE AS RATE,D.WEIGHT_AFTER_CLASSIFICATION,D.WEIGHT_BEFORE_CLASSIFY,D.FROM_SUBINVENTORY_CODE,D.TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_CLASSIFICATION_DTLS (NOLOCK) D,GPIL_CLASSIFICATION_HDR (NOLOCK) H,GPIL_STOCK (NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.CLASSIFICATION_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER  AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND S.CROP='" + crop + "' AND S.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.CLASSIFICATION_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";// lokesh code removed from H.REASONING_CODE='1' above queery

                if (orgnCode != null &&  orgnCode != "")
                {
                    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                }

                strQueryOrderBy = " ORDER BY H.BATCH_NO,D.GPIL_BALE_NUMBER";
            }
            else if (exportData == "Crop Transfer")
            {
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.DATE_OF_OPERATION,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TOT_NO_OF_BALES,H.STATUS,H.TEMP_REF,H.IS_PACKED_TRANSFER,H.WMS_STATUS,D.OLD_BALE_NUMBER,D.OLD_CROP,D.OLD_VARIETY,D.OLD_GRADE,D.FROM_SUBINVENTORY_CODE,D.MARKED_WT,D.NEW_BALE_NUMBER,D.NEW_CROP,D.NEW_VARIETY,D.NEW_GRADE,D.TO_SUBINVENTORY_CODE,D.TRANSFER_TYPE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS FROM GPIL_CROP_TRANS_DTLS (NOLOCK)  D,GPIL_CROP_TRANS_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO  AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.DATE_OF_OPERATION,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TOT_NO_OF_BALES,H.STATUS,H.TEMP_REF,H.IS_PACKED_TRANSFER,H.WMS_STATUS,D.OLD_BALE_NUMBER,D.OLD_CROP,D.OLD_VARIETY,D.OLD_GRADE,D.FROM_SUBINVENTORY_CODE,D.MARKED_WT,P.RATE,D.NEW_BALE_NUMBER,D.NEW_CROP,D.NEW_VARIETY,D.NEW_GRADE,D.TO_SUBINVENTORY_CODE,D.TRANSFER_TYPE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_CROP_TRANS_DTLS (NOLOCK)  D,GPIL_CROP_TRANS_HDR (NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS P,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.OLD_BALE_NUMBER=P.GPIL_BALE_NUMBER AND D.NEW_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO  AND H.STATUS IN ('N') AND D.NEW_CROP='" + crop.Trim() + "' AND D.NEW_VARIETY='" + variety.Trim() + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.DATE_OF_OPERATION,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TOT_NO_OF_BALES,H.STATUS,H.TEMP_REF,H.IS_PACKED_TRANSFER,H.WMS_STATUS,D.OLD_BALE_NUMBER,D.OLD_CROP,D.OLD_VARIETY,D.OLD_GRADE,D.FROM_SUBINVENTORY_CODE,D.MARKED_WT,P.RATE,D.NEW_BALE_NUMBER,D.NEW_CROP,D.NEW_VARIETY,D.NEW_GRADE,D.TO_SUBINVENTORY_CODE,D.TRANSFER_TYPE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_CROP_TRANS_DTLS (NOLOCK)  D,GPIL_CROP_TRANS_HDR (NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS P,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.OLD_BALE_NUMBER=P.GPIL_BALE_NUMBER AND D.NEW_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO  AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.DATE_OF_OPERATION,H.RECIPE_CODE,H.CLASSIFIER_NAME,H.TOT_NO_OF_BALES,H.STATUS,H.TEMP_REF,H.IS_PACKED_TRANSFER,H.WMS_STATUS,D.OLD_BALE_NUMBER,D.OLD_CROP,D.OLD_VARIETY,D.OLD_GRADE,D.FROM_SUBINVENTORY_CODE,D.MARKED_WT,S.PRICE AS RATE,D.NEW_BALE_NUMBER,D.NEW_CROP,D.NEW_VARIETY,D.NEW_GRADE,D.TO_SUBINVENTORY_CODE,D.TRANSFER_TYPE,D.CREATED_BY,D.CREATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_CROP_TRANS_DTLS (NOLOCK) D,GPIL_CROP_TRANS_HDR (NOLOCK) H,GPIL_STOCK(NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.OLD_BALE_NUMBER=S.GPIL_BALE_NUMBER AND D.NEW_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO  AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (orgnCode != null && orgnCode != "")
                {
                    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                }

                strQueryOrderBy = " ORDER BY H.BATCH_NO";
            }
            else if (exportData == "Grading")
            {
                //Changed by Rajeswaran  21-08-2018
                strQueryWhere = string.Empty;
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.SUPERVISOR_NAME,H.ISSUED_GRADE,H.CROP,H.VARIETY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS') THEN 'BY-PRODUCT' ELSE 'PRODUCT' END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.REMARKS FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                // strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.SUPERVISOR_NAME,H.ISSUED_GRADE,H.CROP,H.VARIETY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS') THEN 'BY-PRODUCT' ELSE 'PRODUCT' END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,P.RATE,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS P,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER AND D.GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";

                strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.SUPERVISOR_NAME,H.ISSUED_GRADE,H.CROP,H.VARIETY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS') THEN 'BY-PRODUCT' ELSE 'PRODUCT' END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND H.STATUS IN ('N','NN') AND H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                if (crop != null)
                {
                    strQueryWhere = "AND H.CROP='" + crop.Trim() + "' ";
                }
                if (variety != null)
                {
                    strQueryWhere = strQueryWhere + " AND H.VARIETY='" + variety.Trim() + "' ";
                }
                if (orgnCode != null)
                {
                    strQueryWhere = strQueryWhere + " AND H.ORGN_CODE='" + orgnCode + "' ";
                }
                
                if (operationRec != null)
                {
                    strQueryWhere = strQueryWhere + " AND H.RECIPE_CODE='" + operationRec + "' ";
                }


                strQueryOrderBy = " ORDER BY H.BATCH_NO,D.GPIL_BALE_NUMBER";


                //strQueryWhere = string.Empty;
                ////strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.SUPERVISOR_NAME,H.ISSUED_GRADE,H.CROP,H.VARIETY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS') THEN 'BY-PRODUCT' ELSE 'PRODUCT' END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.REMARKS FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.SUPERVISOR_NAME,H.ISSUED_GRADE,H.CROP,H.VARIETY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS') THEN 'BY-PRODUCT' ELSE 'PRODUCT' END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,P.RATE,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,D.REMARKS,I.ITEM_CODE_GROUP FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS P,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER AND D.GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'') <>'N' AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N','NN') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                //if (orgnCode != null)
                //{
                //    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                //}

                //if (operationRec != null)
                //{
                //    strQueryWhere = strQueryWhere + " AND H.RECIPE_CODE='" + operationRec + "' ";
                //}


                //strQueryOrderBy = " ORDER BY H.BATCH_NO,D.GPIL_BALE_NUMBER";
            }
            else if (exportData == "Threshing")
            {
                //strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.REPORT_NO,H.SHIFT,H.SHIFT_INCHARGE,H.CROP,H.VARIETY,H.STATUS,H.FLAG,H.CREATED_DATE,H.LAST_UPDATED_DATE,H.TEMP_REF,H.WMS_RECEIPT,H.WMS_STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS','SLOSS') THEN 'BY-PRODUCT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('GT') THEN 'GRADE TRANSFER' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('EB') THEN 'DAMAGE / ELIMINATION' ELSE 'NOT DEFINE' END) END) END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) UNION SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.REPORT_NO,H.SHIFT,H.SHIFT_INCHARGE,H.CROP,H.VARIETY,H.STATUS,H.FLAG,H.CREATED_DATE,H.LAST_UPDATED_DATE,H.TEMP_REF,H.WMS_RECEIPT,H.WMS_STATUS,D.CASE_NUMBER AS GPIL_BALE_NUMBER,'PRODUCT' AS BALE_TYPE,D.PACKED_GRADE AS GRADE,D.NET_WT AS MARKED_WT,D.NET_WT AS ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE FROM GPIL_THRESH_RECON_DTLS_2 (NOLOCK)  D,GPIL_THRESH_RECON_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)";
                strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.REPORT_NO,H.SHIFT,H.SHIFT_INCHARGE,H.CROP,H.VARIETY,H.STATUS,H.FLAG,H.CREATED_DATE,H.LAST_UPDATED_DATE,H.TEMP_REF,H.WMS_RECEIPT,H.WMS_STATUS,D.GPIL_BALE_NUMBER,(CASE WHEN D.BALE_TYPE='IPB' THEN 'INCREDIANT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('BP','LOSS','SLOSS') THEN 'BY-PRODUCT' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('GT') THEN 'GRADE TRANSFER' ELSE (CASE WHEN D.PRODUCT_TYPE IN ('EB') THEN 'DAMAGE / ELIMINATION' ELSE 'NOT DEFINE' END) END) END) END) AS BALE_TYPE,D.GRADE,D.MARKED_WT,S.PRICE AS RATE,D.ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,I.ITEM_CODE_GROUP FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H,GPIL_STOCK S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND D.GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + fromDate + " 00:00:00',102) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',102) UNION SELECT H.BATCH_NO,H.ORGN_CODE,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.REPORT_NO,H.SHIFT,H.SHIFT_INCHARGE,H.CROP,H.VARIETY,H.STATUS,H.FLAG,H.CREATED_DATE,H.LAST_UPDATED_DATE,H.TEMP_REF,H.WMS_RECEIPT,H.WMS_STATUS,D.CASE_NUMBER AS GPIL_BALE_NUMBER,'PRODUCT' AS BALE_TYPE,D.PACKED_GRADE AS GRADE,D.NET_WT AS MARKED_WT,S.PRICE AS RATE,D.NET_WT AS ASCERTAIN_WT,D.SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,I.ITEM_CODE_GROUP FROM GPIL_THRESH_RECON_DTLS_2 (NOLOCK)  D,GPIL_THRESH_RECON_HDR (NOLOCK) H,GPIL_STOCK S,GPIL_ITEM_MASTER(NOLOCK) I WHERE D.CASE_NUMBER=S.GPIL_BALE_NUMBER AND D.PACKED_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND H.CROP='" + crop + "' AND H.VARIETY='" + variety + "' AND H.STATUS IN ('N') AND H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',102) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',102)";
                if (orgnCode != null)
                {
                    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                }


                if (operationRec != null)
                {
                    strQueryWhere = strQueryWhere + " AND H.RECIPE_CODE='" + operationRec + "' ";
                }


                strQueryOrderBy = " ORDER BY BATCH_NO,BALE_TYPE,GRADE,GPIL_BALE_NUMBER";
            }
            else if (exportData == "Fumigation")
            {
                strQuery = "SELECT H.BATCH_NO,H.ORGN_CODE,H.FUMIGATION_DAYS_FOR_RUNPREIOD,H.FUMIGATION_DAYS_FOR_EXPIRY,H.FUMIGATION_STARTING_DATE,H.FUMIGATION_ENDING_DATE,H.TOT_FUN_IN_CASES,H.STATUS,H.TEMP_REF,WMS_STATUS,D.CASE_NUMBER,D.FUMIGATION_EXPIRY_DATE,D.FUM_STATUS FROM GPIL_FUMIGATION_DTLS (NOLOCK) D,GPIL_FUMIGATION_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND H.FUMIGATION_STARTING_DATE BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";

                if (orgnCode != null)
                {
                    strQueryWhere = " AND H.ORGN_CODE='" + orgnCode + "' ";
                }

                strQueryOrderBy = " ORDER BY H.BATCH_NO,D.CASE_NUMBER";
            }
            else if (exportData == "Stock")
            {
                //strQuery = "SELECT GPIL_BALE_NUMBER,TB_LOT_NO,TBGR_NO,TB_GRADE,BUYER_GRADE,GRADE,MARKED_WT,CURR_WT,ORIGN_ORGN_CODE,CURR_ORGN_CODE,CROP,VARIETY,PRICE,SUBINVENTORY_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,BALE_CARD_TYPE,PRODUCT_TYPE,BATCH_NO,ATTRIBUTE2 AS FROM_ORGN,ATTRIBUTE3 AS TO_ORGN,STATUS,PROCESS_STATUS,WMS_STATUS,PICKLIST_NO,LP5NO,NIC,TRS,CL,FUMIGATION_STATUS,FUMIGATION_STARTING_DATE,FUMIGATION_ENDING_DATE,FUMIGATION_EXPIRY_DATE FROM GPIL_STOCK (NOLOCK) WHERE CROP='" + crop + "' AND VARIETY='" + variety + "' AND STATUS IN ('INT','Y')";
                strQuery = "SELECT S.GPIL_BALE_NUMBER,S.TB_LOT_NO,S.TBGR_NO,S.TB_GRADE,S.BUYER_GRADE,S.GRADE,S.MARKED_WT,S.PRICE AS RATE,S.CURR_WT,S.ORIGN_ORGN_CODE,S.CURR_ORGN_CODE,S.CROP,S.VARIETY,S.SUBINVENTORY_CODE,S.CREATED_BY,S.CREATED_DATE,S.LAST_UPDATED_BY,S.LAST_UPDATED_DATE,S.BALE_CARD_TYPE,S.PRODUCT_TYPE,S.BATCH_NO,S.ATTRIBUTE2 AS FROM_ORGN,S.ATTRIBUTE3 AS TO_ORGN,S.STATUS,S.PROCESS_STATUS,S.WMS_STATUS,S.PICKLIST_NO,S.LP5NO,S.NIC,S.TRS,S.CL,S.FUMIGATION_STATUS,S.FUMIGATION_STARTING_DATE,S.FUMIGATION_ENDING_DATE,S.FUMIGATION_EXPIRY_DATE,I.ITEM_CODE_GROUP FROM GPIL_STOCK(NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE S.CROP='" + crop + "' AND S.VARIETY='" + variety + "' AND S.STATUS IN ('INT','Y')";
                if (orgnCode != null)
                {
                    strQueryWhere = " AND H.CURR_ORGN_CODE='" + orgnCode + "' ";
                }

                strQueryOrderBy = " ORDER BY S.CURR_ORGN_CODE,S.STATUS,S.GRADE,S.GPIL_BALE_NUMBER";
            }
            else if (exportData == "Packed Stock")
            {
                //strQuery = "SELECT CURR_ORGN_CODE,GRADE,GPIL_BALE_NUMBER,STATUS FROM GPIL_STOCK (NOLOCK) WHERE LEN(GPIL_BALE_NUMBER)=31 AND STATUS IN ('INT','Y') AND GPIL_BALE_NUMBER NOT IN (SELECT LOT_NUM FROM BC_ERP_IOT_DTL (NOLOCK))";
                strQuery = "SELECT S.CURR_ORGN_CODE AS LOCATIONCODE,S.VARIETY,S.CROP,S.GRADE,I.ITEM_DESC AS DESCRIPTION,S.GPIL_BALE_NUMBER,";
                strQuery = strQuery + " ISNULL(S.MARKED_WT,'0') as QTY,S.STATUS FROM GPIL_STOCK(NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I WHERE S.GRADE=I.ITEM_CODE AND";
                strQuery = strQuery + " ((LEN(S.GPIL_BALE_NUMBER)= 31 AND GRADE LIKE 'LF%') OR (GRADE LIKE 'LE%' AND LEN(S.GPIL_BALE_NUMBER)=13) OR (GRADE LIKE 'LE%' AND LEN(S.GPIL_BALE_NUMBER)=14)) AND";
                strQuery = strQuery + " S.STATUS IN ('INT','Y') AND S.GPIL_BALE_NUMBER NOT IN (SELECT LOT_NUM FROM BC_ERP_IOT_DTL (NOLOCK))";

                //if (orgnCode != null)
                //{
                //    strQueryWhere = " AND S.CURR_ORGN_CODE='" + orgnCode + "' ";
                //}
                //if (crop != null)
                //{
                //    strQueryWhere = strQueryWhere + " AND CROP='" + crop + "' ";
                //}

                //if (variety != null)
                //{
                //    strQueryWhere = strQueryWhere + " AND VARIETY='" + variety + "' ";
                //}

                strQueryOrderBy = " ORDER BY    S.CURR_ORGN_CODE,S.VARIETY,S.CROP,S.GRADE,I.ITEM_DESC,S.GPIL_BALE_NUMBER,S.MARKED_WT,S.STATUS";
            }
            else if (exportData == "GST Download")
            {

                // btnClear.Text = "IN";
                strQuery = "SELECT LOC_CODE,SHIPMENT_NO,SNO,CREATEDDATE,DC_NO,QTY,VALUE FROM GPIL_GST_INVOICE_NO WHERE CREATEDDATE  BETWEEN CONVERT(VARCHAR,'" + fromDate + " 00:00:00',105) AND CONVERT(VARCHAR,'" + toDate + " 23:59:59',105)";
                strQueryWhere = "";
                strQueryOrderBy = "";


                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Inside Condition');", true);

            }
            //strQuery = strQuery + strQueryWhere + strQueryOrderBy;
            strQuery = strQuery + strQueryWhere + strQueryOrderBy;  // prasad comment these line

            dt = cMgt.GetQueryResult(strQuery);





            return dt;

        }


        [NonAction]
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NO");
            dt.Columns.Add("NAME");

            dt.Rows.Add(1, "Jeswanth");
            dt.Rows.Add(2, "Jagan");

            return dt;

        }
    }
}