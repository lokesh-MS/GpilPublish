using CrystalDecisions.CrystalReports.Engine;
using DocumentFormat.OpenXml.Office2010.Excel;
using GPI;
using GPILWebApp.Controllers;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using GPIWebApp;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class GRADINGCHARTPROCESS : System.Web.UI.Page
    {
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();
        ReportManagement rptMgt = new ReportManagement();
        DataServerSync DtserSyc = new DataServerSync();
        public static string errfile;
        

        public static DataTable objDataTable = new DataTable();
        public static DataTable objDataTable1 = new DataTable();
        public static DataTable objDataTable2 = new DataTable();
        int intFuncCode = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
           scriptManager.RegisterPostBackControl(this.btnExportToExcel);

            
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
                    ddlorgcode.DataSource = rptMgt.GetOrnCode();
                    ddlorgcode.DataTextField = "OrgnName";
                    ddlorgcode.DataValueField = "OrgnCode";
                    ddlorgcode.DataBind();
                    ddlorgcode.Items.Insert(0, "< -- Select -- >");
                    //ddlrecipecode.DataSource = rptMgt.GetrecCode();
                    //ddlrecipecode.DataTextField = "DataTextField";
                    //ddlrecipecode.DataValueField = "DataValueField";
                    //ddlrecipecode.DataBind();
                    //ddlrecipecode.Items.Insert(0, "< -- Select -- >");

                    DataSet ds4 = new DataSet();
                    ds4 = crd.GetOperation("RecpCode", "", "");
                    ddlrecipecode.DataSource = ds4.Tables[0];
                    ddlrecipecode.DataTextField = "OPERATION_RECIPE";
                    //cbxRecipe.DataValueField = "RECIPE_CODE";
                    ddlrecipecode.DataBind();
                    ddlrecipecode.Items.Insert(0, "<--Select-->");


                }

                catch (Exception ex)
                { }
            }
            else
            {
                //if (ddlCrop.SelectedIndex != 0)

                //    viewrpt();
            }

        }
        public bool validated()

        {
            if (txt_Report_Date.Text == "<--Select-->" || txt_Report_Date.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_Report_Date.Focus();
                return false;
            }
            else if (txttodate.Text == "<--Select-->" || txttodate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txttodate.Focus();
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
                string strQuery = "";
                string strColQuery = "";

                string strColumnNames = "";
                string strIsNullColumnNames = "";

                string strBYPColumnNames = "";
                string strBYPIsNullColumnNames = "";

                string strCropVariety = "";
                string strCropVarietyCondition = " ";
                string strOrgCondition = " ";
                string strOrgCondition1 = " ";

                if (ddlCrop.SelectedIndex == 0)
                {
                    if (ddlVariety.SelectedIndex == 0)
                    {
                        strCropVariety = "%";
                        strCropVarietyCondition = " ";
                    }
                    else
                    {
                        strCropVariety = "__" + ddlVariety.Text + "%";
                        strCropVarietyCondition = " AND H.VARIETY='" + ddlVariety.Text + "' ";
                    }

                }
                else
                {
                    if (ddlVariety.SelectedIndex == 0)
                    {
                        strCropVariety = ddlCrop.Text + "%";
                        strCropVarietyCondition = " AND H.CROP='" + ddlCrop.Text + "' ";
                    }
                    else
                    {
                        strCropVariety = ddlCrop.Text + ddlVariety.Text + "%";
                        strCropVarietyCondition = " AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' ";
                    }
                }

                
                SqlConnection connection = new SqlConnection(DtserSyc.Constring);

                if (ddlorgcode.SelectedIndex != 0)
                {
                    if (ddlrecipecode.SelectedIndex != 0)
                    {
                        strOrgCondition = " AND H.ORGN_CODE='" + ddlorgcode.Text + "' AND H.RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                        strOrgCondition1 = " where ORGN_CODE='" + ddlorgcode.Text + "' AND RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                    }
                    else
                    {
                        strOrgCondition = " AND H.ORGN_CODE='" + ddlorgcode.Text + "' ";
                        strOrgCondition1 = " where ORGN_CODE='" + ddlorgcode.Text + "' ";
                    }
                }
                else
                {
                    if (ddlrecipecode.SelectedIndex != 0)
                    {
                        strOrgCondition = " AND H.RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                        strOrgCondition1 = " where RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                    }
                }


                strColQuery = "SELECT DISTINCT D.GRADE FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) AND D.GRADE LIKE '" + strCropVariety + "LOSS' " + strOrgCondition + " GROUP BY H.ORGN_CODE,H.RECIPE_CODE,D.GRADE";


                SqlCommand cmdLoss = new SqlCommand(strColQuery, connection);
                cmdLoss.CommandTimeout = 0;
                SqlDataAdapter adpLoss = new SqlDataAdapter(cmdLoss);
                DataTable objDataTableColLoss = new DataTable();
                adpLoss.Fill(objDataTableColLoss);

                string strLossColumnNames = "";
                string strLossIsNullColumnNames = "";

                for (int h = 0; h < objDataTableColLoss.Rows.Count; h++)
                {
                    strLossColumnNames = strLossColumnNames + ",[" + objDataTableColLoss.Rows[h]["GRADE"].ToString() + "]";
                    strLossIsNullColumnNames = strLossIsNullColumnNames + ",ISNULL([" + objDataTableColLoss.Rows[h]["GRADE"].ToString() + "],0) AS [" + objDataTableColLoss.Rows[h]["GRADE"].ToString() + "]";
                }

                strColQuery = "SELECT DISTINCT D.GRADE FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) AND D.GRADE NOT LIKE '" + strCropVariety + "LOSS' " + strOrgCondition + " GROUP BY H.ORGN_CODE,H.RECIPE_CODE,D.GRADE";


                SqlCommand command = new SqlCommand(strColQuery, connection);
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable objDataTableCol = new DataTable();
                adapter.Fill(objDataTableCol);




                for (int i = 0; i < objDataTableCol.Rows.Count; i++)
                {
                    strBYPColumnNames = strBYPColumnNames + ",[" + objDataTableCol.Rows[i]["GRADE"].ToString() + "]";
                    strBYPIsNullColumnNames = strBYPIsNullColumnNames + ",ISNULL([" + objDataTableCol.Rows[i]["GRADE"].ToString() + "],0) AS [" + objDataTableCol.Rows[i]["GRADE"].ToString() + "]";
                }

                strColumnNames = "[IN-CREDIANT],[PRODUCT]" + strBYPColumnNames + strLossColumnNames + ",[OUT-TURN]";
                strIsNullColumnNames = "ISNULL([IN-CREDIANT],0) AS [IN-CREDIANT], ISNULL([PRODUCT],0) AS [PRODUCT]" + strBYPIsNullColumnNames + strLossIsNullColumnNames + ",ISNULL([OUT-TURN],0) AS [OUT-TURN]";


                strQuery = "select ORGN_CODE AS ORGN,RECIPE_CODE AS RECIPE,GRADERS," + strIsNullColumnNames + " from ( select ORGN_CODE,RECIPE_CODE,GRADERS,QTY,GRADE  from  (SELECT TB1.ORGN_CODE,TB1.RECIPE_CODE,TB1.GRADERS ,TB2.GRADE,TB2.QTY FROM (SELECT H.ORGN_CODE,H.RECIPE_CODE,SUM(NO_OF_GRADERS) AS GRADERS FROM GPIL_GRADING_HDR H WHERE H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' " + strCropVarietyCondition + " AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE) AS TB1 LEFT OUTER JOIN (SELECT H.ORGN_CODE,H.RECIPE_CODE,'IN-CREDIANT' AS GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='IPB' AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE UNION SELECT H.ORGN_CODE,H.RECIPE_CODE,'PRODUCT' AS GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE NOT IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE UNION SELECT H.ORGN_CODE,H.RECIPE_CODE,'OUT-TURN' AS GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND D.GRADE LIKE '" + strCropVariety + "' AND H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE UNION SELECT H.ORGN_CODE,H.RECIPE_CODE,D.GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE,D.GRADE) AS TB2 ON TB1.ORGN_CODE =TB2.ORGN_CODE AND TB1.RECIPE_CODE=TB2.RECIPE_CODE ) as mytable " + strOrgCondition1 + " ) d pivot ( sum(QTY) for GRADE in (" + strColumnNames + ") ) piv order by  ORGN_CODE,RECIPE_CODE ";


                SqlCommand cmd = new SqlCommand(strQuery, connection);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(objDataTable);


                double[] dblArySummary = new double[objDataTable.Columns.Count];


                for (int k = 0; k < objDataTable.Rows.Count; k++)
                {
                    for (int j = 0; j < objDataTable.Columns.Count; j++)
                    {
                        if (j > 1)
                        {
                            double intValue = Convert.ToDouble(objDataTable.Rows[k][j].ToString());
                            dblArySummary[j] += intValue;
                        }
                    }

                }




                DataRow objDataRow = objDataTable.NewRow(); ;
                objDataRow["ORGN"] = "SUMMARY";
                objDataRow["RECIPE"] = "GRAND TOTAL";
                for (int l = 0; l < objDataTable.Columns.Count; l++)
                {
                    if (l > 1)
                    {
                        objDataRow[objDataTable.Columns[l].ColumnName] = dblArySummary[l].ToString();
                    }
                }

                objDataTable.Rows.Add(objDataRow);


                DataTable objDataTableTemp = new DataTable();
                objDataTableTemp = objDataTable.Clone();

                foreach (DataColumn objDataColumn in objDataTableTemp.Columns)
                {
                    objDataColumn.DataType = typeof(String);
                }
                objDataTableTemp.Load(objDataTable.CreateDataReader());


                for (int m = 0; m < objDataTableTemp.Rows.Count; m++)
                {
                    for (int n = 0; n < objDataTableTemp.Columns.Count; n++)
                    {
                        if (n > 3 && n < objDataTableTemp.Columns.Count - 1)
                        {
                            double dblValue = Convert.ToDouble(objDataTableTemp.Rows[m][n].ToString());
                            double dblIssue = Convert.ToDouble(objDataTableTemp.Rows[m][3].ToString());
                            double dblPerc = 0;
                            if (dblIssue != 0)
                            {
                                dblPerc = (dblValue / dblIssue) * 100;

                            }

                            //objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0") + " - " + dblPerc.ToString("0.00") + "%";
                            //-----------------------------------------------------------------
                            if (rdoQuantity.Checked)
                            {
                                objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0");
                            }
                            else if (rdoShare.Checked)
                            {
                                objDataTableTemp.Rows[m][n] = dblPerc.ToString("0.00") + "%";
                            }
                            else
                            {
                                objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0") + " - " + dblPerc.ToString("0.00") + "%";
                            }
                            //-----------------------------------------------------------------

                        }
                        else if (n == 3 || n == objDataTableTemp.Columns.Count - 1)
                        {
                            double dblValue = Convert.ToDouble(objDataTableTemp.Rows[m][n].ToString());
                            objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0");
                        }
                    }

                }



                GridViewSamp.DataSource = objDataTableTemp;
                GridViewSamp.DataBind();

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
                try
                {
                    intFuncCode = 1;
                    viewrpt1();
                }
                catch (Exception ex)
                {

                }

            }
            else
            {

            }
        }

        //clear function for gridview
        public void FuncVoidClearGrid()
        {
            try
            {
                DataSet objDataSet = new DataSet();
                objDataSet.Tables.Add("TEMP");
                objDataSet.Tables[0].Columns.Add("ORGN");
                objDataSet.Tables[0].Columns.Add("RECIPE");
                objDataSet.Tables[0].Columns.Add("GRADERS");
                objDataSet.Tables[0].Columns.Add("IN-CREDIANT");
                objDataSet.Tables[0].Columns.Add("PRODUCT");
                objDataSet.Tables[0].Columns.Add("2330SBS");
                objDataSet.Tables[0].Columns.Add("2330LOSS");
                objDataSet.Tables[0].Columns.Add("OUT-TURN");
                //objDataSet.Tables[0].Columns.Add("AVE_PRICE");
                //objDataSet.Tables[0].Columns.Add("VALUE");
                objDataSet.Tables[0].Rows.Add(objDataSet.Tables[0].NewRow());

                GridViewSamp.DataSource = objDataSet;
                GridViewSamp.DataBind();
                int columncount = GridViewSamp.Rows[0].Cells.Count;
                GridViewSamp.Rows[0].Cells.Clear();
                GridViewSamp.Rows[0].Cells.Add(new TableCell());
                GridViewSamp.Rows[0].Cells[0].ColumnSpan = columncount;
                GridViewSamp.Rows[0].Cells[0].Text = "No Records Found";
               

        
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }



        protected void btnclose_Click(object sender, EventArgs e)
        {
            ddlCrop.SelectedIndex = 0;
            ddlVariety.SelectedIndex = 0; 
            ddlVariety.SelectedIndex = 0;
            ddlVariety.SelectedIndex = 0;
            ddlrecipecode.SelectedIndex = 0;
            txt_Report_Date.Text = "<--Select-->";
            txttodate.Text = "<--Select-->";
            FuncVoidClearGrid();

        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void ExportToExcel()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                    GridViewSamp.AllowPaging = false;

                    //this.BindGrid();
                    GridViewSamp.HeaderRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell cell in GridViewSamp.HeaderRow.Cells)
                    {
                        cell.BackColor = GridViewSamp.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in GridViewSamp.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridViewSamp.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridViewSamp.RowStyle.BackColor;
                            }

                            cell.CssClass = "textmode";
                        }
                    }

                    GridViewSamp.RenderControl(hw);
                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void GridViewSamp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSamp.PageIndex = e.NewPageIndex;
            GridViewSamp.DataSource = objDataTable;
            GridViewSamp.DataBind();
        }

        protected void GridViewSamp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int intMainCol = 0;
                    if (intFuncCode == 0)
                    {
                        intMainCol = 2;
                    }
                    else
                    {
                        intMainCol = 3;
                    }

                    e.Row.HorizontalAlign = HorizontalAlign.Right;


                    for (int i = 0; i < intMainCol; i++)
                    {
                        TableCell cell = e.Row.Cells[i];
                        if (e.Row.RowIndex != objDataTable.Rows.Count - 1 && e.Row.RowIndex != objDataTable.Rows.Count)
                        {
                            if (i == 0)
                            {
                                cell.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                cell.BackColor = System.Drawing.Color.White;
                            }
                        }
                        else
                        {
                            cell.BackColor = System.Drawing.Color.White;
                            cell.Font.Bold = true;
                        }

                        cell.HorizontalAlign = HorizontalAlign.Left;
                    }


                    for (int i = intMainCol; i <= objDataTable.Columns.Count - 1; i++)
                    {


                        TableCell cell = e.Row.Cells[i];
                        if (e.Row.RowIndex != objDataTable.Rows.Count - 1 && e.Row.RowIndex != objDataTable.Rows.Count)
                        {
                            if (i == intMainCol + 1 || i == objDataTable.Columns.Count - 1)
                            {
                                cell.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                cell.BackColor = System.Drawing.Color.White;
                            }
                        }
                        else
                        {
                            cell.BackColor = System.Drawing.Color.White;
                            cell.Font.Bold = true;
                        }
                    }

                    //TableRow NRow = e.Row;
                    //if (NRow.Cells[0].Text == "SUMMARY")
                    //{
                    //    NRow.BackColor = Color.Yellow;
                    //}
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        public void viewrpt1()
        {
            try
            {
                string strQuery = "";
                string strColQuery = "";

                string strColumnNames = "";
                string strIsNullColumnNames = "";

                string strBYPColumnNames = "";
                string strBYPIsNullColumnNames = "";

                string strCropVariety = "";
                string strCropVarietyCondition = " ";
                string strOrgCondition = " ";
                string strOrgCondition1 = " ";

                if (ddlCrop.SelectedIndex == 0)
                {
                    if (ddlVariety.SelectedIndex == 0)
                    {
                        strCropVariety = "%";
                        strCropVarietyCondition = " ";
                    }
                    else
                    {
                        strCropVariety = "__" + ddlVariety.Text + "%";
                        strCropVarietyCondition = " AND H.VARIETY='" + ddlVariety.Text + "' ";
                    }

                }
                else
                {
                    if (ddlVariety.SelectedIndex == 0)
                    {
                        strCropVariety = ddlCrop.Text + "%";
                        strCropVarietyCondition = " AND H.CROP='" + ddlCrop.Text + "' ";
                    }
                    else
                    {
                        strCropVariety = ddlCrop.Text + ddlVariety.Text + "%";
                        strCropVarietyCondition = " AND H.CROP='" + ddlCrop.Text + "' AND H.VARIETY='" + ddlVariety.Text + "' ";
                    }
                }


                SqlConnection connection = new SqlConnection(ClsConnection.strConnectionString);


                if (ddlorgcode.SelectedIndex != 0)
                {
                    if (ddlrecipecode.SelectedIndex != 0)
                    {
                        strOrgCondition = " AND H.ORGN_CODE='" + ddlorgcode.Text + "' AND H.RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                        strOrgCondition1 = " where ORGN_CODE='" + ddlorgcode.Text + "' AND RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                    }
                    else
                    {
                        strOrgCondition = " AND H.ORGN_CODE='" + ddlorgcode.Text + "' ";
                        strOrgCondition1 = " where ORGN_CODE='" + ddlorgcode.Text + "' ";
                    }
                }
                else
                {
                    if (ddlrecipecode.SelectedIndex != 0)
                    {
                        strOrgCondition = " AND H.RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                        strOrgCondition1 = " where RECIPE_CODE='" + ddlrecipecode.Text + "' ";
                    }
                }

                strColQuery = "SELECT DISTINCT D.GRADE FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) AND D.GRADE LIKE '" + strCropVariety + "LOSS' " + strOrgCondition + " GROUP BY H.ORGN_CODE,H.RECIPE_CODE,D.GRADE";


                SqlCommand cmdLoss = new SqlCommand(strColQuery, connection);
                cmdLoss.CommandTimeout = 0;
                SqlDataAdapter adpLoss = new SqlDataAdapter(cmdLoss);
                DataTable objDataTableColLoss = new DataTable();
                adpLoss.Fill(objDataTableColLoss);

                string strLossColumnNames = "";
                string strLossIsNullColumnNames = "";

                for (int h = 0; h < objDataTableColLoss.Rows.Count; h++)
                {
                    strLossColumnNames = strLossColumnNames + ",[" + objDataTableColLoss.Rows[h]["GRADE"].ToString() + "]";
                    strLossIsNullColumnNames = strLossIsNullColumnNames + ",ISNULL([" + objDataTableColLoss.Rows[h]["GRADE"].ToString() + "],0) AS [" + objDataTableColLoss.Rows[h]["GRADE"].ToString() + "]";
                }

                strColQuery = "SELECT DISTINCT D.GRADE FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) AND D.GRADE NOT LIKE '" + strCropVariety + "LOSS' " + strOrgCondition + " GROUP BY H.ORGN_CODE,H.RECIPE_CODE,D.GRADE";


                SqlCommand command = new SqlCommand(strColQuery, connection);
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable objDataTableCol = new DataTable();
                adapter.Fill(objDataTableCol);




                for (int i = 0; i < objDataTableCol.Rows.Count; i++)
                {
                    strBYPColumnNames = strBYPColumnNames + ",[" + objDataTableCol.Rows[i]["GRADE"].ToString() + "]";
                    strBYPIsNullColumnNames = strBYPIsNullColumnNames + ",ISNULL([" + objDataTableCol.Rows[i]["GRADE"].ToString() + "],0) AS [" + objDataTableCol.Rows[i]["GRADE"].ToString() + "]";
                }

                strColumnNames = "[IN-CREDIANT],[PRODUCT]" + strBYPColumnNames + strLossColumnNames + ",[OUT-TURN]";
                strIsNullColumnNames = "ISNULL([IN-CREDIANT],0) AS [IN-CREDIANT], ISNULL([PRODUCT],0) AS [PRODUCT]" + strBYPIsNullColumnNames + strLossIsNullColumnNames + ",ISNULL([OUT-TURN],0) AS [OUT-TURN]";


                strQuery = "select ORGN_CODE AS ORGN,RECIPE_CODE AS RECIPE,ISSUED_GRADE AS [ISSUED-GRADE],GRADERS," + strIsNullColumnNames + " from ( select ORGN_CODE,RECIPE_CODE,ISSUED_GRADE,GRADERS,QTY,GRADE  from  (SELECT TB1.ORGN_CODE,TB1.RECIPE_CODE,TB1.ISSUED_GRADE,TB1.GRADERS ,TB2.GRADE,TB2.QTY FROM (SELECT H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE,SUM(NO_OF_GRADERS) AS GRADERS FROM GPIL_GRADING_HDR H WHERE H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' " + strCropVarietyCondition + " AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE) AS TB1 LEFT OUTER JOIN (SELECT H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE,'IN-CREDIANT' AS GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='IPB' AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE UNION SELECT H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE,'PRODUCT' AS GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE NOT IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE UNION SELECT H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE,'OUT-TURN' AS GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND D.GRADE LIKE '" + strCropVariety + "' AND H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE UNION SELECT H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE,D.GRADE,ROUND(SUM(D.MARKED_WT),1) AS QTY FROM GPIL_GRADING_DTLS D,GPIL_GRADING_HDR H WHERE H.BATCH_NO=D.BATCH_NO AND H.STATUS IN ('N','NN') AND ISNULL(H.ATTRIBUTE3,'')<>'N' AND D.BALE_TYPE='OPB' AND PRODUCT_TYPE IN ('BP','LOSS') AND D.GRADE LIKE '" + strCropVariety + "' AND  H.CREATED_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txttodate.Text + " 23:59:59',102) GROUP BY H.ORGN_CODE,H.RECIPE_CODE,H.ISSUED_GRADE,D.GRADE) AS TB2 ON TB1.ORGN_CODE =TB2.ORGN_CODE AND TB1.RECIPE_CODE=TB2.RECIPE_CODE AND TB1.ISSUED_GRADE=TB2.ISSUED_GRADE ) as mytable " + strOrgCondition1 + " ) d pivot ( sum(QTY) for GRADE in (" + strColumnNames + ") ) piv order by  ORGN_CODE,RECIPE_CODE,ISSUED_GRADE ";


                SqlCommand cmd = new SqlCommand(strQuery, connection);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(objDataTable);


                double[] dblArySummary = new double[objDataTable.Columns.Count];


                for (int k = 0; k < objDataTable.Rows.Count; k++)
                {
                    for (int j = 0; j < objDataTable.Columns.Count; j++)
                    {
                        if (j > 2)
                        {
                            double intValue = Convert.ToDouble(objDataTable.Rows[k][j].ToString());
                            dblArySummary[j] += intValue;
                        }
                    }

                }




                DataRow objDataRow = objDataTable.NewRow(); ;
                objDataRow["ORGN"] = "SUMMARY";
                objDataRow["RECIPE"] = "GRAND TOTAL";
                objDataRow["ISSUED-GRADE"] = "";
                for (int l = 0; l < objDataTable.Columns.Count; l++)
                {
                    if (l > 2)
                    {
                        objDataRow[objDataTable.Columns[l].ColumnName] = dblArySummary[l].ToString();
                    }
                }

                objDataTable.Rows.Add(objDataRow);


                DataTable objDataTableTemp = new DataTable();
                objDataTableTemp = objDataTable.Clone();

                foreach (DataColumn objDataColumn in objDataTableTemp.Columns)
                {
                    objDataColumn.DataType = typeof(String);
                }
                objDataTableTemp.Load(objDataTable.CreateDataReader());


                for (int m = 0; m < objDataTableTemp.Rows.Count; m++)
                {
                    for (int n = 0; n < objDataTableTemp.Columns.Count; n++)
                    {
                        if (n > 4 && n < objDataTableTemp.Columns.Count - 1)
                        {
                            double dblValue = Convert.ToDouble(objDataTableTemp.Rows[m][n].ToString());
                            double dblIssue = Convert.ToDouble(objDataTableTemp.Rows[m][4].ToString());
                            double dblPerc = 0;
                            if (dblIssue != 0)
                            {
                                dblPerc = (dblValue / dblIssue) * 100;

                            }

                            //objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0") + "  - " + dblPerc.ToString("0.00") + "%";
                            //-----------------------------------------------------------------
                            if (rdoQuantity.Checked)
                            {
                                objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0");
                            }
                            else if (rdoShare.Checked)
                            {
                                objDataTableTemp.Rows[m][n] = dblPerc.ToString("0.00") + "%";
                            }
                            else
                            {
                                objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0") + "  - " + dblPerc.ToString("0.00") + "%";
                            }
                            //-----------------------------------------------------------------

                        }
                        else if (n == 4 || n == objDataTableTemp.Columns.Count - 1)
                        {
                            double dblValue = Convert.ToDouble(objDataTableTemp.Rows[m][n].ToString());
                            objDataTableTemp.Rows[m][n] = dblValue.ToString("0.0");
                        }
                    }

                }

                DataRow objDataLastRow = objDataTableTemp.NewRow(); ;
                for (int l = 0; l < objDataTableTemp.Columns.Count; l++)
                {
                    if (l == 0)
                    {
                        objDataLastRow[objDataTableTemp.Columns[l].ColumnName] = "*******";
                    }
                    else if (l == 1)
                    {
                        objDataLastRow[objDataTableTemp.Columns[l].ColumnName] = "********************";
                    }
                    else if (l == 3)
                    {
                        objDataLastRow[objDataTableTemp.Columns[l].ColumnName] = "**********";
                    }
                    else if (l == 4 || l == objDataTableTemp.Columns.Count - 1)
                    {
                        objDataLastRow[objDataTableTemp.Columns[l].ColumnName] = "**************";
                    }
                    else
                    {
                        objDataLastRow[objDataTableTemp.Columns[l].ColumnName] = "******************";
                    }

                }
                objDataTableTemp.Rows.Add(objDataLastRow);



                GridViewSamp.DataSource = objDataTableTemp;
                GridViewSamp.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

    }
    }
