using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPILWebApp.ViewModel;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace GPILWebApp
{
    public partial class ThreshingHourBasedBlendRatio : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    DataSet ds = new DataSet();
                    ds = crd.GetORGN("CROP", "", "");
                    ddlCrop.DataSource = ds.Tables[0];
                    ddlCrop.DataTextField = "CROPYEAR";
                    ddlCrop.DataValueField = "CROP";
                    ddlCrop.DataBind();
                    ddlCrop.Items.Insert(0, "SELECT CROP YEAR");

                    DataSet ds1 = new DataSet();
                    ds1 = crd.GetORGN("VARIETY", "", "");
                    ddlVariety.DataSource = ds1.Tables[0];
                    ddlVariety.DataTextField = "VARIETYNAME";
                    ddlVariety.DataValueField = "VARIETY";
                    ddlVariety.DataBind();
                    ddlVariety.Items.Insert(0, "SELECT VARIETY CODE");

                    DataSet ds2 = new DataSet();
                    ds2 = crd.GetORGN("ORGN", "", "");
                    ddlOrgnCode.DataSource = ds2.Tables[0];
                    ddlOrgnCode.DataTextField = "ORGN_CODE1";
                    ddlOrgnCode.DataValueField = "ORGN_CODE";
                    ddlOrgnCode.DataBind();
                    ddlOrgnCode.Items.Insert(0, "SELECT ORGN CODE");

                    viewrpt();

                }
                catch (Exception ex)
                { }
            }
            else
            {
                viewrpt();
            }
        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlVariety_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBatchRefNumber();
        }

        protected void ddlBatchNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void GetBatchRefNumber()
        {
            try
            {
                VerificationManagement vMgt = new VerificationManagement();
                DataSet ds = new DataSet();
                //ds = crd.GetORGN("FARMCODE", ddlOrgnCode.SelectedValue.ToString() + "20150620", "");
                //strsql = "SELECT DISTINCT BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP WHERE ORGN_CODE='" + ddlOrgnCode.Text + "' AND CROP='" + ddlCrop.Text + "' AND VARIETY='" + ddlVariety.Text + "' AND STATUS IN ('Y','I','CC')";
                string strsql = "SELECT TBL1.BATCH_NO AS BATCH_NO,(TBL1.BATCH_NO + ' || ' + ISNULL(TBL2.PACKED_GRADE,'') + ' - ' + ISNULL(TBL2.ITEM_DESC,'Not Yet Defined')) AS BATCH_DESC FROM (SELECT  H.BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP H WHERE H.STATUS IN ('Y','I','CC')AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "') AS TBL1 LEFT OUTER JOIN (SELECT DISTINCT BATCH_NO,PACKED_GRADE,ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_ITEM_MASTER I WHERE D.PACKED_GRADE=I.ITEM_CODE AND D.BATCH_NO IN ((SELECT  H.BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP H WHERE H.STATUS IN ('Y','I','CC')AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "'))) AS TBL2 ON TBL1.BATCH_NO=TBL2.BATCH_NO  ORDER BY TBL1.BATCH_NO";
                ds = vMgt.GetdsQueryResult(strsql);
                ddlBatchNumber.DataSource = ds.Tables[0];
                ddlBatchNumber.DataTextField = "BATCH_DESC";
                ddlBatchNumber.DataValueField = "BATCH_NO";
                ddlBatchNumber.DataBind();
                ddlBatchNumber.Items.Insert(0, "SELECT BATCH NUMBER");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
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
            string strsql = "";
            try
            {
                VerificationManagement vMgt = new VerificationManagement();
                ReportDocument CustomerReport = new ReportDocument();
                RDLCReport rdlcReport = new RDLCReport();
                DataSet ds = new DataSet();

                if (ddlBatchNumber.SelectedIndex > 0)
                {
                    if (rdoIssue.Checked == true)
                    {
                        //strsql = "SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO AND H.BATCH_NO='" + ddlBatchNumber.Text + "' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='IPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) ORDER BY BATCH_NO,GRADE,TIME_INTERVAL";
                        strsql = "SELECT (TBL1.BATCH_NO + ' ' + ISNULL(TBL2.PACKED_GRADE,'') + ' (' + ISNULL(TBL2.ITEM_DESC,'Not Yet Defined') + ')') AS BATCH_NO,GRADE, BALES,QUANTITY,TIME_INTERVAL,CROP,VARIETY,ISNULL(TBL2.ITEM_DESC,'Not Yet Defined') AS PACKED_GRADE,FROM_DATE,TO_DATE,BALE_TYPE FROM (SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO  AND H.BATCH_NO='" + ddlBatchNumber.Text + "' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='IPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1)) AS TBL1 LEFT OUTER JOIN (SELECT DISTINCT BATCH_NO,PACKED_GRADE,ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_ITEM_MASTER I WHERE D.PACKED_GRADE=I.ITEM_CODE AND D.BATCH_NO IN ((SELECT  H.BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP H WHERE H.STATUS IN ('Y','I','CC')  AND H.BATCH_NO='" + ddlBatchNumber.Text + "' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "'))) AS TBL2 ON TBL1.BATCH_NO=TBL2.BATCH_NO ORDER BY TBL1.BATCH_NO,GRADE,TIME_INTERVAL";

                    }
                    else
                    {
                        //strsql = "SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'BY-PRODUCTS' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO AND H.BATCH_NO='" + ddlBatchNumber.Text + "' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='OPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) ORDER BY BATCH_NO,GRADE,TIME_INTERVAL";
                        strsql = "SELECT (TBL1.BATCH_NO + ' ' + ISNULL(TBL2.PACKED_GRADE,'') + ' (' + ISNULL(TBL2.ITEM_DESC,'Not Yet Defined') + ')') AS BATCH_NO,GRADE, BALES,QUANTITY,TIME_INTERVAL,CROP,VARIETY,ISNULL(TBL2.ITEM_DESC,'Not Yet Defined') AS PACKED_GRADE,FROM_DATE,TO_DATE,BALE_TYPE FROM (SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO  AND H.BATCH_NO='" + ddlBatchNumber.Text + "' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='IPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1)) AS TBL1 LEFT OUTER JOIN (SELECT DISTINCT BATCH_NO,PACKED_GRADE,ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_ITEM_MASTER I WHERE D.PACKED_GRADE=I.ITEM_CODE AND D.BATCH_NO IN ((SELECT  H.BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP H WHERE H.STATUS IN ('Y','I','CC')  AND H.BATCH_NO='" + ddlBatchNumber.Text + "' AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "'))) AS TBL2 ON TBL1.BATCH_NO=TBL2.BATCH_NO ORDER BY TBL1.BATCH_NO,GRADE,TIME_INTERVAL";
                    }
                }
                else
                {
                    if (rdoIssue.Checked == true)
                    {
                        //strsql = "SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='IPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) ORDER BY BATCH_NO,GRADE,TIME_INTERVAL";
                        strsql = "SELECT (TBL1.BATCH_NO + ' ' + ISNULL(TBL2.PACKED_GRADE,'') + ' (' + ISNULL(TBL2.ITEM_DESC,'Not Yet Defined') + ')') AS BATCH_NO,GRADE, BALES,QUANTITY,TIME_INTERVAL,CROP,VARIETY,TBL1.PACKED_GRADE,FROM_DATE,TO_DATE,BALE_TYPE FROM (SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='IPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1)) AS TBL1 LEFT OUTER JOIN (SELECT DISTINCT BATCH_NO,PACKED_GRADE,ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_ITEM_MASTER I WHERE D.PACKED_GRADE=I.ITEM_CODE AND D.BATCH_NO IN ((SELECT  H.BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP H WHERE H.STATUS IN ('Y','I','CC') AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "'))) AS TBL2 ON TBL1.BATCH_NO=TBL2.BATCH_NO ORDER BY TBL1.BATCH_NO,GRADE,TIME_INTERVAL";
                    }
                    else
                    {
                        //strsql = "SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'BY-PRODUCTS' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='OPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) ORDER BY BATCH_NO,GRADE,TIME_INTERVAL";
                        strsql = "SELECT (TBL1.BATCH_NO + ' ' + ISNULL(TBL2.PACKED_GRADE,'') + ' (' + ISNULL(TBL2.ITEM_DESC,'Not Yet Defined') + ')') AS BATCH_NO,GRADE, BALES,QUANTITY,TIME_INTERVAL,CROP,VARIETY,TBL1.PACKED_GRADE,FROM_DATE,TO_DATE,BALE_TYPE FROM (SELECT D.BATCH_NO,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(MARKED_WT),2) AS QUANTITY,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1) AS TIME_INTERVAL,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(ISNULL(D.CREATED_DATE,GETDATE())) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_HDR_TEMP H,GPIL_THRESH_RECON_DTLS_1_TEMP D  WHERE H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.STATUS IN ('Y','I','CC') AND D.BALE_TYPE='IPB' GROUP BY D.BATCH_NO,D.GRADE,(CEILING((DATEDIFF(SECOND,H.DATE_OF_OPERATION,D.CREATED_DATE)/3600))+1)) AS TBL1 LEFT OUTER JOIN (SELECT DISTINCT BATCH_NO,PACKED_GRADE,ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_ITEM_MASTER I WHERE D.PACKED_GRADE=I.ITEM_CODE AND D.BATCH_NO IN ((SELECT  H.BATCH_NO FROM GPIL_THRESH_RECON_HDR_TEMP H WHERE H.STATUS IN ('Y','I','CC') AND H.ORGN_CODE='" + ddlOrgnCode.Text + "' AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "'))) AS TBL2 ON TBL1.BATCH_NO=TBL2.BATCH_NO ORDER BY TBL1.BATCH_NO,GRADE,TIME_INTERVAL";

                    }
                }
                ds= vMgt.GetdsQueryResult(strsql);
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptThreshingHourlyBlendRatio.rpt"));
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
            Response.Redirect("ThreshingHourBasedBlendRatio.aspx");

        }
    }
}