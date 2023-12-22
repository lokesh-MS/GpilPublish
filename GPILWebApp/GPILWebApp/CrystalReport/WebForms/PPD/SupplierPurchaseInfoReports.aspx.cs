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
    public partial class SupplierPurchaseInfoReports : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        PPDManagement pMgt = new PPDManagement();
     
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string strsql = "";
            DataSet ds = new DataSet();
            try
            {
              

                ds = pMgt.GetSupplierPurchaseInfoReport(ddlPurchaseDocNo.SelectedItem.Text);
                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptSupplierPurchaseInfo.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                SupplierPurchaseInfoRpt.ReportSource = CustomerReport;
                SupplierPurchaseInfoRpt.DataBind();


               
                ////ClsConnection.closeDB();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Report Date');", true);

            }
        }



        protected void btnview_Click(object sender, EventArgs e)
        {
            if (ddlPurchaseDocNo.SelectedIndex == 0)
            {
                lblMessage.Text = "Select Purchase Document No";
            }
            else
            {
                viewrpt();
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }

        protected void txttodate_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                if (txtFromDate.Text == string.Empty)
                {
                    lblMessage.Text = "Select From Date";
                }
                else if (txtToDate.Text == string.Empty)
                {
                    lblMessage.Text = "Select To Date";
                }
                else
                {
                    
                    dt = pMgt.GetPurchaseDocNo(txtFromDate.Text, txtToDate.Text);


                    ddlPurchaseDocNo.DataSource = dt;
                    ddlPurchaseDocNo.DataTextField = "LP4_NUMBER";
                    ddlPurchaseDocNo.DataValueField = "LP4_NUMBER";
                    ddlPurchaseDocNo.DataBind();
                    ddlPurchaseDocNo.Items.Insert(0, "< -- Select -- >");
                   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}