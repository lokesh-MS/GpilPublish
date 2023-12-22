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
    public partial class PPGRDReport : System.Web.UI.Page
    {

        string sql;
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDropDown();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        private void bindDropDown()
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
        protected void btnview_Click(object sender, EventArgs e)
        {
            rptView();
        }


        public void rptView()
        {
            try
            {

                DateTime dt = Convert.ToDateTime(txt_From_Date.Text);
                txt_From_Date.Text = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txt_To_Date.Text);
                txt_To_Date.Text = dt1.ToString("dd-MM-yyyy");


                sql = "SELECT '" + txt_From_Date.Text + "' AS FROMDATE,'" + txt_To_Date.Text + "' AS TODATE,H.ORGN_CODE+'-'+O.ORGN_NAME AS ORGNAME, P.PPG, SUM(D.NET_WT) AS NET_WT,ROUND(SUM(D.NET_WT*D.RATE)/SUM(D.NET_WT),2) AS RATE,H.CROP+'-'+C.CROP_YEAR AS CROP, H.VARIETY+'-'+V.VARIETY_NAME AS VARIETY";
                sql = sql + " FROM GPIL_TAP_FARM_PURCHS_DTLS D, GPIL_TAP_FARM_PURCHS_HDR H, GPIL_PPGRD_MASTER P , GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V WHERE P.PPGRD=SUBSTRING(D.TB_GRADE,3,LEN(D.TB_GRADE)) AND O.ORGN_CODE=H.ORGN_CODE AND C.CROP=H.CROP AND V.VARIETY=H.VARIETY";
                sql = sql + " and H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND D.STATUS='Y' AND D.HEADER_ID=H.HEADER_ID AND H.DATE_OF_PURCH BETWEEN	CONVERT(DATETIME,'" + txt_From_Date.Text + "',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + "',105) GROUP BY H.ORGN_CODE+'-'+O.ORGN_NAME,P.PPG,H.CROP+'-'+C.CROP_YEAR,H.VARIETY+'-'+V.VARIETY_NAME ORDER BY P.PPG DESC";

                DataSet ds = new DataSet();
                ds = lpdMgt.GetTabPurchaseSummary(sql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptPPGRDreport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();


                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("PPGRDReport.aspx");
        }
    }
    }
