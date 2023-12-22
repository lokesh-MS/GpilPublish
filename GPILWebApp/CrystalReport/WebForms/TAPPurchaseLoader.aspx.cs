using GPI;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace GPILWebApp
{
    public partial class TAPPurchaseLoader : System.Web.UI.Page
    {

        DataSet exceldata = new DataSet();
        OleDbDataAdapter data;
        public static DataTable dtclstr = new DataTable();
        public static DataTable orgdata = new DataTable();
        public static DataSet purdata = new DataSet();
        public static string filename;
        public static string TAPerror;
        //testDataContext test;
        //SqlTransaction trx;
        //SqlCommand cmd;
        //SqlDataReader strrs;
        public static string errfile;
        string strsql;

        static string servicechargeid;
        static string servicetaxid;
        static string servicechargeedshid;
        static string servicechargeshcessid;

        static double servicecharge;
        static double servicetax;
        static double servicechargeedsh;
        static double servicechargeshcess;

        double servicechargeamt;
        double servicetaxamt;
        double servicechargeedshamt;
        double servicechargeshcessamt;

        TPLoader tpLoader = new TPLoader();
        DataTable dt;
        List<string> lstQuery = new List<string>(15);

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                Response.ClearHeaders();
                Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
                Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1
                Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1
                Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1
                Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.1

                //This code is used to maintain UserName in the Home page using Session and Cookies 

                //if (Session["SessionUserName"] == null)
                //{
                //    Response.Redirect("FrmLogin.aspx");
                //}

                //else
                //{

                //    IEnumerator mc;
                //    mc = Request.Cookies.AllKeys.GetEnumerator();

                //    while (mc.MoveNext())
                //    {

                //        if (Request.Cookies[mc.Current.ToString()].HasKeys == true)
                //        {

                //            IEnumerator sc;
                //            sc = Request.Cookies[mc.Current.ToString()].Value.GetEnumerator();

                //            while (sc.MoveNext())
                //            {

                //                //Response.Write(sc.Current.ToString() + Request.Cookies[mc.Current.ToString()][sc.Current.ToString()]); 
                //            }
                //        }
                //    }
                //}
                if (!IsPostBack)
                {

                    clrgridview();

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in loading";
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }



        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                errfile = string.Empty;
                btndwnerr.Visible = false;
                filename = fileuploaditem.FileName;
                string path = string.Concat(Server.MapPath("~/TempFiles/"), filename);
                OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
                fileuploaditem.SaveAs(path);
                string query = "select GPIL_BALE_NUMBER,TB_LOT_NO,TBGR_NO,TB_GRADE,NET_WT,RATE,BUYER_GRADE,REJE_STATUS,IIF(REJE_TYPE='CR',REJE_TYPE ,IIF(REJE_TYPE='RR',REJE_TYPE,'NONE')) AS REJE_TYPE,PATTA_CHARGE,ORGN_CODE,BUYER_CODE,PURCH_DOC_NO,PURCHASE_DATE,CROP,VARIETY,'V' AS INS_STS from [Sheet1$]";
                data = new OleDbDataAdapter(query, oconn);
                dtclstr.Clear();
                data.Fill(dtclstr);
                GridViewSample.DataSource = dtclstr;
                GridViewSample.DataBind();
                lblgridcount.Text = Convert.ToString(dtclstr.Rows.Count);
                GridViewSample.EditIndex = -1;
                data.Dispose();
                string query1 = "select ORGN_CODE,PURCH_DOC_NO from [Sheet1$] GROUP BY ORGN_CODE,PURCH_DOC_NO ";
                data = new OleDbDataAdapter(query1, oconn);
                orgdata.Clear();
                data.Fill(orgdata);
                query = "ASDASD";
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
        private void LoadData()
        {
            GridViewSample.DataSource = dtclstr;
            GridViewSample.DataBind();
            lblgridcount.Text = Convert.ToString(dtclstr.Rows.Count);
        }


        protected void GridViewSample_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSample.PageIndex = e.NewPageIndex;
            LoadData();

        }

        protected void GridViewSample_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSample.EditIndex = -1;
            LoadData();
        }

        protected void GridViewSample_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSample.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void GridViewSample_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label baleno = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblbaleno");
                //  TextBox Name = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txttblot");
                TextBox tblot = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txttblot");
                TextBox tbgrno = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txttbgrno");
                TextBox tbgrade = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtTBGrade");
                TextBox netwt = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtNETWT");
                TextBox rate = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtRATE");
                TextBox byrgrade = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtBUYERGRADE");
                DropDownList rejtype = (DropDownList)GridViewSample.Rows[e.RowIndex].FindControl("ddlrejtype");

                TextBox pattacharge = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtpattacharge");
                TextBox orgcode = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtorgcode");
                TextBox buyercode = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtbuyercode");
                TextBox purchasedoc = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtpurchdocno");
                TextBox purchasedate = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtpurchdate");
                TextBox crop = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtCrop");
                TextBox variety = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtvariety");
                TextBox flg = (TextBox)GridViewSample.Rows[e.RowIndex].FindControl("txtupdatests");

                DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER ='" + baleno.Text + "'");
                // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["TB_LOT_NO"] = tblot.Text.Trim();
                        row["TBGR_NO"] = tbgrno.Text.Trim();
                        row["TB_GRADE"] = tbgrade.Text.Trim();
                        row["NET_WT"] = netwt.Text.Trim();
                        row["RATE"] = rate.Text.Trim();
                        row["BUYER_GRADE"] = byrgrade.Text.Trim();
                        row["REJE_TYPE"] = rejtype.Text.Trim();
                        row["PATTA_CHARGE"] = pattacharge.Text.Trim();
                        row["ORGN_CODE"] = orgcode.Text.Trim();
                        row["BUYER_CODE"] = buyercode.Text.Trim();
                        row["PURCH_DOC_NO"] = purchasedoc.Text.Trim();
                        row["PURCHASE_DATE"] = purchasedate.Text.Trim();
                        row["CROP"] = crop.Text.Trim();
                        row["VARIETY"] = variety.Text.Trim();
                        row["INS_STS"] = flg.Text.Trim();

                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }


                GridViewSample.EditIndex = -1;
                LoadData();
                lblgridcount.Text = Convert.ToString(dtclstr.Rows.Count);
                lblMessage.Text = "Record Updated Successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void GridViewSample_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label baleno = (Label)GridViewSample.Rows[e.RowIndex].FindControl("lblbaleno");
                DataRow[] rowsdel = dtclstr.Select("GPIL_BALE_NUMBER ='" + baleno.Text + "'");
                foreach (var rows in rowsdel)
                    rows.Delete();
                GridViewSample.EditIndex = -1;
                LoadData();
                lblgridcount.Text = Convert.ToString(dtclstr.Rows.Count);
                lblMessage.Text = "Record deleted successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void GridViewSample_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Insert"))
                {
                    TextBox baleno1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddbaleno");
                    TextBox tblot1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddtblot");
                    TextBox tbgrno1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddtbgrno");
                    TextBox tbgrade1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddTBGrade");
                    TextBox netwt1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddNETWT");
                    TextBox rate1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddRATE");
                    TextBox byrgrade1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddBUYERGRADE");
                    TextBox rejtype1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddAddrejtype");
                    TextBox pattacharge1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddpattacharge");
                    TextBox orgcode1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddorgcode");
                    TextBox buyercode1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddbuyercode");
                    TextBox purchasedoc1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddpurchdocno");
                    TextBox purchasedate1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddpurchdate");
                    TextBox crop1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddCrop");
                    TextBox variety1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddvariety");
                    TextBox flg1 = (TextBox)GridViewSample.FooterRow.FindControl("txtAddupdatests");

                    DataRow row = dtclstr.NewRow();
                    row["GPIL_BALE_NUMBER"] = baleno1.Text;
                    row["TB_LOT_NO"] = tblot1.Text;
                    row["TBGR_NO"] = tbgrno1.Text;
                    row["TB_GRADE"] = tbgrade1.Text;
                    row["NET_WT"] = netwt1.Text;
                    row["RATE"] = rate1.Text;
                    row["BUYER_GRADE"] = byrgrade1.Text;
                    row["REJE_TYPE"] = rejtype1.Text.Trim();
                    row["PATTA_CHARGE"] = pattacharge1.Text.Trim();
                    row["ORGN_CODE"] = orgcode1.Text.Trim();
                    row["BUYER_CODE"] = buyercode1.Text.Trim();
                    row["PURCH_DOC_NO"] = purchasedoc1.Text.Trim();
                    row["PURCHASE_DATE"] = purchasedate1.Text.Trim();
                    row["CROP"] = crop1.Text.Trim();
                    row["VARIETY"] = variety1.Text.Trim();
                    row["INS_STS"] = flg1.Text.Trim();

                    dtclstr.Rows.Add(row);
                    GridViewSample.EditIndex = -1;
                    LoadData();
                    lblgridcount.Text = Convert.ToString(dtclstr.Rows.Count);
                    lblMessage.Text = "Record inserted successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                clrgridview();
                cleardataset();
                dtclstr.Clear();
                lblMessage.Text = string.Empty;
                lblgridcount.Text = string.Empty;
                errfile = string.Empty;
                btndwnerr.Visible = false;
            }
            catch (Exception ex)
            {
               lblMessage.Text = "Error: Catch";

                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }

        public void cleardataset()
        {
            try
            {
                while (purdata.Tables.Count > 0)
                {
                    DataTable tb = purdata.Tables[0];
                    if (purdata.Tables.CanRemove(tb))
                    {
                        purdata.Tables.Remove(tb);
                    }
                }
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }
        public void sevicetaxes()
        {

            DataTable dtServiceAmt = new DataTable();
            try
            {
                dtServiceAmt = tpLoader.GetServiceAmount("1");

                if (dtServiceAmt.Rows.Count > 0)
                {
                    servicetaxid = dtServiceAmt.Rows[0]["TAX_ID"].ToString();
                    servicetax = Convert.ToDouble(dtServiceAmt.Rows[0]["RATE"].ToString());
                }


                dtServiceAmt = tpLoader.GetServiceAmount("2");
                if (dtServiceAmt.Rows.Count > 0)
                {
                    servicechargeid = dtServiceAmt.Rows[0]["TAX_ID"].ToString();
                    servicecharge = Convert.ToDouble(dtServiceAmt.Rows[0]["RATE"].ToString());
                }
                dtServiceAmt = tpLoader.GetServiceAmount("3");
                if (dtServiceAmt.Rows.Count > 0)
                {
                    servicechargeedshid = dtServiceAmt.Rows[0]["TAX_ID"].ToString();
                    servicechargeedsh = Convert.ToDouble(dtServiceAmt.Rows[0]["RATE"].ToString());
                }
                dtServiceAmt = tpLoader.GetServiceAmount("3");
                if (dtServiceAmt.Rows.Count > 0)
                {
                    servicechargeshcessid = dtServiceAmt.Rows[0]["TAX_ID"].ToString();
                    servicechargeshcess = Convert.ToDouble(dtServiceAmt.Rows[0]["RATE"].ToString());
                }

            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }

        public void clrgridview()
        {
            try
            {
                DataSet clrds = new DataSet();
                clrds.Tables.Add("TEMP");
                clrds.Tables[0].Columns.Add("GPIL_BALE_NUMBER");
                clrds.Tables[0].Columns.Add("TB_LOT_NO");
                clrds.Tables[0].Columns.Add("TBGR_NO");
                clrds.Tables[0].Columns.Add("TB_GRADE");
                clrds.Tables[0].Columns.Add("NET_WT");
                clrds.Tables[0].Columns.Add("RATE");
                clrds.Tables[0].Columns.Add("BUYER_GRADE");
                clrds.Tables[0].Columns.Add("REJE_STATUS");
                clrds.Tables[0].Columns.Add("REJE_TYPE");
                clrds.Tables[0].Columns.Add("PATTA_CHARGE");
                clrds.Tables[0].Columns.Add("ORGN_CODE");
                clrds.Tables[0].Columns.Add("BUYER_CODE");

                clrds.Tables[0].Columns.Add("PURCH_DOC_NO");
                clrds.Tables[0].Columns.Add("PURCHASE_DATE");
                clrds.Tables[0].Columns.Add("CROP");
                clrds.Tables[0].Columns.Add("VARIETY");
                clrds.Tables[0].Columns.Add("INS_STS");
                clrds.Tables[0].Rows.Add(clrds.Tables[0].NewRow());
                GridViewSample.DataSource = clrds;
                GridViewSample.DataBind();
                int columncount = GridViewSample.Rows[0].Cells.Count;
                GridViewSample.Rows[0].Cells.Clear();
                GridViewSample.Rows[0].Cells.Add(new TableCell());
                GridViewSample.Rows[0].Cells[0].ColumnSpan = columncount;
                GridViewSample.Rows[0].Cells[0].Text = "No Records Found";
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }

        protected void btncomplete_Click(object sender, EventArgs e)
        {
            cleardataset();
            purchasedata();
            insertinfo();
            lblgridcount.Text = string.Empty;
        }

        public void purchasedata()
        {
            try
            {
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["ORGN_CODE"].ToString() + orgdata.Rows[s]["PURCH_DOC_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("TB_LOT_NO");
                    purdata.Tables[s].Columns.Add("TBGR_NO");
                    purdata.Tables[s].Columns.Add("TB_GRADE");
                    purdata.Tables[s].Columns.Add("NET_WT");
                    purdata.Tables[s].Columns.Add("RATE");
                    purdata.Tables[s].Columns.Add("BUYER_GRADE");
                    purdata.Tables[s].Columns.Add("REJE_STATUS");
                    purdata.Tables[s].Columns.Add("REJE_TYPE");
                    purdata.Tables[s].Columns.Add("PATTA_CHARGE");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("BUYER_CODE");
                    purdata.Tables[s].Columns.Add("PURCH_DOC_NO");
                    purdata.Tables[s].Columns.Add("PURCHASE_DATE");
                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");

                    string orgcd = orgdata.Rows[s]["ORGN_CODE"].ToString();
                    string purdoc = orgdata.Rows[s]["PURCH_DOC_NO"].ToString();
                    DataRow[] purrows = dtclstr.Select("ORGN_CODE ='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "' AND PURCH_DOC_NO ='" + orgdata.Rows[s]["PURCH_DOC_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow row in purrows)
                        {
                            purdata.Tables[s].ImportRow(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: Catch";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }
        public void insertinfo()
        {
            if (validate())
            {
                //trx = ClsConnection.SqlCon.BeginTransaction();
                try
                {
                    string headerid = "";
                    string orgcd = "";
                    string byrcd = "";
                    string purchdoc = "";
                    string dateofpurch = "";
                    string crop = "";
                    string variety = "";

                    for (int d = 0; d < purdata.Tables.Count; d++)
                    {
                         headerid = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                         orgcd = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                         byrcd = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                         purchdoc = purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                        dateofpurch = Convert.ToDateTime(purdata.Tables[d].Rows[0]["PURCHASE_DATE"]).ToString("yyyy/MM/dd");
                        crop = purdata.Tables[d].Rows[0]["CROP"].ToString();
                        variety = purdata.Tables[d].Rows[0]["VARIETY"].ToString();

                        strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_HDR]([HEADER_ID],[ORGN_CODE],[PURCHASE_TYPE],[BUYER_CODE],[PURCH_DOC_NO],[DATE_OF_PURCH],[CROP],[VARIETY],[CREATED_BY],[CREATION_DATE],[STATUS])";
                        strsql = strsql + " VALUES('" + headerid + "','" + orgcd + "','TAP PURCHASE','" + byrcd + "','" + purchdoc + "', '"+dateofpurch+"','" + crop + "','" + variety + "','" + Session["UserID"].ToString() + "',GETDATE(),'P')";
                        // strsql = strsql + " VALUES('" + headerid + "','" + orgcd + "','TAP PURCHASE','" + byrcd + "','" + purchdoc + "',convert(datetime,'"+Convert.ToDateTime(dateofpurch)+"',105),'" + crop + "','" + variety + "','"+Propertycls.EMPCODE+"',GETDATE(),'P')";



                        //////cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                        //////cmd.CommandTimeout = 0;
                        //////cmd.Transaction = trx;
                        //////cmd.ExecuteNonQuery();
                        //////cmd.Dispose();

                        lstQuery.Add(strsql);



                        for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                        {
                            string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            string tblot = purdata.Tables[d].Rows[h]["TB_LOT_NO"].ToString();
                            string tbgrno = purdata.Tables[d].Rows[h]["TBGR_NO"].ToString();
                            string tbgrade = purdata.Tables[d].Rows[h]["TB_GRADE"].ToString();
                            string netwt = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                            string rate = purdata.Tables[d].Rows[h]["RATE"].ToString();
                            string buyergrade = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                            //  string rejests = purdata.Tables[d].Rows[h]["REJE_STATUS"].ToString();
                            string rejetype = purdata.Tables[d].Rows[h]["REJE_TYPE"].ToString();
                            string pattacharge = purdata.Tables[d].Rows[h]["PATTA_CHARGE"].ToString();
                            string status;
                            string rejests;
                            if (rejetype == "NONE")
                            {
                                rejests = "OK";
                            }
                            else
                            {
                                rejests = "RJ";

                            }
                            if (rejests.Trim() == "RJ")
                            {
                                status = "N";
                            }
                            else
                            {
                                status = "Y";
                            }
                            sevicetaxes();
                            double totalprice;
                            double totalservicetaxamt;
                            totalprice = Convert.ToDouble(netwt) * Convert.ToDouble(rate);
                            servicechargeamt = (totalprice * servicecharge) / 100;
                            servicetaxamt = (servicechargeamt * servicetax) / 100;
                            servicechargeedshamt = (servicetaxamt * servicechargeedsh) / 100;
                            servicechargeshcessamt = (servicetaxamt * servicechargeshcess) / 100;
                            totalservicetaxamt = servicetaxamt + servicechargeamt + servicechargeedshamt + servicechargeshcessamt;

                            strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,TB_LOT_NO,TBGR_NO,TB_GRADE,BUYER_GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRICE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                            strsql = strsql + "Values('" + baleno.Trim() + "','FW','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + buyergrade.Trim() + "','" + netwt.Trim() + "','" + netwt.Trim() + "','LOC1','" + orgcd.Trim() + "','" + crop + "','" + variety + "','" + rate.Trim() + "','G','N','" + headerid + "','" + status + "','" + Session["UserID"].ToString() + "',GETDATE(),'" + Session["UserID"].ToString() + "',GETDATE(),'N','TAP','LOC1','" + orgcd + "')";

                            lstQuery.Add(strsql);

                            //////////////cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            ////////////////cmd.CommandTimeout = 0;
                            ////////////////cmd.Transaction = trx;
                            ////////////////cmd.ExecuteNonQuery();
                            ////////////////cmd.Dispose();

                            //////////////lstQuery.Add(strsql);

                            strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[TBGR_NO],[TB_GRADE],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[PATTA_CHARGE],[SERVICE_CHARGE],[SERVICE_CHARGE_AMT],[SERVICE_TAX],[SERVICE_TAX_AMT],[CREATED_BY],[CREATED_DATE] ,[SH_ED_TAX],[ED_CESS_TAX])";
                            if (rejetype == "NONE")
                            {
                                strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "',NULL,'" + status + "','N','" + pattacharge + "','" + servicechargeid + "',ROUND('" + servicechargeamt + "',2),'" + servicetaxid + "',ROUND('" + totalservicetaxamt + "',2),'" + Session["UserID"].ToString() + "',GETDATE(),'" + servicechargeedshid + "','" + servicechargeshcessid + "')";
                            }
                            else
                            {
                                strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "','" + rejetype + "','" + status + "','N','" + pattacharge + "','" + servicechargeid + "','" + servicechargeamt + "','" + servicetaxid + "','" + totalservicetaxamt + "','" + Session["UserID"].ToString() + "',GETDATE(),'" + servicechargeedshid + "','" + servicechargeshcessid + "')";
                            }
                            lstQuery.Add(strsql);
                            //////////////cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //////////////cmd.CommandTimeout = 0;
                            //////////////cmd.Transaction = trx;
                            //////////////cmd.ExecuteNonQuery();
                            //////////////cmd.Dispose();


                            strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                            strsql = strsql + " Values('PC" + orgcd + DateTime.Now.ToString("yyyyMMddhhmmss") + h + "','" + baleno.Trim() + "','TAP Purchase','" + headerid.Trim() + "','" + netwt.Trim() + "','" + orgcd.Trim() + "','" + dateofpurch + "','N')";
                            lstQuery.Add(strsql);
                            // not working strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "','" + Convert.ToDateTime(loadtime) + "','N')";
                            //  cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                            //////////////// cmd.Transaction = trx;
                            //////////////// cmd.ExecuteNonQuery();
                            //////////////// cmd.Dispose();
                            //bool result = tpLoader.InsertTapPurchase(headerid, orgcd, buyergrade, purchdoc, dateofpurch,crop,
                            //      totalprice,variety,Session["UserID"].ToString(),baleno,tblot,rejests,netwt,tbgrno, tbgrno,tbgrade,rate,status,rejetype,pattacharge,servicechargeid,
                            //    rejetype,servicechargeamt.ToString(),servicechargeedshid,servicechargeshcessid,servicetaxid,servicetaxamt.ToString());





                        }

                    }
                    //trx.Commit();
                    ////GridViewSample.DataSource = null;
                    ////GridViewSample.DataBind();

                    //bool result = false;
                    bool result = tpLoader.InsertTapPurchase(lstQuery);


                    if (result)
                    {
                        clrgridview();
                        dtclstr.Clear();
                        lblMessage.Text = "DONE";
                    }
                    else
                    {
                        lblMessage.Text = "NOT DONE";
                    }




                }
                catch (Exception ex)
                {
                    StringBuilder err = new StringBuilder();
                    err.Append(" Message : " + ex.Message);
                    err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                    err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                    err.AppendLine(" SOURCE : " + ex.Source);
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                }
                finally
                {
                    cleardataset();
                }
            }
            else
            {
                cleardataset();
                lblMessage.Text = "Error In Data Which Have Provided Please verify red color rows";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error In Data Which Have Provoded Please verify red color rows');", true);
                btndwnerr.Visible = true;
            }
        }
        //private DataSet GetData(string query)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //        SqlCommand cmd = new SqlCommand(query);
        //        cmd.CommandTimeout = 0;
        //        using (SqlConnection con = new SqlConnection(conString))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.Connection = con;

        //                sda.SelectCommand = cmd;
        //                //using (DataSet ds = new DataSet())
        //                //{
        //                sda.Fill(ds);
        //                //return ds;
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
        //    }
        //    return ds;
        //}
        protected void GridViewSample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
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
                    DropDownList ddlrejtype = (e.Row.FindControl("ddlrejtype") as DropDownList);
                    if (ddlrejtype == null)
                    { }
                    else
                    {
                        //ddlrejtype.DataSource = GetData("select distinct REJ_TYPE From GPIL_REJECTION_TYPE(NOLOCK)");
                        ddlrejtype.DataSource = tpLoader.GetRejectionType();
                        ddlrejtype.DataTextField = "REJ_TYPE";
                        ddlrejtype.DataValueField = "REJ_TYPE";
                        ddlrejtype.DataBind();
                        //Add Default Item in the DropDownList
                        ddlrejtype.Items.Insert(0, new ListItem("NONE"));
                        // Select the Country of Customer in DropDownList
                        Label rejtype = (Label)e.Row.FindControl("lbltemprejtype");
                        // rejtype.Text = ddlrejtype.Text;
                        string country = rejtype.Text;
                        try
                        {
                            ddlrejtype.Items.FindByValue(country).Selected = true;
                        }
                        catch(Exception ex)
                        {
                            StringBuilder err = new StringBuilder();
                            err.Append(" Message : " + ex.Message);
                            err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                            err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                            err.AppendLine(" SOURCE : " + ex.Source);
                            Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                lblMessage.Text = "Please check the transaction";
            }
        }

        public bool validate()
        {
            int i = 0;
            TAPerror = "Error :";
            try
            {

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string tblname = purdata.Tables[d].TableName;
                    int rowcount = purdata.Tables[d].Rows.Count;
                    if (d == 13)
                    {
                        lblMessage.Text = "ADS";
                    }
                    string headerid1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                    string orgcd1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string byrcd1 = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                    string purchdoc1 = purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                    string dateofpurch1 = purdata.Tables[d].Rows[0]["PURCHASE_DATE"].ToString();
                    string crop1 = purdata.Tables[d].Rows[0]["CROP"].ToString();
                    string variety1 = purdata.Tables[d].Rows[0]["VARIETY"].ToString();



                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string tblot1 = purdata.Tables[d].Rows[h]["TB_LOT_NO"].ToString();
                        string tbgrno1 = purdata.Tables[d].Rows[h]["TBGR_NO"].ToString();
                        string tbgrade1 = purdata.Tables[d].Rows[h]["TB_GRADE"].ToString();
                        string netwt1 = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                        string rate1 = purdata.Tables[d].Rows[h]["RATE"].ToString();
                        string buyergrade1 = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                        string rejests1 = purdata.Tables[d].Rows[h]["REJE_STATUS"].ToString();
                        string rejetype1 = purdata.Tables[d].Rows[h]["REJE_TYPE"].ToString();
                        string pattacharge1 = purdata.Tables[d].Rows[h]["PATTA_CHARGE"].ToString();


                        if (baleno1.Substring(0, 2) != crop1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + " Bale Number  and Corp Year MisMatch BaleNumber--" + baleno1;
                        }
                        else if (baleno1.Substring(2, 2) != variety1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + " Bale Number  and Variety MisMatch BaleNumber--" + baleno1;
                        }
                        else if (baleno1.Substring(4, 3) != orgcd1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + " Bale Number  and Orginization MisMatch BaleNumber--" + baleno1;
                        }
                        else if (tbgrade1.Substring(0, 2) != crop1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TB Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                        }
                        else if (tbgrade1.Substring(2, 2) != variety1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TB Grade and Variety MisMatch BaleNumber--" + baleno1;
                        }
                        else if (buyergrade1.Substring(0, 2) != crop1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Buyer Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                        }
                        else if (buyergrade1.Substring(2, 2) != variety1)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Buyer Grade and Variety MisMatch BaleNumber--" + baleno1;
                        }

                        else if (baleno1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;

                        }
                        else if (tblot1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Lot Number is Empty for BaleNumber--" + baleno1;
                        }
                        else if (tbgrno1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TBGR number is Empty for BaleNumber--" + baleno1;
                        }
                        else if (tbgrade1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TB Grade is Empty for BaleNumber--" + baleno1;
                        }
                        else if (buyergrade1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Buyer Grade is Empty for BaleNumber--" + baleno1;
                        }
                        else if (netwt1 == "" || Convert.ToDouble(netwt1) == 0)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Weight is Empty for BaleNumber--" + baleno1;
                        }
                        else if (Convert.ToDouble(netwt1) > 150)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Weight is more than 150 for BaleNumber--" + baleno1;
                        }
                        else if (rate1 == "" || Convert.ToDouble(rate1) == 0)
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Rate is Empty for BaleNumber--" + baleno1;
                        }
                        else if (rejests1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Rejection Status is Empty for BaleNumber--" + baleno1;
                        }
                        else if (pattacharge1 == "")
                        {
                            update(baleno1, "N");
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Patta Charge is Empty for BaleNumber--" + baleno1;
                        }
                        else
                        {
                            dt = new DataTable();
                            dt = tpLoader.GetGrade(tbgrade1.Trim());
                           
                            if (dt.Rows.Count>0)
                            {
                                
                                dt = new DataTable();
                                dt = tpLoader.GetBalNumber(baleno1.Trim());


                                if (dt.Rows.Count>0)
                                {                                   
                                    update(baleno1, "N");
                                    i = i + 1;
                                    TAPerror = TAPerror + Environment.NewLine + "Bale Already Purchased BaleNumber--" + baleno1;
                                }
                                else
                                {
                                    dt = new DataTable();
                                    dt = tpLoader.GetGrade(buyergrade1.Trim());
                                 
                                    if (dt.Rows.Count>0)
                                    {
                                        update(baleno1, "Y");
                                    }
                                    else
                                    {
                                        update(baleno1, "N");
                                        i = i + 1;
                                        TAPerror = TAPerror + Environment.NewLine + "Buyer Grade Does not exit in master BaleNumber--" + baleno1;
                                    }

                                }

                            }
                            else
                            {
                                update(baleno1, "N");
                                i = i + 1;
                                TAPerror = TAPerror + Environment.NewLine + "TB Grade Does not exit in master BaleNumber--" + baleno1;
                            }
                        }
                    }

                }
                if (i == 0)
                {
                    TAPerror = TAPerror + " NO ERROR";
                    StringBuilder err = new StringBuilder();
                    err.Append(" Message : " + "Sucess while uploading the data");                    
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                    return true;
                }
                else
                {
                    StringBuilder err = new StringBuilder();
                    err.Append(" Message : " + "Error while uploading the data");
                    Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
                    return false;
                }




            }
            catch (Exception ex)
            {
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
               // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please check the uploading file');", true);
                return false;
            }
            finally
            {
            }
        }


        public void update(string gpilbaleno, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER ='" + gpilbaleno + "'");
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


                GridViewSample.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error";
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }
        protected void btndwnerr_Click(object sender, EventArgs e)
        {
            download(Server.MapPath("LOGFILES\\"), errfile);
        }
        public void download(string path, string filename)
        {
            try
            {
                FileStream fs = null;
                fs = File.Open(path + filename, FileMode.Open);
                byte[] btfile = new byte[fs.Length];
                fs.Read(btfile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(path + filename);
                Response.End();
                // HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error";
                StringBuilder err = new StringBuilder();
                err.Append(" Message : " + ex.Message);
                err.AppendLine(" STACK TRACE : " + ex.StackTrace);
                err.AppendLine(" INNER EXCEPTION : " + ex.InnerException);
                err.AppendLine(" SOURCE : " + ex.Source);
                Utils.LogError(err.ToString(), Utils.LogEntry.EXCEPTION);
            }
        }

      
    }
}