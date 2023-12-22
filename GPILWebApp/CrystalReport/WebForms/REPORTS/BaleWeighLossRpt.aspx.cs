using CrystalDecisions.CrystalReports.Engine;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class BaleWeighLossRpt : System.Web.UI.Page
    {
        string sql;
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        GltManagement GLTMgt = new GltManagement();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
                //viewrpt();
            }
            else
            {
                bindDropDown();
            }
        }
        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGNCodeNTF", "", "");
                ddlOrgnCode.DataSource = ds.Tables[0];
                ddlOrgnCode.DataTextField = "ORGN_CODE1";
                ddlOrgnCode.DataValueField = "ORGN_CODE";
                ddlOrgnCode.DataBind();
                ddlOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));
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
        protected void btn_View_Click(object sender, EventArgs e)
        {
            if (this.valgrdrpt() == true)
            {
                viewrpt();
            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            ddlCrop.SelectedIndex = 0;
            ddlVariety.SelectedIndex = 0;
            ddlOrgnCode.SelectedIndex = 0;
        }

        public void viewrpt()
        {
            DateTime dt1 = Convert.ToDateTime(txtFromDate.Text);
            txtFromDate.Text = dt1.ToString("dd-MM-yyyy");
            DateTime dt2 = Convert.ToDateTime(txtToDate.Text);
            txtToDate.Text = dt2.ToString("dd-MM-yyyy");



            // sql = "SELECT D.GRADE,H.CROP,H.VARIETY,H.ORGN_CODE,H.RECIPE_CODE,CONVERT(NVARCHAR(10),H.DATE_OF_OPERATION,105),H.ISSUED_GRADE,H.TOT_ISSUE_BALES,H.TOT_ISSUE_QTY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,COUNT(D.GPIL_BALE_NUMBER) AS CNN,SUM(D.MARKED_WT)as wtt,O.ORGN_NAME FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H,GPIL_ORGN_MASTER O where H.RECIPE_CODE='" + cmb_Operation_Receipe.Text + "' AND H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + cmb_Orgn_Code.Text + "' AND H.VARIETY='" + cmb_Variety.Text + "' AND H.STATUS='N' AND D.BALE_TYPE='OPB' AND CONVERT(NVARCHAR(10),H.DATE_OF_OPERATION,105)= '" + txt_From_Date.Text + "' AND O.ORGN_CODE=H.ORGN_CODE GROUP BY D.GRADE,H.CROP,H.RECIPE_CODE,H.DATE_OF_OPERATION,H.ISSUED_GRADE,H.TOT_ISSUE_BALES,H.TOT_ISSUE_QTY,H.NO_OF_GRADERS,H.AVG_BALES_GRADER,H.ORGN_CODE,H.VARIETY,O.ORGN_NAME";
            //       sql = "SELECT  SUM(D.MARKED_WT) AS MARKEDWT,SUM(D.RECEIPT_WEIGHT) AS LP5RECEIPT,ROUND(SUM(D.MARKED_WT)-SUM(D.RECEIPT_WEIGHT),1)  AS QTY,H.RECEIVER_ORGN_CODE,CASE WHEN H.RECEV_WEIGH_TYPE='TWT'  THEN '10%  & 20% CHECK WEIGHMENT' ELSE '100% CHECK WEIGHMENT' END AS WEIGHMENTTYPE FROM GPIL_SHIPMENT_HDR H,GPIL_SHIPMENT_DTLS D,GPIL_ORGN_MASTER O WHERE D.SHIPMENT_NO=H.SHIPMENT_NO AND D.WEIGHT_STATUS='Y' AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,(REPLACE('" + txt_From_Date.Text + "','12:00:00 AM','00:00:00 AM')),105) AND CONVERT(DATETIME,(REPLACE('" + txt_To_Date.Text + "','12:00:00 AM','23:59:59 PM')),105) AND H.STATUS='RCV' AND O.ORGN_TYPE !='TAP' AND O.ORGN_TYPE !='FARMER' GROUP BY H.RECEV_WEIGH_TYPE,H.RECEIVER_ORGN_CODE";
            if (ddlOrgnCode.SelectedIndex == 0)
            {
                sql = "select MH.SENDER_ORGN_CODE , SUM(MD.MARKED_WT) AS QUANTITY,MH.SENDER_ORGN_CODE+' - '+O1.ORGN_NAME AS SENDER_ORGN_NAME,(SELECT CASE WHEN ROUND(SUM(MARKED_WT),2) IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNWT,";
                sql = sql + " (SELECT CASE WHEN ROUND(SUM(RECEIPT_WEIGHT),2) IS NULL THEN 0 ELSE ROUND(SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNRECEVWT,(SELECT CASE WHEN ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2)  IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNRECEVWTLOSS,";
                sql = sql + " (SELECT CASE WHEN ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) IS NULL THEN 0 ELSE ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNWTLOSPER,(SELECT CASE WHEN ROUND(SUM(MARKED_WT),2) IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTWT,";
                sql = sql + " (SELECT CASE WHEN ROUND(SUM(RECEIPT_WEIGHT),2) IS NULL THEN 0 ELSE ROUND(SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTRECEVWT,(SELECT CASE WHEN ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2)  IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTRECEVWTLOSS,";
                sql = sql + " (SELECT CASE WHEN ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) IS NULL THEN 0 ELSE ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTWTLOSSPER,C.CROP+' - '+C.CROP_YEAR AS CROP,V.VARIETY+' - '+V.VARIETY_NAME AS VARIETY from GPIL_SHIPMENT_DTLS MD,GPIL_SHIPMENT_HDR MH ,GPIL_ORGN_MASTER O1,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V";
                sql = sql + " WHERE MH.SHIPMENT_NO=MD.SHIPMENT_NO AND MH.STATUS='N' AND MH.SENDER_ORGN_CODE=O1.ORGN_CODE AND  C.CROP=SUBSTRING(MD.GPIL_BALE_NUMBER,1,2) AND V.VARIETY=SUBSTRING(MD.GPIL_BALE_NUMBER,3,2) and MH.RECEIVED_DATE between	CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59 PM',105) AND C.CROP='" + ddlCrop.Text + "' AND V.VARIETY='" + ddlVariety.Text + "'  GROUP BY MH.SENDER_ORGN_CODE,MH.SENDER_ORGN_CODE+' - '+O1.ORGN_NAME,C.CROP+' - '+C.CROP_YEAR,V.VARIETY+' - '+V.VARIETY_NAME ";
            }
            else
            {
                sql = "select MH.SENDER_ORGN_CODE,MH.RECEIVER_ORGN_CODE , SUM(MD.MARKED_WT) AS QUANTITY,MH.SENDER_ORGN_CODE+' - '+O1.ORGN_NAME AS SENDER_ORGN_NAME,MH.RECEIVER_ORGN_CODE+' - '+O2.ORGN_NAME AS RECEV_ORGN_NAME,(SELECT CASE WHEN ROUND(SUM(MARKED_WT),2) IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNWT,";
                sql = sql + " (SELECT CASE WHEN ROUND(SUM(RECEIPT_WEIGHT),2) IS NULL THEN 0 ELSE ROUND(SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNRECEVWT,(SELECT CASE WHEN ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2)  IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNRECEVWTLOSS,";
                sql = sql + " (SELECT CASE WHEN ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) IS NULL THEN 0 ELSE ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='HND' AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS HUNWTLOSPER,(SELECT CASE WHEN ROUND(SUM(MARKED_WT),2) IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTWT,";
                sql = sql + " (SELECT CASE WHEN ROUND(SUM(RECEIPT_WEIGHT),2) IS NULL THEN 0 ELSE ROUND(SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTRECEVWT,(SELECT CASE WHEN ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2)  IS NULL THEN 0 ELSE ROUND(SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT),2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTRECEVWTLOSS,";
                sql = sql + " (SELECT CASE WHEN ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) IS NULL THEN 0 ELSE ROUND(((SUM(MARKED_WT)-SUM(RECEIPT_WEIGHT))/SUM(MARKED_WT))*100,2) END FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEV_WEIGH_TYPE='TWT' AND D.WEIGHT_STATUS='Y'  AND H.SENDER_ORGN_CODE=MH.SENDER_ORGN_CODE ) AS TWTWTLOSSPER,C.CROP+' - '+C.CROP_YEAR AS CROP,V.VARIETY+' - '+V.VARIETY_NAME AS VARIETY from GPIL_SHIPMENT_DTLS MD,GPIL_SHIPMENT_HDR MH ,GPIL_ORGN_MASTER O1,GPIL_ORGN_MASTER O2,GPIL_CROP_MASTER C,GPIL_VARIETY_MASTER V";
                sql = sql + " WHERE MH.SHIPMENT_NO=MD.SHIPMENT_NO AND MH.STATUS='N' AND MH.RECEV_WEIGH_TYPE IS NOT NULL AND MH.RECEIVER_ORGN_CODE='" + ddlOrgnCode.Text + "' AND MH.SENDER_ORGN_CODE=O1.ORGN_CODE AND MH.RECEIVER_ORGN_CODE=O2.ORGN_CODE AND C.CROP=SUBSTRING(MD.GPIL_BALE_NUMBER,1,2) AND V.VARIETY=SUBSTRING(MD.GPIL_BALE_NUMBER,3,2) and MH.RECEIVED_DATE between CONVERT(datetime,'" + txtFromDate.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txtToDate.Text + " 23:59:59 PM',105) AND C.CROP='" + ddlCrop.Text + "' AND V.VARIETY='" + ddlVariety.Text + "'  GROUP BY MH.RECEIVER_ORGN_CODE ,MH.SENDER_ORGN_CODE,MH.SENDER_ORGN_CODE+' - '+O1.ORGN_NAME,MH.RECEIVER_ORGN_CODE+' - '+O2.ORGN_NAME,C.CROP+' - '+C.CROP_YEAR,V.VARIETY+' - '+V.VARIETY_NAME ";
            }


            //dr = SqlHelper.ExecuteReader(ClsConnection.SqlCon, CommandType.Text, sql);
          
            //dt.Load(dr);

            dt = GLTMgt.GetQueryResult(sql);
           



           
            if (dt.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Info_msg", "alert('No Data Found');", true);
                return;
            }
            else
            {
                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/rptWeightLossrpt.rpt"));
                CustomerReport.SetDataSource(dt.DefaultView);
                rdlcWeightLossReport.ReportSource = CustomerReport;
                rdlcWeightLossReport.DataBind();
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
    }
}