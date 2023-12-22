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
    public partial class ClassificationReport : System.Web.UI.Page
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
        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                viewrpt();
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



        public void viewrpt()
        {
            String strsql = "";
            try
            {
                DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                txtFromDate.Text = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txtToDate.Text);
                txtToDate.Text = dt1.ToString("dd-MM-yyyy");


                if (ddlOrganization.SelectedIndex == 0)
                {
                    strsql = "SELECT SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE)) as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as avgpr,O.ORGN_CODE+' - '+ O.ORGN_NAME AS ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59',105) AND HC.RECIPE_CODE='CLASSIFICATION' group by O.ORGN_CODE+' - '+ O.ORGN_NAME ,H.CROP,H.VARIETY,V.VARIETY_NAME,SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE))";
                    //strsql = "SELECT C.CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(AVG(D.RATE),2) as avgpr,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C where O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txt_Report_Date.Text) + "','12:00:00 AM','00:00:00 AM'),105) and CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txttodate.Text) + "','12:00:00 AM','23:59:59 PM'),105) group by O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,C.CLASSIFICATION_GRADE";
                }
                else
                {
                    strsql = "SELECT SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE)) as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(AVG(D.RATE),2) as avgpr,O.ORGN_CODE+' - '+ O.ORGN_NAME AS ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txtFromDate.Text + "' AS FROMDATE,'" + txtToDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO  AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and HC.CLASSIFICATION_DATE  between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59',105) and HC.ORGN_CODE='" + ddlOrganization.Text + "' AND HC.RECIPE_CODE='CLASSIFICATION'  group by O.ORGN_CODE+' - '+ O.ORGN_NAME ,H.CROP,H.VARIETY,V.VARIETY_NAME,SUBSTRING(C.CLASSIFICATION_GRADE,5,len(C.CLASSIFICATION_GRADE))";

                }


                DataSet ds = new DataSet();
                ds = ppdMgt.GetClassificationReport(strsql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptClassificationReportforTAP.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();


                //SqlCommand command = new SqlCommand(strsql, connection);
                //command.CommandTimeout = 0;
                //SqlDataAdapter adapter = new SqlDataAdapter(command);
                ////Customer _Customer = new Customer();
                //DataSet dataset = new DataSet();
                //adapter.Fill(dataset, "byr");

                //CustomerReport.Load(Server.MapPath("~/Reports/rptClassificationReportforTAP.rpt"));
                //CustomerReport.SetDataSource(dataset.Tables["byr"]);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                //CrystalReportViewer1.ReportSource = CustomerReport;
                //CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}