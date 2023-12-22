using GPILWebApp.Controllers;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class FactoryDispatch : System.Web.UI.Page
    {
        ReportManagement rptMgt = new ReportManagement();
        DataSet exceldata = new DataSet();
        public static DataTable dtclstr = new DataTable();
        public static DataTable orgdata = new DataTable();
        public static DataSet purdata = new DataSet();
        public static string errordata = string.Empty;
        double mrkdwt;
        string processts;
        public string filename;
        string retVal = string.Empty;
        SqlCommand cmd;
        SqlDataReader strrs;
        SqlCommand cmd1;
        SqlDataReader strrs1;
        public static string errfile;

        string sendorg;
        string temphdr;
        SqlTransaction objSqlTrx;


        string strsql;


        string strSqlQuery;
        protected void Page_Init(object sender, EventArgs e)
        {

            //test = new testDataContext();
            //ddlLocationCode.DataSource = rptMgt.GetOrnCode1();
            //ddlLocationCode.DataTextField = "ORGN_CODE1";
            //ddlLocationCode.DataValueField = "ORGN_CODE";
            //ddlLocationCode.DataBind();
            //ddlLocationCode.Items.Insert(0, "< -- Select -- >");
            ddlLocationCode.DataSource = rptMgt.GetOrnCode1();
            ddlLocationCode.DataTextField = "OrgnName";
            ddlLocationCode.DataValueField = "OrgnCode";
            ddlLocationCode.DataBind();
            ddlLocationCode.Items.Insert(0, "< -- Select -- >");

            ClsConnection.connectDB();
            string strTransporterQuery = "select TransporterCode, (TransporterCode + ' - ' +  TransporterName ) as TransporterName from mTransporters(nolock)";
            SqlCommand objSqlCommand = new SqlCommand(strTransporterQuery, ClsConnection.SqlCon);
            objSqlCommand.CommandTimeout = 0;
            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
            DataTable objDataTable = new DataTable();
            objSqlDataAdapter.Fill(objDataTable);
            ClsConnection.closeDB();

            ddlTransporterCode.DataSource = objDataTable;
            ddlTransporterCode.DataTextField = "TransporterName";
            ddlTransporterCode.DataValueField = "TransporterCode";
            ddlTransporterCode.DataBind();
            ddlTransporterCode.Items.Insert(0, "< -- Select -- >");


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.Width = Unit.Percentage(100);
           

        }
        protected void gvbind()
        {
            ClsConnection.connectDB();

            strSqlQuery = "SELECT D.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENDER_DATE,H.RECEIVED_DATE,COUNT(D.GPIL_BALE_NUMBER) AS CASES,SUM(MARKED_WT) AS QUANTITY FROM GPIL_SHIPMENT_DTLS(nolock) D, GPIL_SHIPMENT_HDR(nolock) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEIVER_ORGN_CODE='" + ddlLocationCode.Text + "' AND H.STATUS='INT' AND H.IS_WMS_SHIPMENT='F' AND  H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txt_Report_Date.Text + " 23:59:59',102) AND H.SENDER_TRUCK_NO='" + ddlTruckNumber.Text + "' AND H.TRANSPORT_NAME='" + ddlTransporterCode.Text + "' GROUP BY D.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENDER_DATE,H.RECEIVED_DATE ORDER BY D.SHIPMENT_NO";

            SqlCommand cmd = new SqlCommand(strSqlQuery, ClsConnection.SqlCon);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ClsConnection.closeDB();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "No Records Found";
            }

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txt_Report_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the LR Date');", true);
                return;
            }

            if (ddlLocationCode.SelectedIndex == 0 || ddlLocationCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Location code for Factory Dispatch');", true);
                return;
            }

            if (ddlTransporterCode.SelectedIndex == 0 || ddlTransporterCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Transporter Code for Factory Dispatch');", true);
                return;
            }

            if (ddlTruckNumber.SelectedIndex == 0 || ddlTruckNumber.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Truck Number for Factory Dispatch');", true);
                return;
            }


            gvbind();
        }

        protected void btnViewNew_Click(object sender, EventArgs e)
        {
            if (txt_Report_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the LR Date');", true);
                return;
            }

            if (ddlLocationCode.SelectedIndex == 0 || ddlLocationCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Location code for Factory Dispatch');", true);
                return;
            }

            if (ddlTransporterCode.SelectedIndex == 0 || ddlTransporterCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Transporter Code for Factory Dispatch');", true);
                return;
            }

            if (ddlTruckNumber.SelectedIndex == 0 || ddlTruckNumber.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Truck Number for Factory Dispatch');", true);
                return;
            }


            gvbind();
        }

        protected void btnFumigate_Click(object sender, EventArgs e)
        {
            if (txt_Report_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the LR Date');", true);
                return;
            }

            if (ddlLocationCode.SelectedIndex == 0 || ddlLocationCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Location code for Factory Dispatch');", true);
                return;
            }

            if (ddlTransporterCode.SelectedIndex == 0 || ddlTransporterCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Transporter Code for Factory Dispatch');", true);
                return;
            }

            if (ddlTruckNumber.SelectedIndex == 0 || ddlTruckNumber.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Truck Number for Factory Dispatch');", true);
                return;
            }



            string strHeadeIDs = "";
            object oFreightAmount = "0";
            string temp_ref = "";
            try
            {

                ClsConnection.connectDB();

                objSqlTrx = ClsConnection.SqlCon.BeginTransaction();

                int cnn = GridView1.Rows.Count;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (CheckBox)row.FindControl("chkDispatch");
                        if (chkRow.Checked)
                        {
                            string strHeaderID = row.Cells[1].Text;
                            sendorg = row.Cells[2].Text;
                            //////////----------- ERP OR WMS ENTRY---------------------///////

                            if (strHeadeIDs == "")
                            {
                                strHeadeIDs = "'" + strHeaderID + "'";
                            }
                            else
                            {
                                strHeadeIDs = strHeadeIDs + "," + "'" + strHeaderID + "'";
                            }


                            string sqltemp_refno= Convert.ToString(ExecuteQueryScalarWithTransaction("select temp_ref from gpil_shipment_hdr(nolock) where shipment_no = '" + strHeaderID + "'", ClsConnection.SqlCon, objSqlTrx));


                            string sqlpicklistno = Convert.ToString(ExecuteQueryScalarWithTransaction("select picklist_no from gpil_shipment_hdr_temp(nolock) where shipment_no = '" + sqltemp_refno + "'", ClsConnection.SqlCon, objSqlTrx));
                            string sqllp5no = Convert.ToString(ExecuteQueryScalarWithTransaction("select LP5_No from gpil_shipment_hdr_temp(nolock) where shipment_no = '" + sqltemp_refno + "'", ClsConnection.SqlCon, objSqlTrx));
                            //DataTable dtDispatchDetails = ReturnDataTable("select * from tdispatchdetails where loccode='" + sendorg + "' and picklistno=" + sqlpicklistno.ToString() + " and lp5no=" + sqllp5no.ToString() + "", ClsConnection.SqlCon, objSqlTrx);
                            DataTable dtDispatchDetails = ReturnDataTable("select * from tdispatchdetails(nolock) where loccode='" + sendorg + "' and picklistno=ISNULL('" + sqlpicklistno.ToString() + "',0) and lp5no=ISNULL('" + sqllp5no.ToString() + "',0)", ClsConnection.SqlCon, objSqlTrx); //Saba 20150603

                            object objLocType = ExecuteQueryScalarWithTransaction("Select LocType from mLocations(nolock) Where LocCode='" + sendorg + "'", ClsConnection.SqlCon, objSqlTrx);

                            if (Convert.ToString(objLocType).ToUpper() == "PSW")
                            {


                                foreach (DataRow drtDD in dtDispatchDetails.Rows)
                                {

                                    DataTable dtTransporter = ReturnDataTable("Select TransporterName,Destination from mTransporters(nolock) Where TransporterCode='" + drtDD["TransporterCode"].ToString() + "'", ClsConnection.SqlCon, objSqlTrx);
                                    string sTransporterName = Convert.ToString(dtTransporter.Rows[0]["TransporterName"]).Trim();
                                    sTransporterName = sTransporterName.Length > 25 ? sTransporterName.Substring(0, 25) : sTransporterName;
                                    string sDestination = Convert.ToString(dtTransporter.Rows[0]["Destination"]);

                                    string sTruckNo = "";

                                    if (drtDD["ToLocCode"].ToString().ToUpper() == "M82")
                                    {
                                        sTruckNo = drtDD["TruckNo"].ToString().ToUpper() + "/" + DateTime.Now.Date.ToString("ddMMyy");
                                    }
                                    else
                                    {
                                        sTruckNo = drtDD["LRNo"].ToString().Trim().ToUpper().Replace("-", "").Replace("/", "").Replace(@"\", "") + "/" + drtDD["TruckNo"].ToString().ToUpper() + "/" + DateTime.Now.Date.ToString("ddMMyy");
                                    }

                                    oFreightAmount = ExecuteQueryScalarWithTransaction("Select Amount from mTransporters(nolock) Where TransporterCode='" + drtDD["TransporterCode"].ToString() + "' And Destination='" + sDestination + "'", ClsConnection.SqlCon, objSqlTrx);

                                    object oExistingTruckWeight = ExecuteQueryScalarWithTransaction("Select sum(Trx_Qty) from BC_ERP_IOT_DTL(nolock) Where Ship_Num='" + sTruckNo + "'", ClsConnection.SqlCon, objSqlTrx);
                                    //string[] ERPLoc = strERPLoc.Split('|');
                                    string strToLoc = drtDD["ToLocCode"].ToString();
                                    //int iERP = Array.IndexOf(ERPLoc, strToLoc);
                                    DataTable dtWebSetting = ReturnDataTable("Select * from mWebSetting(nolock) Where FactoryCode='" + strToLoc + "'", ClsConnection.SqlCon, objSqlTrx);
                                    //if (iERP != -1)
                                    if (dtWebSetting.Rows.Count > 0)
                                    {


                                        //string sERPLoc = ERPLoc[iERP];
                                        //string[] ERPOrg = strERPOrg.Split('|');
                                        //string sERPOrg = ERPOrg[iERP];
                                        //string[] ERPAcctCode = strERPAcctCode.Split('|');
                                        //string sERPAcctCode = ERPAcctCode[iERP];


                                        bool bERP = Convert.ToBoolean(dtWebSetting.Rows[0]["ERP"]);
                                        bool bWMS = Convert.ToBoolean(dtWebSetting.Rows[0]["WMS"]);
                                        string sBasedOn = dtWebSetting.Rows[0]["ERPBasedOn"].ToString();
                                        string sERPOrg = dtWebSetting.Rows[0]["ERPOrgCode"].ToString();
                                        string sERPAcctCode = dtWebSetting.Rows[0]["ERPAcctCode"].ToString();


                                        if (sERPOrg == "RGT")
                                        {
                                            sDestination = "GHAZIABAD";
                                        }
                                        decimal dExistingTruckWeight = 0;


                                        if (oExistingTruckWeight != System.DBNull.Value)
                                        { dExistingTruckWeight = Convert.ToDecimal(oExistingTruckWeight); }

                                        oFreightAmount = Convert.ToDecimal(oFreightAmount) / (Convert.ToDecimal(drtDD["FreightWeight"]) + dExistingTruckWeight);

                                        //SqlCommand cmdERP = new SqlCommand("sp_ERPInterface",cDB.m_Con,cDB.);
                                        //cmdERP.CommandType = CommandType.StoredProcedure;
                                        //cmdERP.Parameters.Add("@LocCode", SqlDbType.Char, 3).Value = sLocCode;
                                        //cmdERP.Parameters.Add("@PickListNo", SqlDbType.Int).Value = Convert.ToInt32(drtDD["PickListNo"]);
                                        //cmdERP.Parameters.Add("@LP5No", SqlDbType.Int).Value = Convert.ToInt32(drtDD["LP5No"]);
                                        //SqlDataAdapter sda = new SqlDataAdapter(cmdERP);
                                        DataTable dtERP = ReturnDataTable("Exec sp_ERPInterface '" + sendorg + "'," + Convert.ToInt32(drtDD["PickListNo"]) + "," + Convert.ToInt32(drtDD["LP5No"]), ClsConnection.SqlCon, objSqlTrx);
                                        //sda.Fill(dtERP);

                                        string sItemCode = "";
                                        string sCropYear = "";
                                        decimal NetWeight = 0;
                                        string sSQLQuery = "";

                                        //cDB.Connect();
                                        //cDB.BeginTrans();

                                        foreach (DataRow drERP in dtERP.Rows)
                                        {
                                            if (bERP)
                                            {
                                                if (sBasedOn == "ITEMCODE")
                                                {
                                                    if (sItemCode == drERP["ItemCode"].ToString())
                                                    {
                                                        NetWeight = NetWeight + Convert.ToDecimal(drERP["NetWeight"]);
                                                        if (!(sCropYear.Contains("|" + drERP["CropYearName"].ToString())))
                                                        {
                                                            sCropYear = sCropYear + "|" + drERP["CropYearName"].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (sItemCode != "")
                                                        {
                                                            //SqlConnection cnnServer2 = new SqlConnection(ConfigurationSettings.AppSettings["cnnServer"].ToString());
                                                            //SqlCommand cmd = new SqlCommand("Select * from BC_ERP_IOT_DTL Where ITEM_CODE='" + sItemCode + "' And TRX_REF='LP5 " + sLocCode + " " + drtDD["LP5No"].ToString() + "' And LOT_NUM='" + sCropYear.Substring(1) + "'", cnnServer2);
                                                            DataTable dtERP2 = ReturnDataTable("Select * from BC_ERP_IOT_DTL(nolock) Where ITEM_CODE='" + sItemCode + "' And TRX_REF='LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "' And LOT_NUM='" + sCropYear.Substring(1) + "'", ClsConnection.SqlCon, objSqlTrx);
                                                            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                                                            //sda.Fill(dtERP2);
                                                            if (dtERP2.Rows.Count == 0)
                                                            {
                                                                sSQLQuery = " INSERT INTO BC_ERP_IOT_DTL(ITEM_CODE,FROM_INV_ORG,TO_INV_ORG,FREIGHT_COST_PER_UNIT,TRX_QTY,TRX_REF,FREIGHT_CODE,SHIP_NUM,WAYBILL,ACCT_CODE,LOT_NUM) "
                                                                  + "values('" + sItemCode + "','" + sendorg + "','" + sERPOrg + "'," + Convert.ToDecimal(oFreightAmount) + "," + Convert.ToDecimal(NetWeight) + ",'LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "','" + sTransporterName + "','" + sTruckNo + "','" + drtDD["AWBNo"].ToString() + "','" + sERPAcctCode + "','" + sCropYear.Substring(1) + "')";
                                                                ExecuteQueryWithTransaction(sSQLQuery, ClsConnection.SqlCon, objSqlTrx);
                                                            }
                                                            //cnnServer2 = null;
                                                            //cmd = null;
                                                            dtERP2 = null;
                                                            //sda = null;
                                                        }
                                                        sItemCode = drERP["ItemCode"].ToString();
                                                        NetWeight = Convert.ToDecimal(drERP["NetWeight"]);
                                                        sCropYear = "|" + drERP["CropYearName"].ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    //SqlConnection cnnServer2 = new SqlConnection(ConfigurationSettings.AppSettings["cnnServer"].ToString());
                                                    //SqlCommand cmd = new SqlCommand("Select * from BC_ERP_IOT_DTL Where ITEM_CODE='" + drERP["ItemCode"].ToString() + "' And TRX_REF='LP5 " + sLocCode + " " + drtDD["LP5No"].ToString() + "' And LOT_NUM='" + drERP["CaseBarCode"].ToString() + "'", cnnServer2);
                                                    DataTable dtERP2 = ReturnDataTable("Select * from BC_ERP_IOT_DTL(nolock) Where ITEM_CODE='" + drERP["ItemCode"].ToString() + "' And TRX_REF='LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "' And LOT_NUM='" + drERP["CaseBarCode"].ToString() + "'", ClsConnection.SqlCon, objSqlTrx);
                                                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                                                    //sda.Fill(dtERP2);
                                                    if (dtERP2.Rows.Count == 0)
                                                    {
                                                        sSQLQuery = " INSERT INTO BC_ERP_IOT_DTL(ITEM_CODE,FROM_INV_ORG,TO_INV_ORG,FREIGHT_COST_PER_UNIT,TRX_QTY,TRX_REF,FREIGHT_CODE,SHIP_NUM,WAYBILL,ACCT_CODE,LOT_NUM) "
                                                                  + "values('" + drERP["ItemCode"].ToString() + "','" + sendorg + "','" + sERPOrg + "'," + Convert.ToDecimal(oFreightAmount) + "," + Convert.ToDecimal(drERP["NetWeight"]) + ",'LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "','" + sTransporterName + "','" + sTruckNo + "','" + drtDD["AWBNo"].ToString() + "','" + sERPAcctCode + "','" + drERP["CaseBarCode"].ToString() + "')";
                                                        ExecuteQueryWithTransaction(sSQLQuery, ClsConnection.SqlCon, objSqlTrx);
                                                    }
                                                    //cnnServer2 = null;
                                                    //cmd = null;
                                                    dtERP2 = null;
                                                    //sda = null;
                                                }
                                            }





                                            //////////---Start-------Entry in WMS table----------/////
                                            //if (strToLoc == "M82")
                                            //{
                                            if (bWMS)
                                            {
                                                //SqlConnection cnnServer2 = new SqlConnection(ConfigurationSettings.AppSettings["cnnServer"].ToString());
                                                //SqlCommand cmd = new SqlCommand("Select * from BC_WMS_LEAF_ASN_D Where BC_WALD_SKU_CD='" + drERP["ItemCode"].ToString() + "' And BC_WALD_LP5_NO='LP5 " + sLocCode + " " + drtDD["LP5No"].ToString() + "' And BC_WALD_BALE_NO='" + drERP["CaseBarCode"].ToString() + "'", cnnServer2);
                                                DataTable dtWMS = ReturnDataTable("Select * from BC_WMS_LEAF_ASN_D(nolock) Where BC_WALD_SKU_CD='" + drERP["ItemCode"].ToString() + "' And BC_WALD_LP5_NO='LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "' And BC_WALD_BALE_NO='" + drERP["CaseBarCode"].ToString() + "'", ClsConnection.SqlCon, objSqlTrx);
                                                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                                                //sda.Fill(dtWMS);
                                                if (dtWMS.Rows.Count == 0)
                                                {
                                                    //sSQLQuery = "INSERT INTO BC_WMS_LEAF_ASN_D (BC_WALD_ITM,BC_WALD_LP5_NO,BC_WALD_LP5_DATE,BC_WALD_LOT_NO,BC_WALD_SKU_CD,BC_WALD_BALE_NO,BC_WALD_QTY_NET,BC_WALD_QTY_GROSS,BC_WALD_EXPIRY_DT,BC_WALD_TAR,BC_WALD_NIC,BC_WALD_CHLORIDE,BC_WALD_MOISTURE,BC_WALD_INSERT_DT) "
                                                    //    + "values('TOB_RM','LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "',CONVERT(DATETIME,'" + Convert.ToDateTime(drtDD["DispatchedOn"].ToString()) + "',105),'" + drERP["CropYearName"].ToString() + "','" + drERP["ItemCode"].ToString() + "','" + drERP["CaseBarCode"].ToString() + "'," + Convert.ToDecimal(drERP["NetWeight"]) + "," + Convert.ToDecimal(drERP["GrossWeight"]) + ",CONVERT(DATETIME,'" + Convert.ToDateTime(drERP["FumEndOn"]) + "',105),'" + drERP["TRS"].ToString() + "','" + drERP["NIC"].ToString() + "','" + drERP["CL"].ToString() + "','" + drERP["Moisture"].ToString() + "',getdate())";

                                                    //////
                                                    /////
                                                    /////Code Added to Anandraj -  Get the FumStartOn - 20 days from tcaseDetails into BC_WMS_LEAF_ASN_D table BC_WALD_EXPIRY_DT Fields (29th Nov 2021)
                                                    /////
                                                    /////

                                                    DataTable dtWaldExpityDate = ReturnDataTable("select FumStartOn  from dbo.tCaseDetails where CaseBarCode ='" + drERP["CaseBarCode"].ToString() + "' ", ClsConnection.SqlCon, objSqlTrx);
                                                    DateTime WaldExpityDate = new DateTime();
                                                    if (dtWaldExpityDate.Rows.Count > 0)
                                                    {
                                                        WaldExpityDate = Convert.ToDateTime(dtWaldExpityDate.Rows[0]["FumStartOn"]);
                                                        // string sWaldExpityDate = Convert.ToString(dtWaldExpityDate.Rows[0]["FumStartOn"]).Trim();
                                                        WaldExpityDate = WaldExpityDate.Subtract(TimeSpan.FromDays(15));
                                                    }
                                                    string formattedExpityDate = WaldExpityDate.ToString("yyyy-MM-dd HH:mm:ss");
                                                    
                                                    sSQLQuery = "INSERT INTO BC_WMS_LEAF_ASN_D (BC_WALD_ITM,BC_WALD_LP5_NO,BC_WALD_LP5_DATE,BC_WALD_LOT_NO,BC_WALD_SKU_CD,BC_WALD_BALE_NO,BC_WALD_QTY_NET,BC_WALD_QTY_GROSS,BC_WALD_EXPIRY_DT,BC_WALD_TAR,BC_WALD_NIC,BC_WALD_CHLORIDE,BC_WALD_MOISTURE,BC_WALD_INSERT_DT) "
                                                    + "values('TOB_RM','LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "',GETDATE(),'" + drERP["CropYearName"].ToString() + "','" + drERP["ItemCode"].ToString() + "','" + drERP["CaseBarCode"].ToString() + "'," + Convert.ToDecimal(drERP["NetWeight"]) + "," + Convert.ToDecimal(drERP["GrossWeight"]) + ",'" + formattedExpityDate + "','" + drERP["TRS"].ToString() + "','" + drERP["NIC"].ToString() + "','" + drERP["CL"].ToString() + "','" + drERP["Moisture"].ToString() + "',getdate())";

                                                    ////////////
                                                    //////////////
                                                    ///////////////

                                                    // sSQLQuery = "INSERT INTO BC_WMS_LEAF_ASN_D (BC_WALD_ITM,BC_WALD_LP5_NO,BC_WALD_LP5_DATE,BC_WALD_LOT_NO,BC_WALD_SKU_CD,BC_WALD_BALE_NO,BC_WALD_QTY_NET,BC_WALD_QTY_GROSS,BC_WALD_EXPIRY_DT,BC_WALD_TAR,BC_WALD_NIC,BC_WALD_CHLORIDE,BC_WALD_MOISTURE,BC_WALD_INSERT_DT) "
                                                    // + "values('TOB_RM','LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "',GETDATE(),'" + drERP["CropYearName"].ToString() + "','" + drERP["ItemCode"].ToString() + "','" + drERP["CaseBarCode"].ToString() + "'," + Convert.ToDecimal(drERP["NetWeight"]) + "," + Convert.ToDecimal(drERP["GrossWeight"]) + ",GETDATE()+7,'" + drERP["TRS"].ToString() + "','" + drERP["NIC"].ToString() + "','" + drERP["CL"].ToString() + "','" + drERP["Moisture"].ToString() + "',getdate())";

                                                    ExecuteQueryWithTransaction(sSQLQuery, ClsConnection.SqlCon, objSqlTrx);
                                                }
                                                //cnnServer2 = null;
                                                //cmd = null;
                                                dtWMS = null;
                                                //sda = null;

                                            }
                                            //}

                                            //////////---End-------Entry in WMS table----------/////








                                        }
                                        if (bERP)
                                        {
                                            if (sBasedOn == "ITEMCODE" && sItemCode != "")
                                            {
                                                //SqlConnection cnnServer2 = new SqlConnection(ConfigurationSettings.AppSettings["cnnServer"].ToString());
                                                //SqlCommand cmd = new SqlCommand("Select * from BC_ERP_IOT_DTL Where ITEM_CODE='" + sItemCode + "' And TRX_REF='LP5 " + sLocCode + " " + drtDD["LP5No"].ToString() + "' And LOT_NUM='" + sCropYear.Substring(1) + "'", cnnServer2);
                                                DataTable dtERP2 = ReturnDataTable("Select * from BC_ERP_IOT_DTL(nolock) Where ITEM_CODE='" + sItemCode + "' And TRX_REF='LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "' And LOT_NUM='" + sCropYear.Substring(1) + "'", ClsConnection.SqlCon, objSqlTrx);
                                                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                                                //sda.Fill(dtERP2);
                                                if (dtERP2.Rows.Count == 0)
                                                {
                                                    sSQLQuery = " INSERT INTO BC_ERP_IOT_DTL(ITEM_CODE,FROM_INV_ORG,TO_INV_ORG,FREIGHT_COST_PER_UNIT,TRX_QTY,TRX_REF,FREIGHT_CODE,SHIP_NUM,WAYBILL,ACCT_CODE,LOT_NUM) "
                                                        + "values('" + sItemCode + "','" + sendorg + "','" + sERPOrg + "'," + Convert.ToDecimal(oFreightAmount) + "," + Convert.ToDecimal(NetWeight) + ",'LP5 " + sendorg + " " + drtDD["LP5No"].ToString() + "','" + sTransporterName + "','" + sTruckNo + "','" + drtDD["AWBNo"].ToString() + "','" + sERPAcctCode + "','" + sCropYear.Substring(1) + "')";
                                                    ExecuteQueryWithTransaction(sSQLQuery, ClsConnection.SqlCon, objSqlTrx);
                                                }
                                                //cnnServer2 = null;
                                                //cmd = null;
                                                dtERP2 = null;
                                                //sda = null;

                                            }
                                        }

                                        sSQLQuery = "Update BC_ERP_IOT_DTL set FREIGHT_COST_PER_UNIT=" + Convert.ToDecimal(oFreightAmount) + " Where Ship_Num='" + sTruckNo + "'";
                                        ExecuteQueryWithTransaction(sSQLQuery, ClsConnection.SqlCon, objSqlTrx);


                                    }
                                }


                            }


                        }
                    }
                }


                string strSQLQuery = "Update GPIL_SHIPMENT_HDR SET FRIEGHT_CHARGES=ROUND(" + Convert.ToDecimal(oFreightAmount).ToString() + ",2),FLAG='YY',nFLAG='YY',STATUS='N' Where SHIPMENT_NO IN (" + strHeadeIDs + ")";
                ExecuteQueryWithTransaction(strSQLQuery, ClsConnection.SqlCon, objSqlTrx);


                objSqlTrx.Commit();
                ClsConnection.SqlCon.Close();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Factory Dispatch", "alert('Dispatch has completed for selected shipment numbers');", true);

                ddlLocationCode.SelectedIndex = 0;
                ddlTransporterCode.SelectedIndex = 0;
                ddlTruckNumber.SelectedIndex = 0;
                gvbind();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);

                objSqlTrx.Rollback();
                objSqlTrx.Dispose();

            }


        }
        private bool FuncBoolCompleteFactoryDispatch(string inParamStrShipmentNos)
        {
            return true;

            //try
            //{
            //    SqlDataAdapter objSqlDataAdapter1 = new SqlDataAdapter(strReceivedBales, ClsConnection.SqlCon);
            //    dtclstr.Clear();
            //    objSqlDataAdapter1.Fill(dtclstr);

            //   SqlDataAdapter objSqlDataAdapter2 = new SqlDataAdapter(strbatch, ClsConnection.SqlCon);
            //    orgdata.Clear();
            //    objSqlDataAdapter2.Fill(orgdata);

            //    purchasedata();

            //    if (insertinfo() == true)
            //    {

            //        string strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
            //        SqlCommand objSqlCommand = new SqlCommand(strUpdate, ClsConnection.SqlCon);
            //        objSqlCommand.ExecuteNonQuery();
            //        objSqlCommand.Dispose();
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }



            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
        private bool FuncBoolIsExist(string inParamStrQuery)
        {
            try
            {
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(inParamStrQuery, ClsConnection.SqlCon);
                objSqlDataAdapter.SelectCommand.CommandTimeout = 0;
                DataTable objDataTable = new DataTable();
                objSqlDataAdapter.Fill(objDataTable);
                if (objDataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            gvbind();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void ddlTransporterCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsConnection.connectDB();
            strsql = "SELECT DISTINCT SENDER_TRUCK_NO FROM GPIL_SHIPMENT_HDR(nolock) WHERE SENDER_DATE BETWEEN CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) AND CONVERT(DATETIME,'" + txt_Report_Date.Text + " 23:59:59',102) AND STATUS='INT' AND RECEIVER_ORGN_CODE='" + ddlLocationCode.Text + "' AND TRANSPORT_NAME='" + ddlTransporterCode.Text + "'";
            SqlCommand cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            ClsConnection.closeDB();

            ddlTruckNumber.DataSource = ds1;
            ddlTruckNumber.DataTextField = "SENDER_TRUCK_NO";
            ddlTruckNumber.DataValueField = "SENDER_TRUCK_NO";
            ddlTruckNumber.DataBind();
            ddlTruckNumber.Items.Insert(0, "< -- Select -- >");

        }
        public void ExecuteQueryWithTransaction(string sSqlQuery, SqlConnection cnn, SqlTransaction trxn)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, ClsConnection.SqlCon, trxn);
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
        }

        public DataTable ReturnDataTable(string sSqlQuery, SqlConnection cnn, SqlTransaction trxn)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, cnn);
            cmd.CommandTimeout = 0;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand.Transaction = trxn;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public object ExecuteQueryScalarWithTransaction(string sSqlQuery, SqlConnection cnn, SqlTransaction trxn)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, cnn, trxn);
            cmd.CommandTimeout = 0;
            object obj = cmd.ExecuteScalar();
            return obj;
        }

        protected void btnView_Click3(object sender, EventArgs e)
        {
            if (txt_Report_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the LR Date');", true);
                return;
            }

            if (ddlLocationCode.SelectedIndex == 0 || ddlLocationCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Location code for Factory Dispatch');", true);
                return;
            }

            if (ddlTransporterCode.SelectedIndex == 0 || ddlTransporterCode.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Transporter Code for Factory Dispatch');", true);
                return;
            }

            if (ddlTruckNumber.SelectedIndex == 0 || ddlTruckNumber.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Truck Number for Factory Dispatch');", true);
                return;
            }


            gvbind();
        }

    }
}