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
    public partial class OperationReceipeMaster : System.Web.UI.Page
    {

        string variety, vtype, vname, vdesc, strsql;
        public static DataTable dtclstr = new DataTable();
        public static string status = "";
        OperationReceipeManagement orMGT = new OperationReceipeManagement();
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
                string query = "select RECIPE_CODE , OPERATION_RECIPE, 'V' as INS_STS  from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvOprReceipe.DataSource = dtclstr;
                    gvOprReceipe.DataBind();
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
                dt = orMGT.GetOperationReceipe("0");
                gvOprReceipe.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvOprReceipe.DataSource = dt;
                    gvOprReceipe.DataBind();
                }
                else
                {
                    gvOprReceipe.DataSource = null;
                    gvOprReceipe.DataBind();
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

        protected void gvOprReceipe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOprReceipe.PageIndex = e.NewPageIndex;
            LoadData();
        }
        private void LoadData()
        {
            gvOprReceipe.DataSource = dtclstr;
            gvOprReceipe.DataBind();

        }
        protected void gvOprReceipe_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOprReceipe.EditIndex = -1;
            LoadData();
        }

        protected void gvOprReceipe_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox rCode = (TextBox)gvOprReceipe.FooterRow.FindControl("txtAddReceipeCode");
                    TextBox oprReceipe = (TextBox)gvOprReceipe.FooterRow.FindControl("txtAddOperationReceipe");
                    TextBox flg = (TextBox)gvOprReceipe.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["RECIPE_CODE"] = rCode.Text;
                    row["OPERATION_RECIPE"] = oprReceipe.Text;
                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvOprReceipe.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvOprReceipe_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label receipCode = (Label)gvOprReceipe.Rows[e.RowIndex].FindControl("lReceipeCode");
                TextBox oprReceipe = (TextBox)gvOprReceipe.Rows[e.RowIndex].FindControl("txtOperationReceipe");
                TextBox flg = (TextBox)gvOprReceipe.Rows[e.RowIndex].FindControl("txtupdatests");
                DataRow[] rows = dtclstr.Select("RECIPE_CODE ='" + receipCode.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["OPERATION_RECIPE"] = oprReceipe.Text;
                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvOprReceipe.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvOprReceipe_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvOprReceipe_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label oprReceipe = (Label)gvOprReceipe.Rows[e.RowIndex].FindControl("lReceipeCode");
                DataRow[] rowsdel = dtclstr.Select("RECIPE_CODE ='" + oprReceipe.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvOprReceipe.EditIndex = -1;
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

        protected void gvOprReceipe_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvOprReceipe.EditIndex = e.NewEditIndex;
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

                    string rCode = dtclstr.Rows[d]["RECIPE_CODE"].ToString();
                    string oprReceipe = dtclstr.Rows[d]["OPERATION_RECIPE"].ToString();
                    

                    if (oprReceipe.Trim() == string.Empty)
                    {
                        update(rCode, "N");
                        i = i + 1;
                    }
                    //else if (sitename.Trim() == string.Empty)
                    //{
                    //    update(supcd, "N");
                    //    i = i + 1;
                    //}
                    else
                    {

                        string s = "";
                        s = orMGT.GetOperationReceipeDetails(rCode);
                        if (s == "Y")
                        {
                            update(rCode, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(rCode, "Y");
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

        public void update(string recpcode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("RECIPE_CODE ='" + recpcode + "'");
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


                gvOprReceipe.EditIndex = -1;
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
            OperationReciepeDetails oprRec = new OperationReciepeDetails();
            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                oprRec.RECIPECODE = dtclstr.Rows[s][0].ToString().ToUpper();
                oprRec.OPERATIONRECIPE = dtclstr.Rows[s][1].ToString().ToUpper();
                oprRec.CREATEDBY = Session["UserName"].ToString();
                oprRec.STATUS = "Y";


                try
                {
                    orMGT.UserInsertOperationReceipe(oprRec, "INSERT");
                    lblMessage.Text = "OperationReceipe inserted sucessfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                    divImportSave.Visible = false;
                    idUpload.Visible = false;
                }
                catch (Exception ee)
                {
                    //insertdataintosql1(Variety, vtype, vname, vdesc, "Y");
                }
                
            }
        }

        public void clrgridview()
        {
            DataSet clrds = new DataSet();
            clrds.Tables.Add("TEMP");
            clrds.Tables[0].Columns.Add("RECIPE_CODE");
            clrds.Tables[0].Columns.Add("OPERATION_RECIPE");
            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvOprReceipe.DataSource = clrds;
            gvOprReceipe.DataBind();
            int columncount = gvOprReceipe.Rows[0].Cells.Count;
            gvOprReceipe.Rows[0].Cells.Clear();
            gvOprReceipe.Rows[0].Cells.Add(new TableCell());
            gvOprReceipe.Rows[0].Cells[0].ColumnSpan = columncount;
            gvOprReceipe.Rows[0].Cells[0].Text = "No Records Found";
        }
        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            insertdataintosql1(txtReceipeCode.Text, txtOperationReceipe.Text, "Y");
            ClearControls();
        }

        public void insertdataintosql1(string recpCode, string operationreceipe, string sts)
        {
            bool b = false;
            try
            {
                OperationReciepeDetails or = new OperationReciepeDetails();
                or.RECIPECODE = txtReceipeCode.Text.Trim();
                or.OPERATIONRECIPE = txtOperationReceipe.Text.Trim();
                or.CREATEDBY = Session["UserName"].ToString();
                or.STATUS = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = orMGT.GetOperationReceipeDetails(or.RECIPECODE);
                    if (res == "N")
                    {
                        orMGT.UserInsertOperationReceipe(or, "INSERT");
                        lblMessage.Text = "OperationReceipe inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "OperationReceipe already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    orMGT.UserInsertOperationReceipe(or, "UPDATE");
                    lblMessage.Text = "OperationReceipe updated sucessfully";
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
            txtReceipeCode.Text = "";
            txtOperationReceipe.Text = "";
            txtReceipeCode.Focus();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}