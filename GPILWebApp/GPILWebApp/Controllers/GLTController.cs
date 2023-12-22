using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using GPILWebApp.ViewModel.GLT;
using GPILWebApp.ViewModel;
using GPI;

namespace GPILWebApp.Controllers
{
    public class GLTController : Controller
    {


        private GREEN_LEAF_TRACEABILITYEntities _data;
        public GLTController()
        {
            _data = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _data.Dispose();
        }
        
        /// <summary>
        /// THRESHING PROCESS CHART
        /// </summary>
        /// <returns></returns>

        public ActionResult ThreshingProcessChartIndex()
        {
            return View();
        }
        
        /// <summary>
        /// THRESHING BLEND RATIO
        /// </summary>
        /// <returns></returns>

        public ActionResult ThreshingBlendRatioIndex()
        {
            return View();
        }
        
        /// <summary>
        /// THRESHING BATCH LIVE BLEND RATIO'S
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreshingBatchLiveBlendRatioIndex()
        {
            return View();
        }
        
        /// <summary>
        /// Threshing Hourly-Base Blend Ratio's Report
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreshingHourBasedBlendIndex()
        {
            return View();
        }

        // GET: GLT
        /// <summary>
        /// THRESHING BATCH DETAILS
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreshingBatchIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ThreshingBatchIssueDetails(string strBatchNumber)
        {
            GLTManagement GLTMgt = new GLTManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='IPB'";
            dtclstr = GLTMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult ThreshingBatchOutTurnProductDetails(string strBatchNumber)
        {
            GLTManagement GLTMgt = new GLTManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,CASE_NUMBER,PACKED_GRADE,NET_WT,GROSS_WT,TARE_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_2 WHERE BATCH_NO='" + strBatchNumber + "'";
            dtclstr = GLTMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult ThreshingBatchOutTurnByProductDetails(string strBatchNumber)
        {
            GLTManagement GLTMgt = new GLTManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='OPB'";
            dtclstr = GLTMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// Threshing Batch Temp Details
        /// </summary>
        /// <returns></returns>

        public ActionResult ThreshingBatchTempIndex()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ThreshingBatchIssueTempDetails(string strBatchNumber)
        {

            GLTManagement GLTMgt = new GLTManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_1_TEMP WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='IPB'";
            dtclstr = GLTMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult ThreshingBatchOutTurnProductTempDetails(string strBatchNumber)
        {
            GLTManagement GLTMgt = new GLTManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,CASE_NUMBER,PACKED_GRADE,NET_WT,GROSS_WT,TARE_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_2_TEMP WHERE BATCH_NO='" + strBatchNumber + "'";
            dtclstr = GLTMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult ThreshingBatchOutTurnByProductTempDetails(string strBatchNumber)
        {
            GLTManagement GLTMgt = new GLTManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT BATCH_NO,GPIL_BALE_NUMBER,BALE_TYPE,PRODUCT_TYPE,GRADE,MARKED_WT,ASCERTAIN_WT,SUBINVENTORY_CODE FROM GPIL_THRESH_RECON_DTLS_1_TEMP WHERE BATCH_NO='" + strBatchNumber + "' AND BALE_TYPE='OPB'";
            dtclstr = GLTMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// PACKED CASE TRACK
        /// </summary>
        /// <returns></returns>
        public ActionResult PackedCaseTrackIndex()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _data.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _data.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();
        }

        [HttpGet]

        public ActionResult GetPacedCaseGrade(string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = String.Empty;
            GltManagement gltMgt = new GltManagement();
            try
            {

                strsql = "SELECT DISTINCT PACKED_GRADE,I.ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H,GPIL_ITEM_MASTER (NOLOCK) I WHERE H.BATCH_NO=D.BATCH_NO AND I.ITEM_CODE=D.PACKED_GRADE AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' ORDER BY I.ITEM_DESC";
                ds1 = gltMgt.GetQueryResult(strsql);
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

        public ActionResult GetThreshingBatch(string strCrop, string strVariety, string strGrade)
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            GltManagement gltMgt = new GltManagement();
            try
            {

                strsql = "SELECT DISTINCT D.BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.PACKED_GRADE='" + strGrade + "' AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' ORDER BY D.BATCH_NO";
                ds1 = gltMgt.GetQueryResult(strsql);
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

        public JsonResult GetPackedCaseTrack(string threshingBatch)
        {

            string strsql;
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            GltManagement gltMgt = new GltManagement();
            try
            {

                string query = "SELECT  ROW_NUMBER() OVER (ORDER BY GPIL_BALE_NUMBER) AS SNO ,GPIL_BALE_NUMBER,[1] AS STEP1,[2] AS STEP2,[3] AS STEP3,[4]  AS STEP4,[5] AS STEP5,[6] AS STEP6,[7] AS STEP7,[8] AS STEP8,[9] AS STEP9,[10] AS STEP10,[11] AS STEP11,[12] AS STEP12 FROM (SELECT ROW_NUMBER() OVER (PARTITION BY GPIL_BALE_NUMBER ORDER BY CREATED_DATE ASC,PROCESS DESC) AS SNO,GPIL_BALE_NUMBER,(PROCESS + '||' + ORGN_CODE + '||' + REF_ID  + '||' +(CASE WHEN PROCESS IN ('CLASSIFICATION','RE-CLASSIFICATION','CROP-TRANSFER ISSUE','CROP-TRANSFER OUTTURN')  THEN (FROM_GRD + ' ~ ' + TO_GRD) ELSE FROM_GRD END)  + '||' +(CASE WHEN FROM_SUBINV=TO_SUBINV THEN FROM_SUBINV ELSE FROM_SUBINV + ' ~ ' + TO_SUBINV END)  + '||' +CONVERT(NVARCHAR(10),ASRTN_WT)  + '||' + CONVERT(NVARCHAR(21),CREATED_DATE,105) + ' ' + CONVERT(NVARCHAR(21),CREATED_DATE,108)) AS DETAILS FROM (SELECT H.ORGN_CODE AS ORGN_CODE,'TAP PURCHASE'  AS PROCESS,H.HEADER_ID AS REF_ID,H.HEADER_ID AS TEMP_REF,D.GPIL_BALE_NUMBER,D.BUYER_GRADE AS FROM_GRD,D.BUYER_GRADE AS TO_GRD,D.NET_WT AS MKD_WT,D.NET_WT AS ASRTN_WT ,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE'  UNION SELECT H.ORGN_CODE AS ORGN_CODE,'FARMER PURCHASE'  AS PROCESS,H.HEADER_ID AS REF_ID,H.HEADER_ID AS TEMP_REF,D.GPIL_BALE_NUMBER,D.BUYER_GRADE AS FROM_GRD,D.BUYER_GRADE AS TO_GRD,D.NET_WT AS MKD_WT,D.NET_WT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='SUNDRY PURCHASE'  UNION SELECT (H.RECEV_ORGN_CODE + ' (' + H.SUPP_CODE + ')') AS ORGN_CODE,H.ATTRIBUTE1  AS PROCESS,H.HEADER_ID AS REF_ID,H.HEADER_ID AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.NET_WEIGHT AS MKD_WT,D.NET_WEIGHT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_SUPP_PURCHS_DTLS (NOLOCK) D,GPIL_SUPP_PURCHS_HDR (NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID UNION SELECT  (H.SENDER_ORGN_CODE + ' - ' + H.RECEIVER_ORGN_CODE) AS ORGN_CODE, (CASE WHEN REDIRECT_STATUS='Y' THEN 'RE-DIRECT SHIPMENT' ELSE 'SHIPMENT' END) AS PROCESS,H.SHIPMENT_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,(D.DISPATCH_WEIGHT)AS ASRTN_WT,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_SHIPMENT_DTLS (NOLOCK) D,GPIL_SHIPMENT_HDR (NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO  UNION SELECT  (H.SENDER_ORGN_CODE + ' - ' + H.RECEIVER_ORGN_CODE) AS ORGN_CODE, 'SALES' AS PROCESS,H.SHIPMENT_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,(D.DISPATCH_WEIGHT)AS ASRTN_WT,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_SO_RESERVATION_DTLS (NOLOCK) D,GPIL_SO_RESERVATION_HDR (NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO UNION SELECT  H.ORGN_CODE AS ORGN_CODE,H.RECIPE_CODE  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE AS FROM_GRD,D.CLASSIFICATION_GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.WEIGHT_BEFORE_CLASSIFY AS ASRTN_WT,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_CLASSIFICATION_DTLS (NOLOCK) D,GPIL_CLASSIFICATION_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO  UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'CROP-TRANSFER ISSUE'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.OLD_BALE_NUMBER,D.OLD_GRADE AS FROM_GRD,D.NEW_GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.MARKED_WT AS ASRTN_WT,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_CROP_TRANS_DTLS (NOLOCK) D,GPIL_CROP_TRANS_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO  UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'CROP-TRANSFER OUTTURN'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.NEW_BALE_NUMBER,D.OLD_GRADE AS FROM_GRD,D.NEW_GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.MARKED_WT AS ASRTN_WT,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_CROP_TRANS_DTLS (NOLOCK) D,GPIL_CROP_TRANS_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO  UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'GRADING-ISSUE'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.ASCERTAIN_WT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='IPB' UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'GRADING-OUTTURN'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.ASCERTAIN_WT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_GRADING_DTLS (NOLOCK) D,GPIL_GRADING_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'THRESHING-ISSUE'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.ASCERTAIN_WT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='IPB' UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'THRESHING-BY-PRODUCT'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.GPIL_BALE_NUMBER,D.GRADE AS FROM_GRD,D.GRADE AS TO_GRD,D.MARKED_WT AS MKD_WT,D.ASCERTAIN_WT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO  AND D.BALE_TYPE='OPB' UNION SELECT  H.ORGN_CODE AS ORGN_CODE,'THRESHING-PRODUCT'  AS PROCESS,H.BATCH_NO AS REF_ID,H.TEMP_REF AS TEMP_REF,D.CASE_NUMBER AS GPIL_BALE_NUMBER,D.PACKED_GRADE AS FROM_GRD,D.PACKED_GRADE AS TO_GRD,D.NET_WT AS MKD_WT,D.NET_WT AS ASRTN_WT,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.CREATED_DATE AS CREATED_DATE,H.STATUS,H.FLAG  FROM GPIL_THRESH_RECON_DTLS_2 (NOLOCK) D,GPIL_THRESH_RECON_HDR (NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO ) T1 WHERE T1.GPIL_BALE_NUMBER IN (SELECT DISTINCT GPIL_BALE_NUMBER FROM GPIL_THRESH_RECON_DTLS_1 (NOLOCK) WHERE BATCH_NO='" + threshingBatch + "' AND BALE_TYPE='IPB') ) AS D PIVOT  (	MAX(DETAILS) FOR SNO IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]) )PIV; ";

                ds1 = gltMgt.GetQueryResult(query);
                ds1.TableName = "Table";
                var data = ds1;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


                //Session["frmPrintid"] = poNumber;


            }
            catch (Exception ex)
            { }
            return Json(ds);
        }



        /// <summary>
        /// THRESHING BATCH COMPLETE
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreshingBatchCompleteIndex()
        {
            return View();
        }

        DataTable dt = new DataTable();
        CommonManagement CMgt = new CommonManagement();

        [HttpGet]
        public ActionResult GetBatchRefNumber()
        {
            string query = "";
            query = "SELECT DISTINCT BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP WHERE STATUS='CC'";
            dt = CMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult GetThreshingBatchComplete(string batchNumber)
        {


            string strQuery = "SELECT H.ORGN_CODE,D.BATCH_NO,D.PRODUCT_TYPE,D.BALES,D.QTY FROM GPIL_THRESH_RECON_HDR_TEMP H,(SELECT BATCH_NO,'ISSUE' AS PRODUCT_TYPE,COUNT(GPIL_BALE_NUMBER) AS BALES,SUM(MARKED_WT) AS QTY FROM GPIL_THRESH_RECON_DTLS_1_TEMP  WHERE BATCH_NO='" + batchNumber + "' AND BALE_TYPE='IPB' GROUP BY BATCH_NO  UNION  SELECT BATCH_NO,PRODUCT_TYPE ,COUNT(GPIL_BALE_NUMBER) AS BALES,SUM(MARKED_WT) AS QTY FROM GPIL_THRESH_RECON_DTLS_1_TEMP  WHERE BATCH_NO='" + batchNumber + "' AND BALE_TYPE='OPB' GROUP BY BATCH_NO,PRODUCT_TYPE  UNION  SELECT BATCH_NO,'PROD' AS PRODUCT_TYPE ,COUNT(CASE_NUMBER) AS BALES,SUM(NET_WT) AS QTY FROM GPIL_THRESH_RECON_DTLS_2_TEMP  WHERE BATCH_NO='" + batchNumber + "' GROUP BY BATCH_NO) AS D WHERE H.BATCH_NO=D.BATCH_NO AND H.BATCH_NO='" + batchNumber + "'";

            dt = CMgt.GetQueryResult(strQuery);

            string json = JsonConvert.SerializeObject(dt);
            return Json(json, JsonRequestBehavior.AllowGet);
            //return Json(new { result = "Redirect", url = Url.Action("FarmerPurchasePendingBalesIndex", "LDD") });

        }


        [HttpGet]

        public ActionResult GetThreshingBatchCompleteButton(string batchNumber)
        {
            string lblMessage = string.Empty;
            if (batchNumber.Length == 0)
            {

                lblMessage = "Error: Please select the bacth ref. number for completion...";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Warning!", "alert('Please select the bacth ref. number for completion...');", true);
                //return;
            }
            else if (batchNumber.Trim().Length == 0)
            {
                lblMessage = "Error: Please select the batch ref. number for completion...";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Warning!", "alert('Please select the batch ref. number for completion...');", true);
                //return;
            }

            

            try
            {

                if(batchNumber.Length != 0)
                {
                    string strQuery = "";

                    strQuery = "UPDATE GPIL_STOCK SET PROCESS_STATUS='N',STATUS='N' WHERE GPIL_BALE_NUMBER IN (SELECT GPIL_BALE_NUMBER FROM GPIL_THRESH_RECON_DTLS_1_TEMP D,GPIL_THRESH_RECON_HDR_TEMP H WHERE D.BATCH_NO=H.BATCH_NO AND D.BATCH_NO='" + batchNumber + "' AND BALE_TYPE='IPB' AND H.STATUS='CC')";
                    CMgt.UpdateUsingExecuteNonQuery(strQuery);

                    strQuery = "UPDATE GPIL_STOCK SET PROCESS_STATUS='N',STATUS='Y' WHERE GPIL_BALE_NUMBER IN (SELECT GPIL_BALE_NUMBER FROM GPIL_THRESH_RECON_DTLS_1_TEMP D,GPIL_THRESH_RECON_HDR_TEMP H WHERE D.BATCH_NO=H.BATCH_NO AND D.BATCH_NO='" + batchNumber + "' AND BALE_TYPE='OPB' AND H.STATUS='CC')";
                    CMgt.UpdateUsingExecuteNonQuery(strQuery);

                    strQuery = "UPDATE GPIL_STOCK SET PROCESS_STATUS='N',STATUS='Y' WHERE GPIL_BALE_NUMBER IN (SELECT CASE_NUMBER FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_THRESH_RECON_HDR_TEMP H WHERE D.BATCH_NO=H.BATCH_NO AND D.BATCH_NO='" + batchNumber + "' AND H.STATUS='CC')";
                    CMgt.UpdateUsingExecuteNonQuery(strQuery);

                    strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_1_TEMP SET HEADER_STATUS='N' WHERE BATCH_NO IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP WHERE BATCH_NO='" + batchNumber + "' AND STATUS='CC')";
                    CMgt.UpdateUsingExecuteNonQuery(strQuery);

                    strQuery = "UPDATE GPIL_THRESH_RECON_DTLS_2_TEMP SET HEADER_STATUS='N' WHERE BATCH_NO IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP WHERE BATCH_NO='" + batchNumber + "' AND STATUS='CC')";
                    CMgt.UpdateUsingExecuteNonQuery(strQuery);

                    strQuery = "UPDATE GPIL_THRESH_RECON_HDR_TEMP SET STATUS='C' WHERE BATCH_NO='" + batchNumber + "' AND STATUS='CC'";
                    CMgt.UpdateUsingExecuteNonQuery(strQuery);


                    //SqlCommand cmdSP = new SqlCommand();
                    //cmdSP.Connection = ClsConnection.SqlCon;
                    //ClsConnection.connectDB();
                    //cmdSP.Transaction = trx;
                    //cmdSP.CommandType = CommandType.StoredProcedure;
                    //cmdSP.CommandText = "THRESHING_BATCH";
                    //cmdSP.Parameters.Add(new SqlParameter("@tempbatch", SqlDbType.VarChar, 50));
                    //cmdSP.Parameters.Add("@sts", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    //cmdSP.Parameters["@tempbatch"].Value = lblBatchRefNumber.Text.Trim();
                    //cmdSP.ExecuteNonQuery();

                    //string retVal = cmdSP.Parameters["@sts"].Value.ToString();


                    string retVal = "Batch No : " + batchNumber + " has been Completed";
                    lblMessage = "Success: Batch No : " + batchNumber + " has been Completed";

                    //trx.Commit();
                    //trx.Dispose();



                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Batch Complete", "alert('" + Convert.ToString(retVal.Replace("True;", "")) + "');", true);

                    //clear();
                    GetBatchRefNumber();
                }
                
                string data = String.Empty, jsonn = String.Empty;
                JsonResult jsonResult;
                if (lblMessage.Length > 0)
                {
                    data = lblMessage;
                    jsonn = JsonConvert.SerializeObject(data);
                    jsonResult = Json(jsonn.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Success";
                    jsonn = JsonConvert.SerializeObject(data);
                    jsonResult = Json(jsonn.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }


            }
            catch (Exception ex)
            {

                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                //trx.Rollback();
                //trx.Dispose();

            }
            finally
            {
                //trx.Dispose();
                //ClsConnection.closeDB();
            }
            //string json = JsonConvert.SerializeObject(dt);
            //return Json(json, JsonRequestBehavior.AllowGet);
            //return Json(new { result = "Redirect", url = Url.Action("FarmerPurchasePendingBalesIndex", "LDD") });

        }
    }
}