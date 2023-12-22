using CrystalDecisions.CrystalReports.Engine;
using GPI;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class GradeWiseStockAllReport : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        string strsql;
        GLTManagement GLTMgt = new GLTManagement();
        DataTable dt = new DataTable();
        CommonManagement cMgt = new CommonManagement();

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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {          

                if (!IsPostBack)
                {

                  string strsql = "Select ORGN_CODE,ORGN_CODE + ' - ' + ORGN_NAME as ORGN_CODE1 From GPIL_ORGN_MASTER where ORGN_TYPE = 'TAP'";
                    DataSet ds = new DataSet();
                    ds = cMgt.GetdsQueryResult(strsql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlToOrgnCode.DataSource = ds;
                        ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                        ddlToOrgnCode.DataValueField = "ORGN_CODE";
                        ddlToOrgnCode.DataBind();

                    }
                    ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");
                }
                else
                {
                    viewrpt();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
               bindDropDown(); 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
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
            if (ddlCrop.SelectedIndex == 0 && ddlVariety.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                ddlCrop.Focus();
                return false;
            }
            else if (ddlVariety.SelectedIndex == 0 && ddlCrop.SelectedIndex != 0)
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
            try
            {
               
                if (ddlToOrgnCode.SelectedIndex == 0)
                {
                    if (rdbtapstk.Checked == true)
                    {
                        if (ddlCrop.SelectedIndex != 0)
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,SUBSTRING(S.BUYER_GRADE,5,LEN(S.BUYER_GRADE)) AS BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME,SUM(D.VALUE) AS RATE,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVGPRS";
                            strsql = strsql + " from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_TAP_FARM_PURCHS_DTLS D where S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE='TAP' AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP AND D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER";
                            strsql = strsql + " GROUP BY S.BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME ,S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR";
                        }
                        else
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,SUBSTRING(S.BUYER_GRADE,5,LEN(S.BUYER_GRADE)) AS BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME,SUM(D.VALUE) AS RATE,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVGPRS";
                            strsql = strsql + " from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_TAP_FARM_PURCHS_DTLS D where S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE='TAP' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP AND D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER";
                            strsql = strsql + " GROUP BY S.BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME ,S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR";
                        }
                    }
                    else
                    {
                        //strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,case when S.GRADE IS null then 'UN-Classified' else SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end as GRADE, S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V";
                        //strsql = strsql + " where S.STATUS='Y'  AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE!='TAP' AND  S.CROP='"+cbxcrop.Text+"' AND S.VARIETY='"+cbxvariety.Text+"' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP GROUP BY S.GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME , S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR";

                        if (ddlCrop.SelectedIndex != 0)
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,case when S.GRADE IS null then 'UN-Classified' else case when SUBSTRING(S.GRADE,1,1)='L' then S.GRADE + ' (' + I.ITEM_DESC + ')' else SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end end as GRADE, S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I where I.ITEM_CODE=S.GRADE AND ";
                            strsql = strsql + " S.STATUS='Y'  AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE!='TAP' AND  S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP GROUP BY S.GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME , S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR,I.ITEM_DESC";
                        }
                        else
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,case when S.GRADE IS null then 'UN-Classified' else case when SUBSTRING(S.GRADE,1,1)='L' then S.GRADE + ' (' + I.ITEM_DESC + ')' else SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end end as GRADE, S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I where I.ITEM_CODE=S.GRADE AND ";
                            strsql = strsql + " S.STATUS='Y'  AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE!='TAP' AND  V.VARIETY=S.VARIETY AND C.CROP=S.CROP GROUP BY S.GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME , S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR,I.ITEM_DESC";
                        }
                    }

                    //strsql = "SELECT D.BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(AVG(D.RATE),2) as avgpr,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'"+txt_Report_Date.Text+"' AS FROMDATE,'"+txttodate.Text+"' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txt_Report_Date.Text) + "','12:00:00 AM','00:00:00 AM'),105) and CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txttodate.Text) + "','12:00:00 AM','23:59:59 PM'),105) group by D.BUYER_GRADE,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME ";
                }
                else
                {
                    if (rdbtapstk.Checked == true)
                    {

                        if (ddlCrop.SelectedIndex != 0)
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,SUBSTRING(S.BUYER_GRADE,5,LEN(S.BUYER_GRADE)) AS BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME,SUM(D.VALUE) AS RATE,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVGPRS";
                            strsql = strsql + " from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_TAP_FARM_PURCHS_DTLS D where S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE='TAP' AND S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP AND D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER  AND S.CURR_ORGN_CODE='" + ddlToOrgnCode.Text + "' ";
                            strsql = strsql + " GROUP BY S.BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME ,S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR";
                        }
                        else
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,SUBSTRING(S.BUYER_GRADE,5,LEN(S.BUYER_GRADE)) AS BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME,SUM(D.VALUE) AS RATE,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVGPRS";
                            strsql = strsql + " from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_TAP_FARM_PURCHS_DTLS D where S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE='TAP' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP AND D.GPIL_BALE_NUMBER=S.GPIL_BALE_NUMBER  AND S.CURR_ORGN_CODE='" + ddlToOrgnCode.Text + "' ";
                            strsql = strsql + " GROUP BY S.BUYER_GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME ,S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR";
                        }
                    }
                    else
                    {
                        //strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,case when S.GRADE IS null then 'UN-Classified' else SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end as GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V";
                        //strsql = strsql + " where S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE!='TAP' AND  S.CROP='"+cbxcrop.Text+"' AND S.VARIETY='"+cbxvariety.Text+"' AND S.CURR_ORGN_CODE='"+cbxorgcd.Text+"' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP GROUP BY S.GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME , S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR";

                        if (ddlCrop.SelectedIndex != 0)
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,case when S.GRADE IS null then 'UN-Classified' else case when SUBSTRING(S.GRADE,1,1)='L' then S.GRADE + ' (' + I.ITEM_DESC + ')' else SUBSTRING(s.grade,1,2) + SUBSTRING(S.VARIETY,1,2) + '-' +  SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end end as GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I where I.ITEM_CODE=S.GRADE AND ";
                            strsql = strsql + "  S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE!='TAP' AND  S.CROP='" + ddlCrop.Text + "' AND S.VARIETY='" + ddlVariety.Text + "' AND S.CURR_ORGN_CODE='" + ddlToOrgnCode.Text + "' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP GROUP BY S.GRADE,S.variety,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME , S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR,I.ITEM_DESC";
                        }
                        else
                        {
                            strsql = "select COUNT(*) AS BUNDLES,SUM(S.MARKED_WT) AS QUANTITY,case when S.GRADE IS null then 'UN-Classified' else case when SUBSTRING(S.GRADE,1,1)='L' then S.GRADE + ' (' + I.ITEM_DESC + ')' else SUBSTRING(s.grade,1,2) + SUBSTRING(S.VARIETY,1,2) + '-' + SUBSTRING(S.GRADE,5,LEN(S.GRADE)) end end as GRADE,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME, S.CROP+' - '+C.CROP_YEAR AS CROP_YEAR,S.VARIETY+' - '+V.VARIETY_NAME AS VARIETY_NAME from GPIL_STOCK S,GPIL_ORGN_MASTER O,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V,GPIL_ITEM_MASTER I where I.ITEM_CODE=S.GRADE AND ";
                            strsql = strsql + "  S.STATUS='Y' AND O.ORGN_CODE=S.CURR_ORGN_CODE AND O.ORGN_TYPE!='TAP' AND  S.CURR_ORGN_CODE='" + ddlToOrgnCode.Text + "' AND V.VARIETY=S.VARIETY AND C.CROP=S.CROP GROUP BY S.GRADE,s.variety,S.CURR_ORGN_CODE+' - '+O.ORGN_NAME , S.VARIETY+' - '+V.VARIETY_NAME, S.CROP+' - '+C.CROP_YEAR,I.ITEM_DESC";
                        }

                    }

                }


                GLTManagement gMgt = new GLTManagement();

                dt = gMgt.GetQueryResult(strsql);




                if (dt.Rows.Count > 0)
                {


                    if (rdbtapstk.Checked == true)
                    {


                        CustomerReport.Load(Server.MapPath("~/CrystalReport/rptGradeWiseSTK.rpt"));
                        CustomerReport.SetDataSource(dt.DefaultView);
                        CrystalReportViewer1.ReportSource = CustomerReport;
                        CrystalReportViewer1.DataBind();
                    }
                    else
                    {


                        CustomerReport.Load(Server.MapPath("~/CrystalReport/rptGradeWiseSTKCLS.rpt"));
                        CustomerReport.SetDataSource(dt.DefaultView);
                        CrystalReportViewer1.ReportSource = CustomerReport;
                        CrystalReportViewer1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("GradeWiseStockAllReport.aspx");

        }

        protected void rdbTapStock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                string strsql = "Select ORGN_CODE,ORGN_CODE + ' - ' + ORGN_NAME as ORGN_CODE1 From GPIL_ORGN_MASTER where ORGN_TYPE = 'TAP'";
                DataSet ds = new DataSet();
                ds = cMgt.GetdsQueryResult(strsql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlToOrgnCode.DataSource = ds;
                    ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                    ddlToOrgnCode.DataValueField = "ORGN_CODE";
                    ddlToOrgnCode.DataBind();

                }
                ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");
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
                string strsql = "Select ORGN_CODE,ORGN_CODE + ' - ' + ORGN_NAME as ORGN_CODE1 From GPIL_ORGN_MASTER where ORGN_TYPE != 'TAP'";
                DataSet ds = new DataSet();
                ds = cMgt.GetdsQueryResult(strsql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlToOrgnCode.DataSource = ds;
                    ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                    ddlToOrgnCode.DataValueField = "ORGN_CODE";
                    ddlToOrgnCode.DataBind();

                }
                ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {

            if (CustomerReport != null)
            {
                CustomerReport.Close();
                CustomerReport.Dispose();
            }
        }
    }
}