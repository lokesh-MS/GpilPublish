using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ReportDocument CustomerReport = new ReportDocument();
               // RDLCReport rdlcReport = new RDLCReport();
                DataSet ds = new DataSet();
              //  ds = crd.GetORGN("FPSlip", HeaderID, farmerCode);
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptFarmerPurchaseSlip_New.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                //CustomerReport.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                CrystalReportViewer1.ReportSource = CustomerReport;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}