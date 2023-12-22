using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using GPI;
using System.Web;
using System.Text;
using GPIWebApp;

namespace GPILWebApp.ViewModel
{
    public class PPDManagement : GPIObject
    {

        public DataSet GetClassificationRptforTap(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {

                ds = base.ODataServer.GetDataset(strQry);
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




        public DataSet GetGradingDetailsIssue(string strBatchNumber, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("GradingBatchNumber", strBatchNumber));
                if (strReportType == "ISSUE")
                {
                    parameters.Add(new SqlParameter("ReportType", strReportType));
                    ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetGradingDetailsPPD", "Table");
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
                return ds = null;
            }
            return ds;
        }


        public DataSet GetGradingDetailsOutProduct(string strBatchNumber, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("GradingBatchNumber", strBatchNumber));

                if (strReportType == "OUTPRODUCT")
                {
                    parameters.Add(new SqlParameter("ReportType", strReportType));
                    ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetGradingDetailsPPD", "Table");
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
                return ds = null;
            }
            return ds;
        }

        public DataSet GetGradingDetailsByProduct(string strBatchNumber, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("GradingBatchNumber", strBatchNumber));
                if (strReportType == "BYPRODUCT")
                {
                    parameters.Add(new SqlParameter("ReportType", strReportType));
                    ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetGradingDetailsPPD", "Table");
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
                return ds = null;
            }
            return ds;
        }



