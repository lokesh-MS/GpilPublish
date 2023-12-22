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
    public partial class FarmerWisePurchaseSummary : System.Web.UI.Page
    {
        LDManagement ldMgt = new LDManagement();
        CommonManagement cMgt = new CommonManagement();
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


        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            bindReport();

        }

        protected void btnView_Click(object sender, EventArgs e)
        {

            bindReport();
        }
        private void bindReport()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ldMgt.FarmerWisePurchaseSummary(ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);
                if (dt.Rows.Count > 0)
                {
                    gvReport.DataSource = dt;
                    gvReport.DataBind();


                    int intBaleCount = 0;
                    double dblQuantity = 0, dblValue = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            intBaleCount = intBaleCount + Convert.ToInt32(dt.Rows[i]["BALES"].ToString().Trim());
                            dblQuantity = dblQuantity + Convert.ToDouble(dt.Rows[i]["QUANTITY"].ToString().Trim());
                            dblValue = dblValue + Convert.ToDouble(dt.Rows[i]["TOTAL_VALUE"].ToString().Trim());
                        }
                    }
                    lblFarmerCount.Text = dt.Rows.Count.ToString();
                    lblBaleCount.Text = intBaleCount.ToString();
                    lblQuantity.Text = dblQuantity.ToString("0.0") + " kgs";
                    lblValue.Text = dblValue.ToString("0.00");
                }
                else
                {
                    gvReport.DataSource = null;
                    gvReport.DataBind();
                }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            bindDropDown();
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ldMgt.FarmerWisePurchaseSummary(ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);
                string attachment = "attachment; filename=FarmerwisePurchaseSummary.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }
    }
}