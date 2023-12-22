using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class VreificationNewController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;


        public VreificationNewController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: VreificationNew
        public ActionResult SupplierPurchaseVerificationIndex()
        {
            //var SupplierCode = _context.GPIL_SUPP_PURCHS_HDR
            //     .Select(i => i.LP4_NUMBER)
            //     .Distinct();
            ////var SupplierCode =(from i in _context.GPIL_SUPP_PURCHS_HDR select new { i.LP4_NUMBER}).Distinct();
            //ViewBag.GPIL_SUPP_PURCHS_HDR = new SelectList(SupplierCode);
            ViewBag.GPIL_SUPP_PURCHS_HDR = (from s in _context.GPIL_SUPP_PURCHS_HDR where s.STATUS == "P"  select new { s.SUPP_CODE }).Distinct();
            
            return View();
        }

        [HttpGet]
        
        //GET Supplier Code while selecting LP4 number
        public ActionResult SupplierCode(string suppCode)
        {
            string json = "";
            try
            {
                
                var result = (from s in _context.GPIL_SUPP_PURCHS_HDR where s.LP4_NUMBER == int.Parse(suppCode)
                              && s.STATUS == "P" group s by new { s.SUPP_CODE, s.SITE_NAME, } 
                into t orderby t.Key.SUPP_CODE select new { t.Key.SUPP_CODE, SUPPLIER = t.Key.SUPP_CODE + " - " + t.Key.SITE_NAME }).Distinct().ToString();
             
                json = JsonConvert.SerializeObject(result.ToArray());
            }
            catch (Exception ex)
            { }

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Connect Ora
        /// </summary>
        /// <returns></returns>
        public ActionResult ConnectOraIndex()
        {
            ViewBag.GPIL_TAP_FARM_PURCHS_HDR = (from s in _context.GPIL_TAP_FARM_PURCHS_HDR where s.STATUS == "P" && s.PURCHASE_TYPE == "TAP PURCHASE" select new { s.PURCH_DOC_NO }).Distinct();

            return View();
        }

        /// <summary>
        /// BALE TRACK WITH UPDATION
        /// </summary>
        /// <returns></returns>
        public ActionResult BaleTrackWithUpdationIndex()
        {
            

            return View();
        }
        DataTable dtclstr = new DataTable();

        [HttpGet]
        public ActionResult GradingBatchIssueDetails(string strCaseNumber)
        {
            PPDManagement ppdMgt = new PPDManagement();
            DataTable dt = new DataTable();
            string query = "";
            query = "SELECT D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN H.PURCHASE_TYPE='TAP PURCHASE' THEN 'TAP PURCHASE' ELSE CASE WHEN H.PURCHASE_TYPE='SUNDRY PURCHASE' THEN 'FARMER PURCHASE' END END PROCESS,H.HEADER_ID AS REPORT_REF,H.HEADER_ID AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.BUYER_GRADE AS FROM_GRADE,D.BUYER_GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV, D.NET_WT AS MARKED_WT,D.NET_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dtclstr = ppdMgt.GetQueryResult(query);

            query = "SELECT D.GPIL_BALE_NUMBER,(H.RECEV_ORGN_CODE + ' (' + H.SUPP_CODE + ')') AS ORGN_CODE,H.ATTRIBUTE1  AS PROCESS,H.HEADER_ID AS REPORT_REF,H.HEADER_ID AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS, D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.NET_WEIGHT AS MARKED_WT,D.NET_WEIGHT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_SUPP_PURCHS_DTLS(NOLOCK) D,GPIL_SUPP_PURCHS_HDR(NOLOCK) H WHERE H.HEADER_ID=D.HEADER_ID AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT  D.GPIL_BALE_NUMBER,(H.SENDER_ORGN_CODE + ' - ' + H.RECEIVER_ORGN_CODE) AS ORGN_CODE, (CASE WHEN REDIRECT_STATUS='Y' THEN 'RE-DIRECT SHIPMENT' ELSE 'SHIPMENT' END) AS PROCESS,H.SHIPMENT_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.DISPATCH_WEIGHT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT  D.GPIL_BALE_NUMBER,(H.SENDER_ORGN_CODE + ' - ' + H.RECEIVER_ORGN_CODE) AS ORGN_CODE, 'SALES' AS PROCESS,H.SHIPMENT_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.DISPATCH_WEIGHT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_SO_RESERVATION_DTLS(NOLOCK) D,GPIL_SO_RESERVATION_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT  D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,H.RECIPE_CODE  AS PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.ISSUED_GRADE AS FROM_GRADE,D.CLASSIFICATION_GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.WEIGHT_BEFORE_CLASSIFY AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_CLASSIFICATION_DTLS(NOLOCK) D,GPIL_CLASSIFICATION_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT CASE WHEN D.OLD_BALE_NUMBER='" + strCaseNumber.Trim() + "' THEN D.OLD_BALE_NUMBER ELSE CASE WHEN D.NEW_BALE_NUMBER='" + strCaseNumber.Trim() + "' THEN D.NEW_BALE_NUMBER END END GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN D.OLD_BALE_NUMBER='" + strCaseNumber.Trim() + "' THEN 'CROP-TRANSFER ISSUE' ELSE CASE WHEN D.NEW_BALE_NUMBER='" + strCaseNumber.Trim() + "' THEN 'CROP-TRANSFER OUTTURN' END END PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.OLD_GRADE AS FROM_GRADE,D.NEW_GRADE AS TO_GRADE,D.FROM_SUBINVENTORY_CODE AS FROM_SUBINV,D.TO_SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.MARKED_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_CROP_TRANS_DTLS(NOLOCK) D,GPIL_CROP_TRANS_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND (D.OLD_BALE_NUMBER='" + strCaseNumber.Trim() + "' or D.NEW_BALE_NUMBER='" + strCaseNumber.Trim() + "') ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT  D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN D.BALE_TYPE='IPB' THEN 'GRADING-ISSUE' ELSE CASE WHEN D.BALE_TYPE='OPB' THEN 'GRADING-OUTTURN' END END PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.ASCERTAIN_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_GRADING_DTLS(NOLOCK) D,GPIL_GRADING_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT D.GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,CASE WHEN D.BALE_TYPE='IPB' THEN 'THRESHING-ISSUE' ELSE CASE WHEN D.BALE_TYPE='OPB' THEN 'THRESHING-BY-PRODUCT' END END PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS,H.FLAG AS ERP_STATUS,D.GRADE AS FROM_GRADE,D.GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.MARKED_WT AS MARKED_WT,D.ASCERTAIN_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_THRESH_RECON_DTLS_1(NOLOCK) D,GPIL_THRESH_RECON_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }
            query = "SELECT D.CASE_NUMBER AS GPIL_BALE_NUMBER,H.ORGN_CODE AS ORGN_CODE,'THRESHING-PRODUCT'  AS PROCESS,H.BATCH_NO AS REPORT_REF,H.TEMP_REF AS USER_REF,H.STATUS AS SQL_STATUS ,H.FLAG AS ERP_STATUS,D.PACKED_GRADE AS FROM_GRADE,D.PACKED_GRADE AS TO_GRADE,D.SUBINVENTORY_CODE AS FROM_SUBINV,D.SUBINVENTORY_CODE AS TO_SUBINV,D.NET_WT AS MARKED_WT,D.NET_WT AS ASCERTAIN_WT,D.CREATED_DATE AS CREATED_DATE FROM GPIL_THRESH_RECON_DTLS_2(NOLOCK) D,GPIL_THRESH_RECON_HDR(NOLOCK) H WHERE H.BATCH_NO=D.BATCH_NO AND D.CASE_NUMBER='" + strCaseNumber.Trim() + "' ORDER BY D.CREATED_DATE";
            dt = ppdMgt.GetQueryResult(query);
            if (dt.Rows.Count > 0)
            {
                dtclstr.Merge(dt);
            }


            if (dtclstr.Rows.Count > 0)
            {
                dtclstr.DefaultView.Sort = "CREATED_DATE";
                //GridViewSample.DataSource = ds.Tables[0];
                //GridViewSample.DataBind();
                //GridViewSample.EditIndex = -1;
                //data.Dispose();
            }


            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }



        
    }
}


