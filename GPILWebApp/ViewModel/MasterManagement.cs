using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using GPI;
using System.Data.SqlClient;
using GPIWebApp;
using System.Configuration;

namespace GPILWebApp.ViewModel
{
    public class MasterManagement : GPIObject
    {
        public string strConnection = "";

        public DataTable GetQueryResult(string strQry)
        {
            DataTable dt = new DataTable();
            DataColumn dtColumn = new DataColumn();
            DataRow myDataRow;
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


                dt.Columns.Add("ErrorMessage");
                myDataRow = dt.NewRow();
                myDataRow["ErrorMessage"] = ex.Message;
                dt.Rows.Add(myDataRow);
                return dt;
            }
            return dt;
        }


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






    }
}