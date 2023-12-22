using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class TapPurchaseInfo : System.Web.UI.Page
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

        string a,b;
        protected void btnview_Click(object sender, EventArgs e)
        {
            a = txt_From_Date.Text;
            b = txt_To_Date.Text;
            try
            {
               
                if (this.valgrdrpt() == true)
                {
                    viewrpt();
                }
               
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
            txt_From_Date.Text = a;
            txt_To_Date.Text = b;
        }


        public bool valgrdrpt()
        {
            if (txt_From_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_From_Date.Focus();
                return false;
            }
            else if (txt_To_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txt_To_Date.Focus();
                return false;
            }
            else if (ddlCropYear.SelectedIndex == 0)
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
            else
            {
                return true;
            }
        }

        string sql;
        public void viewrpt()
        {



            try
            {
                string from_date;
                string to_date;
                DateTime dtFDate, dtTDate;
                if (string.IsNullOrEmpty(txt_From_Date.Text.Trim()))
                    return;
                if (string.IsNullOrEmpty(txt_To_Date.Text.Trim()))
                    return;

                //dtFDate = Convert.ToDateTime(txt_From_Date.Text);
                //dtTDate = Convert.ToDateTime(txt_To_Date.Text);

                DateTime dt = Convert.ToDateTime(txt_From_Date.Text);
                //txt_From_Date.Text = dt.ToString("dd-MM-yyyy");
                from_date = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txt_To_Date.Text);
                //txt_To_Date.Text = dt1.ToString("dd-MM-yyyy");
                to_date = dt1.ToString("dd-MM-yyyy");

                dtFDate = dt;// DateTime.ParseExact(txt_From_Date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtTDate = dt1;// DateTime.ParseExact(txt_To_Date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);


                //dtFDate = DateTime.ParseExact(txt_From_Date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                //dtTDate = DateTime.ParseExact(txt_To_Date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                if (dtTDate >= DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture) & dtFDate < DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture))
                {
                    lblMessage.Text = "Please Select Proper Date Order(GST/NonGST)";
                    return;
                }

                DateTime dtValue, dtDate;
                //dtDate = System.DateTime.Today;
                //dtValue = Convert.ToDateTime(txt_From_Date.Text);

                //sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE ,H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100),2) AS SERVICE_CHARGE_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000),2) AS SERVICE_TB_TAX_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000),2) AS EDS, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000),2) AS HEDS, ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE,ROUND(SUM((NET_WT*RATE) + D.PATTA_CHARGE + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000)  + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000)),2) AS INVOICE_VAL FROM GPIL_TAP_FARM_PURCHS_DTLS D ,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_SERVICE_CHARGE_INFO SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS ='N' AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.ORGN_CODE ORDER BY H.ORGN_CODE,CROP,VARIETY,FROM_DATE,TO_DATE";
                //sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE ,H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100),2) AS SERVICE_CHARGE_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000),2) AS SERVICE_TB_TAX_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000),2) AS EDS, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000),2) AS HEDS, ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE,ROUND(SUM((NET_WT*RATE) + D.PATTA_CHARGE + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000)  + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000)),2) AS INVOICE_VAL FROM GPIL_TAP_FARM_PURCHS_DTLS D ,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_SERVICE_CHARGE_CHART SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID AND H.DATE_OF_PURCH BETWEEN SC.STARTING_DATE AND ISNULL(SC.ENDING_DATE,GETDATE()) GROUP BY H.ORGN_CODE ORDER BY H.ORGN_CODE,CROP,VARIETY,FROM_DATE,TO_DATE";


                SqlCommand cmd;
                SqlDataAdapter da;
                //LP5 = new ReportDocument();
                if (dtFDate >= DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture))
                {
                    sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + from_date + "' AS FROM_DATE,'" + to_date + "' AS TO_DATE,";
                    sql = sql + "H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),2) AS TOTAL_PURC_VAL,";
                    sql = sql + "ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H";
                    sql = sql + " WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text.Trim() + "' AND";
                    sql = sql + " H.VARIETY ='" + ddlVariety.Text.Trim() + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN";
                    sql = sql + " CONVERT(DATETIME,'" + from_date + " 00:00:00',105) AND CONVERT(DATETIME,'" + to_date + " 23:59:59',105) GROUP BY H.ORGN_CODE";

                    //strsr = SqlHelper.ExecuteReader(ClsConnection.SqlCon, CommandType.Text, sql);
                    //cmd = new SqlCommand(sql, ClsConnection.SqlCon);
                    //cmd.CommandTimeout = 0;
                    //da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ds = lpdMgt.GetTabPurchaseSummary(sql);
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPMonthEndValuesGST.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    TAPMonthEndValuesGST.ReportSource = CustomerReport;
                    TAPMonthEndValuesGST.DataBind();


                    
                }
                else
                {
                    sql = "select T1.CROP,T1.VARIETY,T1.FROM_DATE,T1.TO_DATE,T1.ORGN_CODE,T2.BALES,T1.QUANTITY,T1.TOTAL_PURC_VAL,T1.SERVICE_CHARGE_VAL,";
                    sql = sql + "T1.SERVICE_TB_TAX_VAL,T1.EDS,T1.HEDS,T1.PATTA_CHARGE,T1.INVOICE_VAL FROM ";
                    sql = sql + "(SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + from_date + "' AS FROM_DATE,'" + to_date + "' AS TO_DATE ,H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100),2) AS SERVICE_CHARGE_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000),2) AS SERVICE_TB_TAX_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000),2) AS EDS, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000),2) AS HEDS, ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE,ROUND(SUM((NET_WT*RATE) + D.PATTA_CHARGE + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000)  + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000)),2) AS INVOICE_VAL FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + from_date + " 00:00:00',105) AND CONVERT(DATETIME,'" + to_date + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.ORGN_CODE)T1,";
                    sql = sql + "(SELECT ORGN_CODE,COUNT(BALES)BALES FROM (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + from_date + " 00:00:00',105) AND CONVERT(DATETIME,'" + to_date + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.ORGN_CODE,D.GPIL_BALE_NUMBER)S1 GROUP BY ORGN_CODE)T2";
                    sql = sql + " WHERE T1.ORGN_CODE=T2.ORGN_CODE ORDER BY T1.ORGN_CODE";

                    DataSet ds = new DataSet();
                    ds = lpdMgt.GetTabPurchaseSummary(sql);
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPMonthEndValues.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    TAPMonthEndValues.ReportSource = CustomerReport;
                    TAPMonthEndValues.DataBind();
                  
                }


               
                if (dtFDate >= DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture))
                {
                    sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + from_date + "' AS FROM_DATE,'" + to_date + "' AS TO_DATE,";
                    sql = sql + "H.PURCH_DOC_NO,COUNT(DISTINCT H.ORGN_CODE) AS NO_OF_TAP,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,";
                    sql = sql + "ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE";
                    sql = sql + " FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H";
                    sql = sql + " WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "'";
                    sql = sql + " AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + from_date + " 00:00:00',105) AND";
                    sql = sql + " CONVERT(DATETIME,'" + to_date + " 23:59:59',105) GROUP BY H.PURCH_DOC_NO";

                    DataSet ds = new DataSet();
                    ds = lpdMgt.GetTabPurchaseSummary(sql);
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPInvoiceValuesGST.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    TAPInvoiceValuesGST.ReportSource = CustomerReport;
                    TAPInvoiceValuesGST.DataBind();
                    
                }
                else
                {
                    sql = "SELECT T1.CROP,T1.VARIETY,T1.FROM_DATE,T1.TO_DATE,T1.PURCH_DOC_NO,T1.NO_OF_TAP,T2.BALES,T1.QUANTITY,T1.TOTAL_PURC_VAL,";
                    sql = sql + "T1.SERVICE_CHARGE_VAL,T1.SERVICE_TB_TAX_VAL,T1.EDS,T1.HEDS,T1.PATTA_CHARGE,T1.INVOICE_VAL FROM ";
                    sql = sql + "(SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + from_date + "' AS FROM_DATE,'" + to_date + "' AS TO_DATE ,H.PURCH_DOC_NO,COUNT(DISTINCT H.ORGN_CODE) AS NO_OF_TAP,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100),2) AS SERVICE_CHARGE_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000),2) AS SERVICE_TB_TAX_VAL,  ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000),2) AS EDS,  ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000),2) AS HEDS,  ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE,ROUND(SUM((NET_WT*RATE) + D.PATTA_CHARGE + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000)  + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000)),2) AS INVOICE_VAL FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + from_date + " 00:00:00',105) AND CONVERT(DATETIME,'" + to_date + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.PURCH_DOC_NO)T1,";
                    sql = sql + "(SELECT PURCH_DOC_NO,COUNT(BALES)BALES FROM (SELECT H.PURCH_DOC_NO,COUNT(D.GPIL_BALE_NUMBER) AS BALES FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC  WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + from_date + " 00:00:00',105) AND CONVERT(DATETIME,'" + to_date + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER)S1 GROUP BY PURCH_DOC_NO)T2";
                    sql = sql + " WHERE T1.PURCH_DOC_NO=T2.PURCH_DOC_NO ORDER BY T1.PURCH_DOC_NO";

                    DataSet ds = new DataSet();
                    ds = lpdMgt.GetTabPurchaseSummary(sql);
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPInvoiceValues.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    TAPInvoiceValuesReport.ReportSource = CustomerReport;
                    TAPInvoiceValuesReport.DataBind();
                    
                }


                
            }
            catch(Exception ex)
            {

            }
            //try
            //{
            //    string sql = "";
            //    DateTime dtFDate, dtTDate;
            //    if (string.IsNullOrEmpty(txt_From_Date.Text.Trim()))
            //        return;
            //    if (string.IsNullOrEmpty(txt_To_Date.Text.Trim()))
            //        return;

            //    //dtFDate = Convert.ToDateTime(txt_From_Date.Text);
            //    //dtTDate = Convert.ToDateTime(txt_To_Date.Text);

            //    DateTime dt = Convert.ToDateTime(txt_From_Date.Text);
            //    txt_From_Date.Text = dt.ToString("dd-MM-yyyy");

            //    DateTime dt1 = Convert.ToDateTime(txt_To_Date.Text);
            //    txt_To_Date.Text = dt1.ToString("dd-MM-yyyy");



            //    dtFDate = DateTime.ParseExact(txt_From_Date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //    dtTDate = DateTime.ParseExact(txt_To_Date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //    if (dtTDate >= DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture) & dtFDate < DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture))
            //    {
            //        lblMessage.Text = "Please Select Proper Date Order(GST/NonGST)";
            //        return;
            //    }              
            //    if (dtFDate >= DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture))
            //    {
            //        sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,";
            //        sql = sql + "H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),2) AS TOTAL_PURC_VAL,";
            //        sql = sql + "ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H";
            //        sql = sql + " WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text.Trim() + "' AND";
            //        sql = sql + " H.VARIETY ='" + ddlVariety.Text.Trim() + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN";
            //        sql = sql + " CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105) GROUP BY H.ORGN_CODE";
            //        DataSet ds = new DataSet();
            //        ds = lpdMgt.GetTabPurchaseSummary(sql);
            //        ReportDocument CustomerReport = new ReportDocument();
            //        CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPMonthEndValuesGST.rpt"));
            //        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
            //        CrystalReportViewer1.ReportSource = CustomerReport;
            //        CrystalReportViewer1.DataBind();
            //    }
            //    else
            //    {
            //        sql = "select T1.CROP,T1.VARIETY,T1.FROM_DATE,T1.TO_DATE,T1.ORGN_CODE,T2.BALES,T1.QUANTITY,T1.TOTAL_PURC_VAL,T1.SERVICE_CHARGE_VAL,";
            //        sql = sql + "T1.SERVICE_TB_TAX_VAL,T1.EDS,T1.HEDS,T1.PATTA_CHARGE,T1.INVOICE_VAL FROM ";
            //        sql = sql + "(SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE ,H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100),2) AS SERVICE_CHARGE_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000),2) AS SERVICE_TB_TAX_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000),2) AS EDS, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000),2) AS HEDS, ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE,ROUND(SUM((NET_WT*RATE) + D.PATTA_CHARGE + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000)  + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000)),2) AS INVOICE_VAL FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.ORGN_CODE)T1,";
            //        sql = sql + "(SELECT ORGN_CODE,COUNT(BALES)BALES FROM (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS BALES FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.ORGN_CODE,D.GPIL_BALE_NUMBER)S1 GROUP BY ORGN_CODE)T2";
            //        sql = sql + " WHERE T1.ORGN_CODE=T2.ORGN_CODE ORDER BY T1.ORGN_CODE";
            //        DataSet ds = new DataSet();
            //        ds = lpdMgt.GetTabPurchaseSummary(sql);
            //        ReportDocument CustomerReport = new ReportDocument();
            //        CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPMonthEndValues.rpt"));
            //        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
            //        CrystalReportViewer1.ReportSource = CustomerReport;
            //        CrystalReportViewer1.DataBind();

            //    }
            //    if (dtFDate >= DateTime.ParseExact("01-07-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture))
            //    {
            //        sql = "SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,";
            //        sql = sql + "H.PURCH_DOC_NO,COUNT(DISTINCT H.ORGN_CODE) AS NO_OF_TAP,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,";
            //        sql = sql + "ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE";
            //        sql = sql + " FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H";
            //        sql = sql + " WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "'";
            //        sql = sql + " AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND";
            //        sql = sql + " CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105) GROUP BY H.PURCH_DOC_NO";

            //        DataSet ds = new DataSet();
            //        ds = lpdMgt.GetTabPurchaseSummary(sql);
            //        ReportDocument CustomerReport = new ReportDocument();
            //        CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPInvoiceValuesGST.rpt"));
            //        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
            //        CrystalReportViewer2.ReportSource = CustomerReport;
            //        CrystalReportViewer2.DataBind();
            //    }
            //    else
            //    {
            //        sql = "SELECT T1.CROP,T1.VARIETY,T1.FROM_DATE,T1.TO_DATE,T1.PURCH_DOC_NO,T1.NO_OF_TAP,T2.BALES,T1.QUANTITY,T1.TOTAL_PURC_VAL,";
            //        sql = sql + "T1.SERVICE_CHARGE_VAL,T1.SERVICE_TB_TAX_VAL,T1.EDS,T1.HEDS,T1.PATTA_CHARGE,T1.INVOICE_VAL FROM ";
            //        sql = sql + "(SELECT '" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE ,H.PURCH_DOC_NO,COUNT(DISTINCT H.ORGN_CODE) AS NO_OF_TAP,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY,ROUND(SUM(NET_WT*RATE),1) AS TOTAL_PURC_VAL,ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100),2) AS SERVICE_CHARGE_VAL, ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000),2) AS SERVICE_TB_TAX_VAL,  ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000),2) AS EDS,  ROUND(SUM((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000),2) AS HEDS,  ROUND(SUM(D.PATTA_CHARGE),2) AS PATTA_CHARGE,ROUND(SUM((NET_WT*RATE) + D.PATTA_CHARGE + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE)/100) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE)/10000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_SBC_RATE)/10000)  + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_SH_EDUCATION_CESS_RATE)/1000000) + ((NET_WT*RATE*SC.SERVICE_CHARGE_RATE*SC.SERVICE_TAX_RATE*SC.SERVICE_TAX_EDUCATION_CESS_RATE)/1000000)),2) AS INVOICE_VAL FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.PURCH_DOC_NO)T1,";
            //        sql = sql + "(SELECT PURCH_DOC_NO,COUNT(BALES)BALES FROM (SELECT H.PURCH_DOC_NO,COUNT(D.GPIL_BALE_NUMBER) AS BALES FROM GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D ,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_SERVICE_CHARGE_CHART(NOLOCK) SC  WHERE D.HEADER_ID=H.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE' AND H.CROP='" + ddlCropYear.Text + "' AND H.VARIETY ='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND H.STATUS IN ('N','P') AND H.DATE_OF_PURCH BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',105)  AND D.SERVICE_CHARGE=SC.SERVICE_CHARGE_ID AND D.SERVICE_TAX=SC.SERVICE_TAX_ID AND D.ED_CESS_TAX=SC.SERVICE_SH_EDUCATION_CESS_ID AND D.SH_ED_TAX=SC.SERVICE_TAX_EDUCATION_CESS_ID GROUP BY H.PURCH_DOC_NO,D.GPIL_BALE_NUMBER)S1 GROUP BY PURCH_DOC_NO)T2";
            //        sql = sql + " WHERE T1.PURCH_DOC_NO=T2.PURCH_DOC_NO ORDER BY T1.PURCH_DOC_NO";

            //        DataSet ds = new DataSet();
            //        ds = lpdMgt.GetTabPurchaseSummary(sql);
            //        ReportDocument CustomerReport = new ReportDocument();
            //        CustomerReport.Load(Server.MapPath("~/CrystalReport/RptTAPInvoiceValues.rpt"));
            //        CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
            //        CrystalReportViewer2.ReportSource = CustomerReport;
            //        CrystalReportViewer2.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("TapPurchaseInfo.aspx");

        }
    }
}