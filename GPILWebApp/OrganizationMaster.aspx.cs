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
    public partial class OrganizationMaster : System.Web.UI.Page
    {

        public static DataTable dtclstr = new DataTable();
        CrystalReportData crd = new CrystalReportData();
        OrganizationManagement oMGT = new OrganizationManagement();
        string orgncode, orgnname, orgntype, orgnadd1, orgnadd2, orgnadd3, orgnadd4, orgnadd5, orgnadd6, orgnctry, orgnpincode, orgntellno, orgnmblno, orgnemailid, orgninsval, vrty;
        public static string status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDropDown();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }


        private void bindDropDown()
        {
            try
            {
                
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
                string query = "select ORGN_CODE, ORGN_NAME, ORGN_TYPE, VARIETY, ORGN_ADDRESS1, ORGN_ADDRESS2, ORGN_ADDRESS3, ORGN_COUNTRY, PIN_CODE, TEL_NO, MOBILE_NO, EMAIL_ID, INSURANCE_VAL 'V' as INS_STS  from[Sheet1$]";
                OleDbDataAdapter data = new OleDbDataAdapter(query, oconn);
                data.Fill(dtclstr);

                if (dtclstr.Rows.Count > 0)
                {
                    gvOrgn.DataSource = dtclstr;
                    gvOrgn.DataBind();
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

        protected void gvOrgn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrgn.PageIndex = e.NewPageIndex;
            LoadData();
        }
        private void LoadData()
        {
            gvOrgn.DataSource = dtclstr;
            gvOrgn.DataBind();

        }
        protected void gvOrgn_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrgn.EditIndex = -1;
            LoadData();
        }

        protected void gvOrgn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {

                    TextBox orgnCode = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrgn");
                    TextBox orgnName = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrganizationName");
                    TextBox orgnType = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrganizationType");
                    TextBox vrty = (TextBox)gvOrgn.FooterRow.FindControl("txtAddVariety");
                    TextBox orgnAdd1 = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrgnAddress1");
                    TextBox orgnAdd2 = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrgnAddress2");
                    TextBox orgnAdd3 = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrgnAddress3");
                    TextBox orgnCtry = (TextBox)gvOrgn.FooterRow.FindControl("txtAddOrgnCountry");
                    TextBox tellNO = (TextBox)gvOrgn.FooterRow.FindControl("txtAddTelNo");
                    TextBox pinCode = (TextBox)gvOrgn.FooterRow.FindControl("txtAddPinCode");
                    TextBox mblNO = (TextBox)gvOrgn.FooterRow.FindControl("txtAddMobileNo");
                    TextBox emailID = (TextBox)gvOrgn.FooterRow.FindControl("txtAddEmailID");
                    TextBox insurancevalue = (TextBox)gvOrgn.FooterRow.FindControl("txtAddInsuranceValue");                    
                    TextBox flg = (TextBox)gvOrgn.FooterRow.FindControl("txtAddupdatests");
                    DataRow row = dtclstr.NewRow();

                    row["ORGN_CODE"] = orgnCode.Text;
                    row["ORGN_NAME"] = orgnName.Text;
                    row["ORGN_TYPE"] = orgnType.Text;
                    row["VARIETY"] = vrty.Text;
                    row["ORGN_ADDRESS1"] = orgnAdd1.Text;
                    row["ORGN_ADDRESS2"] = orgnAdd2.Text;
                    row["ORGN_ADDRESS3"] = orgnAdd3.Text;
                    row["ORGN_COUNTRY"] = orgnCtry.Text;
                    row["PIN_CODE"] = pinCode.Text;
                    row["TEL_NO"] = tellNO.Text;
                    row["MOBILE_NO"] = mblNO.Text;
                    row["EMAIL_ID"] = emailID.Text;
                    row["INSURANCE_VAL"] = insurancevalue.Text;
                    row["INS_STS"] = flg.Text.Trim();
                    dtclstr.Rows.Add(row);
                    gvOrgn.EditIndex = -1;
                    LoadData();
                    lblMessage.Text = "Record Inserted Successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvOrgn_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label OrgnCode = (Label)gvOrgn.Rows[e.RowIndex].FindControl("lOrganizationCode");
                TextBox orgnName = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtOrganizationName");
                TextBox OrgnType = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtOrganizationType");
                TextBox vrty = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtVariety");
                TextBox orgnAdd1 = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtOrgnAddress1");

                TextBox orgnAdd2 = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtOrgnAddress2");
                TextBox orgnAdd3 = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtOrgnAddress3");
                TextBox orgnCntry = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtOrgnCountry");
                TextBox pinCode = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtPinCode");
                TextBox tellNO = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtTelNo");
                TextBox mblNO = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtMobileNo");
                TextBox emailID = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtEmailID");
                TextBox insurancevalue = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtInsuranceValue");
                TextBox flg = (TextBox)gvOrgn.Rows[e.RowIndex].FindControl("txtupdatests");
                DataRow[] rows = dtclstr.Select("ORGN_CODE ='" + OrgnCode.Text + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["ORGN_NAME"] = orgnName.Text;
                        row["ORGN_TYPE"] = OrgnType.Text;
                        row["VARIETY"] = vrty.Text;

                        row["ORGN_ADDRESS1"] = orgnAdd1.Text;
                        row["ORGN_ADDRESS2"] = orgnAdd2.Text;
                        row["ORGN_ADDRESS3"] = orgnAdd3.Text;

                        row["ORGN_COUNTRY"] = orgnCntry.Text;
                        row["PIN_CODE"] = pinCode.Text;
                        row["TEL_NO"] = tellNO.Text;

                        row["MOBILE_NO"] = mblNO.Text;
                        row["EMAIL_ID"] = emailID.Text;
                        row["INSURANCE_VAL"] = insurancevalue.Text;

                        
                        row["INS_STS"] = flg.Text.Trim();
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }
                gvOrgn.EditIndex = -1;
                LoadData();
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvOrgn_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvOrgn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                Label Organization = (Label)gvOrgn.Rows[e.RowIndex].FindControl("lOrganizationCode");
                DataRow[] rowsdel = dtclstr.Select("ORGN_CODE ='" + Organization.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                gvOrgn.EditIndex = -1;
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

        protected void gvOrgn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvOrgn.EditIndex = e.NewEditIndex;
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

                    string orgCode = dtclstr.Rows[d]["ORGN_CODE"].ToString();
                    string orgName = dtclstr.Rows[d]["ORGN_NAME"].ToString();
                    string orgType = dtclstr.Rows[d]["ORGN_TYPE"].ToString();

                    string varty = dtclstr.Rows[d]["VARIETY"].ToString();

                    string orgAddd1 = dtclstr.Rows[d]["ORGN_ADDRESS1"].ToString();
                    string orgAddd2 = dtclstr.Rows[d]["ORGN_ADDRESS2"].ToString();
                    string orgAddd3 = dtclstr.Rows[d]["ORGN_ADDRESS3"].ToString();

                    string orgCntry = dtclstr.Rows[d]["ORGN_COUNTRY"].ToString();

                    string orgPinCode = dtclstr.Rows[d]["PIN_CODE"].ToString();
                    string orgTellNO = dtclstr.Rows[d]["TEL_NO"].ToString();
                    string orgMblNO = dtclstr.Rows[d]["MOBILE_NO"].ToString();

                    string orgEmailID = dtclstr.Rows[d]["EMAIL_ID"].ToString();

                    string orgInsValue = dtclstr.Rows[d]["INSURANCE_VAL"].ToString();

                    if (orgName.Trim() == string.Empty)
                    {
                        update(orgCode, "N");
                        i = i + 1;
                    }
                    else if (orgType.Trim() == string.Empty)
                    {
                        update(orgCode, "N");
                        i = i + 1;
                    }
                    else
                    {

                        string s = "";
                        s = oMGT.GetOrganizationDetails(orgCode);
                        if (s == "Y")
                        {
                            update(orgCode, "N");
                            i = i + 1;
                        }
                        else
                        {
                            update(orgCode, "Y");
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

        public void update(string ORGNcode, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("ORGN_CODE ='" + ORGNcode + "'");
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


                gvOrgn.EditIndex = -1;
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

            OrganizatioDetails o = new OrganizatioDetails();
            //Variety v = new Variety();
            for (int s = 0; s < dtclstr.Rows.Count; s++)
            {
                o.ORGNCODE = dtclstr.Rows[s][0].ToString().ToUpper();
                o.ORGNNAME = dtclstr.Rows[s][1].ToString().ToUpper();
                o.ORGNTYPE = dtclstr.Rows[s][2].ToString().ToUpper();
                o.ORGNADDRESS1 = dtclstr.Rows[s][3].ToString().ToUpper();
                o.ORGNADDRESS2 = dtclstr.Rows[s][4].ToString().ToUpper();
                o.ORGNADDRESS3 = dtclstr.Rows[s][5].ToString().ToUpper();
                o.ORGNADDRESS4 = dtclstr.Rows[s][6].ToString().ToUpper();
                o.ORGNADDRESS5 = dtclstr.Rows[s][7].ToString().ToUpper();
                o.ORGNADDRESS6 = dtclstr.Rows[s][8].ToString().ToUpper();
                o.ORGNCOUNTRY = dtclstr.Rows[s][9].ToString().ToUpper();
                o.PINCODE = dtclstr.Rows[s][10].ToString().ToUpper();
                o.TELNO = dtclstr.Rows[s][11].ToString().ToUpper();
                o.MOBILENO = dtclstr.Rows[s][12].ToString().ToUpper();
                o.EMAILID = dtclstr.Rows[s][13].ToString().ToUpper();
                o.INSURANCEVAL = dtclstr.Rows[s][14].ToString().ToUpper();
                o.VARIETY = dtclstr.Rows[s][15].ToString().ToUpper();               
                o.CREATEDBY = Session["UserName"].ToString();
                o.STATUS = "Y";


                try
                {
                    oMGT.UserInsertOrganization(o, "INSERT");
                    lblMessage.Text = "Variety inserted sucessfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                    divImportSave.Visible = false;
                    idUpload.Visible = false;
                }
                catch (Exception ee)
                {
                    insertdataintosql1(orgncode, orgnname, orgntype, orgnadd1, orgnadd2, orgnadd3, orgnadd4, orgnadd5, orgnadd6, orgnctry, orgnpincode, orgntellno, orgnmblno, orgnemailid, orgninsval, vrty, "Y");
                }
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Village Master Details Imported');", true);
            }
        }

        public void clrgridview()
        {
            DataSet clrds = new DataSet();
            clrds.Tables.Add("TEMP");
            clrds.Tables[0].Columns.Add("ORGN_CODE");
            clrds.Tables[0].Columns.Add("ORGN_NAME");
            clrds.Tables[0].Columns.Add("ORGN_TYPE");
            clrds.Tables[0].Columns.Add("VARIETY");

            clrds.Tables[0].Columns.Add("ORGN_ADDRESS1");
            clrds.Tables[0].Columns.Add("ORGN_ADDRESS2");
            clrds.Tables[0].Columns.Add("ORGN_ADDRESS3");
            clrds.Tables[0].Columns.Add("ORGN_COUNTRY");

            clrds.Tables[0].Columns.Add("PIN_CODE");
            clrds.Tables[0].Columns.Add("TEL_NO");
            clrds.Tables[0].Columns.Add("MOBILE_NO");
            clrds.Tables[0].Columns.Add("EMAIL_ID");
            clrds.Tables[0].Columns.Add("INSURANCE_VAL");

            clrds.Tables[0].Columns.Add("INS_STS");
            clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
            gvOrgn.DataSource = clrds;
            gvOrgn.DataBind();
            int columncount = gvOrgn.Rows[0].Cells.Count;
            gvOrgn.Rows[0].Cells.Clear();
            gvOrgn.Rows[0].Cells.Add(new TableCell());
            gvOrgn.Rows[0].Cells[0].ColumnSpan = columncount;
            gvOrgn.Rows[0].Cells[0].Text = "No Records Found";
        }

        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            insertdataintosql1(txtOrganizationCode.Text, txtCOrganizationName.Text, txtOrganizationType.Text, txtOrganizationAddress1.Text, txtOrganizationAddress2.Text, txtOrganizationAddress3.Text, txtOrganizationAddress4.Text, txtOrganizationAddress5.Text, txtOrganizationAddress6.Text, txtOrganizationCountry.Text, txtOrganizationPincode.Text, txtTelephoneNo.Text, txtMobileNo.Text, txtEmailID.Text, txtInsuranceValue.Text, ddlVariety.Text, "Y");
            ClearControls();
        }

        public void insertdataintosql1(string orgnCode, string orgnName, string orgnType, string orgnAdd1, string orgnAdd2, string orgnAdd3, string orgnAdd4, string orgnAdd5, string orgnAdd6, string orgnCtry, string orgnPincode, string orgnphnNO, string orgnmblNO, string orgnemailID, string orgnInsurenvalue, string orgnvariety, string sts)
        {
            bool b = false;
            try
            {

                OrganizatioDetails O = new OrganizatioDetails();
                O.ORGNCODE = txtOrganizationCode.Text.Trim();
                O.ORGNNAME = txtCOrganizationName.Text.Trim();
                O.ORGNTYPE = txtOrganizationType.Text.Trim();
                O.ORGNADDRESS1 = txtOrganizationAddress1.Text.Trim();

                O.ORGNADDRESS2 = txtOrganizationAddress2.Text.Trim();
                O.ORGNADDRESS3 = txtOrganizationAddress3.Text.Trim();
                O.ORGNADDRESS4 = txtOrganizationAddress4.Text.Trim();
                O.ORGNADDRESS5 = txtOrganizationAddress5.Text.Trim();

                O.ORGNADDRESS6 = txtOrganizationAddress6.Text.Trim();
                O.ORGNCOUNTRY  = txtOrganizationCountry.Text.Trim();
                O.PINCODE= txtOrganizationPincode.Text.Trim();
                O.TELNO= txtTelephoneNo.Text.Trim();

               O.MOBILENO = txtMobileNo.Text.Trim();
               O.EMAILID = txtEmailID.Text.Trim();
               O.INSURANCEVAL = txtInsuranceValue.Text.Trim();
               O.VARIETY = ddlVariety.Text.Trim();
               O.CREATEDBY = Session["UserName"].ToString();
               O.STATUS = "Y";

                if (btnManualSave.Text == "Save")
                {
                    string res = "";
                    res = oMGT.GetOrganizationDetails(O.ORGNCODE);
                    if (res == "N")
                    {
                        oMGT.UserInsertOrganization(O, "INSERT");
                        lblMessage.Text = "Organization inserted sucessfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "Organization already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
                else if (btnManualSave.Text == "Update")
                {
                    oMGT.UserInsertOrganization(O, "UPDATE");
                    lblMessage.Text = "Organization updated sucessfully";
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
            txtOrganizationCode.Text = "";
            txtCOrganizationName.Text = "";
            txtOrganizationType.Text = "";
            txtOrganizationAddress1.Text = "";
            txtOrganizationAddress2.Text = "";
            txtOrganizationAddress3.Text = "";
            txtOrganizationAddress4.Text = "";
            txtOrganizationAddress5.Text = "";
            txtOrganizationAddress6.Text = "";
            txtOrganizationCountry.Text = "";
            txtOrganizationPincode.Text = "";
            txtMobileNo.Text = "";
            txtTelephoneNo.Text = "";
            txtEmailID.Text = "";
            txtInsuranceValue.Text = "";
            txtOrganizationCode.Focus();


            //txtVarietyCode.Text = "";
            //txtVarietyDesc.Text = "";
            //txtVarietyName.Text = "";
            //txtVarietyType.Text = "";
            //txtVarietyCode.Focus();
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
                dt = oMGT.GetOrganization("0");
                gvOrgn.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    gvOrgn.DataSource = dt;
                    gvOrgn.DataBind();
                }
                else
                {
                    gvOrgn.DataSource = null;
                    gvOrgn.DataBind();

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