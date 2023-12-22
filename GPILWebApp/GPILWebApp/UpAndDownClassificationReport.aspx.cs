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
    public partial class UpAndDownClassificationReport : System.Web.UI.Page
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
                    if (ddlCropYear.SelectedIndex != 0 || ddlVariety.SelectedIndex != 0)
                    {
                        FuncVoidReportView();
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
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                FuncVoidReportView();
            }

        }

        public bool validated()
        {
            if ((txtFromDate.Text == "<--Select-->" || txtFromDate.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if ((txtToDate.Text == "<--Select-->" || txtFromDate.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txtToDate.Focus();
                return false;
            }
            else if (ddlCropYear.SelectedIndex == 0)
            {
                lblMessage.Text = "Please Select Crop..!";
                ddlCropYear.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0)
            {
                lblMessage.Text = "Please Select Variety..!";
                ddlVariety.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }


        public void FuncVoidReportView()
        {
            string strsql = "";
            string frmDate;
            string toDate;
            try
            {
                DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                frmDate = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txtToDate.Text);
                toDate = dt1.ToString("dd-MM-yyyy");
                



                if (ddlCropYear.SelectedIndex > 0 && ddlVariety.SelectedIndex > 0)
                {
                    //CROP & VARIETY
                    // T3.ATTRIBUTE1='" + cbxcrop.Text + "' AND T3.ATTRIBUTE2='" + cbxvariety.Text + "' AND 

                    strsql = "SELECT TBL1.ORIGN_ORGN_CODE AS ORGN_CODE,ROUND(TBL4.TOT_QTY/1000,2) AS TOT_QTY,ROUND(TBL4.TOT_AMT/100000,2) AS TOT_AMT,TBL4.TOT_AVE AS TOT_AVE , ROUND(TBL1.EQUAL_QTY/1000,2) AS EQUAL_QTY,ROUND(TBL2.EQUAL_AMT/100000,2) AS EQUAL_AMT,TBL3.EQUAL_AVE AS EQUAL_AVE,ROUND((TBL1.EQUAL_QTY/TBL4.TOT_QTY) * 100,2) AS EQUAL_PER ,ROUND(TBL1.UP_QTY/1000,2) AS UP_QTY,ROUND(TBL2.UP_AMT/100000,2) AS UP_AMT,TBL3.UP_AVE AS UP_AVE,ROUND((TBL1.UP_QTY/TBL4.TOT_QTY) * 100,2) AS UP_PER,ROUND(TBL1.DOWN_QTY/1000,2) AS DOWN_QTY,ROUND(TBL2.DOWN_AMT/100000,2) AS DOWN_AMT,TBL3.DOWN_AVE AS DOWN_AVE,ROUND((TBL1.DOWN_QTY/TBL4.TOT_QTY) * 100,2) AS DOWN_PER,'" + ddlCropYear.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY,'' AS BUYER FROM " +
                                "(SELECT T10.ORIGN_ORGN_CODE,ISNULL(SUM(T10.UP),0) AS UP_QTY,ISNULL(SUM(T10.EQUAL),0) AS EQUAL_QTY,ISNULL(SUM(T10.DOWN),0) AS DOWN_QTY FROM " +
                                "(SELECT S.ORIGN_ORGN_CODE, (CASE WHEN T3.PAIR_TYPE = 'E' THEN  SUM(S.MARKED_WT) END) AS EQUAL,(CASE WHEN T3.PAIR_TYPE = 'U' THEN  SUM(S.MARKED_WT) END) AS UP,(CASE WHEN T3.PAIR_TYPE = 'D' THEN  SUM(S.MARKED_WT) END) AS DOWN FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND  ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE,T3.PAIR_TYPE ) T10 GROUP BY T10.ORIGN_ORGN_CODE) TBL1, " +
                                "(SELECT T10.ORIGN_ORGN_CODE,ISNULL(SUM(T10.UP),0) AS UP_AMT,ISNULL(SUM(T10.EQUAL),0) AS EQUAL_AMT,ISNULL(SUM(T10.DOWN),0) AS DOWN_AMT FROM " +
                                "(SELECT S.ORIGN_ORGN_CODE, (CASE WHEN T3.PAIR_TYPE = 'E' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE),2) END) AS EQUAL,(CASE WHEN T3.PAIR_TYPE = 'U' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE),2) END) AS UP,(CASE WHEN T3.PAIR_TYPE = 'D' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE),2) END) AS DOWN FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE,T3.PAIR_TYPE ) T10 GROUP BY T10.ORIGN_ORGN_CODE) TBL2, " +
                                "(SELECT T10.ORIGN_ORGN_CODE,ISNULL(SUM(T10.UP),0) AS UP_AVE,ISNULL(SUM(T10.EQUAL),0) AS EQUAL_AVE,ISNULL(SUM(T10.DOWN),0) AS DOWN_AVE FROM " +
                                "(SELECT S.ORIGN_ORGN_CODE, (CASE WHEN T3.PAIR_TYPE = 'E' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),2) END) AS EQUAL,(CASE WHEN T3.PAIR_TYPE = 'U' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),2) END) AS UP,(CASE WHEN T3.PAIR_TYPE = 'D' THEN  ROUND(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),2) END) AS DOWN FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE,T3.PAIR_TYPE ) T10 GROUP BY T10.ORIGN_ORGN_CODE) TBL3, " +
                                "(SELECT S.ORIGN_ORGN_CODE, ROUND(ISNULL(SUM(S.MARKED_WT),0),2) AS TOT_QTY,ROUND(ISNULL(SUM(S.MARKED_WT * S.PRICE),0),2) AS TOT_AMT,ROUND(ISNULL(SUM(S.MARKED_WT * S.PRICE)/SUM(S.MARKED_WT),0),2) AS TOT_AVE FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE GPIL_BALE_NUMBER LIKE '" + ddlCropYear.Text + ddlVariety.Text + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0' AND CLASSIFICATION_DATE BETWEEN CONVERT(DATETIME,'" + frmDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + toDate + " 23:59:59',105)   AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2,GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) T3,GPIL_STOCK(NOLOCK) S WHERE  T3.ATTRIBUTE1=S.CROP AND T3.ATTRIBUTE2=S.VARIETY AND  T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=T1.GPIL_BALE_NUMBER " +
                                "AND T3.BUYER_GRADE_GRP=T1.ISSUED AND T3.CLASSIFIER_GRADE_GRP=T2.CLASSIFIED  " +
                                "GROUP BY S.ORIGN_ORGN_CODE) TBL4 " +
                                "WHERE TBL1.ORIGN_ORGN_CODE=TBL2.ORIGN_ORGN_CODE AND TBL2.ORIGN_ORGN_CODE=TBL3.ORIGN_ORGN_CODE AND TBL3.ORIGN_ORGN_CODE=TBL4.ORIGN_ORGN_CODE  ORDER BY TBL1.ORIGN_ORGN_CODE ";
                }


                DataSet ds = new DataSet();
                ds = ppdMgt.GetClassificationReport(strsql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptUpandDownClassificationReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();


                //SqlCommand command = new SqlCommand(strsql, connection);
                //command.CommandTimeout = 0;
                //SqlDataAdapter adapter = new SqlDataAdapter(command);
                ////customer _customer = new customer();
                //DataTable dtResult = new DataTable("byr");
                //adapter.Fill(dtResult);
                //ClsConnection.dtMain = dtResult;

                //CustomerReport.Load(Server.MapPath("~/Reports/RptUpandDownClassificationReport.rpt"));
                //CustomerReport.SetDataSource(ClsConnection.dtMain);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                //CrystalReportViewer1.ReportSource = CustomerReport;
                //CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
               // ClsConnection.closeDB();
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}