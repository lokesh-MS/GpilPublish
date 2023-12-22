using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.Models;
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
    public partial class LotWiseStock : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDropDown();
                BindTapOrganization();

            }
            else
            {
                //if (validated())
                //{
                //    viewrpt();
                //}
                if (ddlToOrgnCode.SelectedIndex != 0)
                {
                    viewrpt();
                }
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
        string strsql;
        GLTManagement GLTMgt = new GLTManagement();
        DataTable dt = new DataTable();
        public void viewrpt()
        {
            try
            {
              //  SqlConnection connection = new SqlConnection(ClsConnection.strConnectionString);
                if (rdbTapStock.Checked == true)
                {
                    strsql = "select S.GPIL_BALE_NUMBER,SUBSTRING(S.BUYER_GRADE,5,LEN(S.BUYER_GRADE)-4) AS GRADE,S.MARKED_WT,S.PRICE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN,S.CROP+' - '+C.CROP_YEAR AS CROP,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY from GPIL_STOCK S,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ORGN_MASTER O WHERE S.CURR_ORGN_CODE='" + ddlToOrgnCode.Text + "'  and S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND S.STATUS='Y' AND S.PROCESS_STATUS='N' AND S.CROP=C.CROP AND V.VARIETY=S.VARIETY  AND S.CURR_ORGN_CODE=O.ORGN_CODE ";
                }
                else
                {
                    strsql = "select S.GPIL_BALE_NUMBER,case when S.GRADE IS null then 'UN-CLASSIFIED' else case when SUBSTRING(S.GRADE,1,1)='L' then S.GRADE else SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end end  AS GRADE,S.MARKED_WT,S.PRICE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN,S.CROP+' - '+C.CROP_YEAR AS CROP,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY from GPIL_STOCK S,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ORGN_MASTER O WHERE S.CURR_ORGN_CODE='" + ddlToOrgnCode.Text + "'  and S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND S.STATUS='Y' AND S.PROCESS_STATUS='N' AND S.CROP=C.CROP AND V.VARIETY=S.VARIETY  AND S.CURR_ORGN_CODE=O.ORGN_CODE ";

                }


                //SqlCommand command = new SqlCommand(strsql, connection);
                //command.CommandTimeout = 0;
                //SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataset = new DataSet();
                dt = GLTMgt.GetQueryResult(strsql);
               
                //adapter.Fill(dataset, "byr");
                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptBalewiseSTK.rpt"));
                CustomerReport.SetDataSource(dt.DefaultView);
               // CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
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
        protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private GREEN_LEAF_TRACEABILITYEntities _context;
        public LotWiseStock()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected void rdbTapStock_CheckedChanged(object sender, EventArgs e)
        {
            BindTapOrganization();
        }
       
        
        private void BindTapOrganization()
        {
            try
            {

               
                DataSet ds3 = new DataSet();
                ds3 = crd.GetORGN("ORGNcode", "", "");
                ddlToOrgnCode.DataSource = ds3.Tables[0];
                ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                ddlToOrgnCode.DataValueField = "ORGN_CODE";
                ddlToOrgnCode.DataBind();
                ddlToOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void rdbOtherStock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                
                DataSet ds4 = new DataSet();
                ds4 = crd.GetORGN("ORGNCode1", "", "");
                ddlToOrgnCode.DataSource = ds4.Tables[0];
                ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                ddlToOrgnCode.DataValueField = "ORGN_CODE";
                ddlToOrgnCode.DataBind();
                ddlToOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            ddlCrop.SelectedIndex= 0;
            ddlVariety.SelectedIndex= 0;
            ddlToOrgnCode.SelectedIndex= 0;

        }
    }
}