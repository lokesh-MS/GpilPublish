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
                    viewrpt();
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
                ds = crd.GetORGN("ORGNcode", "", "");
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
            if (txt_GradingOutTurn_Date.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Date');", true);
                txt_GradingOutTurn_Date.Focus();
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
                           // viewoprpt();
                        }
                        else if (rdbOperationReport.Checked == true)
                        {
                           // viewoperationrpt();
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
                sql = "SELECT DISTINCT H.CROP,H.VARIETY,V.VARIETY_NAME, H.RECIPE_CODE, H.BATCH_NO, H.ORGN_CODE,H.DATE_OF_OPERATION,H.TOT_ISSUE_BALES,H.ISSUED_GRADE,H.TOT_ISSUE_QTY,D.GPIL_BALE_NUMBER,D.MARKED_WT,ISNULL(I.ATTRIBUTE4,'')HsnCode FROM GPIL_GRADING_HDR H,GPIL_GRADING_DTLS D,GPIL_VARIETY_MASTER v,GPIL_ITEM_MASTER I  WHERE D.BALE_TYPE='IPB' and H.BATCH_NO='" + ddlBatchNo + "' AND D.BATCH_NO=H.BATCH_NO  and v.VARIETY=H.VARIETY AND H.ISSUED_GRADE=I.ITEM_CODE ORDER BY D.GPIL_BALE_NUMBER";
               
                ds = ppdMgt.GetQueryResultDs(sql);
                
             
             if(ds.Tables[0].Rows.Count >0 )
                {
                    ReportDocument CustomerReport = new ReportDocument();
                    CustomerReport.Load(Server.MapPath("~/Reports/rptGradingOperationIssue.rpt"));
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












        protected void btnclose_Click(object sender, EventArgs e)
        {

        }
    }
}