﻿using System;
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
    public partial class ClassificationCumulativeWithGradeTransferReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        string strsql;
        ReportDocument CustomerReport = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
            }
            if (IsPostBack)
            {

                if (rdbcomplete.Checked == true)
                {
                    viewrpt();
                }
                else
                {
                    viewrpt2();
                }
            }
        }

        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                ddlOrganization.DataSource = ds.Tables[0];
                ddlOrganization.DataTextField = "ORGN_CODE1";
                ddlOrganization.DataValueField = "ORGN_CODE";
                ddlOrganization.DataBind();
                ddlOrganization.Items.Insert(0, new ListItem("- Select -", "0"));
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
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                if (validated())
                {
                    if (rdbcomplete.Checked == true)
                    {
                        viewrpt();
                    }
                    else
                    {
                        viewrpt2();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        public bool validated()
        {
            if (txtFromDate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (txtToDate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txtToDate.Focus();
                return false;
            }
            else if (ddlCrop.SelectedIndex == 0)
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
        public void viewrpt2()
        {
            try
            {
                if (ddlOrganization.SelectedIndex == 0)
                    strsql = "SELECT CONVERT( INT,I.ATTRIBUTE1) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as avgpr,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC,GPIL_STOCK STK where HC.BATCH_NO=C.BATCH_NO  AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=STK.GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER AND STK.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59 PM',105) group by H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO";
                else
                    strsql = "SELECT CONVERT( INT,I.ATTRIBUTE1) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as avgpr,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC,GPIL_STOCK STK where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND  I.ITEM_CODE=STK.GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER AND STK.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59 PM',105) and H.ORGN_CODE='" + ddlOrganization.Text + "'  group by H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO";


                DataSet ds = new DataSet();
                ds = ppdMgt.GetDataSet(strsql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptClassificationReportforTAP_Summary.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                ClassificationCumulativeTransferRpt.ReportSource = CustomerReport;
                ClassificationCumulativeTransferRpt.DataBind();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        public void viewrpt()
        {
            try
            {

                if (ddlOrganization.SelectedIndex == 0)
                    strsql = "SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC,GPIL_STOCK STK where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=STK.GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER AND STK.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59 PM',105) group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO";

                else
                    strsql = "SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC,GPIL_STOCK STK where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND  I.ITEM_CODE=STK.GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER AND STK.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER   AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59 PM',105) and H.ORGN_CODE='" + ddlOrganization.Text + "'  group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO";

                DataSet ds = new DataSet();
                ds = ppdMgt.GetDataSet(strsql);


                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptClassificationCumulativeTAPWiseReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                ClassificationCumulativeTransferRpt.ReportSource = CustomerReport;
                ClassificationCumulativeTransferRpt.DataBind();
                
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