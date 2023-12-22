using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace GPILWebApp.Controllers
{
    public class BatchWiseClosingVerificationController : Controller
    {
        // GET: BatchWiseClosingVerification
        public ActionResult BatchwiseClosingIndex()
        {
            return View();
        }

        string varStrQuery;
        [HttpGet]
        // GET: Get Farmercode Based on Organization Code
        public ActionResult GetReferenceNumber(string statusButton)
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();

            string json = "";
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                if (statusButton == "Dispatch")
                {
                    //varStrQuery = "SELECT DISTINCT SHIPMENT_NO AS REF from GPIL_SHIPMENT_HDR_TEMP (NOLOCK) WHERE STATUS='INT'";
                    varStrQuery = "SELECT SHIPMENT_NO AS REF from GPIL_SHIPMENT_HDR_TEMP (NOLOCK) WHERE STATUS='INT' GROUP BY SHIPMENT_NO";
                }
                else if (statusButton == "Receipt")
                {
                    //varStrQuery = "SELECT DISTINCT SHIPMENT_NO AS REF from GPIL_SHIPMENT_HDR (NOLOCK) WHERE STATUS='C'";
                    varStrQuery = "SELECT DISTINCT SHIPMENT_NO AS REF from GPIL_SHIPMENT_HDR (NOLOCK) WHERE STATUS='C' GROUP BY SHIPMENT_NO";
                }
                else if (statusButton == "Classification")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_CLASSIFICATION_HDR_TEMP (NOLOCK) WHERE REASONING_CODE='0' AND STATUS='C'";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_CLASSIFICATION_HDR_TEMP (NOLOCK) WHERE REASONING_CODE='0' AND STATUS='C' GROUP BY BATCH_NO";
                }
                else if (statusButton == "Crop_Transfer")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_CROP_TRANS_HDR_TEMP (NOLOCK) WHERE STATUS='C'";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_CROP_TRANS_HDR_TEMP (NOLOCK) WHERE STATUS='C' GROUP BY BATCH_NO";
                }
                else if (statusButton == "Grade_Transfer")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_CLASSIFICATION_HDR_TEMP (NOLOCK) WHERE REASONING_CODE='1' AND STATUS='C'";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_CLASSIFICATION_HDR_TEMP (NOLOCK) WHERE REASONING_CODE='1' AND STATUS='C' GROUP BY BATCH_NO";
                }
                else if (statusButton == "Grading")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_GRADING_HDR_TEMP (NOLOCK) WHERE STATUS='C'";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_GRADING_HDR_TEMP (NOLOCK) WHERE STATUS='C' GROUP BY BATCH_NO";
                }
                else if (statusButton == "Threshing")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_THRESH_RECON_HDR_TEMP (NOLOCK) WHERE STATUS='C'";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_THRESH_RECON_HDR_TEMP (NOLOCK) WHERE STATUS='C' GROUP BY BATCH_NO";
                }
                else if (statusButton == "Threshing_WMS_Receipt")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_THRESH_RECON_HDR (NOLOCK) WHERE STATUS='N' AND WMS_STATUS IS NULL";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_THRESH_RECON_HDR (NOLOCK) WHERE STATUS='N' AND WMS_STATUS IS NULL GROUP BY BATCH_NO";
                }
                else if (statusButton == "Sales")
                {
                    //varStrQuery = "SELECT DISTINCT BATCH_NO AS REF FROM GPIL_FUMIGATION_HDR_TEMP (NOLOCK) WHERE STATUS='C'";
                    varStrQuery = "SELECT BATCH_NO AS REF FROM GPIL_FUMIGATION_HDR_TEMP (NOLOCK) WHERE STATUS='C' GROUP BY BATCH_NO";
                }
                else if (statusButton == "Fumigation")
                {
                    //varStrQuery = "SELECT DISTINCT SHIPMENT_NO AS REF FROM GPIL_SO_RESERVATION_HDR_TEMP (NOLOCK) WHERE STATUS='INT'";
                    varStrQuery = "SELECT SHIPMENT_NO AS REF FROM GPIL_SO_RESERVATION_HDR_TEMP (NOLOCK) WHERE STATUS='INT' GROUP BY SHIPMENT_NO";
                }
                else
                {
                    //ddlReferenceNo.Visible = false;
                    //return;
                }

                ds1 = ldMgt.GetQueryResult(varStrQuery);

                json = JsonConvert.SerializeObject(ds1);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { }

            return Json(ds1);
        }





        [HttpPost]
        public JsonResult BatchWiseClosingFinal(string refNo, string statusButton)
        {


            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {

                //refNo = "0";
                //int result = Int32.Parse(refNo);


                string lblMessage = string.Empty;
                if (statusButton == "Dispatch")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_DISPATCH(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Dispatch Ref.";

                    }
                }
                else if (statusButton == "Receipt")
                {
                    if (refNo.Length > 0)
                    {
                        string lvarStrUserID = Session["userID"].ToString();
                        lblMessage = GPIL_SP_RECEIPT(refNo, lvarStrUserID, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Receipt Ref.";
                    }
                }
                else if (statusButton == "Classification")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_CLASSIFICATION_TRANSFER(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Classification Batch Ref.";
                    }
                }
                else if (statusButton == "Crop_Transfer")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_CROP_TRANSFER(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Crop Transfer Batch Ref.";
                    }
                }
                else if (statusButton == "Grade_Transfer")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_CLASSIFICATION_TRANSFER(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Grade Transfer Batch Ref.";
                    }
                }
                else if (statusButton == "Grading")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_GRADING(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Grading Batch Ref.";
                    }
                }
                else if (statusButton == "Threshing")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_THRESHING(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Threshing Batch Ref.";
                    }
                }
                else if (statusButton == "Threshing_WMS_Receipt")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_THRESHING_WMS_RECEIPT(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Threshing Batch Ref.";
                    }
                }
                else if (statusButton == "Fumigation")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_FUMIGATION(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Fumigation Batch Ref.";
                    }
                }
                else if (statusButton == "Sales")
                {
                    if (refNo.Length > 0)
                    {
                        lblMessage = GPIL_SP_SALES(refNo, objED.Decrypt(key));
                        GetReferenceNumber(statusButton);
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Sales Ref.";
                    }
                }



                ///*if (lblMessage.Length > 0)*/
                //{
                //    if (lblMessage.Substring(0, 1) == "0")
                //    {
                //        lblMessage = "Error: " + lblMessage.Substring(1, lblMessage.Length - 5);
                //    }
                //    else if (lblMessage.Substring(0, 1) == "1")
                //    {
                //        lblMessage = "Success : " + lblMessage.Substring(1, lblMessage.Text.Length - 1);
                //    }
                //    else
                //    {
                //        lblMessage = "Error : " + lblMessage;
                //    }
                //}
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
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }


            //return JsonResult;
        }



        Class_EncryptionDecryptionTechnique objED = new Class_EncryptionDecryptionTechnique();
        public static string key = ConfigurationManager.AppSettings["SecurityKey"].ToString();
        VerificationManagement vMgt = new VerificationManagement();

        public string GPIL_SP_DISPATCH(string @DISPATCHNO, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }


            //try
            //{

            //    string retVal = "";
            //    string SPName = "GPIL_SP_DISPATCH";
            //    List<SqlParameter> parameters = new List<SqlParameter>();
            //    SqlParameter[] pram = new SqlParameter[3];
            //    pram[0] = (new SqlParameter("@DISPATCHNO", SqlDbType.NVarChar, 50));
            //    //pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
            //    //pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
            //    pram[0].Value = @DISPATCHNO;
            //    pram[1].Direction = ParameterDirection.Output;
            //    pram[2].Direction = ParameterDirection.Output;
            //    for (int i = 0; i < pram.Length; i++)
            //    {
            //        parameters.Add(pram[i]);
            //    }
            //    vMgt.SP_ExecuteNonQuery(parameters, SPName);
            //    if (Convert.ToString(parameters[1].Value) == "1")
            //    {
            //        retVal = "Success: " + Convert.ToString(parameters[2].Value);
            //    }
            //    else
            //    {
            //        retVal = "Error: " + Convert.ToString(parameters[2].Value);
            //    }
            //    return retVal;
            //}
            //catch (Exception ex)
            //{
            //    //objSqlTransaction.Rollback();
            //    return "Error:" + ex.Message;
            //}
            //finally
            //{
            //    //objMSSqlCommand.Dispose();
            //    //objMSSqlConnection.Close();
            //    //objMSSqlConnection.Dispose();
            //}
            try
            {

                string retVal = "";
                string SPName = "GPIL_SP_DISPATCH";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@DISPATCHNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @DISPATCHNO;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;
            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }
        }


        public string GPIL_SP_RECEIPT(string RECEIVEDSHIPMENTNO, string RECEIVEDBY, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {

                string retVal = "";
                string SPName = "GPIL_SP_RECEIPT";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[4];
                pram[0] = (new SqlParameter("@RECEIVEDSHIPMENTNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RECEIVEDBY", SqlDbType.NVarChar, 20));
                pram[2] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[3] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @RECEIVEDSHIPMENTNO;
                pram[1].Value = @RECEIVEDBY;
                pram[2].Direction = ParameterDirection.Output;
                pram[3].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[2].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[3].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[3].Value);
                }
                return retVal;

                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }
        }



        public string GPIL_SP_CLASSIFICATION_TRANSFER(string BATCHTEMPREF, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {

                string retVal = "";
                string SPName = "GPIL_SP_CLASSIFICATION_TRANSFER_WEBNEW";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@BATCHTEMPREF", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @BATCHTEMPREF;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;


                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }

        }

        public string GPIL_SP_CROP_TRANSFER(string BATCHTEMPREF, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate Action!";
            }

            

            try
            {

                string retVal = "";
                string SPName = "GPIL_SP_CROP_TRANSFER";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@BATCHTEMPREF", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @BATCHTEMPREF;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }

        }

        public string GPIL_SP_GRADING(string BATCHTEMPREF, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {
                string retVal = "";
                string SPName = "GPIL_SP_GRADING";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@BATCHTEMPREF", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @BATCHTEMPREF;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;

                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }



        }

        public string GPIL_SP_THRESHING(string BATCHTEMPREF, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate Action!";
            }

            

            try
            {
                string retVal = "";
                string SPName = "GPIL_SP_THRESHING";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@BATCHTEMPREF", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @BATCHTEMPREF;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;

                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }

        }


        public string GPIL_SP_THRESHING_WMS_RECEIPT(string BATCHTEMPREF, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {
                string retVal = "";
                string SPName = "GPIL_SP_THRESHING_WMS_RECEIPT";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@BATCHTEMPREF", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @BATCHTEMPREF;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;

                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }

        }


        public string GPIL_SP_FUMIGATION(string BATCHTEMPREF, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {

                string retVal = "";
                string SPName = "GPIL_SP_FUMIGATION";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@BATCHTEMPREF", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @BATCHTEMPREF;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;


                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }


        }

        public string GPIL_SP_SALES(string @DISPATCHNO, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {
                string retVal = "";
                string SPName = "GPIL_SP_SALES";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[3];
                pram[0] = (new SqlParameter("@@DISPATCHNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @DISPATCHNO;
                pram[1].Direction = ParameterDirection.Output;
                pram[2].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[1].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[2].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[2].Value);
                }
                return retVal;

                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
                //objMSSqlCommand.Dispose();
                //objMSSqlConnection.Close();
                //objMSSqlConnection.Dispose();
            }
        }

    }





    public class Class_EncryptionDecryptionTechnique
    {
        private UTF8Encoding m_utf8 = new UTF8Encoding();
        private byte[] m_key;
        private byte[] m_iv;
        private byte[] key = {
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15,
        16,
        17,
        18,
        19,
        20,
        21,
        22,
        23,
        24
    };
        private byte[] iv = {
        8,
        7,
        6,
        5,
        4,
        3,
        2,
        1
    };
        private TripleDESCryptoServiceProvider m_des = new TripleDESCryptoServiceProvider();
        public void CallSubroutine(byte[] key, byte[] iv)
        {
            this.m_key = key;
            this.m_iv = iv;
        }

        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            // create the necessary streams
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
            // transform the bytes as requested
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            // Read the memory stream and
            // convert it back into byte array
            memStream.Position = 0;
            byte[] result = memStream.ToArray();
            // close and release the streams
            memStream.Close();
            cryptStream.Close();
            // hand back the encrypted buffer
            return result;
        }


        public string Decrypt(string b)
        {
            CallSubroutine(key, iv);
            byte[] input = Convert.FromBase64String(b);
            byte[] output = Transform(input, m_des.CreateDecryptor(m_key, m_iv));
            return m_utf8.GetString(output);

        }

        public string Encrypt(string b)
        {
            CallSubroutine(key, iv);
            byte[] input = m_utf8.GetBytes(b);
            byte[] output = Transform(input, m_des.CreateEncryptor(m_key, m_iv));
            return Convert.ToBase64String(output);

        }
    }
}