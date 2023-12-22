using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.GLT
{
    public partial class ScrapDegradationReport : System.Web.UI.Page
    {

        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        GLTManagement gMgt = new GLTManagement();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDropDown();

                }
                else
                {
                    btnview_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        string query, query1, query2;
        public void bindDropDown()
        {
       
             DataTable dt = new DataTable();

            query = "Select Distinct[Crop] from [dbo].[GPIL_ScrapDegradation]";
            dt = gMgt.GetQueryResult(query);
            ddlCrop.DataSource = dt;
            ddlCrop.DataBind();
            ddlCrop.DataTextField = "Crop";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("---Select---", "0"));
            DataTable dt1 = new DataTable();

            query1 = "Select Distinct[Variety] from [dbo].[GPIL_ScrapDegradation]";
            dt1 = gMgt.GetQueryResult(query1);
            ddlVariety.DataSource = dt1;
            ddlVariety.DataBind();
            ddlVariety.DataTextField = "Variety";
            ddlVariety.DataBind();
            ddlVariety.Items.Insert(0, new ListItem("---Select---", "0"));

            DataTable dt2 = new DataTable();

            query2 = "Select Distinct[ScrapGrade] from [dbo].[GPIL_ScrapDegradation]";
            dt2 = gMgt.GetQueryResult(query2);
            ddlScrapGrade.DataSource = dt2;
            ddlScrapGrade.DataBind();
            ddlScrapGrade.DataTextField = "ScrapGrade";
            ddlScrapGrade.DataBind();
            ddlScrapGrade.Items.Insert(0, new ListItem("---Select---", "0"));


          
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                dt.Clear();
                string query = "SELECT  [Crop],[Variety],[ScrapGrade] ,[ScrapDate]=(convert(varchar,[ScrapDate],103)),[RunNo],[SampleTime]=(convert(varchar,[SampleTime],108)),[RunCaseNo],[TotalSampleWeight],[Held5],[Held5Percent]";
                query += ",[Held10],[Held10Percent],[Held20],[Held20Percent],[Pan],[PanPercent],[SampleWeight]";
                query += ",[FiberGrams],[FiberPercent],[Attribute1] FROM [dbo].[GPIL_ScrapDegradation] where";
                if (txt_From_Date.Text != string.Empty && txt_To_Date.Text != string.Empty)
                {
                    query += " [ScrapDate] between CONVERT(varchar,'" + txt_From_Date.Text + " 00:00:00',103) and CONVERT(varchar,'" + txt_To_Date.Text + " 23:59:59',103) and";

                }

                if (ddlCrop.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Crop]='" + ddlCrop.SelectedItem.ToString() + "' and";
                }

                if (ddlVariety.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Variety]='" + ddlVariety.SelectedItem.ToString() + "' and";
                }

                if (ddlScrapGrade.SelectedItem.ToString() != "---Select---")
                {
                    query += " [ScrapGrade]='" + ddlScrapGrade.SelectedItem.ToString() + "' and";
                }
                query = query.Substring(0, query.Length - 3);

                //SqlCommand csm = new SqlCommand(query, con);
                //SqlDataAdapter sda = new SqlDataAdapter(query, con);
                //sda.SelectCommand.CommandTimeout = 0;
                //sda.Fill(dt);
                //sda.Dispose();
                dt = gMgt.GetQueryResult(query);
                // For Crystal reports 

                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/CrystalReport/WebForms/GLT/RptScrapDegradation.rpt"));

               // rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                rd.SetDataSource(dt);

                rd.SetParameterValue("TXTFROM", txt_From_Date.Text == "" ? "NA" : txt_From_Date.Text);
                rd.SetParameterValue("TXTTODATE", txt_To_Date.Text == "" ? "NA" : txt_To_Date.Text);
                rd.SetParameterValue("TXTCROP", ddlCrop.SelectedItem.ToString() == "---Select---" ? "NA" : ddlCrop.SelectedItem.ToString());
                rd.SetParameterValue("TXTGRADE", ddlScrapGrade.SelectedItem.ToString() == "---Select---" ? "NA" : ddlScrapGrade.SelectedItem.ToString());
                rd.SetParameterValue("TXTVARIETY", ddlVariety.SelectedItem.ToString() == "---Select---" ? "NA" : ddlVariety.SelectedItem.ToString());
                CrystalReportViewer1.ReportSource = rd;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.SeparatePages = false;
                // CrystalReportViewer1.RefreshReport();

                //csm.Dispose();
            }
            catch (Exception es)
            {
                //lblMessage.Visible = true;
                //lblMessage.Text = es.ToString();
              //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }
    }
}