using GPI;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPI;

namespace GPILWebApp
{
    public partial class FarmerAuthorisedQtyReport : System.Web.UI.Page
    {
        LDManagement ldMgt = new LDManagement();
        CommonManagement cMgt = new CommonManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
                file_export.DataSource = null;
                file_export.DataBind();
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
        }
        private void bindReport()
        {
            string strFromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy").ToString() + " 00:00:00";
            string strToDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy").ToString() + " 23:59:59";
            string strTodayDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy").ToString();
            DataTable dt = new DataTable();
            try
            {
                dt = ldMgt.FarmerAuthorisedQtyReport(strFromDate, strToDate, strTodayDate, ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);


                if (dt.Rows.Count > 0)
                {
                    file_export.DataSource = dt;
                    file_export.DataBind();

                    double dblTodayOffered = 0, dblTodayCR = 0, dblTodayRR = 0, dblTodaySold = 0;
                    //   double dblTodateOffered=0, dblTodateCR=0, dblTodateRR=0, dblTodateSold=0;                

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dblTodayOffered = dblTodayOffered + Convert.ToDouble(dt.Rows[i]["Authorised_Qty"].ToString().Trim());
                            dblTodayCR = dblTodayCR + Convert.ToDouble(dt.Rows[i]["TODAY_SOLD"].ToString().Trim());
                            dblTodayRR = dblTodayRR + Convert.ToDouble(dt.Rows[i]["TODATE_SOLD"].ToString().Trim());
                            dblTodaySold = dblTodaySold + Convert.ToDouble(dt.Rows[i]["TOTAL_Difference"].ToString().Trim());
                        }
                    }

                    lblOffered.Text = Math.Round(dblTodayOffered, 0).ToString();
                    lblCR.Text = Math.Round(dblTodayCR, 0).ToString();
                    lblRR.Text = Math.Round(dblTodayRR, 0).ToString();
                    lblSold.Text = Math.Round(dblTodaySold, 0).ToString();
                }
                else
                {
                    file_export.DataSource = null;
                    file_export.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            bindDropDown();
        }

        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            file_export.PageIndex = e.NewPageIndex;
            bindReport();
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            string strFromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy").ToString() + " 00:00:00";
            string strToDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy").ToString() + " 23:59:59";
            string strTodayDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy").ToString();
            DataTable dt = new DataTable();
            try
            {
                dt = ldMgt.FarmerAuthorisedQtyReport(strFromDate, strToDate, strTodayDate, ddlCrop.SelectedItem.Value, ddlVariety.SelectedItem.Value);


                string attachment = "attachment; filename=FarmerAuthorisedQty.xls";
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