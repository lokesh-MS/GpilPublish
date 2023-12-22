using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPILWebApp.ViewModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace GPILWebApp
{
    public partial class WeightLossReport : System.Web.UI.Page
    {
        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please check the drop down bind method');", true);
            }

            BindReport();
        }




        public void BindReport()
        {
            DataTable dt = new DataTable();
            try
            {
                string strAttributes = "";//
                string strCondition = "";//

                if (ddlCrop.SelectedIndex == 0)
                {
                    if (ddlVariety.SelectedIndex == 0)
                    {
                        strAttributes = strAttributes + " 'ALL' AS CROP,'ALL' AS VARIETY, ";
                    }
                    else
                    {
                        strAttributes = strAttributes + " 'ALL' AS CROP,'" + ddlVariety.Text + "' AS VARIETY, ";
                        strCondition = strCondition + " AND D.GPIL_BALE_NUMBER LIKE '__" + ddlVariety.Text + "%' ";
                    }
                }
                else
                {
                    if (ddlVariety.SelectedIndex == 0)
                    {
                        strAttributes = strAttributes + " '" + ddlCrop.Text + "' AS CROP,'ALL' AS VARIETY, ";
                        strCondition = strCondition + " AND D.GPIL_BALE_NUMBER LIKE '" + ddlCrop.Text + "%' ";
                    }
                    else
                    {
                        strAttributes = strAttributes + " '" + ddlCrop.Text + "' AS CROP,'" + ddlVariety.Text + "' AS VARIETY, ";
                        strCondition = strCondition + " AND D.GPIL_BALE_NUMBER LIKE '" + ddlCrop.Text + ddlVariety.Text + "%' ";
                    }
                }


                if (txtFromDate.Text.Trim().Length != 0 && txtToDate.Text.Trim().Length != 0 && txtFromDate.Text != "<--Select-->" && txtToDate.Text != "<--Select-->")
                {
                    strAttributes = strAttributes + "'" + txtFromDate.Text + "' AS FROM_DATE,'" + txtToDate.Text + "' AS TO_DATE,";
                    strCondition = strCondition + " AND RECEIVED_DATE BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text + " 00:00:00 AM',105) AND CONVERT(DATETIME,'" + txtToDate.Text + " 23:59:59 PM',105) ";
                }
                else
                {
                    strAttributes = strAttributes + "'INITIAL DAY' AS FROM_DATE,'AS OF NOW' AS TO_DATE,";
                    strCondition = strCondition + "";
                }

                if (ddlOrgnType.SelectedIndex == 0)
                {
                    strAttributes = strAttributes + "'ALL' AS SENDER_ORGN_TYPE,";
                    strCondition = strCondition + "";
                }
                else
                {
                    strAttributes = strAttributes + "'" + ddlOrgnType.Text + "' AS SENDER_ORGN_TYPE,";
                    strCondition = strCondition + " AND O.ORGN_TYPE='" + ddlOrgnType.Text + "' ";
                }


                if (ddlOrgnCode.SelectedIndex == 0)
                {
                    strAttributes = strAttributes + "'ALL OVER ORGN' AS RECEIVER_ORGN,";
                }
                else
                {
                    strAttributes = strAttributes + "'" + ddlOrgnCode.Text + "' AS RECEIVER_ORGN,";
                    strCondition = strCondition + " AND H.RECEIVER_ORGN_CODE='" + ddlOrgnCode.Text + "' ";
                }



                dt = rptMgt.BindWeightLossReport(strAttributes, strCondition);



                ReportDocument WeightLossRpt = new ReportDocument();

                WeightLossRpt.Load(Server.MapPath("~/CrystalReport/RptWeightLossReport.rpt"));
                WeightLossRpt.SetDataSource(dt.DefaultView);
                rdlcWeightLossReport.ReportSource = WeightLossRpt;
                rdlcWeightLossReport.DataBind();
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
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGNCodeNTF", "", "");
                ddlOrgnCode.DataSource = ds.Tables[0];
                ddlOrgnCode.DataTextField = "ORGN_CODE1";
                ddlOrgnCode.DataValueField = "ORGN_CODE";
                ddlOrgnCode.DataBind();
                ddlOrgnCode.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                ddlCrop.DataSource = ds1.Tables[0];
                ddlCrop.DataTextField = "CROPYEAR";
                ddlCrop.DataValueField = "CROP";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                ddlVariety.DataSource = ds2.Tables[0];
                ddlCrop.DataTextField = "VARIETYNAME"; 
                ddlCrop.DataValueField = "VARIETY";
                ddlCrop.DataBind();
                ddlCrop.Items.Insert(0, new ListItem("- Select -", "0"));

                DataSet ds3 = new DataSet();
                ds3 = crd.GetORGN("ORGNType", "", "");
                ddlOrgnType.DataSource = ds3.Tables[0];
                ddlOrgnType.DataTextField = "ORGN_TYPE";
                ddlOrgnType.DataValueField = "ORGN_TYPE";
                ddlOrgnType.DataBind();
                ddlOrgnType.Items.Insert(0, new ListItem("- Select -", "0"));

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }
    }
}