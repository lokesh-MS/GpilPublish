using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class LP5old1 : System.Web.UI.Page
    {
        ReportManagement rptMgt = new ReportManagement();
        ReportDocument CustomerReport = new ReportDocument();
        ReportDocument weightlist = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlFromOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlFromOrgnCode.DataTextField = "OrgnName";
                    ddlFromOrgnCode.DataValueField = "OrgnCode";
                    ddlFromOrgnCode.DataBind();
                    ddlFromOrgnCode.Items.Insert(0, "< -- Select -- >");


                    ddlToOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlToOrgnCode.DataTextField = "OrgnName";
                    ddlToOrgnCode.DataValueField = "OrgnCode";
                    ddlToOrgnCode.DataBind();
                    ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");

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

        protected void btnview_Click(object sender, EventArgs e)
        {
            viewoprpt();
        }
        public void viewoprpt()
        {
            try
            {

                string sql = "SELECT DISTINCT D.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,COUNT(GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QTY,C.CROP_YEAR AS CROP,V.VARIETY_NAME AS VARIETY,H.SENDER_ORGN_CODE + '--' + SO.ORGN_NAME AS SENDORG,H.RECEIVER_ORGN_CODE+'--'+ RO.ORGN_NAME AS RECEIVORG,I.ITEM_DESC FROM GPIL_SHIPMENT_DTLS D, GPIL_SHIPMENT_HDR H,GPIL_USER_MASTER U,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ORGN_MASTER SO,GPIL_ORGN_MASTER RO,GPIL_ITEM_MASTER I WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO='" + ddlShipmentNumber.Text + "' AND V.VARIETY IN ((CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,30,2) ELSE SUBSTRING(GPIL_BALE_NUMBER,3,2) END)) AND  (CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,1,1) ELSE SUBSTRING(GPIL_BALE_NUMBER,1,2) END) IN (C.CROP,C.ATTRIBUTE1) AND U.USER_ID=H.SENT_BY AND SO.ORGN_CODE=H.SENDER_ORGN_CODE AND RO.ORGN_CODE=H.RECEIVER_ORGN_CODE and D.Grade=I.ITEM_CODE GROUP BY I.ITEM_DESC,D.SHIPMENT_NO,H.SENDER_NO,H.SENDER_DATE,H.RECEIVER_NO,H.RECEIVED_DATE,H.SENDER_ORGN_CODE,H.RECEIVER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENT_BY,H.FRIEGHT_CHARGES,H.RC_NO,H.DRIVER_NAME,H.DRIVING_LICENCE_NO,H.TRANSPORT_NAME,U.USER_NAME,D.GRADE,C.CROP_YEAR,V.VARIETY_NAME,SO.ORGN_NAME,RO.ORGN_NAME,(CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,1,1) ELSE SUBSTRING(GPIL_BALE_NUMBER,1,2) END),(CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,30,2) ELSE SUBSTRING(GPIL_BALE_NUMBER,3,2) END)";
                DataTable dt = new DataTable();
                dt = rptMgt.GetQueryResult(sql);
                if (dt.Rows.Count > 0)
                {
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptLP5.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
                sql = "SELECT DISTINCT D.SHIPMENT_NO,D.GPIL_BALE_NUMBER,D.GRADE,D.MARKED_WT,H.SENDER_TRUCK_NO,H.RECEIVER_ORGN_CODE+'--'+ RO.ORGN_NAME AS RECEIVE_ORGN_CODE,H.SENDER_ORGN_CODE + '--' + SO.ORGN_NAME AS SENDER_ORGN_CODE,H.SENDER_NO,C.CROP_YEAR AS CROP,V.VARIETY_NAME AS VARIETY FROM GPIL_SHIPMENT_DTLS D, GPIL_SHIPMENT_HDR H,GPIL_USER_MASTER U,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ORGN_MASTER SO,GPIL_ORGN_MASTER RO WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO='" + ddlShipmentNumber.Text + "' AND V.VARIETY IN ((CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,30,2) ELSE SUBSTRING(GPIL_BALE_NUMBER,3,2) END)) AND  (CASE WHEN LEN(GPIL_BALE_NUMBER)=31 THEN SUBSTRING(GPIL_BALE_NUMBER,1,1) ELSE SUBSTRING(GPIL_BALE_NUMBER,1,2) END) IN (C.CROP,C.ATTRIBUTE1) AND U.USER_ID=H.SENT_BY AND SO.ORGN_CODE=H.SENDER_ORGN_CODE AND RO.ORGN_CODE=H.RECEIVER_ORGN_CODE ORDER BY D.GRADE,D.GPIL_BALE_NUMBER";
                dt = new DataTable();

                dt = rptMgt.GetQueryResult(sql);
                if (dt.Rows.Count > 0)
                {
                    weightlist = new ReportDocument();
                    weightlist.Load(Server.MapPath("~/Reports/rptWeightList.rpt"));
                    weightlist.SetDataSource(dt.DefaultView);
                    CrystalReportViewer2.ReportSource = weightlist;
                    CrystalReportViewer2.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void cbxtoorg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select SHIPMENT_NO From GPIL_SHIPMENT_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='" + ddlFromOrgnCode.Text + "' AND RECEIVER_ORGN_CODE='" + ddlToOrgnCode.Text + "' AND CONVERT(NVARCHAR(10),SENDER_DATE,105)='" + txtFromDate.Text + "'";
                DataTable dt = new DataTable();
                dt = rptMgt.GetQueryResult(sql);
                ddlShipmentNumber.DataSource = dt;
                ddlShipmentNumber.DataTextField = "SHIPMENT_NO";
                ddlShipmentNumber.DataValueField = "SHIPMENT_NO";
                ddlShipmentNumber.DataBind();
                ddlShipmentNumber.Items.Insert(0, "<--Select-->");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}