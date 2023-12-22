using GPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace GPI
{
    public class OrganizationManagement : GPIObject
    {
        public string GetOrganizationDetails(string strOrgnCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select ORGN_CODE from GPIL_ORGN_MASTER where ORGN_CODE = " + strOrgnCode + " ";
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
        public bool UserInsertOrganization(OrganizatioDetails od, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_ORGN_MASTER (ORGN_CODE,ORGN_NAME,ORGN_TYPE,ORGN_ADDRESS1,ORGN_ADDRESS2,ORGN_ADDRESS3,ORGN_ADDRESS4,ORGN_ADDRESS5,";
                    strQuery = strQuery + " ORGN_ADDRESS6,ORGN_COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,INSURANCE_VAL,STATUS,CREATED_BY,CREATED_DATE,VARIETY) values";

                    strQuery = strQuery + " ('" + od.ORGNCODE + "' ,'" + od.ORGNNAME + "','" + od.ORGNTYPE + "','" + od.ORGNADDRESS1 + "','" + od.ORGNADDRESS2 + "','" + od.ORGNADDRESS3 + "', ";
                    strQuery = strQuery + "  '" + od.ORGNADDRESS4 + "' ,'" + od.ORGNADDRESS5 + "','" + od.ORGNADDRESS6 + "','" + od.ORGNCOUNTRY + "','" + od.PINCODE + "','" + od.TELNO + "', ";
                    strQuery = strQuery + " '" + od.MOBILENO + "' ,'" + od.EMAILID + "','" + od.INSURANCEVAL + "','" + od.STATUS + "','" + od.CREATEDBY + "',getdate(),'" + od.VARIETY + "') ";
                }
                else if (strInsertUpdate == "UPDATE")
                {
                    strQuery = "Update GPIL_ORGN_MASTER set ORGN_NAME='" + od.ORGNNAME + "' , ORGN_TYPE='" + od.ORGNTYPE + "' , ORGN_ADDRESS1='" + od.ORGNADDRESS1 + "' , ";
                    strQuery = strQuery + " ORGN_ADDRESS2 ='" + od.ORGNADDRESS2 + "' ,TEL_NO ='" + od.TELNO + "' ,MOBILE_NO ='" + od.MOBILENO + "' ,EMAIL_ID ='" + od.EMAILID + "' ,";
                    strQuery = strQuery + "LAST_UPDATED_BY ='" + od.CREATEDBY + "' ,LAST_UPDATED_DATE =getdate() ";
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_ORGN_MASTER set STATUS='" + od.STATUS + "' ,LAST_UPDATED_BY ='" + od.CREATEDBY + "' ,LAST_UPDATED_DATE =getdate()  where ORGN_CODE = '" + od.ORGNCODE + "' ";
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


        public DataTable GetOrganization(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "select ORGN_CODE, ORGN_NAME, ORGN_TYPE, VARIETY, ORGN_ADDRESS1, ORGN_ADDRESS2, ORGN_ADDRESS3, ORGN_COUNTRY, PIN_CODE, TEL_NO, MOBILE_NO, EMAIL_ID, INSURANCE_VAL 'V' as INS_STS  from GPIL_ORGN_MASTER where status ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_ORGN_MASTER where ORGN_CODE ='" + strValue + "' ";
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