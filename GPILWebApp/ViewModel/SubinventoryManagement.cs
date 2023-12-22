using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace GPI
{
    public class SubinventoryManagement:GPIObject
    {

        public string GetSubInventoryDetails(string strSubInvCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select SUB_INV_CODE from GPIL_SUBINVENTORY where SUB_INV_CODE =" + strSubInvCode + " ";
                dt = base.ODataServer.GetDataTable(strQuery);

                if (dt.Rows.Count > 0)
                    s = "Y";
                else
                    s = "N";
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                s = "";

            }
            return s;
        }

        public bool UserInsertSubInventory(SubinventoryDetails si, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_SUBINVENTORY (SUB_INV_CODE,SUB_INV_DESC,Status,CREATED_BY,CREATED_DATE) values";
                    strQuery = strQuery + " ('" + si.SUBINVCODE + "' ,'" + si.SUBINVDESC + "','" + si.STATUS + "','" + si.CREATEDBY + "',getdate() ) ";

                }
                else if (strInsertUpdate == "UPDATE")
                {
                    strQuery = "Update GPIL_SUBINVENTORY set SUB_INV_DESC='" + si.SUBINVDESC + "' , ";
                    strQuery = strQuery + " LAST_UPDATED_BY ='" + si.CREATEDBY + "' , LAST_UPDATED_DATE =getdate()  where SUB_INV_CODE = '" + si.SUBINVCODE + "'  ";
                    strQuery = strQuery + "";
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_SUBINVENTORY set Status='" + si.STATUS + "' ,LAST_UPDATED_BY ='" + si.CREATEDBY + "' ,LAST_UPDATED_DATE =getdate()  where SUB_INV_CODE = '" + si.SUBINVCODE + "' ";
                    strQuery = strQuery + "";
                }

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

        public DataTable GetSubInventory(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "select SUB_INV_CODE , SUB_INV_DESC, 'V' as INS_STS from GPIL_SUBINVENTORY where STATUS ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_SUBINVENTORY where SUB_INV_CODE ='" + strValue + "' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
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
                dt = null;

            }
            return dt;
        }
    }
}