using GPI;
using GPIWebApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class RDLCReport : GPIObject
    {
        public DataSet GetPurchaseSlip(string strOrgnCode, string strFarmerCode,string strAction)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Action", strAction.Trim()));
                parameters.Add(new SqlParameter("OrgnCode", strOrgnCode.Trim()));
                parameters.Add(new SqlParameter("FarmerCode", strFarmerCode.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_FarmerPurchaseSlip", "FARMER_PURCHASESLIP");
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

        public DataSet FarmerPurchaseSummary(string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Crop", strCrop.Trim()));
                parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_FarmerPurchaseSummary", "Table");
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