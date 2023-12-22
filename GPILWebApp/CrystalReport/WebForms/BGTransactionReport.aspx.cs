using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms
{
    public partial class BGTransactionReport : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //bindDropDown();
                if (!IsPostBack)
                {
                    BindDropDown();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        private void BindDropDown()
        {
            try
            {
                

                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                cbxcrop.DataSource = ds1.Tables[0];
                cbxcrop.DataTextField = "CROPYEAR";
                cbxcrop.DataValueField = "CROP";
                cbxcrop.DataBind();
                cbxcrop.Items.Insert(0, new ListItem("- Select -", "0"));
                //cbxcrop.Items.Insert(0, "SELECT CROP YEAR");

                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                cbxvariety.DataSource = ds2.Tables[0];
                cbxvariety.DataTextField = "VARIETYNAME";
                cbxvariety.DataValueField = "VARIETY";
                cbxvariety.DataBind();
                cbxvariety.Items.Insert(0, new ListItem("- Select -", "0"));



            }
            catch (Exception ex)
            {

            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Rptbiew();
        }

        string sql;
        public void Rptbiew()
        {

            try
            {
                //sql = "select '" + txt_Report_Date.Text + "' as REPORTDATE, I.CROP,I.VARIETY,T.ORGN_CODE+' - '+O.ORGN_NAME AS ORGNNAME ,MAX(I.BG_AMOUNT) AS BG_AMOUNT,MAX(T.UTILIZED_AMOUNT) AS UTILIZED,CONVERT(NVARCHAR(10),MAX(T.BG_DATE),105) AS BG_DATE from GPIL_BG_TRANSACTION T,GPIL_BG_INFORMATION I ,GPIL_ORGN_MASTER O where T.BG_DATE < CONVERT(DATETIME,'" + txt_Report_Date.Text + "', 105)+1 AND I.ORGN_CODE=T.ORGN_CODE AND O.ORGN_CODE=T.ORGN_CODE  and I.CROP='"+cbxcrop.Text+"' and I.VARIETY='"+cbxvariety.Text+"' GROUP BY T.ORGN_CODE,T.ORGN_CODE+' - '+O.ORGN_NAME,I.CROP,I.VARIETY ";
                sql = "SELECT '" + txt_Report_Date.Text + "' AS REPORTDATE,TBL1.CROP,TBL1.VARIETY,(TBL1.ORGN_CODE + ' - ' + O.ORGN_NAME) AS ORGNNAME,MAX(BG_AMOUNT) AS BG_AMOUNT,MAX(UTILIZED_AMOUNT) AS UTILIZED,MAX(TBL1.BG_DATE) AS BG_DATE FROM (SELECT CROP,VARIETY,ORGN_CODE,BG_AMOUNT,UTILIZED_AMOUNT,BG_DATE  FROM GPIL_BG_TRANSACTION  WHERE CROP='" + cbxcrop.Text + "' AND VARIETY='" + cbxvariety.Text + "' AND BG_DATE <= CONVERT(DATETIME,'" + txt_Report_Date.Text + " 23:59:59', 105)) AS TBL1,(SELECT CROP,VARIETY,ORGN_CODE,MAX(BG_DATE) AS BG_DATE  FROM GPIL_BG_TRANSACTION  WHERE CROP='" + cbxcrop.Text + "' AND VARIETY='" + cbxvariety.Text + "' AND BG_DATE <= CONVERT(DATETIME,'" + txt_Report_Date.Text + " 23:59:59', 105) GROUP BY CROP,VARIETY,ORGN_CODE) AS TBL2,GPIL_ORGN_MASTER O WHERE TBL1.ORGN_CODE=TBL2.ORGN_CODE AND  O.ORGN_CODE=TBL1.ORGN_CODE AND TBL1.CROP=TBL2.CROP AND TBL1.VARIETY=TBL2.VARIETY AND TBL1.BG_DATE=TBL2.BG_DATE GROUP BY TBL1.ORGN_CODE,TBL1.CROP,TBL1.VARIETY,O.ORGN_NAME ORDER BY TBL1.ORGN_CODE";

                DataSet ds = new DataSet();
                ds = lpdMgt.GetCompetitionReport(sql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptBGreport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();


               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("BGTransactionReport.aspx");
        }
    }
}