using GPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class LPDManagementt : GPIObject
    {
        public DataSet GetVariety(string strOrgnCode)
        {
            DataSet ds = new DataSet();
            try
            {
                string strQry = "SELECT VARIETY FROM GPIL_ORGN_MASTER WHERE ORGN_CODE = '" + strOrgnCode + "'";
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


        public DataSet GetCompetetitionConsolidateReport(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {
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

        public DataSet GetCrop()
        {
            DataSet ds = new DataSet();
            try
            {
                string strQry = "select Crop,Crop_Year from GPIL_CROP_MASTER where status='Y'";
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
        public DataSet GetVariety()
        {
            DataSet ds = new DataSet();
            try
            {
                string strQry = "select Crop,Crop_Year from GPIL_CROP_MASTER where status='Y'";
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

        public DataSet GetCompetitionReport(string strOrgnCode, string strVariety, string strCrop, string strReportDate)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("ReportDate", strReportDate.Trim()));
                parameters.Add(new SqlParameter("OrgnCode", strOrgnCode.Trim()));
                parameters.Add(new SqlParameter("Crop", strCrop.Trim()));
                parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_CompetitionReport", "DataSet1");
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

        public DataSet GetTabPurchaseSummary(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {
                
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

        public DataSet GetCompetitionReport(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {
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
        public DataSet GetTabQuantityMarketed(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {

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


    }
}