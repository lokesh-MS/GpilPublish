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

namespace GPILWebApp.CrystalReport.WebForms
{
    public partial class IntransitReport : System.Web.UI.Page
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
        DataTable dt = new DataTable();
        public void viewrpt()
        {
            try
            {
               // SqlConnection connection = new SqlConnection(ClsConnection.strConnectionString);
                if (rdbGenereal.Checked == true)
                {
                    
                    strsql = "SELECT SENDER_ORGN_CODE,RECEIVER_ORGN_CODE,SUM(S.MARKED_WT) AS WT,case when D.GRADE IS null then 'UN-CLASSIFIED' ELSE D.GRADE END AS GRADE,SUM(S.PRICE*S.MARKED_WT) AS VALUE,round(SUM(S.PRICE*S.MARKED_WT) /SUM(S.MARKED_WT),2) AS AVGPR  ,V.VARIETY+' -' +V.VARIETY_NAME AS VRTY,C.CROP+' - '+C.CROP_YEAR AS CROP FROM GPIL_SHIPMENT_HDR H,GPIL_SHIPMENT_DTLS D,GPIL_STOCK S,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V ";
                    strsql = strsql + " WHERE S.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.SHIPMENT_NO=D.SHIPMENT_NO AND H.STATUS='INT' AND RECEIVER_ORGN_CODE='" + ddlToOrgnCode.Text + "' AND C.CROP=SUBSTRING(D.GPIL_BALE_NUMBER,1,2) AND V.VARIETY=SUBSTRING(D.GPIL_BALE_NUMBER,3,2) AND SUBSTRING(D.GPIL_BALE_NUMBER,1,2)='" + ddlCrop.Text + "' AND SUBSTRING(D.GPIL_BALE_NUMBER,3,2)='" + ddlVariety.Text + "' GROUP BY SENDER_ORGN_CODE,D.GRADE,RECEIVER_ORGN_CODE,V.VARIETY+' -' +V.VARIETY_NAME,C.CROP+' - '+C.CROP_YEAR";

                    //SqlCommand command = new SqlCommand(strsql, connection);
                    //command.CommandTimeout = 0;
                    //SqlDataAdapter adapter = new SqlDataAdapter(command);

                    dt = GLTMgt.GetQueryResult(strsql);
                    ReportDocument CustomerReport = new ReportDocument();
                    DataSet ds = new DataSet();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptIntransitReport.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);
                    //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
                else
                {
                    strsql = "SELECT SENDER_ORGN_CODE,ROUND(SUM(MARKED_WT),2) as qty ,RECEIVER_ORGN_CODE,V.VARIETY+' -' +V.VARIETY_NAME AS VRTY,C.CROP+' - '+C.CROP_YEAR AS CROP FROM GPIL_SHIPMENT_HDR H,GPIL_SHIPMENT_DTLS D,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V  ";
                    strsql = strsql + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND RECEIVER_ORGN_CODE='" + ddlToOrgnCode.Text + "' AND H.STATUS='INT' AND C.CROP=SUBSTRING(D.GPIL_BALE_NUMBER,1,2) AND V.VARIETY=SUBSTRING(D.GPIL_BALE_NUMBER,3,2) AND SUBSTRING(D.GPIL_BALE_NUMBER,1,2)='" + ddlCrop.Text + "' AND SUBSTRING(D.GPIL_BALE_NUMBER,3,2)='" + ddlVariety.Text + "' GROUP BY SENDER_ORGN_CODE,RECEIVER_ORGN_CODE,V.VARIETY+' -' +V.VARIETY_NAME ,C.CROP+' - '+C.CROP_YEAR";
                    //SqlCommand command = new SqlCommand(strsql, connection);
                    //command.CommandTimeout = 0;
                    //SqlDataAdapter adapter = new SqlDataAdapter(command);
                    dt = GLTMgt.GetQueryResult(strsql);
                    ReportDocument CustomerReport = new ReportDocument();
                    DataSet dataset = new DataSet();
                  //  adapter.Fill(dataset, "byr");
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/rptIntransitsmry.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);
                   // CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
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

        }
    }
}