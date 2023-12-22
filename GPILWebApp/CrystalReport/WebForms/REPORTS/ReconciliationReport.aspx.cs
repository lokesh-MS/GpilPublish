using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class ReconciliationReport : System.Web.UI.Page
    {
     
        ReportDocument CustomerReport = new ReportDocument();   
        CrystalReportData crd = new CrystalReportData();
     
 
        DataTable dtWeekHdr = new DataTable();
        DataTable dtWeekDtl = new DataTable();
        DataTable dt2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
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


        private void bindDropDown()
        {
            try
            {
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                ddlCrop.DataSource = ds1.Tables[0];
                ddlCrop.DataTextField = "CROPYEAR";
                ddlCrop.DataValueField = "CROP";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                ddlVariety.DataSource = ds2.Tables[0];
                ddlVariety.DataTextField = "VARIETYNAME";
                ddlVariety.DataValueField = "VARIETY";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, new ListItem("- Select -", "0"));

                DataSet ds3 = new DataSet();
                ds2 = crd.GetORGN("ORGN", "", "");
                ddlOrgnCode.DataSource = ds2.Tables[0];
                ddlOrgnCode.DataTextField = "ORGN_CODE1";
                ddlOrgnCode.DataValueField = "ORGN_CODE";
                ddlOrgnCode.DataBind();
                ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");


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

        protected void btnclose_Click(object sender, EventArgs e)
        {
           
        }

        public bool validated()
        {
            if (ddlCrop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                ddlCrop.Focus();
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

        public void viewrpt()
        {
           string  strsql = "SELECT *,('" + ddlCrop.Text + " - ' + " +
                    "(SELECT CROP_YEAR FROM GPIL_CROP_MASTER WHERE CROP='" + ddlCrop.Text + "')) AS RPT_CROP,('" + ddlVariety.Text + " - ' + " +
                    "(SELECT VARIETY_NAME FROM GPIL_VARIETY_MASTER WHERE VARIETY='" + ddlVariety.Text + "')) AS RPT_VARIETY,'" + ddlOrgnCode.Text + " - ' + " +
                    "(SELECT (SUBSTRING(ORGN_NAME,0,32)) AS ORGN_NAME FROM GPIL_ORGN_MASTER WHERE ORGN_CODE='" + ddlOrgnCode.Text + "') AS RPT_ORGN,'0' AS RPT_CLOSING_STK_BALES,'0' AS RPT_CLOSING_STK_QTY FROM " +
                    "(SELECT COUNT(GPIL_BALE_NUMBER) AS STK_BALES,ISNULL(ROUND(SUM(MARKED_WT),1),0) AS STK_QTY FROM GPIL_STOCK WHERE CROP ='" + ddlCrop.Text + "' AND VARIETY ='" + ddlVariety.Text + "' AND CURR_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS='Y' AND PRODUCT_TYPE NOT IN ('LOSS','SLOSS')) AS T1,   " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS DIS_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS DIS_QTY FROM GPIL_SHIPMENT_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR WHERE SENDER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('INT','N'))) AS  T2,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS REC_TAP_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS REC_TAP_QTY FROM GPIL_SHIPMENT_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR WHERE RECEIVER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N') AND SENDER_ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP')) AND D.STATUS='RCV') AS  T3,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS REC_INT_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS REC_INT_QTY FROM GPIL_SHIPMENT_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR WHERE RECEIVER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N') AND SENDER_ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE NOT IN ('TAP'))) AND D.STATUS='RCV') AS  T4, " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS TAP_BALES,ISNULL(ROUND(SUM(NET_WT),1),0) AS TAP_QTY FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND HEADER_ID IN (SELECT HEADER_ID FROM GPIL_TAP_FARM_PURCHS_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','P') AND PURCHASE_TYPE='TAP PURCHASE')) AS  T5, " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS FARMER_BALES,ISNULL(ROUND(SUM(NET_WT),1),0) AS FARMER_QTY FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND REJE_STATUS='OK' AND HEADER_ID IN (SELECT HEADER_ID FROM GPIL_TAP_FARM_PURCHS_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','P') AND PURCHASE_TYPE='SUNDRY PURCHASE')) AS T5P1,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS SUPPLIER_BALES,ISNULL(ROUND(SUM(NET_WEIGHT),1),0) AS SUPPLIER_QTY FROM GPIL_SUPP_PURCHS_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND HEADER_ID IN (SELECT HEADER_ID FROM GPIL_SUPP_PURCHS_HDR WHERE RECEV_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','P') )) AS  T5P2,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS REC_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS REC_QTY FROM GPIL_SHIPMENT_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SHIPMENT_HDR WHERE RECEIVER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N')) AND D.STATUS='RCV') AS  T6,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS CLS_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS CLS_QTY FROM GPIL_CLASSIFICATION_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT D.BATCH_NO FROM GPIL_CLASSIFICATION_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N') AND REASONING_CODE='0'  AND ISNULL(ATTRIBUTE3,'')<>'N'))  AS T7,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS GDT_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS GDT_QTY FROM GPIL_CLASSIFICATION_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT D.BATCH_NO FROM GPIL_CLASSIFICATION_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N') AND REASONING_CODE='1'  AND ISNULL(ATTRIBUTE3,'')<>'N' )) AS T8,  " +
                    "(SELECT COUNT(OLD_BALE_NUMBER) AS CROP_ISSUDED_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS CROP_ISSUDED_QTY FROM GPIL_CROP_TRANS_DTLS D,GPIL_STOCK S WHERE D.OLD_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CROP_TRANS_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN'))) AS  T9,  " +
                    "(SELECT COUNT(NEW_BALE_NUMBER) AS CROP_OUTTURN_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS CROP_OUTTURN_QTY FROM GPIL_CROP_TRANS_DTLS D,GPIL_STOCK S WHERE D.NEW_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CROP_TRANS_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN'))) AS  T10,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS GRD_ISSUDED_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS GRD_ISSUDED_QTY FROM GPIL_GRADING_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_GRADING_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN') AND ISNULL(ATTRIBUTE3,'')<>'N') AND BALE_TYPE='IPB') AS T10A,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS GRD_OUT_PRD_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS GRD_OUT_PRD_QTY FROM GPIL_GRADING_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_GRADING_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN') AND ISNULL(ATTRIBUTE3,'')<>'N') AND BALE_TYPE='OPB' AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) AS  T11,   " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS GRD_OUT_BYP_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS GRD_OUT_BYP_QTY FROM GPIL_GRADING_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_GRADING_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN') AND ISNULL(ATTRIBUTE3,'')<>'N') AND BALE_TYPE='OPB' AND D.PRODUCT_TYPE = 'BP')  AS T12,   " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS GRD_OUT_LOS_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS GRD_OUT_LOS_QTY FROM GPIL_GRADING_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_GRADING_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN') AND ISNULL(ATTRIBUTE3,'')<>'N') AND BALE_TYPE='OPB' AND D.PRODUCT_TYPE = 'LOSS')  AS T13,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS GRD_OUTTURN_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS GRD_OUTTURN_QTY FROM GPIL_GRADING_DTLS D,GPIL_STOCK S WHERE D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND D.BATCH_NO IN (SELECT BATCH_NO FROM GPIL_GRADING_HDR WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('N','NN') AND ISNULL(ATTRIBUTE3,'')<>'N') AND BALE_TYPE='OPB')  AS T14, " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS THR_ISSUDED_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS THR_ISSUDED_QTY FROM GPIL_THRESH_RECON_DTLS_1 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND BALE_TYPE='IPB') AS T15,  " +
                    "(SELECT COUNT(CASE_NUMBER) AS THR_OUT_PRD_BALES,ISNULL(ROUND(SUM(NET_WT),1),0) AS THR_OUT_PRD_QTY FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.CASE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "') AS T16,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS THR_OUT_BYP_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS THR_OUT_BYP_QTY FROM GPIL_THRESH_RECON_DTLS_1 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND BALE_TYPE='OPB' AND D.PRODUCT_TYPE IN('BP','GT','EB')) AS T17,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS THR_OUT_SLOS_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS THR_OUT_SLOS_QTY FROM GPIL_THRESH_RECON_DTLS_1 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND BALE_TYPE='OPB' AND D.PRODUCT_TYPE IN('SLOSS')) AS T18,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS THR_OUT_PLOS_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS THR_OUT_PLOS_QTY FROM GPIL_THRESH_RECON_DTLS_1 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND BALE_TYPE='OPB' AND D.PRODUCT_TYPE IN('LOSS')) AS T18P1,  " +
                    "(SELECT SUM(THR_OUTTURN_BALES) AS THR_OUTTURN_BALES,SUM(THR_OUTTURN_QTY) AS THR_OUTTURN_QTY FROM ((SELECT COUNT(D.GPIL_BALE_NUMBER) AS THR_OUTTURN_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS THR_OUTTURN_QTY FROM GPIL_THRESH_RECON_DTLS_1 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND BALE_TYPE='OPB' ) UNION (SELECT COUNT(CASE_NUMBER) AS THR_OUTTURN_BALES,ISNULL(ROUND(SUM(NET_WT),1),0) AS THR_OUTTURN_QTY FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H,GPIL_STOCK S WHERE H.BATCH_NO=D.BATCH_NO AND  H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.STATUS IN ('N') AND S.GPIL_BALE_NUMBER=D.CASE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "')) AS TBLOT) AS T19,  " +
                    "(SELECT COUNT(D.GPIL_BALE_NUMBER) AS SAL_BALES,ISNULL(ROUND(SUM(D.MARKED_WT),1),0) AS SAL_QTY FROM GPIL_SO_RESERVATION_DTLS D,GPIL_STOCK S WHERE S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND SHIPMENT_NO IN (SELECT SHIPMENT_NO FROM GPIL_SO_RESERVATION_HDR WHERE SENDER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND STATUS IN ('INT','N'))) AS T20  ";

            DataTable dt = new DataTable();
            dt = crd.GetQueryResult(strsql);
            CustomerReport.Load(Server.MapPath("~/CrystalReport/RptOrgWiseStockReconciliationReport.rpt"));
            CustomerReport.SetDataSource(dt.DefaultView);
            // CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
            CrystalReportViewer1.ReportSource = CustomerReport;
            CrystalReportViewer1.DataBind();
        }

    }
}