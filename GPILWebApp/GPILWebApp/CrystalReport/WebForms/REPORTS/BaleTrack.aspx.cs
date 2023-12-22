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
    public partial class BaleTrack : System.Web.UI.Page
    {
        ReportManagement rMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            viewrpt();
        }
        string strsql;
        DataTable dt = new DataTable();
        ReportDocument CustomerReport = new ReportDocument();
    //    ReportManagement CustomerReport = new ReportManagement();
       
        string tablename;
        public void viewrpt()
        {
            try
            {
                //SqlConnection connection = new SqlConnection(ClsConnection.strConnectionString);
                //ClsConnection.SqlCon.Close();
                //ClsConnection.SqlCon.Open();
                if (txtBaleNumber.Text != string.Empty)
                {
                    strsql = "SELECT P.GPIL_BALE_NUMBER,P.PROCESS_NAME,P.PROCESS_REF_ID,P.ORGN_CODE,WEIGHT ,CONVERT(NVARCHAR(10),P.PROCESS_DATE,105) AS PROCESSDATE,S.ORIGN_ORGN_CODE,S.MARKED_WT,S.PRICE, S.GRADE AS CURRGRADE,'' as GRADE,'' as OUTTURNGRADE,ISNULL(TD.TBGR_NO,TD.FARMER_CODE) AS TBGR_FARMER,(SELECT  DISTINCT I.ITEM_DESC FROM GPIL_THRESH_RECON_DTLS_2_TEMP D,GPIL_ITEM_MASTER I WHERE I.ITEM_CODE=D.PACKED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_DTLS_1_TEMP WHERE BALE_TYPE='IPB' AND GPIL_BALE_NUMBER='" + txtBaleNumber.Text.Trim() + "')) AS PACKED_GRADE FROM GPIL_PROCESS_ORDER_CAPTURE P,GPIL_STOCK S,GPIL_TAP_FARM_PURCHS_DTLS TD WHERE P.GPIL_BALE_NUMBER='" + txtBaleNumber.Text.Trim() + "' AND TD.GPIL_BALE_NUMBER= P.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER ORDER BY PROCESS_DATE";
                    //strsql = "SELECT P.GPIL_BALE_NUMBER,P.PROCESS_NAME,P.PROCESS_REF_ID,P.ORGN_CODE,WEIGHT ,CONVERT(NVARCHAR(10),P.PROCESS_DATE,105) AS PROCESSDATE,S.ORIGN_ORGN_CODE,S.MARKED_WT,S.PRICE, S.GRADE AS CURRGRADE,'' as GRADE,'' as OUTTURNGRADE,ISNULL(TD.TBGR_NO,TD.FARMER_CODE) AS TBGR_FARMER,(SELECT  DISTINCT PACKED_GRADE FROM GPIL_THRESH_RECON_DTLS_2_TEMP WHERE BATCH_NO IN (SELECT BATCH_NO FROM GPIL_THRESH_RECON_DTLS_1_TEMP WHERE BALE_TYPE='IPB' AND GPIL_BALE_NUMBER='" + txtbaleno.Text.Trim() + "')) AS PACKED_GRADE FROM GPIL_PROCESS_ORDER_CAPTURE P,GPIL_STOCK S,GPIL_TAP_FARM_PURCHS_DTLS TD WHERE P.GPIL_BALE_NUMBER='" + txtbaleno.Text.Trim() + "' AND TD.GPIL_BALE_NUMBER= P.GPIL_BALE_NUMBER AND S.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER ORDER BY PROCESS_DATE";
                    //strsql = "SELECT P.GPIL_BALE_NUMBER,P.PROCESS_NAME,P.PROCESS_REF_ID,P.ORGN_CODE,WEIGHT ,CONVERT(NVARCHAR(10),P.PROCESS_DATE,105) AS PROCESSDATE,S.ORIGN_ORGN_CODE,S.MARKED_WT,S.PRICE,S.GRADE AS CURRGRADE,'' as GRADE,'' as OUTTURNGRADE FROM GPIL_PROCESS_ORDER_CAPTURE P,GPIL_STOCK S WHERE P.GPIL_BALE_NUMBER='" + txtbaleno.Text.Trim() + "' AND S.GPIL_BALE_NUMBER=P.GPIL_BALE_NUMBER ORDER BY PROCESS_DATE";
                    DataSet dataset = new DataSet();
                    dataset = rMgt.GetQueryResultDS(strsql);
                    //SqlCommand command = new SqlCommand(strsql, ClsConnection.SqlCon);
                    //command.CommandTimeout = 0;
                    //SqlDataAdapter adapter = new SqlDataAdapter(command);
                    //Customer _Customer = new Customer();
                   
                   // adapter.Fill(dataset, 0);

                    // dataset.Tables[0].Columns.Add("ISSUE_GRADE", typeof(string));
                    //dataset.Tables[0].Columns.Add("OUTTURN_GRADE", typeof(string));

                    for (int s = 0; s < dataset.Tables[0].Rows.Count; s++)
                    {
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "DISPATCH")
                        {
                            tablename = "GPIL_SHIPMENT_DTLS";
                            strsql = "SELECT D.GRADE FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H  WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "' AND H.WAYBILL='" + dataset.Tables[0].Rows[s]["PROCESS_REF_ID"].ToString() + "'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();
                          
                            dt = rMgt.GetQueryResult(strsql);

                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();
                            }
                            //strsr.Close();
                            //cmd.Dispose();

                        }
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "RECEIPT")
                        {
                            tablename = "GPIL_SHIPMENT_DTLS";
                            strsql = "SELECT GRADE FROM GPIL_SHIPMENT_DTLS WITH(NOLOCK) WHERE GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "' AND SHIPMENT_NO='" + dataset.Tables[0].Rows[s]["PROCESS_REF_ID"].ToString() + "'";

                            dt = rMgt.GetQueryResult(strsql);
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();

                            }
                            //strsr.Close();
                            //cmd.Dispose();

                        }

                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "CLASSIFICATION")
                        {
                            tablename = "GPIL_CLASSIFICATION_DTLS";
                            strsql = "SELECT D.ISSUED_GRADE,D.CLASSIFICATION_GRADE FROM GPIL_CLASSIFICATION_HDR H,GPIL_CLASSIFICATION_DTLS D WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "' AND H.RECIPE_CODE='CLASSIFICATION'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();

                            dt = rMgt.GetQueryResult(strsql);
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();
                                dataset.Tables[0].Rows[s]["OUTTURNGRADE"] = dt.Rows[0][1].ToString();

                            }
                            //strsr.Close();
                            //cmd.Dispose();

                        }
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "GRADETRANSFER")
                        {
                            tablename = "GPIL_CLASSIFICATION_DTLS";
                            strsql = "SELECT D.ISSUED_GRADE,D.CLASSIFICATION_GRADE FROM GPIL_CLASSIFICATION_HDR H, GPIL_CLASSIFICATION_DTLS D WHERE H.BATCH_NO=D.BATCH_NO AND D.GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "' AND H.RECIPE_CODE='RE-CLASSIFICATION'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();

                            dt = rMgt.GetQueryResult(strsql);
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();
                                dataset.Tables[0].Rows[s]["OUTTURNGRADE"] = dt.Rows[0][1].ToString();

                            }
                            //dt5.Close();
                            //cmd.Dispose();

                        }
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "CROPTRANSFER")
                        {
                            //tablename = "GPIL_CROP_TRANS_DTLS";
                            strsql = "SELECT OLD_GRADE,NEW_GRADE FROM GPIL_CROP_TRANS_DTLS WITH(NOLOCK) WHERE GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();

                            dt = rMgt.GetQueryResult(strsql);
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();
                                dataset.Tables[0].Rows[s]["OUTTURNGRADE"] = dt.Rows[0][1].ToString();

                            }
                            //strsr.Close();
                            //cmd.Dispose();

                        }
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "GRADING")
                        {
                            //tablename = "GPIL_GRADING_DTLS";
                            strsql = "SELECT GRADE FROM GPIL_GRADING_DTLS WITH(NOLOCK) WHERE GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();


                            dt = rMgt.GetQueryResult(strsql);
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();

                            }
                           


                        }
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "THRESHING")
                        {
                            //tablename = "GPIL_THRESH_RECON_DTLS_1";
                            strsql = "SELECT BATCH_NO,GRADE FROM GPIL_THRESH_RECON_DTLS_1 WITH(NOLOCK) WHERE GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();
                            dt = rMgt.GetQueryResult(strsql);
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["PROCESS_REF_ID"] = dt.Rows[0][0].ToString();
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][1].ToString();

                            }
                            //strsr.Close();
                            //cmd.Dispose();


                        }
                        if (dataset.Tables[0].Rows[s]["PROCESS_NAME"].ToString() == "TAPPURCHASE")
                        {
                            //tablename = "GPIL_TAP_FARM_PURCHS_DTLS";
                            strsql = "SELECT BUYER_GRADE FROM GPIL_TAP_FARM_PURCHS_DTLS WITH(NOLOCK) WHERE GPIL_BALE_NUMBER='" + dataset.Tables[0].Rows[s]["GPIL_BALE_NUMBER"].ToString() + "'";
                            //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //cmd.CommandTimeout = 0;
                            //strsr = cmd.ExecuteReader();
                            dt = rMgt.GetQueryResult(strsql);
                            if (dt.Rows.Count > 0)
                            {
                                dataset.Tables[0].Rows[s]["GRADE"] = dt.Rows[0][0].ToString();

                            }
                            //strsr.Close();
                            //cmd.Dispose();

                        }
                    }
                  
                    dataset.Tables[0].AcceptChanges();
                    //DataSet ds = new DataSet();
                    //ds=dataset.Copy();
                    CustomerReport.Load(Server.MapPath("~/CrystalReport/RptBaleTrack.rpt"));
                    CustomerReport.SetDataSource(dataset.Tables[0].DefaultView);
                  //  CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                    CrystalReportViewer1.ReportSource = CustomerReport;
                    CrystalReportViewer1.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }




    }
}