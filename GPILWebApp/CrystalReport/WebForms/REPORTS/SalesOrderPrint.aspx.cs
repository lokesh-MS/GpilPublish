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
    public partial class SalesOrderPrint : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDropDown();
            }

            else
            {
                if (ddlSalesOrderNo.SelectedIndex != -1)
                {
                    BindReport();
                }

            }
        }


        private void bindDropDown()
        {
            try
            {
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("ORGN", "", "");
                ddlOrgnCode.DataSource = ds2.Tables[0];
                ddlOrgnCode.DataTextField = "ORGN_CODE1";
                ddlOrgnCode.DataValueField = "ORGN_CODE";
                ddlOrgnCode.DataBind();
                ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");


                DataSet ds = new DataSet();
                ds2 = crd.GetORGN("SUPP", "", "");
                ddlCustomerCode.DataSource = ds2.Tables[0];
                ddlCustomerCode.DataTextField = "SUPPLIER";
                ddlCustomerCode.DataValueField = "GPIL_SUPP_CODE";
                ddlCustomerCode.DataBind();
                ddlCustomerCode.Items.Insert(0, "select Customer code");

            }
            catch (Exception ex)
            { }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            if (ddlSalesOrderNo.SelectedIndex != -1)
            {
                BindReport();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void BindReport()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                dt = rMgt.BindSalesOrderRpt(ddlSalesOrderNo.SelectedItem.Value);
                if (dt.Rows.Count > 0)
                {
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptLP5.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);
                    rdlc1.ReportSource = CustomerReport;
                    rdlc1.DataBind();

                    ReportDocument CustomerReport1 = new ReportDocument();
                    CustomerReport1.Load(Server.MapPath("~/CrystalReport/rptLP5_Invoice_SO.rpt"));
                    CustomerReport1.SetDataSource(dt.DefaultView);
                    rdlc2.ReportSource = CustomerReport1;
                    rdlc2.DataBind();
                }
                dt1 = rMgt.bindSalesOrderWeightList(ddlSalesOrderNo.SelectedItem.Value);
                if (dt1.Rows.Count > 0)
                {
                    ReportDocument CustomerReport2 = new ReportDocument();
                    CustomerReport2.Load(Server.MapPath("~/CrystalReport/rptWeightList.rpt"));
                    CustomerReport2.SetDataSource(dt1.DefaultView);
                    rdlc3.ReportSource = CustomerReport2;
                    rdlc3.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void ddlCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                DataTable dt = new DataTable();
                dt = rMgt.bindSalesOrderNo(txtFromDate.Text, txtTodate.Text, ddlOrgnCode.SelectedItem.Value, ddlCustomerCode.SelectedItem.Value);
                ddlSalesOrderNo.DataSource = dt;
                ddlSalesOrderNo.DataTextField = "SHIPMENT_NO";
                ddlSalesOrderNo.DataValueField = "SHIPMENT_NO";
                ddlSalesOrderNo.DataBind();
                ddlSalesOrderNo.Items.Insert(0, "select sales order no");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}