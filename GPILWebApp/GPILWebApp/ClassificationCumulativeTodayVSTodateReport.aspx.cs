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
    public partial class ClassificationCumulativeTodayVSTodateReport : System.Web.UI.Page
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (validated())
                {

                   
                        viewrpt();
                   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public bool validated()
        {
            if (txtClassificationDate.Text == "<--Select-->")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txtClassificationDate.Focus();
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
            try
            {

                DateTime dtt = Convert.ToDateTime(txtClassificationDate.Text);
                txtClassificationDate.Text = dtt.ToString("dd-MM-yyyy");
                string strsql = "";

                if (ddlOrganization.SelectedIndex == 0)
                {
                    //strsql = "SELECT I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as avgpr,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP order by I.ITEM_CODE_GROUP";
                    //strsql = "SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO";
                    strsql = "SELECT TBL1.SEQNO,TBL1.CLASSIFICATION_GRADE,ISNULL(TBL2.TODAY_BALES,'0') AS TODAY_BALES,ISNULL(TBL2.TODAY_QTY,'0') AS TODAY_QTY,ISNULL(TBL2.TODAY_AVEPRICE,'0') AS TODAY_AVEPRICE,TBL1.TODATE_BALES,TBL1.TODATE_QTY,TBL1.TODATE_AVEPRICE,TBL1.CROP,TBL1.VARIETY,TBL1.VARIETY_NAME,TBL1.ORGN_NAME,TBL1.TODATE FROM (SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,COUNT(D.GPIL_BALE_NUMBER) AS TODATE_BALES,SUM(D.NET_WT) as TODATE_QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as TODATE_AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txtClassificationDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H,GPIL_ORGN_MASTER (NOLOCK) O,GPIL_VARIETY_MASTER (NOLOCK) V ,GPIL_CLASSIFICATION_DTLS (NOLOCK) C ,GPIL_ITEM_MASTER (NOLOCK) I,GPIL_CLASSIFICATION_HDR (NOLOCK) HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and  HC.CLASSIFICATION_DATE <= CONVERT(datetime,'" + txtClassificationDate.Text + " 23:59:59 PM',105) group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1) TBL1 FULL OUTER JOIN (SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,COUNT(D.GPIL_BALE_NUMBER) AS TODAY_BALES,SUM(D.NET_WT) as TODAY_QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as TODAY_AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txtClassificationDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and  CONVERT(NVARCHAR(15),HC.CLASSIFICATION_DATE,105) = '" + txtClassificationDate.Text + "' group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1) TBL2 ON TBL1.CLASSIFICATION_GRADE=TBL2.CLASSIFICATION_GRADE AND TBL1.CROP=TBL2.CROP AND TBL1.VARIETY=TBL2.VARIETY AND TBL1.ORGN_NAME=TBL2.ORGN_NAME AND TBL1.VARIETY_NAME=TBL2.VARIETY_NAME ORDER BY TBL1.SEQNO,TBL1.CLASSIFICATION_GRADE";


                }
                else
                {
                    //strsql = "SELECT I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as avgpr,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND  I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) and H.ORGN_CODE='" + cbxorgcd.Text + "'  group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP order by I.ITEM_CODE_GROUP";
                    //strsql = "SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,SUM(D.NET_WT) as QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND  I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and  HC.CLASSIFICATION_DATE between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) and H.ORGN_CODE='" + cbxorgcd.Text + "'  group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1 order by SEQNO";
                    strsql = "SELECT TBL1.SEQNO,TBL1.CLASSIFICATION_GRADE,ISNULL(TBL2.TODAY_BALES,'0') AS TODAY_BALES,ISNULL(TBL2.TODAY_QTY,'0') AS TODAY_QTY,ISNULL(TBL2.TODAY_AVEPRICE,'0') AS TODAY_AVEPRICE,TBL1.TODATE_BALES,TBL1.TODATE_QTY,TBL1.TODATE_AVEPRICE,TBL1.CROP,TBL1.VARIETY,TBL1.VARIETY_NAME,TBL1.ORGN_NAME,TBL1.TODATE FROM (SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,COUNT(D.GPIL_BALE_NUMBER) AS TODATE_BALES,SUM(D.NET_WT) as TODATE_QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as TODATE_AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txtClassificationDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS (NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR (NOLOCK) H,GPIL_ORGN_MASTER (NOLOCK) O,GPIL_VARIETY_MASTER (NOLOCK) V ,GPIL_CLASSIFICATION_DTLS (NOLOCK) C ,GPIL_ITEM_MASTER (NOLOCK) I,GPIL_CLASSIFICATION_HDR (NOLOCK) HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and H.ORGN_CODE='" + ddlOrganization.Text + "' and  HC.CLASSIFICATION_DATE <= CONVERT(datetime,'" + txtClassificationDate.Text + " 23:59:59 PM',105) group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1) TBL1 FULL OUTER JOIN (SELECT ISNULL(CONVERT( INT,I.ATTRIBUTE1),0) AS SEQNO,I.ITEM_CODE_GROUP as CLASSIFICATION_GRADE,COUNT(D.GPIL_BALE_NUMBER) AS TODAY_BALES,SUM(D.NET_WT) as TODAY_QTY ,ROUND(SUM(D.VALUE)/SUM(D.NET_WT),2) as TODAY_AVEPRICE,O.ORGN_CODE+' - '+O.ORGN_NAME AS ORGN_NAME,H.CROP AS CROP,H.VARIETY AS VARIETY,V.VARIETY_NAME AS VARIETY_NAME,'" + txtClassificationDate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V ,GPIL_CLASSIFICATION_DTLS C ,GPIL_ITEM_MASTER I,GPIL_CLASSIFICATION_HDR HC where HC.BATCH_NO=C.BATCH_NO AND ISNULL(HC.ATTRIBUTE3,'')<>'N' AND I.ITEM_CODE=C.CLASSIFICATION_GRADE AND HC.RECIPE_CODE='CLASSIFICATION' AND O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND D.GPIL_BALE_NUMBER=C.GPIL_BALE_NUMBER  AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + ddlCrop.Text + "' and H.VARIETY='" + ddlVariety.Text + "' and H.ORGN_CODE='" + ddlOrganization.Text + "' and  CONVERT(NVARCHAR(15),HC.CLASSIFICATION_DATE,105) = '" + txtClassificationDate.Text + "' group by O.ORGN_CODE+' - '+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,I.ITEM_CODE_GROUP,I.ATTRIBUTE1) TBL2 ON TBL1.CLASSIFICATION_GRADE=TBL2.CLASSIFICATION_GRADE AND TBL1.CROP=TBL2.CROP AND TBL1.VARIETY=TBL2.VARIETY AND TBL1.ORGN_NAME=TBL2.ORGN_NAME AND TBL1.VARIETY_NAME=TBL2.VARIETY_NAME ORDER BY TBL1.SEQNO,TBL1.CLASSIFICATION_GRADE";

                }

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable("DT_TODAYANDTODATECLASSIFICATION");
                dt = ppdMgt.GetClassificationCumulaitveReport(strsql);


                ReportDocument CustomerReport = new ReportDocument();
                if (dt.Rows.Count > 0)
                {
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptClassificationCumulativeTodayVsTodateReport.rpt"));
                CustomerReport.SetDataSource(dt);

                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('No Data Found');", true);
                }

                //DataTable dt = new DataTable("DT_TODAYANDTODATECLASSIFICATION");
                //SqlDataAdapter adapter = new SqlDataAdapter(strsql, ClsConnection.SqlCon);
                //adapter.SelectCommand.CommandTimeout = 0;
                //adapter.Fill(dt);

                //ReportDocument rd = new ReportDocument();
                //if (dt.Rows.Count > 0)
                //{
                //    rd.Load(Server.MapPath("~/Reports/RptClassificationCumulativeTodayVsTodateReport.rpt"));
                //    rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                //    rd.SetDataSource(dt);
                //    CrystalReportViewer1.ReportSource = rd;
                //    CrystalReportViewer1.RefreshReport();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('No Data Found');", true);
                //}


                /*
                 SqlCommand command = new SqlCommand(strsql, connection);
                 SqlDataAdapter adapter = new SqlDataAdapter(command);
                 //Customer _Customer = new Customer();
                 DataSet dataset = new DataSet();
                 adapter.Fill(dataset, "byr");
                 //RptTAPWiseClassificationCumulativeReport.rpt
                 //RptClassificationCumulativeTAPWiseReport.rpt
                 //rptClassificationReportforTAP.rpt
                 CustomerReport.Load(Server.MapPath("~/Reports/rptClassificationReportforTAP.rpt"));
                 CustomerReport.SetDataSource(dataset.Tables["byr"]);
                 CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                 CrystalReportViewer1.ReportSource = CustomerReport;
                 CrystalReportViewer1.DataBind();
                 */
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
        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}