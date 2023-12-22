using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using GPI;

namespace GPILWebApp
{
    public partial class VarietyMaster : System.Web.UI.Page
    {
        string variety, vtype, vname, vdesc, strsql;
        public static DataTable dtclstr = new DataTable();
        public static string status = "";
        VarietyManagement vMgt = new VarietyManagement();

        public string Variety
        {
            get
            {
                return variety;
            }

            set
            {
                variety = value;
            }
        }

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

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void gvVariety_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVariety.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void gvVariety_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvVariety.EditIndex = -1;
            LoadData();
        }

        protected void gvVariety_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox vrty = (TextBox)gvVariety.FooterRow.FindControl("txtAddvareity");
                    TextBox vrtytyp = (TextBox)gvVariety.FooterRow.FindControl("txtAddvrytyp");
                    TextBox vrtyname = (TextBox)gvVariety.FooterRow.FindControl("txtAddvrypname");
                    TextBox vrtydesc = (TextBox)gvVariety.FooterRow.FindControl("txtAddvrtydesc");
                    TextBox flg = (TextBox)gvVariety.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["VARIETY"] = vrty.Text;
                    row["VARIETY_TYPE"] = vrtytyp.Text;
                    row["VARIETY_NAME"] = vrtyname.Text;
                    row["VARIETY_DESC"] = vrtydesc.Text;
                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvVariety.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvVariety_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label variety = (Label)gvVariety.Rows[e.RowIndex].FindControl("lVariety");
                TextBox vrtytyp = (TextBox)gvVariety.Rows[e.RowIndex].FindControl("txtvrytyp");
                TextBox vrtyname = (TextBox)gvVariety.Rows[e.RowIndex].FindControl("txtvrypname");
                TextBox vrtydesc = (TextBox)gvVariety.Rows[e.RowIndex].FindControl("txtvrtydesc");
                TextBox flg = (TextBox)gvVariety.Rows[e.RowIndex].FindControl("txtupdatests");
                DataRow[] rows = dtclstr.Select("VARIETY ='" + variety.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["VARIETY_TYPE"] = vrtytyp.Text;
                        row["VARIETY_NAME"] = vrtyname.Text;
                        row["VARIETY_DESC"] = vrtydesc.Text;
                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvVariety.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvVariety_RowDataBound(object sender, GridViewRowEventArgs e)
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


        public bool validate()
        {
            int i = 0;
            try
            {

                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {

                    string supcd = dtclstr.Rows[d]["VARIETY"].ToString();
                    string supname = dtclstr.Rows[d]["VARIETY_TYPE"].ToString();
                    string sitename = dtclstr.Rows[d]["VARIETY_NAME"].ToString();

                    string gpisupcd = dtclstr.Rows[d]["VARIETY_DESC"].ToString();

                    if (supname.Trim() == string.Empty)
                    {
                        update(supcd, "N");
                        i = i + 1;
                    }
                    else if (sitename.Trim() == string.Empty)
                    {
                        update(supcd, "N");
                        i = i + 1;
                    }
                    else
                    {

                        string s = "";
                        s = vMgt.GetVarietyDetails(supcd);
                        if (s == "Y")
                        {
                            update(supcd, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(supcd, "Y");
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
                DataRow[] rows = dtclstr.Select("Variety ='" + itemcode + "'");
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


                gvVariety.EditIndex = -1;
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
        public void clrgridview()
        {
            DataSet clrds = new DataSet();
            clrds.Tables.Add("TEMP");
            clrds.Tables[0].Columns.Add("VARIETY");
            clrds.Tables[0].Columns.Add("VARIETY_TYPE");
            clrds.Tables[0].Columns.Add("VARIETY_NAME");
            clrds.Tables[0].Columns.Add("VARIETY_DESC");
            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvVariety.DataSource = clrds;
            gvVariety.DataBind();
            int columncount = gvVariety.Rows[0].Cells.Count;
            gvVariety.Rows[0].Cells.Clear();
            gvVariety.Rows[0].Cells.Add(new TableCell());
            gvVariety.Rows[0].Cells[0].ColumnSpan = columncount;
            gvVariety.Rows[0].Cells[0].Text = "No Records Found";
        }
        private void LoadData()
        {
            gvVariety.DataSource = dtclstr;
            gvVariety.DataBind();

        }


        protected string valid(OleDbDataReader myreader, int stval)//if any columns are found null then they are replaced by zero
        {
            object val = myreader[stval];
            if (val != DBNull.Value)
                return val.ToString();
            else
                return Convert.ToString(0);
        }
        public void insertdataintosql1(string variety, string vtype, string vname, string vdesc, string sts)
        {
            bool b = false;
            try
            {
                Variety v = new Variety();
                v.VarietyCode = txtVarietyCode.Text.Trim();
                v.VarietyName = txtVarietyName.Text.Trim();
                v.VarietyDesc = txtVarietyDesc.Text.Trim();
                v.VarietyType = txtVarietyType.Text.Trim();
                v.CreatedBy = Session["UserName"].ToString();
                v.Status = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = vMgt.GetVarietyDetails(v.VarietyCode);
                    if (res == "N")
                    {
                        vMgt.UserInsertVariety(v, "INSERT");
                        lblMessage.Text = "Variety inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;                    
                    }
                    else
                    {
                        lblMessage.Text = "Variety already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    vMgt.UserInsertVariety(v, "UPDATE");
                    lblMessage.Text = "Variety updated sucessfully";
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


        public void inserting()
        {
            Variety v = new Variety();
            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                v.VarietyCode = dtclstr.Rows[s][0].ToString().ToUpper();
                v.VarietyType = dtclstr.Rows[s][1].ToString().ToUpper();
                v.VarietyName = dtclstr.Rows[s][2].ToString().ToUpper();
                v.VarietyDesc = dtclstr.Rows[s][3].ToString().ToUpper();
                v.CreatedBy = Session["UserName"].ToString();
                v.Status = "Y";


                try
                {
                    vMgt.UserInsertVariety(v, "INSERT");
                    lblMessage.Text = "Variety inserted sucessfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                    divImportSave.Visible = false;
                    idUpload.Visible = false;               
                }
                catch (Exception ee)
                {
                    insertdataintosql1(Variety, vtype, vname, vdesc, "Y");
                }
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Village Master Details Imported');", true);
            }
        }



        protected void gvVariety_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label variety = (Label)gvVariety.Rows[e.RowIndex].FindControl("lVariety");
                DataRow[] rowsdel = dtclstr.Select("VARIETY ='" + variety.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvVariety.EditIndex = -1;
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

        protected void gvVariety_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvVariety.EditIndex = e.NewEditIndex;
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
            insertdataintosql1(txtVarietyCode.Text, txtVarietyType.Text, txtVarietyName.Text, txtVarietyDesc.Text, "Y");
            ClearControls();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        private void ClearControls()
        {
            txtVarietyCode.Text = "";
            txtVarietyDesc.Text = "";
            txtVarietyName.Text = "";
            txtVarietyType.Text = "";
            txtVarietyCode.Focus();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Concat(Server.MapPath("~/UploadFiles/"), excelUpload.FileName);
                OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
                //Save File as Temp then you can delete it if you want
                excelUpload.SaveAs(path);
                string query = "select VARIETY , VARIETY_TYPE, VARIETY_NAME, VARIETY_DESC, 'V' as INS_STS  from [Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvVariety.DataSource = dtclstr;
                    gvVariety.DataBind();
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
                dt = vMgt.GetVariety("0");
                gvVariety.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvVariety.DataSource = dt;
                    gvVariety.DataBind();
                }
                else
                {
                    gvVariety.DataSource = null;
                    gvVariety.DataBind();

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


        protected void btnCan_Click(object sender, EventArgs e)
        {

        }
    }
}