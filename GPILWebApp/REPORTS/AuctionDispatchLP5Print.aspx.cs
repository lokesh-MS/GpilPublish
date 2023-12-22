﻿using GPI;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPI;
namespace GPILWebApp.REPORTS
{
    public partial class AuctionDispatchLP5Print : System.Web.UI.Page
    {
        CommonManagement cMgt = new CommonManagement();

        CrystalReportData crd = new ViewModel.CrystalReportData();
        ReportManagement rptMgt = new ReportManagement();
        string sql;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlFromOrgnCode.DataSource = rptMgt.GetOrnCode();
                    ddlFromOrgnCode.DataTextField = "OrgnName";
                    ddlFromOrgnCode.DataValueField = "OrgnCode";
                    ddlFromOrgnCode.DataBind();
                    ddlFromOrgnCode.Items.Insert(0, "< -- Select -- >");


                    ddlToOrgnCode.DataSource = rptMgt.GetOrnCode();
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

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }


        protected void ddlToOrgnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlShipmentNumber.DataSource = rptMgt.GetShipmentNumber(ddlFromOrgnCode.SelectedItem.Value, ddlToOrgnCode.SelectedItem.Value, txtDate.Text);
                ddlShipmentNumber.DataTextField = "SHIPMENT_NO";
                ddlShipmentNumber.DataValueField = "SHIPMENT_NO";
                ddlShipmentNumber.DataBind();
                ddlShipmentNumber.Items.Insert(0, "< -- Select -- >");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }
    }
}