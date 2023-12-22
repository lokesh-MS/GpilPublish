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
    public partial class GradingIssueOutTurnSheet : System.Web.UI.Page
    {

        CrystalReportData crd = new CrystalReportData();
        PPDManagement ppdMgt = new PPDManagement();
        ReportManagement rptMgt = new ReportManagement();
        DataTable dt = new DataTable();
        int FLAGREF;
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
                    
                        if (rdbIssuedReport.Checked == true)
                        {
                            viewissuerpt();
                        }
                        else if (rdbOutTurnReport.Checked == true)
                        {
                            viewoprpt();
                        }
                        else if (rdbOperationReport.Checked == true)
                        {
                            viewoperationrpt();
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
                ddlLocationCode.DataSource = ds.Tables[0];
                ddlLocationCode.DataTextField = "ORGN_CODE1";
                ddlLocationCode.DataValueField = "ORGN_CODE";
                ddlLocationCode.DataBind();
                ddlLocationCode.Items.Insert(0, new ListItem("- Select -", "0"));
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
        protected void btnview_Click(object sender, EventArgs e)
        {
            viewrpt();
        }
        bool validate()
        {
            if (txtFromDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Date');", true);
                txtFromDate.Focus();
                return false;
            }
            else if (ddlLocationCode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Location Code');", true);
                ddlLocationCode.Focus();
                return false;
            }
            else if (ddlCropYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Crop Year');", true);
                ddlCropYear.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Variety');", true);
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
            try
            {
                if (validate())
                {
                    if (ddlBatchNo.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Batch Number');", true);
                        ddlBatchNo.Focus();
                    }
                    else
                    {
                        if (rdbIssuedReport.Checked == true)
                        {
                            viewissuerpt();
                        }
                        else if (rdbOutTurnReport.Checked == true)
                        {
                            viewoprpt();
                        }
                        else if (rdbOperationReport.Checked == true)
                        {
                            viewoperationrpt();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        string sql;
        DataSet ds = new DataSet();
        public void viewissuerpt()
        {
            try
            {
                sql = "SELECT DISTINCT H.CROP,H.VARIETY,V.VARIETY_NAME, H.RECIPE_CODE, H.BATCH_NO, H.ORGN_CODE,H.DATE_OF_OPERATION,H.TOT_ISSUE_BALES,H.ISSUED_GRADE,H.TOT_ISSUE_QTY,D.GPIL_BALE_NUMBER,D.MARKED_WT,ISNULL(I.ATTRIBUTE4,'')HsnCode FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D,GPIL_VARIETY_MASTER v,GPIL_ITEM_MASTER I  WHERE D.BALE_TYPE='IPB' and H.BATCH_NO='" + ddlBatchNo.Text + "' AND D.BATCH_NO=H.BATCH_NO  and v.VARIETY=H.VARIETY AND H.ISSUED_GRADE=I.ITEM_CODE ORDER BY D.GPIL_BALE_NUMBER";

                ds = ppdMgt.GetQueryResultDs(sql);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptGradingOperationIssue.rpt"));
                    // CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                    GradingReport.ReportSource = CustomerReport;
                    GradingReport.DataBind();
                    FLAGREF = 1;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('No Data Found');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public void viewoprpt()
        {
            try
            {
                sql = "SELECT DISTINCT H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE, H.BATCH_NO,H.ORGN_CODE,convert(nvarchar(10),H.DATE_OF_OPERATION,105) as DATEOFOPER,d.MARKED_WT,D.GRADE,D.GPIL_BALE_NUMBER,ISNULL(I.ATTRIBUTE4,'')HsnCode FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D,GPIL_VARIETY_MASTER v,GPIL_ITEM_MASTER I WHERE D.BALE_TYPE='OPB'  AND H.BATCH_NO='" + ddlBatchNo.Text + "' AND D.BATCH_NO=H.BATCH_NO and v.VARIETY=H.VARIETY AND D.GRADE=I.ITEM_CODE ORDER BY D.GPIL_BALE_NUMBER";
                ds = ppdMgt.GetQueryResultDs(sql);
                //dt = new DataTable();
                //dt.Load(dr);
                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/CrystalReport/rptGradingOperationoutturn.rpt"));
                //rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                rd.SetDataSource(ds.Tables[0].DefaultView);

                GradingReport.ReportSource = rd;
                GradingReport.DataBind();
                FLAGREF = 2;


                //CrystalReportViewer1.ReportSource = rd;
                //CrystalReportViewer1.RefreshReport();
                //FLAGREF = 2;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }




        public void viewoperationrpt()
        {
            try
            {
                sql = "SELECT H.CROP,H.VARIETY,V.VARIETY_NAME, H.RECIPE_CODE,H.ISSUED_GRADE, H.BATCH_NO,H.ORGN_CODE,convert(nvarchar(10),H.DATE_OF_OPERATION,105) as DATEOFOPER,SUM(d.MARKED_WT) AS QTY,SUBSTRING(D.GRADE,5,LEN(D.GRADE)) AS GRADE,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.TOT_ISSUE_QTY,H.TOT_ISSUE_BALES,COUNT(D.GPIL_BALE_NUMBER) AS BALESCOUNT,ISNULL(I.ATTRIBUTE4,'')HsnCode,ISNULL(I1.ATTRIBUTE4,'')Pro_HsnCode";
                sql = sql + " FROM GPIL_GRADING_HDR(NOLOCK) H,GPIL_GRADING_DTLS(NOLOCK) D,GPIL_VARIETY_MASTER(NOLOCK) v,GPIL_ITEM_MASTER(NOLOCK) I,GPIL_ITEM_MASTER(NOLOCK) I1";
                sql = sql + " WHERE H.BATCH_NO='" + ddlBatchNo.Text + "' and D.BALE_TYPE='OPB' AND D.BATCH_NO=H.BATCH_NO and v.VARIETY=H.VARIETY AND H.ISSUED_GRADE=I.ITEM_CODE AND I1.ITEM_CODE=D.GRADE";
                sql = sql + " GROUP BY H.CROP,H.VARIETY,V.VARIETY_NAME,H.RECIPE_CODE,H.ISSUED_GRADE,H.AVG_BALES_GRADER,H.NO_OF_GRADERS ,H.TOT_ISSUE_QTY,H.TOT_ISSUE_BALES, H.BATCH_NO,H.ORGN_CODE,D.GRADE,convert(nvarchar(10),H.DATE_OF_OPERATION,105),H.ISSUED_GRADE,I.ATTRIBUTE4,I1.ATTRIBUTE4 ORDER BY SUM(d.MARKED_WT) DESC ";
                ds = ppdMgt.GetQueryResultDs(sql);

                // dr = SqlHelper.ExecuteReader(ClsConnection.SqlCon, CommandType.Text, sql);
                //dt = new DataTable();
                //dt.Load(dr);
                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/CrystalReport/rptGradingDailyOperation.rpt"));
                rd.SetDataSource(ds.Tables[0].DefaultView);

                GradingReport.ReportSource = rd;
                GradingReport.DataBind();
                FLAGREF = 2;

                ////rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                //rd.SetDataSource(dt);
                //CrystalReportViewer1.ReportSource = rd;
                //CrystalReportViewer1.RefreshReport();
                //FLAGREF = 2;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }










        protected void btnclose_Click(object sender, EventArgs e)
        {
            //txtFromDate.Text = "";
            //ddlLocationCode.SelectedIndex = 0;
            //ddlCropYear.SelectedIndex = 0;
            //ddlVariety.SelectedIndex = 0;
            //ddlBatchNo.SelectedIndex = 0;

            Response.Redirect("GradingIssueOutTurnSheet.aspx");
        }

        protected void ddlVariety_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlBatchNo.DataSource = rptMgt.GetBatchNumber(ddlCropYear.SelectedItem.Value, ddlVariety.SelectedItem.Value, ddlLocationCode.SelectedItem.Value, txtFromDate.Text);
                ddlBatchNo.DataTextField = "BATCH_NO";
                ddlBatchNo.DataValueField = "BATCH_NO";
                ddlBatchNo.DataBind();
                ddlBatchNo.Items.Insert(0, "< -- Select -- >");

            }
            catch (Exception ex)
            {

            }
        }
    }
}
