using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class ConnectOraController : Controller
    {


        private GREEN_LEAF_TRACEABILITYEntities _context;
        public ConnectOraController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: ConnectOra
        public ActionResult ConnectOraIndex()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.ORGN_TYPE != "TAP" && s.ORGN_TYPE != "FACTORY" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();

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
                if (statusButton == "Re-Direct")
                {

                    varStrQuery = "SELECT DISTINCT SHIPMENT_NO AS REF from GPIL_SHIPMENT_HDR (NOLOCK) WHERE STATUS='INT'";
                }
                else if (statusButton == "Re-Locate")
                {

                    varStrQuery = "SELECT DISTINCT SHIPMENT_NO AS REF from GPIL_SHIPMENT_HDR_TEMP (NOLOCK) WHERE STATUS='Y'";
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
        public JsonResult ConnectOraClosing(string refNo, string orgnCode, string statusButton)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                string lblMessage = string.Empty;
                
                if (statusButton == "Re-Direct")
                {
                    if (refNo.Length > 0)
                    {
                        if (orgnCode.Length > 0)
                        {
                            string lvarStrUserID = Session["userID"].ToString();
                            lblMessage = GPIL_SP_REDIRECT(refNo, orgnCode, lvarStrUserID, objED.Decrypt(key));
                            GetReferenceNumber(statusButton);
                        }
                        else
                        {
                            lblMessage = "Error: Please Select the Re-directed Organization";
                        }
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Dispatch Ref.";
                    }
                }
                else if (statusButton == "Re-Locate")
                {
                    if (refNo.Length > 0)
                    {
                        if (orgnCode.Length > 0)
                        {
                            lblMessage = GPIL_SP_DISPATCH_RELOCATE(refNo, orgnCode, objED.Decrypt(key));
                            GetReferenceNumber(statusButton);
                        }
                        else
                        {
                            lblMessage = "Please Select the Re-located Organization";
                        }
                    }
                    else
                    {
                        lblMessage = "Error: Please Select the Receipt Ref.";
                    }
                }
                else if (statusButton == "Connect_Ora")
                {
                    lblMessage = "";// ConnectAndQuery();
                    GetReferenceNumber(statusButton);
                }




                if (lblMessage.Length > 0)
                {
                    if (statusButton == "Connect_Ora")
                    {

                        lblMessage = "" + lblMessage;
                    }
                    else
                    {
                        //if (lblMessage.Text.Substring(0, 1) == "0")
                        //{
                        //    lblMessage.Text = "Error : " + lblMessage.Text.Substring(1, lblMessage.Text.Length - 1);
                        //}
                        //else if (lblMessage.Text.Substring(0, 1) == "1")
                        //{
                        //    lblMessage.Text = "Success : " + lblMessage.Text.Substring(1, lblMessage.Text.Length - 1);
                        //}
                        //else
                        //{
                        //    lblMessage.Text = "Error : " + lblMessage.Text;
                        //}
                    }
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
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }

        Class_EncryptionDecryptionTechnique objED = new Class_EncryptionDecryptionTechnique();
        public static string key = ConfigurationManager.AppSettings["SecurityKey"].ToString();
        VerificationManagement vMgt = new VerificationManagement();

        public string GPIL_SP_REDIRECT(string OLDSHIPMENTNO, string REDIRECTORGN, string SENDBY, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate Action!";
            }
            

            try
            {
                string retVal = "";

                string SPName = "GPIL_SP_REDIRECT";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[5];
                pram[0] = (new SqlParameter("OLDSHIPMENTNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("REDIRECTORGN", SqlDbType.NVarChar, 20));
                pram[2] = (new SqlParameter("SENDBY", SqlDbType.NVarChar, 20));
                pram[3] = (new SqlParameter("RESULT", SqlDbType.NVarChar, 1));
                pram[4] = (new SqlParameter("OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @OLDSHIPMENTNO;
                pram[1].Value = @REDIRECTORGN;
                pram[2].Value = @SENDBY;                
                pram[3].Direction = ParameterDirection.Output;
                pram[4].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                // retVal = "/" + retVal + "||" + vMgt.SP_ExecuteNonQuery(parameters, SPName);
                vMgt.SP_ExecuteNonQuery(parameters, SPName);
                if (Convert.ToString(parameters[3].Value) == "1")
                {
                    retVal = "Success: " + Convert.ToString(parameters[4].Value);
                }
                else
                {
                    retVal = "Error: " + Convert.ToString(parameters[4].Value);
                }
                return retVal;



                //objMSSqlConnection.Open();
                //objSqlTransaction = objMSSqlConnection.BeginTransaction();
                //objMSSqlCommand.Connection = objMSSqlConnection;
                //objMSSqlCommand.Transaction = objSqlTransaction;
                //objMSSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //objMSSqlCommand.CommandText = "GPIL_SP_REDIRECT";
                //objMSSqlCommand.Parameters.AddWithValue("@OLDSHIPMENTNO", OLDSHIPMENTNO);
                //objMSSqlCommand.Parameters.AddWithValue("@REDIRECTORGN", REDIRECTORGN);
                //objMSSqlCommand.Parameters.AddWithValue("@SENDBY", SENDBY);
                //objMSSqlCommand.Parameters.Add("@RESULT", SqlDbType.NVarChar, 1).Direction = ParameterDirection.Output;
                //objMSSqlCommand.Parameters.Add("@OUTPUT", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                //objMSSqlCommand.ExecuteNonQuery();
                //lvarStrResult = Convert.ToString(objMSSqlCommand.Parameters["@RESULT"].Value) + Convert.ToString(objMSSqlCommand.Parameters["@OUTPUT"].Value);

                //objSqlTransaction.Commit();
                

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


        public string GPIL_SP_DISPATCH_RELOCATE(string OLDSHIPMENTNO, string RELOCATEORGN, string inParamUnknown)
        {
            if (inParamUnknown != objED.Decrypt(key))
            {
                return "Error:" + "Please Contact Admin for Immediate action!";
            }

            

            try
            {
                string retVal = "";

                string SPName = "GPIL_SP_DISPATCH_RELOCATE";
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlParameter[] pram = new SqlParameter[4];
                pram[0] = (new SqlParameter("OLDSHIPMENTNO", SqlDbType.NVarChar, 50));
                pram[1] = (new SqlParameter("RELOCATEORGN", SqlDbType.NVarChar, 20));
                //pram[2] = (new SqlParameter("SENDBY", SqlDbType.NVarChar, 20));
                pram[2] = (new SqlParameter("RESULT", SqlDbType.NVarChar, 1));
                pram[3] = (new SqlParameter("OUTPUT", SqlDbType.NVarChar, 1000));
                pram[0].Value = @OLDSHIPMENTNO;
                pram[1].Value = @RELOCATEORGN;
                //pram[2].Value = @SENDBY;
                pram[2].Direction = ParameterDirection.Output;
                pram[3].Direction = ParameterDirection.Output;
                for (int i = 0; i < pram.Length; i++)
                {
                    parameters.Add(pram[i]);
                }
                //retVal = "/" + retVal + "||" + vMgt.SP_ExecuteNonQuery(parameters, SPName);
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

                //objMSSqlConnection.Open();
                //objSqlTransaction = objMSSqlConnection.BeginTransaction();
                //objMSSqlCommand.Connection = objMSSqlConnection;
                //objMSSqlCommand.Transaction = objSqlTransaction;
                //objMSSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //objMSSqlCommand.CommandText = "GPIL_SP_DISPATCH_RELOCATE";
                //objMSSqlCommand.Parameters.AddWithValue("@OLDSHIPMENTNO", OLDSHIPMENTNO);
                //objMSSqlCommand.Parameters.AddWithValue("@RELOCATEORGN", RELOCATEORGN);
                //objMSSqlCommand.Parameters.Add("@RESULT", SqlDbType.NVarChar, 1).Direction = ParameterDirection.Output;
                //objMSSqlCommand.Parameters.Add("@OUTPUT", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                //objMSSqlCommand.ExecuteNonQuery();
                //lvarStrResult = Convert.ToString(objMSSqlCommand.Parameters["@RESULT"].Value) + Convert.ToString(objMSSqlCommand.Parameters["@OUTPUT"].Value);

                //objSqlTransaction.Commit();
                

            }
            catch (Exception ex)
            {
                //objSqlTransaction.Rollback();
                return "Error:" + ex.Message;
            }
            finally
            {
               
            }


        }



    }
}