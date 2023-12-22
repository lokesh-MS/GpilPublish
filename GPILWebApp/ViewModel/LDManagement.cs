using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPI;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GPILWebApp.ViewModel
{
    public class LDManagement:GPIObject 
    {
        //public DataSet GetPurchaseSlip(string strOrgnCode, string strFarmerCode, string strAction)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("Action", strAction.Trim()));
        //        parameters.Add(new SqlParameter("OrgnCode", strOrgnCode.Trim()));
        //        parameters.Add(new SqlParameter("FarmerCode", strFarmerCode.Trim()));
        //        ds = base.ODataServer.ExecuteSP(parameters, "WSP_FarmerPurchaseSlip", "DataSet1");
        //    }
        //    catch (Exception ex)
        //    {
        //        StringBuilder err = new StringBuilder();
        //        err.Append(" Message : " + ex.Message);
        //        err.AppendLine(" STACK TRACE : " + ex.StackTrace);
        //        err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
        //        err.AppendLine(" SOURCE : " + ex.Source);
        //        Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
        //        return ds = null;
        //    }
        //    return ds;
        //}


        public DataTable FarmerAuthorisedQtyReport(string strFromDate, string strToDate, string strTodate, string strCrop, string strVariety)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FROMDATE", strFromDate));
                parameters.Add(new SqlParameter("TODATE", strToDate));
                parameters.Add(new SqlParameter("TODAYDATEE", strTodate));
                parameters.Add(new SqlParameter("CROPYEAR", strCrop));
                parameters.Add(new SqlParameter("variety", strVariety));
                dt = base.ODataServer.ExecuteSP(parameters, "SP_AUTHORISEDQTYREPORT");
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }


        public DataTable FarmerWisePurchaseSummary(string strCrop, string strVariety)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT ROW_NUMBER() OVER(ORDER BY FARMER_CODE) AS SNO,FARMER_CODE,F.FARM_NAME,F.FARM_FATHER_NAME AS FATHER_NAME, ";
                query = query + " FARM_ADDRESS1 AS VILLAGE,F.BANK_NAME AS BANK_NAME,'AC NO :'+ F.BANK_ACCOUNT_NO AS AccNo ,F.BRANCH_NAME , ";
                query = query + " F.IFSC_CODE , COUNT(GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY ,ROUND(SUM(NET_WT*RATE),2) ";
                query = query + " AS TOTAL_VALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_FARMER_MASTER F ";
                query = query + " WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' ";
                query = query + "  AND H.STATUS IN ('P','N') AND D.REJE_STATUS='OK' AND H.PURCHASE_TYPE = 'SUNDRY PURCHASE' ";
                query = query + " AND F.FARM_CODE=D.FARMER_CODE GROUP BY FARMER_CODE,F.FARM_NAME,F.FARM_FATHER_NAME,FARM_ADDRESS1, ";
                query = query + " F.BANK_NAME, F.BANK_ACCOUNT_NO  ,F.BRANCH_NAME ,F.IFSC_CODE union all select ROW_NUMBER() ";
                query = query + " OVER(ORDER BY F.FARM_CODE) AS SNO,f.FARM_CODE,F.FARM_NAME,F.FARM_FATHER_NAME AS  ";
                query = query + " FATHER_NAME,FARM_ADDRESS1 AS VILLAGE,F.BANK_NAME AS BANK_NAME,'AC NO :'+ F.BANK_ACCOUNT_NO AS AccNo , ";
                query = query + " F.BRANCH_NAME ,F.IFSC_CODE ,'0' AS BALES,'0' AS QUANTITY ,'0' AS TOTAL_VALUE from  ";
                query = query + " GPIL_FARMER_CROP_HISTORY H,GPIL_FARMER_MASTER F where f.FARM_CODE = H.FARM_CODE and ";
                query = query + " CROP ='" + strCrop + "' AND VARIETY='" + strVariety + "' ";
                query = query + " and H.FARM_CODE not in(SELECT FARMER_CODE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H, ";
                query = query + " GPIL_FARMER_MASTER F WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' ";
                query = query + " AND H.STATUS IN ('P','N') AND D.REJE_STATUS='OK' AND H.PURCHASE_TYPE = 'SUNDRY PURCHASE' AND ";
                query = query + " F.FARM_CODE=D.FARMER_CODE GROUP BY FARMER_CODE,F.FARM_NAME,F.FARM_FATHER_NAME,FARM_ADDRESS1, ";
                query = query + " F.BANK_NAME, F.BANK_ACCOUNT_NO  ,F.BRANCH_NAME ,F.IFSC_CODE)";
                dt = base.ODataServer.GetDataTable(query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }



        public DataTable GetFarmerList(string strHeaderID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select DISTINCT (TB_LOT_NO + ' - ' + FARMER_CODE) as FARMER_LOT,FARMER_CODE,CONVERT(INT,TB_LOT_NO) From GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) where HEADER_ID = '" + strHeaderID + "' ORDER BY CONVERT(INT,TB_LOT_NO)";
                dt = base.ODataServer.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }


        public DataTable GetFarmerPurchaseDetailsCount(string strHeaderID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " select ISNULL(MAX(CONVERT(INT, TB_LOT_NO)), '0') as LOT_NO,COUNT(GPIL_BALE_NUMBER) as BALES From GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK)where HEADER_ID = '" + strHeaderID + "'";
                dt = base.ODataServer.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }





        public DataTable FarmerPurchaseSlip333(string strOrgnCode, string strFarmerCode)
        {
            DataTable dt = new DataTable();
            string strQry = "";
            try
            {
                strQry = "SELECT F.FARM_NAME AS FARM_NAME,F.FARM_FATHER_NAME AS FARM_FATHER,H.HEADER_ID AS PURCH_REF,H.DATE_OF_PURCH AS DATE_OF_PURCH, ";
                strQry = strQry + "  V.VILLAGE_NAME,D.FARMER_CODE AS FARMER_CODE,D.TB_LOT_NO AS LOT_NO,D.GPIL_BALE_NUMBER AS GPIL_BALE_NUMBER, ";
                strQry = strQry + "   F.BANK_ACCOUNT_NO AS BANK_ACCOUNT_NO,F.IFSC_CODE AS IFSC_CODE,F.BANK_NAME AS BANK_NAME,F.BRANCH_NAME AS BRANCH_NAME, ";
                strQry = strQry + " FM.FREIGHT_CHARGE AS FREIGHT_CHARGE,F.LOAN_AMOUNT AS LOAN_AMOUNT,F.ALERT_MSG AS ALERT_MSG  FROM GPIL_TAP_FARM_PURCHS_DTLS D ";
                strQry = strQry + "  (NOLOCK),GPIL_TAP_FARM_PURCHS_HDR  H (NOLOCK),GPIL_FARMER_MASTER F (NOLOCK),GPIL_VILLAGE_MASTER V (NOLOCK), ";
                strQry = strQry + "  GPIL_FARMER_FREIGHT_CHARGE_MASTER FM (NOLOCK) WHERE H.HEADER_ID=D.HEADER_ID AND D.FARMER_CODE=F.FARM_CODE AND ";
                strQry = strQry + "  FM.VILLAGE_CODE=F.VILLAGE_CODE AND F.VILLAGE_CODE=V.VILLAGE_CODE AND FM.ORGN_CODE=H.ORGN_CODE AND FM.CROP=H.CROP ";
                strQry = strQry + "  AND FM.VARIETY=H.VARIETY AND H.ORGN_CODE='" + strOrgnCode + "' AND D.FARMER_CODE='" + strFarmerCode + "' ";
                strQry = strQry + "  AND H.HEADER_ID='" + strOrgnCode + "20150217'  ORDER BY D.GPIL_BALE_NUMBER ";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable TodateCRRRBales(string strCropYear, string strVariety, string strFromDate, string strFormatDate)
        {
            DataTable dt = new DataTable();
            string strQry = "";
            try
            {
                strQry = "SELECT ROW_NUMBER() OVER(ORDER BY TBL1.ORGN_CODE) AS SNO,TBL1.ORGN_CODE,ISNULL(TBL2.TODAY_OFFERED,0) AS TODAY_OFFERED, ";
                strQry = strQry + " ISNULL(TBL1.TODATE_OFFERED,0) AS TODATE_OFFERED,ISNULL(TBL3.TODAY_CR,0) AS TODAY_CR,ISNULL(TBL4.TODATE_CR,0) AS TODATE_CR, ";
                strQry = strQry + " ISNULL(TBL5.TODAY_RR,0) AS TODAY_RR,ISNULL(TBL6.TODATE_RR,0) AS TODATE_RR,ISNULL(TBL7.TODAY_SOLD,0) AS TODAY_SOLD, ";
                strQry = strQry + " ISNULL(TBL8.TODATE_SOLD,0) AS TODATE_SOLD FROM (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODATE_OFFERED ";
                strQry = strQry + " FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND ";
                strQry = strQry + " H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.DATE_OF_PURCH < '" + strFromDate + " 23:59:59'  ";
                strQry = strQry + " AND H.CROP='" + strCropYear + "' AND H.VARIETY='" + strVariety + "' GROUP BY H.ORGN_CODE) AS TBL1 FULL OUTER JOIN ";
                strQry = strQry + " (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODAY_OFFERED FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H ";
                strQry = strQry + " WHERE H.HEADER_ID=D.HEADER_ID AND H.HEADER_ID LIKE '%" + strFormatDate + "' ";
                strQry = strQry + " AND H.DATE_OF_PURCH < '" + strFromDate + " 23:59:59' AND H.PURCHASE_TYPE='SUNDRY PURCHASE' ";
                strQry = strQry + " AND H.CROP='" + strCropYear + "' AND H.VARIETY='" + strVariety + "' GROUP BY H.ORGN_CODE ) AS TBL2 ON ";
                strQry = strQry + " TBL2.ORGN_CODE=TBL1.ORGN_CODE FULL OUTER JOIN (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODAY_CR FROM ";
                strQry = strQry + " GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND H.HEADER_ID LIKE ";
                strQry = strQry + "  '%" + strFormatDate + "' AND H.DATE_OF_PURCH ";
                strQry = strQry + " < '" + strFromDate + " 23:59:59' AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND ";
                strQry = strQry + "  H.CROP='" + strCropYear + "' AND H.VARIETY='" + strVariety + "' AND D.REJE_STATUS='RJ' AND REJE_TYPE='CR' ";
                strQry = strQry + " GROUP BY H.ORGN_CODE ) AS TBL3 ON TBL3.ORGN_CODE=TBL1.ORGN_CODE FULL OUTER JOIN (SELECT H.ORGN_CODE, ";
                strQry = strQry + " COUNT(D.GPIL_BALE_NUMBER) AS TODATE_CR FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE ";
                strQry = strQry + " H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.DATE_OF_PURCH <  ";
                strQry = strQry + "  '" + strFromDate + " 23:59:59'  AND H.CROP='" + strCropYear + "' AND ";
                strQry = strQry + "  H.VARIETY='" + strVariety + "' AND D.REJE_STATUS='RJ' AND REJE_TYPE='CR' GROUP BY H.ORGN_CODE ) AS TBL4 ";
                strQry = strQry + "  ON TBL4.ORGN_CODE=TBL1.ORGN_CODE FULL OUTER JOIN (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODAY_RR FROM ";
                strQry = strQry + " GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND H.HEADER_ID ";
                strQry = strQry + " LIKE '%" + strFormatDate + "' AND H.DATE_OF_PURCH ";
                strQry = strQry + "  <  '" + strFromDate + " 23:59:59'  AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND ";
                strQry = strQry + "  H.CROP='" + strCropYear + "' AND H.VARIETY='" + strVariety + "' AND D.REJE_STATUS='RJ' ";
                strQry = strQry + " AND REJE_TYPE='RR' GROUP BY H.ORGN_CODE ) AS TBL5 ON TBL5.ORGN_CODE=TBL1.ORGN_CODE FULL OUTER JOIN ";
                strQry = strQry + "  (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODATE_RR FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H ";
                strQry = strQry + " WHERE H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.DATE_OF_PURCH <  ";
                strQry = strQry + "   '" + strFromDate + " 23:59:59'  AND H.CROP='" + strCropYear + "' AND ";
                strQry = strQry + " H.VARIETY='" + strVariety + "' AND D.REJE_STATUS='RJ' AND REJE_TYPE='RR' GROUP BY H.ORGN_CODE ) AS TBL6 ";
                strQry = strQry + "  ON TBL6.ORGN_CODE=TBL1.ORGN_CODE FULL OUTER JOIN (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODAY_SOLD FROM ";
                strQry = strQry + "  GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND H.HEADER_ID ";
                strQry = strQry + " LIKE '%" + strFormatDate + "' AND ";
                strQry = strQry + "  H.DATE_OF_PURCH < '" + strFromDate + " 23:59:59' AND H.PURCHASE_TYPE='SUNDRY PURCHASE' ";
                strQry = strQry + "  AND H.CROP='" + strCropYear + "' AND H.VARIETY='" + strVariety + "' AND D.REJE_STATUS='OK' GROUP BY H.ORGN_CODE ) ";
                strQry = strQry + "  AS TBL7 ON TBL7.ORGN_CODE=TBL1.ORGN_CODE FULL OUTER JOIN (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TODATE_SOLD ";
                strQry = strQry + "  FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND ";
                strQry = strQry + "  H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.DATE_OF_PURCH < '" + strFromDate + " 23:59:59' AND ";
                strQry = strQry + " H.CROP='" + strCropYear + "' AND H.VARIETY='" + strVariety + "' AND D.REJE_STATUS='OK' GROUP BY H.ORGN_CODE ) ";
                strQry = strQry + " AS TBL8 ON TBL8.ORGN_CODE=TBL1.ORGN_CODE ORDER BY  TBL1.ORGN_CODE";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }




        public DataTable FarmerPurchaseCRRRBalesDetails(string strFromDate, string strToDate, string strCrop, string strVariety)
        {
            DataTable dt = new DataTable();
            string strQry = "";

            try
            {
                string strConfition = "";
                if (strFromDate.Length != 0 && strToDate.Length != 0)
                {
                    strConfition = " AND convert(varchar,H.DATE_OF_PURCH,103) BETWEEN '" + strFromDate + " ' AND '" + strToDate +"' ";

                }

                strQry = " SELECT ROW_NUMBER() OVER(ORDER BY TBL1.HEADER_ID,FARMER_CODE,GPIL_BALE_NUMBER) AS SNO,ORGN_CODE,TBL1.HEADER_ID,ISNULL(TOTAL_OFFERED_BALES,0) AS TOTAL_OFFERED_BALES,ISNULL(TOTAL_REJ_BALES,0) AS TOTAL_REJ_BALES,FARMER_CODE,FARM_NAME,VILLAGE_CODE,VILLAGE_NAME,GPIL_BALE_NUMBER,REMARKS,REJE_TYPE,CREATED_DATE,LOT_NO FROM " +
                        " (SELECT H.ORGN_CODE AS ORGN_CODE,FARMER_CODE,FARM_NAME,V.VILLAGE_CODE,V.VILLAGE_NAME,GPIL_BALE_NUMBER,ISNULL(REMARKS,'') AS REMARKS,REJE_TYPE,D.CREATED_DATE, 'Lot : ' + TB_LOT_NO  + ' - ' + D.ATTRIBUTE3 AS LOT_NO,H.HEADER_ID FROM GPIL_TAP_FARM_PURCHS_HDR H,GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_FARMER_MASTER F,GPIL_VILLAGE_MASTER V  WHERE V.VILLAGE_CODE=F.VILLAGE_CODE AND REJE_STATUS='RJ' AND  FARM_CODE=FARMER_CODE AND H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' " + strConfition + ") AS TBL1 LEFT OUTER JOIN " +
                        " (SELECT H.HEADER_ID ,COUNT(GPIL_BALE_NUMBER) AS TOTAL_OFFERED_BALES FROM GPIL_TAP_FARM_PURCHS_HDR H,GPIL_TAP_FARM_PURCHS_DTLS D  WHERE H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' " + strConfition + " GROUP BY  H.HEADER_ID) AS TBL2 ON TBL1.HEADER_ID=TBL2.HEADER_ID LEFT OUTER JOIN " +
                        " (SELECT H.HEADER_ID ,COUNT(GPIL_BALE_NUMBER) AS TOTAL_REJ_BALES FROM GPIL_TAP_FARM_PURCHS_HDR H,GPIL_TAP_FARM_PURCHS_DTLS D  WHERE REJE_STATUS='RJ' AND  H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='SUNDRY PURCHASE' AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' " + strConfition + " GROUP BY  H.HEADER_ID) AS TBL3 ON TBL1.HEADER_ID=TBL3.HEADER_ID ";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }





        public DataTable FarmerPurchaseSlip(string strOrgnCode, string strFarmerCode)
        {
            DataTable dt = new DataTable();
            string strQry = "";
            try
            {
                strQry = "SELECT F.FARM_NAME AS FARM_NAME,F.FARM_FATHER_NAME AS FARM_FATHER,H.HEADER_ID AS PURCH_REF,H.DATE_OF_PURCH AS DATE_OF_PURCH, ";
                strQry = strQry + "  V.VILLAGE_NAME,D.FARMER_CODE AS FARMER_CODE,D.TB_LOT_NO AS LOT_NO,D.GPIL_BALE_NUMBER AS GPIL_BALE_NUMBER, ";
                strQry = strQry + "   F.BANK_ACCOUNT_NO AS BANK_ACCOUNT_NO,F.IFSC_CODE AS IFSC_CODE,F.BANK_NAME AS BANK_NAME,F.BRANCH_NAME AS BRANCH_NAME, ";
                strQry = strQry + " FM.FREIGHT_CHARGE AS FREIGHT_CHARGE,F.LOAN_AMOUNT AS LOAN_AMOUNT,F.ALERT_MSG AS ALERT_MSG  FROM GPIL_TAP_FARM_PURCHS_DTLS D ";
                strQry = strQry + "  (NOLOCK),GPIL_TAP_FARM_PURCHS_HDR  H (NOLOCK),GPIL_FARMER_MASTER F (NOLOCK),GPIL_VILLAGE_MASTER V (NOLOCK), ";
                strQry = strQry + "  GPIL_FARMER_FREIGHT_CHARGE_MASTER FM (NOLOCK) WHERE H.HEADER_ID=D.HEADER_ID AND D.FARMER_CODE=F.FARM_CODE AND ";
                strQry = strQry + "  FM.VILLAGE_CODE=F.VILLAGE_CODE AND F.VILLAGE_CODE=V.VILLAGE_CODE AND FM.ORGN_CODE=H.ORGN_CODE AND FM.CROP=H.CROP ";
                strQry = strQry + "  AND FM.VARIETY=H.VARIETY AND H.ORGN_CODE='" + strOrgnCode + "' AND D.FARMER_CODE='" + strFarmerCode + "' ";
                strQry = strQry + "  AND H.HEADER_ID='" + strOrgnCode + "20150217'  ORDER BY D.GPIL_BALE_NUMBER ";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }




        public DataSet GetFarmerLoanStatus(string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Crop", strCrop.Trim()));
                parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetFarmerLoanDetailsLD", "Table");
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

        public DataSet GetTTDCRRRDetails(string strFromDate, string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FromDate", strFromDate.Trim()));
                parameters.Add(new SqlParameter("Crop", strCrop.Trim()));
                parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetTTDCRRRDetailsLD", "Table");
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

        public DataSet GetFarmerCRRRBaleDetails(string strFromDate, string strToDate, string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FromDate", strFromDate.Trim()));
                parameters.Add(new SqlParameter("ToDate", strToDate.Trim()));
                parameters.Add(new SqlParameter("Crop", strCrop.Trim()));
                parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetFarmerCRRRBaleDetails", "Table");
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




        public DataSet GetFarmerAuthorisedQty(string strFromDate, string strToDate, string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            try
            {
                string s = strFromDate + " 00:00:00";
                string s1 = strToDate + " 23:59:59";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FROMDATE", s));
                parameters.Add(new SqlParameter("TODATE", s1)); 
                parameters.Add(new SqlParameter("TODAYDATEE", strToDate));
                parameters.Add(new SqlParameter("CROPYEAR", strCrop.Trim()));
                parameters.Add(new SqlParameter("variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "SP_AUTHORISEDQTYREPORT", "Table");
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

        public DataSet GetFarmerPurchaseVerifySummaryReport(string strHeaderID, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
             
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("HeaderID", strHeaderID));
                parameters.Add(new SqlParameter("ReportType", strReportType));
           
                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetFarmerVerificationPurchaseSummaryReport", "Table");
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


        public DataSet GetFarmerPurchaseSummaryDetails(string strHeaderID, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("HeaderID", strHeaderID));
                parameters.Add(new SqlParameter("ReportType", strReportType));

                ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetFarmerPurchaseSummaryReportDetails", "Table");
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




        //public DataSet GetFarmerLoanStatus(string strCrop, string strVariety)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        // parameters.Add(new SqlParameter("Action", strAction.Trim()));
        //        parameters.Add(new SqlParameter("Crop", strCrop.Trim()));
        //        parameters.Add(new SqlParameter("Variety", strVariety.Trim()));
        //        ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetFarmerLoanDetailsLD", "Table");
        //    }
        //    catch (Exception ex)
        //    {
        //        StringBuilder err = new StringBuilder();
        //        err.Append(" Message : " + ex.Message);
        //        err.AppendLine(" STACK TRACE : " + ex.StackTrace);
        //        err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
        //        err.AppendLine(" SOURCE : " + ex.Source);
        //        Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
        //        return ds = null;
        //    }
        //    return ds;
        //}




            /// <summary>
            /// LDD Farmer Authorized Quantity Report
            /// </summary>
            /// <param name="strCrop"></param>
            /// <param name="strVariety"></param>
            /// <returns></returns>

        public DataSet GetFarmerAuthorizedQty(string strFromDate, string strTodate, string strCrop, string strVariety)
        {
            DataSet ds = new DataSet();
            DateTime dt = Convert.ToDateTime(strFromDate);
            strFromDate = dt.ToString("dd-MM-yyyy");
            DateTime dt1 = Convert.ToDateTime(strTodate);
            strTodate = dt1.ToString("dd-MM-yyyy");


            string s = strFromDate + " 00:00:00";
            string s1 = strTodate + " 23:59:59";
            string s2 = strFromDate; 
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FROMDATE", s.Trim()));
                parameters.Add(new SqlParameter("TODATE", s1.Trim()));
                parameters.Add(new SqlParameter("TODAYDATEE", s2.Trim()));
                parameters.Add(new SqlParameter("CROPYEAR", strCrop.Trim()));
                parameters.Add(new SqlParameter("variety", strVariety.Trim()));
                ds = base.ODataServer.ExecuteSP(parameters, "SP_AUTHORISEDQTYREPORT", "Table");
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