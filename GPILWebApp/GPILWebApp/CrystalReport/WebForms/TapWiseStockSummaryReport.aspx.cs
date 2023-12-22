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
    public partial class TapWiseStockSummaryReport : System.Web.UI.Page
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
            if (cbxcrop.SelectedIndex == 0)
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
                if (cbxorgcd.SelectedIndex == 0)
                {
                    strsql = "SELECT TBL1.ORGN_CODE AS ORIGN_ORGN_CODE,CM.CROP +' - '+ CM.CROP_YEAR AS CROP,VM.VARIETY +' - '+ VM.VARIETY_NAME AS VARIETY,TBL1.PURCHBLS,TBL1.PURCHQTY,TBL1.PURCHAVGPR,TBL1.PURCHVALUE,TBL2.DISPATCHWT,TBL2.DISPATCHBALES,TBL2.DISAVGPRC,TBL2.DISVALUE FROM (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS PURCHBLS, ISNULL(ROUND(SUM(D.NET_WT),2),0) AS PURCHQTY,ISNULL(ROUND(SUM(D.NET_WT*D.RATE)/SUM(NET_WT),2),0)  AS PURCHAVGPR ,ISNULL(ROUND(SUM(D.NET_WT*D.RATE),2),0) AS PURCHVALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE D.STATUS='Y' AND H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP') AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' 	GROUP BY H.ORGN_CODE ) TBL1,(SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS DISPATCHBALES, ISNULL(ROUND(SUM(D.NET_WT),2),0) AS DISPATCHWT,ISNULL(ROUND(SUM(D.NET_WT*D.RATE)/SUM(NET_WT),2),0)  AS DISAVGPRC ,ISNULL(ROUND(SUM(D.NET_WT*D.RATE),2),0) AS DISVALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_SHIPMENT_DTLS SHD ,GPIL_SHIPMENT_HDR SHH WHERE D.STATUS='Y' AND SHD.SHIPMENT_NO=SHH.SHIPMENT_NO AND SHH.SENDER_ORGN_CODE=H.ORGN_CODE AND SHD.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP') AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' GROUP BY H.ORGN_CODE) TBL2,GPIL_VARIETY_MASTER VM,GPIL_CROP_MASTER CM WHERE VM.VARIETY='" + cbxvariety.Text + "' AND CM.CROP='" + cbxcrop.Text + "' AND TBL2.ORGN_CODE=TBL1.ORGN_CODE ORDER BY TBL1.ORGN_CODE";

                }
                else
                {
                    strsql = "SELECT TBL1.ORGN_CODE AS ORIGN_ORGN_CODE,CM.CROP +' - '+ CM.CROP_YEAR AS CROP,VM.VARIETY +' - '+ VM.VARIETY_NAME AS VARIETY,TBL1.PURCHBLS,TBL1.PURCHQTY,TBL1.PURCHAVGPR,TBL1.PURCHVALUE,TBL2.DISPATCHWT,TBL2.DISPATCHBALES,TBL2.DISAVGPRC,TBL2.DISVALUE FROM (SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS PURCHBLS, ISNULL(ROUND(SUM(D.NET_WT),2),0) AS PURCHQTY,ISNULL(ROUND(SUM(D.NET_WT*D.RATE)/SUM(NET_WT),2),0)  AS PURCHAVGPR ,ISNULL(ROUND(SUM(D.NET_WT*D.RATE),2),0) AS PURCHVALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H WHERE D.STATUS='Y' AND H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP') AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' 	GROUP BY H.ORGN_CODE ) TBL1,(SELECT H.ORGN_CODE,COUNT(D.GPIL_BALE_NUMBER) AS DISPATCHBALES, ISNULL(ROUND(SUM(D.NET_WT),2),0) AS DISPATCHWT,ISNULL(ROUND(SUM(D.NET_WT*D.RATE)/SUM(NET_WT),2),0)  AS DISAVGPRC ,ISNULL(ROUND(SUM(D.NET_WT*D.RATE),2),0) AS DISVALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_SHIPMENT_DTLS SHD ,GPIL_SHIPMENT_HDR SHH WHERE D.STATUS='Y' AND SHD.SHIPMENT_NO=SHH.SHIPMENT_NO AND SHH.SENDER_ORGN_CODE=H.ORGN_CODE AND SHD.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.HEADER_ID=D.HEADER_ID AND H.ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP') AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' GROUP BY H.ORGN_CODE) TBL2,GPIL_VARIETY_MASTER VM,GPIL_CROP_MASTER CM WHERE VM.VARIETY='" + cbxvariety.Text + "' AND CM.CROP='" + cbxcrop.Text + "' AND TBL2.ORGN_CODE=TBL1.ORGN_CODE AND TBL1.ORGN_CODE='" + cbxorgcd.Text + "' ORDER BY TBL1.ORGN_CODE";

                }
                DataSet ds = new DataSet();
                ds = lpdMgt.GetTabPurchaseSummary(strsql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptStockSummaryrpt.rpt"));
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

        }
    }
}