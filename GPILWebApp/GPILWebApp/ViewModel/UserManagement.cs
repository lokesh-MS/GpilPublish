using GPI;
using GPIWebApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace GPI
{
    public class UserManagement :GPIObject
    {
        public string GetUserDetails(string strUserCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select USER_ID from GPIL_USER_MASTER where USER_ID =" + strUserCode + "";
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


        public bool UserInsertUserDetails(UserDetails u, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_USER_MASTER (USER_ID,USER_NAME,PASSWORD,USER_ERP_NAME,EMP_CODE,DESIGNATION,DEPARTMENT,MOBILE_NO,EMAIL_ID,STATUS,CREATED_BY,CREATED_DATE) values";
                    strQuery = strQuery + " ('" + u.USERID + "' ,'" + u.USERNAME + "','" + u.PASSWORD + "','" + u.USERERPNAME + "','" + u.EMPCODE + "', ";
                    strQuery = strQuery + "'" + u.DESIGNATION + "','" + u.DEPARTMENT + "','" + u.MOBILENO + "','" + u.EMAILID + "','" + u.STATUS + "','" + u.CREATEDBY + "',getdate() )";
                    strQuery = strQuery + "";
                }
                else if (strInsertUpdate == "UPDATE")
                {
                    strQuery = "Update GPIL_USER_MASTER set USER_NAME='" + u.USERNAME + "' , USER_ERP_NAME='" + u.USERERPNAME + "' , DESIGNATION='" + u.DESIGNATION + "' , ";

                    strQuery = strQuery + " DEPARTMENT='" + u.DEPARTMENT + "' , MOBILE_NO='" + u.MOBILENO + "' , EMAIL_ID='" + u.EMAILID + "' ,  ";
                    strQuery = strQuery + " LAST_UPDATED_BY ='" + u.CREATEDBY + "' , LAST_UPDATED_DATE =getdate()  where USER_ID = '" + u.USERID + "'  ";
                    
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_USER_MASTER set STATUS='" + u.STATUS + "' ,LAST_UPDATED_BY ='" + u.CREATEDBY + "' ,LAST_UPDATED_DATE =getdate()  where USER_ID = '" + u.USERID + "' ";
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


        public DataTable GetUser(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {

                    
                    strQuery = "select USER_ID , USER_NAME, PASSWORD, USER_ERP_NAME, EMP_CODE, DESIGNATION, DEPARTMENT, USER_RIGHTS, MOBILE_NO, EMAIL_ID, 'V' as INS_STS from GPIL_USER_MASTER where STATUS ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_USER_MASTER where USER_ID ='" + strValue + "' ";
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