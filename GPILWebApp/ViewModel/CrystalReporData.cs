using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp;
using GPI;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GPILWebApp.ViewModel
{
    public class CrystalReportData : GPIObject
    {
        public DataSet GetORGN(string type, string strHeaderID, string strFarmerCode)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Type", type.Trim()));
                parameters.Add(new SqlParameter("HeaderID", strHeaderID.Trim()));
                parameters.Add(new SqlParameter("FamerCode", strFarmerCode.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetMaster_Details", "Table");
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

        public DataSet GetOperation(string type, string strHeaderID, string strFarmerCode)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                //List<SqlParameter> parameters = new List<SqlParameter>();
                //parameters.Add(new SqlParameter("Type", type.Trim()));
                //parameters.Add(new SqlParameter("HeaderID", strHeaderID.Trim()));
                //parameters.Add(new SqlParameter("FamerCode", strFarmerCode.Trim()));

                ////ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetMaster_Details", "Table");
                string strQry = "SELECT OPERATION_RECIPE   FROM[dbo].[GPIL_OPERATION_RECIPE] WHERE STATUS = 'Y'";
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
        public DataTable GetBaleandLotCount(string strHeaderID)
        {
            DataTable dt = new DataTable();
            try
            {
                //string sql2 = "select ISNULL(MAX(CONVERT(INT,TB_LOT_NO)),'0'),COUNT(GPIL_BALE_NUMBER) From GPIL_TAP_FARM_PURCHS_DTLS where HEADER_ID = '" + strHeaderID + DateTime.Now.ToString("yyyyMMdd") + "' AND REJE_STATUS='OK'";
                string strQry = "select ISNULL(MAX(CONVERT(INT,TB_LOT_NO)),'0') as LotCount,COUNT(GPIL_BALE_NUMBER) as BaleCount From GPIL_TAP_FARM_PURCHS_DTLS where HEADER_ID = '" + strHeaderID + "' AND REJE_STATUS='OK'";
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


        public DataSet GetORGN1(string type, string strHeaderID, string strFarmerCode)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Type", type.Trim()));
                parameters.Add(new SqlParameter("HeaderID", strHeaderID.Trim()));
                parameters.Add(new SqlParameter("FamerCode", strFarmerCode.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetMaster_Details", "Table");
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


        public DataTable GetBaleandLotCount1(string strHeaderID)
        {
            DataTable dt = new DataTable();
            try
            {
                //string sql2 = "select ISNULL(MAX(CONVERT(INT,TB_LOT_NO)),'0'),COUNT(GPIL_BALE_NUMBER) From GPIL_TAP_FARM_PURCHS_DTLS where HEADER_ID = '" + strHeaderID + DateTime.Now.ToString("yyyyMMdd") + "' AND REJE_STATUS='OK'";
                string strQry = "select ISNULL(MAX(CONVERT(INT,TB_LOT_NO)),'0') as LotCount,COUNT(GPIL_BALE_NUMBER) as BaleCount From GPIL_TAP_FARM_PURCHS_DTLS where HEADER_ID = '" + strHeaderID + "' AND REJE_STATUS='OK'";
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


        public DataSet GetQueryResultDs(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {

                ds = base.ODataServer.GetDataset( strQry);
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