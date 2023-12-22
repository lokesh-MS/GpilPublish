using GPI;
using GPILWebApp.ViewModel;
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

namespace GPILWebApp
{
    public partial class UserMaster1 : System.Web.UI.Page
    {
        public static DataTable dtclstr = new DataTable();
        public static string status = "";
        UserManagement uMGT = new UserManagement();
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

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            LoadData();
        }

        private void LoadData()
        {
            gvUser.DataSource = dtclstr;
            gvUser.DataBind();

        }
        protected void gvUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUser.EditIndex = -1;
            LoadData();
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox userID = (TextBox)gvUser.FooterRow.FindControl("txtAddUserID");
                    TextBox userName = (TextBox)gvUser.FooterRow.FindControl("txtAddUserName");
                    TextBox password = (TextBox)gvUser.FooterRow.FindControl("txtAddPassword");
                    TextBox userEPRname = (TextBox)gvUser.FooterRow.FindControl("txtAddUserERPName");

                    TextBox empCode = (TextBox)gvUser.FooterRow.FindControl("txtAddEmpCode");
                    TextBox desig = (TextBox)gvUser.FooterRow.FindControl("txtAddDesignation");
                    TextBox depart = (TextBox)gvUser.FooterRow.FindControl("txtAddDepartment");
                    TextBox usrRights = (TextBox)gvUser.FooterRow.FindControl("txtAddUserRights");

                    TextBox mblNO = (TextBox)gvUser.FooterRow.FindControl("txtAddMobileNumber");
                    TextBox mailID = (TextBox)gvUser.FooterRow.FindControl("txtAddMailID");


                    TextBox flg = (TextBox)gvUser.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["USER_ID"] = userID.Text;
                    row["USER_NAME"] = userName.Text;
                    row["PASSWORD"] = password.Text;
                    row["USER_ERP_NAME"] = userEPRname.Text;

                    row["EMP_CODE"] = empCode.Text;
                    row["DESIGNATION"] = desig.Text;
                    row["DEPARTMENT"] = depart.Text;
                    row["USER_RIGHTS"] = usrRights.Text;

                    row["MOBILE_NO"] = mblNO.Text;
                    row["EMAIL_ID"] = mailID.Text;

                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvUser.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void gvUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label userID = (Label)gvUser.Rows[e.RowIndex].FindControl("lUserID");
                TextBox userName = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtUserName");
                TextBox password = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtPassword");
                TextBox erpName = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtUserERPName");

                TextBox empCode = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtEmpCode");
                TextBox designation = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtDesignation");
                TextBox department = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtDepartment");
                TextBox userRights = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtUserRights");


                TextBox mblNO = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtMobileNumber");
                TextBox mailID = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtMailID");
                TextBox flg = (TextBox)gvUser.Rows[e.RowIndex].FindControl("txtupdatests");


                DataRow[] rows = dtclstr.Select("USER_ID ='" + userID.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["USER_NAME"] = userName.Text;
                        row["PASSWORD"] = password.Text;
                        row["USER_ERP_NAME"] = erpName.Text;
                        row["EMP_CODE"] = empCode.Text;
                        row["DESIGNATION"] = designation.Text;
                        row["DEPARTMENT"] = department.Text;
                        row["USER_RIGHTS"] = userRights.Text;
                        row["MOBILE_NO"] = mblNO.Text;
                        row["EMAIL_ID"] = mailID.Text;

                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvUser.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label userID = (Label)gvUser.Rows[e.RowIndex].FindControl("lUserID");
                DataRow[] rowsdel = dtclstr.Select("USER_ID ='" + userID.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvUser.EditIndex = -1;
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
                Utils.LogError(err.ToString(), Request);
            }
        }

