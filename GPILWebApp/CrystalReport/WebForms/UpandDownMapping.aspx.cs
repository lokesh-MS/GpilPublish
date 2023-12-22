using System;
using GPI;
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
using GPILWebApp.ViewModel;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class UpandDownMapping : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDropDown();
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


        protected void gvbind()
        {
            try
            {
                string strQuery;
                string strCropVariety = "";
                string strCrop = "";
                string strVariety = "";
               // lblMessage.Text = "";
                lblRecordCount.Text = "";

                GridViewSamp.DataSource = null;
                GridViewSamp.DataBind();
                if (ddlCrop.SelectedIndex > 0 && ddlVariety.SelectedIndex > 0)
                {
                    strCropVariety = ddlCrop.Text + ddlVariety.Text;
                    strCrop = ddlCrop.Text;
                    strVariety = ddlVariety.Text;
                    
                    strQuery = "((SELECT DISTINCT T1.CROP AS CROP,T1.VARIETY AS VARIETY,T1.ISSUED AS BUYER_GRADE_GRP ,T2.CLASSIFIED AS CLASSIFIER_GRADE_GRP,'None' AS PAIR_TYPE FROM " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,ISSUED_GRADE,ITEM_CODE_GROUP AS ISSUED,CROP,VARIETY FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE LIKE '" + strCropVariety + "%' AND ITEM_CODE=ISSUED_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0'  AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T1, " +
                                "(SELECT DISTINCT GPIL_BALE_NUMBER,CLASSIFICATION_GRADE,ITEM_CODE_GROUP AS CLASSIFIED,CROP,VARIETY FROM GPIL_CLASSIFICATION_DTLS(NOLOCK),GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE LIKE '" + strCropVariety + "%' AND ITEM_CODE=CLASSIFICATION_GRADE AND BATCH_NO IN (SELECT BATCH_NO FROM GPIL_CLASSIFICATION_HDR(NOLOCK) WHERE REASONING_CODE='0'  AND STATUS='N' AND ISNULL(ATTRIBUTE3,'')<>'N')) T2 " +
                                "WHERE T1.GPIL_BALE_NUMBER=T2.GPIL_BALE_NUMBER AND T1.CROP=T2.CROP AND T1.VARIETY=T2.VARIETY ) " +
                                "EXCEPT(SELECT '" + strCrop + "' AS CROP,'" + strVariety + "' AS VARIETY,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,'None' AS PAIR_TYPE FROM GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) WHERE ATTRIBUTE1='" + strCrop + "' AND ATTRIBUTE2='" + strVariety + "'))  " +
                                "ORDER BY T1.ISSUED,T2.CLASSIFIED ";
                    DataSet ds = new DataSet();
                    ds = crd.GetQueryResultDs( strQuery);                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridViewSamp.DataSource = ds;
                        GridViewSamp.DataBind();
                        lblRecordCount.Text = "Unpair Count : " + ds.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        lblRecordCount.Text = "Unpair Count : 0";
                    }

                }
                else
                {
                    lblMessage.Text = "Please Select Crop & Variety";
                }




            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
               
            }

        }

        protected void GridViewSamp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSamp.PageIndex = e.NewPageIndex;
            gvbind();

        }
        protected void GridViewSamp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSamp.EditIndex = -1;
            gvbind();

        }
        protected void GridViewSamp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSamp.EditIndex = e.NewEditIndex;
            gvbind();

        }

      
        protected void GridViewSamp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlPairType = (e.Row.FindControl("ddlPairType") as DropDownList);
                if (ddlPairType == null)
                { }
                else
                {
                    //ddlrejtype.DataSource = GetData("select distinct REJ_TYPE From GPIL_REJECTION_TYPE");
                    //ddlrejtype.DataTextField = "REJ_TYPE";
                    //ddlrejtype.DataValueField = "REJ_TYPE";
                    //ddlrejtype.DataBind();

                    //Add Default Item in the DropDownList
                    ddlPairType.Items.Insert(0, new ListItem("None"));
                    ddlPairType.Items.Insert(1, new ListItem("Up"));
                    ddlPairType.Items.Insert(2, new ListItem("Equal"));
                    ddlPairType.Items.Insert(3, new ListItem("Down"));

                    try
                    {
                        ddlPairType.Items.FindByValue("None").Selected = true;
                    }
                    catch
                    {
                    }

                }
            }
        }
        protected void btnAddToMaster_Click(object sender, EventArgs e)
        {
            try
            {              
                int cnn = GridViewSamp.Rows.Count;
                foreach (GridViewRow row in GridViewSamp.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        //CheckBox chkRow = (CheckBox)row.FindControl("chkDispatch");
                        DropDownList ddlPairType = (DropDownList)row.FindControl("ddlPairType");

                        if (ddlPairType.SelectedIndex > 0)
                        {
                            string strQuery, strBuyerGrdGrp, strClassifierGrdGrp, strPairType, strPairCode;
                            string strCrop, strVariety, strCropVariety;

                            Label lblBuyerGrdGrp = (Label)row.FindControl("lblBuyerGradeGroup");
                            Label lblClassifierGrdGrp = (Label)row.FindControl("lblClassifierGradeGroup");
                            Label lblCrop = (Label)row.FindControl("lblCrop");
                            Label lblVariety = (Label)row.FindControl("lblVariety");


                            strBuyerGrdGrp = lblBuyerGrdGrp.Text;
                            strClassifierGrdGrp = lblClassifierGrdGrp.Text;
                            strPairType = ddlPairType.SelectedValue;

                            strCrop = lblCrop.Text;
                            strVariety = lblVariety.Text;
                            strCropVariety = strCrop + strVariety;


                            if (strPairType == "Up")
                            {
                                strPairCode = "U";
                            }
                            else if (strPairType == "Down")
                            {
                                strPairCode = "D";
                            }
                            else if (strPairType == "Equal")
                            {
                                strPairCode = "E";
                            }
                            else
                            {
                                return;
                            }

                            strQuery = "INSERT INTO GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER (PAIR_CODE,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,PAIR_TYPE,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2) VALUES ('" + strCropVariety + "-" + strBuyerGrdGrp + "-" + strClassifierGrdGrp + "','" + strBuyerGrdGrp + "','" + strClassifierGrdGrp + "','" + strPairCode + "','" + Session["UserId"].ToString()+ "',GETDATE(),'Y','" + strCrop + "','" + strVariety + "')";
                            GPIWebApp.DataServerSync.Instance.ExecuteNonQuery(strQuery);

                        }
                    }
                }
                gvbind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error While Updating data please try again');", true);
            }
            finally
            {
                
            }

        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dtGrid = new DataTable();
            ConvertGridToTable(out dtGrid);
            ExportToExcel(dtGrid);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void ConvertGridToTable(out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                dt.Columns.Add("CROP");
                dt.Columns.Add("VARIETY");
                dt.Columns.Add("BUYER_GRADE_GRP");
                dt.Columns.Add("CLASSIFIER_GRADE_GRP");
                dt.Columns.Add("PAIR_TYPE");

                int i = 0;
                int cnn = GridViewSamp.Rows.Count;
                foreach (GridViewRow row in GridViewSamp.Rows)
                {
                    dt.Rows.Add();

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string strQuery, strBuyerGrdGrp, strClassifierGrdGrp, strPairType, strPairCode, strCrop, strVariety;

                        Label lblBuyerGrdGrp = (Label)row.FindControl("lblBuyerGradeGroup");
                        Label lblClassifierGrdGrp = (Label)row.FindControl("lblClassifierGradeGroup");

                        Label lblCrop = (Label)row.FindControl("lblCrop");
                        Label lblVariety = (Label)row.FindControl("lblVariety");


                        strBuyerGrdGrp = lblBuyerGrdGrp.Text;
                        strClassifierGrdGrp = lblClassifierGrdGrp.Text;


                        strCrop = lblCrop.Text;
                        strVariety = lblVariety.Text;

                        dt.Rows[i][0] = strCrop;
                        dt.Rows[i][1] = strVariety;
                        dt.Rows[i][2] = strBuyerGrdGrp;
                        dt.Rows[i][3] = strClassifierGrdGrp;
                        i = i + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error While Exporting data please try again');", true);
            }

        }


        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string filename = "Sheet1.xls";
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
            }
            catch (Exception ex) { }
        }

        protected void ExportToExcel(object sender, EventArgs e)
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
                    GridViewSamp.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex) { }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            gvbind();
        }


    }
}