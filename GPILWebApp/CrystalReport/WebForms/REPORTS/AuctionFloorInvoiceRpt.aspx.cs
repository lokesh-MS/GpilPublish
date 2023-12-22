using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class AuctionFloorInvoiceRpt : System.Web.UI.Page
    {
        string sql;
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        GltManagement GLTMgt = new GltManagement();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("ORGN", "", "");
                ddlOrgnCode.DataSource = ds2.Tables[0];
                ddlOrgnCode.DataTextField = "ORGN_CODE1";
                ddlOrgnCode.DataValueField = "ORGN_CODE";
                ddlOrgnCode.DataBind();
                ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");
                //viewoprpt();
            }
            else
            {
                if (ddlOrgnCode.SelectedIndex != 0)
                {
                    viewoprpt();
                }

            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

            txtFromDate.Text="";
            txtToDate.Text="";
            ddlOrgnCode.Text= "SELECT ORGN CODE"; 

           // viewoprpt();

        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            viewoprpt();
        }

        public void viewoprpt()
        {
            try
            {
                // rdlcAuctionInvoiceReport.Visible = false;

                string frmdate;
                string todate;
                if (string.IsNullOrEmpty(txtFromDate.Text.Trim()) || string.IsNullOrEmpty(txtToDate.Text.Trim()))
                    return;
                DateTime dt1 = Convert.ToDateTime(txtFromDate.Text);
                frmdate = dt1.ToString("dd-MM-yyyy");
                DateTime dt2 = Convert.ToDateTime(txtToDate.Text);
                todate = dt2.ToString("dd-MM-yyyy");


                string SQL = "SELECT ATTRIBUTE1 from GPIL_ORGN_MASTER WHERE ORGN_CODE=SUBSTRING('" + ddlOrgnCode.Text.Trim() + "',1,3) ";
                dt = GLTMgt.GetQueryResult(SQL);
                string pcharge = string.Empty;
                pcharge = dt.Rows[0]["ATTRIBUTE1"].ToString();

                //sql = "SELECT O.ORGN_CODE,O.ORGN_NAME,H.INVOICE_NO,CONVERT(char(10),H.INVOICE_DATE,105)INVOICE_DATE,D.TB_LOT_NO,D.GPIL_BALE_NUMBER,";
                //sql = sql + " D.TB_GRADE,D.BUYER_GRADE,D.NET_WT,D.RATE";
                //sql = sql + " FROM GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_ORGN_MASTER(NOLOCK) O";
                //sql = sql + " WHERE H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE=O.ORGN_CODE AND O.ATTRIBUTE1='" + pcharge + "'";
                //sql = sql + " AND CONVERT(CHAR(10),DATE_OF_PURCH,105)>='" + txtFromDate.Text.Trim() + "'";
                //sql = sql + " AND CONVERT(CHAR(10),DATE_OF_PURCH,105)<='" + txtFromDate.Text.Trim() + "'";
                sql = "SELECT O.ORGN_CODE,O.ORGN_NAME,H.INVOICE_NO,CONVERT(char(10),H.INVOICE_DATE,105)INVOICE_DATE,D.TB_LOT_NO,D.GPIL_BALE_NUMBER,";
                sql = sql + " D.TB_GRADE,D.BUYER_GRADE,D.NET_WT,D.RATE";
                sql = sql + " FROM GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_ORGN_MASTER(NOLOCK) O";
                sql = sql + " WHERE H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE=O.ORGN_CODE AND O.ATTRIBUTE1='" + pcharge + "'";
                sql = sql + " AND CONVERT(CHAR(10),DATE_OF_PURCH,105)>='" + frmdate + "'";
                sql = sql + " AND CONVERT(CHAR(10),DATE_OF_PURCH,105)<='" + frmdate + "'";
                if (ddlOrgnCode.SelectedIndex > 0)
                    sql = sql + " AND O.ORGN_CODE='" + ddlOrgnCode.Text.Trim() + "'";

                sql = sql + " GROUP BY O.ORGN_CODE,O.ORGN_NAME,H.INVOICE_NO,H.INVOICE_DATE,D.TB_LOT_NO,D.GPIL_BALE_NUMBER,D.TB_GRADE,D.BUYER_GRADE,D.NET_WT,D.RATE";
                sql = sql + " ORDER BY O.ORGN_CODE,H.INVOICE_NO";

                //DataTable objDT0 = new DataTable();
                bool bolV1 = false;
                //objDT0 = GLTMgt.GetQueryResult(sql);

                if (bolV1 == true)
                {
                    // lblMessage.Text = strErr;
                }

                GLTManagement gMgt = new GLTManagement();

                dt = gMgt.GetQueryResult(sql);

                //if (dt.Rows.Count > 0)
                //{
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptAuctionInvoice.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);
                    rdlcAuctionInvoiceReport.Visible = true;
                    rdlcAuctionInvoiceReport.ReportSource = CustomerReport;
                    rdlcAuctionInvoiceReport.DataBind();
                //}
            }
            catch (Exception ex)
            { }
        }
    }
}