using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPILWebApp.ViewModel;
using System.Collections;

using System.Web.UI;
using System.Data.SqlClient;
using GPILWebApp.Models;
using System.Reflection.Emit;
using System.Data.OleDb;
using DocumentFormat.OpenXml.Office.Word;
using System.IO;
using System.Configuration;

namespace GPILWebApp.CrystalReport.WebForms.REPORTS
{



    public partial class LAMINA_DEGRADATION : System.Web.UI.Page
    {

        int z;
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd;
        DataTable objDataTable = new DataTable();
        public static DataTable dt = new DataTable();

        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                if (!IsPostBack)
                {
                    //RBCheck();
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("Crop", typeof(string));
                        dt.Columns.Add("Grade", typeof(string));
                        dt.Columns.Add("Variety", typeof(string));
                        dt.Columns.Add("Date", typeof(string));
                        dt.Columns.Add("Time", typeof(string));
                        dt.Columns.Add("RunNo", typeof(Int32));
                        dt.Columns.Add("CaseNo", typeof(Int32));
                        dt.Columns.Add("Sampleweight", typeof(double));
                        dt.Columns.Add("Over11", typeof(double));
                        dt.Columns.Add("Over1", typeof(double));
                        dt.Columns.Add("Over1212", typeof(double));
                        dt.Columns.Add("Over12", typeof(double));
                        dt.Columns.Add("TOver1212", typeof(double));
                        dt.Columns.Add("TOver12", typeof(double));
                        dt.Columns.Add("Over14", typeof(double));
                        dt.Columns.Add("Over14Second", typeof(double));
                        dt.Columns.Add("TOver14", typeof(double));
                        dt.Columns.Add("Over18", typeof(double));
                        dt.Columns.Add("Over18Second", typeof(double));
                        dt.Columns.Add("OverPAN", typeof(double));
                        dt.Columns.Add("PercentOverPan", typeof(double));
                        dt.Columns.Add("Over18Percent", typeof(double));
                        dt.Columns.Add("FirstPass", typeof(double));
                        dt.Columns.Add("PercentFirstPass", typeof(double));
                        dt.Columns.Add("SecondPass", typeof(double));
                        dt.Columns.Add("PercentSecondPass", typeof(double));
                        dt.Columns.Add("Obj3_32", typeof(double));
                        dt.Columns.Add("Obj3_32Second", typeof(double));
                        dt.Columns.Add("Slot07", typeof(double));
                        dt.Columns.Add("Slot07Second", typeof(double));
                        dt.Columns.Add("Slot12", typeof(double));
                        dt.Columns.Add("Slot12Second", typeof(double));
                        dt.Columns.Add("Mesh12", typeof(double));
                        dt.Columns.Add("Mesh12Second", typeof(double));
                        dt.Columns.Add("FiberHist", typeof(double));
                        dt.Columns.Add("FiberHistSecond", typeof(double));
                        dt.Columns.Add("Tsef", typeof(double));
                        dt.Columns.Add("TsefHistSecond", typeof(double));
                        dt.Columns.Add("NewFiber", typeof(double));
                        dt.Columns.Add("NewFiberSecond", typeof(double));
                        dt.Columns.Add("TsefNew", typeof(double));
                        dt.Columns.Add("New", typeof(double));
                        dt.Columns.Add("TotalStemintips", typeof(string));
                        dt.Columns.Add("LC", typeof(string));
                        dt.Columns.Add("Stem", typeof(string));
                        dt.Columns.Add("PercentageobjStem", typeof(string));
                        dt.Columns.Add("PercentStemTips", typeof(string));
                        dt.Columns.Add("SystemFlag", typeof(string));
                        dt.Columns.Add("PackedDensity", typeof(string));
                        dt.Columns.Add("Remarks", typeof(string));
                        dt.Columns.Add("Sum", typeof(string));
                    }
                    RBCheck();
                    Bindings();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "alert('" + ex.Message + "');", true);
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (ddCrop.SelectedItem.ToString() != "" && ddGrade.SelectedItem.ToString() != "" && ddVariety.SelectedItem.ToString() != "")
            {
                try
                {
                    int j = 0;

                    string querycheck = "Select Count(*) FROM [dbo].[GPIL_RTQCR] where [Crop]='" + ddCrop.SelectedItem.ToString() + "' and [Grade]='" + ddGrade.SelectedItem.ToString() + "' and [Variety]='" + ddVariety.SelectedItem.ToString() + "' And [RunNo]='" + txtRunNo.Text + "' and [CaseNo]='" + txtCaseNo.Text + "'";
                    SqlCommand sqlCheck = new SqlCommand(querycheck, conn);

                    j = Convert.ToInt32(sqlCheck.ExecuteScalar());
                    if (j == 0)
                    {
                        query = "INSERT INTO [dbo].[GPIL_RTQCR] ";
                        query += "([Crop],[Variety],[Grade],[Grade_Date] ,[Sample_Time] ,[RunNo],[CaseNo] ,[SampleWeight]";
                        query += ",[Over11] ,[Over1] ,[Over1212] ,[Over12] ,[TOver1212]   ,[TOver12],[Over14]  ,";
                        query += "[TOver14],[Over18],[Over182] ,[OverPan] ,[PercntOnPan],[Over18P]";
                        query += " ,[FirstPass],[PercentFirstPass] ,[SecondPass] ,[PercentSecondPass] ";
                        query += ",[Obj3_32] ,[Obj3_32Second] ,[Slot07],[Slot07Second] ,[Slot12],[Slot12Second] ";
                        query += ",[Mesh12] ,[Mesh12Second] ,[FiberHist] ,[FiberHistSecond]  ,[TsefHist] ,[TsefHistSecond] ,";
                        query += "[NewFiber] ,[NewFiberSecond],[TsefNew] ,[New] ,[TotalStemInTips] ,[LC] ,[Stem],[PercentObjStem] ,";
                        query += "[PercentStemTips]  ,[SystemFlagAnalysis] ,[PackedDensityDVR] ,[Remarks] ,[Over14Second])";
                        query += "VALUES('" + ddCrop.SelectedItem.ToString() + "','" + ddVariety.SelectedItem.ToString() + "','" + ddGrade.SelectedItem.ToString() + "','" + txtDate.Text + "','" + txtTime.Text + "','" + txtRunNo.Text + "',";
                        query += "'" + txtCaseNo.Text + "','" + txtSampleWeight.Text + "','" + txtOver11.Text + "','" + txtOver1.Text + "','" + txtOver1212.Text + "',";
                        query += "'" + txtOver12.Text + "','" + txtTOver1212.Text + "','" + txtTOver12.Text + "','" + txtOver14.Text + "','" + txtTOver14.Text + "',";
                        query += "'" + txtOver18.Text + "','" + txtOver182.Text + "','" + txtOverPan.Text + "','" + txtPercenOnPan.Text + "','" + txtOver18P.Text + "',";
                        query += "'" + txtFirstPass.Text + "','" + txtPercentFirstPass.Text + "','" + txtSecondPass.Text + "','" + txtpercentSecondPass.Text + "',";
                        query += "'" + txtObj3_32.Text + "','" + txtObj3_32Second.Text + "','" + txtSloth07.Text + "','" + txtSlot07Second.Text + "','" + txtSloth12.Text + "','" + txtSlot12Second.Text + "','" + txtMesh12.Text + "',";
                        query += "'" + txtMesh12Second.Text + "','" + txtFiberHist.Text + "','" + txtFiberHistSecond.Text + "','" + txtTsefHist.Text + "','" + txtTsefHistSecond.Text + "',";
                        query += "'" + txtNewFiber.Text + "','" + txtNewFiberSecond.Text + "','" + txtTsefNew.Text + "','" + txtNew.Text + "','" + txtPercentTotalStem.Text + "','" + txtLC.Text + "',";
                        query += "'" + txtStem.Text + "','" + txtPercentObjStemTips.Text + "','" + txtPercentStemTips.Text + "','" + txtSystemFlag.Text + "','" + txtPackeddensityDVR.Text + "','" + txtRemarks.Text + "',";
                        query += "'" + txtOver14Second.Text + "')";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        lblMessage.Visible = true;
                        lblMessage.Text = "Updated!!";
                        cleard();
                    }
                    else
                    {
                        lblMessage.Text = "Duplicate data Entered!!!";
                        lblMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.ToString();
                    lblMessage.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = "Enter All the Values";
                lblMessage.Visible = true;
            }
        }
        int k = 0;
        protected void btnClear_Click(object sender, EventArgs e)
        {
            k = 1;
            cleard();
        }


        void Bindings()
        {
            //Crop
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }


                string query;
                SqlDataAdapter sda;
                DataTable dt = new DataTable();

                query = "SELECT[CROP_YEAR] FROM [dbo].[GPIL_CROP_MASTER]";
                sda = new SqlDataAdapter(query, conn);
                sda.Fill(dt);
                ddCrop.DataSource = dt;
                ddCrop.DataBind();
                ddCrop.DataTextField = "CROP_YEAR";
                ddCrop.DataBind();
                ddCrop.DataBind();
                ddCrop.Items.Insert(0, new ListItem("--Select--", "0"));
                sda.Dispose();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
            //Variety
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query;
                SqlDataAdapter sda;
                DataTable dt = new DataTable();
                query = "SELECT [VARIETY_TYPE]  FROM [dbo].[GPIL_VARIETY_MASTER]";
                sda = new SqlDataAdapter(query, conn);
                sda.Fill(dt);
                ddVariety.DataSource = dt;
                ddVariety.DataBind();
                ddVariety.DataTextField = "VARIETY_TYPE";
                ddVariety.DataBind();
                ddVariety.DataBind();
                ddVariety.Items.Insert(0, new ListItem("--Select--", "0"));
                sda.Dispose();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
            //Grade
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }


