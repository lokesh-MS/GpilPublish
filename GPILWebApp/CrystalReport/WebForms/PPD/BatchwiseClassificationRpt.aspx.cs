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
    public partial class BatchwiseClassificationRpt : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
            }
            else
            {
                if(ddlBatchNo.SelectedIndex != -1 && ddlBatchNo.SelectedIndex!=0 )
                {
                    ReportView();
                }
            }
        }
        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                ddlOrgn.DataSource = ds.Tables[0];
                ddlOrgn.DataTextField = "ORGN_CODE1";
                ddlOrgn.DataValueField = "ORGN_CODE";
                ddlOrgn.DataBind();
                ddlOrgn.Items.Insert(0, new ListItem("- Select -", "0"));
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
        protected void cbxorgcd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string frmDate;
            string toDate;
            DataTable dt = new DataTable();

            try
            {
                
                dt = ppdMgt.GetBatchNumber(txtFromDate.Text, txtTodate.Text, ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Text, ddlOrgn.SelectedItem.Text);              
                ddlBatchNo.DataSource = dt;
                ddlBatchNo.DataTextField = "BATCH_NO";
                ddlBatchNo.DataValueField = "BATCH_NO";
                ddlBatchNo.DataBind();
                ddlBatchNo.Items.Insert(0, "< -- Select -- >");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportView();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtFromDate.Text = "";
            //txtTodate.Text = "";
            //ddlCrop.SelectedIndex = 0;
            //ddlVariety.SelectedIndex = 0;
            //ddlOrgn.SelectedIndex = 0;
            //ddlBatchNo.SelectedIndex = 0;
            Response.Redirect("BatchwiseClassificationRpt.aspx");
        }

        public bool validate()
        {
            if (txtFromDate.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (txtTodate.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select To Date');", true);
                txtTodate.Focus();
                return false;
            }
            else if (ddlCrop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select Crop Year');", true);
                ddlCrop.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select Variety');", true);
                ddlVariety.Focus();
                return false;
            }
            else if (ddlOrgn.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select Orginization Code');", true);
                ddlOrgn.Focus();
                return false;
            }
            else if (ddlBatchNo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select  Batch Number');", true);
                ddlBatchNo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }


        public void ReportView()
        {
            DataSet ds = new DataSet();
            try
            {

                if (validate())
                {

                    ds = ppdMgt.GetBatchwiseClassificationReport(ddlBatchNo.SelectedItem.Value.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportDocument CustomerReport = new ReportDocument();
                        CustomerReport.Load(Server.MapPath("~/CrystalReport/rptClassificationReport.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                        CrystalReportViewer1.ReportSource = CustomerReport;
                        CrystalReportViewer1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


    }
}