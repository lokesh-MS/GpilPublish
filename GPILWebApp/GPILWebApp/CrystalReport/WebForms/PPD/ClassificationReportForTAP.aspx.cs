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
    public partial class ClassificationReportForTAP : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
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
            viewrpt();
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
            string strsql = "";
            try
            {
              
                if (cbxorgcd.SelectedIndex == 0)               
                    strsql = "SELECT SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE)) as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as avgpr,O.ORGN_CODE+' - '+ O.ORGN_NAME AS ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59',105) AND HC.RECIPE_CODE='CLASSIFICATION' group by O.ORGN_CODE+' - '+ O.ORGN_NAME ,H.CROP,H.VARIETY,V.VARIETY_NAME,SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE))";                 
               
                else               
                    strsql = "SELECT SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE)) as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(AVG(D.RATE),2) as avgpr,O.ORGN_CODE+' - '+ O.ORGN_NAME AS ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO  AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and HC.CLASSIFICATION_DATE  between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59',105) and HC.ORGN_CODE='" + cbxorgcd.Text + "' AND HC.RECIPE_CODE='CLASSIFICATION'  group by O.ORGN_CODE+' - '+ O.ORGN_NAME ,H.CROP,H.VARIETY,V.VARIETY_NAME,SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE))";

                DataSet dataset = new DataSet();

                DataSet ds = new DataSet();
                ds = ppdMgt.GetClassificationRptforTap(strsql);
                ReportDocument CustomerReport = new ReportDocument();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptClassificationReportforTAP.rpt"));
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassificationReportForTAP.aspx");
        }


    }
}