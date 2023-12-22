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
    public partial class GreenStockReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
            }
            try
            {
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


        public void viewrpt()
        {
            DataSet ds = new DataSet();
            try
            {
                if (ddlSelectOrgCode.SelectedIndex == 0)
                    ds = ppdMgt.GetGreenStockReport(ddlCropYear.Text, ddlVariety.Text, "");
                else
                    ds = ppdMgt.GetGreenStockReport(ddlCropYear.Text, ddlVariety.Text, ddlSelectOrgCode.Text);

                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptTGHISTOCK.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
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
                ddlSelectOrgCode.DataSource = ds.Tables[0];
                ddlSelectOrgCode.DataTextField = "ORGN_CODE1";
                ddlSelectOrgCode.DataValueField = "ORGN_CODE";
                ddlSelectOrgCode.DataBind();
                ddlSelectOrgCode.Items.Insert(0, new ListItem("- Select -", "0"));
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
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            viewrpt();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }


    }
}