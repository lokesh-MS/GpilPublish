using CrystalDecisions.CrystalReports.Engine;
using GPI;
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
    public partial class FPSlip : System.Web.UI.Page
    {
        LDManagement lMgt = new LDManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();

            }
            BindReport();
          //  bindDropDown();
        }

        private void bindDropDown()
        {
            DataTable dt = new DataTable();
            CommonManagement cMgt = new CommonManagement();
            try
            {
                dt = cMgt.BindDropDownOrgn();
                ddlOrganizationCode.DataSource = dt;
                ddlOrganizationCode.DataTextField = "Display";
                ddlOrganizationCode.DataValueField = "Orgn_Code";
                ddlOrganizationCode.DataBind();
                ddlOrganizationCode.Items.Insert(0, "-Select");
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ReportDocument CustomerReport = new ReportDocument();
            try
            {
                dt = lMgt.FarmerPurchaseSlip(ddlOrganizationCode.Text, ddlFarmerCode.Text);
                if (dt.Rows.Count > 0)
                {
                    CustomerReport.Load(Server.MapPath("~/Reports/RptFarmerPurchaseSlip_New.rpt"));
                    CustomerReport.SetDataSource(dt);
                    //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            bindDropDown();
        }

        protected void btnReference_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            try
            {
                string strHID = "";
                strHID = ddlOrganizationCode.SelectedItem.Value + "20150217";// DateTime.Now.ToString("yyyyMMdd");
                dt = lMgt.GetFarmerList(strHID);
                ddlFarmerCode.DataSource = dt;
                ddlFarmerCode.DataTextField = "FARMER_LOT";
                ddlFarmerCode.DataValueField = "FARMER_CODE";
                ddlFarmerCode.DataBind();
                ddlFarmerCode.Items.Insert(0, "-Select");
                //divDetails.Visible = true;
                dt = null;
                dt = lMgt.GetFarmerPurchaseDetailsCount(strHID);
                if (dt.Rows.Count > 0)
                {

                    lblTotalLots.Text = dt.Rows[0][0].ToString();
                    lblTotalNumberofBales.Text = dt.Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlOrganizationCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            try
            {
                string strHID = "";
                strHID = ddlOrganizationCode.SelectedItem.Value + "20150217";// DateTime.Now.ToString("yyyyMMdd");
                dt = lMgt.GetFarmerList(strHID);
                ddlFarmerCode.DataSource = dt;
                ddlFarmerCode.DataTextField = "FARMER_LOT";
                ddlFarmerCode.DataValueField = "FARMER_CODE";
                ddlFarmerCode.DataBind();
                ddlFarmerCode.Items.Insert(0, "-Select");
                //divDetails.Visible = true;
                dt = null;
                dt = lMgt.GetFarmerPurchaseDetailsCount(strHID);
                if (dt.Rows.Count > 0)
                {
                    lblTotalLots.Text = dt.Rows[0][0].ToString();
                    lblTotalNumberofBales.Text = dt.Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlFarmerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            DataTable dt = new DataTable();
            ReportDocument CustomerReport = new ReportDocument();
            try
            {
                dt = lMgt.FarmerPurchaseSlip(ddlOrganizationCode.Text, ddlFarmerCode.Text);
                if (dt.Rows.Count > 0)
                {
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptFarmerPurchaseSlip_New.rpt"));
                    CustomerReport.SetDataSource(dt);                  
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
                else
                {
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptFarmerPurchaseSlip_New.rpt"));
                    CustomerReport.SetDataSource(dt);                  
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}