using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GPILWebApp.ViewModel;
using CrystalDecisions.CrystalReports.Engine;

namespace GPILWebApp
{
    public partial class ReceiptReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {

                    DataSet ds = new DataSet();
                    ds = crd.GetORGN("CROP", "", "");
                    ddlCrop.DataSource = ds.Tables[0];
                    ddlCrop.DataTextField = "CROPYEAR";
                    ddlCrop.DataValueField = "CROP";
                    ddlCrop.DataBind();
                    ddlCrop.Items.Insert(0, "SELECT CROP YEAR");

                    DataSet ds1 = new DataSet();
                    ds1 = crd.GetORGN("VARIETY", "", "");
                    ddlVariety.DataSource = ds1.Tables[0];
                    ddlVariety.DataTextField = "VARIETYNAME";
                    ddlVariety.DataValueField = "VARIETY";
                    ddlVariety.DataBind();
                    ddlVariety.Items.Insert(0, "SELECT VARIETY CODE");

                    DataSet ds2 = new DataSet();
                    ds2 = crd.GetORGN("ORGN", "", "");
                    ddlOrgnCode.DataSource = ds2.Tables[0];
                    ddlOrgnCode.DataTextField = "ORGN_CODE1";
                    ddlOrgnCode.DataValueField = "ORGN_CODE";
                    ddlOrgnCode.DataBind();
                    ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                viewrpt();
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                viewrpt();
            }
        }
        public bool validated()
        {
            if (ddlCrop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                ddlCrop.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Variety');", true);
                ddlVariety.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void viewrpt()
        {
            try
            {
                string strsql = "";
                ReportDocument CustomerReport = new ReportDocument();
                //RDLCReport rdlcReport = new RDLCReport();
                DataSet ds = new DataSet();
                VerificationManagement vMgt = new VerificationManagement();
                strsql = "select CONVERT(NVARCHAR(10),H.RECEIVED_DATE,105) AS SENDER_DATE,H.SHIPMENT_NO,SENDER_NO,RECEIVER_ORGN_CODE+' - '+O2.ORGN_NAME AS RECEVORG,SUM(MARKED_WT) as qty,COUNT(*) AS BDLS ,C.CROP+' - '+C.CROP_YEAR AS CROP,V.VARIETY+' - '+V.VARIETY_NAME AS VARIETY,H.SENDER_ORGN_CODE+' - '+O.ORGN_NAME AS SENDORG from GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H,GPIL_ORGN_MASTER O,GPIL_ORGN_MASTER O2,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V ";
                strsql = strsql + "WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEIVER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.RECEIVED_DATE BETWEEN CONVERT(varchar,'" + txt_From_Date.Text + " 00:00:00',105) and CONVERT(varchar,'" + txt_To_Date.Text + " 23:59:59',105) AND O.ORGN_CODE=H.SENDER_ORGN_CODE AND O2.ORGN_CODE=H.RECEIVER_ORGN_CODE AND C.CROP=SUBSTRING(D.GPIL_BALE_NUMBER,1,2) and SUBSTRING(D.GPIL_BALE_NUMBER,1,2)='" + ddlCrop.Text + "' AND V.VARIETY=SUBSTRING(D.GPIL_BALE_NUMBER,3,2) and SUBSTRING(D.GPIL_BALE_NUMBER,3,2)='" + ddlVariety.Text + "' GROUP BY H.RECEIVED_DATE,H.SHIPMENT_NO,SENDER_NO,RECEIVER_ORGN_CODE+' - '+O2.ORGN_NAME,C.CROP+' - '+C.CROP_YEAR,V.VARIETY+' - '+V.VARIETY_NAME,H.SENDER_ORGN_CODE+' - '+O.ORGN_NAME ORDER BY H.RECEIVED_DATE";
                ds = vMgt.GetdsQueryResult(strsql);                
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptRecepitreprort.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            ddlCrop.SelectedIndex = 0;
            ddlVariety.SelectedIndex = 0;
            ddlOrgnCode.SelectedIndex = 0;
            txt_To_Date.Text = "";
            txt_From_Date.Text = "";
        }
    }
}