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

    public partial class LP5Printing :  System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlFromOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlFromOrgnCode.DataTextField = "OrgnName";
                    ddlFromOrgnCode.DataValueField = "OrgnCode";
                    ddlFromOrgnCode.DataBind();
                    ddlFromOrgnCode.Items.Insert(0, "< -- Select -- >");


                    ddlToOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlToOrgnCode.DataTextField = "OrgnName";
                    ddlToOrgnCode.DataValueField = "OrgnCode";
                    ddlToOrgnCode.DataBind();
                    ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");

                }
                catch (Exception ex)
                {

                }
            }

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            viewoprpt();

        }
        public void viewoprpt()
        {
            DataTable dt = new DataTable();
            try
            {
                CrystalReportViewer1.Visible = false;
                CrystalReportViewer2.Visible = false;
                CrystalReportViewer3.Visible = false;

                if (ddlShipmentNumber.Text.Trim() == "< -- Select -- >")
                    return;

                dt = rptMgt.GetGradeShipmentDetails(ddlShipmentNumber.SelectedItem.Value);

                if (dt.Rows.Count > 0)
                {
                    string sGrade = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i][1]) <= 0)
                        {
                            sGrade = sGrade + dt.Rows[i][0].ToString().Trim() + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(sGrade.Trim()))
                    {
                        lblMessage.Text = "No Rates for the Following Items, " + sGrade;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "";
                    }
                }
                DataSet dsLP5 = new DataSet();
                //Save GST Invoice Number in GPIL_GST_INVOICE_NO Table
                if (rptMgt.InvoiceNumberGeneration(ddlShipmentNumber.SelectedItem.Value,ddlFromOrgnCode.SelectedItem.Value))
                {
                    dsLP5 = rptMgt.GetLP5Details(ddlShipmentNumber.SelectedItem.Value);


                    ReportDocument LP5 = new ReportDocument();
                    ReportDocument LP5DC = new ReportDocument();
                    ReportDocument weightlist = new ReportDocument();
                    RDLCReport rdlcReport = new RDLCReport();
                    DataSet ds = new DataSet();

                    LP5.Load(Server.MapPath("~/CrystalReport/rptLP5_GST.rpt"));
                    LP5.SetDataSource(ds.Tables[0].DefaultView);
                    //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CrystalReportViewer1.ReportSource = LP5;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.RefreshReport();

                    LP5DC.Load(Server.MapPath("~/CrystalReport/RptLP5_DC.rpt"));
                    LP5DC.SetDataSource(ds.Tables[0].DefaultView);                   
                    CrystalReportViewer2.ReportSource = LP5DC;
                    CrystalReportViewer2.DataBind();
                    CrystalReportViewer2.RefreshReport();
                  
                    dt = new DataTable();
                    dt = rptMgt.GetWeightmentList(ddlShipmentNumber.SelectedItem.Value);
                    weightlist.Load(Server.MapPath("~/CrystalReport/rptWeightList.rpt"));
                    weightlist.SetDataSource(dt);
                    CrystalReportViewer3.ReportSource = weightlist;
                    CrystalReportViewer3.DataBind();
                    CrystalReportViewer3.RefreshReport();
                }
                else { }   
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlShipmentNumber.DataSource = rptMgt.GetShipmentNumber(ddlFromOrgnCode.SelectedItem.Value, ddlToOrgnCode.SelectedItem.Value, txtFromDate.Text);
                ddlShipmentNumber.DataTextField = "SHIPMENT_NO";
                ddlShipmentNumber.DataValueField = "SHIPMENT_NO";
                ddlShipmentNumber.DataBind();
                ddlShipmentNumber.Items.Insert(0, "< -- Select -- >");
            }
            catch (Exception ex)
            {

            }

            
        }

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("FrmHomePage.aspx");
        //}
    }
}