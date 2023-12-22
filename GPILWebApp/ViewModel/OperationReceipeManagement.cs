using GPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace GPI
{
    public class OperationReceipeManagement : GPIObject
    {


        public string GetOperationReceipeDetails(string strORCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select RECIPE_CODE from GPIL_OPERATION_RECIPE where RECIPE_CODE =" + strORCode + " ";
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

        public bool UserInsertOperationReceipe(OperationReciepeDetails or, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_OPERATION_RECIPE (RECIPE_CODE,OPERATION_RECIPE,STATUS,CREATED_BY,CREATED_DATE) values";
                    strQuery = strQuery + " ('" + or.RECIPECODE + "' ,'" + or.OPERATIONRECIPE + "','" + or.STATUS + "','" + or.CREATEDBY + "',getdate() ) ";

                }
                else if (strInsertUpdate == "UPDATE")
                {
                    strQuery = "Update GPIL_OPERATION_RECIPE set OPERATION_RECIPE='" + or.OPERATIONRECIPE + "' , ";
                    strQuery = strQuery + " Last_Updated_By ='" + or.CREATEDBY + "' , Last_Updated_date = getdate()  where RECIPE_CODE = '" + or.RECIPECODE + "'  ";
                    strQuery = strQuery + "";
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_OPERATION_RECIPE set STATUS='" + or.STATUS + "' ,Last_Updated_By ='" + or.CREATEDBY + "' ,Last_Updated_date = getdate()  where RECIPE_CODE = '" + or.RECIPECODE + "' ";
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


        public DataTable GetOperationReceipe(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "select RECIPE_CODE , OPERATION_RECIPE, 'V' as INS_STS from GPIL_OPERATION_RECIPE where status ='Y'";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_OPERATION_RECIPE where RECIPE_CODE ='" + strValue + "' ";
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