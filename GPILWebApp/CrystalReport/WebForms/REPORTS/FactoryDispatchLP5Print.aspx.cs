using GPILWebApp.ViewModel;
using CrystalDecisions.CrystalReports.Engine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class FactoryDispatchLP5Print : System.Web.UI.Page
    {


        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlFromOrgnCode.DataSource = rptMgt.GetFrmOrnCode();
                    ddlFromOrgnCode.DataTextField = "OrgnName";
                    ddlFromOrgnCode.DataValueField = "OrgnCode";
                    ddlFromOrgnCode.DataBind();
                    ddlFromOrgnCode.Items.Insert(0, "< -- Select -- >");


                    ddlToOrgnCode.DataSource = rptMgt.GetToOrnCode();
                    ddlToOrgnCode.DataTextField = "OrgnName";
                    ddlToOrgnCode.DataValueField = "OrgnCode";
                    ddlToOrgnCode.DataBind();
                    ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");

                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                if (ddlShipmentNumber.SelectedIndex != 0)
                {
                    viewoprpt();
                }
            }
        }

        protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
                ddlShipmentNumber.Items.Insert(0, "<--Select-->");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
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
                //CrystalReportViewer1.Visible = false;
                //CrystalReportViewer2.Visible = false;
                //CrystalReportViewer3.Visible = false;

                if (ddlShipmentNumber.Text.Trim() == "< -- Select -- >")
                    return;
                dt = rptMgt.GetFactoryLP5GradeShipmentDetails(ddlShipmentNumber.SelectedItem.Value);
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
                if (rptMgt.InvoiceNumberGeneration(ddlShipmentNumber.SelectedItem.Value, ddlFromOrgnCode.SelectedItem.Value))
                {
                    dsLP5 = rptMgt.FACTORYGetLP5Details(ddlShipmentNumber.SelectedItem.Value);


                    ReportDocument LP5 = new ReportDocument();
                    ReportDocument LP5DC = new ReportDocument();
                    ReportDocument LP5DC1 = new ReportDocument();
                    ReportDocument weightlist = new ReportDocument();
                    RDLCReport rdlcReport = new RDLCReport();
                    DataSet ds = new DataSet();
                    LP5.Load(Server.MapPath("~/CrystalReport/rptLP5_Invoice.rpt"));
                    LP5.SetDataSource(dsLP5.Tables[0].DefaultView);
                    //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CrystalReportViewer1.ReportSource = LP5;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.RefreshReport();

                    LP5DC1.Load(Server.MapPath("~/CrystalReport/RptFactoryLP5_DC.rpt"));
                    LP5DC1.SetDataSource(dsLP5.Tables[0].DefaultView);
                    CrystalReportViewer4.ReportSource = LP5DC1;
                    CrystalReportViewer4.DataBind();
                    CrystalReportViewer4.RefreshReport();
                    LP5DC.Load(Server.MapPath("~/CrystalReport/RptFactoryLP5_GST.rpt"));
                    LP5DC.SetDataSource(dsLP5.Tables[0].DefaultView);
                    CrystalReportViewer2.ReportSource = LP5DC;
                    CrystalReportViewer2.DataBind();
                    CrystalReportViewer2.RefreshReport();


                    dt = new DataTable();
                    dt = rptMgt.GetWeightmentList(ddlShipmentNumber.SelectedItem.Value);
                    weightlist.Load(Server.MapPath("~/CrystalReport/RptFactoryLP5WeightList.rpt"));
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

    }


    }
