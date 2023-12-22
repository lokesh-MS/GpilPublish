using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class AuctionDispatchReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    DataSet ds2 = new DataSet();
                    ds2 = crd.GetORGN("ORGN", "", "");
                    ddlOrgnCode.DataSource = ds2.Tables[0];
                    ddlOrgnCode.DataTextField = "ORGN_CODE1";
                    ddlOrgnCode.DataValueField = "ORGN_CODE";
                    ddlOrgnCode.DataBind();
                    ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");

                }
                catch (Exception ex)
                { }
            }
            else
            {
                if (ddlOrgnCode.SelectedIndex != 0)
                {
                    BindViewReport();
                }

            }
        }


        public void BindViewReport()
        {
            DataTable dt = new DataTable();
            string frmDate;
            string toDate;
            try
            {
                DateTime dt1 = Convert.ToDateTime(txtFromDate.Text);
                frmDate = dt1.ToString("dd-MM-yyyy");
                DateTime dt2 = Convert.ToDateTime(txtTodate.Text);
                toDate = dt2.ToString("dd-MM-yyyy");

                //CrystalReportViewer1.Visible = false;

                if (string.IsNullOrEmpty(frmDate.Trim()) || string.IsNullOrEmpty(toDate.Trim()))
                    return;

                dt = rMgt.BindAuctionDispatchRpt(frmDate, toDate, ddlOrgnCode.SelectedItem.Text);

                if (dt.Rows.Count > 0)
                {

                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptAuctionInvoice.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            BindViewReport();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlOrgnCode.SelectedIndex = 0;
            txtTodate.Text = "";
            txtFromDate.Text = "";
        }
    }
}