        protected void gvUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvUser.EditIndex = e.NewEditIndex;
                LoadData();
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Request);
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

                    string usrID = dtclstr.Rows[d]["USER_ID"].ToString();
                    string usrName = dtclstr.Rows[d]["USER_NAME"].ToString();
                    string pswd = dtclstr.Rows[d]["PASSWORD"].ToString();

                    string erpname = dtclstr.Rows[d]["USER_ERP_NAME"].ToString();
                    string empCode = dtclstr.Rows[d]["EMP_CODE"].ToString();
                    string desig = dtclstr.Rows[d]["DESIGNATION"].ToString();
                    string dept = dtclstr.Rows[d]["DEPARTMENT"].ToString();

                    string usrRights = dtclstr.Rows[d]["USER_RIGHTS"].ToString();
                    string mblNO = dtclstr.Rows[d]["MOBILE_NO"].ToString();

                    string emailID = dtclstr.Rows[d]["EMAIL_ID"].ToString();


                    if (usrName.Trim() == string.Empty)
                    {
                        update(usrID, "N");
                        i = i + 1;
                    }
                    else if (pswd.Trim() == string.Empty)
                    {
                        update(usrID, "N");
                        i = i + 1;
                    }
                    else
                    {

                        string s = "";
                        s = uMGT.GetUserDetails(usrID);
                        if (s == "Y")
                        {
                            update(usrID, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(usrID, "Y");
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
                Utils.LogError(err.ToString(), Request);
                return false;
            }
            finally
            {
            }
        }


        public void update(string usercode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("USER_ID ='" + usercode + "'");
                // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["INS_STS"] = flg;
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }


                gvUser.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Request);
            }
        }

        public void inserting()
        {
            UserDetails u = new UserDetails();
           
            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                u.USERID = dtclstr.Rows[s][0].ToString().ToUpper();
                u.USERNAME = dtclstr.Rows[s][1].ToString().ToUpper();
                u.PASSWORD = dtclstr.Rows[s][2].ToString().ToUpper();
                u.USERERPNAME = dtclstr.Rows[s][3].ToString().ToUpper();

                u.EMPCODE = dtclstr.Rows[s][4].ToString().ToUpper();
                u.DESIGNATION = dtclstr.Rows[s][5].ToString().ToUpper();
                u.DEPARTMENT = dtclstr.Rows[s][6].ToString().ToUpper();
                u.USERRIGHTS = dtclstr.Rows[s][7].ToString().ToUpper();

                u.MOBILENO = dtclstr.Rows[s][8].ToString().ToUpper();
                u.EMAILID = dtclstr.Rows[s][9].ToString().ToUpper();
               
                u.CREATEDBY = Session["UserName"].ToString();
                u.STATUS = "Y";


                try
                {
                    uMGT.UserInsertUserDetails(u, "INSERT");
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
            clrds.Tables[0].Columns.Add("USER_ID");
            clrds.Tables[0].Columns.Add("USER_NAME");
            clrds.Tables[0].Columns.Add("PASSWORD");
            clrds.Tables[0].Columns.Add("USER_ERP_NAME");

            clrds.Tables[0].Columns.Add("EMP_CODE");
            clrds.Tables[0].Columns.Add("DESIGNATION");
            clrds.Tables[0].Columns.Add("DEPARTMENT");
            clrds.Tables[0].Columns.Add("USER_RIGHTS");

            clrds.Tables[0].Columns.Add("MOBILE_NO");
            clrds.Tables[0].Columns.Add("EMAIL_ID");
            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvUser.DataSource = clrds;
            gvUser.DataBind();
            int columncount = gvUser.Rows[0].Cells.Count;
            gvUser.Rows[0].Cells.Clear();
            gvUser.Rows[0].Cells.Add(new TableCell());
            gvUser.Rows[0].Cells[0].ColumnSpan = columncount;
            gvUser.Rows[0].Cells[0].Text = "No Records Found";
        }
        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            insertdataintosql1(txtUserID.Text, txtUserName.Text, txtPassword.Text, txtConfirmPassword.Text, txtUserERPName.Text, txtEmpCode.Text, txtDeisgnation.Text, txtDepartment.Text, txtUserRights.Text, txtMobileNo.Text, txtEmailID.Text, "Y");
            ClearControls();
        }

        public void insertdataintosql1(string uid, string uname, string pswd, string cpswd, string erpname, string empcode, string desig, string dept, string usrrights, string mblno, string emailid, string sts)
        {
            bool b = false;
            try
            {
                UserDetails u = new UserDetails();
                u.USERID = txtUserID.Text.Trim();
                u.USERNAME = txtUserName.Text.Trim();
                u.PASSWORD = txtPassword.Text.Trim();
                u.USERERPNAME = txtUserERPName.Text.Trim();

                u.EMPCODE = txtEmpCode.Text.Trim();
                u.DESIGNATION = txtDeisgnation.Text.Trim();
                u.DEPARTMENT = txtDepartment.Text.Trim();
                u.USERRIGHTS = txtUserRights.Text.Trim();

                u.MOBILENO = txtMobileNo.Text.Trim();
                u.EMAILID = txtEmailID.Text.Trim();
                

                u.CREATEDBY = Session["UserName"].ToString();
                u.STATUS = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = uMGT.GetUserDetails(u.USERID);
                    if (res == "N")
                    {
                        uMGT.UserInsertUserDetails(u, "INSERT");
                        lblMessage.Text = "User inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "User already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    uMGT.UserInsertUserDetails(u, "UPDATE");
                    lblMessage.Text = "User updated sucessfully";
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
                Utils.LogError(err.ToString(), Request);
            }
        }


        private void ClearControls()
        {
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtUserERPName.Text = "";

            txtEmpCode.Text = "";
            txtDeisgnation.Text = "";
            txtDepartment.Text = "";
            txtUserRights.Text = "";
            txtMobileNo.Text = "";
            txtEmailID.Text = "";
            txtUserID.Focus();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Concat(Server.MapPath("~/UploadFiles/"), excelUpload.FileName);
                OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
                //Save File as Temp then you can delete it if you want
                excelUpload.SaveAs(path);
                string query = "select USER_ID, USER_NAME, PASSWORD, USER_ERP_NAME, EMP_CODE, DESIGNATION, DEPARTMENT, USER_RIGHTS, MOBILE_NO, EMAIL_ID, 'V' as INS_STS  from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvUser.DataSource = dtclstr;
                    gvUser.DataBind();
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
                Utils.LogError(err.ToString(), Request);
            }
        }

        private void bindGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = uMGT.GetUser("0");
                gvUser.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvUser.DataSource = dt;
                    gvUser.DataBind();
                }
                else
                {
                    gvUser.DataSource = null;
                    gvUser.DataBind();

                }
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Request);
            }
        }
    }
}