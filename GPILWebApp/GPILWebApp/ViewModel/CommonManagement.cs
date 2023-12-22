using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for CommonManagement
/// </summary>
namespace GPI
{
    public class CommonManagement : GPIObject
    {
        public CommonManagement()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Need to bind the dropdown using table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable BindDropDown(string tableName)
        {
            DataTable dtBind = new DataTable();
            try
            {

                string strQry = "Select TextField, ValueField from General where TableName ='" + tableName + "' and flag= 1";
                dtBind = base.ODataServer.GetDataTable(strQry);

            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                dtBind = null;
            }
            return dtBind;
        }


        public DataTable BindDropDownCrop()
        {
            DataTable dtBind = new DataTable();
            try
            {

                string strQry = "select CROP, CROP_Year, Attribute1, CROP +' - ' + CROP_YEAR as DISPLAY from[dbo].[GPIL_CROP_MASTER]  where status = 'Y'";
                dtBind = base.ODataServer.GetDataTable(strQry);

            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                dtBind = null;
            }
            return dtBind;
        }


        


        public DataTable BindDropDownVariety()
        {
            DataTable dtBind = new DataTable();
            try
            {

                string strQry = "select Variety,Variety_Name, Variety + ' - ' + Variety_Name as Display from [dbo].[GPIL_VARIETY_MASTER] where status='Y'";
                dtBind = base.ODataServer.GetDataTable(strQry);

            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                dtBind = null;
            }
            return dtBind;
        }


        public DataTable BindDropDownOrgn()
        {
            DataTable dtBind = new DataTable();
            try
            {

                string strQry = "select Orgn_Code,Orgn_Name,Orgn_Code + ' - ' +Orgn_Name as Display  from [dbo].[GPIL_ORGN_MASTER] where status='Y'";
                dtBind = base.ODataServer.GetDataTable(strQry);

            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                dtBind = null;
            }
            return dtBind;
        }


        public DataTable BindDropDownSupp()
        {
            DataTable dtBind = new DataTable();
            try
            {

                string strQry = "select Supp_Code,Supp_Name, Supp_Code + ' - ' + Supp_Name as Display from [dbo].[GPIL_SUPPLIER_MASTER] where status='Y'";
                dtBind = base.ODataServer.GetDataTable(strQry);

            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                dtBind = null;
            }
            return dtBind;
        }











        /// <summary>
        /// Check the dropdown value using table name and values
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool CheckDropDownFields(string value, string tableName)
        {
            bool b = false;
            DataTable dtBind = new DataTable();
            try
            {
                string strQry = "Select * from General where TableName ='" + tableName + "' and TextField ='" + value + "' and flag= 1   ";
                dtBind = base.ODataServer.GetDataTable(strQry);
                if (dtBind.Rows.Count == 0)
                    b = true;
                else
                    b = false;
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                dtBind = null;
                b = false;
            }
            return b;
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

        public bool UpdateUsingExecuteNonQueryList(List<string> strQuery)
        {
            bool b = false;
            try
            {
                foreach (string qry in strQuery)
                {
                    b = base.ODataServer.ExecuteNonQuery(qry);
                }
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

        public string SP_ExecuteNonQuery(List<SqlParameter> parameters, string SPName)

        {
            string Output = string.Empty;
            try
            {
                Output = base.ODataServer.ExecuteSPP_RtnString(parameters, SPName);
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


    }
}