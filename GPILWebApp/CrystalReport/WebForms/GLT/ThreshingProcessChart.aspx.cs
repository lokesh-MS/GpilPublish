using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.GLT
{
    public partial class ThreshingProcessChart : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
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
                    if (ddlBatchNumber.SelectedIndex != 0)
                    {
                        if (rdbThreshingIssueDetails.Checked == true)
                        {
                            viewissuerpt();
                        }
                        if (rdbProcessChart.Checked == true)
                            viewissuerpt();
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
                ds1 = crd.GetORGN("ORGN", "", "");
                ddlToOrgnCode.DataSource = ds1.Tables[0];
                ddlToOrgnCode.DataTextField = "ORGN_CODE1";
                ddlToOrgnCode.DataValueField = "ORGN_CODE";
                ddlToOrgnCode.DataBind();
                ddlToOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("CROP", "", "");
                ddlCrop.DataSource = ds2.Tables[0];
                ddlCrop.DataTextField = "CROPYEAR";
                ddlCrop.DataValueField = "CROP";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds3 = new DataSet();
                ds3 = crd.GetORGN("VARIETY", "", "");
                ddlVariety.DataSource = ds3.Tables[0];
                ddlVariety.DataTextField = "VARIETYNAME";
                ddlVariety.DataValueField = "VARIETY";
                ddlVariety.DataBind();
                ddlVariety.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {

            }
        }



        GLTManagement GLTMgt = new GLTManagement();
        DataTable dtclstr = new DataTable();
        DataTable dt = new DataTable();
        public void FuncGetBatchList()
        {
            try
            {
                DateTime dt1 = Convert.ToDateTime(txt_From_Date.Text);
                txt_From_Date.Text = dt1.ToString("yyyy-MM-dd");
                DateTime dt3 = Convert.ToDateTime(txt_To_Date.Text);
                txt_To_Date.Text = dt3.ToString("yyyy-MM-dd");
                DataTable dt = new DataTable();
                string sql = "SELECT DISTINCT (BATCH_NO + ' - ' + TEMP_REF) AS BATCH,BATCH_NO  FROM GPIL_THRESH_RECON_HDR H WHERE  H.DATE_OF_OPERATION BETWEEN CONVERT(VARCHAR,'" + txt_From_Date.Text + " 00:00:00',103) AND CONVERT(VARCHAR,'" + txt_To_Date.Text + " 23:59:59',103) AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' AND H.ORGN_CODE='" + ddlToOrgnCode.Text + "' ORDER BY BATCH_NO";
               
                dt = GLTMgt.GetQueryResult(sql);
               
                ddlBatchNumber.DataSource = dt;
                ddlBatchNumber.DataTextField = "BATCH";
                ddlBatchNumber.DataValueField = "BATCH_NO";
                ddlBatchNumber.DataBind();
                ddlBatchNumber.Items.Insert(0, "<--Select-->");

               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }







        protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncGetBatchList();
        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncGetBatchList();
        }

        protected void ddlVariety_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuncGetBatchList();
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                if (validated())
                {
                    if (rdbThreshingIssueDetails.Checked == true)
                    {
                        viewissuerpt();
                    }
                    else if (rdbProcessChart.Checked == true)
                    {
                        viewchrtrpt();
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
            DateTime dt1 = Convert.ToDateTime(txt_From_Date.Text);
            txt_From_Date.Text = dt1.ToString("yyyy-MM-dd");
            DateTime dt3 = Convert.ToDateTime(txt_To_Date.Text);
            txt_To_Date.Text = dt3.ToString("yyyy-MM-dd");
            try
            {
                if (ddlBatchNumber.Text != "< -- Select -- >")
                {
                    sql = "SELECT  ROW_NUMBER() OVER(ORDER BY GRADE) AS SNO,(BATCH_NO + ' - ' + (SELECT TEMP_REF FROM GPIL_THRESH_RECON_HDR WHERE BATCH_NO='" + ddlBatchNumber.Text + "')) AS BATCH_NO,GRADE,I.ATTRIBUTE4 AS HsnCode,COUNT(GPIL_BALE_NUMBER) AS PACKAGES,SUM(MARKED_WT) AS NET_ISSUE,COUNT(GPIL_BALE_NUMBER) + SUM(MARKED_WT) AS MRKD_GROSS ,COUNT(GPIL_BALE_NUMBER) + SUM(ASCERTAIN_WT) AS ASRT_GROSS,(SELECT COUNT(GPIL_BALE_NUMBER) AS GT_COUNT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='GT') AS GT_COUNT,(SELECT ISNULL(SUM(MARKED_WT),0) AS GT_NETWT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='GT') AS GT_NETWT,(SELECT COUNT(GPIL_BALE_NUMBER) AS EL_COUNT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='EB') AS EL_COUNT,(SELECT ISNULL(SUM(MARKED_WT),0) AS EL_NETWT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='EB') AS EL_NETWT  FROM GPIL_THRESH_RECON_DTLS_1 T,GPIL_ITEM_MASTER I WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB' AND T.GRADE=I.ITEM_CODE GROUP BY GRADE,BATCH_NO,I.ATTRIBUTE4 ORDER BY GRADE";
                   
                    dt = GLTMgt.GetQueryResult(sql);


                    ReportDocument CustomerReport = new ReportDocument();

                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptThreshingChartIssueDetails.rpt"));                    
                    CustomerReport.SetDataSource(dt.DefaultView);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();



                    //sql = "SELECT  ROW_NUMBER() OVER(ORDER BY GRADE) AS SNO,(BATCH_NO + ' - ' + (SELECT TEMP_REF FROM GPIL_THRESH_RECON_HDR WHERE BATCH_NO='" + ddlBatchNumber.Text + "')) AS BATCH_NO,GRADE,I.ATTRIBUTE4 AS HsnCode,COUNT(GPIL_BALE_NUMBER) AS PACKAGES,SUM(MARKED_WT) AS NET_ISSUE,COUNT(GPIL_BALE_NUMBER) + SUM(MARKED_WT) AS MRKD_GROSS ,COUNT(GPIL_BALE_NUMBER) + SUM(ASCERTAIN_WT) AS ASRT_GROSS,(SELECT COUNT(GPIL_BALE_NUMBER) AS GT_COUNT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='GT') AS GT_COUNT,(SELECT ISNULL(SUM(MARKED_WT),0) AS GT_NETWT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='GT') AS GT_NETWT,(SELECT COUNT(GPIL_BALE_NUMBER) AS EL_COUNT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='EB') AS EL_COUNT,(SELECT ISNULL(SUM(MARKED_WT),0) AS EL_NETWT FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='EB') AS EL_NETWT  FROM GPIL_THRESH_RECON_DTLS_1 T,GPIL_ITEM_MASTER I WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB' AND T.GRADE=I.ITEM_CODE GROUP BY GRADE,BATCH_NO,I.ATTRIBUTE4 ORDER BY GRADE";
                    ////sql = "SELECT GRADE,COUNT(GPIL_BALE_NUMBER) AS PACKAGES,SUM(MARKED_WT) AS Marked_Gross_Weight,(SELECT COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE PRODUCT_TYPE='EB' and BATCH_NO='" + ddlBatchNumber.Text + "')  AS ELI_PAC,(SELECT COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE PRODUCT_TYPE='GT' and BATCH_NO='" + ddlBatchNumber.Text + "')  AS GT_PAC,(SELECT SUM(MARKED_WT)-COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE PRODUCT_TYPE='GT' and BATCH_NO='" + ddlBatchNumber.Text + "') AS GT_NETWT,(SELECT SUM(MARKED_WT)-COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE PRODUCT_TYPE='EB' and BATCH_NO='" + ddlBatchNumber.Text + "' ) AS ELI_NETWT,SUM(ASCERTAIN_WT) AS Ascertained_Gross_Weight,SUM(MARKED_WT)-COUNT(GPIL_BALE_NUMBER) AS Net_Issue FROM GPIL_THRESH_RECON_DTLS_1 WHERE BALE_TYPE='IPB' AND BATCH_NO='" + ddlBatchNumber.Text + "' GROUP BY GRADE";
                    //dr = SqlHelper.ExecuteReader(ClsConnection.SqlCon, CommandType.Text, sql);
                    //dt = new DataTable();
                    //dt.Load(dr);

                    //rd = new ReportDocument();
                    ////rd.Load(Server.MapPath("~/Reports/rptThreshingIssuedDetails.rpt"));
                    //rd.Load(Server.MapPath("~/Reports/RptThreshingChartIssueDetails.rpt"));
                    //rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    //rd.SetDataSource(dt);
                    //CrystalReportViewer1.ReportSource = rd;
                    //CrystalReportViewer1.RefreshReport();

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Batch Number');", true);
                    ddlBatchNumber.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        public void viewchrtrpt()
        {
            try
            {
                if (ddlBatchNumber.Text != "< -- Select -- >")
                {
                    sql = "SELECT ORGN_CODE,ORGN_NAME,(TL1.BATCH_NO + ' - ' + (SELECT TEMP_REF FROM GPIL_THRESH_RECON_HDR WHERE BATCH_NO='" + ddlBatchNumber.Text + "')) AS BATCH_NO,REPORT_NO,DATE_OF_OPERATION,(TL2.CROP  + ' - ' + (SELECT CROP_YEAR FROM GPIL_CROP_MASTER WHERE CROP=TL2.CROP)) AS CROP,(TL2.CROP  + ' - ' + (SELECT VARIETY_NAME FROM GPIL_VARIETY_MASTER WHERE VARIETY=TL2.VARIETY)) AS VARIETY,TL2.GRADE AS GRADE,MRKD_GROSS,ASCRT_GROSS,ISSUED_CNT,MRKD_NET,ASCRT_NET,SLOSS,SLOSS_PERC,EL_NET,GT_NET,LOSS,LOSS_PERC,CREATED_BY,TL1.OUTGRADE AS OUTGRADE,TL1.CNT AS CNT,TL1.NET AS NET,TL1.PERC AS PERC,TL1.OUTTYPE AS OUTTYPE,TL1.HsnCode FROM " +
                            "((SELECT PACKED_GRADE AS OUTGRADE,COUNT(CASE_NUMBER) AS CNT,SUM(NET_WT) AS NET,ROUND(100 *(SUM(NET_WT)/(SELECT (PROD + BYP) AS TOTAL FROM " +
                            "(SELECT SUM(NET_WT) AS PROD,BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' GROUP BY BATCH_NO )   TBL1, " +
                            "(SELECT SUM(MARKED_WT) AS BYP,BATCH_NO FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('LOSS','BP') GROUP BY BATCH_NO  )  TBL2 WHERE TBL1.BATCH_NO=TBL2.BATCH_NO )),2) AS PERC, " +
                            "'PROD' AS OUTTYPE,D.BATCH_NO,ISNULL(I.ATTRIBUTE4,'')HsnCode FROM GPIL_THRESH_RECON_DTLS_2 D,GPIL_ITEM_MASTER(NOLOCK) I WHERE  D.BATCH_NO='" + ddlBatchNumber.Text + "' AND D.PACKED_GRADE=I.ITEM_CODE GROUP BY PACKED_GRADE,D.BATCH_NO,I.ATTRIBUTE4) " +
                            "UNION " +
                            "(SELECT ITEM_CODE_GROUP AS OUTGRADE,COUNT(GPIL_BALE_NUMBER) AS CNT,SUM(MARKED_WT) AS NET,ROUND(100 *(SUM(MARKED_WT)/(SELECT (PROD + BYP) AS TOTAL FROM " +
                            "(SELECT SUM(NET_WT) AS PROD,BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' GROUP BY BATCH_NO )   TBL1, " +
                            "(SELECT SUM(MARKED_WT) AS BYP,BATCH_NO FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('LOSS','BP') GROUP BY BATCH_NO  )  TBL2 WHERE TBL1.BATCH_NO=TBL2.BATCH_NO )),2) AS PERC, " +
                            "'BYP' AS OUTTYPE,D.BATCH_NO,ISNULL(I.ATTRIBUTE4,'')HsnCode FROM GPIL_THRESH_RECON_DTLS_1 D,GPIL_ITEM_MASTER I WHERE  I.ITEM_CODE=GRADE AND D.BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='BP' GROUP BY ITEM_CODE_GROUP,D.BATCH_NO,I.ATTRIBUTE4) ) " +
                            "TL1, " +
                            "(SELECT H.ORGN_CODE,O.ORGN_NAME,H.BATCH_NO,H.REPORT_NO,H.DATE_OF_OPERATION,H.CROP,H.VARIETY, " +
                            "(SELECT I.ITEM_DESC FROM (SELECT ROW_NUMBER() OVER (ORDER BY PROD DESC) AS SNO, PACKED_GRADE,PROD  FROM  (SELECT  SUM(NET_WT) AS PROD,PACKED_GRADE FROM GPIL_THRESH_RECON_DTLS_2 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' GROUP BY PACKED_GRADE) T1) T2,GPIL_ITEM_MASTER I WHERE T2.SNO='1' AND I.ITEM_CODE=PACKED_GRADE) AS GRADE, " +
                            "(SELECT ISNULL(SUM(MARKED_WT) + COUNT(GPIL_BALE_NUMBER),0)  FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB') AS MRKD_GROSS, " +
                            "(SELECT ISNULL(SUM(ASCERTAIN_WT) + COUNT(GPIL_BALE_NUMBER),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB') AS ASCRT_GROSS, " +
                            "(SELECT COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB') AS ISSUED_CNT, " +
                            "(SELECT ISNULL(SUM(MARKED_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB') AS MRKD_NET, " +
                            "(SELECT ISNULL(SUM(ASCERTAIN_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB') AS ASCRT_NET, " +
                            "(SELECT ISNULL(SUM(MARKED_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='SLOSS') AS SLOSS, " +
                            "(SELECT ROUND((ISNULL(SUM(MARKED_WT),0)/(SELECT ISNULL(SUM(MARKED_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='IPB') * 100),2) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='SLOSS') AS SLOSS_PERC, " +
                            "(SELECT ISNULL(SUM(MARKED_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='EB') AS EL_NET, " +
                            "(SELECT ISNULL(SUM(MARKED_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='GT') AS GT_NET, " +
                            "(SELECT ISNULL(SUM(MARKED_WT),0) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='LOSS') AS LOSS, " +
                            "(SELECT ROUND((ISNULL(SUM(MARKED_WT),0)/((SELECT (PROD + BYP) AS TOTAL FROM (SELECT SUM(NET_WT) AS PROD,BATCH_NO FROM GPIL_THRESH_RECON_DTLS_2 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' GROUP BY BATCH_NO )   TBL1, (SELECT SUM(MARKED_WT) AS BYP,BATCH_NO FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('LOSS','BP') GROUP BY BATCH_NO  )  TBL2 WHERE TBL1.BATCH_NO=TBL2.BATCH_NO )) * 100),2) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BATCH_NO='" + ddlBatchNumber.Text + "' AND BALE_TYPE='OPB' AND PRODUCT_TYPE='LOSS') AS LOSS_PERC, " +
                            "H.CREATED_BY  FROM GPIL_THRESH_RECON_HDR H,GPIL_ORGN_MASTER O WHERE H.BATCH_NO='" + ddlBatchNumber.Text + "' AND O.ORGN_CODE=H.ORGN_CODE) " +
                            "TL2 " +
                            "ORDER BY OUTTYPE DESC,NET DESC";

                    //sql = "SELECT D1.BATCH_NO,D1.GRADE,SUM(D1.MARKED_WT) as Marked_WT,SUM(D1.ASCERTAIN_WT) AS ASCERT_WT,(SELECT COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE BALE_TYPE='IPB' and BATCH_NO='" + ddlBatchNumber.Text + "') as Packages,H.ORGN_CODE,o.ORGN_NAME,H.REPORT_NO,H.DATE_OF_OPERATION,H.CROP,H.VARIETY,(SELECT SUM(MARKED_WT)-COUNT(GPIL_BALE_NUMBER)FROM GPIL_THRESH_RECON_DTLS_1 WHERE PRODUCT_TYPE='EB' and BATCH_NO='" + ddlBatchNumber.Text + "') AS ELI_WT,(SELECT SUM(MARKED_WT)-COUNT(GPIL_BALE_NUMBER) FROM GPIL_THRESH_RECON_DTLS_1 WHERE PRODUCT_TYPE='GT' and BATCH_NO='" + ddlBatchNumber.Text + "') AS GT_WT ,D2.PACKED_GRADE,sum(D2.TARE_WT) as TARE_WT,sum(D2.NET_WT) as NET_WT FROM GPIL_THRESH_RECON_DTLS_1 D1,GPIL_THRESH_RECON_HDR H,GPIL_ORGN_MASTER o,GPIL_THRESH_RECON_DTLS_2 D2 WHERE D1.BALE_TYPE='IPB' and H.BATCH_NO='" + ddlBatchNumber.Text + "' AND D1.BATCH_NO='" + ddlBatchNumber.Text + "'  and o.ORGN_CODE=H.ORGN_CODE and D2.BATCH_NO='" + ddlBatchNumber.Text + "' GROUP BY D1.BATCH_NO,D1.GRADE,H.ORGN_CODE,o.ORGN_NAME,H.REPORT_NO,H.DATE_OF_OPERATION,H.CROP,H.VARIETY,D2.PACKED_GRADE,D2.TARE_WT,D2.NET_WT";
                    dt = GLTMgt.GetQueryResult(sql);

                    ReportDocument CustomerReport = new ReportDocument();

                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptThreshingProcessChart.rpt"));
                    CustomerReport.SetDataSource(dt.DefaultView);

                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                    //dt = new DataTable();
                    //dt.Load(dr);
                    //rd = new ReportDocument();
                    //rd.Load(Server.MapPath("~/Reports/RptThreshingProcessChart.rpt"));
                    //rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    //rd.SetDataSource(dt);
                    //CrystalReportViewer1.ReportSource = rd;
                    //CrystalReportViewer1.RefreshReport();

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please Select Batch Number');", true);
                    ddlBatchNumber.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThreshingProcessChart");
        }
        public bool validated()
        {

            return true;
        }

        protected void btnClose_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ThreshingProcessChart.aspx");
            //Response.Redirect("/Home/Index");
            //Server.Transfer("/Home/Index");
            //return RedirectToAction("Index", "Home");
        }

        //public ActionResult btnClose_Click1()
        //{
        //    // Redirect to the "Index" action in the "Home" controller
        //    return RedirectToAction("Index", "Home");
        //}

    }
}