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
    public partial class ClassificationCumulativeReportVKBU : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        string strsql = "";
        string varStrValue = "D.NET_WT * D.RATE";
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
                    if (rdbcomplete.Checked == true)
                    {
                        viewrpt();
                    }
                    else
                    {
                        viewrpt2();
                    }
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
                ddlOrganization.DataSource = ds.Tables[0];
                ddlOrganization.DataTextField = "ORGN_CODE1";
                ddlOrganization.DataValueField = "ORGN_CODE";
                ddlOrganization.DataBind();
                ddlOrganization.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                ddlCrop.DataSource = ds1.Tables[0];
                ddlCrop.DataTextField = "CROPYEAR";
                ddlCrop.DataValueField = "CROP";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, new ListItem("- Select -", "0"));
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

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                if (validated())
                {

                    if (rdbcomplete.Checked == true)
                    {
                        viewrpt();
                    }
                    else
                    {
                        viewrpt2();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public bool validated()
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
            else if (ddlCrop.SelectedIndex == 0)
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

        public void viewrpt2()
        {
            DataSet ds = new DataSet();
            try
            {
                if (ddlVariety.Text != "10" || ddlVariety.Text != "20" || ddlVariety.Text != "30")
                {
                    if (rbtnWithFreight.Checked == true)
                        varStrValue = "D.NET_WT * D.RATE";

                    else
                        varStrValue = "D.NET_WT * CONVERT(FLOAT,D.ATTRIBUTE4)";

                }
                else

                    varStrValue = "D.NET_WT * D.RATE";


                if (ddlOrganization.SelectedIndex == 0)

                    ds = ppdMgt.GetClassCumReportVKBUSummary(varStrValue, txtFromDate.Text, txtToDate.Text, ddlCrop.Text, ddlVariety.Text, "");
                else
                    ds = ppdMgt.GetClassCumReportVKBUSummary(varStrValue, txtFromDate.Text, txtToDate.Text, ddlCrop.Text, ddlVariety.Text, ddlOrganization.Text);

                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptClassificationReportforTAP_Summary.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                ClassificationCumulativeRpt.ReportSource = CustomerReport;
                ClassificationCumulativeRpt.DataBind();

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
                if (ddlVariety.Text != "10" || ddlVariety.Text != "20" || ddlVariety.Text != "30")
                {
                    if (rbtnWithFreight.Checked == true)
                        varStrValue = "D.NET_WT * D.RATE";
                    else
                        varStrValue = "D.NET_WT * CONVERT(FLOAT,D.ATTRIBUTE4)";
                }
                else
                    varStrValue = "D.NET_WT * D.RATE";
                if (ddlOrganization.SelectedIndex == 0)
                    ds = ppdMgt.GetClassCumReportVKBUComplete(varStrValue, txtFromDate.Text, txtToDate.Text, ddlCrop.Text, ddlVariety.Text, "");
                else
                    ds = ppdMgt.GetClassCumReportVKBUComplete(varStrValue, txtFromDate.Text, txtToDate.Text, ddlCrop.Text, ddlVariety.Text, ddlOrganization.Text);
                if(ds != null)
                {
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptClassificationCumulativeTAPWiseReport.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    ClassificationCumulativeRpt.ReportSource = CustomerReport;
                    ClassificationCumulativeRpt.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('No data found!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassificationCumulativeReportVKBU.aspx");

        }
    }
}