        public DataSet GetGradingTempDetailsIssue(string strBatchNumber, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("GradingBatchNumber", strBatchNumber));
                if (strReportType == "ISSUE")
                {
                    parameters.Add(new SqlParameter("ReportType", strReportType));
                    ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetGradingTempDetails", "Table");
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
                return ds = null;
            }
            return ds;
        }

        public DataSet GetGradingTempDetailsOutProduct(string strBatchNumber, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("GradingBatchNumber", strBatchNumber));

                if (strReportType == "OUTPRODUCT")
                {
                    parameters.Add(new SqlParameter("ReportType", strReportType));
                    ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetGradingTempDetails", "Table");
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
                return ds = null;
            }
            return ds;
        }


        public DataSet GetGradingTempDetailsByProduct(string strBatchNumber, string strReportType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("GradingBatchNumber", strBatchNumber));

                if (strReportType == "BYPRODUCT")
                {
                    parameters.Add(new SqlParameter("ReportType", strReportType));
                    ds = base.ODataServer.ExecuteSP(parameters, "WSP_GetGradingTempDetails", "Table");
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
                return ds = null;
            }
            return ds;
        }


        public DataSet GetClassificationReport(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {

                ds = base.ODataServer.GetDataset(strQry);
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


        public DataSet GetDataSet(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {

                ds = base.ODataServer.GetDataset(strQry);
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




        public DataSet GetClassCumReportVKBUComplete(string varStrValue, string strFromDate, string strTodate, string strCrop, string strVariety, string strOrgnCode)
        {
            string strsql = "";
            
            DataSet ds = new DataSet();
            try
            {
                if (strOrgnCode == "")
                {
                    strsql = "SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as QTY , ";
                    strsql = strsql + " ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) as AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY, ";
                    strsql = strsql + " V.VARIETY_NAME AS VARIETY_NAME,'" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D, ";
                    strsql = strsql + " GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V , ";
                    strsql = strsql + " GPIL_ITEM_MASTER I where  I.ITEM_CODE=D.attribute2 ";
                    strsql = strsql + " AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID  ";
                    strsql = strsql + " AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + strCrop + "' and H.VARIETY='" + strVariety + "'  ";
                    strsql = strsql + " and  H.Date_OF_PURCH between CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',102) and CONVERT(datetime,'" + strTodate + " 23:59:59 PM',102) ";
                    strsql = strsql + " group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO ";

                }
                else
                {
                    strsql = " SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as QTY , ";
                    strsql = strsql + " ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) as AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY, ";
                    strsql = strsql + " V.VARIETY_NAME AS VARIETY_NAME,'" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D, ";
                    strsql = strsql + " GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V , ";
                    strsql = strsql + "  GPIL_ITEM_MASTER I where  I.ITEM_CODE=D.attribute2 ";
                    strsql = strsql + "  AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID /*AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER*/ ";
                    strsql = strsql + "  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + strCrop + "' and H.VARIETY='" + strVariety + "' ";
                    strsql = strsql + "  and  H.Date_OF_PURCH between CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',102) and CONVERT(datetime,'" + strTodate + " 23:59:59 PM',102)";
                    strsql = strsql + "  and H.ORGN_CODE='" + strOrgnCode + "'  ";
                    strsql = strsql + "  group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO ";
                }


                ds = base.ODataServer.GetDataset(strsql);


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


        public DataSet GetClassCumReportVKBUSummary(string varStrValue, string strFromDate, string strTodate, string strCrop, string strVariety, string strOrgnCode)
        {
            string strsql = "";
            DateTime dt = Convert.ToDateTime(strFromDate);
            strFromDate = dt.ToString("dd-MM-yyyy");
            DateTime dt1 = Convert.ToDateTime(strTodate);
            strTodate = dt1.ToString("dd-MM-yyyy");
            DataSet ds = new DataSet();
            try
            {
                if (strOrgnCode == "")
                {
                    strsql = " SELECT CONVERT( INT,I.ATTRIBUTE1) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) ";
                    strsql = strsql + " as avgpr,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D, ";
                    strsql = strsql + " GPIL_TAP_FARM_PURCHS_HDR H,GPIL_VARIETY_MASTER V ,GPIL_ITEM_MASTER I where  I.ITEM_CODE=D.attribute2 AND  D.HEADER_ID=H.HEADER_ID ";
                    strsql = strsql + "   AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + strCrop + "' and H.VARIETY='" + strVariety + "'  ";
                    strsql = strsql + " and  H.Date_OF_PURCH between CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',105) and CONVERT(datetime,'" + strTodate + " 23:59:59 PM',105) ";
                    strsql = strsql + " group by H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO ";


                }
                else
                {
                    strsql = " SELECT CONVERT( INT,I.ATTRIBUTE1) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) ";
                    strsql = strsql + " as avgpr,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D, ";
                    strsql = strsql + " GPIL_TAP_FARM_PURCHS_HDR H,GPIL_VARIETY_MASTER V ,GPIL_ITEM_MASTER I where  I.ITEM_CODE=D.attribute2 AND  D.HEADER_ID=H.HEADER_ID ";
                    strsql = strsql + "  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + strCrop + "' and H.VARIETY='" + strVariety + "'  ";
                    strsql = strsql + " and  H.Date_OF_PURCH between CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',105) and CONVERT(datetime,'" + strTodate + " 23:59:59 PM',105) ";
                    strsql = strsql + " group by H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO ";

                }

                ds = base.ODataServer.GetDataset(strsql);


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








        public DataSet GetClassUpandDownReport(string strFromDate, string strTodate, string strCrop, string strVariety)
        {
            string strsql = "";
            DataSet ds = new DataSet();
            try
            {

                if (strCrop != "" && strVariety != "")
                {
                    strsql = "SELECT TBL1.ORIGN_ORGN_CODE AS ORGN_CODE,ROUND(TBL4.TOT_QTY/1000,2) AS TOT_QTY,ROUND(TBL4.TOT_AMT/100000,2) AS TOT_AMT,TBL4.TOT_AVE AS TOT_AVE , ROUND(TBL1.EQUAL_QTY/1000,2) AS EQUAL_QTY,ROUND(TBL2.EQUAL_AMT/100000,2) AS EQUAL_AMT,TBL3.EQUAL_AVE AS EQUAL_AVE,ROUND((TBL1.EQUAL_QTY/TBL4.TOT_QTY) * 100,2) AS EQUAL_PER ,ROUND(TBL1.UP_QTY/1000,2) AS UP_QTY,ROUND(TBL2.UP_AMT/100000,2) AS UP_AMT,TBL3.UP_AVE AS UP_AVE,ROUND((TBL1.UP_QTY/TBL4.TOT_QTY) * 100,2) AS UP_PER,ROUND(TBL1.DOWN_QTY/1000,2) AS DOWN_QTY,ROUND(TBL2.DOWN_AMT/100000,2) AS DOWN_AMT,TBL3.DOWN_AVE AS DOWN_AVE,ROUND((TBL1.DOWN_QTY/TBL4.TOT_QTY) * 100,2) AS DOWN_PER,'" + strCrop + "' AS CROP,'" + strVariety + "' AS VARIETY,'' AS BUYER FROM " +
                                "(SELECT T10.ORIGN_ORGN_CODE,ISNULL(SUM(T10.UP),0) AS UP_QTY,ISNULL(SUM(T10.EQUAL),0) AS EQUAL_QTY,ISNULL(SUM(T10.DOWN),0) AS DOWN_QTY FROM " +
                                "(SELECT S.ORIGN_ORGN_CODE, (CASE WHEN T3.PAIR_TYPE = 'E' THEN  SUM(S.MARKED_WT) END) AS EQUAL,(CASE WHEN T3.PAIR_TYPE = 'U' THEN  SUM(S.MARKED_WT) END) AS UP,(CASE WHEN T3.PAIR_TYPE = 'D' THEN  SUM(S.MARKED_WT) END) AS DOWN FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND  ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE,T3.PAIR_TYPE ) T10 GROUP BY T10.ORIGN_ORGN_CODE) TBL1, " +
                                "(SELECT T10.ORIGN_ORGN_CODE,ISNULL(SUM(T10.UP),0) AS UP_AMT,ISNULL(SUM(T10.EQUAL),0) AS EQUAL_AMT,ISNULL(SUM(T10.DOWN),0) AS DOWN_AMT FROM " +
                                "(SELECT S.ORIGN_ORGN_CODE, (CASE WHEN T3.PAIR_TYPE = 'E' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE),2) END) AS EQUAL,(CASE WHEN T3.PAIR_TYPE = 'U' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE),2) END) AS UP,(CASE WHEN T3.PAIR_TYPE = 'D' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE),2) END) AS DOWN FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE,T3.PAIR_TYPE ) T10 GROUP BY T10.ORIGN_ORGN_CODE) TBL2, " +
                                "(SELECT T10.ORIGN_ORGN_CODE,ISNULL(SUM(T10.UP),0) AS UP_AVE,ISNULL(SUM(T10.EQUAL),0) AS EQUAL_AVE,ISNULL(SUM(T10.DOWN),0) AS DOWN_AVE FROM " +
                                "(SELECT S.ORIGN_ORGN_CODE, (CASE WHEN T3.PAIR_TYPE = 'E' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),2) END) AS EQUAL,(CASE WHEN T3.PAIR_TYPE = 'U' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),2) END) AS UP,(CASE WHEN T3.PAIR_TYPE = 'D' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),2) END) AS DOWN FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE,T3.PAIR_TYPE ) T10 GROUP BY T10.ORIGN_ORGN_CODE) TBL3, " +
                                "(SELECT S.ORIGN_ORGN_CODE, ROUND(ISNULL(SUM(S.MARKED_WT),0),2) AS TOT_QTY,ROUND(ISNULL(SUM(S.MARKED_WT * S.PRICE),0),2) AS TOT_AMT,ROUND(ISNULL(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),0),2) AS TOT_AVE FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + strTodate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND  T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE) TBL4 " +
                                "WHERE TBL1.ORIGN_ORGN_CODE=TBL2.ORIGN_ORGN_CODE AND TBL2.ORIGN_ORGN_CODE=TBL3.ORIGN_ORGN_CODE AND TBL3.ORIGN_ORGN_CODE=TBL4.ORIGN_ORGN_CODE  ORDER BY TBL1.ORIGN_ORGN_CODE ";
                }
                ds = base.ODataServer.GetDataset(strsql);
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








        public DataTable GetClassificationCumulaitveReport(string strQry)
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



        public DataSet GradingReportPPD(string strFromDate, string strTodate, string strCrop, string strVariety, string strOperRec, string strOrgnCode)
        {
            string strsql = "";
            DataSet ds = new DataSet();
            try
            {

                if (strOrgnCode != "")
                {
                    strsql = "select SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE, ";
                    strsql = strsql + " SUM(D.MARKED_WT) AS QTY ,H.CROP,H.VARIETY,(V.VARIETY_NAME + (SELECT '      # No of Graders : ' + CONVERT(NVARCHAR(20), ";
                    strsql = strsql + "ROUND(SUM(DISTINCT H.NO_OF_GRADERS),1)) + ' & Ave.O/P : ' + CONVERT(NVARCHAR(20),ROUND(SUM(D.MARKED_WT)/SUM(DISTINCT ";
                    strsql = strsql + "H.NO_OF_GRADERS),2)) AS WORKERS FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' ";
                    strsql = strsql + "AND H.BATCH_NO=D.BATCH_NO  AND H.VARIETY='" + strVariety + "' and H.CROP='" + strCrop + "' ";
                    strsql = strsql + " and H.RECIPE_CODE='" + strOperRec + "' and ORGN_CODE='" + strOrgnCode + "' ";
                    strsql = strsql + " and H.DATE_OF_OPERATION between CONVERT(datetime,'" + strFromDate + " 00:00:00',105) ";
                    strsql = strsql + " and CONVERT(datetime,'" + strTodate + " 23:59:59',105) AND D.BALE_TYPE='IPB')) AS VARIETY_NAME , ";
                    strsql = strsql + " H.RECIPE_CODE,H.DATE_OF_OPERATION,ISNULL(I.ATTRIBUTE4,'')HsnCode from GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D , ";
                    strsql = strsql + " GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  ";
                    strsql = strsql + " AND V.VARIETY=H.VARIETY and H.VARIETY='" + strVariety + "' and H.CROP='" + strCrop + "' and  ";
                    strsql = strsql + " H.RECIPE_CODE='" + strOperRec + "' and ORGN_CODE='" + strOrgnCode + "' ";
                    strsql = strsql + "and H.DATE_OF_OPERATION between CONVERT(datetime,'" + strFromDate + " 00:00:00',105) and CONVERT(datetime,'" + strTodate + " 23:59:59',105) AND ";
                    strsql = strsql + " D.BALE_TYPE='OPB' AND H.ISSUED_GRADE=I.ITEM_CODE GROUP BY D.GRADE,H.ISSUED_GRADE,H.CROP,H.VARIETY,V.VARIETY_NAME, ";
                    strsql = strsql + " H.RECIPE_CODE,H.DATE_OF_OPERATION,I.ATTRIBUTE4 ORDER BY H.ISSUED_GRADE,D.GRADE DESC";
                }
                else
                {
                    strsql = "select SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,SUM(D.MARKED_WT) AS QTY ,H.CROP,H.VARIETY,(V.VARIETY_NAME + (SELECT '      # No of Graders : ' + CONVERT(NVARCHAR(20),ROUND(SUM(DISTINCT H.NO_OF_GRADERS),1)) + ' & Ave.O/P : ' + CONVERT(NVARCHAR(20),ROUND(SUM(D.MARKED_WT)/SUM(DISTINCT H.NO_OF_GRADERS),2)) AS WORKERS FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND H.VARIETY='" + strVariety + "' and H.CROP='" + strCrop + "' and H.RECIPE_CODE='" + strOperRec + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + strFromDate + " 00:00:00',105) and CONVERT(datetime,'" + strTodate + " 23:59:59',105) AND D.BALE_TYPE='IPB')) AS VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION,ISNULL(I.ATTRIBUTE4,'')HsnCode from GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D ,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND V.VARIETY=H.VARIETY and H.VARIETY='" + strVariety + "' and H.CROP='" + strCrop + "' and H.RECIPE_CODE='" + strOperRec + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + strFromDate + " 00:00:00',105) and CONVERT(datetime,'" + strTodate + " 23:59:59',105) AND D.BALE_TYPE='OPB' AND H.ISSUED_GRADE=I.ITEM_CODE GROUP BY D.GRADE,H.ISSUED_GRADE,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION,I.ATTRIBUTE4 ORDER BY H.ISSUED_GRADE,D.GRADE DESC";
                }
                ds = base.ODataServer.GetDataset(strsql);
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


        public DataSet GetGreenStockReport(string strCrop, string strVariety,string strOrgnCode)
        {
            string strsql = "";
            DataSet ds = new DataSet();
            try
            {

                if (strOrgnCode == "")
                {
                    strsql = "select case when CHARINDEX('-',S.GRADE)=0 then SUBSTRING(S.GRADE,5,len(S.GRADE)-4) else SUBSTRING(S.GRADE,5,CHARINDEX('-',S.GRADE)-5) end AS GRADE ,S.CURR_ORGN_CODE,case when SUBSTRING(S.GRADE,5,LEN(S.GRADE)-4) in ('STB','SBT','SBS','UCD','USD','LOSS','SLOSS','KEL','OEL','NEL','TEL','DMG','BURNT','FLS/SBS')  then 'BYP' else case when CHARINDEX('-',S.GRADE)=0 then 'BT' else  case when SUBSTRING(S.GRADE,LEN(S.GRADE),1) IN ('R','N','L') THEN 'G' ELSE case when SUBSTRING(S.GRADE,LEN(S.GRADE),1)='P' THEN 'G' ELSE case when SUBSTRING(S.GRADE,LEN(S.GRADE),1)='B' THEN 'T' ELSE SUBSTRING(S.GRADE,LEN(S.GRADE),1) END	END END END END AS INDESXGED ,COUNT(*) AS BDLS,SUM(S.MARKED_WT) AS QTY,C.CROP+' - '+C.CROP_YEAR as crop,V.VARIETY+' - '+V.VARIETY_NAME as varirty from GPIL_STOCK S,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V where S.STATUS='Y' AND S.PROCESS_STATUS='N'  AND SUBSTRING(S.GRADE,LEN(S.GRADE)-1,2) IN ('-T','-H','-I','-G','-W','BT','-S','-R','-P','-B','-N','TB','BT','BS','CD','ST','SS','EL','MG','NT','-L','TA','AA')    ";
                    strsql = strsql + " and S.CROP='" + strCrop + "' and S.VARIETY='" + strVariety + "' AND C.CROP=S.CROP AND V.VARIETY=S.VARIETY group by S.CURR_ORGN_CODE,SUBSTRING(S.GRADE,LEN(S.GRADE),1) ,SUBSTRING(S.GRADE,1,LEN(S.GRADE)-2),S.GRADE,C.CROP+' - '+C.CROP_YEAR,V.VARIETY+' - '+V.VARIETY_NAME";
                }
                else
                {
                    strsql = "select case when CHARINDEX('-',S.GRADE)=0 then SUBSTRING(S.GRADE,5,len(S.GRADE)-4) else SUBSTRING(S.GRADE,5,CHARINDEX('-',S.GRADE)-5) end AS GRADE ,S.CURR_ORGN_CODE,case when SUBSTRING(S.GRADE,5,LEN(S.GRADE)-4) in ('STB','SBT','SBS','UCD','USD','LOSS','SLOSS','KEL','OEL','NEL','TEL','DMG','BURNT','FLS/SBS')  then 'BYP' else case when CHARINDEX('-',S.GRADE)=0 then 'BT' else  case when SUBSTRING(S.GRADE,LEN(S.GRADE),1) IN ('R','N','L') THEN 'G' ELSE case when SUBSTRING(S.GRADE,LEN(S.GRADE),1)='P' THEN 'G' ELSE case when SUBSTRING(S.GRADE,LEN(S.GRADE),1)='B' THEN 'T' ELSE SUBSTRING(S.GRADE,LEN(S.GRADE),1) END	END END END END AS INDESXGED ,COUNT(*) AS BDLS,SUM(S.MARKED_WT) AS QTY,C.CROP+' - '+C.CROP_YEAR as crop,V.VARIETY+' - '+V.VARIETY_NAME as varirty from GPIL_STOCK S,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V where S.STATUS='Y' AND S.PROCESS_STATUS='N' AND SUBSTRING(S.GRADE,LEN(S.GRADE)-1,2) IN ('-T','-H','-I','-G','-W','BT','-S','-R','-P','-B','-N','TB','BT','BS','CD','ST','SS','EL','MG','NT','-L','TA','AA')    ";
                    strsql = strsql + "  and S.CURR_ORGN_CODE='" + strOrgnCode + "' and S.CROP='" + strCrop + "' and S.VARIETY='" + strVariety + "' AND C.CROP=S.CROP AND V.VARIETY=S.VARIETY group by S.CURR_ORGN_CODE,SUBSTRING(S.GRADE,LEN(S.GRADE),1) ,SUBSTRING(S.GRADE,1,LEN(S.GRADE)-2),S.GRADE,C.CROP+' - '+C.CROP_YEAR,V.VARIETY+' - '+V.VARIETY_NAME";

                }

                ds = base.ODataServer.GetDataset(strsql);
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

        public DataSet GetClOrgClassReport(string strCrop, string strVariety, string strOrgnCode,string strFromDate,string strTodate)
        {

            string strsql = "";

            DateTime dt = Convert.ToDateTime(strFromDate);
            strFromDate = dt.ToString("dd-MM-yyyy");
            DateTime dt1 = Convert.ToDateTime(strTodate);
            strTodate = dt1.ToString("dd-MM-yyyy");
            DataSet ds = new DataSet();
            try
            {


                if (strOrgnCode == "") 
                    strsql = "select H.RECIPE_CODE,SUBSTRING(D.CLASSIFICATION_GRADE,1,2) as crop,SUBSTRING(D.CLASSIFICATION_GRADE,3,2) as variety,SUBSTRING(CLASSIFICATION_GRADE,5,LEN(CLASSIFICATION_GRADE)-4) AS CLASSIFICATION_GRADE,SUM(D.MARKED_WT) as qty,SUM(D.MARKED_WT*S.PRICE)/SUM(D.MARKED_WT)  as avgpr,count(*) AS bdls,H.ORGN_CODE,H.CLASSIFICATION_DATE,V.VARIETY_NAME,'" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE  from GPIL_CLASSIFICATION_DTLS D,GPIL_CLASSIFICATION_HDR H,GPIL_STOCK S,GPIL_VARIETY_MASTER V where H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND V.VARIETY= SUBSTRING(D.CLASSIFICATION_GRADE,3,2) and H.RECIPE_CODE='CLASSIFICATION' AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER  AND SUBSTRING(CLASSIFICATION_GRADE,3,2)='" + strVariety + "'  AND SUBSTRING(CLASSIFICATION_GRADE,1,2)='" + strCrop + "'  and H.CLASSIFICATION_DATE between CONVERT(datetime,'" + strFromDate + " 00:00:00',105) and CONVERT(datetime,'" + strTodate + " 23:59:59',105) group by CLASSIFICATION_GRADE,ORGN_CODE,H.RECIPE_CODE,H.CLASSIFICATION_DATE,V.VARIETY_NAME";               
                else               
                    strsql = "select H.RECIPE_CODE,SUBSTRING(D.CLASSIFICATION_GRADE,1,2) as crop,SUBSTRING(D.CLASSIFICATION_GRADE,3,2) as variety,SUBSTRING(CLASSIFICATION_GRADE,5,LEN(CLASSIFICATION_GRADE)-4) AS CLASSIFICATION_GRADE,SUM(D.MARKED_WT) as qty,SUM(D.MARKED_WT*S.PRICE)/SUM(D.MARKED_WT)  as avgpr,count(*) AS bdls,H.ORGN_CODE,H.CLASSIFICATION_DATE,V.VARIETY_NAME,'" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE  from GPIL_CLASSIFICATION_DTLS D,GPIL_CLASSIFICATION_HDR H,GPIL_STOCK S,GPIL_VARIETY_MASTER V where H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND V.VARIETY= SUBSTRING(D.CLASSIFICATION_GRADE,3,2) and H.RECIPE_CODE='CLASSIFICATION' AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER  AND SUBSTRING(CLASSIFICATION_GRADE,3,2)='" + strVariety + "'  AND SUBSTRING(CLASSIFICATION_GRADE,1,2)='" + strCrop + "'  and H.CLASSIFICATION_DATE between CONVERT(datetime,'" + strFromDate + " 00:00:00',105) and CONVERT(datetime,'" + strTodate + " 23:59:59',105) and H.ORGN_CODE='" + strOrgnCode + "' group by CLASSIFICATION_GRADE,ORGN_CODE,H.RECIPE_CODE,H.CLASSIFICATION_DATE,V.VARIETY_NAME";
               
                ds = base.ODataServer.GetDataset(strsql);
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


        public DataSet GetBatchwiseClassificationReport(string strBatchNo)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            try
            {
                strSql = "SELECT DISTINCT SUBSTRING(D.GPIL_BALE_NUMBER,1,2) AS CROP,SUBSTRING(D.GPIL_BALE_NUMBER,3,2) ";
                strSql = strSql + " AS VARIETY,V.VARIETY_NAME,H.CLASSIFICATION_DATE,D.GPIL_BALE_NUMBER,D.ISSUED_GRADE, ";
                strSql = strSql + " D.CLASSIFICATION_GRADE,D.WEIGHT_BEFORE_CLASSIFY,D.WEIGHT_AFTER_CLASSIFICATION,D.REMARKS ";
                strSql = strSql + " FROM GPIL_CLASSIFICATION_DTLS D,GPIL_CLASSIFICATION_HDR H,GPIL_VARIETY_MASTER V WHERE ";
                strSql = strSql + " H.BATCH_NO='" + strBatchNo + "' AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO ";
                strSql = strSql + " and V.VARIETY=SUBSTRING(d.GPIL_BALE_NUMBER,3,2) ";
                ds = base.ODataServer.GetDataset(strSql);
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




        public DataTable GetBatchNumber(string strFromDate,string strTodate,string strCrop,string strVariety,string strOrgnCode)
        {
            DateTime dt1 = Convert.ToDateTime(strFromDate);
            strFromDate = dt1.ToString("dd-MM-yyyy");
            DateTime dt2 = Convert.ToDateTime(strTodate);
            strTodate = dt2.ToString("dd-MM-yyyy");
            var ORGCODE = strOrgnCode.Substring(0, 3);
            var varity = strVariety.Substring(0, 2);


            DataTable dt = new DataTable();
            string strQry = "";
            try
            {
                strQry = "select distinct h.BATCH_NO as BATCH_NO from GPIL_CLASSIFICATION_DTLS d,GPIL_CLASSIFICATION_HDR h ";
                strQry = strQry + " where h.BATCH_NO=d.BATCH_NO AND ISNULL(h.ATTRIBUTE3,'')<>'N' and ";
                strQry = strQry + "  h.ORGN_CODE='" + ORGCODE + "' and SUBSTRING(d.GPIL_BALE_NUMBER,1,2)='" + strCrop  + "' ";
                strQry = strQry + "  and SUBSTRING(d.GPIL_BALE_NUMBER,3,2)='" + varity + "' and h.CLASSIFICATION_DATE ";
                strQry = strQry + " between convert(datetime,'" + strFromDate + " 00:00:00',105) AND convert ";
                strQry = strQry + " (datetime,'" + strTodate + " 23:59:59',105) ";
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


        public DataSet GetSupplierPurchaseInfoReport(string strPurDocNo)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            try
            {
                strSql = "select  h.LP4_NUMBER,H.RECEV_ORGN_CODE,COUNT(*) AS TOTAL_BALES,SUM(D.NET_WEIGHT) AS TOTAL_QTY ,H.SUPP_CODE,S.GPIL_SUPP_CODE+' - '+S.SUPP_NAME AS SUPPNAME";
                strSql = strSql + " from GPIL_SUPP_PURCHS_DTLS D,GPIL_SUPP_PURCHS_HDR H,GPIL_SUPPLIER_MASTER S WHERE H.HEADER_ID=D.HEADER_ID AND  H.LP4_NUMBER='" + strPurDocNo + "' AND S.SUPP_CODE=H.SUPP_CODE";
                strSql = strSql + " GROUP BY D.HEADER_ID ,H.RECEV_ORGN_CODE, h.LP4_NUMBER,H.SUPP_CODE,S.GPIL_SUPP_CODE+' - '+S.SUPP_NAME";
                ds = base.ODataServer.GetDataset(strSql);
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

        public DataTable  GetPurchaseDocNo(string strFromDate,string strToDate)
        {
            DateTime dt1 = Convert.ToDateTime(strFromDate);
            strFromDate = dt1.ToString("dd-MM-yyyy");
            DateTime dt2 = Convert.ToDateTime(strToDate);
            strToDate = dt2.ToString("dd-MM-yyyy");
            DataTable dt = new DataTable();
            string strSql = "";
            try
            {
                strSql = "select distinct LP4_NUMBER from GPIL_SUPP_PURCHS_HDR where LP4_DATE between ";
                strSql = strSql + "CONVERT(datetime,'" + strFromDate + "',105) and CONVERT ";                
                strSql = strSql + " (datetime,'" + strToDate + "',105) + 1 ";
                dt = base.ODataServer.GetDataTable(strSql);
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

        public DataSet GetBuyerVSClassReport(string strFromDate, string strToDate, string strCrop, string strVariety, string strOrgnCode)
        {
            
            var c = strCrop.ToString();
            var Crop = c.Substring(0, 2);

            var V = strVariety.ToString();
            var Variety = V.Substring(0, 2);
            var O = strOrgnCode.ToString();
            var Orng = O.Substring(0, 4);
            DataSet ds = new DataSet();
            string strSQL = "";
            try
            {
                if (strOrgnCode == "0")
                {

                    strSQL = "select '" + strFromDate + "' AS FROMDATE,'" + strToDate + "' AS TODATE,SUM(MARKED_WT) AS QTY , ";
                    strSQL = strSQL + " SUBSTRING(D.ISSUED_GRADE,5,LEN(D.ISSUED_GRADE)-4) AS BYRGRADE,I2.ITEM_CODE_GROUP AS CLASSFIEDGRADE, ";
                    strSQL = strSQL + " V.VARIETY+' - '+V.VARIETY_NAME AS VARIETY,C.CROP+' - '+C.CROP_YEAR AS CROP from ";
                    strSQL = strSQL + " GPIL_CLASSIFICATION_DTLS D,GPIL_CLASSIFICATION_HDR H,GPIL_ITEM_MASTER I1,GPIL_ITEM_MASTER I2, ";
                    strSQL = strSQL + "GPIL_TAP_FARM_PURCHS_DTLS PD,GPIL_TAP_FARM_PURCHS_HDR PH ,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V ";
                    strSQL = strSQL + " WHERE H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND I1.ITEM_CODE=D.ISSUED_GRADE AND ";
                    strSQL = strSQL + " I2.ITEM_CODE=D.CLASSIFICATION_GRADE AND PH.HEADER_ID=PD.HEADER_ID AND ";
                    strSQL = strSQL + " PD.GPIL_BALE_NUMBER= D.GPIL_BALE_NUMBER AND C.CROP=PH.CROP AND V.VARIETY=PH.VARIETY AND ";
                    strSQL = strSQL + " H.RECIPE_CODE='CLASSIFICATION' and PH.CROP='" + Crop + "' AND PH.VARIETY='" + Variety + "' ";
                    strSQL = strSQL + " AND H.CLASSIFICATION_DATE BETWEEN CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',102) ";
                    strSQL = strSQL + " and CONVERT(datetime,'" + strToDate + " 23:59:59 PM',102)  GROUP BY D.ISSUED_GRADE, ";
                    strSQL = strSQL + " I2.ITEM_CODE_GROUP,V.VARIETY+' - '+V.VARIETY_NAME,C.CROP+' - '+C.CROP_YEAR";
                }
                else
                {
                    strSQL = "select '" + strFromDate + "' AS FROMDATE,'" + strToDate + "' AS TODATE,SUM(MARKED_WT) AS QTY , ";
                    strSQL = strSQL + " SUBSTRING(D.ISSUED_GRADE,5,LEN(D.ISSUED_GRADE)-4) AS BYRGRADE,I2.ITEM_CODE_GROUP AS ";
                    strSQL = strSQL + "  CLASSFIEDGRADE,PH.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,V.VARIETY+' - '+V.VARIETY_NAME ";
                    strSQL = strSQL + "  AS VARIETY,C.CROP+' - '+C.CROP_YEAR AS CROP from GPIL_CLASSIFICATION_DTLS D, ";
                    strSQL = strSQL + "  GPIL_CLASSIFICATION_HDR H,GPIL_ITEM_MASTER I1,GPIL_ITEM_MASTER I2, GPIL_TAP_FARM_PURCHS_DTLS PD, ";
                    strSQL = strSQL + "  GPIL_TAP_FARM_PURCHS_HDR PH ,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ORGN_MASTER O ";
                    strSQL = strSQL + " WHERE H.BATCH_NO=D.BATCH_NO AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND I1.ITEM_CODE=D.ISSUED_GRADE ";
                    strSQL = strSQL + "  AND I2.ITEM_CODE=D.CLASSIFICATION_GRADE AND PH.HEADER_ID=PD.HEADER_ID AND ";
                    strSQL = strSQL + "  PD.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND C.CROP=PH.CROP AND V.VARIETY=PH.VARIETY ";
                    strSQL = strSQL + "  AND O.ORGN_CODE=PH.ORGN_CODE AND H.RECIPE_CODE='CLASSIFICATION' and ";
                    strSQL = strSQL + "  PH.CROP='" + Crop + "' AND PH.VARIETY='" + Variety + "' AND ";
                    strSQL = strSQL + "  PH.ORGN_CODE='" + Orng + "' AND H.CLASSIFICATION_DATE BETWEEN ";
                    strSQL = strSQL + "  CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',102) and ";
                    strSQL = strSQL + "  CONVERT(datetime,'" + strToDate + " 23:59:59 PM',102)  GROUP BY ";
                    strSQL = strSQL + "  D.ISSUED_GRADE,I2.ITEM_CODE_GROUP,PH.ORGN_CODE+' - '+O.ORGN_NAME,V.VARIETY+' - '+V.VARIETY_NAME, ";
                    strSQL = strSQL + "  C.CROP+' - '+C.CROP_YEAR";
                }
                ds = base.ODataServer.GetDataset(strSQL);
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
        public DataSet GetQueryResultDs(string strQry)
        {
            DataSet ds = new DataSet();
            try
            {

                ds = base.ODataServer.GetDataset(strQry);
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