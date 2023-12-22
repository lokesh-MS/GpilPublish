using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using GPI;
using System.Text;

namespace GPILWebApp.ViewModel.GLT
{
    public class GLTManagement : GPIObject
    {

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
    }
}