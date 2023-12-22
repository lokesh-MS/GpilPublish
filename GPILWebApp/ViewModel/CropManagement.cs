using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using GPI;

namespace GPILWebApp.ViewModel
{
    public class CropManagement : GPIObject
    {

        public string GetCropDetails(string strCropCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select CROP from GPIL_CROP_MASTER where Crop =" + strCropCode + " ";
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

        public bool UserInsertCrop(Crop cr, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_CROP_MASTER (CROP,CROP_YEAR,Status,Created_By,Created_Date) values";
                    strQuery = strQuery + " ('" + cr.CROP + "' ,'" + cr.CROPYEAR + "','" + cr.STATUS + "','" + cr.CREATEDBY + "',getdate() ) ";

                }
                else if (strInsertUpdate == "UPDATE")
                {
                    strQuery = "Update GPIL_CROP_MASTER set CROP_YEAR='" + cr.CROPYEAR + "' , Last_Updated_By='" + cr.CREATEDBY + "' , Last_Updated_date=getdate() , ";
                    strQuery = strQuery + "  where CROP = '" + cr.CROP + "'  ";
                    strQuery = strQuery + "";
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_CROP_MASTER set Status='" + cr.STATUS + "' ,Last_Updated_By ='" + cr.CREATEDBY + "' ,Last_Updated_date =getdate()  where CROP = '" + cr.CROP + "' ";
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
        

            public DataTable GetJes(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "select SNO, CROP, CROP_YEAR, ATTRIBUTE1 from GPIL_CROP_MASTER where status ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_CROP_MASTER where CROP ='" + strValue + "' ";
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

        public DataTable AddJes(string strValue, string strcrp, string strcrpyr, string strshrtCY)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "insert into GPIL_CROP_MASTER (CROP,CROP_YEAR,ATTRIBUTE1) values ('" + strcrp + "', '" + strcrpyr + "', '" + strshrtCY + "')";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_CROP_MASTER where CROP ='" + strValue + "' ";
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
        public DataTable GetCrop(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {
                    strQuery = "select CROP, CROP_YEAR, 'V' as INS_STS from GPIL_CROP_MASTER where status ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_CROP_MASTER where CROP ='" + strValue + "' ";
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