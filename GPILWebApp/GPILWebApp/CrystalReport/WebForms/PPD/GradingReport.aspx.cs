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
    public partial class GradingReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDropDown();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            viewrpt();
        }
        //RecpCode
        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                ddlOrganizationCode.DataSource = ds.Tables[0];
                ddlOrganizationCode.DataTextField = "ORGN_CODE1";
                ddlOrganizationCode.DataValueField = "ORGN_CODE";
                ddlOrganizationCode.DataBind();
                ddlOrganizationCode.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                ddlCropYear.DataSource = ds1.Tables[0];
                ddlCropYear.DataTextField = "CROPYEAR";
                ddlCropYear.DataValueField = "CROP";
                ddlCropYear.DataBind();
                ddlCropYear.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                ddlVariety.DataSource = ds2.Tables[0];
                ddlVariety.DataTextField = "VARIETYNAME";
                ddlVariety.DataValueField = "VARIETY";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds3 = new DataSet();
                ds3 = crd.GetORGN("RecpCode", "", "");
                ddlOperationReceipe.DataSource = ds3.Tables[0];
                ddlOperationReceipe.DataTextField = "ReceipeCode";
                ddlOperationReceipe.DataValueField = "RECIPE_CODE";
                ddlOperationReceipe.DataBind();
                ddlOperationReceipe.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            if (this.valgrdrpt() == true)
            {
                viewrpt();
            }

        }

        public bool valgrdrpt()
        {
            if (txtFromDate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (txtToDate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txtToDate.Focus();
                return false;
            }
            else if (ddlCropYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                ddlCropYear.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Variety');", true);
                ddlVariety.Focus();
                return false;
            }
            else if (ddlOperationReceipe.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Operation Recipe');", true);
                ddlOperationReceipe.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void viewrpt()
        {
            DataSet ds = new DataSet();
            try
            {
                if (txtFromDate.Text != "")
                {
                    DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                    txtFromDate.Text = dt.ToString("dd-MM-yyyy");
                }
                if (txtToDate.Text != "")
                {
                    DateTime dt1 = Convert.ToDateTime(txtToDate.Text);
                    txtToDate.Text = dt1.ToString("dd-MM-yyyy");
                }



                string sql = "";

                if (ddlOrganizationCode.SelectedIndex != 0)
                    ds = ppdMgt.GradingReportPPD(txtFromDate.Text, txtToDate.Text, ddlCropYear.Text, ddlVariety.Text, ddlOperationReceipe.Text, ddlOrganizationCode.Text);
                else
                    ds = ppdMgt.GradingReportPPD(txtFromDate.Text, txtToDate.Text, ddlCropYear.Text, ddlVariety.Text, ddlOperationReceipe.Text, "");

                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptGradingReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}