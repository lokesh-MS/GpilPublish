using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{ 
    public partial class TapWisePurchaseReport : System.Web.UI.Page
    {

        string strsql;
        SqlCommand cmd;
        SqlDataAdapter sqldta;
        SqlDataReader strsr;        
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        string a;
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //viewrpt();


                if (!IsPostBack)
                {
                    bindDropDown();
                   
                }
                else
                {
                    if (validated())
                    {
                        viewrpt();
                    }
                    txt_Report_Date.Text = a;
                }
               
            }
            catch(Exception ex)
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
                //cbxorgcd.Items.Insert(0, "SELECT ORGN CODE");

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
                //cbxvariety.Items.Insert(0, "SELECT Variety Name");

            }
            catch (Exception ex)
            {

            }
        }

       
        protected void btnview_Click(object sender, EventArgs e)
        {
            //a = txt_Report_Date.Text;
            if (validated())
            {
                viewrpt();
            }
            //txt_Report_Date.Text = a;
        }



        public bool validated()
        {
            if (txt_Report_Date.Text == "DD-MM-YYYY" || txt_Report_Date.Text=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_Report_Date.Focus();
                return false;
            }
            else if (cbxcrop.SelectedIndex==0)
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
            a = txt_Report_Date.Text;
            try
            {
                
                DateTime dt = Convert.ToDateTime(txt_Report_Date.Text);
                txt_Report_Date.Text = dt.ToString("dd-MM-yyyy");
               
                if (cbxorgcd.SelectedIndex == 0)
                {

                    strsql = "select O.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, COUNT(*) AS TODATEBDLS,case when SUM(D.NET_WT) IS null then '0' else SUM(D.NET_WT) end  AS TODATEQUANTITY,case when  SUM(D.VALUE) IS NULL THEN '0' ELSE SUM(D.VALUE) END AS TODATEVALUE ,CASE WHEN  ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) IS NULL THEN '0' ELSE ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) END AS TODATEAVGPRISE, H.CROP+' - '+ C.CROP_YEAR AS CROP_YEAR, H.VARIETY+' - '+V.VARIETY_NAME AS VARIETY,'" + txt_Report_Date.Text + "' AS REPORTDATE, ";
                    strsql = strsql + " (select COUNT(*) from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D1,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H1 WHERE D1.HEADER_ID=H1.HEADER_ID AND D1.STATUS='Y' AND  H1.CROP='" + cbxcrop.Text + "' AND H1.VARIETY='" + cbxvariety.Text + "' AND convert (Date,H1.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105) AND H1.ORGN_CODE=O.ORGN_CODE)AS TODAYBDLS ,(select case when SUM(D.NET_WT) IS NULL THEN '0' ELSE SUM(D.NET_WT) END from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND Convert(Date,H.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105)  AND H.ORGN_CODE=O.ORGN_CODE) AS TODAYQUANTITY,";
                    strsql = strsql + " (select case when SUM(D.VALUE) IS NULL THEN '0' ELSE SUM(D.VALUE)  END from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND convert(Date,H.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105)  AND H.ORGN_CODE=O.ORGN_CODE) AS TODAYVALUE,(select case when ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) IS NULL THEN '0' ELSE ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) END from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND convert(Date,H.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105) AND H.ORGN_CODE=O.ORGN_CODE) AS TODAYAVGPRISE ";
                    strsql = strsql + " from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_CROP_MASTER(NOLOCK) C,GPIL_VARIETY_MASTER(NOLOCK) V WHERE D.NET_WT<>0 and D.HEADER_ID=H.HEADER_ID  AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND convert(DateTime,H.DATE_OF_PURCH,105) < CONVERT(DateTime,'" + txt_Report_Date.Text + "',105)+1 AND V.VARIETY=H.VARIETY AND C.CROP=H.CROP AND H.ORGN_CODE=O.ORGN_CODE ";
                    strsql = strsql + " GROUP BY H.ORGN_CODE+' - '+O.ORGN_NAME,O.ORGN_CODE,H.CROP+' - '+ C.CROP_YEAR, H.VARIETY+' - '+V.VARIETY_NAME ORDER BY H.ORGN_CODE+' - '+O.ORGN_NAME,O.ORGN_CODE,H.CROP+' - '+ C.CROP_YEAR, H.VARIETY+' - '+V.VARIETY_NAME";
                }
                else
                {
                    strsql = "select O.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, COUNT(*) AS TODATEBDLS,case when SUM(D.NET_WT) IS null then '0' else SUM(D.NET_WT) end  AS TODATEQUANTITY,case when  SUM(D.VALUE) IS NULL THEN '0' ELSE SUM(D.VALUE) END AS TODATEVALUE ,CASE WHEN  ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) IS NULL THEN '0' ELSE ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) END AS TODATEAVGPRISE, H.CROP+' - '+ C.CROP_YEAR AS CROP_YEAR, H.VARIETY+' - '+V.VARIETY_NAME AS VARIETY,'" + txt_Report_Date.Text + "' AS REPORTDATE, ";
                    strsql = strsql + " (select COUNT(*) from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D1,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H1 WHERE D1.HEADER_ID=H1.HEADER_ID AND D1.STATUS='Y' AND  H1.CROP='" + cbxcrop.Text + "' AND H1.VARIETY='" + cbxvariety.Text + "' AND convert(Date,H1.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105) AND H1.ORGN_CODE=O.ORGN_CODE)AS TODAYBDLS ,(select case when SUM(D.NET_WT) IS NULL THEN '0' ELSE SUM(D.NET_WT) END from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND convert(Date,H.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105)  AND H.ORGN_CODE=O.ORGN_CODE) AS TODAYQUANTITY,";
                    strsql = strsql + " (select case when SUM(D.VALUE) IS NULL THEN '0' ELSE SUM(D.VALUE)  END from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND Convert(Date,H.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105)  AND H.ORGN_CODE=O.ORGN_CODE) AS TODAYVALUE,(select case when ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) IS NULL THEN '0' ELSE ROUND((SUM(D.VALUE)/SUM(D.NET_WT)),2) END from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H WHERE D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND convert(Date,H.DATE_OF_PURCH,105) = CONVERT(DATE,'" + txt_Report_Date.Text + "',105) AND H.ORGN_CODE=O.ORGN_CODE) AS TODAYAVGPRISE ";
                    strsql = strsql + " from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O,GPIL_CROP_MASTER(NOLOCK) C,GPIL_VARIETY_MASTER(NOLOCK) V WHERE D.NET_WT<>0 and D.HEADER_ID=H.HEADER_ID AND D.STATUS='Y' AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND Convert(DateTime,H.DATE_OF_PURCH,105) < CONVERT(DateTime,'" + txt_Report_Date.Text + "',105)+1 AND V.VARIETY=H.VARIETY AND C.CROP=H.CROP AND H.ORGN_CODE=O.ORGN_CODE and H.ORGN_CODE='" + cbxorgcd.Text + "'";
                    strsql = strsql + " GROUP BY H.ORGN_CODE+' - '+O.ORGN_NAME,O.ORGN_CODE,H.CROP+' - '+ C.CROP_YEAR, H.VARIETY+' - '+V.VARIETY_NAME ORDER BY H.ORGN_CODE+' - '+O.ORGN_NAME,O.ORGN_CODE,H.CROP+' - '+ C.CROP_YEAR, H.VARIETY+' - '+V.VARIETY_NAME";

                }

                DataSet ds = new DataSet();
                ds=lpdMgt.GetTabPurchaseSummary(strsql);               

                ReportDocument CustomerReport = new ReportDocument();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptGPIPurchaseSummary.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                    TAP_Wise_Purchase_Report.ReportSource = CustomerReport;
                    TAP_Wise_Purchase_Report.DataBind();
                }
                else
                {
                    TAP_Wise_Purchase_Report.ReportSource = null;
                    TAP_Wise_Purchase_Report.DataBind();

                }
                //txt_Report_Date.Text=a;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            txt_Report_Date.Text = a;
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("TapWisePurchaseReport.aspx");
        }

      
    }
}