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
    public partial class ClOrgWiseClassificationReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if(!IsPostBack)
                {
                    bindDropDown();
                }
                if (IsPostBack)
                {
                    viewrpt();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (validated())
                viewrpt();
        }


        public bool validated()
        {
            if (txtFromDate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (txtTodate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txtTodate.Focus();
                return false;
            }
            else if (cbxcrop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                cbxcrop.Focus();
                return false;
            }
            else if (cbxvariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Variety');", true);
                cbxvariety.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                cbxorgcd.DataSource = ds.Tables[0];
                cbxorgcd.DataTextField = "ORGN_CODE1";
                cbxorgcd.DataValueField = "ORGN_CODE";
                cbxorgcd.DataBind();
                cbxorgcd.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                cbxcrop.DataSource = ds1.Tables[0];
                cbxcrop.DataTextField = "CROPYEAR";
                cbxcrop.DataValueField = "CROP";
                cbxcrop.DataBind();
                cbxcrop.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                cbxvariety.DataSource = ds2.Tables[0];
                cbxvariety.DataTextField = "VARIETYNAME";
                cbxvariety.DataValueField = "VARIETY";
                cbxvariety.DataBind();
                cbxvariety.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        public void viewrpt()
        {
            DataSet ds = new DataSet();
            try
            {
                if (cbxorgcd.SelectedIndex == 0)
                   ds= ppdMgt.GetClOrgClassReport(cbxcrop.Text, cbxvariety.Text, "", txtFromDate.Text, txtTodate.Text);

                else
                   ds=  ppdMgt.GetClOrgClassReport(cbxcrop.Text, cbxvariety.Text, cbxorgcd.Text, txtFromDate.Text, txtTodate.Text);

                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptClassificationchart.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}