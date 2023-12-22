using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace GPI
{
    public class FarmerManagement : GPIObject
    {
        public string GetFarmerDetails(string strFarmerCode)
        {
            DataTable dt = new DataTable();
            string s = "";
            string strQuery = " ";
            try
            {
                strQuery = "select FARM_CODE from GPIL_FARMER_MASTER where FARM_CODE =" + strFarmerCode + "";
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

        public bool UserInsertFarmerDetails(FarmerDetails f, string strInsertUpdate)
        {
            bool b = false;
            string strQuery = "";
            try
            {
                if (strInsertUpdate == "INSERT")
                {
                    strQuery = "insert into GPIL_FARMER_MASTER (FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,STATUS,CREATED_BY,CREATED_DATE) values";
                    strQuery = strQuery + " ('" + f.FARMCODE + "' ,'" + f.FARMCATEGORY + "','" + f.FARMNAME + "','" + f.FARMFATHERNAME + "','" + f.VILLAGECODE + "', '" + f.SOILTYPE + "', '" + f.FARMADDRESS1 + "', '" + f.FARMADDRESS2 + "', '" + f.FARMADDRESS3 + "', '" + f.FARMADDRESS4 + "', '" + f.FARMADDRESS5 + "', '" + f.FARMADDRESS6 + "', '";
                    strQuery = strQuery + "'" + f.COUNTRY + "','" + f.PINCODE + "','" + f.TELNO + "','" + f.MOBILENO + "','" + f.EMAILID + "','" + f.BANKACCOUNTNO + "','" + f.BANKNAME + "','" + f.BRANCHNAME + "','" + f.IFSCCODE + "','" + f.STATUS + "','" + f.CREATEDBY + "',getdate() )";
                    strQuery = strQuery + "";

                    strQuery = "insert into GPIL_FARMER_CROP_HISTORY (HIS_CODE,FARM_CODE,CROP,VARIETY,LOAN_AMOUNT,BALANCE_AMOUNT,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,STATUS,CREATED_BY,CREATED_DATE) values";
                    strQuery = strQuery + " ('" + f.CROP + f.VARIETY + f.FARMCODE + "' ,'" + f.FARMCODE + "','" + f.CROP + "','" + f.VARIETY + "','" + f.LOANAMOUNT + "', '" + f.BALANCEAMOUNT + "', '" + f.MOBILENO + "', '" + f.EMAILID + "', '" + f.BANKACCOUNTNO + "', '" + f.BANKNAME + "', '" + f.BRANCHNAME + "', '" + f.IFSCCODE + "', '";
                    strQuery = strQuery + "'" + f.STATUS + "','" + f.CREATEDBY + "',getdate() )";
                }
                else if (strInsertUpdate == "UPDATE")
                {




                    strQuery = "Update GPIL_FARMER_MASTER set FARM_NAME='" + f.FARMNAME + "' , FARM_FATHER_NAME='" + f.FARMFATHERNAME + "' , VILLAGE_CODE='" + f.VILLAGECODE + "' , ";
                    strQuery = strQuery + " SOIL_TYPE='" + f.SOILTYPE + "' , FARM_ADDRESS1='" + f.FARMADDRESS1  + "' , FARM_ADDRESS2='" + f.FARMADDRESS1  + "' ,  ";
                    strQuery = strQuery + " FARM_ADDRESS3='" + f.FARMADDRESS3  + "' , FARM_ADDRESS4='" + f.FARMADDRESS4 + "' , FARM_ADDRESS5='" + f.FARMADDRESS5  + "' ,  ";
                    strQuery = strQuery + " FARM_ADDRESS6='" + f.FARMADDRESS6  + "' , COUNTRY='" + f.COUNTRY  + "' , MOBILE_NO='" + f.MOBILENO  + "' ,  ";
                    strQuery = strQuery + " EMAIL_ID='" + f.EMAILID  + "' , BANK_ACCOUNT_NO='" +f.BANKACCOUNTNO  +"' , BANK_NAME='" + f.BANKNAME  + "' ,  ";
                    strQuery = strQuery + " BRANCH_NAME='" + f.BRANCHNAME  + "' , IFSC_CODE='" + f.IFSCCODE  + "' , ";
                    strQuery = strQuery + " LAST_UPDATED_BY ='" + f.CREATEDBY + "' , LAST_UPDATED_DATE =getdate()  where FARM_CODE = '" + f.FARMCODE  + "'  ";




                    strQuery = "Update GPIL_FARMER_CROP_HISTORY set MOBILE_NO='" + f.MOBILENO  + "' , EMAIL_ID='" + f.EMAILID  + "' , BANK_ACCOUNT_NO='" + f.BANKACCOUNTNO  + "' , ";
                    strQuery = strQuery + " BANK_NAME='" + f.BANKNAME + "' , BRANCH_NAME='" + f.BRANCHNAME  + "' , IFSC_CODE='" + f.IFSCCODE  + "' ,  ";
                    strQuery = strQuery + " LAST_UPDATED_BY ='" + f.LASTUPDATEDBY   + "' , LAST_UPDATED_DATE =getdate()  where FARM_CODE = '" + f.FARMCODE  + "' and CROP = '" + f.CROP  + "' and VARIETY = '" + f.FARMCODE  + "' ";
                }
                else if (strInsertUpdate == "DELETE")
                {
                    strQuery = " Update GPIL_FARMER_MASTER set STATUS='" + f.STATUS + "' ,LAST_UPDATED_BY ='" + f.CREATEDBY + "' ,LAST_UPDATED_DATE =getdate()  where FARM_CODE = '" + f.FARMCODE + "' ";
                    strQuery = strQuery + "";

                    strQuery = " Update GPIL_FARMER_CROP_HISTORY set STATUS='" + f.STATUS + "' ,LAST_UPDATED_BY ='" + f.CREATEDBY + "' ,LAST_UPDATED_DATE =getdate()  where FARM_CODE = '" + f.FARMCODE + "' ";
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
        public DataTable GetFarmer(string strValue)
        {
            DataTable dt = new DataTable();
            string strQuery = " ";
            try
            {
                if (strValue == "0")
                {


                    strQuery = "select FARM_CODE , CROP,VARIETY ,FARMER_CATEGORY, FARMER_NAME, FARMER_FATHER_NAME, VILLAGE_CODE, SOIL_TYPE, FARMER_ADDRESS1, FARMER_ADDRESS2, COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO, EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE, 'V' as INS_STS from GPIL_FARMER_MASTER where STATUS ='Y' ";
                    dt = base.ODataServer.GetDataTable(strQuery);
                }
                else
                {
                    strQuery = "select * from GPIL_FARMER_MASTER where FARM_CODE ='" + strValue + "' ";
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