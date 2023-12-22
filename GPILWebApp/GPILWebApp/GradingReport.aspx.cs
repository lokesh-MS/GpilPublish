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
    public partial class GradingReport : System.Web.UI.Page
    {
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
        }
        //RecpCode
        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                ddlOrganizationCode.DataSource = ds.Tables[0];
                ddlOrganizationCode.DataTextField = "ORGN_CODE1";
                ddlOrganizationCode.DataValueField = "ORGN_CODE";
                ddlOrganizationCode.DataBind();
                ddlOrganizationCode.Items.Insert(0, new ListItem("- Select -", "0"));
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
                DataSet ds3 = new DataSet();
                ds3 = crd.GetORGN("RecpCode", "", "");
                ddlOperationReceipe.DataSource = ds3.Tables[0];
                ddlOperationReceipe.DataTextField = "ReceipeCode";
                ddlOperationReceipe.DataValueField = "RECIPE_CODE";
                ddlOperationReceipe.DataBind();
                ddlOperationReceipe.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            if (this.valgrdrpt() == true)
            {
                viewrpt();
            }

        }

        public bool valgrdrpt()
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
            else if (ddlOperationReceipe.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Operation Recipe');", true);
                ddlOperationReceipe.Focus();
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
                DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                txtFromDate.Text = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txtToDate.Text);
                txtToDate.Text = dt1.ToString("dd-MM-yyyy");
                string sql = "";
                // sql = "SELECT D.GRADE,H.CROP,H.VARIETY,H.ORGN_CODE,H.RECIPE_CODE,CONVERT(NVARCHAR(10),H.DATE_OF_OPERATION,105),H.ISSUED_GRADE,H.TOT_ISSUE_BALES,H.TOT_ISSUE_QTY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,COUNT(D.GPIL_BALE_NUMBER) AS CNN,SUM(D.MARKED_WT)as wtt,O.ORGN_NAME FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H,GPIL_ORGN_MASTER O where H.RECIPE_CODE='" + cmb_Operation_Receipe.Text + "' AND H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + cmb_Orgn_Code.Text + "' AND H.VARIETY='" + cmb_Variety.Text + "' AND H.STATUS='N' AND D.BALE_TYPE='OPB' AND CONVERT(NVARCHAR(10),H.DATE_OF_OPERATION,105)= '" + txt_From_Date.Text + "' AND O.ORGN_CODE=H.ORGN_CODE GROUP BY D.GRADE,H.CROP,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.ISSUED_GRADE,H.TOT_ISSUE_BALES,H.TOT_ISSUE_QTY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.ORGN_CODE,H.VARIETY,O.ORGN_NAME";
                if (ddlOrganizationCode.SelectedIndex != 0)
                {
                    sql = "select SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,SUM(D.MARKED_WT) AS QTY ,H.CROP,H.VARIETY,(V.VARIETY_NAME + (SELECT '      # No of Graders : ' + CONVERT(NVARCHAR(20),ROUND(SUM(DISTINCT H.NO_OF_GRADERS),1)) + ' & Ave.O/P : ' + CONVERT(NVARCHAR(20),ROUND(SUM(D.MARKED_WT)/SUM(DISTINCT H.NO_OF_GRADERS),2)) AS WORKERS FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND H.VARIETY='" + ddlVariety.Text + "' and H.CROP='" + ddlCropYear.Text + "' and H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' and ORGN_CODE='" + ddlOrganizationCode.Text + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59',105) AND D.BALE_TYPE='IPB')) AS VARIETY_NAME ,H.RECIPE_CODE,H.DATE_OF_OPERATION,ISNULL(I.ATTRIBUTE4,'')HsnCode from GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D ,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND V.VARIETY=H.VARIETY and H.VARIETY='" + ddlVariety.Text + "' and H.CROP='" + ddlCropYear.Text + "' and H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' and ORGN_CODE='" + ddlOrganizationCode.Text + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59',105) AND D.BALE_TYPE='OPB' AND H.ISSUED_GRADE=I.ITEM_CODE GROUP BY D.GRADE,H.ISSUED_GRADE,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION,I.ATTRIBUTE4 ORDER BY H.ISSUED_GRADE,D.GRADE DESC";
                    //sql = "select SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,SUM(D.MARKED_WT) AS QTY ,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION from GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D ,GPIL_VARIETY_MASTER V WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND V.VARIETY=H.VARIETY and H.VARIETY='" + cmb_Variety.Text + "' and H.CROP='" + cmb_Crop.Text + "' and H.RECIPE_CODE='" + cmb_Operation_Receipe.Text + "' and ORGN_CODE='" + cmb_Orgn_Code.Text + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + txt_From_Date.Text + " 00:00:00',105) and CONVERT(datetime,'" + txt_To_Date.Text + " 23:59:59',105) AND D.BALE_TYPE='OPB' GROUP BY D.GRADE,H.ISSUED_GRADE,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION ORDER BY H.ISSUED_GRADE,D.GRADE DESC";
                }
                else
                {
                    sql = "select SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,SUM(D.MARKED_WT) AS QTY ,H.CROP,H.VARIETY,(V.VARIETY_NAME + (SELECT '      # No of Graders : ' + CONVERT(NVARCHAR(20),ROUND(SUM(DISTINCT H.NO_OF_GRADERS),1)) + ' & Ave.O/P : ' + CONVERT(NVARCHAR(20),ROUND(SUM(D.MARKED_WT)/SUM(DISTINCT H.NO_OF_GRADERS),2)) AS WORKERS FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND H.VARIETY='" + ddlVariety.Text + "' and H.CROP='" + ddlCropYear.Text + "' and H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59',105) AND D.BALE_TYPE='IPB')) AS VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION,ISNULL(I.ATTRIBUTE4,'')HsnCode from GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D ,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND V.VARIETY=H.VARIETY and H.VARIETY='" + ddlVariety.Text + "' and H.CROP='" + ddlCropYear.Text + "' and H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59',105) AND D.BALE_TYPE='OPB' AND H.ISSUED_GRADE=I.ITEM_CODE GROUP BY D.GRADE,H.ISSUED_GRADE,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION,I.ATTRIBUTE4 ORDER BY H.ISSUED_GRADE,D.GRADE DESC";
                    //sql = "select SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,SUM(D.MARKED_WT) AS QTY ,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION from GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D ,GPIL_VARIETY_MASTER V WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.BATCH_NO=D.BATCH_NO  AND V.VARIETY=H.VARIETY and H.VARIETY='" + cmb_Variety.Text + "' and H.CROP='" + cmb_Crop.Text + "' and H.RECIPE_CODE='" + cmb_Operation_Receipe.Text + "' and H.DATE_OF_OPERATION between CONVERT(datetime,'" + txt_From_Date.Text + " 00:00:00',105) and CONVERT(datetime,'" + txt_To_Date.Text + " 23:59:59',105) AND D.BALE_TYPE='OPB' GROUP BY D.GRADE,H.ISSUED_GRADE,H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.DATE_OF_OPERATION ORDER BY H.ISSUED_GRADE,D.GRADE DESC";

                }


                DataSet ds = new DataSet();
                ds = ppdMgt.GetClassificationReport(sql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptGradingReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();


               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}