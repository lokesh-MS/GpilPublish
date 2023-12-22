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
    public partial class CompetitionReport : System.Web.UI.Page
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
                else
                {
                    BindReport();
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
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                cbxorgcode.DataSource = ds.Tables[0];
                cbxorgcode.DataTextField = "ORGN_CODE1";
                cbxorgcode.DataValueField = "ORGN_CODE";
                cbxorgcode.DataBind();
                cbxorgcode.Items.Insert(0, new ListItem("- Select -", "0"));
                //cbxorgcd.Items.Insert(0, "SELECT ORGN CODE");

                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                cbxcrop.DataSource = ds1.Tables[0];
                cbxcrop.DataTextField = "CROPYEAR";
                cbxcrop.DataValueField = "CROP";
                cbxcrop.DataBind();
                cbxcrop.Items.Insert(0, new ListItem("- Select -", "0"));
                //cbxcrop.Items.Insert(0, "SELECT CROP YEAR");



            }
            catch (Exception ex)
            {

            }
        }
        string a;
        protected void btnReport_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            //try
            //{
                a = txtReportDate.Text;

                try
                {
                    if (validate())
                    {
                        DateTime dt = Convert.ToDateTime(txtReportDate.Text);
                        txtReportDate.Text = dt.ToString("dd-MM-yyyy");

                        repview = 1;




                        sql = "SELECT D.COMPANY_CODE,H.ORGN_CODE,C.COMP_GROUP_CODE AS COMPANY_NAME,C.COMPANY_NAME AS CNAME,H.ORGN_CODE,D.COMPANY_CODE,convert(nvarchar(10),H.REPORT_DATE,105) as REPORT_DATE,H.REPORT_NO,D.NO_OF_BALES,H.TOT_SOLD_BALES, H.NB_NS_BALES,H.TOT_OFF_BALES,H.FLOOR_HB,H.FLOOR_LB,H.GPIL_HB,H.GPIL_LB,H.CLSTR_OFF_TODAY,H.CLSTR_OFF_TOMORROW,u.USER_NAME,H.BALES_MRKTD,H.AVG_PRICE_MRKTD,H.QTY_MRKTD,";

                        sql = sql + " (SELECT  SUM(H.BALES_MRKTD) FROM GPIL_COMPETITION_HDR H WHERE H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + variety + "' and H.REPORT_DATE<CONVERT(DATETIME,'" + txtReportDate.Text + "',105)+1) AS TOTBALES_MRKTD,(select SUM(QTY_MRKTD) from GPIL_COMPETITION_HDR where CROP='" + cbxcrop.Text + "' and VARIETY='" + variety + "' AND ORGN_CODE='" + cbxorgcode.Text + "'  and REPORT_DATE < CONVERT(datetime,'" + txtReportDate.Text + "',105)+1)  AS TOTQTY_MRKTD ,";

                        sql = sql + " (select ROUND(SUM(QTY_MRKTD*AVG_PRICE_MRKTD)/NULLIF(SUM(QTY_MRKTD),0),2) from GPIL_COMPETITION_HDR where CROP='" + cbxcrop.Text + "' and VARIETY='" + variety + "' AND ORGN_CODE='" + cbxorgcode.Text + "' and REPORT_DATE < CONVERT(datetime,'" + txtReportDate.Text + "',105)+1)  AS AVG_PRICE_MRKTD,(SELECT COUNT(*) FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + variety + "' AND D.STATUS='Y' AND H.HEADER_ID=D.HEADER_ID  and H.DATE_OF_PURCH < CONVERT(datetime,'" + txtReportDate.Text + "',105)+1)  AS NOOFBALES ,";

                        sql = sql + " (SELECT ROUND(SUM(NET_WT),2) FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + variety + "' AND D.STATUS='Y'  AND H.HEADER_ID=D.HEADER_ID  and H.DATE_OF_PURCH < CONVERT(datetime,'" + txtReportDate.Text + "',105)+1) AS NET_WT,(SELECT SUM(VALUE) FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + variety + "'  AND D.STATUS='Y' AND H.HEADER_ID=D.HEADER_ID  and H.DATE_OF_PURCH < CONVERT(datetime,'" + txtReportDate.Text + "',105)+1)  AS VALUE,";

                        sql = sql + " (SELECT COUNT(*) AS NOOFBALES FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE D.STATUS='Y' AND  H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + variety + "' AND H.HEADER_ID=D.HEADER_ID AND CONVERT(NVARCHAR(10), H.DATE_OF_PURCH,105)='" + txtReportDate.Text + "') as todaygpiNOOFBALES,(SELECT ROUND(SUM(NET_WT),2) AS NET_WT FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE D.STATUS='Y' AND  H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + variety + "' AND H.HEADER_ID=D.HEADER_ID AND CONVERT(NVARCHAR(10), H.DATE_OF_PURCH,105)='" + txtReportDate.Text + "') as todaygpiNET_WT,";

                        sql = sql + " (SELECT SUM(VALUE) AS VALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE D.STATUS='Y' AND  H.ORGN_CODE='" + cbxorgcode.Text + "' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + variety + "' AND H.HEADER_ID=D.HEADER_ID AND CONVERT(NVARCHAR(10), H.DATE_OF_PURCH,105)='" + txtReportDate.Text + "') as todaygpiavlue , (SELECT COUNT(*) FROM GPIL_STOCK WHERE CURR_ORGN_CODE='" + cbxorgcode.Text + "' AND STATUS='Y' AND PROCESS_STATUS='N'  AND CREATED_DATE< CONVERT(DATETIME,'" + txtReportDate.Text + "',105)+1 AND SUBSTRING(GPIL_BALE_NUMBER,1,2)='" + cbxcrop.Text + "' AND  SUBSTRING(GPIL_BALE_NUMBER,3,2)='" + variety + "') AS CURRSTK,";

                        sql = sql + " (SELECT SUM(MARKED_WT) FROM GPIL_STOCK WHERE CURR_ORGN_CODE='" + cbxorgcode.Text + "' AND STATUS='Y' AND PROCESS_STATUS='N'  AND CREATED_DATE< CONVERT(DATETIME,'" + txtReportDate.Text + "',105)+1 AND SUBSTRING(GPIL_BALE_NUMBER,1,2)='" + cbxcrop.Text + "' AND  SUBSTRING(GPIL_BALE_NUMBER,3,2)='" + variety + "') AS QUANTITY, (SELECT BG_AMOUNT FROM GPIL_BG_INFORMATION WHERE CROP='" + cbxcrop.Text + "' AND VARIETY='" + variety + "' AND ORGN_CODE='" + cbxorgcode.Text + "') AS BGAMOUNT ";

                        sql = sql + " FROM GPIL_COMPETITION_HDR H,GPIL_COMPETITION_DTLS D,GPIL_COMPANY_MASTER C,GPIL_USER_MASTER U,GPIL_ORGN_MASTER O";

                        sql = sql + " WHERE H.HEADER_ID=D.HEADER_ID AND C.COMPANY_CODE=D.COMPANY_CODE AND O.ORGN_CODE=H.ORGN_CODE AND U.USER_ID=H.BUYER_CODE AND H.ORGN_CODE='" + cbxorgcode.Text + "' AND CONVERT(NVARCHAR(10),H.REPORT_DATE,105)='" + txtReportDate.Text + "'";

                        sql = sql + " GROUP BY D.COMPANY_CODE,H.ORGN_CODE,C.COMP_GROUP_CODE,C.COMPANY_NAME,H.ORGN_CODE,D.COMPANY_CODE,H.REPORT_NO,D.NO_OF_BALES,H.TOT_SOLD_BALES, H.NB_NS_BALES,H.TOT_OFF_BALES,H.FLOOR_HB,H.FLOOR_LB,H.GPIL_HB,H.GPIL_LB,H.CLSTR_OFF_TODAY,H.CLSTR_OFF_TOMORROW,u.USER_NAME,H.BALES_MRKTD,H.AVG_PRICE_MRKTD,H.QTY_MRKTD,convert(nvarchar(10),H.REPORT_DATE,105) ORDER BY D.COMPANY_CODE";
                        DataSet ds = new DataSet();
                        ds = lpdMgt.GetCompetitionReport(sql);


                        ReportDocument CustomerReport = new ReportDocument();

                        CustomerReport.Load(Server.MapPath("~/CrystalReport/rptCompetetionReport.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                        CrystalReportViewer1.ReportSource = CustomerReport;
                        CrystalReportViewer1.DataBind();

                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please check the input values');", true);
                }
                txtReportDate.Text = a;
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please check the input values');", true);
            //}
        }
        public bool validate()
        {
            if (txtReportDate.Text == string.Empty)
            {
                txtReportDate.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Competation Date');", true);
                return false;
            }
            else if (cbxcrop.SelectedIndex == 0)
            {
                cbxcrop.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select crop year');", true);
                return false;
            }
            else if (cbxorgcode.SelectedIndex == 0)
            {
                cbxorgcode.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Orginization Code');", true);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string variety;
        public static int repview = 0;
        string sql;
        
        public void Rptview()
        {
           
            
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompetitionReport.aspx");
        }

        protected void cbxorgcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxorgcode.SelectedIndex != 0)
                {
                    sql = "SELECT VARIETY FROM GPIL_ORGN_MASTER WHERE ORGN_CODE='" + cbxorgcode.Text + "'";
                    DataSet ds = new DataSet();
                    ds = lpdMgt.GetCompetitionReport(sql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        variety = Convert.ToString(ds.Tables[0].Rows[0]["VARIETY"]);
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}