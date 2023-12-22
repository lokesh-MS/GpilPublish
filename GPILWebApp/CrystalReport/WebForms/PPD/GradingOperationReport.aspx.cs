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
    public partial class GradingOperationReport : System.Web.UI.Page
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
                else
                {
                    if(ddlVariety.SelectedIndex != 0 || ddlOperationReceipe.SelectedIndex != 0)
                    {
                        viewrpt();
                    }
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            viewrpt();
        }

        public void viewrpt()
        {
            try
            {
                string frmDate;
                string toDate;
                DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                frmDate = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txtToDate.Text);
                toDate = dt1.ToString("dd-MM-yyyy");
                string sql = "";

                if (valgrdrpt())
                {
                    if (ddlOrganizationCode.SelectedIndex != 0)
                    {

                        sql = "SELECT TBL1.CROP,(TBL1.VARIETY + ' - ' + V.VARIETY_NAME) AS VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL1.FROM_DATE,TBL1.TO_DATE,('Graders : ' + CONVERT(NVARCHAR(15),TBL1.GRADERS) + ' & APH : ' +  CONVERT(NVARCHAR(15),ROUND(TBL3.QTY/TBL1.GRADERS,2))) AS GRADERS,TBL2.GRADE,TBL2.PRODUCT_TYPE,SUM(TBL2.QTY) AS QUANTITY,(SELECT ('Graders: ' + CASE WHEN T2.QTY=0 THEN '0' ELSE CONVERT(NVARCHAR(15),T1.GDRS) END + ' & APH:' +  CONVERT(NVARCHAR(15),ROUND(T2.QTY/T1.GDRS,2))) AS APH FROM (SELECT ISNULL(SUM(H.NO_OF_GRADERS),1) AS GDRS  FROM GPIL_GRADING_HDR H  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "'  AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'01-01-2015 00:00:00',105) AND CONVERT(DATETIME,'01-01-2016 23:59:59',105)) AS T1,(SELECT ISNULL(SUM(D.MARKED_WT),0) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'01-01-2015 00:00:00',105) AND CONVERT(DATETIME,'01-01-2016 23:59:59',105) AND ((H.RECIPE_CODE IN ('HAND STEMMING','BUTTING') AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) OR (H.RECIPE_CODE NOT IN ('HAND STEMMING','BUTTING')))) AS T2) AS APH  FROM (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(H.DATE_OF_OPERATION) AS TO_DATE,SUM(H.NO_OF_GRADERS) AS GRADERS FROM GPIL_GRADING_HDR H  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND  H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) GROUP BY H.ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE) AS TBL1 LEFT OUTER JOIN (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,D.PRODUCT_TYPE,SUM(D.MARKED_WT) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) GROUP BY H.ISSUED_GRADE,D.GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,D.PRODUCT_TYPE) AS TBL2 ON TBL1.CROP=TBL2.CROP AND TBL1.VARIETY=TBL2.VARIETY AND TBL1.RECIPE_CODE=TBL2.RECIPE_CODE AND TBL1.ISSUED_GRADE=TBL2.ISSUED_GRADE LEFT OUTER JOIN (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,SUM(D.MARKED_WT) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) AND ((H.RECIPE_CODE IN ('HAND STEMMING','BUTTING') AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) OR (H.RECIPE_CODE NOT IN ('HAND STEMMING','BUTTING'))) GROUP BY H.ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE) AS TBL3 ON TBL1.CROP=TBL3.CROP AND TBL1.VARIETY=TBL3.VARIETY AND TBL1.RECIPE_CODE=TBL3.RECIPE_CODE AND TBL1.ISSUED_GRADE=TBL3.ISSUED_GRADE LEFT OUTER JOIN (SELECT VARIETY,VARIETY_NAME FROM GPIL_VARIETY_MASTER WHERE VARIETY='" + ddlVariety.Text + "') AS V ON V.VARIETY=TBL1.VARIETY GROUP BY TBL1.CROP,TBL1.VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL1.FROM_DATE,TBL1.TO_DATE,TBL1.GRADERS,TBL2.GRADE,TBL2.PRODUCT_TYPE,V.VARIETY_NAME,TBL3.QTY ORDER BY TBL1.CROP,TBL1.VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL2.GRADE";
                    }
                    else
                    {

                        sql = "SELECT TBL1.CROP,(TBL1.VARIETY + ' - ' + V.VARIETY_NAME) AS VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL1.FROM_DATE,TBL1.TO_DATE,('Graders : ' + CONVERT(NVARCHAR(15),TBL1.GRADERS) + ' & APH : ' +  CONVERT(NVARCHAR(15),ROUND(TBL3.QTY/TBL1.GRADERS,2))) AS GRADERS,TBL2.GRADE,TBL2.PRODUCT_TYPE,SUM(TBL2.QTY) AS QUANTITY,(SELECT ('Graders: ' + CASE WHEN T2.QTY=0 THEN '0' ELSE CONVERT(NVARCHAR(15),T1.GDRS) END + ' & APH:' +  CONVERT(NVARCHAR(15),ROUND(T2.QTY/T1.GDRS,2))) AS APH FROM (SELECT ISNULL(SUM(H.NO_OF_GRADERS),1) AS GDRS  FROM GPIL_GRADING_HDR H  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'01-01-2015 00:00:00',105) AND CONVERT(DATETIME,'01-01-2016 23:59:59',105)) AS T1,(SELECT ISNULL(SUM(D.MARKED_WT),0) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'01-01-2015 00:00:00',105) AND CONVERT(DATETIME,'01-01-2016 23:59:59',105) AND ((H.RECIPE_CODE IN ('HAND STEMMING','BUTTING') AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) OR (H.RECIPE_CODE NOT IN ('HAND STEMMING','BUTTING')))) AS T2) AS APH  FROM (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(H.DATE_OF_OPERATION) AS TO_DATE,SUM(H.NO_OF_GRADERS) AS GRADERS FROM GPIL_GRADING_HDR H  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND  H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) GROUP BY H.ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE) AS TBL1 LEFT OUTER JOIN (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,D.PRODUCT_TYPE,SUM(D.MARKED_WT) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) GROUP BY H.ISSUED_GRADE,D.GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,D.PRODUCT_TYPE) AS TBL2 ON TBL1.CROP=TBL2.CROP AND TBL1.VARIETY=TBL2.VARIETY AND TBL1.RECIPE_CODE=TBL2.RECIPE_CODE AND TBL1.ISSUED_GRADE=TBL2.ISSUED_GRADE LEFT OUTER JOIN (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,SUM(D.MARKED_WT) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105) AND ((H.RECIPE_CODE IN ('HAND STEMMING','BUTTING') AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) OR (H.RECIPE_CODE NOT IN ('HAND STEMMING','BUTTING'))) GROUP BY H.ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE) AS TBL3 ON TBL1.CROP=TBL3.CROP AND TBL1.VARIETY=TBL3.VARIETY AND TBL1.RECIPE_CODE=TBL3.RECIPE_CODE AND TBL1.ISSUED_GRADE=TBL3.ISSUED_GRADE LEFT OUTER JOIN (SELECT VARIETY,VARIETY_NAME FROM GPIL_VARIETY_MASTER WHERE VARIETY='" + ddlVariety.Text + "') AS V ON V.VARIETY=TBL1.VARIETY GROUP BY TBL1.CROP,TBL1.VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL1.FROM_DATE,TBL1.TO_DATE,TBL1.GRADERS,TBL2.GRADE,TBL2.PRODUCT_TYPE,V.VARIETY_NAME,TBL3.QTY ORDER BY TBL1.CROP,TBL1.VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL2.GRADE";
                    }
                }
                else
                {
                    sql = "SELECT TBL1.CROP,(TBL1.VARIETY + ' - ' + V.VARIETY_NAME) AS VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL1.FROM_DATE,TBL1.TO_DATE,('Graders : ' + CONVERT(NVARCHAR(15),TBL1.GRADERS) + ' & APH : ' +  CONVERT(NVARCHAR(15),ROUND(TBL3.QTY/TBL1.GRADERS,2))) AS GRADERS,TBL2.GRADE,TBL2.PRODUCT_TYPE,SUM(TBL2.QTY) AS QUANTITY,(SELECT ('Graders: ' + CASE WHEN T2.QTY=0 THEN '0' ELSE CONVERT(NVARCHAR(15),T1.GDRS) END + ' & APH:' +  CONVERT(NVARCHAR(15),ROUND(T2.QTY/T1.GDRS,2))) AS APH FROM (SELECT ISNULL(SUM(H.NO_OF_GRADERS),1) AS GDRS  FROM GPIL_GRADING_HDR H  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "'  AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' ) AS T1,(SELECT ISNULL(SUM(D.MARKED_WT),0) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "'  AND ((H.RECIPE_CODE IN ('HAND STEMMING','BUTTING') AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) OR (H.RECIPE_CODE NOT IN ('HAND STEMMING','BUTTING')))) AS T2) AS APH  FROM (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,MIN(H.DATE_OF_OPERATION) AS FROM_DATE,MAX(H.DATE_OF_OPERATION) AS TO_DATE,SUM(H.NO_OF_GRADERS) AS GRADERS FROM GPIL_GRADING_HDR H  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND  H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "'  GROUP BY H.ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE) AS TBL1 LEFT OUTER JOIN (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,D.PRODUCT_TYPE,SUM(D.MARKED_WT) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "'  GROUP BY H.ISSUED_GRADE,D.GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,D.PRODUCT_TYPE) AS TBL2 ON TBL1.CROP=TBL2.CROP AND TBL1.VARIETY=TBL2.VARIETY AND TBL1.RECIPE_CODE=TBL2.RECIPE_CODE AND TBL1.ISSUED_GRADE=TBL2.ISSUED_GRADE LEFT OUTER JOIN (SELECT SUBSTRING(H.ISSUED_GRADE,5,LEN(H.ISSUED_GRADE)) AS ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE,SUM(D.MARKED_WT) AS QTY  FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D  WHERE ISNULL(H.ATTRIBUTE3,'')<>'N' AND H.STATUS IN ('N','NN') AND H.BATCH_NO=D.BATCH_NO AND D.BALE_TYPE='OPB' AND H.VARIETY='" + ddlVariety.Text + "' AND H.CROP='" + ddlCropYear.Text + "' AND H.RECIPE_CODE='" + ddlOperationReceipe.Text + "' AND H.ORGN_CODE='" + ddlOrganizationCode.Text + "' AND  ((H.RECIPE_CODE IN ('HAND STEMMING','BUTTING') AND D.PRODUCT_TYPE NOT IN ('BP','LOSS')) OR (H.RECIPE_CODE NOT IN ('HAND STEMMING','BUTTING'))) GROUP BY H.ISSUED_GRADE,H.CROP,H.VARIETY,H.RECIPE_CODE) AS TBL3 ON TBL1.CROP=TBL3.CROP AND TBL1.VARIETY=TBL3.VARIETY AND TBL1.RECIPE_CODE=TBL3.RECIPE_CODE AND TBL1.ISSUED_GRADE=TBL3.ISSUED_GRADE LEFT OUTER JOIN (SELECT VARIETY,VARIETY_NAME FROM GPIL_VARIETY_MASTER WHERE VARIETY='" + ddlVariety.Text + "') AS V ON V.VARIETY=TBL1.VARIETY GROUP BY TBL1.CROP,TBL1.VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL1.FROM_DATE,TBL1.TO_DATE,TBL1.GRADERS,TBL2.GRADE,TBL2.PRODUCT_TYPE,V.VARIETY_NAME,TBL3.QTY ORDER BY TBL1.CROP,TBL1.VARIETY,TBL1.RECIPE_CODE,TBL1.ISSUED_GRADE,TBL2.GRADE";
                }


                DataSet ds = new DataSet();
                ds = ppdMgt.GetClassificationReport(sql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptGradingOperation.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("GradingOperationReport.aspx");
        }
    }
}