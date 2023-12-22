using GPI;
using GPIWebApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace GPILWebApp.ViewModel
{

    public class ReportManagement : GPIObject
    {
        public DataTable GetShipmentNumber(string FromOrgnCode, string ToOrgnCode, string ReportDate)
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "select SHIPMENT_NO From GPIL_SHIPMENT_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='" + FromOrgnCode + "' AND RECEIVER_ORGN_CODE='" + ToOrgnCode + "' AND CONVERT(NVARCHAR(10),SENDER_DATE,121)='" + ReportDate + "' ";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public DataTable GetOrnCode()
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "select ORGN_CODE + ' - ' + ORGN_NAME as OrgnName, ORGN_CODE as OrgnCode from[dbo].[GPIL_ORGN_MASTER]  where STATUS = 'Y'";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public DataTable GetFrmOrnCode()
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "select ORGN_CODE + ' - ' + ORGN_NAME as OrgnName, ORGN_CODE as OrgnCode from[dbo].[GPIL_ORGN_MASTER]  where STATUS = 'Y' and ORGN_TYPE = 'WH'";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public DataTable GetToOrnCode()
        {
            DataTable dt = new DataTable();
            try
            {
                string strQry = "select ORGN_CODE + ' - ' + ORGN_NAME as OrgnName, ORGN_CODE as OrgnCode from[dbo].[GPIL_ORGN_MASTER]  where STATUS = 'Y' and ORGN_TYPE = 'FACTORY'";
                dt = base.ODataServer.GetDataTable(strQry);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }


        public DataTable GetGradeShipmentDetails(string shipmentNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT D.GRADE,ISNULL(R.Rate,0)RATE FROM GPIL_SHIPMENT_DTLS(NOLOCK) D LEFT OUTER JOIN GPIL_SHIPMENT_HDR(NOLOCK) H";
                sql = sql + " ON H.SHIPMENT_NO=D.SHIPMENT_NO LEFT OUTER JOIN GPIL_RATE_MASTER(NOLOCK) R ON H.SENDER_ORGN_CODE=R.Loc_Code AND D.GRADE=R.Item_Code";
                sql = sql + " AND CONVERT(CHAR(6),H.SENDER_DATE,112)=CONVERT(CHAR(6),R.Last_Updated_Date,112) WHERE H.SHIPMENT_NO='" + shipmentNo + "' GROUP BY D.GRADE,R.Rate";

                dt = base.ODataServer.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }


        public bool InvoiceNumberGeneration(string strShipmentNo, string strFromOrg)
        {
            bool b = false;
            try
            {
                string sql = "EXEC sp_Invoice_SNO '" + strFromOrg + "','" + strShipmentNo + "'";
                b = base.ODataServer.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;
        }



        public DataSet GetLP5Details(string strShipmentNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "";
                sql = "SELECT T1.*,T2.RATE FROM(";
                sql = sql + "SELECT DISTINCT D.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_TRUCK_NO,";
                sql = sql + "H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,COUNT(GPIL_BALE_NUMBER) AS BALES,";
                sql = sql + "SUM(D.MARKED_WT) AS QTY,C.CROP_YEAR AS CROP,V.VARIETY_NAME AS VARIETY,H.SENDER_ORGN_CODE + '--' + SO.ORGN_NAME+ ' - ' +SO.ORGN_ADDRESS2+ ' - ' +SO.ORGN_ADDRESS3+ ' - ' +SO.ORGN_ADDRESS4 AS SENDORG,";
                sql = sql + "H.RECEIVER_ORGN_CODE+'--'+ RO.ORGN_NAME+ ' - ' +RO.ORGN_ADDRESS2+ ' - ' +RO.ORGN_ADDRESS3+ ' - ' +RO.ORGN_ADDRESS4 AS RECEIVORG,I.ITEM_DESC,I.ATTRIBUTE4,ISNULL(G.DC_No,0)SNO FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H,";
                sql = sql + "GPIL_USER_MASTER(NOLOCK) U,GPIL_CROP_MASTER(NOLOCK) C,GPIL_VARIETY_MASTER(NOLOCK) V,GPIL_ORGN_MASTER(NOLOCK) SO,GPIL_ORGN_MASTER(NOLOCK) RO,";
                sql = sql + "GPIL_ITEM_MASTER(NOLOCK) I,GPIL_GST_INVOICE_NO(NOLOCK) G WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO='" + strShipmentNo + "' AND V.VARIETY IN";
                sql = sql + " ((CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,30,2) ELSE SUBSTRING(GPIL_BALE_NUMBER,3,2) END)) AND";
                sql = sql + " (CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,1,1) ELSE SUBSTRING(GPIL_BALE_NUMBER,1,2) END) IN (C.CROP,C.ATTRIBUTE1)";
                sql = sql + " AND U.USER_ID=H.SENT_BY AND SO.ORGN_CODE=H.SENDER_ORGN_CODE AND RO.ORGN_CODE=H.RECEIVER_ORGN_CODE and D.Grade=I.ITEM_CODE AND H.SHIPMENT_NO=G.SHIPMENT_NO";
                sql = sql + " GROUP BY I.ITEM_DESC,D.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,";
                sql = sql + "H.SENDER_TRUCK_NO,H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,C.CROP_YEAR,";
                sql = sql + "SO.ORGN_ADDRESS2,SO.ORGN_ADDRESS3,SO.ORGN_ADDRESS4,RO.ORGN_ADDRESS2,RO.ORGN_ADDRESS3,RO.ORGN_ADDRESS4,";
                sql = sql + "V.VARIETY_NAME,SO.ORGN_NAME,RO.ORGN_NAME,(CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,1,1) ELSE";
                sql = sql + " SUBSTRING(GPIL_BALE_NUMBER,1,2) END),(CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,30,2) ELSE";
                sql = sql + " SUBSTRING(GPIL_BALE_NUMBER,3,2) END),I.ATTRIBUTE4,G.DC_No)T1,";
                sql = sql + "(SELECT D.GRADE,ISNULL(R.Rate,0)RATE FROM GPIL_SHIPMENT_DTLS(NOLOCK) D LEFT OUTER JOIN GPIL_SHIPMENT_HDR(NOLOCK) H";
                sql = sql + " ON H.SHIPMENT_NO=D.SHIPMENT_NO LEFT OUTER JOIN GPIL_RATE_MASTER(NOLOCK) R ON H.SENDER_ORGN_CODE=R.Loc_Code AND D.GRADE=R.Item_Code";
                sql = sql + " AND CONVERT(CHAR(6),H.SENDER_DATE,112)=CONVERT(CHAR(6),R.Last_Updated_Date,112)";
                sql = sql + " WHERE H.SHIPMENT_NO='" + strShipmentNo + "' GROUP BY D.GRADE,R.Rate)T2";
                sql = sql + " WHERE T1.GRADE=T2.GRADE ORDER BY T1.GRADE";
                ds = base.ODataServer.GetDataset(sql);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }






        public DataSet GetLP5AuctionDocument(string strShipmentNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "";

                sql = "SELECT DISTINCT D.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,";
                sql = sql + " H.SENDER_TRUCK_NO,H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,'' AS TRANSPORT_CODE,H.TRANSPORT_NAME,";
                sql = sql + " U.USER_NAME,D.GRADE,I.ITEM_DESC AS GRADE_DESC,COUNT(D.GPIL_BALE_NUMBER) AS CASES,CONVERT(decimal(18,2),ISNULL(SUM(D.MARKED_WT),0)) AS NET_WT,C.CROP_YEAR AS CROP,";
                sql = sql + " V.VARIETY_NAME AS VARIETY,H.SENDER_ORGN_CODE + '--' + SO.ORGN_NAME+ ' - ' + ISNULL(SO.ORGN_ADDRESS2,'') + ' - '";
                sql = sql + " + ISNULL(SO.ORGN_ADDRESS3,'') + ' - ' + ISNULL(SO.ORGN_ADDRESS4,'') AS SENDER,";
                sql = sql + " H.RECEIVER_ORGN_CODE+'--'+ ISNULL(RO.ORGN_NAME,'')+ ' - ' +ISNULL(RO.ORGN_ADDRESS2,'')+ ' - ' +ISNULL(RO.ORGN_ADDRESS3,'')+ ' - ' +";
                sql = sql + " ISNULL(RO.ORGN_ADDRESS4,'') AS RECEIVER,'' AS ATTRIBUTE1,'' AS ATTRIBUTE2,'' AS ATTRIBUTE3,ISNULL(I.ATTRIBUTE4,'')ATTRIBUTE4,";
                sql = sql + " SO.ATTRIBUTE5 AS Fr_State,SO.ATTRIBUTE6 AS Fr_GST,RO.ATTRIBUTE5 AS To_State,RO.ATTRIBUTE6 AS To_GST,ISNULL(G.DC_No,0)SNO,";
                sql = sql + " (SUM(D.MARKED_WT)*ISNULL(DT.ATTRIBUTE2,0)) AS TOTAL_RATE,ISNULL(DT.ATTRIBUTE2,0) AS FRIEGHT_RATE";
                sql = sql + " FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H,GPIL_USER_MASTER(NOLOCK) U,GPIL_CROP_MASTER(NOLOCK) C,";
                sql = sql + " GPIL_VARIETY_MASTER(NOLOCK) V,GPIL_ORGN_MASTER(NOLOCK) SO,GPIL_ORGN_MASTER(NOLOCK) RO,GPIL_ITEM_MASTER(NOLOCK) I,";
                sql = sql + " GPIL_SHIPMENT_DTLS_temp (NOLOCK) DT,GPIL_GST_INVOICE_NO(NOLOCK) G WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO='" + strShipmentNo + "' AND V.VARIETY IN";
                sql = sql + " ((CASE WHEN LEN(D.GPIL_BALE_NUMBER)=31 THEN SUBSTRING(D.GPIL_BALE_NUMBER,30,2) ELSE SUBSTRING(D.GPIL_BALE_NUMBER,3,2) END)) AND";
                sql = sql + " (CASE WHEN LEN(D.GPIL_BALE_NUMBER)=31 THEN SUBSTRING(D.GPIL_BALE_NUMBER,1,1) ELSE SUBSTRING(D.GPIL_BALE_NUMBER,1,2) END) IN";
                sql = sql + " (C.CROP,C.ATTRIBUTE1) AND U.USER_ID=H.SENT_BY AND SO.ORGN_CODE=H.SENDER_ORGN_CODE AND RO.ORGN_CODE=H.RECEIVER_ORGN_CODE and";
                sql = sql + " D.Grade=I.ITEM_CODE AND H.WAYBILL=DT.SHIPMENT_NO AND D.GPIL_BALE_NUMBER=DT.GPIL_BALE_NUMBER and H.SHIPMENT_NO=G.SHIPMENT_NO";
                sql = sql + " GROUP BY I.ITEM_DESC,D.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,";
                sql = sql + " H.SENDER_TRUCK_NO,H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,C.CROP_YEAR,";
                sql = sql + " SO.ORGN_ADDRESS2,SO.ORGN_ADDRESS3,SO.ORGN_ADDRESS4,RO.ORGN_ADDRESS2,RO.ORGN_ADDRESS3,RO.ORGN_ADDRESS4,";
                sql = sql + " V.VARIETY_NAME,SO.ORGN_NAME,RO.ORGN_NAME,(CASE WHEN LEN(D.GPIL_BALE_NUMBER)=31 THEN SUBSTRING(D.GPIL_BALE_NUMBER,1,1) ELSE";
                sql = sql + " SUBSTRING(D.GPIL_BALE_NUMBER,1,2) END),(CASE WHEN LEN(D.GPIL_BALE_NUMBER)=31 THEN SUBSTRING(D.GPIL_BALE_NUMBER,30,2) ELSE";
                sql = sql + " SUBSTRING(D.GPIL_BALE_NUMBER,3,2) END),I.ATTRIBUTE4,SO.ATTRIBUTE5,SO.ATTRIBUTE6,RO.ATTRIBUTE5,RO.ATTRIBUTE6,DT.ATTRIBUTE2,G.DC_No";
                ds = base.ODataServer.GetDataset(sql);
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }









        public DataTable GetWeightmentList(string shipmentNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT DISTINCT D.SHIPMENT_NO,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,H.SENDER_TRUCK_NO, ";
                sql = sql + " H.RECEIVER_ORGN_CODE+'--'+ RO.ORGN_NAME AS RECEIVE_ORGN_CODE,H.SENDER_ORGN_CODE + '--' + SO.ORGN_NAME AS ";
                sql = sql + " SENDER_ORGN_CODE,H.SENDER_NO,C.CROP_YEAR AS CROP,V.VARIETY_NAME AS VARIETY FROM GPIL_SHIPMENT_DTLS(NOLOCK) D, ";
                sql = sql + " GPIL_SHIPMENT_HDR(NOLOCK) H,GPIL_USER_MASTER(NOLOCK) U,GPIL_CROP_MASTER(NOLOCK) C,GPIL_VARIETY_MASTER(NOLOCK)";
                sql = sql + " V,GPIL_ORGN_MASTER(NOLOCK) SO,GPIL_ORGN_MASTER(NOLOCK) RO WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ";
                sql = sql + " H.SHIPMENT_NO='" + shipmentNo + "' AND V.VARIETY IN ((CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN ";
                sql = sql + " SUBSTRING(GPIL_BALE_NUMBER,30,2) ELSE SUBSTRING(GPIL_BALE_NUMBER,3,2) END)) AND  (CASE WHEN LEN(GPIL_BALE_NUMBER)=31 ";
                sql = sql + " THEN SUBSTRING(GPIL_BALE_NUMBER,1,1) ELSE SUBSTRING(GPIL_BALE_NUMBER,1,2) END) IN (C.CROP,C.ATTRIBUTE1) ";
                sql = sql + " AND U.USER_ID=H.SENT_BY ";
                sql = sql + " AND SO.ORGN_CODE=H.SENDER_ORGN_CODE AND RO.ORGN_CODE=H.RECEIVER_ORGN_CODE ORDER BY D.GRADE,D.GPIL_BALE_NUMBER";

                dt = base.ODataServer.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }




        public DataTable BindDropDownSupp()
        {
            DataTable dtBind = new DataTable();
            try
            {

                string strQry = "select Supp_Code,Supp_Name, Supp_Code + ' - ' + Supp_Name as Display from [dbo].[GPIL_SUPPLIER_MASTER] where status='Y'";
                dtBind = base.ODataServer.GetDataTable(strQry);

            }
            catch (Exception ex)
            {

                dtBind = null;
            }
            return dtBind;
        }


        public DataTable GetLp4No(string PurchaseOrgnCode, string SupplierCode, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select HEADER_ID From GPIL_SUPP_PURCHS_HDR WITH(NOLOCK) where RECEV_ORGN_CODE='" + PurchaseOrgnCode + "' AND SUPP_CODE='" + PurchaseOrgnCode + "' AND LP4_DATE between CONVERT(datetime,'" + FromDate + " 00:00:00 AM',105) and CONVERT(datetime,'" + ToDate + " 23:59:59 PM',105)";
                //string strQry = "select SHIPMENT_NO From GPIL_SHIPMENT_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='" + FromOrgnCode + "' AND RECEIVER_ORGN_CODE='" + ToOrgnCode + "' AND CONVERT(NVARCHAR(10),SENDER_DATE,105)='" + ReportDate + "' ";
                dt = base.ODataServer.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        public DataTable BindAuctionDispatchRpt(string strFromDate, string strToDate, string strOrgnCode)
        {
            DataTable dt = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "SELECT O.ORGN_CODE,O.ORGN_NAME,H.INVOICE_NO,CONVERT(char(10),H.INVOICE_DATE,105)INVOICE_DATE,D.TB_LOT_NO,D.GPIL_BALE_NUMBER,";
                strQuery = strQuery + " D.TB_GRADE,D.BUYER_GRADE,D.NET_WT,D.RATE";
                strQuery = strQuery + " FROM GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_ORGN_MASTER(NOLOCK) O";
                strQuery = strQuery + " WHERE H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE=O.ORGN_CODE AND O.ATTRIBUTE1='84'";
                strQuery = strQuery + " AND CONVERT(CHAR(10),DATE_OF_PURCH,105)>='" + strFromDate + "'";
                strQuery = strQuery + " AND CONVERT(CHAR(10),DATE_OF_PURCH,105)<='" + strToDate + "'";
                if (strOrgnCode != "0")
                    strQuery = strQuery + " AND O.ORGN_CODE='" + strOrgnCode + "'";

                strQuery = strQuery + " GROUP BY O.ORGN_CODE,O.ORGN_NAME,H.INVOICE_NO,H.INVOICE_DATE,D.TB_LOT_NO,D.GPIL_BALE_NUMBER,D.TB_GRADE,D.BUYER_GRADE,D.NET_WT,D.RATE";
                strQuery = strQuery + " ORDER BY O.ORGN_CODE,H.INVOICE_NO";
                dt = base.ODataServer.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }


        public DataTable GetSalesOrderNo(string OrgnCode, string recCode, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select SHIPMENT_NO From GPIL_SO_RESERVATION_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='" + OrgnCode + "' AND RECEIVER_ORGN_CODE='" + recCode + "' AND SENDER_DATE between CONVERT(varchar,'" + FromDate + " 00:00:00 AM',105) and CONVERT(varchAR,'" + ToDate + " 23:59:59 PM',105)";
                            
                dt = base.ODataServer.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }




        public DataTable BindSalesOrderRpt(string strSalesOrderNo)
        {
            DataTable dt = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "select DISTINCT T1.*,ISNULL(T2.Rate,0)Rate from";
                strQuery = strQuery + "(SELECT DISTINCT H.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,";
                strQuery = strQuery + "H.SENDER_TRUCK_NO,H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,";
                strQuery = strQuery + "COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QTY,('20'+ S.CROP) AS CROP,V.VARIETY_NAME AS VARIETY,";
                strQuery = strQuery + "H.SENDER_ORGN_CODE+'--'+ O.ORGN_NAME+'--'+O.ORGN_ADDRESS2+'--'+O.ORGN_ADDRESS3+'--'+O.ORGN_ADDRESS4+'--'+O.ORGN_ADDRESS5 as sendorg,";
                strQuery = strQuery + "H.RECEIVER_ORGN_CODE+'--'+ SU.SUPP_NAME+'--'+ISNULL(SU.SUPP_ADDRESS1,'')+'--'+ISNULL(SU.SUPP_ADDRESS2,'')+'--'+ISNULL(SU.SUPP_ADDRESS3,'')+'--'+ISNULL(SU.SUPP_ADDRESS4,'')+'--'+ISNULL(SU.SUPP_ADDRESS8,'') AS RECEIVORG,";
                strQuery = strQuery + "I.ITEM_DESC,I.ATTRIBUTE4,O.ATTRIBUTE5 AS GST_ProV_Id,O.ATTRIBUTE6 AS Fr_GST,SU.ATTRIBUTE1 AS GST_Reg_No,SU.ATTRIBUTE2 AS To_GST";
                strQuery = strQuery + " FROM  GPIL_SO_RESERVATION_DTLS(NOLOCK) D,GPIL_SO_RESERVATION_HDR(NOLOCK) H,GPIL_VARIETY_MASTER(NOLOCK) V,GPIL_USER_MASTER(NOLOCK) U,";
                strQuery = strQuery + "GPIL_ORGN_MASTER(NOLOCK) O,GPIL_SUPPLIER_MASTER(NOLOCK) SU,GPIL_STOCK(NOLOCK) S,GPIL_ITEM_MASTER(NOLOCK) I";
                strQuery = strQuery + " WHERE H.SHIPMENT_NO='" + strSalesOrderNo + "' AND D.SHIPMENT_NO=H.SHIPMENT_NO AND";
                strQuery = strQuery + " D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND D.GRADE=I.ITEM_CODE  AND SU.GPIL_SUPP_CODE=H.RECEIVER_ORGN_CODE AND V.VARIETY=S.VARIETY AND";
                strQuery = strQuery + " U.USER_ID=H.SENT_BY AND O.ORGN_CODE=H.SENDER_ORGN_CODE";
                strQuery = strQuery + " GROUP BY H.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_TRUCK_NO,";
                strQuery = strQuery + "H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,V.VARIETY_NAME,O.ORGN_NAME,";
                strQuery = strQuery + "S.CROP,SU.SUPP_NAME,I.ITEM_DESC,O.ORGN_ADDRESS2,O.ORGN_ADDRESS3,O.ORGN_ADDRESS4,O.ORGN_ADDRESS5,SU.SUPP_ADDRESS1,SU.SUPP_ADDRESS2,";
                strQuery = strQuery + "SU.SUPP_ADDRESS3,I.ATTRIBUTE4,O.ATTRIBUTE5,O.ATTRIBUTE6,SU.ATTRIBUTE1,SU.ATTRIBUTE2,SU.SUPP_ADDRESS4,SU.SUPP_ADDRESS8)T1";
                strQuery = strQuery + " LEFT OUTER JOIN";
                strQuery = strQuery + " (select * from GPIL_ByProduct_Sale_RateMaster(NOLOCK) where CONVERT(varchar(6),Last_Updated_Date,112)=CONVERT(varchar(6),GETDATE(),112))T2";
                strQuery = strQuery + " ON T1.GRADE=T2.Item_Code and T2.Loc_Code=T1.SENDER_ORGN_CODE";
                dt = base.ODataServer.GetDataTable(strQuery);

            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }



        public DataTable bindSalesOrderWeightList(string strSalesOrderNo)
        {
            DataTable dt = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "SELECT DISTINCT H.SHIPMENT_NO,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,H.SENDER_TRUCK_NO,SU.SUPP_NAME AS RECEIVE_ORGN_CODE, ";
                strQuery = strQuery + "  O.ORGN_NAME AS SENDER_ORGN_CODE,H.SENDER_NO,'20'+ S.CROP AS CROP,V.VARIETY_NAME,I.ITEM_DESC FROM GPIL_SO_RESERVATION_HDR H, ";
                strQuery = strQuery + "  GPIL_SO_RESERVATION_DTLS D,GPIL_STOCK S ,GPIL_ORGN_MASTER O,GPIL_ITEM_MASTER I,GPIL_VARIETY_MASTER V,GPIL_SUPPLIER_MASTER SU ";
                strQuery = strQuery + " WHERE H.SHIPMENT_NO='" + strSalesOrderNo + "' AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND D.GRADE=I.ITEM_CODE ";
                strQuery = strQuery + " AND D.SHIPMENT_NO=H.SHIPMENT_NO AND SU.GPIL_SUPP_CODE=H.RECEIVER_ORGN_CODE AND O.ORGN_CODE=H.SENDER_ORGN_CODE AND ";
                strQuery = strQuery + " V.VARIETY=S.VARIETY ORDER BY D.GRADE";
                dt = base.ODataServer.GetDataTable(strQuery);

            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }


        public DataTable bindSalesOrderNo(string strFromDate, string strTodate, string strOrgnCode, string strCustCode)
        {
            DataTable dt = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "select SHIPMENT_NO From GPIL_SO_RESERVATION_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='" + strOrgnCode + "' ";
                strQuery = strQuery + " AND RECEIVER_ORGN_CODE='" + strCustCode + "' AND SENDER_DATE between ";
                strQuery = strQuery + " CONVERT(datetime,'" + strFromDate + " 00:00:00 AM',105) and  ";
                strQuery = strQuery + " CONVERT(datetime,'" + strTodate + " 23:59:59 PM',105) ";
                dt = base.ODataServer.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }



        public DataTable BindGradeTransferReport(string strFromDate, string strTodate, string strOrgnCode, string strCrop, string strVariety)
        {
            DataTable dt = new DataTable();
            string strQuery = "";
            try
            {
                if (strOrgnCode == "0")
                {
                    strQuery = "select '" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE,ISSUED_GRADE,CLASSIFICATION_GRADE,SUM(MARKED_WT) ";
                    strQuery = strQuery + " AS QTY,COUNT(*) AS BDLS,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,V.VARIETY+' - '+V.VARIETY_NAME AS VARIETY, ";
                    strQuery = strQuery + " C.CROP_YEAR,I.ATTRIBUTE4 AS HsnCode,I1.ATTRIBUTE4 AS CLS_HsnCode from GPIL_CLASSIFICATION_DTLS(NOLOCK) D, ";
                    strQuery = strQuery + " GPIL_CLASSIFICATION_HDR(NOLOCK) H ,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_VARIETY_MASTER(NOLOCK) V,GPIL_CROP_MASTER(NOLOCK) C, ";
                    strQuery = strQuery + " GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1 WHERE D.ISSUED_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND ";
                    strQuery = strQuery + " ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.ORGN_CODE=O.ORGN_CODE AND SUBSTRING(D.GPIL_BALE_NUMBER,1,2)=C.CROP AND SUBSTRING ";
                    strQuery = strQuery + " (D.GPIL_BALE_NUMBER,3,2)=V.VARIETY AND H.RECIPE_CODE='RE-CLASSIFICATION' AND C.CROP='" + strCrop + "' AND ";
                    strQuery = strQuery + " V.VARIETY='" + strVariety + "' AND  H.CLASSIFICATION_DATE BETWEEN CONVERT(varchar,'" + strFromDate + "',105) ";
                    strQuery = strQuery + " AND CONVERT(varchar,'" + strTodate + "',105)+1 AND D.CLASSIFICATION_GRADE=I1.ITEM_CODE GROUP BY ISSUED_GRADE, ";
                    strQuery = strQuery + " CLASSIFICATION_GRADE,H.ORGN_CODE+' - '+O.ORGN_NAME,V.VARIETY+' - '+V.VARIETY_NAME,C.CROP_YEAR,I.ATTRIBUTE4,I1.ATTRIBUTE4 ";
                }
                else
                {
                    strQuery = "select '" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE,ISSUED_GRADE,CLASSIFICATION_GRADE,SUM(MARKED_WT) ";
                    strQuery = strQuery + " AS QTY,COUNT(*) AS BDLS,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,V.VARIETY+' - '+V.VARIETY_NAME AS VARIETY, ";
                    strQuery = strQuery + " C.CROP_YEAR,I.ATTRIBUTE4 AS HsnCode,I1.ATTRIBUTE4 AS CLS_HsnCode from GPIL_CLASSIFICATION_DTLS(NOLOCK) D, ";
                    strQuery = strQuery + " GPIL_CLASSIFICATION_HDR(NOLOCK) H ,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_VARIETY_MASTER(NOLOCK) V,GPIL_CROP_MASTER(NOLOCK) C, ";
                    strQuery = strQuery + " GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1 WHERE D.ISSUED_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO ";
                    strQuery = strQuery + " AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.ORGN_CODE=O.ORGN_CODE AND SUBSTRING(D.GPIL_BALE_NUMBER,1,2)=C.CROP ";
                    strQuery = strQuery + " AND SUBSTRING(D.GPIL_BALE_NUMBER,3,2)=V.VARIETY AND H.RECIPE_CODE='RE-CLASSIFICATION' AND ";
                    strQuery = strQuery + " C.CROP='" + strCrop + "' AND V.VARIETY='" + strVariety + "' AND  H.CLASSIFICATION_DATE ";
                    strQuery = strQuery + " BETWEEN CONVERT(varchar,'" + strFromDate + "',105) AND CONVERT(varchar,'" + strTodate + "',105)+1 AND ";
                    strQuery = strQuery + " O.ORGN_CODE='" + strOrgnCode + "' AND D.CLASSIFICATION_GRADE=I1.ITEM_CODE GROUP BY ISSUED_GRADE,CLASSIFICATION_GRADE, ";
                    strQuery = strQuery + " H.ORGN_CODE+' - '+O.ORGN_NAME,V.VARIETY+' - '+V.VARIETY_NAME,C.CROP_YEAR,I.ATTRIBUTE4,I1.ATTRIBUTE4 ";
                }
                dt = base.ODataServer.GetDataTable(strQuery);

            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }








        public DataTable BindCropTransferReport(string strFromDate, string strTodate, string strOrgnCode)
        {
            DataTable dt = new DataTable();
            string strQuery = "";
            try
            {
                if (strOrgnCode == "0")
                {

                    strQuery = "SELECT '" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE,OLD_GRADE,NEW_GRADE,COUNT(OLD_BALE_NUMBER) ";
                    strQuery = strQuery + " AS BALES,ROUND(SUM(MARKED_WT),1) AS QUANTITY,'All' AS ORGN_NAME,ISNULL(I.ATTRIBUTE4,'')HsnCode,ISNULL(I1.ATTRIBUTE4,'') ";
                    strQuery = strQuery + " OGR_HsnCode  FROM GPIL_CROP_TRANS_DTLS(NOLOCK) D,GPIL_CROP_TRANS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O, ";
                    strQuery = strQuery + " GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1 WHERE D.NEW_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO ";
                    strQuery = strQuery + " AND H.STATUS='N' AND H.CREATED_DATE BETWEEN CONVERT(varchar,'" + strFromDate + " 00:00:00',105) ";
                    strQuery = strQuery + " AND CONVERT(varchar,'" + strTodate + " 23:59:59',105) AND H.ORGN_CODE=O.ORGN_CODE AND ";
                    strQuery = strQuery + " H.RECIPE_CODE='RE-CLASSIFICATION' AND OLD_GRADE=I1.ITEM_CODE GROUP BY OLD_GRADE,NEW_GRADE,H.ORGN_CODE, ";
                    strQuery = strQuery + " I.ATTRIBUTE4,I1.ATTRIBUTE4 ORDER BY H.ORGN_CODE,OLD_GRADE,NEW_GRADE";
                }
                else
                {
                    strQuery = "SELECT '" + strFromDate + "' AS FROMDATE,'" + strTodate + "' AS TODATE,OLD_GRADE,NEW_GRADE,COUNT(OLD_BALE_NUMBER) AS BALES, ";
                    strQuery = strQuery + " ROUND(SUM(MARKED_WT),1) AS QUANTITY,H.ORGN_CODE + ' - ' + O.ORGN_NAME AS ORGN_NAME,ISNULL(I.ATTRIBUTE4,'')HsnCode, ";
                    strQuery = strQuery + " ISNULL(I1.ATTRIBUTE4,'')OGR_HsnCode  FROM GPIL_CROP_TRANS_DTLS(NOLOCK) D,GPIL_CROP_TRANS_HDR(NOLOCK) H,GPIL_ORGN_MASTER ";
                    strQuery = strQuery + " (NOLOCK) O,GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1 WHERE D.NEW_GRADE=I.ITEM_CODE AND ";
                    strQuery = strQuery + " H.BATCH_NO=D.BATCH_NO AND H.STATUS='N' AND H.CREATED_DATE BETWEEN ";
                    strQuery = strQuery + " CONVERT(varchar,'" + strFromDate + " 00:00:00',105) AND CONVERT(varchar,'" + strTodate + " 23:59:59',105) ";
                    strQuery = strQuery + " AND H.ORGN_CODE=O.ORGN_CODE AND H.RECIPE_CODE='RE-CLASSIFICATION' AND H.ORGN_CODE='" + strOrgnCode + "' AND ";
                    strQuery = strQuery + " OLD_GRADE =I1.ITEM_CODE GROUP BY OLD_GRADE,NEW_GRADE,H.ORGN_CODE,O.ORGN_NAME,I.ATTRIBUTE4,I1.ATTRIBUTE4 ";
                    strQuery = strQuery + " ORDER BY H.ORGN_CODE,OLD_GRADE,NEW_GRADE";
                }
                dt = base.ODataServer.GetDataTable(strQuery);

            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }


        public DataTable BindWeightLossReport(string strAttributes, string strCondition)
        {
            DataTable dt = new DataTable();
            try
            {

                string strQuery = "";


                //if (strCrop == "0")
                //{
                //    if (strVariety == "0")
                //    {
                //        strAttributes = strAttributes + " 'ALL' AS CROP,'ALL' AS VARIETY, ";
                //    }
                //    else
                //    {
                //        strAttributes = strAttributes + " 'ALL' AS CROP,'" + strVariety + "' AS VARIETY, ";
                //        strCondition = strCondition + " AND D.GPIL_BALE_NUMBER LIKE '__" + strVariety + "%' ";
                //    }
                //}
                //else
                //{
                //    if (strVariety == "0")
                //    {
                //        strAttributes = strAttributes + " '" + strCrop + "' AS CROP,'ALL' AS VARIETY, ";
                //        strCondition = strCondition + " AND D.GPIL_BALE_NUMBER LIKE '" + strCrop + "%' ";
                //    }
                //    else
                //    {
                //        strAttributes = strAttributes + " '" + strCrop + "' AS CROP,'" + strVariety + "' AS VARIETY, ";
                //        strCondition = strCondition + " AND D.GPIL_BALE_NUMBER LIKE '" + strCrop + strVariety + "%' ";
                //    }
                //}


                //if (strFromDate.Length != 0 && strToDate.Trim().Length != 0 && strFromDate != "-Select-" && strToDate != "<--Select-->")
                //{
                //    strAttributes = strAttributes + "'" + strFromDate + "' AS FROM_DATE,'" + strToDate + "' AS TO_DATE,";
                //    strCondition = strCondition + " AND RECEIVED_DATE BETWEEN CONVERT(DATETIME,'" + strFromDate + " 00:00:00 AM',105) AND CONVERT(DATETIME,'" + strToDate + " 23:59:59 PM',105) ";
                //}
                //else
                //{
                //    strAttributes = strAttributes + "'INITIAL DAY' AS FROM_DATE,'AS OF NOW' AS TO_DATE,";
                //    strCondition = strCondition + "";
                //}

                //if (strOrgnCode == "0")
                //{
                //    strAttributes = strAttributes + "'ALL' AS SENDER_ORGN_TYPE,";
                //    strCondition = strCondition + "";
                //}
                //else
                //{
                //    strAttributes = strAttributes + "'" + strOrgnType + "' AS SENDER_ORGN_TYPE,";
                //    strCondition = strCondition + " AND O.ORGN_TYPE='" + strOrgnType + "' ";
                //}


                //if (strOrgnCode == "0")
                //{
                //    strAttributes = strAttributes + "'ALL OVER ORGN' AS RECEIVER_ORGN,";
                //}
                //else
                //{
                //    strAttributes = strAttributes + "'" + strOrgnCode + "' AS RECEIVER_ORGN,";
                //    strCondition = strCondition + " AND H.RECEIVER_ORGN_CODE='" + strOrgnCode + "' ";
                //}

                strQuery = "SELECT " + strAttributes + " T1.ORGN_CODE AS SENDER_ORGN,T1.ORGN_CODE AS SENDER_ORGN,ISNULL(T1.TOT_MARKED_WT,0) AS ";
                strQuery = strQuery + " TOT_MARKED_WT,ISNULL(T1.TOT_ASCERTAIN_WT,0) AS TOT_ASCERTAIN_WT,ISNULL(T1.TOT_LOSS,0) AS TOT_LOSS, ";
                strQuery = strQuery + "  ISNULL(T2.HND_MARKED_WT,0) AS HND_MARKED_WT,ISNULL(T2.HND_ASCERTAIN_WT,0) AS HND_ASCERTAIN_WT,ISNULL(T2.HND_LOSS,0) AS ";
                strQuery = strQuery + "  HND_LOSS,ISNULL(T3.TWT_MARKED_WT,0) AS TWT_MARKED_WT,ISNULL(T3.TWT_ASCERTAIN_WT,0) AS TWT_ASCERTAIN_WT, ";
                strQuery = strQuery + " ISNULL(T3.TWT_LOSS,0) AS TWT_LOSS,(CASE WHEN T2.HND_MARKED_WT <> 0 THEN ROUND(ISNULL((T2.HND_LOSS/T2.HND_MARKED_WT) * 100,0),2) ";
                strQuery = strQuery + " ELSE 0 END) AS HND_LOSS_PERC,(CASE WHEN T3.TWT_MARKED_WT <> 0 THEN ROUND(ISNULL((T3.TWT_LOSS/T3.TWT_MARKED_WT) * 100,0),2) ";
                strQuery = strQuery + " ELSE 0 END) AS TWT_LOSS_PERC,(CASE WHEN (ISNULL(T2.HND_MARKED_WT,0)+ISNULL(T3.TWT_MARKED_WT,0)) <> 0 THEN ";
                strQuery = strQuery + " ROUND((100*((ISNULL(T2.HND_LOSS,0)+ISNULL(T3.TWT_LOSS,0))/(ISNULL(T2.HND_MARKED_WT,0)+ISNULL(T3.TWT_MARKED_WT,0)))),2) ";
                strQuery = strQuery + " ELSE 0 END) AS TOT_LOSS_PERC FROM (SELECT H.SENDER_ORGN_CODE AS ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS TOT_BALES, ";
                strQuery = strQuery + " ROUND(SUM(MARKED_WT),1) AS TOT_MARKED_WT,ROUND(SUM(RECEIPT_WEIGHT),1) AS TOT_ASCERTAIN_WT,(ROUND(SUM(MARKED_WT - RECEIPT_WEIGHT),1)) AS ";
                strQuery = strQuery + " TOT_LOSS FROM GPIL_SHIPMENT_DTLS D, GPIL_SHIPMENT_HDR H,GPIL_ORGN_MASTER O WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ";
                strQuery = strQuery + " H.STATUS='N' AND H.SENDER_ORGN_CODE=O.ORGN_CODE  " + strCondition + "  GROUP BY H.SENDER_ORGN_CODE) AS ";
                strQuery = strQuery + " T1 FULL OUTER JOIN (SELECT H.SENDER_ORGN_CODE AS ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS HND_BALES,ROUND(SUM(MARKED_WT),1) AS ";
                strQuery = strQuery + " HND_MARKED_WT,ROUND(SUM(RECEIPT_WEIGHT),1) AS HND_ASCERTAIN_WT,(ROUND(SUM(MARKED_WT - RECEIPT_WEIGHT),1)) AS HND_LOSS ";
                strQuery = strQuery + " FROM GPIL_SHIPMENT_DTLS D, GPIL_SHIPMENT_HDR H,GPIL_ORGN_MASTER O WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.STATUS='N' ";
                strQuery = strQuery + " AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=O.ORGN_CODE  " + strCondition + "  GROUP BY H.SENDER_ORGN_CODE) AS ";
                strQuery = strQuery + " T2 ON T1.ORGN_CODE=T2.ORGN_CODE  FULL OUTER JOIN (SELECT H.SENDER_ORGN_CODE AS ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS ";
                strQuery = strQuery + " TWT_BALES,ROUND(SUM(MARKED_WT),1) AS TWT_MARKED_WT, ROUND(SUM(RECEIPT_WEIGHT),1) AS TWT_ASCERTAIN_WT, ";
                strQuery = strQuery + " (ROUND(SUM(MARKED_WT - RECEIPT_WEIGHT),1)) AS TWT_LOSS FROM GPIL_SHIPMENT_DTLS D, GPIL_SHIPMENT_HDR H,GPIL_ORGN_MASTER O ";
                strQuery = strQuery + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.STATUS='N' AND  H.RECEV_WEIGH_TYPE='TWT' AND WEIGHT_STATUS in( 'Y') AND ";
                strQuery = strQuery + " H.SENDER_ORGN_CODE=O.ORGN_CODE  " + strCondition + "  GROUP BY H.SENDER_ORGN_CODE) AS T3 ON ";
                strQuery = strQuery + " T1.ORGN_CODE=T3.ORGN_CODE ORDER BY T1.ORGN_CODE";
                dt = base.ODataServer.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;

        }






















        //public DataTable BindDropDownSupp()
        //{
        //    DataTable dtBind = new DataTable();
        //    try
        //    {

        //        string strQry = "select Supp_Code,Supp_Name, Supp_Code + ' - ' + Supp_Name as Display from [dbo].[GPIL_SUPPLIER_MASTER] where status='Y'";
        //        dtBind = base.ODataServer.GetDataTable(strQry);

        //    }
        //    catch (Exception ex)
        //    {

        //        dtBind = null;
        //    }
        //    return dtBind;
        //}


        //public DataTable GetLp4No(string PurchaseOrgnCode, string SupplierCode, string FromDate, string ToDate)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string sql = "select HEADER_ID From GPIL_SUPP_PURCHS_HDR WITH(NOLOCK) where RECEV_ORGN_CODE='" + PurchaseOrgnCode + "' AND SUPP_CODE='" + PurchaseOrgnCode + "' AND LP4_DATE between CONVERT(datetime,'" + FromDate + " 00:00:00 AM',105) and CONVERT(datetime,'" + ToDate + " 23:59:59 PM',105)";
        //        //string strQry = "select SHIPMENT_NO From GPIL_SHIPMENT_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='" + FromOrgnCode + "' AND RECEIVER_ORGN_CODE='" + ToOrgnCode + "' AND CONVERT(NVARCHAR(10),SENDER_DATE,105)='" + ReportDate + "' ";
        //        dt = base.ODataServer.GetDataTable(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = null;
        //    }

        //    return dt;
        //}

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

        public DataSet GetQueryResultDS(string strQry)
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

    }

}
