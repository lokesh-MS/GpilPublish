using GPI;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class JesCrop : System.Web.UI.Page
    {
        public static DataTable dtclstr = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
                //LoadData();
            }
        }
        private void LoadData()
        {
            gvjes.DataSource = dtclstr;
            gvjes.DataBind();

        }

        private void bindGrid()
        {
            CropManagement cMGT = new CropManagement();
            DataTable dt = new DataTable();
            try
            {
                dt = cMGT.GetJes("0");
                //gvjes.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvjes.DataSource = dt;
                    gvjes.DataBind();
                }
                else
                {
                    gvjes.DataSource = null;
                    gvjes.DataBind();

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
            //return dt;
        }

        protected void gvjes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvjes.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void gvjes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CropManagement cMGT = new CropManagement();
            DataTable dt = new DataTable();
            try
            {
               // dt = cMGT.InsertJes("0");
                if (e.CommandName.Equals("AddNEW"))
                {

                    TextBox crop = (TextBox)gvjes.FooterRow.FindControl("txtCropFooter");
                    TextBox cropyear = (TextBox)gvjes.FooterRow.FindControl("txtCropYearFooter");
                    TextBox sCropYear = (TextBox)gvjes.FooterRow.FindControl("txtShortCropYearFooter");
                    //TextBox flg = (TextBox)gvCrop.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["CROP"] = crop.Text;
                    row["CROP_YEAR"] = cropyear.Text;
                    row["Attribute1"] = sCropYear.Text;
                    //row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvjes.EditIndex = -1;
                    LoadData();
                    lblSuccessMsg.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
        //protected void btnManualSave_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnImportSave_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{

        //}

        //protected void rdbimport_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbimport.Checked == true)
        //    {
        //        idUpload.Visible = true;
        //        idManual.Visible = false;
        //        idManualButton.Visible = false;
        //        //divGrid.Visible = false;
        //        divImportSave.Visible = true;
        //        lblMessage.Text = "";

        //    }
        //}

        //protected void rdbmanual_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbmanual.Checked == true)
        //    {
        //        idManual.Visible = true;
        //        idManualButton.Visible = true;
        //        idUpload.Visible = false;
        //        //divGrid.Visible = false;
        //        divImportSave.Visible = false;
        //        lblMessage.Text = "";
        //    }
        //}
    }
}