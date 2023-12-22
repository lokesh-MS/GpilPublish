using CrystalDecisions.CrystalReports.Engine;
using GPI;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPI;

namespace GPILWebApp
{
    public partial class FarmerPurchaseCRRRBalesDetails : System.Web.UI.Page
    {
        CommonManagement cMgt = new CommonManagement();
        LDManagement lMgt = new LDManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
            }
        }
        private void bindDropDown()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = cMgt.BindDropDownCrop();
                ddlCrop.DataSource = dt;
                ddlCrop.DataTextField = "Display";
                ddlCrop.DataValueField = "CROP";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, "-Select");

                dt = cMgt.BindDropDownVariety();
                ddlVariety.DataSource = dt;
                ddlVariety.DataTextField = "Display";
                ddlVariety.DataValueField = "Variety";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, "-Select");




            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Request);
            }

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            bindReport();
           // bindDropDown();
        }

        private void bindReport()
        {
            string strFromDate = "";
            string strToDate = "";

             if (txtFromDate.Text.Trim().Length != 0 && txtToDate.Text.Trim().Length != 0)
            {
                 strFromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy").ToString() + " 00:00:00";
                 strToDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy").ToString() + " 23:59:59";
            }

            

            DataTable dt = new DataTable();
            try
            {
                dt = lMgt.FarmerPurchaseCRRRBalesDetails(strFromDate, strToDate, ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);
                if (dt.Rows.Count > 0)
                {
                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                    lblRR.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvReport.DataSource = null;
                    gvReport.DataBind();
                    lblRR.Text = dt.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            gvReport.DataBind();
        }
    }
}