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
    public partial class GradeTransferReport : System.Web.UI.Page
    {
        
        CrystalReportData crd = new CrystalReportData();
        ReportManagement  rptMgt = new ReportManagement();
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

            BindReport();
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
            if (validated())
                BindReport();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }


        public bool validated()
        {
            if (txtFromDate.Text == "DD-MM-YYYY")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (txtToDate.Text == "DD-MM-YYYY")
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
            else if (ddlOrgnCode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                ddlOrgnCode.Focus();
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



        public void BindReport()
        {
            DataTable dt = new DataTable();
            try
            {
               
                if (ddlOrgnCode.SelectedIndex == 0)
                {
                    dt = rptMgt.BindGradeTransferReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), "0", ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);
                }
                else
                {
                    dt = rptMgt.BindGradeTransferReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), ddlOrgnCode.SelectedItem.Value, ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);

                }

                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptGradeTransfer.rpt"));
                CustomerReport.SetDataSource(dt.DefaultView);
                rdlcGradeTransfer.ReportSource = CustomerReport;
                rdlcGradeTransfer.DataBind();

               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }



    }
}