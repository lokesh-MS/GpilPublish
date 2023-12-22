using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using CrystalDecisions.ReportAppServer.DataDefModel;
using System.IO;
using System.Configuration;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{
    public partial class ScrapDegradation : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        string Crop, Variety, Production, Scrap, Date, RunNo, SampleTime, RunCaseNo;
        Double TotalSampleWeight, HeldOver5, HeldOver5_2, HeldOver10, HeldOver10_2;
        Double HeldOver20, HeldOver20_2, Pan, Pan_2, SampleWeight, FiberGrams, FiberPercent;
        public static string errfile;
        DataTable objDataTable = new DataTable();
        //DataTable objDataTable = new DataTable();
        public static DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binding();
                if (dt.Columns.Count == 0)
                {
                    //dt.Columns.Add("Date", typeof(string));
                    //1-19 rows
                    dt.Columns.Add("CROP", typeof(string));
                    dt.Columns.Add("Variety", typeof(string));
                    dt.Columns.Add("Grade", typeof(string));
                    dt.Columns.Add("RunNo", typeof(Int32));
                    dt.Columns.Add("Time", typeof(string));
                    dt.Columns.Add("Date", typeof(string));
                    dt.Columns.Add("Production", typeof(string));
                    dt.Columns.Add("CaseNo", typeof(Int32));
                    dt.Columns.Add("HeldOver5", typeof(double));
                    dt.Columns.Add("HeldOver10", typeof(double));
                    dt.Columns.Add("HeldOver20", typeof(double));
                    dt.Columns.Add("TotalSampleWeight", typeof(double));
                    dt.Columns.Add("PanGrams", typeof(double));
                    dt.Columns.Add("FiberGrams", typeof(string));
                    dt.Columns.Add("HeldOver5Percent", typeof(double));
                    dt.Columns.Add("HeldOver10Percent", typeof(double));
                    dt.Columns.Add("HeldOver20Percent", typeof(double));
                    dt.Columns.Add("PanPercent", typeof(double));
                    dt.Columns.Add("FiberPercent", typeof(double));
                    dt.Columns.Add("SampleWeight", typeof(double));
                    //rbManual.Checked = true;
                }
                this.RBCheck();

            }
        }
        public void binding()
        {
            string query;
            SqlDataAdapter sda;

            // Bank ID ddl 
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                // string query;
                DataTable dt = new DataTable();

                query = "SELECT[CROP_YEAR] FROM [dbo].[GPIL_CROP_MASTER]";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                sda.Dispose();
                ddCrop.DataSource = dt;
                ddCrop.DataBind();
                ddCrop.DataTextField = "CROP_YEAR";
                ddCrop.DataBind();


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
            //Variety
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                //SqlDataAdapter sda;
                DataTable dt = new DataTable();

                query = "SELECT [VARIETY_TYPE]  FROM [dbo].[GPIL_VARIETY_MASTER]";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                sda.Dispose();
                ddVariety.DataSource = dt;
                ddVariety.DataBind();
                ddVariety.DataTextField = "VARIETY_TYPE";
                ddVariety.DataBind();


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
            //Product

            // try
            // {
            //     if (con.State != ConnectionState.Open)
            //     {
            //         con.Open();
            //     }


            //     string query;
            //     SqlDataAdapter sda;
            //     DataTable dt = new DataTable();

            //     query = "SELECT [VARIETY_TYPE]  FROM [dbo].[GPIL_VARIETY_MASTER]";
            //     sda = new SqlDataAdapter(query, con);
            //     sda.Fill(dt);
            //     ddProduction.DataSource = dt;
            //     ddProduction.DataBind();
            //     ddProduction.DataTextField = "VARIETY_TYPE";
            //     ddProduction.DataBind();


            // }
            // catch (Exception ex)
            // {
            //     lblMessage.Text = ex.ToString();
            // }

            ///Scrap grade
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                query = "SELECT [ITEM_CODE] FROM [dbo].[GPIL_ITEM_MASTER] where [ITEM_CODE] like 'L%'";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                ddScrapGrade.DataSource = dt;
                ddScrapGrade.DataBind();
                sda.Dispose();
                ddScrapGrade.DataTextField = "ITEM_CODE";
                ddScrapGrade.DataBind();
                ddScrapGrade.DataBind();
                ddScrapGrade.Items.Insert(0, new ListItem("---Select---", "0"));

                ddProductionFrom.DataSource = dt;
                ddProductionFrom.DataBind();
                ddProductionFrom.DataTextField = "ITEM_CODE";
                ddProductionFrom.DataBind();
                ddProductionFrom.DataBind();
                ddProductionFrom.Items.Insert(0, new ListItem("---Select---", "0"));

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsData();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmHomePage.aspx");
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            dt.Clear();
            int k = 0;
            //if (rbImport.Checked == true)
            //{
            //    try
            //    {
            //        if (con.State != ConnectionState.Open)
            //        {
            //            con.Open();
            //        }

            //        long j;
            //        // if (!IsPostBack)
            //        // {
            //        //string DOP, Crop, Variety, Grade, Mark, Sourceorg, TypeS, Product, DomExp, Type, FromRunNo, ToRunNo, NIC, TRS, CL, Moisture;
            //        string serpath = string.Concat(Server.MapPath("~/TempFiles/"), FileUpload01.FileName);
            //        OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + serpath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
            //        //Save File as Temp then you can delete it if you want
            //        DataTable asw = new DataTable();
            //        //  FileUpload.SaveAs(serpath);
            //        FileUpload01.SaveAs(serpath);

            //        string queryExcel = "SELECT * from [Sheet1$]";
            //        try
            //        {
            //            //SqlTransaction tran = null;
            //            OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter(queryExcel, oconn);
            //            objDataTable.Clear();
            //            objOleDbDataAdapter.Fill(objDataTable);
            //            //tran = con.BeginTransaction();
            //            try
            //            {
            //                for (int d = 0; d < objDataTable.Rows.Count; d++)
            //                {
            //                    Crop = objDataTable.Rows[d][0].ToString();
            //                    Variety = objDataTable.Rows[d][1].ToString();
            //                    Scrap = objDataTable.Rows[d][2].ToString();
            //                    Production = objDataTable.Rows[d][3].ToString();
            //                    Date = Convert.ToDateTime(objDataTable.Rows[d][4]).ToString("yyyy-MM-dd hh:mm:ss");
            //                    RunNo = objDataTable.Rows[d][5].ToString();
            //                    SampleTime = Convert.ToDateTime(objDataTable.Rows[d][6].ToString()).ToShortTimeString(); ;
            //                    RunCaseNo = objDataTable.Rows[d][7].ToString();
            //                    HeldOver5 = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][8].ToString()), 2);
            //                    HeldOver10 = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][9].ToString()), 2);
            //                    HeldOver20 = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][10].ToString()), 2);
            //                    Pan = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][11].ToString()), 2);
            //                    SampleWeight = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][12].ToString()), 2);
            //                    FiberGrams = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][13].ToString()), 2);
            //                    i = 2;

            //                    if (Crop != null)
            //                    {
            //                        Calculations();
            //                        try
            //                        {
            //                            if (con.State != ConnectionState.Open)
            //                            {
            //                                con.Open();
            //                            }
            //                            int o = 0;
            //                            string querycheck1 = "SELECT Count(*) FROM [dbo].[GPIL_CROP_MASTER] where [CROP_YEAR]='" + Crop + "'  union ";
            //                            querycheck1 += "SELECT Count(*) FROM [dbo].[GPIL_VARIETY_MASTER] where [VARIETY_TYPE]='" + Variety + "'"; //union ";
            //                                                                                                                                      //querycheck1 += "SELECT Count(*) FROM [dbo].[GPIL_ITEM_MASTER] where [ITEM_CODE]='" + Grade + "'";
            //                            SqlCommand sqlCheck01 = new SqlCommand(querycheck1, con);
            //                            //sqlCheck01.Transaction = tran;
            //                            o = Convert.ToInt32(sqlCheck01.ExecuteScalar());
            //                            sqlCheck01.Dispose();

            //                            if (0 != 1)
            //                            {
            //                                string querycheck = "Select Count(*)   from [dbo].[GPIL_ScrapDegradation] where [Crop]='" + Crop + "' And [Variety]='" + Variety + "' and[Production]='" + Production + "' and [ScrapGrade]='" + Scrap + "' and[ScrapDate]='" + Date + "'";
            //                                SqlCommand sqlcmdcheck = new SqlCommand(querycheck, con);
            //                                // sqlcmdcheck.Transaction = tran;
            //                                j = Convert.ToInt32(sqlcmdcheck.ExecuteScalar());
            //                                sqlcmdcheck.Dispose();
            //                                if (j == 0)
            //                                {
            //                                    if ((System.Math.Round(Convert.ToDouble(HeldOver5_2) + Convert.ToDouble(HeldOver10_2) + Convert.ToDouble(HeldOver20_2) + Convert.ToDouble(Pan_2), 0)) == 100)
            //                                    {
            //                                        k = 1;
            //                                    }
            //                                    DataRow NewRow = dt.NewRow();
            //                                    NewRow[0] = Crop;
            //                                    NewRow[1] = Variety;
            //                                    NewRow[2] = Scrap;
            //                                    NewRow[3] = Convert.ToInt32(RunNo);
            //                                    NewRow[4] = SampleTime;
            //                                    NewRow[5] = Date;
            //                                    NewRow[6] = Production;
            //                                    NewRow[7] = Convert.ToInt32(RunCaseNo);
            //                                    NewRow[8] = Convert.ToDouble(HeldOver5);
            //                                    NewRow[9] = Convert.ToDouble(HeldOver10);
            //                                    NewRow[10] = Convert.ToDouble(HeldOver20);
            //                                    NewRow[11] = Convert.ToDouble(TotalSampleWeight);
            //                                    NewRow[12] = Convert.ToDouble(Pan);
            //                                    NewRow[13] = Convert.ToDouble(FiberGrams);
            //                                    NewRow[14] = Convert.ToDouble(HeldOver5_2);
            //                                    NewRow[15] = Convert.ToDouble(HeldOver10_2);
            //                                    NewRow[16] = Convert.ToDouble(HeldOver20_2);
            //                                    NewRow[17] = Convert.ToDouble(Pan_2);
            //                                    NewRow[18] = Convert.ToDouble(FiberPercent);
            //                                    NewRow[19] = Convert.ToDouble(SampleWeight);

            //                                    dt.Rows.Add(NewRow);
            //                                    // Alert.Show(dt.Rows.Count.ToString());
            //                                    this.GridViewSample.Visible = true;
            //                                    GridViewSample.DataSource = dt;
            //                                    GridViewSample.DataBind();
            //                                    GridViewSample.EditIndex = -1;


            //                                }
            //                                else
            //                                {
            //                                    lblMessage.Visible = true;
            //                                    lblMessage.Text = "The value already exists";
            //                                    k = 1;
            //                                }

            //                            }
            //                            else
            //                            {
            //                                lblMessage.Visible = true;
            //                                lblMessage.Text = "The Crop variety or grade is incorrect at line: " + (d + 2) + "";
            //                                k = 1;
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            lblMessage.Visible = true;
            //                            lblMessage.Text = ex.ToString() + "a";
            //                            k = 1;
            //                        }
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                lblMessage.Visible = true;
            //                lblMessage.Text = ex.ToString() + "b";
            //                k = 1;
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            lblMessage.Visible = true;
            //            lblMessage.Text = ex.ToString() + "c";
            //            k = 1;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        lblMessage.Visible = true;
            //        lblMessage.Text = ex.ToString();
            //        k = 1;

            //    }
            //}

            if (FileUpload01.HasFile)
            {
                string filePath = string.Empty;
                string path = Server.MapPath("~/ExcelUploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(FileUpload01.FileName);
                string extension = Path.GetExtension(FileUpload01.FileName);
                FileUpload01.SaveAs(filePath);
                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //For Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //For Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                try
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    conString = string.Format(conString, filePath);
                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;
                                //Get the name of First Sheet.
                                connExcel.Open();
                                System.Data.DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();
                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);

                                GridViewSample.DataSource = dt;
                                GridViewSample.DataBind();

                                connExcel.Close();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }

            }
            if (k == 0)
            {
                btnsaveG.Enabled = true;
            }
            else
            {
                btnsaveG.Enabled = false;
            }
        }
        int i = 0;
        protected void txtPanGrams_TextChanged(object sender, EventArgs e)
        {
            try
            {

                HeldOver5 = (Convert.ToDouble(txtHeadOver5.Text));
                HeldOver10 = (Convert.ToDouble(txtHeadOver10.Text));
                HeldOver20 = (Convert.ToDouble(txtHeadOver20.Text));
                Pan = (Convert.ToDouble(txtPanGrams.Text));
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }

            i = 0;
            Calculations();
            txtTotalSampleWeight.Text = TotalSampleWeight.ToString();
            txtHeldOver5_2.Text = HeldOver5_2.ToString();
            txtHeldOver10_2.Text = HeldOver10_2.ToString();
            txtheldOver20_2.Text = HeldOver20_2.ToString();
            txtPan_2.Text = Pan_2.ToString();


            // Calculation Part done under the calc methods!!
            //txtTotalSampleWeight.Text = Convert.ToString(Convert.ToDouble(txtHeadOver5.Text) + Convert.ToDouble(txtHeadOver10.Text) + Convert.ToDouble(txtHeadOver20.Text) + Convert.ToDouble(txtPanGrams.Text));
            //txtHeldOver5_2.Text=Convert.ToString((Convert.ToDouble(txtHeadOver5.Text)/Convert.ToDouble(txtTotalSampleWeight.Text))*100);
            //txtHeldOver10_2.Text = Convert.ToString((Convert.ToDouble(txtHeadOver10.Text) / Convert.ToDouble(txtTotalSampleWeight.Text)) * 100);
            //txtheldOver20_2.Text = Convert.ToString((Convert.ToDouble(txtHeadOver20.Text) / Convert.ToDouble(txtTotalSampleWeight.Text)) * 100);
            //txtPan_2.Text = Convert.ToString((Convert.ToDouble(txtPanGrams.Text) / Convert.ToDouble(txtTotalSampleWeight.Text)) * 100);
        }
        public void Calculations()
        {
            try
            {
                if (i == 0 || i == 2)
                {
                    TotalSampleWeight = System.Math.Round((HeldOver5 + HeldOver10 + HeldOver20 + Pan), 2);
                    HeldOver5_2 = System.Math.Round(((HeldOver5 / TotalSampleWeight) * 100), 2);
                    HeldOver10_2 = System.Math.Round(((HeldOver10 / TotalSampleWeight) * 100), 2);
                    HeldOver20_2 = System.Math.Round(((HeldOver20 / TotalSampleWeight) * 100), 2);
                    Pan_2 = System.Math.Round(((Pan / TotalSampleWeight) * 100), 2);
                }
                if (i == 1 || i == 2)
                {
                    FiberPercent = System.Math.Round(((FiberGrams / SampleWeight) * 100), 2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }
        }
        protected void txtFiber_TextChanged(object sender, EventArgs e)
        {
            SampleWeight = Convert.ToDouble(txtSampleWeight.Text);
            FiberGrams = Convert.ToDouble(txtFiber.Text);
            i = 1;
            Calculations();
            txtFiber_2.Text = FiberPercent.ToString();
            Label35.Text = Convert.ToString(Convert.ToDouble(txtHeldOver5_2.Text) + Convert.ToDouble(txtHeldOver10_2.Text) + Convert.ToDouble(txtheldOver20_2.Text) + Convert.ToDouble(txtPan_2.Text));
            if (System.Math.Round((Convert.ToDouble(Label35.Text)), 0) == 100)
            {
                btnSave.Enabled = true;
            }
            else
            {
                //Alert.Show("Added Percentage values not equal to 100");
                string script = "alert('Added Percentage values not equal to 100');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                btnSave.Enabled = false;
            }
        }
        public void ClearControls()
        {
            txtEntryDate.Text = "";
            txtFiber.Text = "";
            txtFiber_2.Text = "";
            txtHeadOver10.Text = "";
            txtHeadOver20.Text = "";
            txtHeadOver5.Text = "";
            txtHeldOver10_2.Text = "";
            txtheldOver20_2.Text = "";
            txtHeldOver5_2.Text = "";
            txtPan_2.Text = "";
            txtPanGrams.Text = "";
            //txtProductionfrom.Text = "";
            ddProductionFrom.SelectedIndex = 0;
            txtRunCaseNo.Text = "";
            txtRUnNo.Text = "";
            txtSampleWeight.Text = "";
            ddScrapGrade.SelectedIndex = 0;
            // txtScrapGrade.Text = "";
            txtTime.Text = "";
            txtTotalSampleWeight.Text = "";
            ddCrop.SelectedIndex = 0;
            ddVariety.SelectedIndex = 0;
        }
        int j = 0;
        public void InsData()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string querycheck = "select Count(*)   from [dbo].[GPIL_ScrapDegradation] where [Crop]='" + ddCrop.SelectedItem.ToString() + "' And [Variety]='" + ddVariety.SelectedItem.ToString() + "' and[Production]='" + ddProductionFrom.SelectedItem.ToString() + "' and[ScrapGrade]='" + ddScrapGrade.SelectedItem.ToString() + "' and[ScrapDate]='" + txtEntryDate.Text + "'";
                SqlCommand sqlcmdcheck = new SqlCommand(querycheck, con);
                j = Convert.ToInt32(sqlcmdcheck.ExecuteScalar());
                sqlcmdcheck.Dispose();
                if (j == 0)
                {
                    string queryIns = "INSERT INTO [dbo].[GPIL_ScrapDegradation] ([Crop],[Variety],[ScrapGrade],[Production],[ScrapDate],[RunNo],[SampleTime],[RunCaseNo],[TotalSampleWeight],[Held5],[Held5Percent],[Held10],[Held10Percent],[Held20],[Held20Percent],[Pan] ,[PanPercent],[SampleWeight],[FiberGrams],[FiberPercent])";
                    queryIns += "  VALUES('" + ddCrop.SelectedItem.ToString() + "','" + ddVariety.SelectedItem.ToString() + "','" + ddProductionFrom.SelectedItem.ToString() + "','" + ddScrapGrade.SelectedItem.ToString() + "','" + txtEntryDate.Text + "'," + Convert.ToInt32(txtRUnNo.Text) + ",'" + txtTime.Text + "'," + Convert.ToInt32(txtRunCaseNo.Text) + "";
                    queryIns += "," + Convert.ToDouble(txtTotalSampleWeight.Text) + "," + Convert.ToDouble(txtHeadOver5.Text) + "," + Convert.ToDouble(txtHeldOver5_2.Text) + "," + Convert.ToDouble(txtHeadOver10.Text) + "," + Convert.ToDouble(txtHeldOver10_2.Text) + "," + Convert.ToDouble(txtHeadOver20.Text) + "," + Convert.ToDouble(txtheldOver20_2.Text) + "," + Convert.ToDouble(txtPanGrams.Text) + "," + Convert.ToDouble(txtPan_2.Text) + "," + Convert.ToDouble(txtSampleWeight.Text) + "," + Convert.ToDouble(txtFiber.Text) + "," + Convert.ToDouble(txtFiber_2.Text) + ")";
                    SqlCommand sqlcmd = new SqlCommand(queryIns, con);
                    sqlcmd.ExecuteScalar();
                    sqlcmd.Dispose();
                    lblMessage.Visible = true;
                    lblMessage.Text = "Updated";
                    ClearControls();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "The value already exists";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }
        }
        protected void txtScrapGrade_TextChanged(object sender, EventArgs e)
        {

        }
        protected void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManual.Checked == true)
            {
                z = 1;

            }
            else if (rbImport.Checked == true)
            {
                z = 2;

            }

            this.RBCheck();
        }
        protected void btnsaveG_Click1(object sender, EventArgs e)
        {
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                tran = con.BeginTransaction();
                for (int i = 0; i < GridViewSample.Rows.Count; i++)
                {
                    var tim = GridViewSample.Rows[i].FindControl("lblTime");
                    string queryIns = "INSERT INTO [dbo].[GPIL_ScrapDegradation] ([Crop],[Variety],[ScrapGrade],[Production],[ScrapDate],[RunNo],[SampleTime],[RunCaseNo],[TotalSampleWeight],[Held5],[Held5Percent],[Held10],[Held10Percent],[Held20],[Held20Percent],[Pan] ,[PanPercent],[SampleWeight],[FiberGrams],[FiberPercent])";
                    queryIns += "  VALUES('" + ((Label)GridViewSample.Rows[i].FindControl("lblCrop")).Text + "','" + ((Label)GridViewSample.Rows[i].FindControl("lblVariety")).Text + "','" + ((Label)GridViewSample.Rows[i].FindControl("lblGrade")).Text + "','" + ((Label)GridViewSample.Rows[i].FindControl("lblProduction")).Text + "',";
                    //queryIns += "(select convert(datetime,'" + ((Label)GridViewSample.Rows[i].FindControl("lblDate")).Text + "',103))," + ((Label)GridViewSample.Rows[i].FindControl("lblRunNo")).Text + ",'" + ((Label)GridViewSample.Rows[i].FindControl("lblTime")).Text + "'," + Convert.ToInt32(((Label)GridViewSample.Rows[i].FindControl("lblCaseNo")).Text) + ",";
                    queryIns += "(select convert(datetime,'" + ((Label)GridViewSample.Rows[i].FindControl("lblDate")).Text + "',3))," + ((Label)GridViewSample.Rows[i].FindControl("lblRunNo")).Text + ",";
                    queryIns += "(select convert(datetime,'" + ((Label)GridViewSample.Rows[i].FindControl("lblTime")).Text + "',3))," + Convert.ToInt32(((Label)GridViewSample.Rows[i].FindControl("lblCaseNo")).Text) + ",";
                    queryIns += "" + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblTotalSampleWeight")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblHeldOver5")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblHeldOver5Percent")).Text) + ",";
                    queryIns += "" + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblHeldOver10")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblHeldOver10Percent")).Text) + "";
                    queryIns += "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblHeldOver20")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblHeldOver20Percent")).Text) + "";
                    queryIns += "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblPanGrams")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblPanPercent")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblSampleWeight")).Text) + ",";
                    queryIns += "" + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblFiberGrams")).Text) + "," + Convert.ToDouble(((Label)GridViewSample.Rows[i].FindControl("lblFiberPercent")).Text) + ")";
                    SqlCommand sqlcmd = new SqlCommand(queryIns, con);
                    sqlcmd.Transaction = tran;
                    sqlcmd.ExecuteScalar();
                    sqlcmd.Dispose();
                    lblMessage.Visible = true;
                }
                tran.Commit();
                GridViewSample.DataSource = null;
                GridViewSample.DataBind();
                //Alert.Show("Updated!!");
                string script = "alert('Updated');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                lblMessage.Text = ex.ToString();
                lblMessage.Visible = true;
            }
        }
        int z;
        protected void btnsaveG_Click(object sender, EventArgs e)
        {

        }
        protected void rbImport_CheckedChanged(object sender, EventArgs e)
        {

            if (rbManual.Checked == true)
            {
                z = 1;

            }
            else if (rbImport.Checked == true)
            {
                z = 2;

            }

            this.RBCheck();
            //RadioButton2.Checked = false;
        }
        bool str02;
        bool str01;
        public void RBCheck()
        {

            if (z == 1)
            {
                str01 = false;
                str02 = true;
            }
            else if (z == 2)
            {

                str01 = true;
                str02 = false;
            }
            else
            {
                str01 = false;
                str02 = false;
            }




            lblDate0.Visible = str02;
            ddCrop.Visible = str02;
            lblOver13.Visible = str02;
            txtEntryDate.Visible = str02;
            Label19.Visible = str02;
            Label29.Visible = str02;
            lblDate.Visible = str02;
            Label20.Visible = str02;
            ddVariety.Visible = str02;
            lblOver12.Visible = str02;
            Label30.Visible = str02;
            txtRUnNo.Visible = str02;
            Label5.Visible = str02;
            Label21.Visible = str02;
            ddScrapGrade.Visible = str02;
            Label2.Visible = str02;
            Label31.Visible = str02;
            txtTime.Visible = str02;
            Label1.Visible = str02;
            Label22.Visible = str02;
            ddProductionFrom.Visible = str02;
            txtRunCaseNo.Visible = str02;
            Label6.Visible = str02;
            Label32.Visible = str02;
            txtHeadOver5.Visible = str02;
            Label7.Visible = str02;
            Label23.Visible = str02;
            txtHeadOver10.Visible = str02;
            Label8.Visible = str02;
            Label33.Visible = str02;
            txtHeadOver20.Visible = str02;
            Label9.Visible = str02;
            Label24.Visible = str02;
            txtSampleWeight.Visible = str02;
            Label28.Visible = str02;
            Label14.Visible = str02;
            txtPanGrams.Visible = str02;
            Label13.Visible = str02;
            Label25.Visible = str02;
            txtFiber.Visible = str02;
            Label4.Visible = str02;
            Label34.Visible = str02;
            txtHeldOver5_2.Visible = str02;
            nic.Visible = str02;
            txtHeldOver10_2.Visible = str02;
            Label16.Visible = str02;
            txtheldOver20_2.Visible = str02;
            Label17.Visible = str02;
            txtPan_2.Visible = str02;
            Label18.Visible = str02;
            txtTotalSampleWeight.Visible = str02;
            Label3.Visible = str02;
            txtFiber_2.Visible = str02;
            Label11.Visible = str02;
            btnSave.Visible = str02;

            //excel
            Label12.Visible = str01;
            FileUpload01.Visible = str01;
            btnImport.Visible = str01;
            GridViewSample.Visible = str01;
            btnsaveG.Visible = str01;


        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

    }
}
