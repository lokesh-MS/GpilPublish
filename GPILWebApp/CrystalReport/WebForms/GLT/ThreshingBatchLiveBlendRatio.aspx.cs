using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.GLT
{
    public partial class ThreshingBatchLiveBlendRatio : System.Web.UI.Page
    {

        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();

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
                ds3 = crd.GetORGN("ORGN", "", "");
                ddlToOrgnCode.DataSource = ds3.Tables[0];
                ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                ddlToOrgnCode.DataValueField = "ORGN_CODE";
                ddlToOrgnCode.DataBind();
                ddlToOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));
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
            Response.Redirect("ThreshingBatchLiveBlendRatio.aspx");
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
        string strsql;
        GLTManagement GLTMgt = new GLTManagement();
        
        public void viewrpt()
        {
            try
            {
               // SqlConnection connection = new SqlConnection(ClsConnection.strConnectionString);
                //strsql = "SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + cbxcrop.Text + "' AS CROP,'" + cbxvariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_Report_Date.Text + "' AS FROM_DATE,'" + txttodate.Text + "' AS TO_DATE FROM GPIL_THRESH_RECON_DTLS_1 D1,GPIL_THRESH_RECON_DTLS_2 D2, GPIL_THRESH_RECON_HDR H WHERE D1.BATCH_NO=H.BATCH_NO AND D2.BATCH_NO=H.BATCH_NO AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',105) AND H.CROP='" + cbxcrop.Text + "' AND H.VARIETY='" + cbxvariety.Text + "' AND D2.PACKED_GRADE IN (SELECT ITEM_CODE FROM GPIL_ITEM_MASTER WHERE ITEM_DESC='" + ddlPackedGrade.Text + "') AND D1.BALE_TYPE='IPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY D1.BATCH_NO,D1.GRADE";

                if (rdbIssueBlendRatio.Checked == true)
                {
                    strsql = "SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(H.DATE_OF_OPERATION) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_DTLS_1_TEMP D1 ,GPIL_THRESH_RECON_HDR_TEMP H WHERE H.BATCH_NO=D1.BATCH_NO AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.ORGN_CODE='" + ddlToOrgnCode.Text + "' AND H.STATUS IN ('Y','I') AND D1.BALE_TYPE='IPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY D1.BATCH_NO,D1.GRADE";
                }
                else
                {
                    strsql = "SELECT D.BATCH_NO, D.PACKED_GRADE AS GRADE,ROUND(SUM(NET_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(H.DATE_OF_OPERATION) AS TO_DATE,'PRODUCT' AS BALE_TYPE  FROM GPIL_THRESH_RECON_DTLS_2_TEMP D, GPIL_THRESH_RECON_HDR_TEMP H,GPIL_ITEM_MASTER I WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('Y','I') AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.ORGN_CODE='" + ddlToOrgnCode.Text + "' AND PACKED_GRADE=I.ITEM_CODE GROUP BY D.BATCH_NO,D.PACKED_GRADE UNION SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'-' AS PACKED_GRADE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(H.DATE_OF_OPERATION) AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_DTLS_1_TEMP D1 ,GPIL_THRESH_RECON_HDR_TEMP H WHERE H.BATCH_NO=D1.BATCH_NO AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.ORGN_CODE='" + ddlToOrgnCode.Text + "' AND H.STATUS IN ('Y','I') AND D1.BALE_TYPE='OPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY BATCH_NO,GRADE";
                }

                DataTable dt = new DataTable("DT_BLENDRATIO_REPORT");
                //SqlDataAdapter adapter = new SqlDataAdapter(strsql, ClsConnection.SqlCon);
                //adapter.SelectCommand.CommandTimeout = 0;
                //adapter.Fill(dt);
                dt = GLTMgt.GetQueryResult(strsql);
                ReportDocument CustomerReport = new ReportDocument();


                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptThreshingBlendRatio.rpt"));
                CustomerReport.SetDataSource(dt.DefaultView);
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