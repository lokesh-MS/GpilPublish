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
    public partial class CompetitionHighBidLowBidInfo : System.Web.UI.Page
    {
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
                ddlCropYear.DataSource = ds1.Tables[0];
                ddlCropYear.DataTextField = "CROPYEAR";
                ddlCropYear.DataValueField = "CROP";
                ddlCropYear.DataBind();
                ddlCropYear.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                ddlVariety.DataSource = ds2.Tables[0];
                ddlVariety.DataTextField = "VARIETYNAME";
                ddlVariety.DataValueField = "VARIETY";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {

        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            if (this.valgrdrpt() == true)
            {
                viewrpt();
            }
        }

        public bool valgrdrpt()
        {
            if (ddlCropYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                ddlCropYear.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Variety');", true);
                ddlVariety.Focus();
                return false;
            }
            else if (txt_From_Date.Text == "DD-MM-YYYY")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_From_Date.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void viewrpt()
        {
            try
            {
                string sql;

                DateTime dt = Convert.ToDateTime(txt_From_Date.Text);
                txt_From_Date.Text = dt.ToString("dd-MM-yyyy");

                if (ddlType.Text.ToUpper() == "OTHERS")
                {
                    sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,CONVERT(NVARCHAR(15),H.REPORT_DATE,105) AS PURCHASE_DATE,H.ORGN_CODE,D.COMPANY_CODE,CM.COMP_SHORT_NAME,NO_OF_BALES,HIGHEST_BID,LOWEST_BID FROM GPIL_COMPETITION_DTLS D, GPIL_COMPETITION_HDR H, GPIL_COMPANY_MASTER CM WHERE H.REPORT_DATE BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_From_Date.Text + " 23:59:59',105) AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND CM.COMPANY_CODE=D.COMPANY_CODE AND H.HEADER_ID=D.HEADER_ID AND D.COMPANY_CODE IN (SELECT COMPANY_CODE FROM GPIL_COMPANY_MASTER WHERE COMP_GROUP_CODE='OTHERS') ORDER BY H.ORGN_CODE,D.COMPANY_CODE";
                }
                else
                {
                    sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,CONVERT(NVARCHAR(15),H.REPORT_DATE,105) AS PURCHASE_DATE,H.ORGN_CODE,D.COMPANY_CODE,CM.COMP_SHORT_NAME,NO_OF_BALES,HIGHEST_BID,LOWEST_BID FROM GPIL_COMPETITION_DTLS D, GPIL_COMPETITION_HDR H, GPIL_COMPANY_MASTER CM WHERE H.REPORT_DATE BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_From_Date.Text + " 23:59:59',105) AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND CM.COMPANY_CODE=D.COMPANY_CODE AND H.HEADER_ID=D.HEADER_ID ORDER BY H.ORGN_CODE,D.COMPANY_CODE";
                }


                DataSet ds = new DataSet();
                ds = lpdMgt.GetTabPurchaseSummary(sql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptCompetitionHighAndLowBids.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();




                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}