                string query;
                SqlDataAdapter sda;
                DataTable dt = new DataTable();

                query = "SELECT [ITEM_CODE] FROM [dbo].[GPIL_ITEM_MASTER] where [Item_Code] like 'L%'";
                sda = new SqlDataAdapter(query, conn);
                sda.Fill(dt);
                ddGrade.DataSource = dt;
                ddGrade.DataBind();
                ddGrade.DataTextField = "ITEM_CODE";
                ddGrade.DataBind();
                ddGrade.DataBind();
                ddGrade.Items.Insert(0, new ListItem("--Select--", "0"));
                sda.Dispose();


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmHomePage.aspx");
        }

        protected void txtOverPan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtOver11.Text != "" && txtOver1212.Text != "" && txtOver1212.Text != "" && txtOver14.Text != "" && txtOver18.Text != "" && txtOverPan.Text != "")
                {
                    txtSampleWeight.Text = System.Math.Round(((Convert.ToDecimal(txtOver11.Text) + Convert.ToDecimal(txtOver1212.Text) + Convert.ToDecimal(txtOver14.Text) + Convert.ToDecimal(txtOver18.Text) + Convert.ToDecimal(txtOverPan.Text))), 2).ToString().Trim();
                    txtOver1.Text = System.Math.Round(((Convert.ToDecimal(txtOver11.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100), 2).ToString().Trim();
                    //txtOver1212.Text = System.Math.Round(Convert.ToDecimal(txtOver11.Text) + Convert.ToDecimal(txtOver1212.Text), 2).ToString().Trim();
                    txtOver12.Text = System.Math.Round(((Convert.ToDecimal(txtOver1212.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100), 2).ToString().Trim();
                    txtTOver1212.Text = System.Math.Round(Convert.ToDecimal(txtOver11.Text) + Convert.ToDecimal(txtOver1212.Text), 2).ToString().Trim();
                    txtTOver12.Text = System.Math.Round(Convert.ToDecimal(txtOver12.Text) + Convert.ToDecimal(txtOver1.Text), 2).ToString().Trim();
                    txtOver14Second.Text = System.Math.Round(((Convert.ToDecimal(txtOver14.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100), 2).ToString().Trim();
                    txtTOver14.Text = System.Math.Round(Convert.ToDecimal(txtOver14Second.Text) + Convert.ToDecimal(txtTOver12.Text), 2).ToString().Trim();
                    txtOver182.Text = System.Math.Round(((Convert.ToDecimal(txtOver18.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100), 2).ToString().Trim();
                    txtPercenOnPan.Text = System.Math.Round(((Convert.ToDecimal(txtOverPan.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100), 2).ToString().Trim();
                    txtOver18P.Text = System.Math.Round(Convert.ToDecimal(txtOver182.Text) + Convert.ToDecimal(txtPercenOnPan.Text), 2).ToString().Trim();
                    txtFirstPass.Focus();
                    lblMessage.Visible = false;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Some Textbox Fields are EMPTY!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void txtFirstPass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstPass.Text != "")
                {
                    txtPercentFirstPass.Text = System.Math.Round(((Convert.ToDecimal(txtFirstPass.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100), 2).ToString().Trim();
                    txtObj3_32.Focus(); lblMessage.Visible = false;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Some Textbox Fields are EMPTY!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void txtFiberHist_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtObj3_32.Text != "" || txtSloth07.Text != "" || txtSloth12.Text != "" || txtMesh12.Text != "" || txtFiberHist.Text != "")
                {
                    txtSecondPass.Text = System.Math.Round((Convert.ToDecimal(txtObj3_32.Text) + Convert.ToDecimal(txtSloth07.Text) + Convert.ToDecimal(txtSloth12.Text) + Convert.ToDecimal(txtMesh12.Text) + Convert.ToDecimal(txtFiberHist.Text)), 2).ToString().Trim();
                    txtpercentSecondPass.Text = System.Math.Round((Convert.ToDecimal(txtSecondPass.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtObj3_32Second.Text = System.Math.Round((Convert.ToDecimal(txtObj3_32.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtSlot07Second.Text = System.Math.Round((Convert.ToDecimal(txtSloth07.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtSlot12Second.Text = System.Math.Round((Convert.ToDecimal(txtSloth12.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtMesh12Second.Text = System.Math.Round((Convert.ToDecimal(txtMesh12.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtFiberHistSecond.Text = System.Math.Round((Convert.ToDecimal(txtFiberHist.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtTsefHist.Text = System.Math.Round((Convert.ToDecimal(txtObj3_32.Text) + Convert.ToDecimal(txtSloth07.Text) + Convert.ToDecimal(txtSloth12.Text) + Convert.ToDecimal(txtMesh12.Text)), 2).ToString().Trim();
                    txtTsefHistSecond.Text = System.Math.Round((Convert.ToDecimal(txtTsefHist.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtNewFiber.Text = System.Math.Round(Convert.ToDecimal(txtMesh12.Text) + Convert.ToDecimal(txtFiberHist.Text), 2).ToString().Trim();
                    txtNewFiberSecond.Text = System.Math.Round((Convert.ToDecimal(txtNewFiber.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    txtTsefNew.Text = System.Math.Round((Convert.ToDecimal(txtObj3_32.Text) + Convert.ToDecimal(txtSloth07.Text) + Convert.ToDecimal(txtSloth12.Text)), 2).ToString().Trim();
                    txtNew.Text = System.Math.Round((Convert.ToDecimal(txtTsefNew.Text) / Convert.ToDecimal(txtSampleWeight.Text)) * 100, 2).ToString().Trim();
                    lblMessage.Visible = false;
                }

                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Some Textbox Fields are EMPTY!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString().Trim();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {

            GridViewSample.DataSource = null;
            GridViewSample.DataBind();
            try
            {//Values to do the calc
                btnSaveGrid.Enabled = false;
                string Crop; string Variety; string Sampleweight; string Over1; string Over12; string TOver1212; string TOver12; string Over14Second; string TOver14;
                string Over182; string PercentOverPan; string Over18p; string PercentFirstPass; string SecondPass; string PercentSecondPass;
                string Obj3_32Second; string Slot07Second; string Mesh12Second; string FiberHistSecond; string TsefHist; string TsefHistSecond; string NewFiber;
                string NewFiberSecond; string TsefNew; string New; string Grade; string Date; string Sample_Time; string RunNo; string CaseNo; string Over11;
                string Over1212; string over14; string Over18; string OverPan; string First_Pass; string Obj3_32; string Slot07; string Slot12; string mesh12;
                string FiberHist; string TotalStemTips; string LC; string Stem; string PercentObjStem; string PercentSteminTips; string SysFlagAnalysis; string PackedDesity;
                string Remarks; string Slot12Second;

                int k = 0;
                //// Alert.Show(dt.Rows.Count.ToString());
                ////string serpath = FileUpload01.ToString();
                ////@"D:\\SampleGpi.xlsx";
                ////SqlTransaction tran = null;
                //string serpath = string.Concat(Server.MapPath("~/TempFiles/"), FileUpload01.FileName);
                //OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + serpath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"");
                ////Save File as Temp then you can delete it if you want
                //DataTable asw = new DataTable();
                ////  FileUpload.SaveAs(serpath);
                //FileUpload01.SaveAs(serpath);




                //string query = "SELECT * from [Sheet1$]";
                try
                {

                    //OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter(query, oconn);
                    //objDataTable.Clear();
                    //objOleDbDataAdapter.Fill(objDataTable);

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

                    //try
                    //{
                    //    for (int d = 0; d < objDataTable.Rows.Count; d++)
                    //    {
                    //        if (objDataTable.Rows[d][0].ToString() != "" && objDataTable.Rows[d][1].ToString() != "" && objDataTable.Rows[d][2].ToString() != "")
                    //        {
                    //            Crop = objDataTable.Rows[d][0].ToString();
                    //            Variety = objDataTable.Rows[d][1].ToString();
                    //            Grade = objDataTable.Rows[d][2].ToString();
                    //            Date = Convert.ToDateTime(objDataTable.Rows[d][3]).ToString("yyyy-MM-dd hh:mm:ss");
                    //            Sample_Time = (Convert.ToDateTime(objDataTable.Rows[d][4].ToString())).ToShortTimeString();
                    //            RunNo = objDataTable.Rows[d][5].ToString();
                    //            CaseNo = objDataTable.Rows[d][6].ToString();
                    //            Over11 = objDataTable.Rows[d][7].ToString();
                    //            Over1212 = objDataTable.Rows[d][8].ToString();
                    //            over14 = objDataTable.Rows[d][9].ToString();
                    //            Over18 = objDataTable.Rows[d][10].ToString();
                    //            OverPan = objDataTable.Rows[d][11].ToString();
                    //            First_Pass = objDataTable.Rows[d][12].ToString();
                    //            Obj3_32 = objDataTable.Rows[d][13].ToString();
                    //            Slot07 = objDataTable.Rows[d][14].ToString();
                    //            Slot12 = objDataTable.Rows[d][15].ToString();
                    //            mesh12 = objDataTable.Rows[d][16].ToString();
                    //            FiberHist = objDataTable.Rows[d][17].ToString();
                    //            TotalStemTips = objDataTable.Rows[d][18].ToString();
                    //            LC = objDataTable.Rows[d][19].ToString();
                    //            Stem = objDataTable.Rows[d][20].ToString();
                    //            PercentObjStem = objDataTable.Rows[d][21].ToString();
                    //            PercentSteminTips = objDataTable.Rows[d][22].ToString();
                    //            SysFlagAnalysis = objDataTable.Rows[d][23].ToString();
                    //            PackedDesity = objDataTable.Rows[d][24].ToString();
                    //            Remarks = objDataTable.Rows[d][25].ToString();

                    //            Over11 = Convert.ToString(System.Math.Round(Convert.ToDouble(Over11), 2));
                    //            Over1212 = Convert.ToString(System.Math.Round(Convert.ToDouble(Over1212), 2));
                    //            over14 = Convert.ToString(System.Math.Round(Convert.ToDouble(over14), 2));
                    //            Over18 = Convert.ToString(System.Math.Round(Convert.ToDouble(Over18), 2));
                    //            OverPan = Convert.ToString(System.Math.Round(Convert.ToDouble(OverPan), 2));
                    //            First_Pass = Convert.ToString(System.Math.Round(Convert.ToDouble(First_Pass), 2));
                    //            Obj3_32 = Convert.ToString(System.Math.Round(Convert.ToDouble(Obj3_32), 2));
                    //            Slot07 = Convert.ToString(System.Math.Round(Convert.ToDouble(Slot07), 2));
                    //            Slot12 = Convert.ToString(System.Math.Round(Convert.ToDouble(Slot12), 2));
                    //            mesh12 = Convert.ToString(System.Math.Round(Convert.ToDouble(mesh12), 2));
                    //            FiberHist = Convert.ToString(System.Math.Round(Convert.ToDouble(FiberHist), 2));
                    //            //TotalStemTips = Convert.ToString(System.Math.Round(Convert.ToDouble(TotalStemTips), 2));
                    //            //LC = Convert.ToString(System.Math.Round(Convert.ToDouble(LC), 2));
                    //            //Stem = Convert.ToString(System.Math.Round(Convert.ToDouble(Stem), 2));
                    //            //PercentObjStem = Convert.ToString(System.Math.Round(Convert.ToDouble(PercentObjStem), 2));

                    //            if (Obj3_32 != "" || Slot07 != "" || Slot07 != "" || mesh12 != "" || FiberHist != "" || Over11 != "" || Over1212 != "" || Over1212 != "" || over14 != "" || Over18 != "" || OverPan != "")
                    //            {
                    //                Sampleweight = System.Math.Round(((Convert.ToDecimal(Over11) + Convert.ToDecimal(Over1212) + Convert.ToDecimal(over14) + Convert.ToDecimal(Over18) + Convert.ToDecimal(OverPan))), 2).ToString().Trim();
                    //                Over1 = System.Math.Round(((Convert.ToDecimal(Over11) / Convert.ToDecimal(Sampleweight)) * 100), 2).ToString().Trim();
                    //                //txtOver1212.Text = System.Math.Round(Convert.ToDecimal(txtOver11.Text) + Convert.ToDecimal(txtOver1212.Text), 2).ToString().Trim();
                    //                Over12 = System.Math.Round(((Convert.ToDecimal(Over1212) / Convert.ToDecimal(Sampleweight)) * 100), 2).ToString().Trim();
                    //                TOver1212 = System.Math.Round(Convert.ToDecimal(Over11) + Convert.ToDecimal(Over1212), 2).ToString().Trim();
                    //                TOver12 = System.Math.Round(Convert.ToDecimal(Over12) + Convert.ToDecimal(Over1), 2).ToString().Trim();
                    //                Over14Second = System.Math.Round(((Convert.ToDecimal(over14) / Convert.ToDecimal(Sampleweight)) * 100), 2).ToString().Trim();
                    //                TOver14 = System.Math.Round(Convert.ToDecimal(Over14Second) + Convert.ToDecimal(TOver12), 2).ToString().Trim();
                    //                Over182 = System.Math.Round(((Convert.ToDecimal(Over18) / Convert.ToDecimal(Sampleweight)) * 100), 2).ToString().Trim();
                    //                PercentOverPan = System.Math.Round(((Convert.ToDecimal(OverPan) / Convert.ToDecimal(Sampleweight)) * 100), 2).ToString().Trim();
                    //                Over18p = System.Math.Round(Convert.ToDecimal(Over182) + Convert.ToDecimal(PercentOverPan), 2).ToString().Trim();
                    //                PercentFirstPass = System.Math.Round((Convert.ToDecimal(First_Pass) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                SecondPass = System.Math.Round((Convert.ToDecimal(Obj3_32) + Convert.ToDecimal(Slot07) + Convert.ToDecimal(Slot12) + Convert.ToDecimal(mesh12) + Convert.ToDecimal(FiberHist)), 2).ToString().Trim();
                    //                PercentSecondPass = System.Math.Round((Convert.ToDecimal(SecondPass) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                Obj3_32Second = System.Math.Round((Convert.ToDecimal(Obj3_32) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                Slot07Second = System.Math.Round((Convert.ToDecimal(Slot07) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                Slot12Second = System.Math.Round((Convert.ToDecimal(Slot12) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                Mesh12Second = System.Math.Round((Convert.ToDecimal(mesh12) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                FiberHistSecond = System.Math.Round((Convert.ToDecimal(FiberHist) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                TsefHist = System.Math.Round((Convert.ToDecimal(Obj3_32) + Convert.ToDecimal(Slot07) + Convert.ToDecimal(Slot12) + Convert.ToDecimal(mesh12)), 2).ToString().Trim();
                    //                TsefHistSecond = System.Math.Round((Convert.ToDecimal(TsefHist) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                NewFiber = System.Math.Round(Convert.ToDecimal(mesh12) + Convert.ToDecimal(FiberHist), 2).ToString().Trim();
                    //                NewFiberSecond = System.Math.Round((Convert.ToDecimal(NewFiber) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();
                    //                TsefNew = System.Math.Round((Convert.ToDecimal(Obj3_32) + Convert.ToDecimal(Slot07) + Convert.ToDecimal(Slot12)), 2).ToString().Trim();
                    //                New = System.Math.Round((Convert.ToDecimal(TsefNew) / Convert.ToDecimal(Sampleweight)) * 100, 2).ToString().Trim();

                    //                try
                    //                {
                    //                    long j = 0;
                    //                    int o = 0;
                    //                    string querycheck = "Select Count(*) FROM [dbo].[GPIL_RTQCR] where [Crop]='" + Crop + "' and [Grade]='" + Grade + "' and [Variety]='" + Variety + "' and caseno='" + CaseNo + "'  And [RunNo]='" + RunNo + "'";
                    //                    SqlCommand sqlCheck = new SqlCommand(querycheck, conn);
                    //                    //sqlCheck.Transaction = tran;
                    //                    j = Convert.ToInt64(sqlCheck.ExecuteScalar());
                    //                    sqlCheck.Dispose();
                    //                    if (j == 0)
                    //                    {


                    //                        string querycheck1 = "SELECT Count(*) FROM [dbo].[GPIL_CROP_MASTER] where [CROP_YEAR]='" + Crop + "'  union ";
                    //                        querycheck1 += "SELECT Count(*) FROM [dbo].[GPIL_VARIETY_MASTER] where [VARIETY_TYPE]='" + Variety + "' union ";
                    //                        querycheck1 += "SELECT Count(*) FROM [dbo].[GPIL_ITEM_MASTER] where [ITEM_CODE]='" + Grade + "'";
                    //                        SqlCommand sqlCheck01 = new SqlCommand(querycheck1, conn);
                    //                        // sqlCheck01.Transaction = tran;
                    //                        o = Convert.ToInt32(sqlCheck01.ExecuteScalar());
                    //                        sqlCheck01.Dispose();





                    //                        ///Insert into Dt
                    //                        ///


                    //                        DataRow NewRow = dt.NewRow();
                    //                        NewRow[0] = Crop;
                    //                        NewRow[1] = Grade;
                    //                        NewRow[2] = Variety;
                    //                        NewRow[3] = Date;
                    //                        NewRow[4] = Sample_Time;
                    //                        NewRow[5] = RunNo;
                    //                        NewRow[6] = CaseNo;
                    //                        NewRow[7] = Sampleweight;
                    //                        NewRow[8] = Over11;
                    //                        NewRow[9] = Over1;
                    //                        NewRow[10] = Over1212;
                    //                        NewRow[11] = Over12;
                    //                        NewRow[12] = TOver1212;
                    //                        NewRow[13] = TOver12;
                    //                        NewRow[14] = over14;
                    //                        NewRow[15] = Over14Second;
                    //                        NewRow[16] = TOver14;
                    //                        NewRow[17] = Over18;
                    //                        NewRow[18] = Over182;
                    //                        NewRow[19] = OverPan;
                    //                        NewRow[20] = PercentOverPan;
                    //                        NewRow[21] = Over18p;
                    //                        NewRow[22] = First_Pass;
                    //                        NewRow[23] = PercentFirstPass;
                    //                        NewRow[24] = SecondPass;
                    //                        NewRow[25] = PercentSecondPass;
                    //                        NewRow[26] = Obj3_32;
                    //                        NewRow[27] = Obj3_32Second;
                    //                        NewRow[28] = Slot07;
                    //                        NewRow[29] = Slot07Second;
                    //                        NewRow[30] = Slot12;
                    //                        NewRow[31] = Slot12Second;
                    //                        NewRow[32] = mesh12;
                    //                        NewRow[33] = Mesh12Second;
                    //                        NewRow[34] = FiberHist;
                    //                        NewRow[35] = FiberHistSecond;
                    //                        NewRow[36] = TsefHist;
                    //                        NewRow[37] = TsefHistSecond;
                    //                        NewRow[38] = NewFiber;
                    //                        NewRow[39] = NewFiberSecond;
                    //                        NewRow[40] = TsefNew;
                    //                        NewRow[41] = New;
                    //                        NewRow[42] = TotalStemTips;
                    //                        NewRow[43] = LC;
                    //                        NewRow[44] = Stem;
                    //                        NewRow[45] = PercentObjStem;
                    //                        NewRow[46] = PercentSteminTips;
                    //                        NewRow[47] = SysFlagAnalysis;
                    //                        NewRow[48] = PackedDesity;
                    //                        NewRow[49] = Remarks;


                    //                        dt.Rows.Add(NewRow);
                    //                        ///Alert.Show(dt.Rows.Count.ToString());
                    //                        this.GridViewSample.Visible = true;
                    //                        GridViewSample.DataSource = dt;
                    //                        GridViewSample.DataBind();
                    //                        GridViewSample.EditIndex = -1;

                    //                        //if (0 != 1)
                    //                        //{




                    //                        //}
                    //                        //else
                    //                        //{
                    //                        //    lblMessage.Visible = true;
                    //                        //    lblMessage.Text = "The Crop variety or grade is incorrect at line: " + (d + 2) + "";
                    //                        //k = 1;
                    //                        //}
                    //                    }
                    //                    else
                    //                    {
                    //                        lblMessage.Text = "Duplicate Record...";
                    //                        lblMessage.Visible = true;
                    //                    }
                    //                }
                    //                catch (Exception ex)
                    //                {

                    //                    lblMessage.Visible = true;
                    //                    lblMessage.Text = ex.ToString();
                    //                    k = 1;
                    //                }



                    //            }
                    //            else
                    //            {
                    //                lblMessage.Visible = true;
                    //                lblMessage.Text = "EXCEL has some Incomplete Fields";
                    //                k = 1;
                    //            }


                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{

                    //    lblMessage.Visible = true;
                    //    lblMessage.Text = ex.ToString();
                    //    k = 1;
                    //}
                    if (k == 0)
                    {
                        // tran.Commit();
                        btnSaveGrid.Enabled = true;

                    }
                    else
                    {
                        //tran.Rollback();
                        lblMessage.Visible = true;
                        lblMessage.Text = "Error, Check the excel File and Upload!!";
                        k = 1;
                    }
                }

                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    // lblMessage.Text = "Error, Check the excel File and Upload!!";
                    lblMessage.Text = ex.ToString(); k = 1;
                }

            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                // lblMessage.Text = "Error, Check the excel File and Upload!!";
                lblMessage.Text = ex.ToString(); k = 1;
            }
            if (k == 1)
            {
                btnSaveGrid.Enabled = false;
            }

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
            //RadioButton2.Checked = false;
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
            ///Excel Upload 
            Label1.Visible = str01;
            FileUpload01.Visible = str01;
            btnImport.Visible = str01;
            btnSaveGrid.Visible = str01;
            //btnSave0.Visible = str01;

            ///Manual Entry
            ddCrop.Visible = str02; ddVariety.Visible = str02;
            Label2.Visible = str02; Label4.Visible = str02;
            Label3.Visible = str02; Label5.Visible = str02;
            ddGrade.Visible = str02; lblGrade.Visible = str02;
            txtDate.Visible = str02; lblDate.Visible = str02;
            txtTime.Visible = str02; lblTime.Visible = str02;
            txtRunNo.Visible = str02; lblRunNo.Visible = str02;
            txtCaseNo.Visible = str02; lblCaseNo.Visible = str02;
            txtSampleWeight.Visible = str02; lblSampleweight.Visible = str02;
            txtOver11.Visible = str02; lblOver1.Visible = str02;
            txtOver1.Visible = str02; lblOver12.Visible = str02;
            txtOver12.Visible = str02; lblOver1212.Visible = str02;
            txtOver1212.Visible = str02; lblOver12.Visible = str02;
            txtTOver12.Visible = str02; lblTover122.Visible = str02;
            txtOver14Second.Visible = str02; lblOver14.Visible = str02;
            txtTOver14.Visible = str02; lbltOver14.Visible = str02;
            txtOver18.Visible = str02; lblOver18.Visible = str02;
            txtOver18P.Visible = str02; lblOver18p.Visible = str02;
            txtFirstPass.Visible = str02; lblFirstPass.Visible = str02;
            txtPercentFirstPass.Visible = str02; lblPercent1pass.Visible = str02;
            txtSecondPass.Visible = str02; lbl2Pass.Visible = str02;
            txtpercentSecondPass.Visible = str02; lblpercent2pass.Visible = str02;
            txtObj3_32.Visible = str02; lblObj332.Visible = str02;
            txtObj3_32Second.Visible = str02; lblobj3322.Visible = str02;
            txtSloth07.Visible = str02; lblslot7.Visible = str02;
            txtSloth12.Visible = str02; lblslot12.Visible = str02;
            txtSlot07Second.Visible = str02; lblslot72.Visible = str02;
            txtSlot12Second.Visible = str02; lblslot122.Visible = str02;
            txtMesh12.Visible = str02; lblMesh12.Visible = str02;
            txtMesh12Second.Visible = str02; lblmesh122.Visible = str02;
            txtFiberHist.Visible = str02; lblFiberHist.Visible = str02;
            txtFiberHistSecond.Visible = str02; lblFiberhist2.Visible = str02;
            txtNewFiber.Visible = str02; lblNewFiber.Visible = str02;
            txtNewFiberSecond.Visible = str02; lblnewfiber2.Visible = str02;
            txtTsefHist.Visible = str02; lblTsefhist.Visible = str02;
            txtTsefHistSecond.Visible = str02; lbltsefhisy2.Visible = str02;
            txtTsefNew.Visible = str02; lblTSEFNew.Visible = str02;
            txtNew.Visible = str02; lblNew.Visible = str02;
            txtStem.Visible = str02; lblStem.Visible = str02;
            txtPercentStemTips.Visible = str02; lblpercentstemtips.Visible = str02;
            txtOver182.Visible = str02; lblOver182.Visible = str02;
            txtOver14.Visible = str02; lblOver14.Visible = str02;
            txtLC.Visible = str02; lblLC.Visible = str02;
            txtPercentObjStemTips.Visible = str02; lblObjStem.Visible = str02;
            txtSystemFlag.Visible = str02; lblFlagAnalysis.Visible = str02;
            txtPackeddensityDVR.Visible = str02; lblPackedDensity.Visible = str02;
            txtRemarks.Visible = str02; lblRemarks.Visible = str02;
            lblOver11.Visible = str02; lblOverpan.Visible = str02;
            lblTotalstem.Visible = str02; lbltover12122.Visible = str02;
            txtTOver1212.Visible = str02; lblOver14.Visible = str02;
            lblPercentPan.Visible = str02; txtOverPan.Visible = str02;
            txtPercenOnPan.Visible = str02;
            lbl142.Visible = str02;
            txtPercentTotalStem.Visible = str02;
            btnSave.Visible = str02;
            btnClear.Visible = str02;
            btnBack.Visible = str02;
            lblSpan1.Visible = str02;
            lblSpan10.Visible = str02;
            lblSpan11.Visible = str02;
            lblSpan12.Visible = str02;
            lblSpan13.Visible = str02;
            lblSpan14.Visible = str02;
            lblSpan15.Visible = str02;
            lblSpan16.Visible = str02;
            lblSpan17.Visible = str02;
            lblSpan18.Visible = str02;
            lblSpan19.Visible = str02;
            lblSpan2.Visible = str02;
            lblSpan20.Visible = str02;
            lblSpan21.Visible = str02;
            lblSpan22.Visible = str02;
            lblSpan23.Visible = str02;
            lblSpan24.Visible = str02;
            lblSpan25.Visible = str02;
            lblSpan26.Visible = str02;
            lblSpan27.Visible = str02;
            lblSpan28.Visible = str02;
            lblSpan29.Visible = str02;
            lblSpan3.Visible = str02;
            lblSpan4.Visible = str02;
            lblSpan5.Visible = str02;
            lblSpan6.Visible = str02;
            lblSpan7.Visible = str02;
            lblSpan9.Visible = str02;

        }

        public void cleard()
        {
            //ClearText();
            // ddGrade.SelectedIndex = 0;
            txtDate.Text = "";
            txtTime.Text = "";
            txtRunNo.Text = "";
            txtCaseNo.Text = "";
            txtSampleWeight.Text = "";
            txtOver11.Text = "";
            txtOver1.Text = "";
            txtOver12.Text = "";
            txtOver1212.Text = "";
            txtTOver12.Text = "";
            txtOver14Second.Text = "";
            txtTOver14.Text = "";
            txtOver18.Text = "";
            txtOver18P.Text = "";
            txtFirstPass.Text = "";
            txtpercentSecondPass.Text = "";
            txtObj3_32.Text = "";
            txtObj3_32Second.Text = "";
            txtSloth07.Text = "";
            txtSloth12.Text = "";
            txtSlot07Second.Text = "";
            txtSlot12Second.Text = "";
            txtMesh12.Text = "";
            txtMesh12Second.Text = "";
            txtFiberHist.Text = "";
            txtFiberHistSecond.Text = "";
            txtNewFiber.Text = "";
            txtNewFiberSecond.Text = "";
            txtTsefHist.Text = "";
            txtTsefHistSecond.Text = "";
            txtTsefNew.Text = "";
            txtNew.Text = "";
            txtStem.Text = "";
            txtPercentStemTips.Text = "";
            txtSecondPass.Text = "";
            txtPercentFirstPass.Text = "";
            txtOver182.Text = "";
            txtOver14.Text = "";
            txtLC.Text = "";
            txtPercentObjStemTips.Text = "";
            txtSystemFlag.Text = "";
            txtPackeddensityDVR.Text = "";
            txtRemarks.Text = "";
            txtPercenOnPan.Text = "";
            txtPercentTotalStem.Text = "";
            txtOverPan.Text = "";
            txtTOver1212.Text = "";
        }
        protected void btnSaveGrid_Click(object sender, EventArgs e)
        {

            string Crop; string Variety; double Sampleweight; double Over1; double Over12; double TOver1212; double TOver12; double Over14Second; double TOver14;
            double Over182; double PercentOverPan; double Over18p; double PercentFirstPass; double SecondPass; double PercentSecondPass;
            double Obj3_32Second; double Slot07Second; double Mesh12Second; double FiberHistSecond; double TsefHist; double TsefHistSecond; double NewFiber;
            double NewFiberSecond; double TsefNew; double New; string Grade; string Date; string Sample_Time; int RunNo; int CaseNo; double Over11;
            double Over1212; double over14; double Over18; double OverPan; double First_Pass; double Obj3_32; double Slot07; double Slot12; double mesh12;
            double FiberHist; string TotalStemTips; string LC; string Stem; string PercentObjStem; string PercentSteminTips; string SysFlagAnalysis; string PackedDesity;
            string Remarks; double Slot12Second;
            int k = 1;

            SqlTransaction tran = null;
            tran = conn.BeginTransaction();
            for (int i = 0; i < GridViewSample.Rows.Count; i++)
            {
                try
                {
                    Crop = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblCrop")).Text;
                    Grade = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblGrade")).Text;
                    Variety = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblVariety")).Text;
                    Date = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblDate")).Text;
                    Sample_Time = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTime")).Text;
                    RunNo = Convert.ToInt32(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblRunNo")).Text);
                    CaseNo = Convert.ToInt32(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblCaseNo")).Text);
                    Sampleweight = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblSampleWeight")).Text);
                    Over11 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver11")).Text);
                    Over1 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver1")).Text);
                    Over1212 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver1212")).Text);
                    Over12 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver12")).Text);
                    TOver1212 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTOver1212")).Text);
                    TOver12 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTOver12")).Text);
                    over14 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver14")).Text);
                    Over14Second = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOVer14Second")).Text);
                    TOver14 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTOver14")).Text);
                    Over18 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver18")).Text);
                    Over182 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver18Second")).Text);
                    OverPan = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOverPan")).Text);
                    PercentOverPan = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblPercentOverPan")).Text);
                    Over18p = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOver18Percent")).Text);
                    First_Pass = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblOverFirstPass")).Text);
                    PercentFirstPass = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblPercentFirstPass")).Text);
                    SecondPass = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblSecondPass")).Text);
                    PercentSecondPass = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblPercentSecondPass")).Text);
                    Obj3_32 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblObj3_32")).Text);
                    Obj3_32Second = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblObj3_32Second")).Text);
                    Slot07 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblSlot07")).Text);
                    Slot07Second = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblSlot07Second")).Text);
                    Slot12 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblSlot12")).Text);
                    Slot12Second = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblslot12Second")).Text);
                    mesh12 = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblMesh12")).Text);
                    Mesh12Second = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblMesh12Second")).Text);
                    FiberHist = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblFiberHist")).Text);
                    FiberHistSecond = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblFiberHistSecond")).Text);
                    TsefHist = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTsef")).Text);
                    TsefHistSecond = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTsefHistSecond")).Text);
                    NewFiber = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblNewFiber")).Text);
                    NewFiberSecond = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblNewFiberSecond")).Text);
                    TsefNew = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTsefNew")).Text);
                    New = Convert.ToDouble(((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblNew")).Text);
                    TotalStemTips = (((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblTotalSteminTips")).Text);
                    LC = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblLC")).Text;
                    Stem = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblStem")).Text;
                    PercentObjStem = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblPercentageObjStem")).Text;
                    PercentSteminTips = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblPercentStemInTips")).Text;
                    SysFlagAnalysis = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblSystemFlag")).Text;
                    PackedDesity = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblPackedDensity")).Text;
                    Remarks = ((System.Web.UI.WebControls.Label)GridViewSample.Rows[i].FindControl("lblRemarks")).Text;





                    query = "INSERT INTO [dbo].[GPIL_RTQCR] ";
                    query += "([Crop],[Variety],[Grade],[Grade_Date] ,[Sample_Time] ,[RunNo],[CaseNo] ,[SampleWeight]";
                    query += ",[Over11] ,[Over1] ,[Over1212] ,[Over12] ,[TOver1212]   ,[TOver12],[Over14]  ,";
                    query += "[TOver14],[Over18],[Over182] ,[OverPan] ,[PercntOnPan],[Over18P]";
                    query += " ,[FirstPass],[PercentFirstPass] ,[SecondPass] ,[PercentSecondPass] ";
                    query += ",[Obj3_32] ,[Obj3_32Second] ,[Slot07],[Slot07Second] ,[Slot12],[Slot12Second] ";
                    query += ",[Mesh12] ,[Mesh12Second] ,[FiberHist] ,[FiberHistSecond]  ,[TsefHist] ,[TsefHistSecond] ,";
                    query += "[NewFiber] ,[NewFiberSecond],[TsefNew] ,[New] ,[TotalStemInTips] ,[LC] ,[Stem],[PercentObjStem] ,";
                    query += "[PercentStemTips]  ,[SystemFlagAnalysis] ,[PackedDensityDVR] ,[Remarks] ,[Over14Second])";
                    query += "VALUES('" + Crop + "','" + Variety + "','" + Grade + "',(select convert(varchar,'" + Date + "',103)),'" + Sample_Time + "','" + RunNo + "',";
                    query += "'" + CaseNo + "','" + Sampleweight + "','" + Over11 + "','" + Over1 + "','" + Over1212 + "',";
                    query += "'" + Over12 + "','" + TOver1212 + "','" + TOver12 + "','" + over14 + "','" + TOver14 + "',";
                    query += "'" + Over18 + "','" + Over182 + "','" + OverPan + "','" + PercentOverPan + "','" + Over18p + "',";
                    query += "'" + First_Pass + "','" + PercentFirstPass + "','" + SecondPass + "','" + PercentSecondPass + "',";
                    query += "'" + Obj3_32 + "','" + Obj3_32Second + "','" + Slot07 + "','" + Slot07Second + "','" + Slot12 + "','" + Slot12Second + "','" + mesh12 + "',";
                    query += "'" + Mesh12Second + "','" + FiberHist + "','" + FiberHistSecond + "','" + TsefHist + "','" + TsefHistSecond + "',";
                    query += "'" + NewFiber + "','" + NewFiberSecond + "','" + TsefNew + "','" + New + "','" + TotalStemTips + "','" + LC + "',";
                    query += "'" + Stem + "','" + PercentObjStem + "','" + PercentSteminTips + "','" + SysFlagAnalysis + "','" + PackedDesity + "','" + Remarks + "',";
                    query += "'" + Over14Second + "')";
                    cmd = new SqlCommand(query, conn);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    lblMessage.Text = ex.ToString();
                    lblMessage.Visible = true;
                    return;
                }

            }
            tran.Commit();
            lblMessage.Text = "Updated";
            lblMessage.Visible = true;
            //Alert.Show("Updated");
            GridViewSample.DataSource = null;
            GridViewSample.DataBind();

        }
        protected void gSaveG_Click(object sender, EventArgs e)
        {

        }

    }


}










