using GPI;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class FarmerPurchaseLoanLoader : System.Web.UI.Page
    {
        DataSet exceldata = new DataSet();
        OleDbDataAdapter data;
        public static DataTable dtclstr = new DataTable();
        public static DataTable orgdata = new DataTable();
        public static DataSet purdata = new DataSet();
        double mrkdwt;
        string processts;
        public string filename;
        public string temprefno;
        public static string thresisserr = string.Empty;
        public static string errfile;

        TPLoader tpLoader = new TPLoader();
        protected void Page_Load(object sender, EventArgs e)
        {
            // lblMessage.Text = "";
            try
            {
                Response.ClearHeaders();
                Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
                Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1
                Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1
                Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.1

                //This code is used to maintain UserName in the Home page using Session and Cookies 

                if (Session["SessionUserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                else
                {

                    IEnumerator mc;
                    mc = Request.Cookies.AllKeys.GetEnumerator();

                    while (mc.MoveNext())
                    {

                        if (Request.Cookies[mc.Current.ToString()].HasKeys == true)
                        {

                            IEnumerator sc;
                            sc = Request.Cookies[mc.Current.ToString()].Value.GetEnumerator();

                            while (sc.MoveNext())
                            {

                                //Response.Write(sc.Current.ToString() + Request.Cookies[mc.Current.ToString()][sc.Current.ToString()]); 
                            }
                        }
                    }
                }
                if (!IsPostBack)
                {

                    clrgridview();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                errfile = string.Empty;
                btndwnerr.Visible = false;
                filename = fileuploaditem.FileName;
                string path = string.Concat(Server.MapPath("~/TempFiles/"), filename);
                OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
                fileuploaditem.SaveAs(path);

                string query = "select CROP,VARIETY,FARMER_CODE,LOAN_AMOUNT,'V' AS INS_STS from [Sheet1$]";
                data = new OleDbDataAdapter(query, oconn);
                dtclstr.Clear();
                data.Fill(dtclstr);
                GridViewSample.DataSource = dtclstr;
                GridViewSample.DataBind();
                GridViewSample.EditIndex = -1;
                data.Dispose();
                //string query1 = "select distinct CROP,VARIETY,FARMER_CODE,LOAN_AMOUNT from [Sheet1$]";
                string query1 = "select CROP,VARIETY,FARMER_CODE,LOAN_AMOUNT from [Sheet1$] GROUP BY CROP,VARIETY,FARMER_CODE,LOAN_AMOUNT";
                data = new OleDbDataAdapter(query1, oconn);
                orgdata.Clear();
                data.Fill(orgdata);

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }


        private void LoadData()
        {
            GridViewSample.DataSource = dtclstr;
            GridViewSample.DataBind();
        }


        protected void GridViewSample_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSample.PageIndex = e.NewPageIndex;
            LoadData();

        }

        protected void GridViewSample_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSample.EditIndex = -1;
            LoadData();
        }

        protected void GridViewSample_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSample.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void GridViewSample_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblCrop = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblCrop");
                Label lblVariety = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblVariety");

                Label lblFarmerCode = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblFarmerCode");


                TextBox flg = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtupdatests");

                DataRow[] rows = dtclstr.Select("FARMER_CODE ='" + lblFarmerCode.Text + "' AND CROP='" + lblCrop.Text + "' AND VARIETY='" + lblVariety.Text + "'");

                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["CROP"] = lblCrop.Text.Trim().ToUpper();
                        row["VARIETY"] = lblVariety.Text.Trim().ToUpper();

                        row["FARMER_CODE"] = lblFarmerCode.Text.Trim().ToUpper();

                        row["INS_STS"] = flg.Text.Trim();

                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }


                GridViewSample.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record updated successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void GridViewSample_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblCrop = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblCrop");
                Label lblVariety = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblVariety");


                Label lblFarmerCode = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblFarmerCode");

                DataRow[] rowsdel = dtclstr.Select("FARMER_CODE ='" + lblFarmerCode.Text + "' AND CROP='" + lblCrop.Text + "' AND VARIETY='" + lblVariety.Text + "'");

                foreach (var rows in rowsdel)
                    rows.Delete();
                GridViewSample.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record deleted successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            clrgridview();
            cleardataset();
            dtclstr.Clear();
            lblMessage.Text = string.Empty;
            errfile = string.Empty;
            btndwnerr.Visible = false;
        }

        public void clrgridview()
        {
            DataSet clrds = new DataSet();
            clrds.Tables.Add("TEMP");
            clrds.Tables[0].Columns.Add("CROP");
            clrds.Tables[0].Columns.Add("VARIETY");
            clrds.Tables[0].Columns.Add("FARMER_CODE");
            clrds.Tables[0].Columns.Add("LOAN_AMOUNT");
            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            GridViewSample.DataSource = clrds;
            GridViewSample.DataBind();
            int columncount = GridViewSample.Rows[0].Cells.Count;
            GridViewSample.Rows[0].Cells.Clear();
            GridViewSample.Rows[0].Cells.Add(new TableCell());
            GridViewSample.Rows[0].Cells[0].ColumnSpan = columncount;
            GridViewSample.Rows[0].Cells[0].Text = "No Records Found";
        }

        public void cleardataset()
        {
            while (purdata.Tables.Count > 0)
            {
                DataTable tb = purdata.Tables[0];
                if (purdata.Tables.CanRemove(tb))
                {
                    purdata.Tables.Remove(tb);
                }
            }
        }
        protected void btncomplete_Click(object sender, EventArgs e)
        {
            try
            {
                cleardataset();
                //dtclstr
                insertinfo();
                //bool retval = validate();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        public void insertinfo()
        {
            if (validate())
            {
                temprefno = string.Empty;
                //  SqlTransaction trx = ClsConnection.SqlCon.BeginTransaction();
                try
                {
                    List<string> lstQuery = new List<string>();
                    for (int d = 0; d < dtclstr.Rows.Count; d++)
                    {
                        string strFarmerCode, strLoanAmount;
                        string strQuery;
                        string strCrop, strVariety;


                        strCrop = dtclstr.Rows[d]["CROP"].ToString();
                        strVariety = dtclstr.Rows[d]["VARIETY"].ToString();

                        strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                        strLoanAmount = dtclstr.Rows[d]["LOAN_AMOUNT"].ToString();

                        string lvarStrTotalLoan, lvarStrTotalBalance;

                        DataTable dt = new DataTable();
                        dt = tpLoader.GetFarmerLoanAmount(strFarmerCode, strCrop, strVariety);

                        if (dt.Rows.Count > 0)
                        {
                            lvarStrTotalLoan = dt.Rows[0]["LOAN_AMOUNT"].ToString();// strrs.GetDouble(1).ToString();
                            lvarStrTotalBalance = dt.Rows[0]["BALANCE_AMOUNT"].ToString();
                        }
                        else
                        {
                            lvarStrTotalLoan = "0";
                            lvarStrTotalBalance = "0";
                        }
                        string lvarStrFinalLoan, lvarStrFinalBalance;

                        lvarStrFinalLoan = Convert.ToString(Convert.ToDouble(lvarStrTotalLoan) + Convert.ToDouble(strLoanAmount));
                        lvarStrFinalBalance = Convert.ToString(Convert.ToDouble(lvarStrTotalBalance) + Convert.ToDouble(strLoanAmount));


                        string lvarLoanTransactionQuery = "INSERT INTO [GPIL_FARMER_LOAN_TRANSACTIONS] ([TRAN_ID],[CROP],[VARIETY],[FARM_CODE],[CURR_LOAN_AMOUNT],[CREDIT_AMOUNT],[DEBIT_AMOUNT],[FINAL_LOAN_AMOUNT],[REMARKS],[STATUS],[CREATED_BY],[CREATED_DATE]) VALUES ('" + DateTime.Now.ToString("yyyyMMddHHmmss") + d.ToString().PadLeft(4, '0') + "','" + strCrop + "','" + strVariety + "','" + strFarmerCode + "','" + lvarStrTotalBalance + "','" + strLoanAmount + "','0','" + lvarStrFinalBalance + "','CREDIT AMOUNT','Y','" + Session["UserId"].ToString() + "',GETDATE())";
                        string lvarFarmerMasterUpdateQuery = "UPDATE GPIL_FARMER_MASTER SET ATTRIBUTE4='" + lvarStrFinalLoan + "',LOAN_AMOUNT='" + lvarStrFinalBalance + "',LAST_UPDATED_BY='" + Session["UserId"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "'";
                        string lvarFarmerCropHisUpdateQuery = "UPDATE GPIL_FARMER_CROP_HISTORY SET LOAN_AMOUNT='" + lvarStrFinalLoan + "',BALANCE_AMOUNT='" + lvarStrFinalBalance + "',LAST_UPDATED_BY='" + Session["UserId"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "' AND CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";

                        lstQuery.Add(lvarLoanTransactionQuery);
                        lstQuery.Add(lvarFarmerMasterUpdateQuery);
                        lstQuery.Add(lvarFarmerCropHisUpdateQuery);

                    }

                    bool b = tpLoader.InsertFarmerLoan(lstQuery);
                    lblMessage.Text = "Error in loading";
                    StringBuilder err = new StringBuilder();
                    err.Append(" UpandDown : " + "GradeMappingUpload Inserrt method");
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                    btndwnerr.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error in loading";
                    StringBuilder err = new StringBuilder();
                    err.Append(" err_msg : " + ex.Message);
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);

                   
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                 
                }
                finally
                {
                    cleardataset();

                }
            }
            else
            {
                cleardataset();
                lblMessage.Text = "Error In Data Which Have Provided Please verify red color rows";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error In Data Which Have Provoded Please verify red color rows');", true);
                btndwnerr.Visible = true;
            }
        }



        public bool validate()
        {
            int i = 0;
            thresisserr = "Error :";
            try
            {
                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {
                    string strCrop = dtclstr.Rows[d]["CROP"].ToString();
                    string strVariety = dtclstr.Rows[d]["VARIETY"].ToString();


                    string strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                    string strLoanAmount = dtclstr.Rows[d]["LOAN_AMOUNT"].ToString();

                    string strQuery = "SELECT F.FARM_CODE FROM GPIL_FARMER_MASTER(NOLOCK) F,GPIL_FARMER_CROP_HISTORY(NOLOCK) FC,GPIL_VARIETY_SEASON_MASTER(NOLOCK) VS WHERE F.FARM_CODE=FC.FARM_CODE AND VS.VARIETY=FC.VARIETY AND VS.CROP=FC.CROP AND FC.CROP='" + strCrop + "' AND FC.VARIETY='" + strVariety + "' AND FC.FARM_CODE='" + strFarmerCode + "'";
                    if (tpLoader.FormerExistsorNot(strCrop,strVariety,strFarmerCode) == false)
                    {
                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        thresisserr = thresisserr + Environment.NewLine + "Farmer Code (" + strFarmerCode + ") may doesn't exist/Invalid Crop & Variety/Crop not match with current Crop as against Variety";
                    }
                    else
                    {
                        try
                        {
                            float f;
                            if (float.TryParse(strLoanAmount, out f) == false)
                            {
                                update(strCrop, strVariety, strFarmerCode, "N");
                                i = i + 1;
                                thresisserr = thresisserr + Environment.NewLine + "Invalid Loan Amount as against Farmer Code (" + strFarmerCode + ")";
                            }
                            else
                            {
                                update(strCrop, strVariety, strFarmerCode, "Y");
                            }
                        }
                        catch (Exception ex)
                        {
                            update(strCrop, strVariety, strFarmerCode, "N");
                            i = i + 1;
                            thresisserr = "Invalid Loan Amount as against Farmer Code (" + strFarmerCode + ")";
                        }
                    }
                }

                if (i == 0)
                {
                    return true;
                }
                else
                {
                    lblMessage.Text = "Error in loading";
                    StringBuilder err = new StringBuilder();
                    err.Append(" Message : " + "FarmerLoan_Error _Validate");
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in loading";
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + "FarmerLoan_Error _Validate");
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                return false;
            }
            finally
            {

                // purdata.Clear();

            }
        }


        protected void GridViewSample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label flg = (Label)e.Row.FindControl("lblupdatests");
                    if (flg == null)
                    { }
                    else
                    {
                        if (flg.Text == "N")                       
                            e.Row.BackColor = Color.Red;                        
                        else if (flg.Text == "Y")                       
                            e.Row.BackColor = Color.Green;                        
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in loading";
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + "FarmerLoan_Error _Validate");
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }


    


        public void update(string inParamStrCrop, string inParamStrVariety, string inParamStrFarmerCode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("FARMER_CODE='" + inParamStrFarmerCode + "' AND CROP='" + inParamStrCrop + "' AND VARIETY='" + inParamStrVariety + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["INS_STS"] = flg;
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                GridViewSample.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        public void download(string path, string filename)
        {
            try
            {
                FileStream fs = null;
                fs = File.Open(path + filename, FileMode.Open);
                byte[] btfile = new byte[fs.Length];
                fs.Read(btfile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(path + filename);
                Response.End();
                // HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btndwnerr_Click(object sender, EventArgs e)
        {
            download(Server.MapPath("LOGFILES\\"), errfile);
        }
    }
}