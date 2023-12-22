using GPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPILWebApp.ViewModel;

namespace GPILWebApp
{
    public partial class FarmerMaster : System.Web.UI.Page
    {
        public static DataTable dtclstr = new DataTable();
        public static string status = "";
        FarmerManagement fMGT = new FarmerManagement();
        private object txtFarmerFatherName;
        private object txtCrop;
        private object txtSoilType;
        private object txtupdatests;
        private object txtVarietyCode;
        private object txtVariety;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rdbimport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbimport.Checked == true)
            {
                idUpload.Visible = true;
                idManual.Visible = false;
                idManualButton.Visible = false;
                divGrid.Visible = false;
                divImportSave.Visible = true;
                lblMessage.Text = "";

            }
        }

        protected void rdbmanual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbmanual.Checked == true)
            {
                idManual.Visible = true;
                idManualButton.Visible = true;
                idUpload.Visible = false;
                divGrid.Visible = false;
                divImportSave.Visible = false;
                lblMessage.Text = "";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Concat(Server.MapPath("~/UploadFiles/"), excelUpload.FileName);
                OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
                //Save File as Temp then you can delete it if you want
                excelUpload.SaveAs(path);
                string query = "select FARM_CODE , CROP,VARIETY ,FARMER_CATEGORY, FARMER_NAME, FARMER_FATHER_NAME, VILLAGE_CODE, SOIL_TYPE, FARMER_ADDRESS1, FARMER_ADDRESS2, COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO, EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,  'V' as INS_STS  from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvFarmer.DataSource = dtclstr;
                    gvFarmer.DataBind();
                    //idUpload.Visible = false;
                    divGrid.Visible = true;
                    divImportSave.Visible = true;
                    status = "Excel";
                }



            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
              //  Utils.LogError(err.ToString(), Request);
            }
        }

        protected void gvFarmer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFarmer.PageIndex = e.NewPageIndex;
            LoadData();
        }
        private void LoadData()
        {
            gvFarmer.DataSource = dtclstr;
            gvFarmer.DataBind();
        }
        protected void gvFarmer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFarmer.EditIndex = -1;
            LoadData();
        }

        protected void gvFarmer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox crop = (TextBox)gvFarmer.FooterRow.FindControl("txtAddCrop");
                    TextBox variety = (TextBox)gvFarmer.FooterRow.FindControl("txtAddVariety");
                    TextBox fmrCode = (TextBox)gvFarmer.FooterRow.FindControl("txtAddFarmerCode");
                    TextBox fmrCategory = (TextBox)gvFarmer.FooterRow.FindControl("txtAddFarmerCategory");

                    TextBox fmrName = (TextBox)gvFarmer.FooterRow.FindControl("txtAddFarmerName");
                    TextBox fmrFatherName = (TextBox)gvFarmer.FooterRow.FindControl("txtAddFarmerFatherName");
                    TextBox vgeCode = (TextBox)gvFarmer.FooterRow.FindControl("txtAddVillageCode");
                    TextBox soilType = (TextBox)gvFarmer.FooterRow.FindControl("txtAddSoilType");

                    TextBox fmrAdd1 = (TextBox)gvFarmer.FooterRow.FindControl("txtAddFarmerAddress1");
                    TextBox fmrAdd2 = (TextBox)gvFarmer.FooterRow.FindControl("txtAddFarmerAddress2");
                    TextBox cntry = (TextBox)gvFarmer.FooterRow.FindControl("txtAddCountry");
                    TextBox pinCode = (TextBox)gvFarmer.FooterRow.FindControl("txtAddPinCode");

                    TextBox tlpNo = (TextBox)gvFarmer.FooterRow.FindControl("txtAddTelePhoneNo");
                    TextBox mblNo = (TextBox)gvFarmer.FooterRow.FindControl("txtAddMobileNo");
                    TextBox emailID = (TextBox)gvFarmer.FooterRow.FindControl("txtAddEmailID");

                    TextBox bankAccNo = (TextBox)gvFarmer.FooterRow.FindControl("txtAddBankAccNo");
                    TextBox bankName = (TextBox)gvFarmer.FooterRow.FindControl("txtAddBankName");
                    TextBox branchName = (TextBox)gvFarmer.FooterRow.FindControl("txtAddBranchName");
                    TextBox ifscCode = (TextBox)gvFarmer.FooterRow.FindControl("txtAddIFSCCode");

                    TextBox flg = (TextBox)gvFarmer.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();


                    row["FARMER_CODE"] = fmrCode.Text;
                    row["VARIETY"] = variety.Text;
                    row["CROP"] = crop.Text;
                    row["FARMER_CATEGORY"] = fmrCategory.Text;

                    row["FARMER_NAME"] = fmrName.Text;
                    row["FARMER_FATHER_NAME"] = fmrFatherName.Text;
                    row["VILLAGE_CODE"] = vgeCode.Text;
                    row["SOIL_TYPE"] = soilType.Text;

                    row["FARMER_ADDRESS1"] = fmrAdd1.Text;
                    row["FARMER_ADDRESS2"] = fmrAdd2.Text;
                    row["COUNTRY"] = cntry.Text;
                    row["PIN_CODE"] = pinCode.Text;

                    row["TEL_NO"] = tlpNo.Text;
                    row["MOBILE_NO"] = mblNo.Text;
                    row["EMAIL_ID"] = emailID.Text;
                    row["BANK_ACCOUNT_NO"] = bankAccNo.Text;

                    row["BANK_NAME"] = bankName.Text;
                    row["BRANCH_NAME"] = branchName.Text;
                    row["IFSC_CODE"] = ifscCode.Text;

                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvFarmer.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvFarmer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label frmCode = (Label)gvFarmer.Rows[e.RowIndex].FindControl("lFarmerCode");
                TextBox variety = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtVariety");
                TextBox crop = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtCrop");
                TextBox fmrCategory = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtFarmerCategory");

                TextBox fmrName = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtFarmerName");
                TextBox fmrFatherName = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtFarmerFatherName");
                TextBox vgeCode = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtVillageCode");
                TextBox soilType = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtSoilType");


                TextBox fmrAdd1 = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtFarmerAddress1");
                TextBox fmrAdd2 = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtFarmerAddress2");
                TextBox cntry = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtCountry");
                TextBox pinCode = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtPinCode");

                TextBox tlpNo = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtTelePhoneNo");
                TextBox mblNo = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtMobileNo");
                TextBox emailID = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtEmailID");
                TextBox bankAccNo = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtBankAccNo");

                TextBox bankName = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtBankName");
                TextBox branchName = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtBranchName");
                TextBox ifscCode = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtIFSCCode");
                TextBox flg = (TextBox)gvFarmer.Rows[e.RowIndex].FindControl("txtupdatests");


                DataRow[] rows = dtclstr.Select("FARMER_CODE   ='" + frmCode.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["VARIETY"] = variety.Text;
                        row["CROP"] = crop.Text;
                        row["FARMER_CATEGORY"] = fmrCategory.Text;
                        row["FARMER_NAME"] = fmrName.Text;
                        row["FARMER_FATHER_NAME"] = fmrFatherName.Text;
                        row["VILLAGE_CODE"] = vgeCode.Text;
                        row["SOIL_TYPE"] = soilType.Text;
                        row["FARMER_ADDRESS1"] = fmrAdd1.Text;
                        row["FARMER_ADDRESS2"] = fmrAdd2.Text;
                        row["COUNTRY"] = cntry.Text;
                        row["PIN_CODE"] = pinCode.Text;
                        row["TEL_NO"] = tlpNo.Text;
                        row["MOBILE_NO"] = mblNo.Text;
                        row["EMAIL_ID"] = emailID.Text;
                        row["BANK_ACCOUNT_NO"] = bankAccNo.Text;
                        row["BANK_NAME"] = bankName.Text;
                        row["BRANCH_NAME"] = branchName.Text;
                        row["IFSC_CODE"] = ifscCode.Text;
                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvFarmer.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvFarmer_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvFarmer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label frmCode = (Label)gvFarmer.Rows[e.RowIndex].FindControl("lFarmerCode");
                DataRow[] rowsdel = dtclstr.Select("FARMER_CODE ='" + frmCode.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvFarmer.EditIndex = -1;
                dtclstr.AcceptChanges();
                LoadData();
                lblMessage.Text = "Record Deleted Successfully!";


            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
               // Utils.LogError(err.ToString(), Request);
            }
        }

        protected void gvFarmer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvFarmer.EditIndex = e.NewEditIndex;
                LoadData();
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
               // Utils.LogError(err.ToString(), Request);
            }
        }

        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                inserting();
                clrgridview();
                lblMessage.Text = "Data Saved Successfully";
                dtclstr.Clear();
            }
            else
            {
                lblMessage.Text = "Error In Data Which You have provided";
            }
        }
        public bool validate()
        {
            int i = 0;
            try
            {

                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {

                    string fcode = dtclstr.Rows[d]["FARM_CODE"].ToString();
                    string crop = dtclstr.Rows[d]["CROP"].ToString();
                    string vrty = dtclstr.Rows[d]["VARIETY"].ToString();

                    string fctcry = dtclstr.Rows[d]["FARMER_CATEGORY"].ToString();
                    string fmrname = dtclstr.Rows[d]["FARMER_NAME"].ToString();
                    string fmrfathername = dtclstr.Rows[d]["FARMER_FATHER_NAME"].ToString();
                    string vcode = dtclstr.Rows[d]["VILLAGE_CODE"].ToString();

                    string solitype = dtclstr.Rows[d]["SOIL_TYPE"].ToString();
                    string fadd1 = dtclstr.Rows[d]["FARMER_ADDRESS1"].ToString();

                    string fadd2 = dtclstr.Rows[d]["FARMER_ADDRESS2"].ToString();
                    string cntry = dtclstr.Rows[d]["COUNTRY"].ToString();
                    string pincode = dtclstr.Rows[d]["PIN_CODE"].ToString();
                    string telno = dtclstr.Rows[d]["TEL_NO"].ToString();

                    string mblno = dtclstr.Rows[d]["MOBILE_NO"].ToString();
                    string mailid = dtclstr.Rows[d]["EMAIL_ID"].ToString();
                    string bankaccno = dtclstr.Rows[d]["BANK_ACCOUNT_NO"].ToString();
                    string bankname = dtclstr.Rows[d]["BANK_NAME"].ToString();

                    string brnchname = dtclstr.Rows[d]["BRANCH_NAME"].ToString();
                    string ifsccode = dtclstr.Rows[d]["IFSC_CODE"].ToString();
                    string sts = dtclstr.Rows[d]["INS_STS"].ToString();



                    if (fmrname.Trim() == string.Empty)
                    {
                        update(fcode, "N");
                        i = i + 1;
                    }
                    else if (vcode.Trim() == string.Empty)
                    {
                        update(fcode, "N");
                        i = i + 1;
                    }
                    else
                    {

                        string s = "";
                        s = fMGT.GetFarmerDetails(fcode);
                        if (s == "Y")
                        {
                            update(fcode, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(fcode, "Y");
                        }
                    }
                }

                if (i == 0) return true;
                else return false;

            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
               // Utils.LogError(err.ToString(), Request);
                return false;
            }
            finally
            {
            }
        }

        public void update(string fcode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("FARM_CODE ='" + fcode + "'");

                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["INS_STS"] = flg;
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }


                gvFarmer.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
              //  Utils.LogError(err.ToString(), Request);
            }
        }



        public void inserting()
        {
            FarmerDetails f = new FarmerDetails();


            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                f.FARMCODE = dtclstr.Rows[s][0].ToString().ToUpper();
                f.FARMNAME = dtclstr.Rows[s][1].ToString().ToUpper();
                f.FARMCATEGORY = dtclstr.Rows[s][2].ToString().ToUpper();
                f.VILLAGECODE = dtclstr.Rows[s][3].ToString().ToUpper();

                f.FARMFATHERNAME = dtclstr.Rows[s][4].ToString().ToUpper();
                f.REDMARK = dtclstr.Rows[s][5].ToString().ToUpper();
                f.SOILTYPE = dtclstr.Rows[s][6].ToString().ToUpper();
                f.FARMADDRESS1 = dtclstr.Rows[s][7].ToString().ToUpper();

                f.FARMADDRESS2 = dtclstr.Rows[s][8].ToString().ToUpper();
                f.FARMADDRESS3 = dtclstr.Rows[s][9].ToString().ToUpper();
                f.FARMADDRESS4 = dtclstr.Rows[s][10].ToString().ToUpper();
                f.FARMADDRESS5 = dtclstr.Rows[s][11].ToString().ToUpper();
                f.FARMADDRESS6 = dtclstr.Rows[s][12].ToString().ToUpper();
                f.COUNTRY = dtclstr.Rows[s][13].ToString().ToUpper();

                f.PINCODE = dtclstr.Rows[s][14].ToString().ToUpper();
                f.TELNO = dtclstr.Rows[s][15].ToString().ToUpper();
                f.MOBILENO = dtclstr.Rows[s][16].ToString().ToUpper();
                f.EMAILID = dtclstr.Rows[s][17].ToString().ToUpper();

                f.BANKACCOUNTNO = dtclstr.Rows[s][18].ToString().ToUpper();
                f.BANKNAME = dtclstr.Rows[s][19].ToString().ToUpper();
                f.BRANCHNAME = dtclstr.Rows[s][20].ToString().ToUpper();
                f.IFSCCODE = dtclstr.Rows[s][21].ToString().ToUpper();
                f.VARIETY = dtclstr.Rows[s][22].ToString().ToUpper();
                f.ADHAARNO = dtclstr.Rows[s][23].ToString().ToUpper();

                f.CREATEDBY = Session["UserName"].ToString();
                f.STATUS = "Y";


                try
                {
                    fMGT.UserInsertFarmerDetails(f, "INSERT");
                    lblMessage.Text = "Variety inserted sucessfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                    divImportSave.Visible = false;
                    idUpload.Visible = false;
                }
                catch (Exception ex)
                {
                    //insertdataintosql1(Variety, vtype, vname, vdesc, "Y");
                }
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Village Master Details Imported');", true);
            }
        }

        public void clrgridview()
        {
            DataSet clrds = new DataSet();
            clrds.Tables.Add("TEMP");
            clrds.Tables[0].Columns.Add("FARM_CODE");
            clrds.Tables[0].Columns.Add("CROP");
            clrds.Tables[0].Columns.Add("VARIETY");
            clrds.Tables[0].Columns.Add("FARMER_CATEGORY");

            clrds.Tables[0].Columns.Add("FARMER_NAME");
            clrds.Tables[0].Columns.Add("FARMER_FATHER_NAME");
            clrds.Tables[0].Columns.Add("VILLAGE_CODE");
            clrds.Tables[0].Columns.Add("SOIL_TYPE");

            clrds.Tables[0].Columns.Add("FARMER_ADDRESS1");
            clrds.Tables[0].Columns.Add("FARMER_ADDRESS2");

            clrds.Tables[0].Columns.Add("COUNTRY");
            clrds.Tables[0].Columns.Add("PIN_CODE");
            clrds.Tables[0].Columns.Add("TEL_NO");
            clrds.Tables[0].Columns.Add("MOBILE_NO");

            clrds.Tables[0].Columns.Add("EMAIL_ID");
            clrds.Tables[0].Columns.Add("BANK_ACCOUNT_NO");
            clrds.Tables[0].Columns.Add("BANK_NAME");
            clrds.Tables[0].Columns.Add("BRANCH_NAME");
            clrds.Tables[0].Columns.Add("IFSC_CODE");

            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvFarmer.DataSource = clrds;
            gvFarmer.DataBind();
            int columncount = gvFarmer.Rows[0].Cells.Count;
            gvFarmer.Rows[0].Cells.Clear();
            gvFarmer.Rows[0].Cells.Add(new TableCell());
            gvFarmer.Rows[0].Cells[0].ColumnSpan = columncount;
            gvFarmer.Rows[0].Cells[0].Text = "No Records Found";
        }

        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            insertdataintosql1(txtFarmerCode.Text, txtFarmerName.Text, txtFarmerCategory.Text, txtVillageCode.Text, txtFatherName.Text, txtRedMark.Text, ddlSoilType.Text, txtFarmerAddress1.Text, txtFarmerAddress2.Text, txtFarmerAddress3.Text, txtFarmerAddress4.Text, txtFarmerAddress5.Text, txtFarmerAddress6.Text, txtCountry.Text, txtPinCode.Text, txtTelePhoneNo.Text, txtMobileNo.Text, txtEmailID.Text, txtBankAccNo.Text, txtBankName.Text, txtBranchName.Text, txtIFSCCode.Text, ddlVariety.Text, txtAdhaarNo.Text, ddlCrop.Text, "Y");
            ClearControls();
        }

        private void insertdataintosql1(string fcode, string fname, string fctcry, string vcode, string fathername, string rdmark, string sltype, string fadd1, string fadd2, string fadd3, string fadd4, string fadd5, string fadd6, string ctry, string pcode, string telno, string mobno, string mailid, string bankaccno, string bankname, string branchname, string ifsccode, string vrty, string adhrno, string crop, string sts)
        {
            bool b = false;
            try
            {

                FarmerDetails f = new FarmerDetails();
                f.FARMCODE = txtFarmerCode.Text.Trim();
                f.FARMNAME = txtFarmerName.Text.Trim();
                f.FARMCATEGORY = txtFarmerCategory.Text.Trim();
                f.VILLAGECODE = txtVillageCode.Text.Trim();

                f.FARMFATHERNAME = txtFatherName.Text.Trim();
                f.REDMARK = txtRedMark.Text.Trim();
                f.SOILTYPE = ddlSoilType.Text.Trim();
                f.FARMADDRESS1 = txtFarmerAddress1.Text.Trim();
                f.FARMADDRESS2 = txtFarmerAddress2.Text.Trim();
                f.FARMADDRESS3 = txtFarmerAddress3.Text.Trim();
                f.FARMADDRESS4 = txtFarmerAddress4.Text.Trim();
                f.FARMADDRESS5 = txtFarmerAddress5.Text.Trim();
                f.FARMADDRESS6 = txtFarmerAddress6.Text.Trim();
                f.COUNTRY = txtCountry.Text.Trim();
                f.PINCODE = txtPinCode.Text.Trim();
                f.TELNO = txtTelePhoneNo.Text.Trim();
                f.MOBILENO = txtMobileNo.Text.Trim();

                f.EMAILID = txtEmailID.Text.Trim();
                f.BANKACCOUNTNO = txtBankAccNo.Text.Trim();
                f.BANKNAME = txtBankName.Text.Trim();
                f.BRANCHNAME = txtBranchName.Text.Trim();

                f.IFSCCODE = txtIFSCCode.Text.Trim();
                f.VARIETY = ddlVariety.Text.Trim();
                f.ADHAARNO = txtAdhaarNo.Text.Trim();
                f.CROP = ddlCrop.Text.Trim();
                f.CREATEDBY = Session["UserName"].ToString();
                f.STATUS = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = fMGT.GetFarmerDetails(f.FARMCODE);
                    if (res == "N")
                    {
                        fMGT.UserInsertFarmerDetails(f, "INSERT");
                        lblMessage.Text = "Farmer inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "Farmer already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    fMGT.UserInsertFarmerDetails(f, "UPDATE");
                    lblMessage.Text = "Farmer updated sucessfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
              //  Utils.LogError(err.ToString(), Request);
            }
        }

        private void ClearControls()
        {
            txtFarmerCode.Text = "";
            txtFarmerName.Text = "";
            txtFarmerCategory.Text = "";
            txtVillageCode.Text = "";
            txtFatherName.Text = "";

            txtRedMark.Text = "";
            ddlSoilType.Text = "";
            txtFarmerAddress1.Text = "";
            txtFarmerAddress2.Text = "";
            txtFarmerAddress3.Text = "";
            txtFarmerAddress4.Text = "";
            txtFarmerAddress5.Text = "";
            txtFarmerAddress6.Text = "";
            txtCountry.Text = "";
            txtPinCode.Text = "";
            txtTelePhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailID.Text = "";
            txtBankAccNo.Text = "";
            txtBankName.Text = "";
            txtBranchName.Text = "";
            txtIFSCCode.Text = "";
            ddlVariety.Text = "";
            txtAdhaarNo.Text = "";
            ddlCrop.Text = "";

            txtFarmerCode.Focus();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void bindGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = fMGT.GetFarmer("0");
                gvFarmer.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvFarmer.DataSource = dt;
                    gvFarmer.DataBind();
                }
                else
                {
                    gvFarmer.DataSource = null;
                    gvFarmer.DataBind();

                }
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
             //   Utils.LogError(err.ToString(), Request);
            }
        }
    }
}