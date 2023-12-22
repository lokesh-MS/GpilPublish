using GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GPILWebApp.ViewModel
{
    public class DataLoaderManagement : GPIObject
    {

        public bool UpdateUsingExecuteNonQuery(string strQuery)
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

        bool disposed;

        SqlConnection con = null;
        SqlCommand comm = null;
        SqlDataAdapter adap = null;

        private static DataServer instance = null;
        private static object syncRoot = new Object();
        private string connectionString1 = null;
        private string connectionString = null;


        SqlCommand commTans = null;
        string strConnection = null;
        SqlTransaction trans = null;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (comm != null)
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                        {
                            comm.Connection.Close();
                            comm = null;
                        }
                    }
                    instance = null;
                }

                disposed = true;
            }
        }
        //public bool UpdateUsingExecuteNonQueryList(List<string> qryStringCol)
        //{
        //    bool b = false;
        //    try
        //    {
        //        foreach (string qry in qryStringCol)
        //        {
        //            b = base.ODataServer.ExecuteNonQuery(qry);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StringBuilder err = new StringBuilder();
        //        err.Append(" Message : " + ex.Message);
        //        err.AppendLine(" STACK TRACE : " + ex.StackTrace);
        //        err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
        //        err.AppendLine(" SOURCE : " + ex.Source);
        //        Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
        //        b = false;

        //    }
        //    return b;
        //}

        public bool UpdateUsingExecuteNonQueryList(List<string> strQuery)
        {
            bool b = false;
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



               


        public bool Transaction(List<string> qryCollection)
        {
            bool res = false;
            SqlTransaction transaction = null;

            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    using (comm = new SqlCommand())
                    {
                        comm.Connection = con;
                        comm.Connection.Open();

                        transaction = con.BeginTransaction();
                        // Assign Transaction to Command
                        comm.Transaction = transaction;
                        foreach (string qry in qryCollection)
                        {
                            comm.CommandText = qry;
                            comm.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        res = true;
                    }
                }
            }

            catch (InvalidOperationException ex)
            {
                transaction.Rollback();
                Dispose(true);
                string errMsg = "There is already an open DataReader associated with this Command which must be closed first.";
                if (ex.Message.ToUpper().Equals(errMsg.ToUpper()))
                {
                    if (comm != null)
                    {
                        if (comm.Connection.State == ConnectionState.Open) { }
                        //Transaction(qryCollection);
                    }
                }
                return res;
            }
            catch
            {
                transaction.Rollback();
                Dispose(true);
                return res;
            }
            finally
            { }
            return res;
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
    }
}