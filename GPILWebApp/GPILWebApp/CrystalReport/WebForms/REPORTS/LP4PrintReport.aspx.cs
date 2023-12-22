﻿using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{

    public partial class LP4PrintReport : System.Web.UI.Page
    {
        ReportManagement rptMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                try
                {
                    ddlPurchaseOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlPurchaseOrgnCode.DataTextField = "OrgnName";
                    ddlPurchaseOrgnCode.DataValueField = "OrgnCode";
                    ddlPurchaseOrgnCode.DataBind();
                    ddlPurchaseOrgnCode.Items.Insert(0, "-- Select --");


                    /* ddlToOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlToOrgnCode.DataTextField = "OrgnName";
                    ddlToOrgnCode.DataValueField = "OrgnCode";
                    ddlToOrgnCode.DataBind();
                    ddlToOrgnCode.Items.Insert(0, "< -- Select -- >"); */

                }
                catch (Exception ex)
                {

                }

            }

            else
            {
                viewoprpt();
            }
        }

        protected void ddlSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "";
            DataTable dt = new DataTable();
            try
            {
                sql = "select HEADER_ID From GPIL_SUPP_PURCHS_HDR WITH(NOLOCK) where RECEV_ORGN_CODE='" + ddlPurchaseOrgnCode.SelectedItem.Value + "' AND SUPP_CODE='" + ddlSupplierCode.SelectedItem.Value + "' AND LP4_DATE between CONVERT(varchar,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(varchar,'" + txtToDate.Text + " 23:59:59 PM',105)";
                dt = rptMgt.GetQueryResult(sql);
                ddlLP4Number.DataSource = dt;
                ddlLP4Number.DataTextField = "HEADER_ID";
                ddlLP4Number.DataValueField = "HEADER_ID";
                ddlLP4Number.DataBind();
                ddlLP4Number.Items.Insert(0, "--Select--");
            }
        catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public void viewoprpt()
        {
            string strQry = "";
            DataSet ds = new DataSet();
          
            ReportDocument weightlist = new ReportDocument();
            ReportDocument LP4 = new ReportDocument();

            try
            {

                strQry = "SELECT DISTINCT H.HEADER_ID AS SHIPMENT_NO,'' AS SENDER_NO,H.LP4_DATE AS SENDER_DATE,'' AS RECEIVER_NO,H.CREATED_DATE AS RECEIVED_DATE,H.SUPP_CODE AS SENDER_ORGN_CODE,H.RECEV_ORGN_CODE AS RECEIVER_ORGN_CODE,'' AS SENDER_TRUCK_NO,H.BUYER_CODE AS SENT_BY,'' AS FRIEGHT_CHARGES,'' AS RC_NO,'' AS DRIVER_NAME,'' AS DRIVING_LICENCE_NO,(CASE WHEN I.ITEM_CODE LIKE 'L%' THEN I.ITEM_DESC ELSE I.ITEM_CODE_GROUP END) AS TRANSPORT_NAME,U.USER_NAME,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(D.NET_WEIGHT),1) AS QTY,C.CROP_YEAR AS CROP,V.VARIETY_NAME AS VARIETY, H.SUPP_CODE +'--'+ SU.SUPP_NAME +'--'+ ISNULL(SU.SUPP_ADDRESS1,'') +'--'+ ISNULL(SU.SUPP_ADDRESS2,'') +'--'+ ISNULL(SU.SUPP_ADDRESS3,'') +'--'+ ISNULL(SU.SUPP_ADDRESS4,'') +'--'+ ISNULL(SU.SUPP_ADDRESS5,'') +'--'+ ISNULL(SU.SUPP_ADDRESS6,'') +'--'+ ISNULL(SU.SUPP_ADDRESS7,'') +'--'+ ISNULL(SU.SUPP_ADDRESS8,'') AS SENDORG,H.RECEV_ORGN_CODE+'--'+ O.ORGN_NAME+'--'+O.ORGN_ADDRESS2+'--'+O.ORGN_ADDRESS3+'--'+O.ORGN_ADDRESS4+'--'+O.ORGN_ADDRESS5 AS RECEIVORG,I.ATTRIBUTE4,SU.ATTRIBUTE1 AS GST_Prov_Id,SU.ATTRIBUTE2 AS GST_Reg_No FROM GPIL_SUPP_PURCHS_HDR H,GPIL_SUPP_PURCHS_DTLS D,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I,GPIL_CROP_MASTER C,GPIL_USER_MASTER U,GPIL_ORGN_MASTER O,GPIL_ORGN_MASTER M,GPIL_SUPPLIER_MASTER SU  WHERE H.HEADER_ID='" + ddlLP4Number.Text + "' AND D.HEADER_ID=H.HEADER_ID AND SU.SUPP_CODE=H.SUPP_CODE AND V.VARIETY=D.VARIETY AND C.CROP=D.CROP AND D.GRADE=I.ITEM_CODE AND U.USER_ID=H.BUYER_CODE AND O.ORGN_CODE=H.RECEV_ORGN_CODE  GROUP BY H.HEADER_ID,H.HEADER_ID,H.LP4_DATE,H.CREATED_DATE,H.SUPP_CODE,H.RECEV_ORGN_CODE,H.BUYER_CODE,U.USER_NAME,D.GRADE,C.CROP_YEAR,V.VARIETY_NAME,O.ORGN_NAME,M.ORGN_NAME,SU.SUPP_NAME,(CASE WHEN I.ITEM_CODE LIKE 'L%' THEN I.ITEM_DESC ELSE I.ITEM_CODE_GROUP END),I.ATTRIBUTE4,SU.ATTRIBUTE1,SU.ATTRIBUTE2,O.ORGN_ADDRESS2,O.ORGN_ADDRESS3,O.ORGN_ADDRESS4,O.ORGN_ADDRESS5,SU.SUPP_ADDRESS1,SU.SUPP_ADDRESS2,SU.SUPP_ADDRESS3,SU.SUPP_ADDRESS4,SU.SUPP_ADDRESS5,SU.SUPP_ADDRESS6,SU.SUPP_ADDRESS7,SU.SUPP_ADDRESS8";
                ds = rptMgt.GetQueryResultDS(strQry);
                LP4.Load(Server.MapPath("~/CrystalReport/RptLP4_GST.rpt"));
                LP4.SetDataSource(ds.Tables[0].DefaultView);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                CrystalReportViewer1.ReportSource = LP4;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.RefreshReport();
                //strQry = "SELECT DISTINCT H.HEADER_ID AS SHIPMENT_NO,D.GPIL_BALE_NUMBER,D.GRADE,ROUND(D.NET_WEIGHT,1) AS MARKED_WT,'' AS SENDER_TRUCK_NO,SU.SUPP_NAME AS SENDER_ORGN_CODE, H.RECEV_ORGN_CODE + S.ORGN_NAME AS RECEIVE_ORGN_CODE,'' AS SENDER_NO,C.CROP_YEAR AS CROP,V.VARIETY_NAME FROM GPIL_SUPP_PURCHS_HDR H,GPIL_SUPP_PURCHS_DTLS D,GPIL_ORGN_MASTER R,GPIL_ORGN_MASTER S,GPIL_VARIETY_MASTER V,GPIL_CROP_MASTER C,GPIL_SUPPLIER_MASTER SU  WHERE H.HEADER_ID='" + cbxshipno.Text + "' AND D.HEADER_ID=H.HEADER_ID AND SU.SUPP_CODE=H.SUPP_CODE AND S.ORGN_CODE=H.RECEV_ORGN_CODE AND V.VARIETY=D.VARIETY AND C.CROP=D.CROP ORDER BY D.GRADE";
                strQry = "SELECT DISTINCT H.HEADER_ID AS SHIPMENT_NO,D.GPIL_BALE_NUMBER,D.GRADE,ROUND(D.NET_WEIGHT,1) AS MARKED_WT,'' AS SENDER_TRUCK_NO,SU.SUPP_NAME AS SENDER_ORGN_CODE, H.RECEV_ORGN_CODE + S.ORGN_NAME AS RECEIVE_ORGN_CODE,'' AS SENDER_NO,C.CROP_YEAR AS CROP,V.VARIETY_NAME FROM GPIL_SUPP_PURCHS_HDR H,GPIL_SUPP_PURCHS_DTLS D,GPIL_ORGN_MASTER R,GPIL_ORGN_MASTER S,GPIL_VARIETY_MASTER V,GPIL_CROP_MASTER C,GPIL_SUPPLIER_MASTER SU  WHERE H.HEADER_ID='' AND D.HEADER_ID=H.HEADER_ID AND SU.SUPP_CODE=H.SUPP_CODE AND S.ORGN_CODE=H.RECEV_ORGN_CODE AND V.VARIETY=D.VARIETY AND C.CROP=D.CROP ORDER BY D.GRADE";
                ds = rptMgt.GetQueryResultDS(strQry);
                LP4.SetDataSource(ds.Tables[0].DefaultView);
                weightlist.Load(Server.MapPath("~/Reports/RptLP4WeightList.rpt"));              
                CrystalReportViewer2.ReportSource = weightlist;
                CrystalReportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}