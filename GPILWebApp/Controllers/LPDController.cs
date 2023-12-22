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
    public class LPDController : Controller
    {


        private GREEN_LEAF_TRACEABILITYEntities _context;
        public LPDController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: LPD
        public ActionResult CompetitionReport()
        {
            return View();
        }

        public ActionResult CompetitionConsolitateReport()
        {
            return View();
        }

        public ActionResult MiniMaxReportForVKBU()
        {
           return View();
        }
        public ActionResult BGTransactionReport()
        {
            return View();
        }

        public ActionResult BCMAxMinTransChrReport()
        {
            return View();
        }



        /// <summary>
        /// TapWise Purchase Information
        /// </summary>
        /// <returns></returns>
        public ActionResult TapPurchase()
        {
            return View();
        }

        /// <summary>
        /// TapWise Quantity Marketed Reports
        /// </summary>
        /// <returns></returns>
        public ActionResult TapQuantityMarketed()
        {
            return View();
        }

        /// <summary>
        /// Buyer Classification Report
        /// </summary>
        /// <returns></returns>
        public ActionResult BuyerClassification()
        {
            return View();
        }

        /// <summary>
        /// TapPurchase Info / Tap Invoice value
        /// </summary>
        /// <returns></returns>
        public ActionResult TapPurchaseInfo()
        {
            return View();
        }
        /// <summary>
        /// Buyer Class MinMax Report
        /// </summary>
        /// <returns></returns>
        public ActionResult MinMaxreport()
        {
            return View();
        }
        /// <summary>
        /// PPGRD REPORT
        /// </summary>
        /// <returns></returns>
        public ActionResult PPGRDreport()
        {
            return View();
        }
        /// <summary>
        /// TAP Wise Stock Reconciliation Report
        /// </summary>
        /// <returns></returns>
        public ActionResult TapStockRecon()
        {
            return View();
        }

        /// <summary>
        /// Competition High/Low bit and Others
        /// </summary>
        /// <returns></returns>
        public ActionResult HighLowBidCompetition()
        {
            return View();
        }

        /// <summary>
        /// BGInformation
        /// </summary>
        /// <returns></returns>
        //public ActionResult BGInformation()
        //{
        //    return View();
        //}


        public ActionResult BGInformation()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from c in _context.GPIL_VARIETY_MASTER select new { VARIETY1 = c.VARIETY + " - " + c.VARIETY_DESC, c.VARIETY }).ToList();
            ViewBag.GPIL_BANK_NAME_LIST = (from c in _context.GPIL_BANK_NAME_LIST select new { c.BANK_CODE }).ToList();

           // ViewBag.GPIL_VARIETY_MASTER = (from c in _context.GPIL_VARIETY_MASTER select new { VARIETY1 = c.VARIETY + " - " + c.VARIETY_DESC, c.VARIETY }).ToList();
            ViewBag.GPIL_BANK_NAME_LIST1 = (from c in _context.GPIL_BANK_NAME_LIST select new { c.BANK_CODE }).ToList();
            return View();
        }


        [HttpGet]
        public ActionResult GetOrganizationCode(string variety)
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                strsql = "SELECT ORGN_CODE,(ORGN_CODE + ' - ' + ORGN_NAME) AS ORGN FROM GPIL_ORGN_MASTER (NOLOCK) WHERE ORGN_TYPE='TAP' AND VARIETY='" + variety + "' ORDER BY ORGN_CODE";
                ds1 = ldMgt.GetQueryResult(strsql);
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
        public ActionResult GetTransferTypeOrgn(string transferType)
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
               
                if (transferType == "TRANSFER")
                {
                  //  transferType = true;
                    //ddlTransferToBank.Visible = true;
                    //transtoOrgn.Visible = true;
                    //transtoBank.Visible = true;
                    strsql = "SELECT ORGN_CODE,(ORGN_CODE + ' - ' + ORGN_NAME) AS ORGN FROM GPIL_ORGN_MASTER (NOLOCK) WHERE ORGN_TYPE='TAP' AND ORGN_CODE<>'" + transferType + "' ORDER BY ORGN_CODE";
                    ds1 = ldMgt.GetQueryResult(strsql);
                    ds1.TableName = "Table";
                    var data = ds1;
                    json = JsonConvert.SerializeObject(data);
                    var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {

                }
                
            }
            catch (Exception ex)
            { }
            return Json(ds);
        }

        string strTransferVariety;
        double dblToPreviousBGAmount;
        double dblPreviousBGAmount;
        double dblPreviousUtilizationAmount;
        double dblToPreviousUtilizationAmount;
        double dblTransactionAmount;
        double dblCurrentBGAmount;
        double dblToCurrentBGAmount;
        double dblCurrentUtilizationAmount;
        double dblToCurrentUtilizationAmount;

        //string strTransferVariety;
        //double dblToPreviousBGAmount;
        //double dblPreviousBGAmount;
        //double dblPreviousUtilizationAmount;
        //double dblToPreviousUtilizationAmount;
        //// double dblTransactionAmount;
        //double dblCurrentBGAmount;
        //double dblToCurrentBGAmount;
        //double dblCurrentUtilizationAmount;
        //double dblToCurrentUtilizationAmount;
        //double reciverorgputlsamount;
        //double reciverorgBGamount;
        //double rcvbgamt;
        //******************************* sunil *******************************
        bool bolFromOrg = false;
        bool bolToOrg = false;
        string data = String.Empty;
        string json = String.Empty;
        JsonResult jsonResult;

        [HttpPost]
        public ActionResult TransactionSave(string Crop, string Variety, string Organization, string TransactionType, string BankName, string Amount ,string transferTOOrgnList, string transferToBankList)
        {
            double dblTransactionAmount = Convert.ToDouble(Amount);
            try
            {
                if (ModelState.IsValid)
                {
                    string strQuery = "SELECT BG_AMOUNT,UTILIZED_AMOUNT FROM GPIL_BG_INFORMATION WHERE ORGN_CODE = '" + Organization + "' and CROP='" + Crop + "' and VARIETY='" + Variety + "'";

                    DataTable objDataTablerBG1 = new DataTable();
                    VerificationManagement ldMgt = new VerificationManagement();
                    objDataTablerBG1 = ldMgt.GetQueryResult(strQuery);
                    if (objDataTablerBG1.Rows.Count > 0)
                    {
                        dblPreviousBGAmount = Convert.ToDouble(objDataTablerBG1.Rows[0][0].ToString());
                        dblPreviousUtilizationAmount = Convert.ToDouble(objDataTablerBG1.Rows[0][1].ToString());
                        bolFromOrg = true;
                    }
                    else
                    {
                        dblPreviousBGAmount = 0;
                        dblPreviousUtilizationAmount = 0;
                    }
                    if (TransactionType == "TRANSFER")
                    {
                        strQuery = "SELECT VARIETY FROM GPIL_ORGN_MASTER WHERE ORGN_CODE = '" + transferTOOrgnList + "'";
                        DataTable objDataTablerBG2 = new DataTable();
                        objDataTablerBG2 = ldMgt.GetQueryResult(strQuery);
                        if (objDataTablerBG2.Rows.Count > 0)
                        {
                            strTransferVariety = objDataTablerBG2.Rows[0][0].ToString();
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error", "alert('Variety not exist for selected Transfer Org.');", true);
                            //return;
                            data = "Error: Variety not exist for selected Transfer Org";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        strQuery = "SELECT BG_AMOUNT,UTILIZED_AMOUNT FROM GPIL_BG_INFORMATION WHERE ORGN_CODE = '" + transferTOOrgnList + "' AND CROP='" + Crop + "' AND VARIETY='" + strTransferVariety + "'";
                        DataTable objDataTablerBG3 = new DataTable();
                        objDataTablerBG3 = ldMgt.GetQueryResult(strQuery);
                        if (objDataTablerBG3.Rows.Count > 0)
                        {
                            dblToPreviousBGAmount = Convert.ToDouble(objDataTablerBG3.Rows[0][0].ToString());
                            dblToPreviousUtilizationAmount = Convert.ToDouble(objDataTablerBG3.Rows[0][1].ToString());
                            bolToOrg = true;
                        }
                        else
                        {
                            dblToPreviousBGAmount = 0;
                            dblToPreviousUtilizationAmount = 0;
                        }
                        if (dblTransactionAmount > dblPreviousBGAmount)
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error", "alert('T');", true);
                            //return;
                            data = "Error: error";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        dblCurrentBGAmount = dblPreviousBGAmount - dblTransactionAmount;
                       // dblToCurrentBGAmount = dblToCurrentBGAmount + dblTransactionAmount;
                        dblToCurrentBGAmount = dblToPreviousBGAmount + dblTransactionAmount;
                        dblCurrentUtilizationAmount = dblPreviousUtilizationAmount;
                        dblToCurrentUtilizationAmount = dblToPreviousUtilizationAmount;
                    }
                    else
                    {
                        dblCurrentBGAmount = dblPreviousBGAmount + dblTransactionAmount;
                        dblCurrentUtilizationAmount = dblPreviousUtilizationAmount;
                    }
                    string strFromBGQuery = "";
                    string strFromBGTransactionQuery = "";
                    if (bolFromOrg == true)
                    {
                        strFromBGQuery = "UPDATE GPIL_BG_INFORMATION SET BG_AMOUNT='" + dblCurrentBGAmount.ToString() + "' where ORGN_CODE = '" + Organization + "' and CROP='" + Crop + "' and VARIETY='" + Variety + "'";
                        //strFromBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BG" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + transferTOOrgnList + "','" + dblToCurrentBGAmount.ToString() + "','0','" + Crop + "','" + Variety + "',GETDATE(),'5655',GETDATE(),'Y')";
                    }
                    else
                    {
                        strFromBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BG" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + Organization + "','" + dblCurrentBGAmount.ToString() + "','0','" + Crop + "','" + Variety + "',GETDATE(),'5655',GETDATE(),'Y')";
                    }
                    strFromBGTransactionQuery = "INSERT INTO GPIL_BG_TRANSACTION (TRANS_ID,TRANS_TYPE,ORGN_CODE,CROP,VARIETY,PREVIOUS_BG_AMOUNT,PREVIOUS_UTILIZED_AMOUNT,TRANS_AMOUNT,BG_AMOUNT,UTILIZED_AMOUNT,BG_DATE,CREATED_BY,CREATED_DATE,STATUS)";
                    strFromBGTransactionQuery = strFromBGTransactionQuery + "  VALUES('" + Organization + DateTime.Now.ToString("yyyyMMddhhmmss") + "','" + TransactionType + "','" + Organization + "','" + Crop + "','" + Variety + "','" + dblPreviousBGAmount.ToString() + "','" + dblPreviousUtilizationAmount.ToString() + "','" + dblTransactionAmount.ToString() + "','" + dblCurrentBGAmount.ToString() + "','" + dblCurrentUtilizationAmount.ToString() + "',GETDATE(),'5655',GETDATE(),'Y')";
                    ldMgt.UpdateUsingExecuteNonQuery(strFromBGQuery);
                    ldMgt.UpdateUsingExecuteNonQuery(strFromBGTransactionQuery);
                    if (TransactionType == "TRANSFER")
                    {
                        string strToBGQuery = "";
                        string strToBGTransactionQuery = "";
                        if (bolToOrg == true)
                        {
                            strToBGQuery = "UPDATE GPIL_BG_INFORMATION SET BG_AMOUNT='" + dblToCurrentBGAmount.ToString() + "' WHERE ORGN_CODE = '" + transferTOOrgnList + "' AND CROP='" + Crop + "' AND VARIETY='" + strTransferVariety + "'";
                            //evening i was change this line *************
                            //  strToBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BGT" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + transferTOOrgnList + "','" + dblToCurrentBGAmount.ToString() + "','0','" + Crop + "','" + strTransferVariety + "',GETDATE(),'5655',GETDATE(),'Y')";
                        }
                        else
                        {
                            strToBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BGT" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + transferTOOrgnList + "','" + dblToCurrentBGAmount.ToString() + "','0','" + Crop + "','" + strTransferVariety + "',GETDATE(),'5655',GETDATE(),'Y')";
                        }
                        strToBGTransactionQuery = "INSERT INTO GPIL_BG_TRANSACTION (TRANS_ID,TRANS_TYPE,ORGN_CODE,CROP,VARIETY,PREVIOUS_BG_AMOUNT,PREVIOUS_UTILIZED_AMOUNT,TRANS_AMOUNT,BG_AMOUNT,UTILIZED_AMOUNT,BG_DATE,CREATED_BY,CREATED_DATE,STATUS)";
                        strToBGTransactionQuery = strToBGTransactionQuery + "  VALUES('T" + transferTOOrgnList + DateTime.Now.ToString("yyyyMMddhhmmss") + "','ADDITION','" + transferTOOrgnList + "','" + Crop + "','" + strTransferVariety + "','" + dblToPreviousBGAmount.ToString() + "','" + dblToPreviousUtilizationAmount.ToString() + "','" + dblTransactionAmount.ToString() + "','" + dblToCurrentBGAmount.ToString() + "','" + dblToCurrentUtilizationAmount.ToString() + "',GETDATE(),'5655',GETDATE(),'Y')";

                        //LOKESH//

                        strQuery = "SELECT BG_AMOUNT,UTILIZED_AMOUNT FROM GPIL_BG_INFORMATION WHERE ORGN_CODE = '" + transferTOOrgnList + "' AND CROP='" + Crop + "' AND VARIETY='" + strTransferVariety + "'";
                        DataTable objDataTablerBG3 = new DataTable();
                        objDataTablerBG3 = ldMgt.GetQueryResult(strQuery);
                        if (objDataTablerBG3.Rows.Count > 0)
                        {
                            strToBGQuery = "UPDATE GPIL_BG_INFORMATION SET BG_AMOUNT='" + dblToCurrentBGAmount.ToString() + "' WHERE ORGN_CODE = '" + transferTOOrgnList + "' AND CROP='" + Crop + "' AND VARIETY='" + strTransferVariety + "'";
                            ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);
                            ldMgt.UpdateUsingExecuteNonQuery(strToBGTransactionQuery);
                        }
             
                        else
                        {
                            strToBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BGT" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + transferTOOrgnList + "','" + dblToCurrentBGAmount.ToString() + "','0','" + Crop + "','" + strTransferVariety + "',GETDATE(),'5655',GETDATE(),'Y')";
                            ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);
                            ldMgt.UpdateUsingExecuteNonQuery(strToBGTransactionQuery);
                        }
                         /// sunil 


                        //string strtoOrgpriviousutilizedsamt = "";

                        //string strrcvbgamt = "";
                        //strtoOrgpriviousutilizedsamt = "SELECT BG_AMOUNT,UTILIZED_AMOUNT FROM GPIL_BG_INFORMATION WHERE ORGN_CODE = '" + transferTOOrgnList + "' AND CROP='" + Crop + "' AND VARIETY='" + strTransferVariety + "'";
                        //DataTable objDataTablerutli = new DataTable();
                        //VerificationManagement bgmtd = new VerificationManagement();
                        //objDataTablerutli = bgmtd.GetQueryResult(strtoOrgpriviousutilizedsamt);

                        //if (objDataTablerutli.Rows.Count > 0)
                        //{
                        //    reciverorgputlsamount = Convert.ToDouble(objDataTablerutli.Rows[0][1].ToString());
                        //    reciverorgBGamount = Convert.ToDouble(objDataTablerutli.Rows[0][0].ToString());
                        //    if (reciverorgBGamount !=0 || reciverorgBGamount != null)
                        //    {
                        //        rcvbgamt = reciverorgBGamount + dblTransactionAmount;

                        //        strrcvbgamt = "UPDATE GPIL_BG_INFORMATION SET BG_AMOUNT='" + rcvbgamt.ToString() + "'  WHERE ORGN_CODE = '" + transferTOOrgnList + "' AND CROP='" + Crop + "' AND VARIETY='" + strTransferVariety + "'  ";
                        //        ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);

                        //    }
                        //    else
                        //    {
                        //        strToBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BGT" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + transferTOOrgnList + "','" + reciverorgBGamount.ToString() + "','0','" + Crop + "','" + strTransferVariety + "',GETDATE(),'5655',GETDATE(),'Y')";
                        //        ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);
                        //    }
                        //    strToBGTransactionQuery = "INSERT INTO GPIL_BG_TRANSACTION (TRANS_ID,TRANS_TYPE,ORGN_CODE,CROP,VARIETY,PREVIOUS_BG_AMOUNT,PREVIOUS_UTILIZED_AMOUNT,TRANS_AMOUNT,BG_AMOUNT,UTILIZED_AMOUNT,BG_DATE,CREATED_BY,CREATED_DATE,STATUS)";
                        //    strToBGTransactionQuery = strToBGTransactionQuery + "  VALUES('T" + transferTOOrgnList + DateTime.Now.ToString("yyyyMMddhhmmss") + "','ADDITION','" + transferTOOrgnList + "','" + Crop + "','" + strTransferVariety + "','" + reciverorgBGamount.ToString() + "','" + reciverorgputlsamount.ToString() + "','" + dblTransactionAmount.ToString() + "','" + dblToCurrentBGAmount.ToString() + "','" + reciverorgputlsamount.ToString() + "',GETDATE(),'5655',GETDATE(),'Y')";
                        //    ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);
                        //    ldMgt.UpdateUsingExecuteNonQuery(strToBGTransactionQuery);
                        //}
                        //else
                        //{



                        //        strToBGQuery = "INSERT GPIL_BG_INFORMATION(INFO_ID,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,CROP,VARIETY,[BG_DATE],[CREATED_BY],[CREATED_DATE],[STATUS]) VALUES('BGT" + DateTime.Now.ToString("yyMMddhhmmss") + "','" + transferTOOrgnList + "','" + reciverorgBGamount.ToString() + "','0','" + Crop + "','" + strTransferVariety + "',GETDATE(),'5655',GETDATE(),'Y')";
                        //    ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);

                        //    strToBGTransactionQuery = "INSERT INTO GPIL_BG_TRANSACTION (TRANS_ID,TRANS_TYPE,ORGN_CODE,CROP,VARIETY,PREVIOUS_BG_AMOUNT,PREVIOUS_UTILIZED_AMOUNT,TRANS_AMOUNT,BG_AMOUNT,UTILIZED_AMOUNT,BG_DATE,CREATED_BY,CREATED_DATE,STATUS)";
                        //    strToBGTransactionQuery = strToBGTransactionQuery + "  VALUES('T" + transferTOOrgnList + DateTime.Now.ToString("yyyyMMddhhmmss") + "','ADDITION','" + transferTOOrgnList + "','" + Crop + "','" + strTransferVariety + "','" + dblToPreviousBGAmount.ToString() + "','" + reciverorgputlsamount.ToString() + "','" + dblTransactionAmount.ToString() + "','" + dblToCurrentBGAmount.ToString() + "','" + reciverorgputlsamount.ToString() + "',GETDATE(),'5655',GETDATE(),'Y')";
                        //    ldMgt.UpdateUsingExecuteNonQuery(strToBGQuery);
                        //    ldMgt.UpdateUsingExecuteNonQuery(strToBGTransactionQuery);
                        //}


                        //////////////////////////////////************************////////////////////////////  sunil






                    }
                    data = "Succuss: BG Value Update Successfully";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error", "alert('" + ex.Message.ToString() + "');", true);
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        VerificationManagement ldMgt = new VerificationManagement();
        [HttpGet]
        public ActionResult GetlblValues(string crop, string variety, string organization)
        {
            try
            {
                string strsql = "select BG_AMOUNT,UTILIZED_AMOUNT FROM GPIL_BG_INFORMATION WHERE ORGN_CODE = '" + organization + "' and CROP='" + crop + "' and VARIETY='" + variety + "'";
                DataTable dt = new DataTable();
                dt = ldMgt.GetQueryResult(strsql);
                //if (dt.Rows.Count > 0)
                //{
                string json = JsonConvert.SerializeObject(dt);
                return Json(json, JsonRequestBehavior.AllowGet);
                // }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetlblValues1(string crop, string variety)
        {
            try
            {
                string strsql = "select BG_AMOUNT,UTILIZED_AMOUNT FROM GPIL_BG_INFORMATION WHERE CROP='" + crop + "' and VARIETY='" + variety + "'";
                DataTable dt = new DataTable();
                dt = ldMgt.GetQueryResult(strsql);
                //if (dt.Rows.Count > 0)
                //{
                string json = JsonConvert.SerializeObject(dt);
                return Json(json, JsonRequestBehavior.AllowGet);
                // }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}