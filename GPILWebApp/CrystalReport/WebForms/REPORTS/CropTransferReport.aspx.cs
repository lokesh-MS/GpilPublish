using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPILWebApp.ViewModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace GPILWebApp
{
    public partial class CropTransferReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDropDown();
                }
                else
                {
                    BindReport();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }


        }

        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                ddlOrgnCode.DataSource = ds.Tables[0];
                ddlOrgnCode.DataTextField = "ORGN_CODE1";
                ddlOrgnCode.DataValueField = "ORGN_CODE";
                ddlOrgnCode.DataBind();
                ddlOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));

            }
            catch (Exception ex)
            {

            }
        }

        public bool validated()
        {
            if (txtFromDate.Text == "- Select -")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (txtToDate.Text == "- Select -")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txtToDate.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }



        public void BindReport()
        {
            DataTable dt = new DataTable();
            try
            {

                if (ddlOrgnCode.SelectedIndex == 0)
                {
                    dt = rptMgt.BindCropTransferReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), "0");
                }
                else
                {
                    dt = rptMgt.BindCropTransferReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), ddlOrgnCode.SelectedItem.Value);

                }

                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptCropTransferReport.rpt"));
                CustomerReport.SetDataSource(dt.DefaultView);
                rdlcCropTransfer.ReportSource = CustomerReport;
                rdlcCropTransfer.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }

        public void viewrpt()
        {
            try
            {
                string strsql = "";

                if (ddlOrgnCode.SelectedItem.Value =="-Select-")
                {
                    strsql = "SELECT '" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE,OLD_GRADE,NEW_GRADE,COUNT(OLD_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),1) AS QUANTITY,'All' AS ORGN_NAME,ISNULL(I.ATTRIBUTE4,'')HsnCode,ISNULL(I1.ATTRIBUTE4,'')OGR_HsnCode  FROM GPIL_CROP_TRANS_DTLS(NOLOCK) D,GPIL_CROP_TRANS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1 WHERE D.NEW_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND H.STATUS='N' AND H.CREATED_DATE BETWEEN CONVERT(varchar,'" + txtFromDate.Text + " 00:00:00',105) AND CONVERT(varchar,'" + txtToDate.Text + " 23:59:59',105) AND H.ORGN_CODE=O.ORGN_CODE AND H.RECIPE_CODE='RE-CLASSIFICATION' AND OLD_GRADE=I1.ITEM_CODE GROUP BY OLD_GRADE,NEW_GRADE,H.ORGN_CODE,I.ATTRIBUTE4,I1.ATTRIBUTE4 ORDER BY H.ORGN_CODE,OLD_GRADE,NEW_GRADE";
                }
                else
                {
                    strsql = "SELECT '" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE,OLD_GRADE,NEW_GRADE,COUNT(OLD_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),1) AS QUANTITY,H.ORGN_CODE + ' - ' + O.ORGN_NAME AS ORGN_NAME,ISNULL(I.ATTRIBUTE4,'')HsnCode,ISNULL(I1.ATTRIBUTE4,'')OGR_HsnCode  FROM GPIL_CROP_TRANS_DTLS(NOLOCK) D,GPIL_CROP_TRANS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1 WHERE D.NEW_GRADE=I.ITEM_CODE AND H.BATCH_NO=D.BATCH_NO AND H.STATUS='N' AND H.CREATED_DATE BETWEEN CONVERT(varchar,'" + txtFromDate.Text + " 00:00:00',105) AND CONVERT(varchar,'" + txtToDate.Text + " 23:59:59',105) AND H.ORGN_CODE=O.ORGN_CODE AND H.RECIPE_CODE='RE-CLASSIFICATION' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND OLD_GRADE=I1.ITEM_CODE GROUP BY OLD_GRADE,NEW_GRADE,H.ORGN_CODE,O.ORGN_NAME,I.ATTRIBUTE4,I1.ATTRIBUTE4 ORDER BY H.ORGN_CODE,OLD_GRADE,NEW_GRADE";

                }                
                ReportDocument CustomerReport = new ReportDocument();
                RDLCReport rdlcReport = new RDLCReport();
                DataSet ds = new DataSet();
                VerificationManagement vMgt = new VerificationManagement();
                ds = vMgt.GetdsQueryResult(strsql);
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptCropTransferReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                rdlcCropTransfer.ReportSource = CustomerReport;
                rdlcCropTransfer.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}