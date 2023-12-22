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
    public partial class SubInventoryMaster : System.Web.UI.Page
    {
        public static DataTable dtclstr = new DataTable();
        public static string status = "";
        SubinventoryManagement siMGT = new SubinventoryManagement();
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
                string query = "select SUB_INV_CODE , SUB_INV_DESC, 'V' as INS_STS  from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvSubinv.DataSource = dtclstr;
                    gvSubinv.DataBind();
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

        protected void gvSubinv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvSubinv.PageIndex = e.NewPageIndex;
            LoadData();

        }
        private void LoadData()
        {
            gvSubinv.DataSource = dtclstr;
            gvSubinv.DataBind();

        }
        protected void gvSubinv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSubinv.EditIndex = -1;
            LoadData();
        }

        protected void gvSubinv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox sunbinvCode = (TextBox)gvSubinv.FooterRow.FindControl("txtAddSubinventoryCode");
                    TextBox subinvDesc = (TextBox)gvSubinv.FooterRow.FindControl("txtAddSubinventoryDescription");
                    TextBox flg = (TextBox)gvSubinv.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["SUB_INV_CODE"] = sunbinvCode.Text;
                    row["SUB_INV_DESC"] = subinvDesc.Text;
                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvSubinv.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void gvSubinv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label subinvCode = (Label)gvSubinv.Rows[e.RowIndex].FindControl("lSubinventoryCode");
                TextBox subinvDesc = (TextBox)gvSubinv.Rows[e.RowIndex].FindControl("txtSubinventoryDescription");
                TextBox flg = (TextBox)gvSubinv.Rows[e.RowIndex].FindControl("txtupdatests");
                DataRow[] rows = dtclstr.Select("SUB_INV_CODE ='" + subinvCode.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["SUB_INV_DESC"] = subinvDesc.Text;
                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvSubinv.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvSubinv_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvSubinv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label subinvCode = (Label)gvSubinv.Rows[e.RowIndex].FindControl("lSubinventoryCode");
                DataRow[] rowsdel = dtclstr.Select("SUB_INV_CODE ='" + subinvCode.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvSubinv.EditIndex = -1;
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

        protected void gvSubinv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvSubinv.EditIndex = e.NewEditIndex;
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

                    string siCode = dtclstr.Rows[d]["SUB_INV_CODE"].ToString();
                    string siDesc = dtclstr.Rows[d]["SUB_INV_DESC"].ToString();
                    

                    if (siDesc.Trim() == string.Empty)
                    {
                        update(siCode, "N");
                        i = i + 1;
                    }
                   
                    else
                    {

                        string s = "";
                        s = siMGT.GetSubInventoryDetails(siCode);
                        if (s == "Y")
                        {
                            update(siCode, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(siCode, "Y");
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

        public void update(string subinvcode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("SUB_INV_CODE ='" + subinvcode + "'");
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


                gvSubinv.EditIndex = -1;
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
            SubinventoryDetails si = new SubinventoryDetails();
            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                si.SUBINVCODE = dtclstr.Rows[s][0].ToString().ToUpper();
                si.SUBINVDESC = dtclstr.Rows[s][1].ToString().ToUpper();
                si.CREATEDBY = Session["UserName"].ToString();
                si.STATUS = "Y";


                try
                {
                    siMGT.UserInsertSubInventory(si, "INSERT");
                    lblMessage.Text = "SubInventory inserted sucessfully";
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
            clrds.Tables[0].Columns.Add("SUB_INV_CODE");
            clrds.Tables[0].Columns.Add("SUB_INV_DESC");
            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvSubinv.DataSource = clrds;
            gvSubinv.DataBind();
            int columncount = gvSubinv.Rows[0].Cells.Count;
            gvSubinv.Rows[0].Cells.Clear();
            gvSubinv.Rows[0].Cells.Add(new TableCell());
            gvSubinv.Rows[0].Cells[0].ColumnSpan = columncount;
            gvSubinv.Rows[0].Cells[0].Text = "No Records Found";
        }
        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            insertdataintosql1(txtSubInventoryCode.Text, txtSubInventoryDesc.Text, "Y");
            ClearControls();
        }

        public void insertdataintosql1(string subinvCode, string subinvDesc, string sts)
        {
            bool b = false;
            try
            {
                SubinventoryDetails si = new SubinventoryDetails();
                si.SUBINVCODE = txtSubInventoryCode.Text.Trim();
                si.SUBINVDESC = txtSubInventoryDesc.Text.Trim();
                si.CREATEDBY = Session["UserName"].ToString();
                si.STATUS = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = siMGT.GetSubInventoryDetails(si.SUBINVCODE);
                    if (res == "N")
                    {
                        siMGT.UserInsertSubInventory(si, "INSERT");
                        lblMessage.Text = "SubInventory inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "SubInventory already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    siMGT.UserInsertSubInventory(si, "UPDATE");
                    lblMessage.Text = "SubInventory updated sucessfully";
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
            txtSubInventoryCode.Text = "";
            txtSubInventoryDesc.Text = "";
            txtSubInventoryCode.Focus();
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
                dt = siMGT.GetSubInventory("0");
                gvSubinv.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvSubinv.DataSource = dt;
                    gvSubinv.DataBind();
                }
                else
                {
                    gvSubinv.DataSource = null;
                    gvSubinv.DataBind();

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