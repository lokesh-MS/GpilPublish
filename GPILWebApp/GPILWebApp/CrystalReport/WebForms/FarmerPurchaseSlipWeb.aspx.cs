using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.Models;
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
    public partial class FarmerPurchaseSlipWeb : System.Web.UI.Page
    {
       
        CrystalReportData crd = new CrystalReportData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            else
            {
                ReportBales(ddlOrgnCode.SelectedValue.ToString() + DateTime.Now.ToString("yyyyMMdd"), ddlFarmerCode.SelectedValue.ToString());
            }
        }


        private void GetFarmerCode()
        {
            try
            {
                DataSet ds = new DataSet();
                 ds = crd.GetORGN("FARMCODE", ddlOrgnCode.SelectedValue.ToString() + DateTime.Now.ToString("yyyyMMdd"), "");
               // ds = crd.GetORGN("FARMCODE", ddlOrgnCode.SelectedValue.ToString() + "20150620", "");
                ddlFarmerCode.DataSource = ds.Tables[0];
                ddlFarmerCode.DataTextField = "FARMER_LOT";
                ddlFarmerCode.DataValueField = "FARMER_CODE";
                ddlFarmerCode.DataBind();
                ddlFarmerCode.Items.Insert(0, "SELECT FARMER CODE");

                DataTable dt = new DataTable();
                dt = crd.GetBaleandLotCount(ddlOrgnCode.SelectedValue.ToString() + DateTime.Now.ToString("yyyyMMdd"));

                if(dt.Rows.Count>0)
                {                    
                    lblTotalBales.Text = dt.Rows[0]["BaleCount"].ToString();
                    lblTotalLot.Text = dt.Rows[0]["LotCount"].ToString(); 
                }


            }
            catch
            { }
        }
        protected void ddlOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
           GetFarmerCode();
        }

        protected void ddlFarmerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportBales(ddlOrgnCode.SelectedValue.ToString() +DateTime.Now.ToString("yyyyMMdd"), ddlFarmerCode.SelectedValue.ToString());
           // ReportBales(ddlOrgnCode.SelectedValue.ToString() + "20150620", ddlFarmerCode.SelectedValue.ToString());
        }

        private void ReportBales(string HeaderID, string farmerCode)
        {
           
                try
                {
                    ReportDocument CustomerReport = new ReportDocument();
                    RDLCReport rdlcReport = new RDLCReport();
                    DataSet ds = new DataSet();
                    ds = crd.GetORGN("FPSlip", HeaderID, farmerCode);
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptFarmerPurchaseSlip_New.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                }
            
        }

    }
}