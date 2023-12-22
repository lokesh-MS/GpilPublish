using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using GPILWebApp.ViewModel;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class BaleWeightLossReport : System.Web.UI.Page
    {
        public static string errfile;

        DataSet exceldata = new DataSet();

        public static DataTable objDataTable = new DataTable();
        public static DataTable objDataTable1 = new DataTable();
        public static DataTable objDataTable2 = new DataTable();


        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExportToExcel0);

            scriptManager.RegisterPostBackControl(this.btnExportToExcel1);
            if (!IsPostBack)
            {
                bindDropDown();
                FuncVoidClearGrid();
                txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            }
        }

        private void bindDropDown()
        {
            try
            {
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("ORGN", "", "");
                ddlOrgCode.DataSource = ds2.Tables[0];
                ddlOrgCode.DataTextField = "ORGN_CODE1";
                ddlOrgCode.DataValueField = "ORGN_CODE";
                ddlOrgCode.DataBind();
                ddlOrgCode.Items.Insert(0, "SELECT ORGN CODE");

            }
            catch (Exception ex)
            { }
        }

        public void FuncVoidClearGrid()
        {

            DataSet objDataSet = new DataSet();
            objDataSet.Tables.Add("TEMP");
            objDataSet.Tables[0].Columns.Add("SNO");
            objDataSet.Tables[0].Columns.Add("SENDER_ORGN_CODE");
            objDataSet.Tables[0].Columns.Add("SHIPMENT_NO");
            objDataSet.Tables[0].Columns.Add("BALES");
            objDataSet.Tables[0].Columns.Add("MARKED_WT");
            objDataSet.Tables[0].Columns.Add("RECEIPT_WT");
            objDataSet.Tables[0].Columns.Add("W_BALES");
            objDataSet.Tables[0].Columns.Add("W_DISPATCHED_WT");
            objDataSet.Tables[0].Columns.Add("W_RECEIPT_WT");
            objDataSet.Tables[0].Columns.Add("DIFF_WT");
            objDataSet.Tables[0].Columns.Add("PERC");
            objDataSet.Tables[0].Rows.Add(objDataSet.Tables[0].NewRow());

            GridViewSamp.DataSource = objDataSet;
            GridViewSamp.DataBind();
            int columncount = GridViewSamp.Rows[0].Cells.Count;
            GridViewSamp.Rows[0].Cells.Clear();
            GridViewSamp.Rows[0].Cells.Add(new TableCell());
            GridViewSamp.Rows[0].Cells[0].ColumnSpan = columncount;
            GridViewSamp.Rows[0].Cells[0].Text = "No Records Found";




            DataSet objDataSet1 = new DataSet();
            objDataSet1.Tables.Add("TEMP");
            objDataSet1.Tables[0].Columns.Add("SNO");
            objDataSet1.Tables[0].Columns.Add("SENDER_ORGN_CODE");
            objDataSet1.Tables[0].Columns.Add("SHIPMENT_NO");
            objDataSet1.Tables[0].Columns.Add("GPIL_BALE_NUMBER");
            objDataSet1.Tables[0].Columns.Add("MARKED_WT");
            objDataSet1.Tables[0].Columns.Add("RECEIPT_WEIGHT");
            objDataSet1.Tables[0].Columns.Add("DIFF_WT");
            objDataSet1.Tables[0].Columns.Add("PERC");
            objDataSet1.Tables[0].Rows.Add(objDataSet1.Tables[0].NewRow());

            GridView1.DataSource = objDataSet1;
            GridView1.DataBind();
            int columncount1 = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount1;
            GridView1.Rows[0].Cells[0].Text = "No Records Found";

        }


        private void FuncBatchDetails()
        {
            try
            {
                lblMessage.Text = string.Empty;
                errfile = string.Empty;


                if (txtFromDate.Text == "<--Select-->")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select the date of Receipt');", true);
                    txtFromDate.Focus();
                    return;
                }
                else if ((txtFromDate.Text.Trim().Length) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select the date of Receipt');", true);
                    txtFromDate.Focus();
                    return;
                }
                else if (txtToDate.Text == "<--Select-->")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select the date of Receipt');", true);
                    txtToDate.Focus();
                    return;
                }
                else if ((txtToDate.Text.Trim().Length) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select the date of Receipt');", true);
                    txtToDate.Focus();
                    return;
                }

                string strWeighmentType = "TWT";
                if (rdo100.Checked == true)
                {
                    strWeighmentType = "HND";
                }

                string query = "SELECT ROW_NUMBER() OVER(ORDER BY TBL1.SENDER_ORGN_CODE,TBL1.SHIPMENT_NO) AS SNO,TBL1.SENDER_ORGN_CODE,TBL1.SHIPMENT_NO,TBL1.BALES,TBL1.DISPATCHED_WT AS MARKED_WT,TBL1.RECEIPT_WT,TBL2.W_BALES,TBL2.W_DISPATCHED_WT,TBL2.W_RECEIPT_WT,TBL2.DIFF_WT,TBL2.PERC FROM (SELECT SENDER_ORGN_CODE,H.SHIPMENT_NO,COUNT(GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(ISNULL(MARKED_WT,0)),1) AS DISPATCHED_WT,ROUND(SUM(ISNULL(RECEIPT_WEIGHT,0)),1) AS RECEIPT_WT  FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE D.SHIPMENT_NO=H.SHIPMENT_NO AND H.RECEIVER_ORGN_CODE='" + ddlOrgCode.Text + "' AND H.STATUS IN ('N','P') AND H.RECEIVED_DATE BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txtToDate.Text + " 23:59:59',102) GROUP BY SENDER_ORGN_CODE,H.SHIPMENT_NO) AS TBL1,(SELECT SENDER_ORGN_CODE,H.SHIPMENT_NO,COUNT(GPIL_BALE_NUMBER) AS W_BALES,ROUND(SUM(ISNULL(MARKED_WT,0)),1) AS W_DISPATCHED_WT,ROUND(SUM(ISNULL(RECEIPT_WEIGHT,0)),1) AS W_RECEIPT_WT,ROUND((SUM(ISNULL(MARKED_WT,0))-SUM(ISNULL(RECEIPT_WEIGHT,0))),1) AS DIFF_WT, ROUND(100 * ((SUM(ISNULL(MARKED_WT,0))-SUM(ISNULL(RECEIPT_WEIGHT,0))) / SUM(ISNULL(MARKED_WT,0))),2) AS PERC  FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE D.SHIPMENT_NO=H.SHIPMENT_NO AND H.RECEIVER_ORGN_CODE='" + ddlOrgCode.Text + "' AND H.STATUS IN ('N','P') AND ISNULL(H.RECEV_WEIGH_TYPE,'TWT') ='" + strWeighmentType + "' AND D.WEIGHT_STATUS='Y' AND H.RECEIVED_DATE BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txtToDate.Text + " 23:59:59',102) GROUP BY SENDER_ORGN_CODE,H.SHIPMENT_NO) AS TBL2 WHERE TBL1.SENDER_ORGN_CODE=TBL2.SENDER_ORGN_CODE AND TBL1.SHIPMENT_NO=TBL2.SHIPMENT_NO ORDER BY TBL1.SENDER_ORGN_CODE,TBL1.SHIPMENT_NO";
                objDataTable.Clear();
                objDataTable = crd.GetQueryResult(query);
                GridViewSamp.DataSource = objDataTable;
                GridViewSamp.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        protected void lnkView_Click(object sender, EventArgs e)
        {
            //GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            //string varShipmentNo = grdrow.Cells[2].Text;        
            //BindData(varShipmentNo);
        }


        private void BindData(string varShipmentNo)
        {
            try
            {
                lblMessage.Text = string.Empty;
                errfile = string.Empty;
                objDataTable2.Clear();

                string query = "SELECT ROW_NUMBER() OVER(ORDER BY SENDER_ORGN_CODE,H.SHIPMENT_NO) AS SNO,SENDER_ORGN_CODE,H.SHIPMENT_NO,GPIL_BALE_NUMBER,ROUND((ISNULL(MARKED_WT,0)),1) AS MARKED_WT,ROUND((ISNULL(RECEIPT_WEIGHT,0)),1) AS RECEIPT_WEIGHT,ROUND(((ISNULL(MARKED_WT,0))-(ISNULL(RECEIPT_WEIGHT,0))),1) AS DIFF_WT, ROUND(100 * (((ISNULL(MARKED_WT,0))-(ISNULL(RECEIPT_WEIGHT,0))) / (ISNULL(MARKED_WT,0))),2) AS PERC  FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE D.WEIGHT_STATUS='Y' AND D.SHIPMENT_NO=H.SHIPMENT_NO AND H.SHIPMENT_NO='" + varShipmentNo + "'";
                objDataTable.Clear();
                objDataTable = crd.GetQueryResult(query);
                GridView1.DataSource = objDataTable;
                GridView1.DataBind();

                objDataTable2 = objDataTable;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnview_Click(object sender, EventArgs e)
        {
            FuncBatchDetails();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlOrgCode.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            FuncVoidClearGrid();
            txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }


        public void ConvertGridToTable1(out DataTable dt)
        {
            dt = new DataTable();
            try
            {


                dt.Columns.Add("SNO");
                dt.Columns.Add("SENDER_ORGN_CODE");
                dt.Columns.Add("SHIPMENT_NO");
                dt.Columns.Add("BALES");
                dt.Columns.Add("MARKED_WT");
                dt.Columns.Add("RECEIPT_WT");
                dt.Columns.Add("W_BALES");
                dt.Columns.Add("W_DISPATCHED_WT");
                dt.Columns.Add("W_RECEIPT_WT");
                dt.Columns.Add("DIFF_WT");
                dt.Columns.Add("PERC");
                int i = 0;
                int cnn = GridViewSamp.Rows.Count;
                foreach (GridViewRow row in GridViewSamp.Rows)
                {
                    dt.Rows.Add();

                    if (row.RowType == DataControlRowType.DataRow)
                    {


                        Label lblAtt1 = (Label)row.FindControl("lblAtt1");
                        Label lblAtt2 = (Label)row.FindControl("lblAtt2");
                        Label lblAtt3 = (Label)row.FindControl("lblAtt3");
                        Label lblAtt4 = (Label)row.FindControl("lblAtt4");
                        Label lblAtt5 = (Label)row.FindControl("lblAtt5");
                        Label lblAtt6 = (Label)row.FindControl("lblAtt6");
                        Label lblAtt7 = (Label)row.FindControl("lblAtt7");
                        Label lblAtt8 = (Label)row.FindControl("lblAtt8");
                        Label lblAtt9 = (Label)row.FindControl("lblAtt9");
                        Label lblAtt10 = (Label)row.FindControl("lblAtt10");
                        Label lblAtt11 = (Label)row.FindControl("lblAtt11");


                        dt.Rows[i][0] = lblAtt1.Text;
                        dt.Rows[i][1] = lblAtt2.Text;
                        dt.Rows[i][2] = lblAtt3.Text;
                        dt.Rows[i][3] = lblAtt4.Text;
                        dt.Rows[i][4] = lblAtt5.Text;
                        dt.Rows[i][5] = lblAtt6.Text;
                        dt.Rows[i][6] = lblAtt7.Text;
                        dt.Rows[i][7] = lblAtt8.Text;
                        dt.Rows[i][8] = lblAtt9.Text;
                        dt.Rows[i][9] = lblAtt10.Text;
                        dt.Rows[i][10] = lblAtt11.Text;
                        i = i + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error While Exporting data please try again');", true);
            }

        }

        public void ConvertGridToTable2(out DataTable dt)
        {
            dt = new DataTable();
            try
            {


                dt.Columns.Add("SNO");
                dt.Columns.Add("SENDER_ORGN_CODE");
                dt.Columns.Add("SHIPMENT_NO");
                dt.Columns.Add("GPIL_BALE_NUMBER");
                dt.Columns.Add("MARKED_WT");
                dt.Columns.Add("RECEIPT_WEIGHT");
                dt.Columns.Add("DIFF_WT");
                dt.Columns.Add("PERC");

                int i = 0;
                int cnn = GridView1.Rows.Count;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    dt.Rows.Add();

                    if (row.RowType == DataControlRowType.DataRow)
                    {


                        Label lblAtt1 = (Label)row.FindControl("lblAtt1");
                        Label lblAtt2 = (Label)row.FindControl("lblAtt2");
                        Label lblAtt3 = (Label)row.FindControl("lblAtt3");
                        Label lblAtt4 = (Label)row.FindControl("lblAtt4");
                        Label lblAtt5 = (Label)row.FindControl("lblAtt5");
                        Label lblAtt6 = (Label)row.FindControl("lblAtt6");
                        Label lblAtt7 = (Label)row.FindControl("lblAtt7");


                        dt.Rows[i][0] = lblAtt1.Text;
                        dt.Rows[i][1] = lblAtt2.Text;
                        dt.Rows[i][2] = lblAtt3.Text;
                        dt.Rows[i][3] = lblAtt4.Text;
                        dt.Rows[i][4] = lblAtt5.Text;
                        dt.Rows[i][5] = lblAtt6.Text;
                        dt.Rows[i][6] = lblAtt7.Text;

                        i = i + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error While Exporting data please try again');", true);
            }

        }


        public void ExportToExcel1(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string filename = "CHECKMATE-REPORT.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=dailydispatch" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
                ////Response.Clear();
                ////Response.AddHeader("content-disposition", "attachment;filename = baleweightlossreport" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls");
                ////Response.ContentType = "application/vnd.xls";
                ////StringWriter stringWrite = new StringWriter();
                ////HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                ////GridViewSamp.RenderControl(htmlWrite);
                ////Response.Write(stringWrite.ToString());
                ////Response.End();
            }
        }

        public void ExportToExcel2(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string filename = "CHECKMATE-RECEIPT-SUMMARY.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
            ////Response.Clear();
            ////Response.AddHeader("content-disposition", "attachment;filename = Baleweightlossreport" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls");
            ////Response.ContentType = "application/vnd.xls";
            ////StringWriter stringWrite = new StringWriter();
            ////HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ////GridView1.RenderControl(htmlWrite);
            ////Response.Write(stringWrite.ToString());
            ////Response.End();
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dtGrid1 = new DataTable();
            ConvertGridToTable1(out dtGrid1);
            ExportToExcel1(dtGrid1);



        }

        protected void GridViewSamp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSamp.PageIndex = e.NewPageIndex;
            GridViewSamp.DataSource = objDataTable;
            GridViewSamp.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = objDataTable1;
            GridView1.DataBind();

        }
        protected void btnExportToExcel1_Click(object sender, EventArgs e)
        {
            //DataTable dtGrid2 = new DataTable();
            //ConvertGridToTable2(out dtGrid2);
            ExportToExcel2(objDataTable2);
        }

        protected void GridViewSamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int RowId = Convert.ToInt16(GridViewSamp.SelectedRow);
            //int RowId = Convert.ToInt16(GridViewSamp.Attributes["RowIndex"]);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int RowId = gvRow.RowIndex;
            string sShipmentNo = (GridViewSamp.Rows[RowId].FindControl("lblAtt3") as Label).Text;
            BindData(sShipmentNo);
        }

    }
}