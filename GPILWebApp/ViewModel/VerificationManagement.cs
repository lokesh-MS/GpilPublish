using GPI;
using GPIWebApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class VerificationManagement : GPIObject
    {
        /// <summary>
        /// Farmer Purchase Verification
        /// </summary>
        /// <param name="strPoNumber"></param>
        /// <returns></returns>
        public DataSet GetFarmerPurchase(string strPoNumber)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("PoNumber", strPoNumber.Trim()));
                parameters.Add(new SqlParameter("PURCHASE_TYPE ", "SUNDRY PURCHASE"));
                //parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_Verification_GetFarmerPurchaseDetails", "Table");
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return ds = null;
            }
            return ds;
        }


        public DataSet GetFarmerPurchaseBaleWise(string strPoNumber, string strOrgnCode)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("PoNumber", strPoNumber.Trim()));
                parameters.Add(new SqlParameter("ORGN_CODE", strOrgnCode.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_Verification_GetFarmerPurchaseBaleWiseDetails", "Table");
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return ds = null;
            }
            return ds;
        }
        public DataTable GetQueryResult(string strQry)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return dt = null;
            }
            return dt;
        }
        public bool UpdateUsingExecuteNonQuery( string strQuery)
        {
            bool b = false; 
            try
            { 
                b = base.ODataServer.ExecuteNonQuery(strQuery);
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                b = false;

            }
            return b;
        }






        /// <summary>
        /// Invoice Verification
        /// </summary>
        /// <param name="strPoNumber"></param>
        /// <returns></returns>
        public DataSet GetInvoiceVerification(string strPoNumber)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("PoNumber", strPoNumber.Trim()));
                //parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_Verification_GetFarmerPurchaseDetails", "Table");
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return ds = null;
            }
            return ds;
        }



        public DataSet GetInvoiceVerify(string strPoNumber, string strOrgnCode)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("PoNumber", strPoNumber.Trim()));
                parameters.Add(new SqlParameter("ORGN_CODE", strOrgnCode.Trim()));
                parameters.Add(new SqlParameter("PURCHASE_TYPE ", "TAP PURCHASE"));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_Verification_GetInvoiceverifyDetails", "Table");
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return ds = null;
            }
            return ds;
        }

        /// <summary>
        /// Supplier PURCHASE
        /// </summary>
        /// <param name="strPoNumber"></param>
        /// <returns></returns>
        public DataSet GetSupplierPurchaseVerify(string strhdrID)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("HdrID", strhdrID.Trim()));
                //parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_Verification_GetSupplierPurchaseDetails", "Table");
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return ds = null;
            }
            return ds;
        }

        public string SP_ExecuteNonQuery(List<SqlParameter> parameters, string SPName)

        {
            string Output = string.Empty;
            try
            {
                Output = base.ODataServer.ExecuteSP_RtnString(parameters, SPName);
            }
            catch (Exception ex)
            {
                Output = null;
            }
            return Output;
        }


        public DataSet GetdsQueryResult(string strQry)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {

                dt = base.ODataServer.GetDataTable(strQry);
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return ds = null;
            }
            return ds;
        }



        public bool UpdateUsingExecuteNonQueryList(List<string> strQuery)
        {
            bool b = false;
            string strConnection = "";
            strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //try
            //{
            SqlTransaction objTrans = null;
            using (SqlConnection objConn = new SqlConnection(strConnection))
            {

                objConn.Open();
                objTrans = objConn.BeginTransaction();
                try
                {
                    foreach (string qry in strQuery)
                    {

                        SqlCommand objCmd1 = new SqlCommand(qry, objConn, objTrans);

                        objCmd1.ExecuteNonQuery();
                        //objCmd2.ExecuteNonQuery();
                    }
                    objTrans.Commit();
                    b = true;
                }
                catch (Exception ex)
                {
                    StringBuilder err = new StringBuilder();
                    err.Append(" Message : " + ex.Message);
                    err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                    err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                    err.AppendLine(" SOURCE : " + ex.Source);
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                    b = false;
                    objTrans.Rollback();
                }
                finally
                {

                    objConn.Close();
                }




            }
            //}
            //catch (Exception ex)
            //{


            //}
            return b;
        }



    }
}