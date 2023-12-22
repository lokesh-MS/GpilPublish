using GPI;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.REPORTS
{
    public partial class ReportLP4Print : System.Web.UI.Page
    {
        CommonManagement cMgt = new CommonManagement();

        CrystalReportData crd = new ViewModel.CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        string sql;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlPurchaseOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlPurchaseOrgnCode.DataTextField = "OrgnName";
                    ddlPurchaseOrgnCode.DataValueField = "OrgnCode";
                    ddlPurchaseOrgnCode.DataBind();
                    ddlPurchaseOrgnCode.Items.Insert(0, "< -- Select -- >");


                    ddlSupplierCode.DataSource = rptMgt.GetOrnCode();
                    ddlSupplierCode.DataTextField = "SuppName";
                    ddlSupplierCode.DataValueField = "SuppCode";
                    ddlSupplierCode.DataBind();
                    ddlSupplierCode.Items.Insert(0, "< -- Select -- >");

                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        //protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
           
        //}

        protected void ddlSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlLp4No.DataSource = rptMgt.GetLp4No(ddlPurchaseOrgnCode.SelectedItem.Value, ddlSupplierCode.SelectedItem.Value, txtFromDate.Text, txtToDate.Text);
                ddlLp4No.DataTextField = "LP4_No";
                ddlLp4No.DataValueField = "LP4_No";
                ddlLp4No.DataBind();
                ddlLp4No.Items.Insert(0, "< -- Select -- >");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}