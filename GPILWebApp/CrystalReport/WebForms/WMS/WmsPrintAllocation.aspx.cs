//using DocumentFormat.OpenXml.Spreadsheet;
using GPILWebApp.Controllers;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.WMS
{
    public partial class WmsPrintAllocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    FuncVoidLoad();

                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }


        }

        WMSManagement WMgt = new WMSManagement();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        public void FuncVoidLoad()
        {
            try
            {
               string sSqlQuery = "Select CropYearCode, CropYearName from mCropYears Order By CropYearCode";
                dt = WMgt.GetQueryResult(sSqlQuery);

                ddlRunningYear.DataSource = dt;
                ddlRunningYear.DataBind();
                ddlRunningYear.DataValueField = "CropYearName";
                ddlRunningYear.DataTextField = "CropYearCode";
                ddlRunningYear.DataBind();
                ddlRunningYear.Items.Insert(0, new ListItem("---Select---", "0"));
                if (ddlRunningYear.Items.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Barcode Printing", "alert('Crop year code not found!');", true);
                    return;
                }
                else
                {
                    string strsql = "Select Distinct CropYearCode from tPrintAllocation Order By CropYearCode";
                    //DataTable dt = oDBActions.ReturnDataTable("Select Distinct CropYearCode from tPrintAllocation Order By CropYearCode");
                    dt = WMgt.GetQueryResult(strsql);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRunningYear.ClearSelection(); //making sure the previous selection has been cleared
                        string strSelectedValue = dt.Rows[dt.Rows.Count - 1][0].ToString();
                        ddlRunningYear.SelectedIndex = ddlRunningYear.Items.IndexOf(ddlRunningYear.Items.FindByText(strSelectedValue));

                    }
                    else
                    {
                        ddlRunningYear.SelectedIndex = -1;
                    }

                    lblRunYear.Text = ddlRunningYear.SelectedItem.Value.ToString();
                    pc.ProbertyStrLastCropYear = ddlRunningYear.SelectedItem.Text.ToString();
                    //Session["StrLastCropYear"].ToString() = ddlRunningYear.SelectedItem.Text.ToString();
                }
                // sSqlQuery = "Select LocCode, LocName+'|'+LocType from mLocations Order By LocCode";
                sSqlQuery = "select LocCode,LocName from mLocations ORDER BY  LocCode";
                dt = WMgt.GetQueryResult(sSqlQuery);
                ddlLocationCode.DataSource = dt;
                ddlLocationCode.DataBind();
              //  ddlLocationCode.DataValueField = "LocName";
                ddlLocationCode.DataTextField = "LocCode";
                ddlLocationCode.DataValueField = "LocName";
                ddlLocationCode.DataBind();
                ddlLocationCode.Items.Insert(0, new ListItem("---Select---", "0"));
                sSqlQuery = "SELECT GradeCode, GradeName FROM mGrades ORDER BY GradeCode";
                
                dt = WMgt.GetQueryResult(sSqlQuery);
                ddlGradeCode.DataSource = dt;
                ddlGradeCode.DataBind();
                ddlGradeCode.DataValueField = "GradeName";
                ddlGradeCode.DataTextField = "GradeCode";
                ddlGradeCode.DataBind();
                ddlGradeCode.Items.Insert(0, new ListItem("---Select---", "0"));

            }
            catch (SqlException ex)
            {
                ClsConnection.SqlCon.Close();
                
            }
            catch (Exception ex)
            {
                ClsConnection.SqlCon.Close();
                
            }

        }
        Propertycls pc = new Propertycls();

        protected void ddlRunningYear_TextChanged(object sender, EventArgs e)
        {
            string str1 = ddlRunningYear.SelectedItem.Text;
            string str2 = ddlRunningYear.SelectedValue.ToString();

            //if (Propertycls.ProbertyStrLastCropYear != "" && Propertycls.ProbertyStrLastCropYear != ddlRunningYear.SelectedItem.Text)
         
            if (pc.ProbertyStrLastCropYear != "" && pc.ProbertyStrLastCropYear != ddlRunningYear.SelectedItem.Text)
            {
                
                if (true)
                {
                    lblRunYear.Text = ddlRunningYear.SelectedValue.ToString();
                    DefaultSetting(true);
                    ddlGradeCode.SelectedIndex = 0;
                   
                    MaxPMRunNo(ddlRunningYear.SelectedItem.Text); // This procedure gives Max PMRunNo
                    pc.ProbertyStrLastCropYear = ddlRunningYear.SelectedItem.Text;
                }
                else
                {
                    ddlRunningYear.SelectedItem.Text = pc.ProbertyStrLastCropYear;
                }
            }
        }
        private void MaxPMRunNo(string sCropYearCode)
        {
            string sSqlQuery = "SELECT top 1 MAX(PMRunNo) PMRunNo,PlannedOn FROM tPrintAllocation WHERE CropYearCode ='" + sCropYearCode + "'And GradeCode='" + ddlGradeCode.SelectedItem.Text + "' Group By PlannedOn order by PlannedOn desc";
            dt = WMgt.GetQueryResult(sSqlQuery);
           // DataTable dtPMRunNo = oDBActions.ReturnDataTable("SELECT top 1 MAX(PMRunNo) PMRunNo,PlannedOn FROM tPrintAllocation WHERE CropYearCode ='" + sCropYearCode + "'And GradeCode='" + ddlGradeCode.SelectedItem.Text + "' Group By PlannedOn order by PlannedOn desc");
            if (dt.Rows.Count == 0)
            {
                txtRunNo.Text = "001";
            }
            else
            {
                txtRunNo.Text = dt.Rows[0]["PMRunNo"].ToString();
                if (Convert.ToDateTime(dt.Rows[0]["PlannedOn"].ToString()) < clsSettings.dtSysDate)
                { ShowPMRunNo(); }
            }
        }
        private void ShowPMRunNo()
        {
            int iMaxPMRunNo = Convert.ToInt32(txtRunNo.Text) + 1;
            if (iMaxPMRunNo.ToString().Length > 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('Run No. cann't be exceeds from 999');", true);
                return;
            }
            else
            { txtRunNo.Text = GetPMRunNo(iMaxPMRunNo); }

        }
        private string GetPMRunNo(int iPMRunNo)
        {
            string strPMRunNo = null;
            if (iPMRunNo.ToString().Length < 3)
            {
                if (iPMRunNo <= 9)
                {
                    strPMRunNo = "00" + iPMRunNo;
                }
                else if (iPMRunNo > 9 && iPMRunNo <= 99)
                {
                    strPMRunNo = "0" + iPMRunNo;
                }
                else
                {
                    strPMRunNo = iPMRunNo.ToString();
                }
            }
            else
            {
                strPMRunNo = iPMRunNo.ToString();
            }
            return strPMRunNo;
        }

        protected void ddlGradeCode_TextChanged(object sender, EventArgs e)
        {
            if (ddlGradeCode.SelectedIndex > 0)
            {
                lblGradeName.Text = ddlGradeCode.SelectedValue.ToString();
                MaxPMRunNo(ddlRunningYear.SelectedItem.Text); // This procedure gives Max PMRunNo
                MaxCaseNo(ddlRunningYear.SelectedItem.Text);//This procedure gives new CaseNo after getting maximum PMRunNo.
                GetMaxAccuCaseNo();// This procedure gives Max Accumulative CaseNo
            }
            else
            {
                lblGradeName.Text = "";
                DefaultSetting(true);
            }
        }

        protected void ddlLocationCode_TextChanged(object sender, EventArgs e)
        {
            lblLocationName.Text = ddlLocationCode.SelectedValue.ToString().Split('|')[0];
        }

        protected void txtNoOfCasesPrint_TextChanged(object sender, EventArgs e)
        {
            CaseNumberCrossCheck();
        }
        public void CaseNumberCrossCheck()
        {
            int iCaseSrNo = 0;
            if (ddlGradeCode.SelectedIndex == 0)
            {
                txtNoOfCasesPrint.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('Select grade code !');", true);
                ddlGradeCode.Focus();
                txtNoOfCasesPrint.Text = "";
                txtTo.Text = "";
                return;
            }
            else if (txtNoOfCasesPrint.Text.Trim() == "")
            {
                txtTo.Text = ""; return;
            }
            else
            {
                iCaseSrNo = Convert.ToInt32(txtNoOfCasesPrint.Text.Trim());
                if (iCaseSrNo == 0)
                {
                    txtTo.Text = ""; return;
                }
                else
                {
                    iCaseSrNo = Convert.ToInt32(txtFrom.Text) + iCaseSrNo - 1;
                }
            }
            if (iCaseSrNo > 99999)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('Case Sr.No. cannot be greater than 99999');", true);
                txtNoOfCasesPrint.Text = pc.ProbertyStrLastCropYear;
                
            }
            else
            {
                txtTo.Text = CaseSrNo(iCaseSrNo);
                txtTo1.Text = txtTo.Text;
            }
        }
        protected void btnAllocate_Click(object sender, EventArgs e)
        {
            clsSettings.strUserName = "gpi";

             if (true)
            {
                try
                {

                    if (ddlGradeCode.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('Select grade code !');", true);
                        ddlGradeCode.Focus();
                        return;
                    }
                    else if (txtTo.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('Enter no of cases to be print !');", true);
                        txtNoOfCasesPrint.Focus();
                        return;
                    }
                    else
                    {
                        string oMaxID = "Select isnull(MAX(ID),0)+1 From tPrintAllocation";
                        dt = WMgt.GetQueryResult(oMaxID);
                        int dt2 = int.Parse(dt.Rows[0][0].ToString());

                       // string dt2 = Convert.ToString(dt.Rows[0][0].ToString());
                        string oMaxCaseNo = "Select isnull(MAX(MaxCaseNo),0)+1 From tPrintAllocation Where CropYearCode='" + ddlRunningYear.SelectedItem.Text + "' And GradeCode='" + ddlGradeCode.SelectedItem.Text + "'";
                        dt1 = WMgt.GetQueryResult(oMaxCaseNo);
                        int dt3 = int.Parse(dt1.Rows[0][0].ToString());
                        

                        //SqlCommand cmdIns = new SqlCommand("sp_tPrintAllocation_Ins", ClsConnection.SqlCon);
                        //cmdIns.CommandType = CommandType.StoredProcedure;
                        //cmdIns.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(dt2);
                        //cmdIns.Parameters.Add("@CropYearCode", SqlDbType.Char, 1).Value = ddlRunningYear.SelectedItem.Text;
                        //cmdIns.Parameters.Add("@PMRunNo", SqlDbType.Char, 3).Value = txtRunNo.Text;
                        //cmdIns.Parameters.Add("@LocCode", SqlDbType.VarChar, 3).Value = ddlLocationCode.SelectedItem.Text;
                        //cmdIns.Parameters.Add("@GradeCode", SqlDbType.Char, 4).Value = ddlGradeCode.SelectedItem.Text;
                        //cmdIns.Parameters.Add("@CaseNoFrom", SqlDbType.Char, 5).Value = txtFrom.Text.Trim();
                        //cmdIns.Parameters.Add("@CaseNoTo", SqlDbType.Char, 5).Value = txtTo.Text.Trim();
                        //cmdIns.Parameters.Add("@CasesToPrint", SqlDbType.VarChar, 5).Value = txtNoOfCasesPrint.Text.Trim();
                        //cmdIns.Parameters.Add("@MaxCaseNo", SqlDbType.Char, 5).Value = string.Format("{0:00000}", Convert.ToInt32(dt3) + (Convert.ToInt32(txtTo.Text.Trim()) - Convert.ToInt32(txtFrom.Text.Trim())));
                        //cmdIns.Parameters.Add("@D_Stts", SqlDbType.TinyInt).Value = 0;
                        //cmdIns.Parameters.Add("@PlannedBy", SqlDbType.VarChar, 15).Value = clsSettings.strUserName;
                        //cmdIns.Parameters.Add("@IsExistingStock", SqlDbType.Bit).Value = 0;
                        //ClsConnection.SqlCon.Open();
                        //cmdIns.ExecuteNonQuery();
                        //ClsConnection.SqlCon.Close();




                        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
                        con.Open();

                        

                        SqlCommand cmdIns = new SqlCommand("sp_tPrintAllocation_Ins", con);
                             cmdIns.CommandType = CommandType.StoredProcedure;
                                cmdIns.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(dt2);
                                cmdIns.Parameters.Add("@CropYearCode", SqlDbType.Char, 1).Value = ddlRunningYear.SelectedItem.Text;
                                cmdIns.Parameters.Add("@PMRunNo", SqlDbType.Char, 3).Value = txtRunNo.Text;
                                cmdIns.Parameters.Add("@LocCode", SqlDbType.VarChar, 3).Value = ddlLocationCode.SelectedItem.Text;
                                cmdIns.Parameters.Add("@GradeCode", SqlDbType.Char, 4).Value = ddlGradeCode.SelectedItem.Text;
                                cmdIns.Parameters.Add("@CaseNoFrom", SqlDbType.Char, 5).Value = txtFrom.Text.Trim();
                                cmdIns.Parameters.Add("@CaseNoTo", SqlDbType.Char, 5).Value = txtTo.Text.Trim();
                                cmdIns.Parameters.Add("@CasesToPrint", SqlDbType.VarChar, 5).Value = txtNoOfCasesPrint.Text.Trim();
                                cmdIns.Parameters.Add("@MaxCaseNo", SqlDbType.Char, 5).Value = string.Format("{0:00000}", Convert.ToInt32(dt3) + (Convert.ToInt32(txtTo.Text.Trim()) - Convert.ToInt32(txtFrom.Text.Trim())));
                                cmdIns.Parameters.Add("@D_Stts", SqlDbType.TinyInt).Value = 0;
                                cmdIns.Parameters.Add("@PlannedBy", SqlDbType.VarChar, 15).Value = clsSettings.strUserName;
                                cmdIns.Parameters.Add("@IsExistingStock", SqlDbType.Bit).Value = 0;

                        //SqlParameter sqlParam = new SqlParameter("@Result", DbType.Boolean);
                        
                        //sqlParam.Direction = ParameterDirection.Output;
                        //cmdIns.Parameters.Add(sqlParam);
                        cmdIns.ExecuteNonQuery();
                        con.Close();
                        //Response.Write(cmdIns.Parameters["@Result"].Value);
                        

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('Record successfully saved!');", true);
                        DefaultSetting(false);
                       
                        MaxPMRunNo(ddlRunningYear.SelectedItem.Text); // This procedure gives Max PMRunNo
                        MaxCaseNo(ddlRunningYear.SelectedItem.Text); // This procedure gives Max CaseNo
                        GetMaxAccuCaseNo();// This procedure gives Max Accumulative CaseNo
                        ddlLocationCode.Focus();
                    }
                }
                catch (SqlException ex)
                {
                    ClsConnection.SqlCon.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation - Error", "alert('" + ex.Message.ToString() + "');", true);
                    
                }
                catch (Exception ex)
                {
                    ClsConnection.SqlCon.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation - Error", "alert('" + ex.Message.ToString() + "');", true);
                    
                }
            }
        }

        private void GetMaxAccuCaseNo()
        {
            //oMaxCaseNo = oDBActions.ExecuteScalar("Select isnull(MAX(MaxCaseNo),0)+1 From tPrintAllocation Where CropYearCode='" + ddlRunningYear.SelectedItem.Text + "' And GradeCode='" + ddlGradeCode.SelectedItem.Text + "'");
            string oMaxCaseNo = "Select isnull(MAX(MaxCaseNo),0)+1 From tPrintAllocation Where CropYearCode='" + ddlRunningYear.SelectedItem.Text + "' And GradeCode='" + ddlGradeCode.SelectedItem.Text + "'";
             dt = WMgt.GetQueryResult(oMaxCaseNo);
            int oMcse = int.Parse(dt.Rows[0][0].ToString());
            lblTStartCaseNo.Text = "Starting Accumulative Case No. :" + string.Format("{0:00000}", Convert.ToInt32(oMcse));
        }
        protected void btnNewRun_Click(object sender, EventArgs e)
        {
            if (true)
            {
                DefaultSetting(false);
                ShowPMRunNo();
                if (ddlGradeCode.SelectedIndex > 0)
                {
                    MaxCaseNo(ddlRunningYear.SelectedItem.Text);
                }
            }
        }
       
        private void MaxCaseNo(string sCropYearCode)
        {
            string  oCaseNo = "SELECT isnull(MAX(CaseNoTo),0) CaseNoTo FROM tPrintAllocation WHERE CropYearCode ='" + sCropYearCode + "' And PMRunNo ='" + txtRunNo.Text.Trim() + "' And GradeCode='" + ddlGradeCode.SelectedItem.Text + "'";
            dt = WMgt.GetQueryResult(oCaseNo);
            //object oCaseNo = oDBActions.ExecuteScalar("SELECT isnull(MAX(CaseNoTo),0) CaseNoTo FROM tPrintAllocation WHERE CropYearCode ='" + sCropYearCode + "' And PMRunNo ='" + txtPMRunNo.Text.Trim() + "' And GradeCode='" + ddlGradeCode.SelectedItem.Text + "'");
            // if (Convert.ToInt32(dt) != 99999)
            //if (dt.Rows.Count > 0 ){

                int CseNo = int.Parse(dt.Rows[0][0].ToString());
            //}
           
            
            if (Convert.ToInt32(CseNo) != 99999)
            {
                txtFrom.Text = CaseSrNo(Convert.ToInt32(CseNo) + 1);
            }
            else
            {
                string sPMRunNo = txtRunNo.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Printing Allocation", "alert('" + sPMRunNo + " PM Run No. has been full. PM Run No. needs to be change !');", true);
                DefaultSetting(true);
                txtRunNo.Text = sPMRunNo;
                ShowPMRunNo();
                txtFrom.Text = "00001";
                return;
            }

        }
        private void DefaultSetting(bool b)
        {
            if (b) txtRunNo.Text = "";
            txtFrom.Text = "";
           
            txtTo.Text = "";
            txtNoOfCasesPrint.Text = "";
            lblTStartCaseNo.Text = "";
        }
        private string CaseSrNo(int iCaseSrNo)
        {
            string strCaseSrNo = null;
            if (iCaseSrNo.ToString().Length < 5)
            {
                if (iCaseSrNo <= 9)
                {
                    strCaseSrNo = "0000" + iCaseSrNo;
                }
                else if (iCaseSrNo > 9 && iCaseSrNo <= 99)
                {
                    strCaseSrNo = "000" + iCaseSrNo;
                }
                else if (iCaseSrNo > 99 && iCaseSrNo <= 999)
                {
                    strCaseSrNo = "00" + iCaseSrNo;
                }
                else if (iCaseSrNo > 999)
                {
                    strCaseSrNo = "0" + iCaseSrNo;
                }
                else
                {
                    strCaseSrNo = iCaseSrNo.ToString();
                }
            }
            else
            {
                strCaseSrNo = iCaseSrNo.ToString();
            }
            return strCaseSrNo;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlGradeCode.SelectedIndex = -1;
            ddlLocationCode.SelectedIndex = -1;
            lblGradeName.Text = "";
            lblLocationName.Text = "";
            lblTStartCaseNo.Text = "";

            txtRunNo.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            txtNoOfCasesPrint.Text = "";
            txtTo1.Text = "";
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Response.RedirectToRoute( new { controller = "Home", action = "Index" });
            Response.Redirect("WmsPrintAllocation.aspx");
        }

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("WmsPrintAllocation.aspx");

        //    Response.RedirectToRoute("Index","Home");
        //}
    }
}