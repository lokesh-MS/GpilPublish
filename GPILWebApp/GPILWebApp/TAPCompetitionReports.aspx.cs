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

namespace GPILWebApp
{
    public partial class TAPCompetitionReports : System.Web.UI.Page
    {
        public static string variety;
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
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
                DataSet ds1 = new DataSet();
                ds1 = lpdMgt.GetCrop();
                ddlCrop.DataSource = ds1.Tables[0];
                ddlCrop.DataTextField = "Crop_Year";
                ddlCrop.DataValueField = "Crop";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, "SELECT CROP YEAR");
            }
            catch (Exception ex)
            { }
        }


        protected void ddlOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlOrgnCode.SelectedIndex != 0)
                {
                    DataSet ds = new DataSet();
                    ds = lpdMgt.GetVariety(ddlOrgnCode.SelectedItem.Value);
                    if (ds.Tables[0].Rows.Count > 0)
                        variety = ds.Tables[0].Rows[0]["Variety"].ToString();

                }
            }

            catch (Exception ex)
            {

            }
        }
        private void ReportBales()
        {
            DataSet ds = new DataSet();
            LPDManagementt lpbMgt = new LPDManagementt();
            try
            {
                ReportDocument CustomerReport = new ReportDocument();
                ds = lpbMgt.GetCompetitionReport(ddlOrgnCode.SelectedItem.Value, variety, ddlCrop.SelectedItem.Value, txtDate.Text.Trim());
                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptCompetetionReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                ReportBales();
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
    }
    }