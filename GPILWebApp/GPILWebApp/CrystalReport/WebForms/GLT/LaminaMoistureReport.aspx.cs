using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GPILWebApp.ViewModel.GLT;

namespace GPILWebApp.CrystalReport.WebForms.GLT
{
    public partial class LaminaMoistureReport : System.Web.UI.Page
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

            query = "Select Distinct[Crop] from [dbo].[GPIL_LamiaGrade]";
            dt = gMgt.GetQueryResult(query);
            ddlCrop.DataSource = dt;
            ddlCrop.DataBind();
            ddlCrop.DataTextField = "Crop";
            ddlCrop.DataBind();
            ddlCrop.Items.Insert(0, new ListItem("---Select---", "0"));
            DataTable dt1 = new DataTable();

            query1 = "Select Distinct[Grade] FROM [dbo].[GPIL_LamiaGrade]";
            dt1 = gMgt.GetQueryResult(query1);
            ddlGrade.DataSource = dt1;
            ddlGrade.DataBind();
            ddlGrade.DataTextField = "Grade";
            ddlGrade.DataBind();
            ddlGrade.Items.Insert(0, new ListItem("---Select---", "0"));

            DataTable dt2 = new DataTable();

            query2 = "Select Distinct[Type] FROM [dbo].[GPIL_LamiaGrade]";
            dt2 = gMgt.GetQueryResult(query2);
            ddlType.DataSource = dt2;
            ddlType.DataBind();
            ddlType.DataTextField = "Type";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("---Select---", "0"));



        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                dt.Clear();

                // query = "SELECT [Crop],[Variety],[StripGrade],[ScrapGrade],[Date]=(Convert(varchar,[Date],103)),[Time]=(Convert(varchar,[Time],108)),[RunNo],[RunCaseNo],[AccCaseNO],[MoistureResult],[CaseTemp] FROM [GPI].[dbo].[GPIL_Scrap_Moisture] where";
                string query = "SELECT [Crop],[Type],[Grade],[GradeCode],[Date]=(Convert(varchar,[Date],103)),[SampleTime],[RunNo],[RunCaseNo],[TimeIn],[TimeOut],[Results],[AfterCF] ,[PackedTemp],[GrindingStartTIme],[GrindingEndTIme] FROM [dbo].[GPIL_LamiaGrade] where ";

                if (txt_From_Date.Text != string.Empty && txt_To_Date.Text != string.Empty)
                {
                    query += " [Date] between CONVERT(varchar,'" + txt_From_Date.Text + " 00:00:00',103) and CONVERT(varchar,'" + txt_To_Date.Text + " 23:59:59',103) and";

                }

                if (ddlType.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Type]='" + ddlType.SelectedItem.ToString() + "' and";
                }

                if (ddlGrade.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Grade]='" + ddlGrade.SelectedItem.ToString() + "' and";
                }
                if (ddlCrop.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Crop]='" + ddlCrop.SelectedItem.ToString() + "' and";
                }

                query = query.Substring(0, query.Length - 3);

                ////SqlCommand csm = new SqlCommand(query, con);
                //SqlDataAdapter sda = new SqlDataAdapter(query, con);
                //sda.SelectCommand.CommandTimeout = 0;
                //dt.Clear();
                //sda.Fill(dt);
                dt = gMgt.GetQueryResult(query);

                // For Crystal reports 

                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/CrystalReport/WebForms/GLT/RptLamia.rpt"));

             //   rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                rd.SetDataSource(dt);
                rd.SetParameterValue("TXTFROMDATE", txt_From_Date.Text == "" ? "NA" : txt_From_Date.Text);
                rd.SetParameterValue("TXTTODATE", txt_To_Date.Text == "" ? "NA" : txt_To_Date.Text);
                rd.SetParameterValue("TXTCROP", ddlCrop.SelectedItem.ToString() == "---Select---" ? "NA" : ddlCrop.SelectedItem.ToString());
                rd.SetParameterValue("TXTGRADE", ddlGrade.SelectedItem.ToString() == "---Select---" ? "NA" : ddlGrade.SelectedItem.ToString());
                rd.SetParameterValue("TXTTYPE", ddlType.SelectedItem.ToString() == "---Select---" ? "NA" : ddlType.SelectedItem.ToString());

                CrystalReportViewer1.ReportSource = rd;
                //CrystalReportViewer1.RefreshReport();

                //csm.Dispose();
            }
            catch (Exception es)
            {
                //lblMessage.Visible = true;
                //lblMessage.Text = es.ToString();
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }
    }
}