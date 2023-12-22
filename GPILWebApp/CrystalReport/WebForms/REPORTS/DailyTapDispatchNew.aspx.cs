using CrystalDecisions.CrystalReports.Engine;
using DocumentFormat.OpenXml.Office2010.Excel;
using GPI;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class DailyTapDispatchNew : System.Web.UI.Page
    {

        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();

        public static string errfile;

        public static DataTable objDataTable = new DataTable();
        public static DataTable objDataTable1 = new DataTable();
        public static DataTable objDataTable2 = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExportToExcel0);
           
            scriptManager.RegisterPostBackControl(this.btnExportToExcel1);
            if (!IsPostBack)
            {

                try
                {

                    DataSet ds = new DataSet();
                    ds = crd.GetORGN("CROP", "", "");
                    ddlCrop.DataSource = ds.Tables[0];
                    ddlCrop.DataTextField = "CROPYEAR";
                    ddlCrop.DataValueField = "CROP";
                    ddlCrop.DataBind();
                    ddlCrop.Items.Insert(0, "SELECT CROP YEAR");

                    DataSet ds1 = new DataSet();
                    ds1 = crd.GetORGN("VARIETY", "", "");
                    ddlVariety.DataSource = ds1.Tables[0];
                    ddlVariety.DataTextField = "VARIETYNAME";
                    ddlVariety.DataValueField = "VARIETY";
                    ddlVariety.DataBind();
                    ddlVariety.Items.Insert(0, "SELECT VARIETY CODE");
                    
                }
                catch (Exception ex)
                { }
            }
           
            else
            {
                if(ddlVariety.SelectedIndex != 0 &&  ddlCrop.SelectedIndex != 0)
                {
                    //FuncVoidClearGrid();
                }
               
            }
        }

        public void FuncVoidClearGrid()
        {
            try
            {
                DataSet objDataSet = new DataSet();
                objDataSet.Tables.Add("TEMP");
                objDataSet.Tables[0].Columns.Add("SNO");
                objDataSet.Tables[0].Columns.Add("SHIPMENT_NO");
                objDataSet.Tables[0].Columns.Add("SENDER_ORGN_CODE");
                objDataSet.Tables[0].Columns.Add("SENDER_NO");
                objDataSet.Tables[0].Columns.Add("BALES");
                objDataSet.Tables[0].Columns.Add("QUANTITY");
                objDataSet.Tables[0].Columns.Add("RECEIVER_ORGN_CODE");
                objDataSet.Tables[0].Columns.Add("SENDER_TRUCK_NO");
                objDataSet.Tables[0].Columns.Add("AVE_PRICE");
                objDataSet.Tables[0].Columns.Add("VALUE");
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
                objDataSet1.Tables[0].Columns.Add("GRADE");
                objDataSet1.Tables[0].Columns.Add("BALES");
                objDataSet1.Tables[0].Columns.Add("QUANTITY");
                objDataSet1.Tables[0].Columns.Add("AVE_PRICE");
                objDataSet1.Tables[0].Rows.Add(objDataSet1.Tables[0].NewRow());

                GridView1.DataSource = objDataSet1;
                GridView1.DataBind();
                int columncount1 = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount1;
                GridView1.Rows[0].Cells[0].Text = "No Records Found";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        private void FuncBatchDetails()
        {
            try
            {
                //lblMessage.Text = string.Empty;
                errfile = string.Empty;
                DateTime dt1 = Convert.ToDateTime(txt_From_Date.Text);
                txt_From_Date.Text = dt1.ToString("dd-MM-yyyy");
                //DateTime dt2 = Convert.ToDateTime(txtToDate.Text);
                //txtToDate.Text = dt2.ToString("dd-MM-yyyy");
                if (txt_From_Date.Text == "<--Select-->")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select the date of Dispatch');", true);
                    txt_From_Date.Focus();
                    return;
                }
                else if ((txt_From_Date.Text.Trim().Length) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select the date of Dispatch');", true);
                    txt_From_Date.Focus();
                    return;
                }

                string strCrop, strVariety;
                string strCropVariety = "";

                if (ddlCrop.SelectedIndex == 0)
                {
                    strCrop = "";
                }
                else
                {
                    strCrop = ddlCrop.Text;
                }

                if (ddlVariety.SelectedIndex == 0)
                {
                    strVariety = "";
                }
                else
                {
                    strVariety = ddlVariety.Text;
                }

                strCropVariety = strCrop + strVariety;



                string query = "SELECT ROW_NUMBER() OVER(ORDER BY H.SHIPMENT_NO) AS SNO,H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_NO,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QUANTITY,H.RECEIVER_ORGN_CODE,H.SENDER_TRUCK_NO,ROUND(SUM(P.NET_WT*P.RATE)/SUM(P.NET_WT),2) AS AVE_PRICE,(SUM(D.MARKED_WT)*ROUND(SUM(P.NET_WT*P.RATE)/SUM(P.NET_WT),2)) AS VALUE FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H,GPIL_TAP_FARM_PURCHS_DTLS P WHERE P.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.SHIPMENT_NO=D.SHIPMENT_NO AND H.STATUS IN ('INT','N') AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_From_Date.Text + " 23:59:59',105) AND D.GRADE LIKE '%" + strCropVariety + "%' AND H.SENDER_ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP') GROUP BY H.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_NO,H.RECEIVER_ORGN_CODE,H.SENDER_TRUCK_NO ORDER BY H.SHIPMENT_NO";

                objDataTable = crd.GetQueryResult(query);


                GridViewSamp.DataSource = objDataTable;
                GridViewSamp.DataBind();

                query = "SELECT ROW_NUMBER() OVER(ORDER BY D.GRADE) AS SNO, D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(D.MARKED_WT) AS QUANTITY,ROUND(SUM(P.NET_WT*P.RATE)/SUM(P.NET_WT),2) AS AVE_PRICE FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H,GPIL_TAP_FARM_PURCHS_DTLS P WHERE P.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.SHIPMENT_NO=D.SHIPMENT_NO AND H.STATUS IN ('INT','N') AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_From_Date.Text + " 23:59:59',105) AND D.GRADE LIKE '%" + strCropVariety + "%' AND H.SENDER_ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP') GROUP BY D.GRADE ORDER BY D.GRADE";

                objDataTable1 = crd.GetQueryResult(query);
                GridView1.DataSource = objDataTable1;
                GridView1.DataBind();

                query = "SELECT COUNT(D.GPIL_BALE_NUMBER),SUM(D.MARKED_WT),ROUND(SUM(P.NET_WT*P.RATE)/SUM(P.NET_WT),2) AS AVE_PRICE FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H,GPIL_TAP_FARM_PURCHS_DTLS P WHERE P.GPIL_BALE_NUMBER=D.GPIL_BALE_NUMBER AND H.SHIPMENT_NO=D.SHIPMENT_NO AND H.STATUS IN ('INT','N') AND H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + txt_From_Date.Text + " 00:00:00',105) AND CONVERT(DATETIME,'" + txt_From_Date.Text + " 23:59:59',105) AND D.GRADE LIKE '%" + strCropVariety + "%' AND H.SENDER_ORGN_CODE IN (SELECT ORGN_CODE FROM GPIL_ORGN_MASTER WHERE ORGN_TYPE='TAP')";
                objDataTable2 = crd.GetQueryResult(query);

                if (objDataTable2.Rows.Count > 0)
                {
                    lblSummary.Text = "Total Bales : " + objDataTable2.Rows[0][0].ToString();
                    lblSummary1.Text = "Total Quantity : " + objDataTable2.Rows[0][1].ToString() + " kgs.";
                    lblSummary2.Text = "Total Ave. Price : " + objDataTable2.Rows[0][2].ToString() + " Rs./kg.";
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            FuncBatchDetails();
            //if (txt_From_Date.Text != "<--Select-->"&& txt_From_Date.Text.Trim().Length != 0)
            //{
            //    FuncVoidClearGrid();
            //}
                

        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            ddlCrop.SelectedIndex = 0;
            ddlVariety.SelectedIndex = 0;
            txt_From_Date.Text = "";

        }
        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    ddlCrop.SelectedIndex = 0;
        //    ddlVariety.SelectedIndex = 0;
        //    FuncVoidClearGrid();
        //}

        public void ConvertGridToTable1(out DataTable dt)
        {
            dt = new DataTable();
            try
            {


                dt.Columns.Add("SNO");
                dt.Columns.Add("SHIPMENT_NO");
                dt.Columns.Add("SENDER_ORGN_CODE");
                dt.Columns.Add("SENDER_NO");
                dt.Columns.Add("BALES");
                dt.Columns.Add("QUANTITY");
                dt.Columns.Add("RECEIVER_ORGN_CODE");
                dt.Columns.Add("SENDER_TRUCK_NO");
                dt.Columns.Add("AVE_PRICE");
                dt.Columns.Add("VALUE");
                int i = 0;
                int cnn = GridViewSamp.Rows.Count;
                foreach (GridViewRow row in GridViewSamp.Rows)
                {
                    dt.Rows.Add();

                    if (row.RowType == DataControlRowType.DataRow)
                    {


                        Label lblSNo = (Label)row.FindControl("lblSNo");
                        Label lblShipmentNo = (Label)row.FindControl("lblShipmentNo");
                        Label lblSenderOrg = (Label)row.FindControl("lblSenderOrg");
                        Label lblSenderNo = (Label)row.FindControl("lblSenderNo");
                        Label lblBales = (Label)row.FindControl("lblBales");
                        Label lblQuantity = (Label)row.FindControl("lblQuantity");
                        Label lblReceiverOrg = (Label)row.FindControl("lblReceiverOrg");
                        Label lblLorryNo = (Label)row.FindControl("lblLorryNo");
                        Label lblAvePrice = (Label)row.FindControl("lblAvePrice");
                        Label lblValue = (Label)row.FindControl("lblValue");


                        dt.Rows[i][0] = lblSNo.Text;
                        dt.Rows[i][1] = lblShipmentNo.Text;
                        dt.Rows[i][2] = lblSenderOrg.Text;
                        dt.Rows[i][3] = lblSenderNo.Text;
                        dt.Rows[i][4] = lblBales.Text;
                        dt.Rows[i][5] = lblQuantity.Text;
                        dt.Rows[i][6] = lblReceiverOrg.Text;
                        dt.Rows[i][7] = lblLorryNo.Text;
                        dt.Rows[i][8] = lblAvePrice.Text;
                        dt.Rows[i][9] = lblValue.Text;
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
                dt.Columns.Add("GRADE");
                dt.Columns.Add("BALES");
                dt.Columns.Add("QUANTITY");
                dt.Columns.Add("AVE_PRICE");
                int i = 0;
                int cnn = GridView1.Rows.Count;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    dt.Rows.Add();

                    if (row.RowType == DataControlRowType.DataRow)
                    {


                        Label lblSNo = (Label)row.FindControl("lblSNo1");
                        Label lblGrade = (Label)row.FindControl("lblGrade");
                        Label lblBales = (Label)row.FindControl("lblBales1");
                        Label lblQuantity = (Label)row.FindControl("lblQuantity1");
                        Label lblAvePrice = (Label)row.FindControl("lblAvePrice1");


                        dt.Rows[i][0] = lblSNo.Text;
                        dt.Rows[i][1] = lblGrade.Text;
                        dt.Rows[i][2] = lblBales.Text;
                        dt.Rows[i][3] = lblQuantity.Text;
                        dt.Rows[i][4] = lblAvePrice.Text;
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
            //try
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        string filename = "DAILY-TAP-DISPATCH.xls";
            //        System.IO.StringWriter tw = new System.IO.StringWriter();
            //        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //        DataGrid dgGrid = new DataGrid();
            //        dgGrid.DataSource = dt;
            //        dgGrid.DataBind();

            //        //Get the HTML for the control.
            //        dgGrid.RenderControl(hw);
            //        //Write the HTML back to the browser.
            //        //Response.ContentType = application/vnd.ms-excel;
            //        Response.ContentType = "application/vnd.ms-excel";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //        this.EnableViewState = false;
            //        Response.Write(tw.ToString());
            //        Response.End();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            //}
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = dailydispatch" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls");
            Response.ContentType = "application/vnd.xls";
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridViewSamp.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        public void ExportToExcel2(DataTable dt)
        {
            //try
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        string filename = "DAILY-TAP-DISPATCH-SUMMARY.xls";
            //        System.IO.StringWriter tw = new System.IO.StringWriter();
            //        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //        DataGrid dgGrid = new DataGrid();
            //        dgGrid.DataSource = dt;
            //        dgGrid.DataBind();

            //        //Get the HTML for the control.
            //        dgGrid.RenderControl(hw);
            //        //Write the HTML back to the browser.
            //        //Response.ContentType = application/vnd.ms-excel;
            //        Response.ContentType = "application/vnd.ms-excel";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //        this.EnableViewState = false;
            //        Response.Write(tw.ToString());
            //        Response.End();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            //}



            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = dailydispatch" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls");
            Response.ContentType = "application/vnd.xls";
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();

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
            DataTable dtGrid2 = new DataTable();
            ConvertGridToTable2(out dtGrid2);
            ExportToExcel2(dtGrid2);
        }

    }
}