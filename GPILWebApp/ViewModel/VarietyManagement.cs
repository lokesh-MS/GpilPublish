using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for VarietyManagement
/// </summary>
/// 
namespace GPI
{

    public class VarietyManagement : GPIObject
    {
        //
        // TODO: Add constructor logic here
        //



        public bool UserInsertVariety(Variety vr, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_VARIETY_MASTER (Variety,Variety_Type,Variety_Name,Variety_desc,[Status],Created_By,Created_Date) values";
                    strQuery = strQuery + " ('"+vr.VarietyCode+ "' ,'" + vr.VarietyType + "','" + vr.VarietyName + "','" + vr.VarietyDesc + "','" + vr.Status + "','"+vr.CreatedBy+"',getdate() ) ";

                }
                else if (strInsertUpdate == "UPDATE")
                {
                    strQuery = "Update GPIL_VARIETY_MASTER set Variety_Type='" + vr.VarietyType + "' , Variety_Name='" + vr.VarietyName + "' , Variety_desc='" + vr.VarietyDesc + "' , ";
                    strQuery = strQuery + " Last_Updated_By ='" + vr.CreatedBy + "' , Last_Updated_date =getdate()  where Variety = '" + vr.VarietyCode + "'  ";
                    strQuery = strQuery + "";
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_VARIETY_MASTER set Status='" + vr.Status + "' ,Last_Updated_By ='" + vr.CreatedBy + "' ,Last_Updated_date =getdate  where Variety = '" + vr.VarietyCode + "' ";
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


        public string  GetVarietyDetails(string strVarietyCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select Variety from GPIL_VARIETY_MASTER where Variety =" + strVarietyCode + " ";
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


        public DataTable GetVariety(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "select VARIETY , VARIETY_TYPE, VARIETY_NAME, VARIETY_DESC, 'V' as INS_STS from GPIL_VARIETY_MASTER where status ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
               else
                {
                    strQuery = "select * from GPIL_VARIETY_MASTER where Variety ='"+ strValue+"' ";
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
