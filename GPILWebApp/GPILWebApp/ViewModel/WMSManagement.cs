using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPI;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GPIWebApp;

namespace GPILWebApp.ViewModel
{
    public class WMSManagement : GPIObject
    {
        public DataSet GetWMSDataStatus(string strFromDate, string strToDate,string strLP5)
        {
            DataSet ds = new DataSet();
            try
            {
                string strQry = "";
                strQry = "Select BC_WALD_LP5_NO as LP5No, BC_WALD_BALE_NO as CaseBarCode,(Case when BC_WALD_READ_FLAG='Y' then 'Uploaded' else 'Pending' end) as Status from BC_WMS_LEAF_ASN_D where 1=1 ";
                if (strLP5 !="")                
                    strQry = strQry + "  and BC_WALD_LP5_NO='" + strLP5 + "' ";
                if (strFromDate != "" && strToDate != "")
                {
                    strQry = strQry + "  and BC_WALD_INSERT_DT between ";
                    strQry = strQry + " convert(datetime, '" + strFromDate + " 00:00:00',105) AND ";
                    strQry = strQry + " convert(datetime, '" + strToDate + " 23:59:59',105) ";
                }
                ds = base.ODataServer.GetDataset(strQry);                
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

    }
}