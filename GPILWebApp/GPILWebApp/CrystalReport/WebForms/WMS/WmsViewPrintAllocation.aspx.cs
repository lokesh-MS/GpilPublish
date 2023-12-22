using GPILWebApp.Controllers;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp.CrystalReport.WebForms.WMS
{
    public partial class WmsViewPrintAllocation : System.Web.UI.Page
    {
        WMSManagement wMgt = new WMSManagement();
        DataTable dt = new DataTable();
        string sQuery;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FuncVoidLoad();
            }
            else
            {
                
            }
        }


      //  WMSManagement wMgt = new WMSManagement();
        private void FuncVoidLoad()
        {
            if (clsSettings.strLocCode == clsSettings.sOngoleLoc)
            {
               sQuery = "Select LocCode, LocName from mLocations Where Active='True' And LocType<>'FACTORY' Order By LocCode";
            }
            else
            {
                sQuery = "Select LocCode, LocName from mLocations Where LocCode='" + clsSettings.strLocCode + "'";
            }
            //oDBActions.FillComboBox(ddlLocationCode, sQuery, false);
            //ddlLocationCode.SelectedIndex = ddlLocationCode.Items.IndexOf(ddlLocationCode.Items.FindByText(clsSettings.strLocCode));
            //BindGrid(clsSettings.strLocCode);

           dt= wMgt.GetQueryResult(sQuery);
            ddlLocationCode.DataSource = dt;
            ddlLocationCode.DataBind();
            ddlLocationCode.DataTextField = "LocCode";
            ddlLocationCode.DataValueField = "LocName";
            ddlLocationCode.DataBind();
            ddlLocationCode.Items.Insert(0, new ListItem("---Select---", "0"));
            BindGrid(clsSettings.strLocCode);

        }

        private void BindGrid(string strLocCode)
        {
            //DataTable dtBind = oDBActions.ReturnDataTable("Select ID, CropYearName, PMRunNo, GradeCode, CaseNoFrom, CaseNoTo, CasesToPrint, IsExistingStock From tPrintAllocation tpa Inner Join mCropYears mcy On tpa.CropYearCode = mcy.CropYearCode Where tpa.LocCode = '" + strLocCode + "' And D_Stts = 0 And Convert(int, CasesToPrint) > 0");
          string  query = "Select ID, CropYearName, PMRunNo, GradeCode, CaseNoFrom, CaseNoTo, CasesToPrint, IsExistingStock From tPrintAllocation tpa Inner Join mCropYears mcy On tpa.CropYearCode = mcy.CropYearCode Where tpa.LocCode = '" + strLocCode + "' And D_Stts = 0 And Convert(int, CasesToPrint) > 0";
            dt = wMgt.GetQueryResult(query);
            GridViewSample.AutoGenerateColumns = false;
            GridViewSample.DataSource = dt;
            GridViewSample.DataBind();
            GridViewSample.Visible = true;

        }

        protected void ddlLocationCode_TextChanged(object sender, EventArgs e)
        {
            lblLocationCode.Text = ddlLocationCode.SelectedValue.ToString();
            BindGrid(ddlLocationCode.SelectedItem.Text);
        }

        protected void GridViewSample_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMessage.Text = "";

            try
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                Label lblID = (Label)GridViewSample.Rows[RowIndex].FindControl("lblID");
                Label lblCropYearName = (Label)GridViewSample.Rows[RowIndex].FindControl("lblCropYearName");
                Label lblPMRunNo = (Label)GridViewSample.Rows[RowIndex].FindControl("lblPMRunNo");
                Label lblGradeCode = (Label)GridViewSample.Rows[RowIndex].FindControl("lblGradeCode");
                Label lblCaseNoFrom = (Label)GridViewSample.Rows[RowIndex].FindControl("lblCaseNoFrom");
                Label lblCaseNoTo = (Label)GridViewSample.Rows[RowIndex].FindControl("lblCaseNoTo");
                Label lblCasesToPrint = (Label)GridViewSample.Rows[RowIndex].FindControl("lblCasesToPrint");
                Label lblIsExistingStock = (Label)GridViewSample.Rows[RowIndex].FindControl("lblIsExistingStock");


                if (GridViewSample.Rows.Count <= 0)
                { return; }
                if (clsSettings.bAccessTran == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Access permission", "alert('You don’t have access permissions!" + Environment.NewLine + "Contact Administrator');", true);
                    //MessageBox.Show("You don’t have access permissions!" + Environment.NewLine + "Contact Administrator.", "Access permission", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }

                clsSettings.sPrintID = lblID.Text; //dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[0].Text.ToString();
                clsSettings.sPrintLocCode = ddlLocationCode.SelectedItem.Text;
                clsSettings.sPrintCropYr = lblCropYearName.Text; //dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[1].Text.ToString();
                clsSettings.sPrintLotNo = lblPMRunNo.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[2].Text.ToString();
                clsSettings.sPrintGradeCode = lblGradeCode.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[3].Text.ToString();
                clsSettings.sPrintFromCaseNo = lblCaseNoFrom.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[4].Text.ToString();
                clsSettings.sPrintToCaseNo = lblCaseNoTo.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[5].Text.ToString();
                clsSettings.sPrintCasesCount = lblCasesToPrint.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[6].Text.ToString();
                clsSettings.sPrintExistingStock = lblIsExistingStock.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[7].Text.ToString();
                clsSettings.sDateOfPacking = txtDateOfPacking.Text;
                if (clsSettings.sPrintExistingStock.Trim().ToUpper() == "FALSE")
                {
                    Response.Redirect("Form_WMS_BarcodePrinting.aspx");
                }
                else if (clsSettings.sPrintExistingStock.Trim().ToUpper() == "TRUE")
                {
                    Response.Redirect("Form_WMS_BarCodeReprint.aspx");
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        //protected void dgvPrinting_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    lblMessage.Text = "";

        //    try
        //    {
        //        GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //        int RowIndex = gvr.RowIndex;

        //        Label lblID = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblID");
        //        Label lblCropYearName = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblCropYearName");
        //        Label lblPMRunNo = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblPMRunNo");
        //        Label lblGradeCode = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblGradeCode");
        //        Label lblCaseNoFrom = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblCaseNoFrom");
        //        Label lblCaseNoTo = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblCaseNoTo");
        //        Label lblCasesToPrint = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblCasesToPrint");
        //        Label lblIsExistingStock = (Label)dgvPrinting.Rows[RowIndex].FindControl("lblIsExistingStock");


        //        if (dgvPrinting.Rows.Count <= 0)
        //        { return; }
        //        if (clsSettings.bAccessTran == false)
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Access permission", "alert('You don’t have access permissions!" + Environment.NewLine + "Contact Administrator');", true);
        //            //MessageBox.Show("You don’t have access permissions!" + Environment.NewLine + "Contact Administrator.", "Access permission", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
        //            return;
        //        }

        //        clsSettings.sPrintID = lblID.Text; //dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[0].Text.ToString();
        //        clsSettings.sPrintLocCode = cboLocCode.SelectedItem.Text;
        //        clsSettings.sPrintCropYr = lblCropYearName.Text; //dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[1].Text.ToString();
        //        clsSettings.sPrintLotNo = lblPMRunNo.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[2].Text.ToString();
        //        clsSettings.sPrintGradeCode = lblGradeCode.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[3].Text.ToString();
        //        clsSettings.sPrintFromCaseNo = lblCaseNoFrom.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[4].Text.ToString();
        //        clsSettings.sPrintToCaseNo = lblCaseNoTo.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[5].Text.ToString();
        //        clsSettings.sPrintCasesCount = lblCasesToPrint.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[6].Text.ToString();
        //        clsSettings.sPrintExistingStock = lblIsExistingStock.Text;//dgvPrinting.Rows[dgvPrinting.SelectedRow.RowIndex].Cells[7].Text.ToString();
        //        clsSettings.sDateOfPacking = dtpDateOfPacking.Text;
        //        if (clsSettings.sPrintExistingStock.Trim().ToUpper() == "FALSE")
        //        {
        //            Response.Redirect("Form_WMS_BarcodePrinting.aspx");
        //        }
        //        else if (clsSettings.sPrintExistingStock.Trim().ToUpper() == "TRUE")
        //        {
        //            Response.Redirect("Form_WMS_BarCodeReprint.aspx");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //    }

        //}





    }
}