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

namespace GPILWebApp
{
    public partial class TodateCRRRReports : System.Web.UI.Page
    {
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
                ddlCrop.Items.Insert(0, "-Select-");

                dt = cMgt.BindDropDownVariety();
                ddlVariety.DataSource = dt;
                ddlVariety.DataTextField = "Display";
                ddlVariety.DataValueField = "Variety";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, "-Select-");




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

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                bindReport();
            }
            catch (Exception ex)
            {

            }
        }


        private void bindReport()
        {
            DataTable dt = new DataTable();
            LDManagement lMgt = new LDManagement();
            try
            {
                string strDate = Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd").ToString();
                string strCrop = ddlCrop.SelectedItem.Value;
                string strVariety = ddlVariety.SelectedItem.Value;
                dt = lMgt.TodateCRRRBales(strCrop, strVariety, txtDate.Text, strDate);
                //int intOrgnCount=0;
                double dblTodayOffered = 0, dblTodayCR = 0, dblTodayRR = 0, dblTodaySold = 0;
                double dblTodateOffered = 0, dblTodateCR = 0, dblTodateRR = 0, dblTodateSold = 0;

                if (dt.Rows.Count > 0)
                {
                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dblTodayOffered = dblTodayOffered + Convert.ToDouble(dt.Rows[i]["TODAY_OFFERED"].ToString().Trim());
                        dblTodayCR = dblTodayCR + Convert.ToDouble(dt.Rows[i]["TODAY_CR"].ToString().Trim());
                        dblTodayRR = dblTodayRR + Convert.ToDouble(dt.Rows[i]["TODAY_RR"].ToString().Trim());
                        dblTodaySold = dblTodaySold + Convert.ToDouble(dt.Rows[i]["TODAY_SOLD"].ToString().Trim());

                        dblTodateOffered = dblTodateOffered + Convert.ToDouble(dt.Rows[i]["TODATE_OFFERED"].ToString().Trim());
                        dblTodateCR = dblTodateCR + Convert.ToDouble(dt.Rows[i]["TODATE_CR"].ToString().Trim());
                        dblTodateRR = dblTodateRR + Convert.ToDouble(dt.Rows[i]["TODATE_RR"].ToString().Trim());
                        dblTodateSold = dblTodaySold + Convert.ToDouble(dt.Rows[i]["TODATE_SOLD"].ToString().Trim());


                    }
                    lblOffered.Text = "Today = " + Math.Round(dblTodayOffered, 0).ToString() + " & Todate = " + Math.Round(dblTodateOffered, 0).ToString();
                    lblCR.Text = "Today = " + Math.Round(dblTodayCR, 0).ToString() + " & Todate = " + Math.Round(dblTodateCR, 0).ToString();
                    lblRR.Text = "Today = " + Math.Round(dblTodayRR, 0).ToString() + " & Todate = " + Math.Round(dblTodateRR, 0).ToString();
                    lblSold.Text = "Today = " + Math.Round(dblTodaySold, 0).ToString() + " & Todate = " + Math.Round(dblTodateSold, 0).ToString();


                }

                else
                {
                    gvReport.DataSource = null;
                    gvReport.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtDate.Text = "";
            bindDropDown();
            gvReport.DataSource = null;
            gvReport.DataBind();
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            LDManagement lMgt = new LDManagement();
            try
            {

                string strDate = Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd").ToString();
                string strCrop = ddlCrop.SelectedItem.Value;
                string strVariety = ddlVariety.SelectedItem.Value;
                dt = lMgt.TodateCRRRBales(strCrop, strVariety, txtDate.Text, strDate);
                string attachment = "attachment; filename=TodateCRRR.xls";
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