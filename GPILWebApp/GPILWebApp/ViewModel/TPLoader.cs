using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp;
using GPI;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GPIWebApp;

namespace GPILWebApp.ViewModel
{
    public class TPLoader : GPIObject
    {

        public DataTable GetServiceAmount(string strAction)
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "";
                if (strAction == "1")
                {
                    strQry = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='OTHERS' order by SNO";
                }
                else if (strAction == "2")
                {
                    strQry = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='OTHERS' order by SNO";
                }
                else if (strAction == "3")
                {
                    strQry = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='SERVICE SH EDUCATION CESS' order by SNO";
                }
                else if (strAction == "4")
                {
                    strQry = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='SERVICE TAX-EDUCATION CESS' order by SNO";
                }
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

        public DataTable GetRejectionType()
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "";
                strQry = "select REJ_TYPE From GPIL_REJECTION_TYPE(NOLOCK) GROUP BY REJ_TYPE";
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


        public DataTable GetGrade(string strGrade)
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "";
                strQry = "select * from GPIL_ITEM_MASTER(NOLOCK) where ITEM_CODE ='" + strGrade + "'";
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

        public DataTable GetBalNumber(string strBalNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "";
                strQry = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + strBalNo + "'";
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



        //public bool InsertTapPurchase(string strHeaderID, string strOrgnCode, string strBuyerCode, string strPurchaseDoc, string strDateofPurch, string strCrop,
        //    double strValue, string strVariety, string strEmpCode, string strBalNo, string strLotNo, string strRejests, string strNetWeight, string strTgNo, string strTBGNo,
        //    string strTBGGrade, string strRate, string strStatus, string strRejeType, string strPattaCharge, string strServicechargeid,
        //   string strStrRejeType, string strServiceChargeAmt, string strServiceChargeedShid, string strServiceChargeshcessid, string strServiceTaxId, string strTotalServiceTaxAmt)
        //{
        //    List<string> lstQuery = new List<string>(15);
        //    string strQry = "";
        //    bool b = false;
        //    try
        //    {
        //        strQry = "";

        //        strQry = "INSERT INTO [GPIL_TAP_FARM_PURCHS_HDR]([HEADER_ID],[ORGN_CODE],[PURCHASE_TYPE],[BUYER_CODE],[PURCH_DOC_NO],[DATE_OF_PURCH],[CROP],[VARIETY],[CREATED_BY],[CREATION_DATE],[STATUS])";
        //        strQry = strQry + " VALUES('" + strHeaderID + "','" + strOrgnCode + "','TAP PURCHASE','" + strBuyerCode + "','" + strPurchaseDoc + "','" + Convert.ToDateTime(strDateofPurch) + "','" + strCrop + "','" + strVariety + "','" + strEmpCode + "',GETDATE(),'P')";


        //        lstQuery.Add(strQry);

        //        strQry = "";


        //        strQry = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,TB_LOT_NO,TBGR_NO,TB_GRADE,BUYER_GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRICE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
        //        strQry = strQry + "Values('" + strBalNo + "','FW','" + strLotNo + "','" + strTBGNo + "','" + strTBGGrade + "','" + strBuyerCode + "','" + strNetWeight + "','" + strNetWeight + "','LOC1','" + strOrgnCode + "','" + strCrop + "','" + strVariety + "','" + strOrgnCode + "','G','N','" + strHeaderID + "','" + strStatus + "','" + strEmpCode + "',GETDATE(),'" + strEmpCode + "',GETDATE(),'N','TAP','LOC1','" + strOrgnCode + "')";

        //        lstQuery.Add(strQry);


        //        strQry = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[TBGR_NO],[TB_GRADE],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[PATTA_CHARGE],[SERVICE_CHARGE],[SERVICE_CHARGE_AMT],[SERVICE_TAX],[SERVICE_TAX_AMT],[CREATED_BY],[CREATED_DATE] ,[SH_ED_TAX],[ED_CESS_TAX])";
        //        if (strRejeType == "NONE")
        //        {
        //            strQry = strQry + "Values('" + strHeaderID + "','" + strBalNo + "','" + strLotNo + "','" + strTBGNo + "','" + strTBGGrade + "','" + strNetWeight + "','" + strRate + "','" + strValue + "','" + strBuyerCode + "','" + strCrop + "','" + strVariety + "','FW','" + strRejests + "',NULL,'" + strStatus + "','N','" + strPattaCharge + "','" + strServicechargeid + "',ROUND('" + strServiceChargeAmt + "',2),'" + strServiceTaxId + "',ROUND('" + strTotalServiceTaxAmt + "',2),'" + strEmpCode + "',GETDATE(),'" + strServiceChargeedShid + "','" + strServiceChargeshcessid + "')";
        //        }
        //        else
        //        {
        //            strQry = strQry + "Values('" + strHeaderID + "','" + strBalNo + "','" + strLotNo + "','" + strTBGNo + "','" + strTBGGrade + "','" + strNetWeight + "','" + strRate + "','" + strValue + "','" + strBuyerCode + "','" + strCrop + "','" + strVariety + "','FW','" + strRejests + "','" + strRejeType + "','" + strStatus + "','N','" + strPattaCharge + "','" + strServicechargeid + "','" + strServiceChargeAmt + "','" + strServiceTaxId + "','" + strTotalServiceTaxAmt + "','" + strEmpCode + "',GETDATE(),'" + strServiceChargeedShid + "','" + strServiceChargeshcessid + "')";
        //        }
        //        lstQuery.Add(strQry);

        //        b = base.ODataServer.Transaction(lstQuery);



        //    }
        //    catch (Exception ex)
        //    {
        //        return b;
        //    }
        //    return b;
        //}


        public bool InsertTapPurchase(List<string> lstQuery)
        {
            bool b = false;
            try
            {
                b = base.ODataServer.Transaction(lstQuery);
            }
            catch (Exception ex)
            {
                return b;
            }
            return b;
        }


        public DataTable GetFarmerLoanAmount(string strFarmerCode, string strCrop, string strVariety)
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "";
                strQry = "SELECT FARM_CODE, LOAN_AMOUNT, BALANCE_AMOUNT FROM GPIL_FARMER_CROP_HISTORY(NOLOCK) WHERE FARM_CODE = '" + strFarmerCode + "' AND CROP = '" + strCrop + "' AND VARIETY = '" + strVariety + "'";
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
        
        public bool InsertFarmerLoan(List<string> lstQuery)
        {               
           
            bool b = false;
            try
            {                
                b = base.ODataServer.Transaction(lstQuery);
            }
            catch (Exception ex)
            {
                return b;
            }
            return b;
        }



        public bool FormerExistsorNot(string strCrop,string strVariety,string strFarmerCode)
        {
            DataTable dt = new DataTable();
            bool b = false;
            try
            {               
                string strQry = "SELECT F.FARM_CODE FROM GPIL_FARMER_MASTER(NOLOCK) F,GPIL_FARMER_CROP_HISTORY(NOLOCK) FC,GPIL_VARIETY_SEASON_MASTER(NOLOCK) VS WHERE F.FARM_CODE=FC.FARM_CODE AND VS.VARIETY=FC.VARIETY AND VS.CROP=FC.CROP AND FC.CROP='" + strCrop + "' AND FC.VARIETY='" + strVariety + "' AND FC.FARM_CODE='" + strFarmerCode + "'";
                dt = base.ODataServer.GetDataTable(strQry);
                if(dt.Rows.Count >0)                
                    b= true;               
                else               
                    b= false;               
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

        public bool CheckFarmer(string strFarmerCode)
        {
            DataTable dt = new DataTable();
            bool b = false;
            try
            {
                string strQry = "SELECT FARM_CODE FROM GPIL_FARMER_MASTER WHERE FARM_CODE='" + strFarmerCode + "'";
                dt = base.ODataServer.GetDataTable(strQry);
                if (dt.Rows.Count > 0)
                    b = true;
                else
                    b = false;
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


        public bool CheckVariety(string strVariety)
        {
            DataTable dt = new DataTable();
            bool b = false;
            try
            {
                string strQry = "SELECT * FROM GPIL_VARIETY_MASTER WHERE VARIETY='" + strVariety + "'";
                dt = base.ODataServer.GetDataTable(strQry);
                if (dt.Rows.Count > 0)
                    b = true;
                else
                    b = false;
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

        public bool CheckCropVariety(string strVariety,string strCrop)
        {
            DataTable dt = new DataTable();
            bool b = false;
            try
            {
                string strQry = "SELECT * FROM GPIL_VARIETY_SEASON_MASTER WHERE CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";
                dt = base.ODataServer.GetDataTable(strQry);
                if (dt.Rows.Count > 0)
                    b = true;
                else
                    b = false;
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


        public DataTable GetFarmerCodeCropHistory(string strFarmerCode, string strCrop, string strVariety)
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "";
                strQry = "SELECT FARM_CODE FROM GPIL_FARMER_CROP_HISTORY(NOLOCK) WHERE FARM_CODE = '" + strFarmerCode + "' AND CROP = '" + strCrop + "' AND VARIETY = '" + strVariety + "'";
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