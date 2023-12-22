using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class VerificationController : Controller
    {

        RDLCReport rdlcReport = new RDLCReport();
        // GET: LD
        private GREEN_LEAF_TRACEABILITYEntities _context;


        public VerificationController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Verification
        /// <summary>
        /// Farmer Purchase Verification
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerPurchaseIndex()
        {
            //from s in test.GPIL_TAP_FARM_PURCHS_HDRs where s.STATUS == "P" && s.PURCHASE_TYPE == "SUNDRY PURCHASE" select new { s.PURCH_DOC_NO }).Distinct();
            ViewBag.GPIL_TAP_FARM_PURCHS_HDR = (from s in _context.GPIL_TAP_FARM_PURCHS_HDR where s.STATUS == "P" && s.PURCHASE_TYPE == "SUNDRY PURCHASE" select new { s.PURCH_DOC_NO }).Distinct();

            return View();
        } 

        [HttpPost]
        public JsonResult UpdateFarmerPurchase(FARM_PURCHS_DTLS gPIL_TAP_FARM_PURCHS_DTLS)
        {
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                VerificationManagement ldMgt = new VerificationManagement();
                string HEADER_ID = gPIL_TAP_FARM_PURCHS_DTLS.HEADER_ID;
                string GPIL_BALE_NUMBER = gPIL_TAP_FARM_PURCHS_DTLS.GPIL_BALE_NUMBER;
                string TB_LOT_NO = (gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO;
                string orgcd = ((string)Session["orgnCode"] == null) ? "" : (string)Session["orgnCode"];
                string FARMER_CODE = (gPIL_TAP_FARM_PURCHS_DTLS.FARMER_CODE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.FARMER_CODE;
                string BUYER_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE;
                string CLASS_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.CLASS_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.CLASS_GRADE;

                double NET_WT = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.NET_WT);
                double RATE = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.RATE);
                string rejetype = gPIL_TAP_FARM_PURCHS_DTLS.REJE_TYPE;
                string sttrsqrt, tablename = string.Empty, temptablename = string.Empty;
                string sttrsqrtstk;
                double dFreight = 0;
                string sAttribute4 = string.Empty;
                sAttribute4 = RATE.ToString();
                string sCrop = GPIL_BALE_NUMBER.Trim().Substring(0, 2);
                string strsql = "select ITEM_CODE from GPIL_ITEM_MASTER where ITEM_CODE='" + BUYER_GRADE.ToString() + "'";
                DataTable ds1 = new DataTable();
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count == 0)
                {
                    data = "Error: BuyerGrade is not in Item Master..!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //ViewBag.ErrorMessage = "BuyerGrade is not in Item Master..!";
                    //return View("FarmerPurchaseBaleWiseIndex", ViewBag);
                }
                ds1.Clear();
                strsql = "select ITEM_CODE from GPIL_ITEM_MASTER where ITEM_CODE='" + CLASS_GRADE.ToString() + "'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count == 0)
                {
                    data = "Error: ClassificationGrade is not in Item Master..!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //ViewBag.ErrorMessage = "ClassificationGrade is not in Item Master..!";
                    //return View("FarmerPurchaseBaleWiseIndex", ViewBag);
                }
                ds1.Clear();
                strsql = "select ISNULL(FREIGHT_CHARGE,'0')FREIGHT_CHARGE from GPIL_FARMER_FREIGHT_CHARGE_MASTER where CROP='" + sCrop + "' and VILLAGE_CODE";
                strsql = strsql + " IN(select VILLAGE_CODE from GPIL_FARMER_MASTER where FARM_CODE='" + FARMER_CODE.ToString() + "') and ORGN_CODE='" + orgcd + "'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    dFreight = Convert.ToDouble(RATE) + Convert.ToDouble(ds1.Rows[0][0].ToString());
                    RATE = dFreight;
                }
                ds1.Clear();
                double value = Convert.ToDouble(NET_WT) * Convert.ToDouble(RATE);
                if (rejetype != "NONE")
                {
                    sttrsqrt = "update GPIL_TAP_FARM_PURCHS_DTLS set REJE_TYPE='" + rejetype.ToString() + "',REJE_STATUS='RJ', VALUE='" + value + "',TB_LOT_NO='" + TB_LOT_NO + "',FARMER_CODE='" + FARMER_CODE + "',ATTRIBUTE2='" + CLASS_GRADE + "',BUYER_GRADE='" + BUYER_GRADE + "',NET_WT='" + NET_WT + "',RATE='" + RATE + "',STATUS='N',ATTRIBUTE4='" + sAttribute4 + "' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                    sttrsqrtstk = "update GPIL_STOCK set TB_LOT_NO='" + TB_LOT_NO + "',GRADE='" + CLASS_GRADE + "',TBGR_NO='" + BUYER_GRADE + "',BUYER_GRADE='" + BUYER_GRADE + "',MARKED_WT='" + NET_WT + "',CURR_WT='" + NET_WT + "',PRICE='" + RATE + "',STATUS='N' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                }
                else
                {
                    sttrsqrt = "update GPIL_TAP_FARM_PURCHS_DTLS set REJE_TYPE=NULL,REJE_STATUS='OK', VALUE='" + value + "',TB_LOT_NO='" + TB_LOT_NO + "',FARMER_CODE='" + FARMER_CODE + "',ATTRIBUTE2='" + CLASS_GRADE + "',BUYER_GRADE='" + BUYER_GRADE + "',NET_WT='" + NET_WT + "',RATE='" + RATE + "',STATUS='Y',ATTRIBUTE4='" + sAttribute4 + "' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                    sttrsqrtstk = "update GPIL_STOCK set TB_LOT_NO='" + TB_LOT_NO + "',GRADE='" + CLASS_GRADE + "',TBGR_NO='" + BUYER_GRADE + "',BUYER_GRADE='" + BUYER_GRADE + "',MARKED_WT='" + NET_WT + "',CURR_WT='" + NET_WT + "',PRICE='" + RATE + "',STATUS='Y' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                }
                ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                ldMgt.UpdateUsingExecuteNonQuery(sttrsqrtstk);
                sttrsqrt = "select PROCESS_NAME,PROCESS_REF_ID from GPIL_PROCESS_ORDER_CAPTURE WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";

                ds1 = ldMgt.GetQueryResult(sttrsqrt);
                for (int s = 0; s < ds1.Rows.Count; s++)
                {
                    if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "DISPATCH")
                    {
                        tablename = "GPIL_SHIPMENT_DTLS";
                        temptablename = "GPIL_SHIPMENT_DTLS_TEMP";
                    }
                    if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "CLASSIFICATION" || ds1.Rows[s]["PROCESS_NAME"].ToString() == "GRADETRANSFER")
                    {
                        tablename = "GPIL_CLASSIFICATION_DTLS";
                        temptablename = "GPIL_CLASSIFICATION_DTLS_TEMP";
                    }
                    if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "CROPTRANSFER")
                    {
                        tablename = "GPIL_CROP_TRANS_DTLS";
                        temptablename = "GPIL_CROP_TRANS_DTLS_TEMP";
                    }
                    if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "GRADING")
                    {
                        tablename = "GPIL_GRADING_DTLS";
                        temptablename = "GPIL_GRADING_DTLS_TEMP";
                    }
                    if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "THRESHING")
                    {
                        tablename = "GPIL_THRESH_RECON_DTLS_1";
                        temptablename = "GPIL_THRESH_RECON_DTLS_1_TEMP";
                    }
                    if (ds1.Rows[s]["PROCESS_NAME"].ToString() != "TAPPURCHASE")
                    {
                        sttrsqrt = "update " + tablename + " set MARKED_WT='" + Convert.ToDouble(NET_WT) + "' WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                        sttrsqrt = "update " + temptablename + " set MARKED_WT='" + Convert.ToDouble(NET_WT) + "' WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                        ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                    }
                }
                data = "Success: The Value Inserted Successfully!!";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
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
            string sttrsqrt;
            VerificationManagement ldMgt = new VerificationManagement();
            sttrsqrt = "delete FROM GPIL_TAP_FARM_PURCHS_DTLS where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            sttrsqrt = "delete FROM gpil_stock where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            return new EmptyResult();
        }
        MasterManagement MstrMgt = new MasterManagement();
        
        // Verified button Event
        [HttpPost] 
        public JsonResult FarmerPurchaseVerified(string poNumber)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            List<string> lstQry = new List<string>();
            List<string> lstQry1 = new List<string>();
            List<string> lstQry2 = new List<string>();
            List<string> lstQry3 = new List<string>();
            VerificationManagement ldMgt = new VerificationManagement();
            DataTable dtLoanTansaction = new DataTable();
            string strLoanQuery = "SELECT H.CROP,H.VARIETY,H.PURCH_DOC_NO,D.FARMER_CODE,H.ORGN_CODE,D.HEADER_ID,F.BALANCE_AMOUNT AS LOAN_AMOUNT,ROUND(SUM(D.NET_WT*D.RATE),2) AS PURCHASE_AMT,(CASE WHEN (F.BALANCE_AMOUNT - ROUND(SUM(D.NET_WT*D.RATE),2)) > 0 THEN (F.BALANCE_AMOUNT - ROUND(SUM(D.NET_WT*D.RATE),2)) ELSE '0' END) AS BALANCE_AMOUNT FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_FARMER_CROP_HISTORY F WHERE H.HEADER_ID=D.HEADER_ID  AND H.CROP=F.CROP AND H.VARIETY=F.VARIETY AND H.PURCH_DOC_NO='" + poNumber.ToString() + "' AND D.FARMER_CODE=F.FARM_CODE AND D.REJE_STATUS='OK' and H.PURCHASE_TYPE='SUNDRY PURCHASE'  GROUP BY D.FARMER_CODE,H.ORGN_CODE,D.HEADER_ID,F.BALANCE_AMOUNT,H.PURCH_DOC_NO,H.CROP,H.VARIETY ORDER BY D.FARMER_CODE ";

            dtLoanTansaction = ldMgt.GetQueryResult(strLoanQuery);
            DataTable dtClassificationDetails = new DataTable();

            string strClassificationDetails = "SELECT ROW_NUMBER() OVER(ORDER BY GPIL_BALE_NUMBER) AS SNO,D.HEADER_ID AS BATCH_NO,(D.HEADER_ID+D.GPIL_BALE_NUMBER) AS DETAIL_ID,D.GPIL_BALE_NUMBER,D.BUYER_GRADE AS ISSUED_GRADE,D.ATTRIBUTE2 AS CLASSIFICATION_GRADE,D.NET_WT AS MARKED_WT,D.NET_WT AS WEIGHT_AFTER_CLASSIFICATION ,D.NET_WT AS WEIGHT_BEFORE_CLASSIFY,'FW' AS FROM_SUBINVENTORY_CODE,'CL' AS TO_SUBINVENTORY_CODE,D.CREATED_BY,D.CREATED_DATE,D.LAST_UPDATED_BY,D.LAST_UPDATED_DATE,'Y' AS STATUS,'N' AS HEADER_STATUS,NULL AS FLAG,D.LASTUPDATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCH_DOC_NO='" + poNumber.ToString() + "' AND D.REJE_STATUS='OK' and H.PURCHASE_TYPE='SUNDRY PURCHASE'";

            dtClassificationDetails = ldMgt.GetQueryResult(strClassificationDetails);

            string strBatchNo = "";
            string strLnQuery = "";
            string strBalanceUpdate = "";
            string strBalanceHistoryUpdate = "";
            bool b = false;
            for (int i = 0; i < dtLoanTansaction.Rows.Count; i++)
            {
                //string strLnQuery = "INSERT INTO GPIL_FARMER_PURCHASE_LOAN_TRANSACTION(TRAN_ID,FARM_CODE,ORGN_CODE,PURCH_REF,LOAN_AMOUNT,PURCH_AMOUNT,BALANCE_AMOUNT,STATUS,CREATED_BY,CREATED_DATE) VALUES('" + DateTime.Now.ToString("yyyyMMddHHmmss") + i.ToString() + "','" + dtLoanTansaction.Rows[i]["FARMER_CODE"].ToString() + "','" + dtLoanTansaction.Rows[i]["ORGN_CODE"].ToString() + "','" + dtLoanTansaction.Rows[i]["HEADER_ID"].ToString() + "','" + dtLoanTansaction.Rows[i]["LOAN_AMOUNT"].ToString() + "','" + dtLoanTansaction.Rows[i]["PURCHASE_AMT"].ToString() + "','" + dtLoanTansaction.Rows[i]["BALANCE_AMOUNT"].ToString() + "','Y','5655',GETDATE()) ";
                strLnQuery = "INSERT INTO GPIL_FARMER_PURCHASE_TRANSACTIONS(TRAN_ID,CROP,VARIETY,FARM_CODE,ORGN_CODE,PURCH_REF,LOAN_AMOUNT,PURCH_AMOUNT,BALANCE_AMOUNT,STATUS,CREATED_BY,CREATED_DATE) VALUES('" + dtLoanTansaction.Rows[i]["ORGN_CODE"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + i.ToString() + "','" + dtLoanTansaction.Rows[i]["CROP"].ToString() + "','" + dtLoanTansaction.Rows[i]["VARIETY"].ToString() + "','" + dtLoanTansaction.Rows[i]["FARMER_CODE"].ToString() + "','" + dtLoanTansaction.Rows[i]["ORGN_CODE"].ToString() + "','" + dtLoanTansaction.Rows[i]["HEADER_ID"].ToString() + "','" + dtLoanTansaction.Rows[i]["LOAN_AMOUNT"].ToString() + "','" + dtLoanTansaction.Rows[i]["PURCHASE_AMT"].ToString() + "','" + dtLoanTansaction.Rows[i]["BALANCE_AMOUNT"].ToString() + "','Y','5655',GETDATE()) ";
                lstQry.Add(strLnQuery);
                strBalanceUpdate = "UPDATE GPIL_FARMER_MASTER SET LOAN_AMOUNT='" + dtLoanTansaction.Rows[i]["BALANCE_AMOUNT"].ToString() + "' WHERE FARM_CODE='" + dtLoanTansaction.Rows[i]["FARMER_CODE"].ToString() + "'";
                lstQry.Add(strBalanceUpdate);
                strBalanceHistoryUpdate = "UPDATE GPIL_FARMER_CROP_HISTORY SET BALANCE_AMOUNT='" + dtLoanTansaction.Rows[i]["BALANCE_AMOUNT"].ToString() + "' WHERE FARM_CODE='" + dtLoanTansaction.Rows[i]["FARMER_CODE"].ToString() + "' AND CROP='" + dtLoanTansaction.Rows[i]["CROP"].ToString() + "' AND VARIETY='" + dtLoanTansaction.Rows[i]["VARIETY"].ToString() + "' ";
                lstQry.Add(strBalanceHistoryUpdate);
                strBatchNo = dtLoanTansaction.Rows[i]["HEADER_ID"].ToString();
                

            }
           // b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry1);
            //b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry2);
            //b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry3);

            string strClassificationQuery = "";
            for (int j = 0; j < dtClassificationDetails.Rows.Count; j++)
            {
                //strClassificationQuery = "INSERT INTO GPIL_CLASSIFICATION_DTLS_TEMP(BATCH_NO,DETAIL_ID,GPIL_BALE_NUMBER,ISSUED_GRADE,CLASSIFICATION_GRADE,MARKED_WT,WEIGHT_BEFORE_CLASSIFY,WEIGHT_AFTER_CLASSIFICATION,FROM_SUBINVENTORY_CODE,TO_SUBINVENTORY_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,HEADER_STATUS) VALUES ('" + dtClassificationDetails.Rows[j]["BATCH_NO"].ToString() + "','" + dtClassificationDetails.Rows[j]["DETAIL_ID"].ToString() + "','" + dtClassificationDetails.Rows[j]["GPIL_BALE_NUMBER"].ToString() + "','" + dtClassificationDetails.Rows[j]["ISSUED_GRADE"].ToString() + "','" + dtClassificationDetails.Rows[j]["CLASSIFICATION_GRADE"].ToString() + "','" + dtClassificationDetails.Rows[j]["MARKED_WT"].ToString() + "','" + dtClassificationDetails.Rows[j]["WEIGHT_BEFORE_CLASSIFY"].ToString() + "','" + dtClassificationDetails.Rows[j]["WEIGHT_AFTER_CLASSIFICATION"].ToString() + "','FW','CL','" + dtClassificationDetails.Rows[j]["CREATED_BY"].ToString() + "','" + dtClassificationDetails.Rows[j]["CREATED_DATE"].ToString() + "','" + dtClassificationDetails.Rows[j]["LAST_UPDATED_BY"].ToString() + "','" + dtClassificationDetails.Rows[j]["LAST_UPDATED_DATE"].ToString() + "','Y','N')";
                strClassificationQuery = "INSERT INTO GPIL_CLASSIFICATION_DTLS_TEMP(BATCH_NO,DETAIL_ID,GPIL_BALE_NUMBER,ISSUED_GRADE,CLASSIFICATION_GRADE,MARKED_WT,WEIGHT_BEFORE_CLASSIFY,WEIGHT_AFTER_CLASSIFICATION,FROM_SUBINVENTORY_CODE,TO_SUBINVENTORY_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,HEADER_STATUS) VALUES ('" + dtClassificationDetails.Rows[j]["BATCH_NO"].ToString() + "','" + dtClassificationDetails.Rows[j]["DETAIL_ID"].ToString() + "','" + dtClassificationDetails.Rows[j]["GPIL_BALE_NUMBER"].ToString() + "','" + dtClassificationDetails.Rows[j]["ISSUED_GRADE"].ToString() + "','" + dtClassificationDetails.Rows[j]["CLASSIFICATION_GRADE"].ToString() + "','" + dtClassificationDetails.Rows[j]["MARKED_WT"].ToString() + "','" + dtClassificationDetails.Rows[j]["WEIGHT_BEFORE_CLASSIFY"].ToString() + "','" + dtClassificationDetails.Rows[j]["WEIGHT_AFTER_CLASSIFICATION"].ToString() + "','FW','CL','" + dtClassificationDetails.Rows[j]["CREATED_BY"].ToString() + "',getdate(),'" + dtClassificationDetails.Rows[j]["LAST_UPDATED_BY"].ToString() + "',getdate(),'Y','N')";
                lstQry.Add(strClassificationQuery);
                //ldMgt.UpdateUsingExecuteNonQuery(strClassificationQuery);
            }

            if (lstQry.Count > 0)
            {

                b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);

                if (b)
                {
                    // data = "Success: The Value Inserted Successfully!!";
                    List<string> lststr = new List<string>();

                    string strsql = "update GPIL_TAP_FARM_PURCHS_HDR set STATUS='N' where PURCH_DOC_NO='" + poNumber.ToString() + "' ";
                    lststr.Add(strsql);

                    strsql = "update GPIL_CLASSIFICATION_HDR_TEMP set STATUS='C',ATTRIBUTE1='" + dtClassificationDetails.Rows.Count + "' where BATCH_NO='" + strBatchNo + "' ";
                    // b = ldMgt.UpdateUsingExecuteNonQuery(strsql);
                    lststr.Add(strsql);

                    b = MstrMgt.UpdateUsingExecuteNonQueryList(lststr);
                    if (b)
                    {

                        return Json(new { result = "Redirect", url = Url.Action("FarmerPurchaseIndex", "Verification") });
                    }
                    else
                    {
                        data = "Error: Error While Inserting Please Check Data";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    //json = JsonConvert.SerializeObject(data);
                    //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //jsonResult.MaxJsonLength = int.MaxValue;
                    //return jsonResult;
                }
                else
                {
                    data = "Error: Error While Inserting Please Check Data";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json("Error: Error While Inserting Please Check Data", JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                data = "Error: No Record available";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //return Json("Error: Error While Inserting Please Check Data", JsonRequestBehavior.AllowGet);
            }

        }

        // Verify Button Event start
        public JsonResult FarmerPurchaceBW(string orgnCodeList, string poNumberList)
        {
            Session["poNumber"] = poNumberList;
            Session["orgnCode"] = orgnCodeList;

            return Json(new { result = "Redirect", url = Url.Action("FarmerPurchaseBaleWiseIndex", "Verification") });
        }

        public ActionResult FarmerPurchaseBaleWiseIndex()
        {
            ViewBag.Title = "FarmerPurchaseBaleWiseIndex";
            ViewBag.poNumber = (string)Session["poNumber"];
            ViewBag.orgnCode = (string)Session["orgnCode"];

            DataSet ds = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                ds = ldMgt.GetFarmerPurchaseBaleWise(ViewBag.poNumber, ViewBag.orgnCode);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return View(jsonResult);
            }
            catch (Exception ex)
            { }
            return View("FarmerPurchaseBaleWiseIndex");
        }


        /// <summary>
        /// 1St Gird Bind
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        public JsonResult FarmerPurchase(string poNumber)
        {
            DataSet ds = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                ds = ldMgt.GetFarmerPurchase(poNumber);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


            }
            catch (Exception ex)
            { }
            return Json(ds);
        }

        public ActionResult Details(int id)
        {
            return View();
        }
        string lblMessage = string.Empty;
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;

        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        [HttpPost]
        public ActionResult CheckBaleAvailability(string Baledata)
        {
            VerificationManagement ldMgt = new VerificationManagement();
            //string strsql1 = "select * from GPIL_STOCK where GPIL_BALE_NUMBER='"+ Baledata + "'";
            //DataTable dt = new DataTable();

            //dt = ldMgt.GetQueryResult(strsql1);

            //if(dt.Rows.Count >0)
            //{
            //       data = "Error: BaleNumber Already Exist";
            //       json = JsonConvert.SerializeObject(data);
            //       jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //       jsonResult.MaxJsonLength = int.MaxValue;
            //       return jsonResult;
            //}

            //System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_STOCK.Where(x => x.GPIL_BALE_NUMBER == Baledata).SingleOrDefault();
            if (usr != null)
            {

                data = "Error: BaleNumber Already Exist";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //if (Baledata == null)
                //{
                //    return Json(0);
                //}
                //PPDManagement ppdMgt = new PPDManagement();
                //DataTable dtclstr = new DataTable();
                //string query = "";
                ////query = " SELECT SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,";
                ////query = query + " COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_FARMER_MASTER where FARM_CODE='" + Farmerdata + "' ";


                //query = "  SELECT FM.SNO,FM.FARM_CODE,FM.FARM_CATEGORY,FM.FARM_NAME,FM.FARM_FATHER_NAME,FM.VILLAGE_CODE,FM.SOIL_TYPE,";
                //query = query + "FM.FARM_ADDRESS1,FM.FARM_ADDRESS2,FM.FARM_ADDRESS3,FM.FARM_ADDRESS4,FM.FARM_ADDRESS5,FM.FARM_ADDRESS6,";
                //query = query + " FM.COUNTRY,FM.PIN_CODE,FM.TEL_NO,FM.MOBILE_NO,FM.EMAIL_ID,FM.BANK_ACCOUNT_NO,FM.BANK_NAME,FM.BRANCH_NAME,";
                //query = query + "FM.IFSC_CODE,FM.CREATED_BY,FM.CREATED_DATE,FM.LAST_UPDATED_BY,FM.LAST_UPDATED_DATE,FM.STATUS,FM.FLAG,FM.LASTUPDATE,";
                //query = query + "FM.LOAN_AMOUNT,FM.ALERT_FLAG,FM.ALERT_MSG,FM.ATTRIBUTE1,FM.ATTRIBUTE2,FM.ATTRIBUTE3 as A3,FM.ATTRIBUTE4,FM.ATTRIBUTE5,";
                //query = query + "FCH.CROP,FCH.VARIETY,FCH.ATTRIBUTE1 as A1,FCH.ATTRIBUTE2 as A2,FCH.ATTRIBUTE3,FCH.ATTRIBUTE4 as A4,FCH.ATTRIBUTE5 as A5";
                //query = query + " from GPIL_FARMER_MASTER FM Join[dbo].[GPIL_FARMER_CROP_HISTORY]  FCH on FM.FARM_CODE = FCH.FARM_CODE";
                //query = query + " where FM.FARM_CODE = '" + Baledata + "'";



                //dtclstr = ppdMgt.GetQueryResult(query);
                //string json = JsonConvert.SerializeObject(dtclstr);
                //return Json(json, JsonRequestBehavior.AllowGet);
                //return Json(0);
            }
            else
            {
                data = "Success: BaleNumber Already Exist";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            //return Json(0);
        }


        [HttpPost]
        public ActionResult CheckFarmerAvailability(string Farmerdata)
        {

            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_FARMER_MASTER.Where(x => x.FARM_CODE == Farmerdata && x.STATUS=="Y").SingleOrDefault();
            if (usr == null)
            {

                data = "Error: Farmer Code Does Not Exist!!";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //if (Baledata == null)
                //{
                //    return Json(0);
                //}
                //PPDManagement ppdMgt = new PPDManagement();
                //DataTable dtclstr = new DataTable();
                //string query = "";
                ////query = " SELECT SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,";
                ////query = query + " COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_FARMER_MASTER where FARM_CODE='" + Farmerdata + "' ";


                //query = "  SELECT FM.SNO,FM.FARM_CODE,FM.FARM_CATEGORY,FM.FARM_NAME,FM.FARM_FATHER_NAME,FM.VILLAGE_CODE,FM.SOIL_TYPE,";
                //query = query + "FM.FARM_ADDRESS1,FM.FARM_ADDRESS2,FM.FARM_ADDRESS3,FM.FARM_ADDRESS4,FM.FARM_ADDRESS5,FM.FARM_ADDRESS6,";
                //query = query + " FM.COUNTRY,FM.PIN_CODE,FM.TEL_NO,FM.MOBILE_NO,FM.EMAIL_ID,FM.BANK_ACCOUNT_NO,FM.BANK_NAME,FM.BRANCH_NAME,";
                //query = query + "FM.IFSC_CODE,FM.CREATED_BY,FM.CREATED_DATE,FM.LAST_UPDATED_BY,FM.LAST_UPDATED_DATE,FM.STATUS,FM.FLAG,FM.LASTUPDATE,";
                //query = query + "FM.LOAN_AMOUNT,FM.ALERT_FLAG,FM.ALERT_MSG,FM.ATTRIBUTE1,FM.ATTRIBUTE2,FM.ATTRIBUTE3 as A3,FM.ATTRIBUTE4,FM.ATTRIBUTE5,";
                //query = query + "FCH.CROP,FCH.VARIETY,FCH.ATTRIBUTE1 as A1,FCH.ATTRIBUTE2 as A2,FCH.ATTRIBUTE3,FCH.ATTRIBUTE4 as A4,FCH.ATTRIBUTE5 as A5";
                //query = query + " from GPIL_FARMER_MASTER FM Join[dbo].[GPIL_FARMER_CROP_HISTORY]  FCH on FM.FARM_CODE = FCH.FARM_CODE";
                //query = query + " where FM.FARM_CODE = '" + Baledata + "'";



                //dtclstr = ppdMgt.GetQueryResult(query);
                //string json = JsonConvert.SerializeObject(dtclstr);
                //return Json(json, JsonRequestBehavior.AllowGet);
                //return Json(0);
            }
            else
            {
                data = "Success: Farmer Code Already Exist";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            //return Json(0);
        }

        // GET: Verification/Create
        public ActionResult Create(string HEADER_ID)
        {
            ViewBag.HEADER_ID = HEADER_ID;
            Session["HEADER_ID"] = HEADER_ID;
            return View();
        }

        // POST: Verification/Create
        [HttpPost]
        public ActionResult Create(GPIL_TAP_FARM_PURCHS_DTLS gPIL_TAP_FARM_PURCHS_DTLS)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    VerificationManagement ldMgt = new VerificationManagement();
                    string HEADER_ID = gPIL_TAP_FARM_PURCHS_DTLS.HEADER_ID;
                    string GPIL_BALE_NUMBER = gPIL_TAP_FARM_PURCHS_DTLS.GPIL_BALE_NUMBER;
                    string TB_LOT_NO = "";//(gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO;
                    //string TBGR_NO = gPIL_TAP_FARM_PURCHS_DTLS.TBGR_NO;
                    string orgcd = ((string)Session["orgnCode"] == null) ? "" : (string)Session["orgnCode"];
                    string FARMER_CODE = (gPIL_TAP_FARM_PURCHS_DTLS.FARMER_CODE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.FARMER_CODE;
                    string BUYER_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE;
                    string CLASS_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.ATTRIBUTE2 == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.ATTRIBUTE2;

                    double NET_WT = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.NET_WT);
                    double RATE = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.RATE);
                    string rejetype = gPIL_TAP_FARM_PURCHS_DTLS.REJE_TYPE;

                    DataTable dtTB_LOT_NO = new DataTable();
                    string strQry = "select distinct TB_LOT_NO from GPIL_TAP_FARM_PURCHS_DTLS where FARMER_CODE='" + FARMER_CODE.ToString() + "' and convert(varchar, CREATED_DATE,	112)  = convert(varchar, getdate(),	112)";

                    dtTB_LOT_NO = ldMgt.GetQueryResult(strQry);
                    if (dtTB_LOT_NO.Rows.Count > 0) {
                        TB_LOT_NO = dtTB_LOT_NO.Rows[0][0].ToString();
                    }
                    string CREATED_BY = Session["userID"].ToString();
                    DateTime CREATED_DATE = DateTime.Now;
                    string rejests;
                    string status;
                    if (rejetype == "NONE")
                    {
                        rejests = "OK";
                    }
                    else
                    {
                        rejests = "RJ";

                    }
                    if (rejests.Trim() == "RJ")
                    {
                        status = "N";
                    }
                    else
                    {
                        status = "Y";
                    }

                    string strClassQuery = "", strsql="";

                    strClassQuery = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,TB_LOT_NO,TBGR_NO,GRADE,BUYER_GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRICE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                    strClassQuery = strClassQuery + "Values('" + GPIL_BALE_NUMBER.ToString().Trim() + "','FW','" + TB_LOT_NO.ToString().Trim() + "','" + FARMER_CODE.ToString().Trim() + "','" + CLASS_GRADE.ToString().Trim() + "','" + BUYER_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','LOC1','" + orgcd.Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','" + RATE.ToString().Trim() + "','G','N','" + HEADER_ID.ToString() + "','" + status + "','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','FARM','LOC1','" + orgcd + "')";
                    ldMgt.UpdateUsingExecuteNonQuery(strClassQuery);

                    strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[FARMER_CODE],[ATTRIBUTE2],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[CREATED_BY],[CREATED_DATE],[ATTRIBUTE4])";
                    if (rejetype  == "NONE")
                    {
                        strsql = strsql + "Values('" + HEADER_ID.ToString() + "','" + GPIL_BALE_NUMBER.ToString().Trim() + "','" + TB_LOT_NO.ToString().Trim() + "','" + FARMER_CODE.ToString().Trim() + "','" + CLASS_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + RATE.ToString().Trim() + "','" + Convert.ToDouble(NET_WT.ToString().Trim()) * Convert.ToDouble(RATE.ToString().Trim()) + "','" + BUYER_GRADE.ToString().Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','FW','" + rejests + "',NULL,'" + status + "','N','" + Session["userID"].ToString() + "',GETDATE(),'" + RATE.ToString().Trim() + "')";
                    }
                    else
                    {
                        strsql = strsql + "Values('" + HEADER_ID.ToString() + "','" + GPIL_BALE_NUMBER.ToString().Trim() + "','" + TB_LOT_NO.ToString().Trim() + "','" + FARMER_CODE.ToString().Trim() + "','" + CLASS_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + RATE.ToString().Trim() + "','" + Convert.ToDouble(NET_WT.ToString().Trim()) * Convert.ToDouble(RATE.ToString().Trim()) + "','" + BUYER_GRADE.ToString().Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','FW','" + rejests + "','" + rejetype .ToString() + "','" + status + "','N','" + Session["userID"].ToString() + "',GETDATE(),'" + RATE.ToString().Trim() + "')";
                    }
                    ldMgt.UpdateUsingExecuteNonQuery(strsql);

                    ModelState.Clear();
                    return RedirectToAction("FarmerPurchaseBaleWiseIndex"); 
                }
                //data = "Error: Please Select all values";
                //json = JsonConvert.SerializeObject(data);
                //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Verification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Verification/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Verification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Verification/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



    }
    public class FARM_PURCHS_DTLS
    {
        public int SNO { get; set; }
        public string HEADER_ID { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string TB_LOT_NO { get; set; }
        public string CLASS_GRADE { get; set; }
        public string TBGR_NO { get; set; }
        public string TB_GRADE { get; set; }
        public Nullable<double> NET_WT { get; set; }
        public Nullable<double> RATE { get; set; }
        public Nullable<double> VALUE { get; set; }
        public string BUYER_GRADE { get; set; }
        public string FARMER_CODE { get; set; }
        public string REJE_STATUS { get; set; }
        public string REJE_TYPE { get; set; }
        public string REMARKS { get; set; }
        public string STATUS { get; set; }
        public string HEADER_STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
    }
}
