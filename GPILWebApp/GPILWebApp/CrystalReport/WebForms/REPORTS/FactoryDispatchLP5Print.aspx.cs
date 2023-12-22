using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class FactoryDispatchLP5Print : System.Web.UI.Page
    {

        CrystalReportData crd = new CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlFromOrgnCode.DataSource = rptMgt.GetFrmOrnCode();
                    ddlFromOrgnCode.DataTextField = "OrgnName";
                    ddlFromOrgnCode.DataValueField = "OrgnCode";
                    ddlFromOrgnCode.DataBind();
                    ddlFromOrgnCode.Items.Insert(0, "< -- Select -- >");


                    ddlToOrgnCode.DataSource = rptMgt.GetToOrnCode();
                    ddlToOrgnCode.DataTextField = "OrgnName";
                    ddlToOrgnCode.DataValueField = "OrgnCode";
                    ddlToOrgnCode.DataBind();
                    ddlToOrgnCode.Items.Insert(0, "< -- Select -- >");

                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                txtFromDate.Text = dt.ToString("dd-MM-yyyy");
                
                string strErr = "";
                string sql;
                sql = "select SHIPMENT_NO From GPIL_SHIPMENT_HDR WITH(NOLOCK) where SENDER_ORGN_CODE='"+ddlFromOrgnCode.Text+"' AND RECEIVER_ORGN_CODE='"+ddlToOrgnCode.Text+"' AND CONVERT(NVARCHAR(10),SENDER_DATE,105)='"+txtFromDate.Text+"'";

                
                

                DataTable ds1 = new DataTable();
                ds1 = rptMgt.GetQueryResult(sql);

                if (ds1.Rows.Count == 0)
                {
                    //lblMessage.Text = strErr;
                }


                ddlShipmentNumber.DataSource = ds1;
                ddlShipmentNumber.DataTextField = "SHIPMENT_NO";
                ddlShipmentNumber.DataValueField = "SHIPMENT_NO";
                ddlShipmentNumber.DataBind();
                ddlShipmentNumber.Items.Insert(0, "<--Select-->");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}