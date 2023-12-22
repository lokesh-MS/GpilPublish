using CrystalDecisions.CrystalReports.Engine;
using GPI;
using GPILWebApp.Models;
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
    public partial class FarmerPurchaseSlipDatewise : System.Web.UI.Page
    {

        CrystalReportData crd = new CrystalReportData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
            }
            else
            { 
                if(ddlFarmerCode.SelectedIndex!=0)

                ReportBales(ddlOrgnCode.SelectedValue.ToString() + DateTime.Parse(txtDate.Text).ToString("yyyyMMdd"), ddlFarmerCode.SelectedValue.ToString());
            }
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
                ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");            

            }
            catch (Exception ex)
            { }
        }

        private void GetFarmerCode()
        {
            DataSet ds = new DataSet();
            try
            {
                
                ds = crd.GetORGN("FARMCODE", ddlOrgnCode.SelectedValue.ToString() + DateTime.Parse(txtDate.Text).ToString("yyyyMMdd"), "");
                //ds = crd.GetORGN("FARMCODE", ddlOrgnCode.SelectedValue.ToString() + DateTime.Parse(txtDate.Text).ToString("yyyyMMdd"), "");
                ddlFarmerCode.DataSource = ds.Tables[0];
                ddlFarmerCode.DataTextField = "FARMER_LOT";
                ddlFarmerCode.DataValueField = "FARMER_CODE";
                ddlFarmerCode.DataBind();
                ddlFarmerCode.Items.Insert(0, "SELECT FARMER CODE");
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                
            }
        }
        protected void ddlOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFarmerCode();
        }

        protected void ddlFarmerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //"20150620"
            //DateTime.Parse(txtDate.Text).ToString("yyyyMMdd")
            ReportBales(ddlOrgnCode.SelectedValue.ToString() + DateTime.Parse(txtDate.Text).ToString("yyyyMMdd"), ddlFarmerCode.SelectedValue.ToString());
        }

        private void ReportBales(string HeaderID, string farmerCode)
        {
            DataSet ds = new DataSet();
            try
            {
                ReportDocument CustomerReport = new ReportDocument();
               

                ds = crd.GetORGN("DATEWISESLIP", HeaderID, farmerCode);
                CustomerReport.Load(Server.MapPath("~/CrystalReport/WebForms/LD/RptFarmerPurchaseSlipFinalByDate.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                FarmerPurchaseSlipDateWise.ReportSource = CustomerReport;
                FarmerPurchaseSlipDateWise.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                
            }



        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            ddlOrgnCode.SelectedIndex = 0;
            ddlFarmerCode.Items.Clear();
            txtDate.Text = "<--Select-->";
            ddlFarmerCode.Items.Insert(0, "SELECT FAMER CODE");
            

        }
    }
}