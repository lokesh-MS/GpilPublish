using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class TapWiseQuantityMarketed : System.Web.UI.Page
    {
        string strsql;
        SqlCommand cmd;
        SqlDataAdapter sqldta;
        SqlDataReader strsr;
        //testDataContext test;
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

            viewrpt();

        }


        public bool validated()
        {
            if (txt_Report_Date.Text == "DD-MM-YYYY")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_Report_Date.Focus();
                return false;
            }
            else if (cbxcrop.SelectedIndex == 0)
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

        string a;
        public void viewrpt()
        {

            a = txt_Report_Date.Text;
            try
            {
                if (validated())
                {
                    DateTime dt = Convert.ToDateTime(txt_Report_Date.Text);
                    txt_Report_Date.Text = dt.ToString("dd-MM-yyyy");
                    a = txt_Report_Date.Text;
                    if (cbxorgcd.SelectedIndex == 0)
                    {


                        strsql = "select H.ORGN_CODE,'" + txt_Report_Date.Text + "' as RPTDATE,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGNNAME,H.CROP,H.CROP+' - '+C.CROP_YEAR AS CROPYR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME AS VTYNAME,CASE WHEN sum(BALES_MRKTD)  IS NULL THEN '0' ELSE SUM(BALES_MRKTD) END AS TODATEBDLSMKT,CASE WHEN sum(QTY_MRKTD) IS NULL THEN '0' ELSE SUM(QTY_MRKTD) END AS TODATEQTYMKT,CASE WHEN  ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) IS NULL THEN '0' ELSE ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) END AS TODATEAVGPRMRKTD, ";
                        strsql = strsql + " CASE WHEN sum(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) IS NULL THEN '0' ELSE SUM(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) END AS TODATEVALUEMKT,";
                        strsql = strsql + " CASE WHEN  (SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) IS NULL THEN '0' ELSE(SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) END  AS TODAYBDLSMKTD,";
                        strsql = strsql + " CASE WHEN (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END AS TODAYQTYMRKTD,  ";
                        strsql = strsql + " CASE WHEN (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYAVGPRMRKTD , ";
                        strsql = strsql + " CASE WHEN (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYVALUEMRKTD";
                        strsql = strsql + " from GPIL_COMPETITION_HDR H,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V WHERE REPORT_DATE < CONVERT(DATETIME,'" + txt_Report_Date.Text + "',105)+1 AND H.ORGN_CODE=O.ORGN_CODE AND C.CROP=H.CROP AND V.VARIETY=H.VARIETY  AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' GROUP BY H.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.CROP+' - '+C.CROP_YEAR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME ORDER BY H.ORGN_CODE";




                        //strsql = "select H.ORGN_CODE,'" + txt_Report_Date.Text + "' as RPTDATE,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGNNAME,H.CROP,H.CROP+' - '+C.CROP_YEAR AS CROPYR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME AS VTYNAME,CASE WHEN sum(BALES_MRKTD)  IS NULL THEN '0' ELSE SUM(BALES_MRKTD) END AS TODATEBDLSMKT,CASE WHEN sum(QTY_MRKTD) IS NULL THEN '0' ELSE SUM(QTY_MRKTD) END AS TODATEQTYMKT,CASE WHEN  ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) IS NULL THEN '0' ELSE ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) END AS TODATEAVGPRMRKTD, ";
                        //strsql = strsql + " CASE WHEN sum(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) IS NULL THEN '0' ELSE SUM(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) END AS TODATEVALUEMKT,";
                        //strsql = strsql + " CASE WHEN  (SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) IS NULL THEN '0' ELSE(SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) END  AS TODAYBDLSMKTD,";
                        //strsql = strsql + " CASE WHEN (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END AS TODAYQTYMRKTD,  ";
                        //strsql = strsql + " CASE WHEN (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYAVGPRMRKTD , ";
                        //strsql = strsql + " CASE WHEN (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYVALUEMRKTD";
                        //strsql = strsql + " from GPIL_COMPETITION_HDR H,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V WHERE REPORT_DATE < CONVERT(DATETIME,'" + txt_Report_Date.Text + "',105)+1 AND H.ORGN_CODE=O.ORGN_CODE AND C.CROP=H.CROP AND V.VARIETY=H.VARIETY  AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' GROUP BY H.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.CROP+' - '+C.CROP_YEAR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME ORDER BY H.ORGN_CODE";
                        DataSet ds = new DataSet();
                        ds = lpdMgt.GetTabQuantityMarketed(strsql);


                        ReportDocument CustomerReport = new ReportDocument();

                        CustomerReport.Load(Server.MapPath("~/CrystalReport/rptQunatityMarktedSummary.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                        CrystalReportViewer1.ReportSource = CustomerReport;
                        CrystalReportViewer1.DataBind();
                    }
                    else
                    {

                        /*strsql = "select H.ORGN_CODE,'" + txt_Report_Date.Text + "' as RPTDATE,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGNNAME,H.CROP,H.CROP+' - '+C.CROP_YEAR AS CROPYR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME AS VTYNAME,CASE WHEN sum(BALES_MRKTD)  IS NULL THEN '0' ELSE SUM(BALES_MRKTD) END AS TODATEBDLSMKT,CASE WHEN sum(QTY_MRKTD) IS NULL THEN '0' ELSE SUM(QTY_MRKTD) END AS TODATEQTYMKT,CASE WHEN  ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/sum(QTY_MRKTD)),2) IS NULL THEN '0' ELSE ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/sum(QTY_MRKTD)),2) END AS TODATEAVGPRMRKTD, ";
                    strsql = strsql + " CASE WHEN sum(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/sum(QTY_MRKTD)),2) IS NULL THEN '0' ELSE SUM(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/sum(QTY_MRKTD)),2) END AS TODATEVALUEMKT,";
                    strsql = strsql + " CASE WHEN  (SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) IS NULL THEN '0' ELSE(SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) END  AS TODAYBDLSMKTD,";
                    strsql = strsql + " CASE WHEN (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END AS TODAYQTYMRKTD,  ";
                    strsql = strsql + " CASE WHEN (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYAVGPRMRKTD , ";
                    strsql = strsql + " CASE WHEN (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYVALUEMRKTD";
                    strsql = strsql + " from GPIL_COMPETITION_HDR H,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V WHERE REPORT_DATE < CONVERT(DATETIME,'" + txt_Report_Date.Text + "',105)+1 AND H.ORGN_CODE=O.ORGN_CODE AND C.CROP=H.CROP AND V.VARIETY=H.VARIETY  AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "'  and H.ORGN_CODE='" + cbxorgcd.Text + "' GROUP BY H.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.CROP+' - '+C.CROP_YEAR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME ";*/
                        strsql = "select H.ORGN_CODE,'" + txt_Report_Date.Text + "' as RPTDATE,H.ORGN_CODE+' - '+O.ORGN_NAME AS ORGNNAME,H.CROP,H.CROP+' - '+C.CROP_YEAR AS CROPYR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME AS VTYNAME,CASE WHEN sum(BALES_MRKTD)  IS NULL THEN '0' ELSE SUM(BALES_MRKTD) END AS TODATEBDLSMKT,CASE WHEN sum(QTY_MRKTD) IS NULL THEN '0' ELSE SUM(QTY_MRKTD) END AS TODATEQTYMKT,CASE WHEN  ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) IS NULL THEN '0' ELSE ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) END AS TODATEAVGPRMRKTD, ";
                        strsql = strsql + " CASE WHEN sum(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) IS NULL THEN '0' ELSE SUM(QTY_MRKTD)*ROUND((sum(QTY_MRKTD* AVG_PRICE_MRKTD)/NULLIF(sum(QTY_MRKTD),0)),2) END AS TODATEVALUEMKT,";
                        strsql = strsql + " CASE WHEN  (SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) IS NULL THEN '0' ELSE(SELECT H1.BALES_MRKTD  FROM GPIL_COMPETITION_HDR H1 WHERE CONVERT(NVARCHAR(10),H1.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H1.ORGN_CODE=H.ORGN_CODE) END  AS TODAYBDLSMKTD,";
                        strsql = strsql + " CASE WHEN (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT H2.QTY_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END AS TODAYQTYMRKTD,  ";
                        strsql = strsql + " CASE WHEN (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  H2.AVG_PRICE_MRKTD FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYAVGPRMRKTD , ";
                        strsql = strsql + " CASE WHEN (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE)  IS NULL THEN '0' ELSE (SELECT  ROUND(H2.QTY_MRKTD*H2.AVG_PRICE_MRKTD,2) FROM GPIL_COMPETITION_HDR H2 WHERE CONVERT(NVARCHAR(10),H2.REPORT_DATE,105)='" + txt_Report_Date.Text + "' AND H2.ORGN_CODE=H.ORGN_CODE) END  AS TODAYVALUEMRKTD";
                        strsql = strsql + " from GPIL_COMPETITION_HDR H,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V WHERE REPORT_DATE < CONVERT(DATETIME,'" + txt_Report_Date.Text + "',105)+1 AND H.ORGN_CODE=O.ORGN_CODE AND C.CROP=H.CROP AND V.VARIETY=H.VARIETY  AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "'  and H.ORGN_CODE='" + cbxorgcd.Text + "' GROUP BY H.ORGN_CODE,H.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.CROP+' - '+C.CROP_YEAR,H.VARIETY,H.VARIETY+' - '+V.VARIETY_NAME ORDER BY H.ORGN_CODE";
                        DataSet ds = new DataSet();
                        ds = lpdMgt.GetTabQuantityMarketed(strsql);


                        ReportDocument CustomerReport = new ReportDocument();

                        CustomerReport.Load(Server.MapPath("~/CrystalReport/rptQunatityMarktedSummary.rpt"));
                        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                        CrystalReportViewer1.ReportSource = CustomerReport;
                        CrystalReportViewer1.DataBind();

                    }
                  
                    txt_Report_Date.Text = a;
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("TapWiseQuantityMarketed.aspx");
        }
    }
}