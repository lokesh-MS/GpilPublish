using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using GPI;
using GPILWebApp.ViewModel;
using System.Text;

namespace GPILWebApp
{
    public partial class CompetetionConsolitate : System.Web.UI.Page
    {
        LPDManagementt lpdMgt = new LPDManagementt();
        CrystalReportData crd = new CrystalReportData();
        ReportDocument rd;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDown();
            }
            else
            {
                bindReport();
            }
        }



        private void BindDropDown()
        {
            try
            {

                DataSet ds = new DataSet();


                ds = crd.GetORGN("CROP", "", "");
                ddlCrop.DataSource = ds.Tables[0];
                ddlCrop.DataTextField = "CROPYEAR";
                ddlCrop.DataValueField = "CROP";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, "SELECT CROP YEAR");
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("VARIETY", "", "");
                ddlVariety.DataSource = ds1.Tables[0];
                ddlVariety.DataTextField = "VARIETYNAME";
                ddlVariety.DataValueField = "VARIETY";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, "SELECT VARIETY");
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDate.Text = "";
            txtDate.Focus();
            BindDropDown();
        }

        public bool validate()
        {
            if (txtDate.Text == string.Empty)
            {
                txtDate.Text = "";
                txtDate.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Competation Date');", true);
                return false;
            }
            else if (ddlCrop.SelectedIndex == 0)
            {
                ddlCrop.SelectedIndex = 0;
                ddlCrop.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Orginization Code');", true);
                return false;
            }
            else
            {
                return true;
            }
        }
        string a;
        protected void btnReport_Click(object sender, EventArgs e)
        {
            bindReport();
        }


        private void bindReport()
        {
            a = txtDate.Text;
            try
            {
                string sql = "";
                LPDManagementt lptMgt = new ViewModel.LPDManagementt();
                DataSet ds = new DataSet();
                if (validate())
                {
                    DateTime dt = Convert.ToDateTime(txtDate.Text);
                    txtDate.Text = dt.ToString("dd-MM-yyyy");


                    if (rdbtodayrpt.Checked == true)
                    {
                        sql = "SELECT C.COMPANY_CODE,'" + txtDate.Text + "' AS REPORTDATE,H.HEADER_ID,H.CROP+' - '+CP.CROP_YEAR AS CROP,H.VARIETY+' - '+V.VARIETY_NAME AS VARIETY,D.COMPANY_CODE,SUM(D.NO_OF_BALES) AS TOTALPURCHBALES,H.ORGN_CODE+'  -  '+O.ORGN_NAME AS ORGNAME,C.COMP_GROUP_CODE AS COMPNAME ,H.FLOOR_HB,H.FLOOR_LB,H.GPIL_HB,H.GPIL_LB,H.NB_NS_BALES, ";
                        sql = sql + " H.TOT_OFF_BALES FROM GPIL_COMPETITION_HDR(NOLOCK) H,GPIL_COMPETITION_DTLS(NOLOCK) D,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_COMPANY_MASTER(NOLOCK) C,GPIL_CROP_MASTER(NOLOCK) CP,GPIL_VARIETY_MASTER(NOLOCK) V  WHERE CONVERT(NVARCHAR(10),H.REPORT_DATE,105)='" + txtDate.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and H.CROP='" + ddlCrop.Text + "' AND H.CROP=CP.CROP AND V.VARIETY=H.VARIETY AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID  AND C.COMPANY_CODE=D.COMPANY_CODE ";
                        sql = sql + " GROUP BY C.COMPANY_CODE,H.HEADER_ID,H.CROP+' - '+CP.CROP_YEAR ,H.VARIETY+' - '+V.VARIETY_NAME,D.COMPANY_CODE,H.ORGN_CODE+'  -  '+O.ORGN_NAME,C.COMP_GROUP_CODE ,H.FLOOR_HB,H.FLOOR_LB,H.GPIL_HB,H.GPIL_LB,H.NB_NS_BALES,H.TOT_OFF_BALES ORDER BY C.COMPANY_CODE ";

                        ds = lptMgt.GetCompetetitionConsolidateReport(sql);
                        ReportDocument CustomerReport = new ReportDocument();
                        CustomerReport.Load(Server.MapPath("~/CrystalReport//rptCompetitonConsilidate.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                        CompetitionConsoliateRpt.ReportSource = CustomerReport;
                        CompetitionConsoliateRpt.DataBind();


                    }
                    else if (rdbtodaycomrpt.Checked == true)
                    {
                        sql = "SELECT '" + txtDate.Text + "' AS REPORTDATE, H.CROP+' - '+CP.CROP_YEAR AS CROP,H.VARIETY+' - '+V.VARIETY_NAME AS VARIETY, D.COMPANY_CODE,C.COMP_SHORT_NAME,SUM(D.NO_OF_BALES) AS TODATEBALES,(SELECT (case when SUM(D1.NO_OF_BALES) IS null then 0 else SUM(D1.NO_OF_BALES) end) as NO_OF_BALES ";
                        sql = sql + " FROM GPIL_COMPETITION_DTLS(NOLOCK) D1,GPIL_COMPETITION_HDR(NOLOCK) H1 WHERE H1.CROP='" + ddlCrop.Text + "' AND H1.VARIETY='" + ddlVariety.Text + "' AND CONVERT(NVARCHAR(10),H1.REPORT_DATE,105) = '" + txtDate.Text + "' AND D1.HEADER_ID=H1.HEADER_ID AND D1.COMPANY_CODE=D.COMPANY_CODE) as  TODAYBALES,(SELECT SUM(NB_NS_BALES) FROM GPIL_COMPETITION_HDR(NOLOCK)  WHERE CROP='" + ddlCrop.Text + "' AND VARIETY='" + ddlVariety.Text + "' AND REPORT_DATE < CONVERT(DATETIME,'" + txtDate.Text + " 23:59:59',105)) AS NB_NS_BALES, ";
                        sql = sql + " (SELECT SUM(NB_NS_BALES) FROM GPIL_COMPETITION_HDR(NOLOCK) WHERE CROP='14' AND VARIETY='10' AND CONVERT(NVARCHAR(10),REPORT_DATE,105) = '" + txtDate.Text + "') AS TODAYNBNSBALES,(SELECT SUM(TOT_OFF_BALES) FROM GPIL_COMPETITION_HDR(NOLOCK) WHERE CROP='14' AND VARIETY='10' AND CONVERT(NVARCHAR(10),REPORT_DATE,105) = '" + txtDate.Text + "') AS TODAYOFFBALES,";
                        sql = sql + " (SELECT SUM(TOT_OFF_BALES) FROM GPIL_COMPETITION_HDR(NOLOCK)  WHERE CROP='" + ddlCrop.Text + "' AND VARIETY='" + ddlVariety.Text + "' AND REPORT_DATE < CONVERT(DATETIME,'" + txtDate.Text + "',105)+1) AS TOT_OFF_BALES FROM GPIL_COMPETITION_DTLS(NOLOCK) D,GPIL_COMPETITION_HDR(NOLOCK) H,GPIL_COMPANY_MASTER(NOLOCK) C ,GPIL_CROP_MASTER(NOLOCK) CP,GPIL_VARIETY_MASTER(NOLOCK) V WHERE C.COMPANY_CODE=D.COMPANY_CODE AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.REPORT_DATE < CONVERT(DATETIME,'" + txtDate.Text + " 23:59:59',105) AND D.HEADER_ID=H.HEADER_ID AND CP.CROP=H.CROP AND V.VARIETY=H.VARIETY ";
                        sql = sql + " GROUP BY D.COMPANY_CODE ,C.COMP_SHORT_NAME,H.CROP+' - '+CP.CROP_YEAR,H.VARIETY+' - '+V.VARIETY_NAME order by D.COMPANY_CODE  ";


                        ds = lptMgt.GetCompetetitionConsolidateReport(sql);
                        ReportDocument CustomerReport = new ReportDocument();
                        CustomerReport.Load(Server.MapPath("~/CrystalReport//rptCompetitonConsilidate1.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                        CompetitionConsoliateRpt.ReportSource = CustomerReport;
                        CompetitionConsoliateRpt.DataBind();

                    }
                    else if (rdbtodaysummary.Checked == true)
                    {
                        sql = "SELECT '13-09-2014' AS REPORTDATE,H.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME as ORGN_NAME,H.CROP+' - '+CP.CROP_YEAR AS CROP,H.VARIETY+' - '+V.VARIETY_NAME AS VARIETY,TOT_SOLD_BALES,NB_NS_BALES,TOT_OFF_BALES,FLOOR_HB, ";
                        sql = sql + " FLOOR_LB,GPIL_HB,GPIL_LB ,H.REPORT_DATE,H.VARIETY ,H.ORGN_CODE,H.CROP,(SELECT DISTINCT REPLACE((Select DISTINCT CH.COMP_GROUP_CODE as [data()] From GPIL_COMPETITION_DTLS(NOLOCK) DH,GPIL_COMPANY_MASTER(NOLOCK) CH,GPIL_COMPETITION_HDR(NOLOCK) HH ";
                        sql = sql + " Where CH.COMPANY_CODE=DH.COMPANY_CODE AND HH.HEADER_ID=DH.HEADER_ID AND DH.HIGHEST_BID=HH1.FLOOR_HB AND HH.REPORT_DATE=HH1.REPORT_DATE and HH.VARIETY=HH1.VARIETY and HH.CROP=HH1.CROP GROUP BY CH.COMP_GROUP_CODE,DH.HIGHEST_BID FOR XML PATH ('') ), ' ', ';') FROM GPIL_COMPETITION_DTLS(NOLOCK) DH1,GPIL_COMPETITION_HDR(NOLOCK) HH1 ";
                        sql = sql + " WHERE HH1.HEADER_ID=DH1.HEADER_ID and HH1.REPORT_DATE=H.REPORT_DATE and HH1.VARIETY=H.VARIETY and HH1.CROP=H.CROP ) AS HIGESTBIDCOMP,(SELECT DISTINCT REPLACE((Select DISTINCT CH.COMP_GROUP_CODE as [data()] From GPIL_COMPETITION_DTLS(NOLOCK) DH,GPIL_COMPANY_MASTER(NOLOCK) CH,GPIL_COMPETITION_HDR(NOLOCK) HH ";
                        sql = sql + " Where CH.COMPANY_CODE=DH.COMPANY_CODE AND HH.HEADER_ID=DH.HEADER_ID AND DH.LOWEST_BID=HH1.FLOOR_LB AND HH.REPORT_DATE=HH1.REPORT_DATE and HH.VARIETY=HH1.VARIETY and HH.CROP=HH1.CROP GROUP BY CH.COMP_GROUP_CODE,DH.HIGHEST_BID FOR XML PATH ('') ), ' ', ',')FROM GPIL_COMPETITION_DTLS(NOLOCK) DH1,GPIL_COMPETITION_HDR(NOLOCK) HH1 WHERE HH1.HEADER_ID=DH1.HEADER_ID and HH1.REPORT_DATE=H.REPORT_DATE and HH1.VARIETY=H.VARIETY and HH1.CROP=H.CROP) AS LOWESTBIDCOMP";
                        sql = sql + " FROM GPIL_COMPETITION_DTLS(NOLOCK) D, GPIL_COMPETITION_HDR(NOLOCK) H,GPIL_CROP_MASTER(NOLOCK) CP,GPIL_VARIETY_MASTER(NOLOCK) V,GPIL_ORGN_MASTER(NOLOCK) O WHERE V.VARIETY=H.VARIETY AND CP.CROP=H.CROP AND H.HEADER_ID=D.HEADER_ID AND O.ORGN_CODE=H.ORGN_CODE and CONVERT(NVARCHAR(10),H.REPORT_DATE,105)='" + txtDate.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and H.CROP='" + ddlCrop.Text + "' GROUP BY H.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP+' - '+CP.CROP_YEAR,H.VARIETY+' - '+V.VARIETY_NAME,NB_NS_BALES,TOT_OFF_BALES,";
                        sql = sql + " FLOOR_HB,FLOOR_LB,GPIL_HB,GPIL_LB,TOT_SOLD_BALES,H.REPORT_DATE,H.VARIETY ,H.ORGN_CODE,H.CROP";
                        ds = lptMgt.GetCompetetitionConsolidateReport(sql);
                        ReportDocument CustomerReport = new ReportDocument();
                        CustomerReport.Load(Server.MapPath("~/CrystalReport//rptCompetetionReport_111.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                        CompetitionConsoliateRpt.ReportSource = CustomerReport;
                        CompetitionConsoliateRpt.DataBind();
                    }

                    a = txtDate.Text = a;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

    }
}