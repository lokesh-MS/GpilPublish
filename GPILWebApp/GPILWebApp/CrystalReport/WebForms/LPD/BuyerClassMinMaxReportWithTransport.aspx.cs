using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPILWebApp.ViewModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace GPILWebApp
{
    public partial class BuyerClassMinMaxReportWithTransport : System.Web.UI.Page
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
                else
                {
                    viewrpt();
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
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                cbxorgcd.DataSource = ds.Tables[0];
                cbxorgcd.DataTextField = "ORGN_CODE1";
                cbxorgcd.DataValueField = "ORGN_CODE";
                cbxorgcd.DataBind();
                cbxorgcd.Items.Insert(0, new ListItem("- Select -", "0"));
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
            if (validated())
            {
                viewrpt();
            }

        }

        public bool validated()
        {
            if (txt_Report_Date.Text == "DD-MM-YYYY")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_Report_Date.Focus();
                return false;
            }
            else if (txttodate.Text == "DD-MM-YYYY")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txttodate.Focus();
                return false;
            }
            else if (cbxcrop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                cbxcrop.Focus();
                return false;
            }
            else if (cbxorgcd.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                cbxcrop.Focus();
                return false;
            }
            else if (cbxvariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Variety');", true);
                cbxvariety.Focus();
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


                string strsql;
                string ColumName = "";
                string GradeColum = "";

                DateTime dt = Convert.ToDateTime(txt_Report_Date.Text);
                txt_Report_Date.Text = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txttodate.Text);
                txttodate.Text = dt1.ToString("dd-MM-yyyy");

                if (cbxvariety.SelectedItem.ToString() == "VLB" || cbxvariety.SelectedItem.ToString() == "HDBRG" || cbxvariety.SelectedItem.ToString() == "NATU")
                {
                    ColumName = "isnull( D.ATTRIBUTE4,0)";
                    GradeColum = "D.ATTRIBUTE2";
                }
                else
                {
                    ColumName = "D.RATE";
                    GradeColum = "D.BUYER_GRADE";
                }


                string s = txt_Report_Date.Text + " 00:00:00";
                string s1 = txttodate.Text + " 23:59:59";

                if (cbxorgcd.SelectedIndex == 0)
                {
                    strsql = "SELECT SUBSTRING(" + GradeColum + ",5,len(" + GradeColum + ")) as BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.ATTRIBUTE4 * D.NET_WT)/SUM(D.NET_WT),2) as avgpr,MAX(" + ColumName + ") AS MINRATE,MIN(" + ColumName + ") AS MAXRATE,H.ORGN_CODE+'-'+O.ORGN_NAME as ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,('" + s + "'),105) and CONVERT(datetime,('" + s1 + "'),105) group by SUBSTRING(" + GradeColum + ",5,len(" + GradeColum + ")),H.ORGN_CODE+'-'+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME ";
                }
                else
                {
                    strsql = "SELECT SUBSTRING(" + GradeColum + ",5,len(" + GradeColum + ")) as BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.ATTRIBUTE4 * D.NET_WT)/SUM(D.NET_WT),2) as avgpr,MAX(" + ColumName + ") AS MINRATE,MIN(" + ColumName + ") AS MAXRATE,H.ORGN_CODE+'-'+O.ORGN_NAME as ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "'  and H.ORGN_CODE='" + cbxorgcd.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,('" + s + "'),105) and CONVERT(datetime,('" + s1 + "'),105) group by SUBSTRING(" + GradeColum + ",5,len(" + GradeColum + ")),H.ORGN_CODE+'-'+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME";

                }
                DataSet ds = new DataSet();
                ds = lpdMgt.GetTabPurchaseSummary(strsql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptBuyerClassmINmAXReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                BCMinMaxWithTransChrRpt.ReportSource = CustomerReport;
                BCMinMaxWithTransChrRpt.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuyerClassMinMaxReportWithTransport.aspx");
        }
    }
}