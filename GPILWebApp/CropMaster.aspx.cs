using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPI;
using System.Data.OleDb;

namespace GPILWebApp
{
    public partial class CropMaster : System.Web.UI.Page
    {
        CropManagement cMGT = new CropManagement();
        public static string status = "";
        public static DataTable dtclstr = new DataTable();
        string crp, crpyr,scrpyr, strsql;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            gvCrop.DataSource = dtclstr;
            gvCrop.DataBind();

        }
        protected void rdbimport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbimport.Checked == true)
            {
                idUpload.Visible = true;
                idManual.Visible = false;
                idManualButton.Visible = false;
                divGrid.Visible = false;
                // divImportSave.Visible = true;
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

                //select CROP, CROP_YEAR, 'V' as INS_STS from GPIL_CROP_MASTER where status ='Y'
                string query = "select CROP, CROP_YEAR,SHORT_CROP_YEAR, 'V' as INS_STS  from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvCrop.DataSource = dtclstr;
                    gvCrop.DataBind();
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

        protected void gvCrop_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCrop.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void gvCrop_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCrop.EditIndex = -1;
            LoadData();
        }

        protected void gvCrop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox crop = (TextBox)gvCrop.FooterRow.FindControl("txtAddCrop");
                    TextBox cropyear = (TextBox)gvCrop.FooterRow.FindControl("txtAddCropYear");
                    TextBox sCropYear = (TextBox)gvCrop.FooterRow.FindControl("txtAddShortCropYear");
                    TextBox flg = (TextBox)gvCrop.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["CROP"] = crop.Text;
                    row["CROP_YEAR"] = cropyear.Text;
                    row["Attribute1"] = sCropYear.Text;
                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvCrop.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void gvCrop_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label crop = (Label)gvCrop.Rows[e.RowIndex].FindControl("lCrop");
                TextBox cropyear = (TextBox)gvCrop.Rows[e.RowIndex].FindControl("txtCropYear");
                TextBox Shortcropyear = (TextBox)gvCrop.Rows[e.RowIndex].FindControl("txtShortCropYear");
                TextBox flg = (TextBox)gvCrop.Rows[e.RowIndex].FindControl("txtupdatests");
                DataRow[] rows = dtclstr.Select("CROP ='" + crop.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["CROP_YEAR"] = cropyear.Text;
                        row["Attribute1"] = Shortcropyear.Text;
                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvCrop.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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

        public void inserting()
        {
            Crop c = new Crop();
            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                c.CROP = dtclstr.Rows[s][0].ToString().ToUpper();
                c.CROPYEAR = dtclstr.Rows[s][1].ToString().ToUpper();
                c.ATTRIBUTE1 = dtclstr.Rows[s][2].ToString().ToUpper();
                c.CREATEDBY = Session["UserName"].ToString();
                c.STATUS = "Y";


                try
                {
                    cMGT.UserInsertCrop(c, "INSERT");
                    lblMessage.Text = "Crop inserted sucessfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                    divImportSave.Visible = false;
                    idUpload.Visible = false;
                }
                catch (Exception ex)
                {
                    insertdataintosql1(crp, crpyr, scrpyr, "Y");
                }
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Village Master Details Imported');", true);
            }
        }
        public void clrgridview()
        {
            DataSet clrds = new DataSet();
            clrds.Tables.Add("TEMP");
            clrds.Tables[0].Columns.Add("CROP");
            clrds.Tables[0].Columns.Add("CROP_YEAR");

            clrds.Tables[0].Columns.Add("ATTRIBUTE1");
            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvCrop.DataSource = clrds;
            gvCrop.DataBind();
            int columncount = gvCrop.Rows[0].Cells.Count;
            gvCrop.Rows[0].Cells.Clear();
            gvCrop.Rows[0].Cells.Add(new TableCell());
            gvCrop.Rows[0].Cells[0].ColumnSpan = columncount;
            gvCrop.Rows[0].Cells[0].Text = "No Records Found";
        }
        public bool validate()
        {
            int i = 0;
            try
            {

                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {

           /*supcd*/  string crp = dtclstr.Rows[d]["CROP"].ToString();
         /*supname*/  string crpyr = dtclstr.Rows[d]["CROP_YEAR"].ToString();
                    string shortcrpyr = dtclstr.Rows[d]["ATTRIBUTE1"].ToString();


                    if (crpyr.Trim() == string.Empty)
                    {
                        update(crp, "N");
                        i = i + 1;
                    }
                   
                    else
                    {

                        string s = "";
                        s = cMGT.GetCropDetails(crp);
                        if (s == "Y")
                        {
                            update(crp, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(crp, "Y");
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

        public void update(string itemcode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("Crop ='" + itemcode + "'");
             
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["INS_STS"] = flg;
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }


                gvCrop.EditIndex = -1;
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


        
        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            insertdataintosql1(txtCrop.Text, txtCropYear.Text,txtShortCropYear.Text, "Y");
            ClearControls();
        }

        public void insertdataintosql1(string crp, string crpyr, string sCYear, string sts)
        {
            bool b = false;
            try
            {
                Crop c = new Crop();
                
                c.CROP = txtCrop.Text.Trim();
                c.CROPYEAR = txtCropYear.Text.Trim();
                c.ATTRIBUTE1 = txtShortCropYear.Text;
                c.CREATEDBY = Session["UserName"].ToString();
                c.STATUS = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = cMGT.GetCropDetails(c.CROP);
                    if (res == "N")
                    {
                        cMGT.UserInsertCrop(c, "INSERT");
                        lblMessage.Text = "Crop inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "Crop already exists.";
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Crop Already exist');", true);

                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    cMGT.UserInsertCrop(c, "UPDATE");
                    lblMessage.Text = "Crop updated sucessfully";
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
            txtCrop.Text = "";
            txtCropYear.Text = "";
            txtCrop.Focus();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvCrop_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvCrop_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label crop = (Label)gvCrop.Rows[e.RowIndex].FindControl("lCrop");
                DataRow[] rowsdel = dtclstr.Select("CROP ='" + crop.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvCrop.EditIndex = -1;
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

        protected void gvCrop_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvCrop.EditIndex = e.NewEditIndex;
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


        private void bindGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = cMGT.GetCrop("0");
                gvCrop.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvCrop.DataSource = dt;
                    gvCrop.DataBind();
                }
                else
                {
                    gvCrop.DataSource = null;
                    gvCrop.DataBind();

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