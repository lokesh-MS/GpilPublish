using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Data;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.GLT;
using System.Configuration;
using GPIWebApp;

namespace GPILWebApp.CrystalReport.WebForms.GLT
{
    public partial class StemDegradation : System.Web.UI.Page
    {

        DataServerSync dataConnection = new DataServerSync();

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        string Crop, Variety, LamiaGrade, Grade, Date, TimeStem;
        int CaseNo;
        double TotalLength;
        int i = 0; int k = 0;
        double AvgStemLength, StemWeight, L3_32, NoofStemPieces, L3_32Percent, G3_32, G3_32Percent, SandnDust, SandnDustPercent, NoofTotalStemPieces, NoofL1_2StemPieces;
        double L1_2StemPiecesPercent, NoofG1_2L4StemPieces, G1_2to4StemPiecesPercent, NoofG4StemPieces, G4StemPiecesPercent;
        public static string errfile;
        public static DataTable dt = new DataTable();
        DataTable objDataTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Binding();
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Crop", typeof(string));
                        dt.Columns.Add("Variety", typeof(string));
                        dt.Columns.Add("LamiaGrade", typeof(string));
                        dt.Columns.Add("Grade", typeof(string));
                        dt.Columns.Add("Date", typeof(string));
                        dt.Columns.Add("CaseNo", typeof(Int32));
                        dt.Columns.Add("TimeStem", typeof(string));
                        dt.Columns.Add("TotalLength", typeof(double));
                        dt.Columns.Add("NoofStemPieces", typeof(double));
                        dt.Columns.Add("AvgStemLength", typeof(double));
                        dt.Columns.Add("StemWeight", typeof(double));
                        dt.Columns.Add("G3_32", typeof(double));
                        dt.Columns.Add("G3_32Percent", typeof(double));
                        dt.Columns.Add("L3_32", typeof(double));
                        dt.Columns.Add("L3_32Percent", typeof(double));
                        dt.Columns.Add("SandnDust", typeof(double));
                        dt.Columns.Add("SandnDustPercent", typeof(double));
                        dt.Columns.Add("NoofTotalStemPieces", typeof(double));
                        dt.Columns.Add("NoofL1_2StemPieces", typeof(double));
                        dt.Columns.Add("L1_2StemPiecesPercent", typeof(double));
                        dt.Columns.Add("NoofG1_2L4StemPieces", typeof(double));
                        dt.Columns.Add("G1_2to4StemPiecesPercent", typeof(double));
                        dt.Columns.Add("NoofG4StemPieces", typeof(double));
                        dt.Columns.Add("G4StemPiecesPercent", typeof(double));
                        dt.Columns.Add("SUM", typeof(string));
                    }
                    this.RBCheck();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "alert('" + ex.Message + "');", true);
                }
            }

        }
        public void Binding()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            //Crop
            try
            {


                string query;
                SqlDataAdapter sda;
                DataTable dt = new DataTable();

                query = "SELECT[CROP_YEAR] FROM [dbo].[GPIL_CROP_MASTER]";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                ddCrop.DataSource = dt;
                ddCrop.DataBind();
                ddCrop.DataTextField = "CROP_YEAR";
                ddCrop.DataBind();
                ddCrop.DataBind();
                ddCrop.Items.Insert(0, new ListItem("---Select---", "0"));
                sda.Dispose();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }


            //Variety
            try
            {

                string query01;
                SqlDataAdapter sda01;
                DataTable dt = new DataTable();

                query01 = "SELECT [VARIETY_TYPE]  FROM [dbo].[GPIL_VARIETY_MASTER]";
                sda01 = new SqlDataAdapter(query01, con);
                sda01.Fill(dt);
                ddVariety.DataSource = dt;
                ddVariety.DataBind();
                ddVariety.DataTextField = "VARIETY_TYPE";
                ddVariety.DataBind();
                ddVariety.DataBind();
                ddVariety.Items.Insert(0, new ListItem("---Select---", "0"));
                sda01.Dispose();



            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
            //Grade
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                string query;
                SqlDataAdapter sda;
                DataTable dt = new DataTable();

                query = "SELECT [ITEM_CODE] FROM [dbo].[GPIL_ITEM_MASTER] where [item_code] like 'L%'";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                ddGrade.DataSource = dt;
                ddGrade.DataBind();
                ddGrade.DataTextField = "ITEM_CODE";
                ddGrade.DataBind();
                ddGrade.DataBind();
                ddGrade.Items.Insert(0, new ListItem("---Select---", "0"));
                sda.Dispose();



            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
            //lamigrade 
            try
            {


                string query;
                SqlDataAdapter sda;
                DataTable dt = new DataTable();
                query = "SELECT [ITEM_CODE] FROM [GPIL_ITEM_MASTER] where ATTRIBUTE2 in ('FGD','FGE')";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                ddLamiaGrade.DataSource = dt;
                ddLamiaGrade.DataBind();
                ddLamiaGrade.DataTextField = "ITEM_CODE";
                ddLamiaGrade.DataBind();
                ddLamiaGrade.DataBind();
                ddLamiaGrade.Items.Insert(0, new ListItem("---Select---", "0"));
                sda.Dispose();

            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }

        }

        //DataTable objDataTable = new DataTable();
        double Tot100 = 0.0;
        protected void btnImport_Click(object sender, EventArgs e)
        {

            dt.Clear();
            Button1.Enabled = true;
            int k = 0;
            //if (RadioButton1.Checked == true)
            //{
            //    try
            //    {
            //        double sumval;
            //        int o = 0;

            //        long j;
            //        // if (!IsPostBack)
            //        //  
            //        string serpath = string.Concat(Server.MapPath("~/TempFiles/"), FileUpload01.FileName);
            //        OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + serpath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
            //        //Save File as Temp then you can delete it if you want
            //        DataTable asw = new DataTable();
            //        //  FileUpload.SaveAs(serpath);
            //        FileUpload01.SaveAs(serpath);



            //        string queryExcel = "SELECT * from [Sheet1$]";
            //        try
            //        {
            //            //SqlTransaction tran = null;//= new SqlTransaction();

            //            OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter(queryExcel, oconn);
            //            objDataTable.Clear();
            //            objOleDbDataAdapter.Fill(objDataTable);
            //            // tran = con.BeginTransaction();
            //            try
            //            {
            //                for (int d = 0; d < objDataTable.Rows.Count; d++)
            //                {
            //                    sumval = 0;
            //                    if (objDataTable.Rows[d][0].ToString() != "" && objDataTable.Rows[d][1].ToString() != "")
            //                    {
            //                        Crop = objDataTable.Rows[d][0].ToString();
            //                        Variety = objDataTable.Rows[d][1].ToString();
            //                        LamiaGrade = objDataTable.Rows[d][2].ToString();
            //                        Grade = objDataTable.Rows[d][3].ToString();
            //                        Date = Convert.ToDateTime(objDataTable.Rows[d][4]).ToString("yyyy-MM-dd hh:mm:ss");
            //                        TimeStem = objDataTable.Rows[d][5].ToString();
            //                        //AVE1 = objDataTable.Rows[d][5].ToString();
            //                        CaseNo = Convert.ToInt32(objDataTable.Rows[d][6].ToString());
            //                        TotalLength = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][7].ToString()), 2);
            //                        //AVE2 = objDataTable.Rows[d][8].ToString();
            //                        NoofStemPieces = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][8].ToString()), 2);
            //                        //AvgStemLength = Convert.ToDouble(objDataTable.Rows[d][9].ToString());
            //                        G3_32 = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][9].ToString()), 2);
            //                        L3_32 = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][10].ToString()), 2);
            //                        SandnDust = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][11].ToString()), 2);
            //                        NoofL1_2StemPieces = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][12].ToString()), 2);
            //                        NoofG4StemPieces = System.Math.Round(Convert.ToDouble(objDataTable.Rows[d][13].ToString()), 2);



            //                        string querycheck1 = "SELECT Count(*) FROM [dbo].[GPIL_CROP_MASTER] where [CROP_YEAR]='" + Crop + "'  union ";
            //                        querycheck1 += "SELECT Count(*) FROM [dbo].[GPIL_VARIETY_MASTER] where [VARIETY_TYPE]='" + Variety + "' union ";
            //                        querycheck1 += "SELECT Count(*) FROM [dbo].[GPIL_ITEM_MASTER] where [ITEM_CODE]='" + Grade + "'";
            //                        SqlCommand sqlCheck01 = new SqlCommand(querycheck1, con);
            //                        //sqlCheck01.Transaction = tran;
            //                        o = Convert.ToInt32(sqlCheck01.ExecuteScalar());
            //                        sqlCheck01.Dispose();

            //                        if (0 != 1)
            //                        {
            //                            string querycheck = "SELECT COUNT(*) FROM [dbo].[GPIL_StemReports] where  [Crop]='" + Crop + "' and [Variety]='" + Variety + "' and [LamiaGrade]='" + LamiaGrade + "' and [Grade]='" + Grade + "' and [CaseNo]='" + CaseNo + "'";
            //                            SqlCommand sqlCheck = new SqlCommand(querycheck, con);
            //                            //sqlCheck.Transaction = tran;
            //                            j = Convert.ToInt64(sqlCheck.ExecuteScalar());
            //                            if (j == 0)
            //                            {
            //                                i = 5;
            //                                TotalCalc();
            //                                sumval = G3_32Percent + L3_32Percent + SandnDustPercent;
            //                                if ((System.Math.Round((L1_2StemPiecesPercent + G1_2to4StemPiecesPercent + G4StemPiecesPercent), 0)) != 100)
            //                                {
            //                                    k = 1;
            //                                }

            //                                ///Insert into table command
            //                                DataRow NewRow = dt.NewRow();
            //                                NewRow[0] = Crop;
            //                                NewRow[1] = Variety;
            //                                NewRow[2] = LamiaGrade;
            //                                NewRow[3] = Grade;
            //                                NewRow[4] = Date;
            //                                NewRow[5] = CaseNo;
            //                                NewRow[6] = TimeStem;
            //                                NewRow[7] = TotalLength;
            //                                NewRow[8] = NoofStemPieces;
            //                                NewRow[9] = AvgStemLength;
            //                                NewRow[10] = StemWeight;
            //                                NewRow[11] = G3_32;
            //                                NewRow[12] = G3_32Percent;
            //                                NewRow[13] = L3_32;
            //                                NewRow[14] = L3_32Percent;
            //                                NewRow[15] = SandnDust;
            //                                NewRow[16] = SandnDustPercent;
            //                                NewRow[17] = NoofTotalStemPieces;
            //                                NewRow[18] = NoofL1_2StemPieces;
            //                                NewRow[19] = L1_2StemPiecesPercent;
            //                                NewRow[20] = NoofG1_2L4StemPieces;
            //                                NewRow[21] = G1_2to4StemPiecesPercent;
            //                                NewRow[22] = NoofG4StemPieces;
            //                                NewRow[23] = G4StemPiecesPercent;
            //                                NewRow[24] = Convert.ToString(sumval);
            //                                sumval = System.Math.Round(sumval, 0);
            //                                if (sumval != 100.00)
            //                                {
            //                                    Button1.Enabled = false;
            //                                }

            //                                //k = 0;   
            //                                Tot100 = Convert.ToDouble(G4StemPiecesPercent) + Convert.ToDouble(G1_2to4StemPiecesPercent) + Convert.ToDouble(L1_2StemPiecesPercent);
            //                                //if (Tot100 != 100)
            //                                //{
            //                                //    Button1.Enabled = false;
            //                                //}
            //                                dt.Rows.Add(NewRow);
            //                                // Alert.Show(dt.Rows.Count.ToString());
            //                                this.GridViewSample.Visible = true;
            //                                GridViewSample.DataSource = dt;
            //                                GridViewSample.DataBind();
            //                                GridViewSample.EditIndex = -1;
            //                            }
            //                            else
            //                            {
            //                                k = 1;

            //                            }
            //                        }
            //                        else
            //                        {
            //                            k = 1;
            //                        }
            //                    }

            //                }

            //            }
            //            catch (Exception ex)
            //            {
            //                //  Alert.Show(ex.ToString());
            //                string script = "alert('" + ex.ToString() + "');";
            //                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", script, true);
            //                k = 1;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            //Alert.Show(ex.ToString());
            //            string script = "alert('" + ex.ToString() + "');";
            //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", script, true);
            //            k = 1;

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMessage.Visible = true;
            //        lblMessage.Text = ex.ToString();
            //        k = 1;
            //    }
            //    // }}
            //    if (k == 1)
            //    {
            //        Button1.Enabled = false;
            //        lblMessage.Text = "Please check the values!!!";
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Crop = ddCrop.SelectedItem.ToString();
            Variety = ddVariety.SelectedItem.ToString();
            LamiaGrade = ddLamiaGrade.SelectedItem.ToString();
            Grade = ddGrade.SelectedItem.ToString();
            Date = Convert.ToString(txtDate.Text);
            TimeStem = Convert.ToString(txtTime.Text);
            CaseNo = Convert.ToInt32(txtCaseNo.Text);
            TotalLength = Convert.ToInt32(txtTotalLength.Text);
            NoofStemPieces = Convert.ToInt32(txtStemPieces.Text);
            AvgStemLength = Convert.ToDouble(txtAvgStemLength.Text);
            StemWeight = Convert.ToDouble(txtStemWeight.Text);
            G3_32 = Convert.ToDouble(txt3_32.Text);
            G3_32Percent = Convert.ToDouble(txt3_32Percent.Text);
            L3_32 = Convert.ToDouble(txtL3_32.Text);
            L3_32Percent = Convert.ToDouble(txtL3_32Percent.Text);
            SandnDust = Convert.ToDouble(txtSandnDust.Text);
            SandnDustPercent = Convert.ToDouble(txtSandnDustPercent.Text);
            NoofTotalStemPieces = Convert.ToDouble(txtNoofTotalStemPieces.Text);
            NoofL1_2StemPieces = Convert.ToDouble(txtNoofL1_2StemPieces.Text);
            L1_2StemPiecesPercent = Convert.ToDouble(txtL1_2StemPiecesPercent.Text);
            NoofG1_2L4StemPieces = Convert.ToDouble(txtNoofG1_2toL4StemPieces.Text);
            G1_2to4StemPiecesPercent = Convert.ToDouble(txtNoofG1_2toL4StemPiecesPercent.Text);
            NoofG4StemPieces = Convert.ToDouble(txtNoofG4StemPieces.Text);
            G4StemPiecesPercent = Convert.ToDouble(txtNoofG4StemPieces.Text);
            //

            insertdata();


        }
        public void ClearControls()
        {
            txt3_32.Text = "";
            txt3_32Percent.Text = "";
            txtAvgStemLength.Text = "";
            txtCaseNo.Text = "";
            txtDate.Text = "";
            txtL1_2StemPiecesPercent.Text = "";
            txtL3_32.Text = "";
            txtL3_32Percent.Text = "";
            txtNoofG1_2toL4StemPieces.Text = "";
            txtNoofG1_2toL4StemPiecesPercent.Text = "";
            txtNoofG4StemPieces.Text = "";
            txtNoofG4StemPiecesPercent.Text = "";
            txtNoofL1_2StemPieces.Text = "";
            txtNoofTotalStemPieces.Text = "";
            txtSandnDust.Text = "";
            txtSandnDustPercent.Text = "";
            txtStemPieces.Text = "";
            txtStemWeight.Text = "";
            txtTime.Text = "";
            txtTotalLength.Text = "";
            //ddCrop.SelectedIndex=0;
            //ddVariety.SelectedIndex=0;
            //ddLamiaGrade.SelectedIndex=0;
            //ddGrade.SelectedIndex=0;
        }
        public void insertdata()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand sqlcmd;
            try
            {

                string querychk = "SELECT COUNT(*) FROM [dbo].[GPIL_StemReports] where  [Crop]='" + Crop + "' and [Variety]='" + Variety + "' and [LamiaGrade]='" + LamiaGrade + "' and [Grade]='" + Grade + "' and [CaseNo]='" + CaseNo + "'";
                sqlcmd = new SqlCommand(querychk, con);
                k = Convert.ToInt32(sqlcmd.ExecuteScalar());
                sqlcmd.Dispose();
                if (k == 0)
                {

                    string queryIns = "INSERT INTO [dbo].[GPIL_StemReports]([Crop],[Variety],[LamiaGrade],[Grade],[Date] ,[Time] ,[CaseNo] ,[TotalLength] ,[NoofStemPieces]";
                    queryIns += ",[AvgStemLength],[StemWeight] ,[StemRotapG3_32],[StemRotapG3_32Percent],[StemRotapL3_32],[StemRotapL3_32Percent]";
                    queryIns += ",[SandnDust],[SandnDustPercent],[NoofTotalStemInPieces],[NoofL1_2Stems],[L1_2StemsPercent],[L1_2_G4Stems],[NoofL1_2toG4StemsPercent],[NoofG4Stems],[NoofG4StemPercent])";
                    queryIns += "VALUES ('" + Crop + "','" + Variety + "','" + LamiaGrade + "','" + Grade + "','" + Date + "','" + TimeStem + "'," + CaseNo + ",";
                    queryIns += "" + TotalLength + "," + NoofStemPieces + "," + AvgStemLength + "," + StemWeight + "," + G3_32 + "," + G3_32Percent + "," + L3_32 + "," + L3_32Percent + "," + SandnDust + "," + SandnDustPercent + ",";
                    queryIns += "" + NoofTotalStemPieces + "," + NoofL1_2StemPieces + "," + L1_2StemPiecesPercent + "," + NoofG1_2L4StemPieces + "," + G1_2to4StemPiecesPercent + "," + NoofG4StemPieces + "," + G4StemPiecesPercent + ")";
                    //sqlcmd = new SqlCommand(queryIns, con);
                    //sqlcmd.ExecuteNonQuery();
                    //sqlcmd.Dispose();

                    dataConnection.ExecuteNonQuery(queryIns);
                    lblMessage.Visible = true;
                    lblMessage.Text = "UPDATED!!";
                    ClearControls();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "DUPLICATE RECORD!!";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }
        }
        protected void txtEndRunNo_TextChanged(object sender, EventArgs e)
        {

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        public void NICcalc()
        {

        }
        protected void txtmpoisture_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txt3_32Percent_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtStemWeight_TextChanged(object sender, EventArgs e)
        {

        }


        protected void txtNoofG4StemPieces_TextChanged(object sender, EventArgs e)
        {
            NoofL1_2StemPieces = Convert.ToDouble(txtNoofL1_2StemPieces.Text);
            //NoofG1_2L4StemPieces = Convert.ToDouble(txtNoofG1_2toL4StemPieces.Text);
            NoofG4StemPieces = Convert.ToDouble(txtNoofG4StemPieces.Text);
            NoofStemPieces = Convert.ToInt32(txtStemPieces.Text);
            i = 3;
            TotalCalc();
            txtNoofTotalStemPieces.Text = NoofTotalStemPieces.ToString();
            txtL1_2StemPiecesPercent.Text = L1_2StemPiecesPercent.ToString();
            txtNoofG1_2toL4StemPiecesPercent.Text = G1_2to4StemPiecesPercent.ToString();
            txtNoofG4StemPiecesPercent.Text = G4StemPiecesPercent.ToString();
            //NoofG1_2L4StemPieces = Convert.ToDouble(txtNoofG1_2toL4StemPieces.Text);
            txtNoofG1_2toL4StemPieces.Text = NoofG1_2L4StemPieces.ToString();
            Label45.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(txtL1_2StemPiecesPercent.Text) + Convert.ToDouble(txtNoofG1_2toL4StemPiecesPercent.Text) + Convert.ToDouble(txtNoofG4StemPiecesPercent.Text), 0));
            if (Label45.Text == "100")
            {
                btnSave.Enabled = true;
                Button1.Enabled = true;

            }
            else
            {
                btnSave.Enabled = false;
            }
        }
        protected void txtStemPieces_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TotalLength = Convert.ToInt32(txtTotalLength.Text);
                NoofStemPieces = Convert.ToInt32(txtStemPieces.Text);
                i = 1;
                TotalCalc();
                txtAvgStemLength.Text = AvgStemLength.ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }
        }
        protected void txtSandnDust_TextChanged(object sender, EventArgs e)
        {
            try
            {
                G3_32 = Convert.ToDouble(txt3_32.Text);
                L3_32 = Convert.ToDouble(txtL3_32.Text);
                SandnDust = Convert.ToDouble(txtSandnDust.Text);
                i = 2;
                TotalCalc();
                txt3_32Percent.Text = G3_32Percent.ToString();
                txtL3_32Percent.Text = L3_32Percent.ToString();
                txtSandnDustPercent.Text = SandnDustPercent.ToString();
                txtStemWeight.Text = StemWeight.ToString();
                if ((G3_32Percent + L3_32Percent + SandnDustPercent) != 100)
                {
                    btnSave.Enabled = false;
                    // Alert.Show("Values result not 100!");
                    string script = "alert('Values result not 100!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ValuesNot100", script, true);
                }

            }


            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.ToString();
            }
        }
        public void TotalCalc()
        {
            if (i == 1 || i == 5)
            {
                AvgStemLength = System.Math.Round((TotalLength / NoofStemPieces / 25.4), 2);
            }
            if (i == 2 || i == 5)
            {
                StemWeight = G3_32 + L3_32 + SandnDust;
                G3_32Percent = System.Math.Round(((G3_32 / StemWeight) * 100), 2);
                L3_32Percent = System.Math.Round(((L3_32 / StemWeight) * 100), 2);
                SandnDustPercent = System.Math.Round(((SandnDust / StemWeight) * 100), 2);
            }
            if (i == 3 || i == 5)
            {
                NoofG1_2L4StemPieces = Math.Abs(Math.Abs(NoofStemPieces - NoofL1_2StemPieces) - NoofG4StemPieces);
                NoofTotalStemPieces = NoofL1_2StemPieces + NoofG1_2L4StemPieces + NoofG4StemPieces;

                L1_2StemPiecesPercent = System.Math.Round(((NoofL1_2StemPieces / NoofTotalStemPieces) * 100), 2);

                G1_2to4StemPiecesPercent = System.Math.Round(((NoofG1_2L4StemPieces / NoofTotalStemPieces) * 100), 2);
                G4StemPiecesPercent = System.Math.Round(((NoofG4StemPieces / NoofTotalStemPieces) * 100), 2);
            }
            //see the 100 values in this 


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlTransaction tran;
            //tran = con.BeginTransaction();
            try
            {
                for (int k = 0; k < GridViewSample.Rows.Count; k++)
                {

                    Crop = ((Label)GridViewSample.Rows[k].FindControl("lblCrop")).Text;
                    Variety = ((Label)GridViewSample.Rows[k].FindControl("lblVariety")).Text;
                    LamiaGrade = ((Label)GridViewSample.Rows[k].FindControl("lblLamiaGrade")).Text;
                    Grade = ((Label)GridViewSample.Rows[k].FindControl("lblGrade")).Text;
                    Date = ((Label)GridViewSample.Rows[k].FindControl("lblDate")).Text;
                    CaseNo = Convert.ToInt32(((Label)GridViewSample.Rows[k].FindControl("lblCaseNo")).Text);
                    TimeStem = ((Label)GridViewSample.Rows[k].FindControl("lblTimeStem")).Text;
                    TotalLength = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblTotalLength")).Text);
                    NoofStemPieces = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblNoofStemPieces")).Text);
                    AvgStemLength = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblAvgStemLength")).Text);
                    StemWeight = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblStemWeight")).Text);
                    G3_32 = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblG3_32")).Text);
                    G3_32Percent = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblG3_32Percent")).Text);
                    L3_32 = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblL3_32")).Text);
                    L3_32Percent = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblL3_32Percent")).Text);
                    SandnDust = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblSandnDust")).Text);
                    SandnDustPercent = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblSandnDustPercent")).Text);
                    NoofTotalStemPieces = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblNoofTotalStemPieces")).Text);
                    NoofL1_2StemPieces = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblNoofL1_2StemPieces")).Text);
                    L1_2StemPiecesPercent = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblL1_2StemPiecesPercent")).Text);
                    NoofG1_2L4StemPieces = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblNoofG1_2L4StemPieces")).Text);
                    G1_2to4StemPiecesPercent = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblG1_2to4StemPiecesPercent")).Text);
                    NoofG4StemPieces = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblNoofG4StemPieces")).Text);
                    G4StemPiecesPercent = Convert.ToDouble(((Label)GridViewSample.Rows[k].FindControl("lblG4StemPiecesPercent")).Text);


                    string queryIns = "INSERT INTO [dbo].[GPIL_StemReports]([Crop],[Variety],[LamiaGrade],[Grade],[Date] ,[Time] ,[CaseNo] ,[TotalLength] ,[NoofStemPieces]";
                    queryIns += ",[AvgStemLength],[StemWeight] ,[StemRotapG3_32],[StemRotapG3_32Percent],[StemRotapL3_32],[StemRotapL3_32Percent]";
                    queryIns += ",[SandnDust],[SandnDustPercent],[NoofTotalStemInPieces],[NoofL1_2Stems],[L1_2StemsPercent],[[L1_2_4Stems],[NoofL1_2toG4StemsPercent],[NoofG4Stems],[NoofG4StemPercent])";
                    queryIns += "VALUES ('" + Crop + "','" + Variety + "','" + LamiaGrade + "','" + Grade + "',(select convert(datetime,'" + Date + "',3)),'" + TimeStem + "'," + CaseNo + ",";
                    queryIns += "" + TotalLength + "," + NoofStemPieces + "," + AvgStemLength + "," + StemWeight + "," + G3_32 + "," + G3_32Percent + "," + L3_32 + "," + L3_32Percent + "," + SandnDust + "," + SandnDustPercent + ",";
                    queryIns += "" + NoofTotalStemPieces + "," + NoofL1_2StemPieces + "," + L1_2StemPiecesPercent + "," + NoofG1_2L4StemPieces + "," + G1_2to4StemPiecesPercent + "," + NoofG4StemPieces + "," + G4StemPiecesPercent + ")";
                    //SqlCommand sqlcmdins = new SqlCommand(queryIns, con);
                    ////sqlcmdins.Transaction = tran;
                    //sqlcmdins.ExecuteNonQuery();

                    //sqlcmdins.Dispose();
                    bool r = dataConnection.ExecuteNonQuery(queryIns);

                }
                //tran.Commit();
                GridViewSample.DataSource = null;
                GridViewSample.DataBind();
                lblMessage.Visible = true;
                lblMessage.Text = "Updated";
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                lblMessage.Text = ex.ToString();
                lblMessage.Visible = true;
            }
        }
        int z;
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                z = 1;

            }
            else if (RadioButton2.Checked == true)
            {
                z = 2;

            }
            else
            {
                z = 2;
            }
            this.RBCheck();
        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                z = 1;

            }
            else if (RadioButton2.Checked == true)
            {
                z = 2;

            }
            else
            {
                z = 2;
            }
            this.RBCheck();
        }
        bool str02 = true;
        bool str01 = false;
        public void RBCheck()
        {

            if (z == 1)
            {
                str01 = true;
                str02 = false;
            }
            else if (z == 2)
            {
                str01 = false;
                str02 = true;
            }
            else
            {
                str01 = false;
                str02 = false;
            }

            Label12.Visible = str01;
            btnImport.Visible = str01;
            FileUpload01.Visible = str01;
            Button1.Visible = str01;


            btnSave.Visible = str02;
            btnClear.Visible = str02;
            btnClose.Visible = str02;
            ddCrop.Visible = str02;
            Label29.Visible = str02;
            lblDate0.Visible = str02;

            lblOver13.Visible = str02;
            Label41.Visible = str02;
            txtDate.Visible = str02;

            lblDate.Visible = str02;
            Label30.Visible = str02;
            ddGrade.Visible = str02;

            txtTime.Visible = str02;
            Label42.Visible = str02;
            lblOver12.Visible = str02;

            Label1.Visible = str02;
            Label31.Visible = str02;
            ddVariety.Visible = str02;

            txtCaseNo.Visible = str02;
            Label43.Visible = str02;
            Label2.Visible = str02;

            ddLamiaGrade.Visible = str02;
            Label32.Visible = str02;
            Label3.Visible = str02;

            txtTotalLength.Visible = str02;
            Label44.Visible = str02;
            Label16.Visible = str02;

            txtStemPieces.Visible = str02;
            Label5.Visible = str02;
            Label33
                .Visible = str02;
            txtAvgStemLength.Visible = str02;
            Label6.Visible = str02;

            txt3_32.Visible = str02;
            Label8.Visible = str02;
            Label34.Visible = str02;

            txt3_32Percent.Visible = str02;
            Label9.Visible = str02;

            txtL3_32.Visible = str02;
            Label17.Visible = str02;
            Label35.Visible = str02;

            txtL3_32Percent.Visible = str02;
            Label10.Visible = str02;

            txtSandnDust.Visible = str02;
            Label36.Visible = str02;
            Label18.Visible = str02;

            txtSandnDustPercent.Visible = str02;
            Label19.Visible = str02;

            txtNoofL1_2StemPieces.Visible = str02;
            Label37.Visible = str02;
            Label25.Visible = str02;

            txtStemWeight.Visible = str02;
            Label7.Visible = str02;

            txtNoofG4StemPieces.Visible = str02;
            Label38.Visible = str02;
            Label24.Visible = str02;

            txtL1_2StemPiecesPercent.Visible = str02;
            Label26.Visible = str02;

            txtNoofG1_2toL4StemPieces.Visible = str02;
            Label39.Visible = str02;
            Label23.Visible = str02;


            txtNoofG1_2toL4StemPiecesPercent.Visible = str02;
            Label27.Visible = str02;

            txtNoofTotalStemPieces.Visible = str02;
            Label21.Visible = str02;
            Label40.Visible = str02;

            txtNoofG4StemPiecesPercent.Visible = str02;
            Label28.Visible = str02;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }
    }
}