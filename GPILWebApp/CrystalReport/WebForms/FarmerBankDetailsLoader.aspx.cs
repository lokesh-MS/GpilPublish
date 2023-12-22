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
    public partial class FarmerBankDetailsLoader : System.Web.UI.Page
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
        public static string varStrStaticValidationLogs = string.Empty;
        //  testDataContext test;
        public static string errfile;
        string strsql;
        TPLoader tpLoader = new TPLoader();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                string query = "select CROP,VARIETY,FARMER_CODE,FARMER_NAME,FARMER_FATHER_NAME,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,'V' AS INS_STS from [Sheet1$]";
                data = new OleDbDataAdapter(query, oconn);
                dtclstr.Clear();
                data.Fill(dtclstr);
                GridViewSample.DataSource = dtclstr;
                GridViewSample.DataBind();
                GridViewSample.EditIndex = -1;
                data.Dispose();
                //string query1 = "select distinct CROP,VARIETY,FARMER_CODE,FARMER_NAME,FARMER_FATHER_NAME,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE from [Sheet1$]";
                string query1 = "select CROP,VARIETY,FARMER_CODE,FARMER_NAME,FARMER_FATHER_NAME,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE from [Sheet1$] GROUP BY CROP,VARIETY,FARMER_CODE,FARMER_NAME,FARMER_FATHER_NAME,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE";
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
                Label lblFarmerName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblFarmerName");
                Label lblFarmerFatherName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblFarmerFatherName");
                Label lblMobileNumber = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblMobileNumber");
                Label lblEmailID = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblEmailID");
                Label lblBankAccountNumber = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblBankAccountNumber");
                Label lblBankName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblBankName");
                Label lblBranchName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblBranchName");
                Label lblIFSCCode = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblIFSCCode");


                TextBox flg = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtupdatests");

                DataRow[] rows = dtclstr.Select("FARMER_CODE ='" + lblFarmerCode.Text + "' AND CROP='" + lblCrop.Text + "' AND VARIETY='" + lblVariety.Text + "'");

                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["CROP"] = lblCrop.Text.Trim().ToUpper();
                        row["VARIETY"] = lblVariety.Text.Trim().ToUpper();

                        row["FARMER_CODE"] = lblFarmerCode.Text.Trim().ToUpper();
                        row["FARMER_NAME"] = lblFarmerName.Text;
                        row["FARMER_FATHER_NAME"] = lblFarmerFatherName.Text;
                        row["MOBILE_NO"] = lblMobileNumber.Text;
                        row["EMAIL_ID"] = lblEmailID.Text;
                        row["BANK_ACCOUNT_NO"] = lblBankAccountNumber.Text.ToUpper();
                        row["BANK_NAME"] = lblBankName.Text;
                        row["BRANCH_NAME"] = lblBranchName.Text;
                        row["IFSC_CODE"] = lblIFSCCode.Text.ToUpper();

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
                Label lblFarmerName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblFarmerName");
                Label lblFarmerFatherName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblFarmerFatherName");
                Label lblMobileNumber = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblMobileNumber");
                Label lblEmailID = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblEmailID");
                Label lblBankAccountNumber = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblBankAccountNumber");
                Label lblBankName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblBankName");
                Label lblBranchName = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblBranchName");
                Label lblIFSCCode = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblIFSCCode");


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
            clrds.Tables[0].Columns.Add("FARMER_NAME");
            clrds.Tables[0].Columns.Add("FARMER_FATHER_NAME");
            clrds.Tables[0].Columns.Add("MOBILE_NO");
            clrds.Tables[0].Columns.Add("EMAIL_ID");
            clrds.Tables[0].Columns.Add("BANK_ACCOUNT_NO");
            clrds.Tables[0].Columns.Add("BANK_NAME");
            clrds.Tables[0].Columns.Add("BRANCH_NAME");
            clrds.Tables[0].Columns.Add("IFSC_CODE");
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
            cleardataset();
            //dtclstr
            insertinfo();
            //bool retval = validate();
        }


        public void insertinfo()
        {
            if (validate())
            {
                temprefno = string.Empty;
               // SqlTransaction trx = ClsConnection.SqlCon.BeginTransaction();
                try
                {
                    List<string> lstQuery = new List<string>();
                    for (int d = 0; d < dtclstr.Rows.Count; d++)
                    {
                        string strFarmerCode, lvarStrFarmerName, lvarStrFarmerFatherName, lvarStrMobileNo, lvarStrEmailId, lvarStrAccountNo, lvarStrBankName, lvarStrBranchName, lvarStrIFSCCode;
                        string strQuery;
                        string strCrop, strVariety;


                        strCrop = dtclstr.Rows[d]["CROP"].ToString();
                        strVariety = dtclstr.Rows[d]["VARIETY"].ToString();

                        strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                        lvarStrFarmerName = dtclstr.Rows[d]["FARMER_NAME"].ToString();
                        lvarStrFarmerFatherName = dtclstr.Rows[d]["FARMER_FATHER_NAME"].ToString();
                        lvarStrMobileNo = dtclstr.Rows[d]["MOBILE_NO"].ToString();
                        lvarStrEmailId = dtclstr.Rows[d]["EMAIL_ID"].ToString();
                        lvarStrAccountNo = dtclstr.Rows[d]["BANK_ACCOUNT_NO"].ToString();
                        lvarStrBankName = dtclstr.Rows[d]["BANK_NAME"].ToString();
                        lvarStrBranchName = dtclstr.Rows[d]["BRANCH_NAME"].ToString();
                        lvarStrIFSCCode = dtclstr.Rows[d]["IFSC_CODE"].ToString();
                        string lvarFarmerMasterUpdateQuery = "";
                        string lvarFarmerCropHisUpdateQuery = "";
                        DataTable dt = new DataTable();
                        dt = tpLoader.GetFarmerCodeCropHistory(strFarmerCode, strCrop, strVariety);
                        if (dt.Rows.Count > 0)
                        {
                            lvarFarmerMasterUpdateQuery = "UPDATE GPIL_FARMER_MASTER SET FARM_NAME='" + lvarStrFarmerName + "',FARM_FATHER_NAME='" + lvarStrFarmerFatherName + "',MOBILE_NO='" + lvarStrMobileNo + "',EMAIL_ID='" + lvarStrEmailId + "',BANK_ACCOUNT_NO='" + lvarStrAccountNo + "',BANK_NAME='" + lvarStrBankName + "',BRANCH_NAME='" + lvarStrBranchName + "',IFSC_CODE='" + lvarStrIFSCCode + "',LAST_UPDATED_BY='" + Session["UserID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "'";
                            lvarFarmerCropHisUpdateQuery = "UPDATE GPIL_FARMER_CROP_HISTORY SET MOBILE_NO='" + lvarStrMobileNo + "',EMAIL_ID='" + lvarStrEmailId + "',BANK_ACCOUNT_NO='" + lvarStrAccountNo + "',BANK_NAME='" + lvarStrBankName + "',BRANCH_NAME='" + lvarStrBranchName + "',IFSC_CODE='" + lvarStrIFSCCode + "',LAST_UPDATED_BY='" + Session["UserID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "' AND CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";

                        }
                        else
                        {
                            lvarFarmerMasterUpdateQuery = "UPDATE GPIL_FARMER_MASTER SET FARM_NAME='" + lvarStrFarmerName + "',FARM_FATHER_NAME='" + lvarStrFarmerFatherName + "',MOBILE_NO='" + lvarStrMobileNo + "',EMAIL_ID='" + lvarStrEmailId + "',BANK_ACCOUNT_NO='" + lvarStrAccountNo + "',BANK_NAME='" + lvarStrBankName + "',BRANCH_NAME='" + lvarStrBranchName + "',IFSC_CODE='" + lvarStrIFSCCode + "',LAST_UPDATED_BY='" + Session["UserID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "'";
                            lvarFarmerCropHisUpdateQuery = "INSERT INTO GPIL_FARMER_CROP_HISTORY (HIS_CODE,FARM_CODE,CROP,VARIETY,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,STATUS,LOAN_AMOUNT,BALANCE_AMOUNT) VALUES ('" + strCrop + strVariety + strFarmerCode + "','" + strFarmerCode + "','" + strCrop + "','" + strVariety + "','" + lvarStrMobileNo + "','" + lvarStrEmailId + "','" + lvarStrAccountNo + "','" + lvarStrBankName + "','" + lvarStrBranchName + "','" + lvarStrIFSCCode + "','" + Session["UserID"].ToString() + "',GETDATE(),'Y','0','0') ";

                        }

                        lstQuery.Add(lvarFarmerMasterUpdateQuery);
                        lstQuery.Add(lvarFarmerCropHisUpdateQuery);
                    }
                   

                    clrgridview();
                    dtclstr.Clear();
                   
                    lblMessage.Text = "DONE";
                    btndwnerr.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                    //trx.Rollback();
                    //trx.Dispose();
                }
                finally
                {
                    cleardataset();
                   // trx.Dispose();
                }
            }
            else
            {
                cleardataset();
                lblMessage.Text = "Error In Data Which Have Provoded Please verify red color rows";              
                btndwnerr.Visible = true;
            }
        }



        public bool validate()
        {
            int i = 0;
            varStrStaticValidationLogs = "Error :";
            try
            {
                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {
                    string strCrop = dtclstr.Rows[d]["CROP"].ToString();
                    string strVariety = dtclstr.Rows[d]["VARIETY"].ToString();
                    string strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                    string lvarStrFarmerName, lvarStrFarmerFatherName, lvarStrMobileNo, lvarStrEmailId, lvarStrAccountNo, lvarStrBankName, lvarStrBranchName, lvarStrIFSCCode;
                    lvarStrFarmerName = dtclstr.Rows[d]["FARMER_NAME"].ToString();
                    lvarStrFarmerFatherName = dtclstr.Rows[d]["FARMER_FATHER_NAME"].ToString();
                    lvarStrMobileNo = dtclstr.Rows[d]["MOBILE_NO"].ToString();
                    lvarStrEmailId = dtclstr.Rows[d]["EMAIL_ID"].ToString();
                    lvarStrAccountNo = dtclstr.Rows[d]["BANK_ACCOUNT_NO"].ToString();
                    lvarStrBankName = dtclstr.Rows[d]["BANK_NAME"].ToString();
                    lvarStrBranchName = dtclstr.Rows[d]["BRANCH_NAME"].ToString();
                    lvarStrIFSCCode = dtclstr.Rows[d]["IFSC_CODE"].ToString();

                    if (tpLoader.CheckFarmer(strFarmerCode) == false)
                    {
                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Farmer Code (" + strFarmerCode + ") doesn't exist in master";
                    }
                    else if (tpLoader.CheckVariety(strVariety) == false)
                    {
                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Variety (" + strVariety + ")  doesn't exist in master; Farmer Code (" + strFarmerCode + ")";
                    }
                    else if (tpLoader.CheckCropVariety(strVariety, strCrop) == false)
                    {
                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ")  is not match with current crop of given variety (" + strVariety + ") ; Farmer Code (" + strFarmerCode + ")";
                    }
                    else if (lvarStrFarmerName.Trim().Length == 0 || lvarStrFarmerName.Length > 50)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Farmer Name (" + lvarStrFarmerName + ") must required with maximum length of 50 characters";
                    }
                    else if (lvarStrFarmerFatherName.Trim().Length == 0 || lvarStrFarmerFatherName.Length > 50)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Farmer Father Name (" + lvarStrFarmerFatherName + ") must required with maximum length of 50 characters";
                    }
                    else if (lvarStrMobileNo.Length > 11)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Mobile No (" + lvarStrMobileNo + ") length should not exist 11 characters";
                    }
                    else if (lvarStrEmailId.Length > 50)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Email Id (" + lvarStrEmailId + ") length should not exist 50 characters";
                    }
                    else if (lvarStrAccountNo.Trim().Length == 0 || lvarStrAccountNo.Length > 25)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Account No (" + lvarStrAccountNo + ") must required with maximum length of 25 characters";
                    }
                    else if (lvarStrBankName.Trim().Length == 0 || lvarStrBankName.Length > 50)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Bank Name (" + lvarStrBankName + ") must required with maximum length of 50 characters";
                    }
                    else if (lvarStrBranchName.Trim().Length == 0 || lvarStrBranchName.Length > 50)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Branch Name (" + lvarStrBranchName + ") must required with maximum length of 50 characters";
                    }
                    else if (lvarStrIFSCCode.Trim().Length == 0 || lvarStrIFSCCode.Length > 11)
                    {

                        update(strCrop, strVariety, strFarmerCode, "N");
                        i = i + 1;
                        varStrStaticValidationLogs = varStrStaticValidationLogs + Environment.NewLine + "Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : IFSC Code (" + lvarStrIFSCCode + ") must required with maximum length of 11 characters";
                    }
                    else
                    {
                        update(strCrop, strVariety, strFarmerCode, "Y");

                    }


                }

                if (i == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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
                        {
                            e.Row.BackColor = Color.Red;
                        }
                        else if (flg.Text == "Y")
                        {
                            e.Row.BackColor = Color.Green;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        //private bool FuncBoolIsExist(string inParamStrQuery)
        //{
        //    try
        //    {
        //        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(inParamStrQuery, ClsConnection.SqlCon);
        //        objSqlDataAdapter.SelectCommand.CommandTimeout = 0;
        //        DataTable objDataTable = new DataTable();
        //        objSqlDataAdapter.Fill(objDataTable);
        //        if (objDataTable.Rows.Count > 0)   
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


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