//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Collections;
//using System.Configuration;
//using System.Data;
//using System.Web.Security;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using System.Data.SqlClient;
//using System.Reflection.Emit;

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using GPILWebApp.ViewModel;
using GPIWebApp;

namespace GPILWebApp.CrystalReport.WebForms
{
    
    
    public partial class Permission : System.Web.UI.Page
    {
        ReportManagement rptMgt = new ReportManagement();
        DataSet ds = new DataSet();        
        
        string sSql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                //Response.Redirect("frmlogin.aspx");
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                fncDeletePermission(txtEmployeeID.Text);

                foreach (GridViewRow row in dgReports.Rows)
                {
                    string sProject = row.Cells[0].Text;
                    string sModule = row.Cells[1].Text;
                    string sForms = row.Cells[2].Text;
                    //if (sUserProject == sProject && sUserModule == sModule && sUserForms == sForms)
                    bool isSelected = (row.FindControl("chkSelect") as CheckBox).Checked;
                    if (isSelected == true)
                    {
                        fncInsertPermission(txtEmployeeID.Text.ToString().Trim(), sProject, sModule, sForms);
                    }
                    //{
                    //    (row.FindControl("chkSelect") as CheckBox).Checked = true;
                    //}            
                }
                fncClear();
                lblMessage.Text = "Data Saved...";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnClear_Click(object sender, ImageClickEventArgs e)
        {
            fncClear();
        }
        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            //fncClear();
            Response.Redirect("FrmHomePage.aspx");
        }
        protected void btnRGPSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text.Trim() == "")
                {
                    tblUser.Visible = false;
                    tblSearch.Visible = true;
                    txtEmployeeNameSearch.Focus();
                    dgReports.Visible = true;
                    lblMessage.Text = "";
                }
                else
                {
                    fncGetEmployeePermission(txtEmployeeID.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnFetch_Click(object sender, ImageClickEventArgs e)
        {
            subLoadSearch();
        }

        private void subLoadSearch()
        {
            try
            {
                ds = fncEmployeeSearch(txtEmployeeCodeSearch.Text.ToString().Trim(),
                    txtEmployeeNameSearch.Text.ToString().Trim(),
                    txtPhoneSearch.Text.ToString().Trim());
                dgEmployeeSearch.DataSource = ds.Tables[0];
                dgEmployeeSearch.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        protected void dgEmployeeSearch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int rowindex = e.NewEditIndex;
                //string s = grdDepartmentMaster..HeaderText.ToString().Trim();
                string sEmpCode = "";
                string sEmpName = "";
                Label Code = (Label)dgEmployeeSearch.Rows[rowindex].Cells[0].FindControl("lblEmployeeCode");
                txtEmployeeID.Text = Code.Text.ToString().Trim();

                Label Code1 = (Label)dgEmployeeSearch.Rows[rowindex].Cells[0].FindControl("lblEmployeeName");
                txtEmployeeName.Text = Code1.Text.ToString().Trim();

                Label Code2 = (Label)dgEmployeeSearch.Rows[rowindex].Cells[0].FindControl("lblDesign");
                txtDesignation.Text = Code2.Text.ToString().Trim();

                Label Code3 = (Label)dgEmployeeSearch.Rows[rowindex].Cells[0].FindControl("lblDepartment");
                txtDepartment.Text = Code3.Text.ToString().Trim();

                DataSet dsUser = new DataSet();
                ds = fncGetProjectDetails();
                dgReports.DataSource = ds.Tables[0];
                dgReports.DataBind();

                dsUser = fncGetPermissionDetails(txtEmployeeID.Text.ToString().Trim());

                for (int i = 0; i < dsUser.Tables[0].Rows.Count; i++)
                {
                    foreach (GridViewRow row in dgReports.Rows)
                    {
                        string sUserProject = dsUser.Tables[0].Rows[i][1].ToString().Trim();
                        string sUserModule = dsUser.Tables[0].Rows[i][2].ToString().Trim();
                        string sUserForms = dsUser.Tables[0].Rows[i][3].ToString().Trim();

                        string sProject = row.Cells[0].Text;
                        string sModule = row.Cells[1].Text;
                        string sForms = row.Cells[2].Text;
                        if (sUserProject == sProject && sUserModule == sModule && sUserForms == sForms)
                        // bool isSelected = (row.FindControl("chkSelect") as CheckBox).Checked;
                        {
                            (row.FindControl("chkSelect") as CheckBox).Checked = true;
                        }
                    }
                }


                tblSearch.Visible = false;
                tblUser.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }

        }
        protected void dgEmployeeSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                dgEmployeeSearch.PageIndex = e.NewPageIndex;
                subLoadSearch();
            }
            catch (Exception ex)
            {

            }
        }
        private void fncClear()
        {
            txtEmployeeID.Text = "";
            txtEmployeeName.Text = "";
            txtDepartment.Text = "";
            txtDesignation.Text = "";
            lblMessage.Text = "";
            dgReports.Visible = false;
        }

        public void fncGetEmployeePermission(string sEmpId)
        {
            try
            {
                ds = fncGetUserPermission(sEmpId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEmployeeName.Text = ds.Tables[0].Rows[0]["USER_NAME"].ToString().Trim();
                    txtDepartment.Text = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString().Trim();
                    txtDesignation.Text = ds.Tables[0].Rows[0]["DESIGNATION"].ToString().Trim();
                }
                else
                {
                    fncClear();
                    lblMessage.Text = "Employee Code is Wrong..";
                    return;
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (dgReports.Visible == false)
                    {
                        dgReports.Visible = true;
                    }
                    dgReports.DataSource = ds.Tables[1];
                    dgReports.DataBind();
                }
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    foreach (GridViewRow row in dgReports.Rows)
                    {
                        string sUserProject = ds.Tables[2].Rows[i][1].ToString().Trim();
                        string sUserModule = ds.Tables[2].Rows[i][2].ToString().Trim();
                        string sUserForms = ds.Tables[2].Rows[i][3].ToString().Trim();

                        string sProject = row.Cells[0].Text;
                        string sModule = row.Cells[1].Text;
                        string sForms = row.Cells[2].Text;
                        if (sUserProject == sProject && sUserModule == sModule && sUserForms == sForms)
                        // bool isSelected = (row.FindControl("chkSelect") as CheckBox).Checked;
                        {
                            (row.FindControl("chkSelect") as CheckBox).Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
        
        DataServerSync dtsyc = new DataServerSync();
        public void fncDeletePermission(string sEmpID)
        {
            try
            {
                bool b = false;
                sSql = "Delete from GPIL_USER_PERMISSION where Employee_ID ='" + sEmpID + "' ";
                //using(SqlConnection conn = new SqlConnection(dtsyc.Constring))
                //{
                //    SqlCommand cmd = new SqlCommand(sSql, conn);
                //    cmd.CommandTimeout = 0;
                //    conn.Open();
                //    cmd.ExecuteNonQuery();
                //}
                
                bool r = dtsyc.ExecuteNonQuery(sSql);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public void fncInsertPermission(string sEmpID, string proj, string modu, string forms)
        {
            try
            {

                sSql = "Insert into GPIL_USER_PERMISSION(Employee_ID,Project,Module,Form,Modified_By,Modified_On) values('";
                sSql += "" + sEmpID + "','" + proj + "','" + modu + "','" + forms + "','" + Session["UserID"].ToString().Trim() + "',GETDATE())";
                //SqlCommand cmd = new SqlCommand(sSql, ClsConnection.SqlCon);
                //cmd.CommandTimeout = 0;
                //cmd.ExecuteNonQuery();
                dtsyc.ExecuteNonQuery(sSql);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public DataSet fncEmployeeSearch(string sEmpCode, string sEmpName, string sPhone)
        {
            try
            {
                //DataSet ds = new DataSet();
                //string spname = "select Emp_No,Emp_Name,Phone from tbl_Emp_Master where Emp_No like '%" + sEmpCode + "%' and ";
                //spname = spname + " Emp_Name like '%" + sEmpName + "%' and Phone like '%" + sPhone + "%'";

                string sSql = "select USER_ID,USER_NAME,MOBILE_NO,DEPARTMENT,DESIGNATION ";
                sSql = sSql + " from GPIL_USER_MASTER(NOLOCK) where USER_ID like '%" + sEmpCode + "%' and ";
                sSql = sSql + " USER_NAME like '%" + sEmpName + "%' and MOBILE_NO like '%" + sPhone + "%'";

                //SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                //da.SelectCommand.CommandTimeout = 0;
                //da.Fill(ds);
                ds = new DataSet();
                ds = dtsyc.GetDataset(sSql);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            return ds;
        }

        public DataSet fncGetProjectDetails()
        {
            try
            {
                sSql = "select * from  GPIL_PROJECT_FORMS(NOLOCK) ";
                //spname = spname + " where a.Emp_No = b.CreateEmpCode and b.RGPNo = '" + sRGPNo + "'";
                //SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                //da.SelectCommand.CommandTimeout = 0;
                //da.Fill(ds);
                ds = new DataSet();
                ds= dtsyc.GetDataset(sSql);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            return ds;
        }

        public DataSet fncGetPermissionDetails(string sEmpID)
        {
            try
            {
                sSql = "select * from  GPIL_USER_PERMISSION(NOLOCK) where Employee_ID = '" + sEmpID + "'  ";
                //spname = spname + " where a.Emp_No = b.CreateEmpCode and b.RGPNo = '" + sRGPNo + "'";
                //SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                //da.SelectCommand.CommandTimeout = 0;
                //da.Fill(ds);
                ds = new DataSet();
                ds = dtsyc.GetDataset(sSql);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            return ds;
        }

        public DataSet fncGetUserPermission(string sEmpID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dtsyc.Constring))
                {
                    conn.Open();
                    sSql = "GPIL_SP_GET_USER_PERMISSION";
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@EmpId", sEmpID);
                    SqlCommand cmd = new SqlCommand(sSql, conn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(param);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);                
                    da.Fill(ds);
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            return ds;
        }
    }
}