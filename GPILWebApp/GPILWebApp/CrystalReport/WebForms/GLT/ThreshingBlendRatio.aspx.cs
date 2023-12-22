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

namespace GPILWebApp.CrystalReport.WebForms.GLT
{
    public partial class ThreshingBlendRatio : System.Web.UI.Page
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
            else
            {
                viewrpt();
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

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncGetItemList();
        }

        protected void ddlVariety_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncGetItemList();
        }
        GLTManagement GLTMgt = new GLTManagement();
       
        public void FuncGetItemList()
        {
            try
            {
                DateTime dt1 = Convert.ToDateTime(txt_From_Date.Text);
                txt_From_Date.Text = dt1.ToString("yyyy-MM-dd");
                DateTime dt3 = Convert.ToDateTime(txt_To_Date.Text);
                txt_To_Date.Text = dt3.ToString("yyyy-MM-dd");
                DataTable dt = new DataTable();
                string sql = "SELECT ITEM_DESC FROM GPIL_ITEM_MASTER WHERE ITEM_CODE IN (SELECT DISTINCT PACKED_GRADE  FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H WHERE H.BATCH_NO =D.BATCH_NO AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',103) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',103) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "') ORDER BY ITEM_DESC";
              

                dt = GLTMgt.GetQueryResult(sql);
                

                    ddlPackedGrade.DataSource = dt;
                    ddlPackedGrade.DataTextField = "ITEM_DESC";
                    ddlPackedGrade.DataValueField = "ITEM_DESC";
                    ddlPackedGrade.DataBind();
                    ddlPackedGrade.Items.Insert(0, "<--Select-->");
                
                //strsr.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }

        }
        protected void ddlPackedGrade_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            if ((txt_From_Date.Text == "<--Select-->") || (txt_From_Date.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_From_Date.Focus();
                return false;
            }
            else if ((txt_To_Date.Text == "<--Select-->") || (txt_To_Date.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txt_To_Date.Focus();
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
        string strsql;
        public static string gradouterror = string.Empty;
        public static string errfile = string.Empty;
        public void viewrpt()
        {
            DateTime dt1 = Convert.ToDateTime(txt_From_Date.Text);
            txt_From_Date.Text = dt1.ToString("yyyy-MM-dd");
            DateTime dt3 = Convert.ToDateTime(txt_To_Date.Text);
            txt_To_Date.Text = dt3.ToString("yyyy-MM-dd");
            try
            {
                
                CrystalReportViewer1.ReportSource = null;
                if (rdbIssueBlendRatio.Checked == true)
                {
                   // strsql = "SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_DTLS_1 D1 WHERE D1.BATCH_NO IN (SELECT D.BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(VARCHAR,'" + txt_To_Date.Text + " 23:59:59',105) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND D.PACKED_GRADE IN (SELECT ITEM_CODE FROM GPIL_ITEM_MASTER WHERE ITEM_DESC='" + ddlPackedGrade.Text + "')) AND D1.BALE_TYPE='IPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY D1.BATCH_NO,D1.GRADE";
                    strsql = "SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,'INCREDIENT' AS BALE_TYPE FROM GPIL_THRESH_RECON_DTLS_1 D1 WHERE D1.BATCH_NO IN (SELECT D.BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',103) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',103) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND D.PACKED_GRADE IN (SELECT ITEM_CODE FROM GPIL_ITEM_MASTER WHERE ITEM_DESC='" + ddlPackedGrade.Text + "')) AND D1.BALE_TYPE='IPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY D1.BATCH_NO,D1.GRADE";
                }
                else
                {
                    //strsql = "SELECT D.BATCH_NO,I.ITEM_DESC + ' (' + D.PACKED_GRADE + ')' AS GRADE,ROUND(SUM(NET_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,'PRODUCT' AS BALE_TYPE  FROM GPIL_THRESH_RECON_DTLS_2 D, GPIL_THRESH_RECON_HDR H,GPIL_ITEM_MASTER I WHERE H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(VARCHAR,'" + txt_To_Date.Text + " 23:59:59',105) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND I.ITEM_DESC='" + ddlPackedGrade.Text + "' AND H.BATCH_NO=D.BATCH_NO AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND PACKED_GRADE=I.ITEM_CODE GROUP BY D.BATCH_NO,I.ITEM_DESC,D.PACKED_GRADE UNION SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,'BY-PRODUCT' AS BALE_TYPE FROM GPIL_THRESH_RECON_DTLS_1 D1 WHERE D1.BATCH_NO IN (SELECT D.BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(VARCHAR,'" + txt_To_Date.Text + " 23:59:59',105) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND D.PACKED_GRADE IN (SELECT ITEM_CODE FROM GPIL_ITEM_MASTER WHERE ITEM_DESC='" + ddlPackedGrade.Text + "')) AND D1.BALE_TYPE='OPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY BATCH_NO,GRADE";
                    strsql = "SELECT D.BATCH_NO,I.ITEM_DESC + ' (' + D.PACKED_GRADE + ')' AS GRADE,ROUND(SUM(NET_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,'PRODUCT' AS BALE_TYPE  FROM GPIL_THRESH_RECON_DTLS_2 D, GPIL_THRESH_RECON_HDR H,GPIL_ITEM_MASTER I WHERE H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',103) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',103) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND I.ITEM_DESC='" + ddlPackedGrade.Text + "' AND H.BATCH_NO=D.BATCH_NO AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND PACKED_GRADE=I.ITEM_CODE GROUP BY D.BATCH_NO,I.ITEM_DESC,D.PACKED_GRADE UNION SELECT D1.BATCH_NO,D1.GRADE,ROUND(SUM(MARKED_WT),2) AS QUANTITY,'" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.SelectedItem.ToString() + "' AS VARIETY,'" + ddlPackedGrade.Text + "' AS PACKED_GRADE,'" + txt_From_Date.Text + "' AS FROM_DATE,'" + txt_To_Date.Text + "' AS TO_DATE,'BY-PRODUCT' AS BALE_TYPE FROM GPIL_THRESH_RECON_DTLS_1 D1 WHERE D1.BATCH_NO IN (SELECT D.BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_THRESH_RECON_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.DATE_OF_OPERATION BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',103) AND CONVERT(DATETIME,'" + txt_To_Date.Text + " 23:59:59',103) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND D.PACKED_GRADE IN (SELECT ITEM_CODE FROM GPIL_ITEM_MASTER WHERE ITEM_DESC='" + ddlPackedGrade.Text + "')) AND D1.BALE_TYPE='OPB' GROUP BY D1.BATCH_NO,D1.GRADE ORDER BY BATCH_NO,GRADE";
                }
                
                DataTable dt = new DataTable("DT_BLENDRATIO_REPORT");

                dt = GLTMgt.GetQueryResult(strsql);

                if (dt.Rows.Count > 0)
                {
                    if (checkWithPercentage.Checked == true)
                    {
                        CustomerReport.Load(Server.MapPath("~/CrystalReport/RptThreshingBlendRatio.rpt"));
                    }
                    else
                    {
                        CustomerReport.Load(Server.MapPath("~/CrystalReport/RptThreshingBlendRatioWtPerc.rpt"));
                    }

                    //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    //CustomerReport.SetDataSource(dt);
                    //CrystalReportViewer1.ReportSource = rd;
                    //CrystalReportViewer1.RefreshReport();
                    // adapter.Dispose();

                    CustomerReport.SetDataSource(dt.DefaultView);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }


            }
            catch (Exception ex)
            {
                gradouterror = gradouterror + Environment.NewLine + ex.Message;
                //Errorlog err = new Errorlog();
                //errfile = err.WriteErrorLog(gradouterror, "ThrshingBlendRatioReport" + Session["userID"].ToString(), Server.MapPath("LOGFILES\\"));
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home");
        }
    }
}