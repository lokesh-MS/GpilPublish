using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Form_StemReport_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString());

    public static string errfile;
    DataTable dt = new DataTable();
    public static DataTable dg = new DataTable();

    string Crop, Variety, LamiaGrade, Grade, Date, TimeStem;
    string CaseNo, TotalLength;
    int i = 0; int k = 0;
    string AvgStemLength, StemWeight, L3_32, NoofStemPieces, L3_32Percent, G3_32, G3_32Percent, SandnDust, SandnDustPercent, NoofTotalStemPieces, NoofL1_2StemPieces;
    string L1_2StemPiecesPercent, NoofG1_2L4StemPieces, G1_2to4StemPiecesPercent, NoofG4StemPieces, G4StemPiecesPercent;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            con.Open();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
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

            if (Session["SessionUserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            else
            {

                IEnumerator mc;
                mc = Request.Cookies.AllKeys.GetEnumerator();

                while (mc.MoveNext())
                {

                    if (Request.Cookies[mc.Current.ToString()].HasKeys == true)
                    {

                        IEnumerator sc;
                        sc = Request.Cookies[mc.Current.ToString()].Value.GetEnumerator();

                        while (sc.MoveNext())
                        {

                            //Response.Write(sc.Current.ToString() + Request.Cookies[mc.Current.ToString()][sc.Current.ToString()]); 
                        }
                    }
                }
            }
            if (!IsPostBack)
            {
                Binding();
                if (dg.Columns.Count == 0)
                {
                    dg.Columns.Add("Crop", typeof(string));
                    dg.Columns.Add("Variety", typeof(string));
                    dg.Columns.Add("LamiaGrade", typeof(string));
                    dg.Columns.Add("Grade", typeof(string));
                    dg.Columns.Add("Date", typeof(string));
                    dg.Columns.Add("CaseNo", typeof(string));
                    dg.Columns.Add("TimeStem", typeof(string));
                    dg.Columns.Add("TotalLength", typeof(string));
                    dg.Columns.Add("NoofStemPieces", typeof(string));
                    dg.Columns.Add("AvgStemLength", typeof(string));
                    dg.Columns.Add("StemWeight", typeof(string));
                    dg.Columns.Add("G3_32", typeof(string));
                    dg.Columns.Add("G3_32Percent", typeof(string));
                    dg.Columns.Add("L3_32", typeof(string));
                    dg.Columns.Add("L3_32Percent", typeof(string));
                    dg.Columns.Add("SandnDust", typeof(string));
                    dg.Columns.Add("SandnDustPercent", typeof(string));
                    dg.Columns.Add("NoofTotalStemPieces", typeof(string));
                    dg.Columns.Add("NoofL1_2StemPieces", typeof(string));
                    dg.Columns.Add("L1_2StemPiecesPercent", typeof(string));
                    dg.Columns.Add("NoofG1_2L4StemPieces", typeof(string));
                    dg.Columns.Add("G1_2to4StemPiecesPercent", typeof(string));
                    dg.Columns.Add("NoofG4StemPieces", typeof(string));
                    dg.Columns.Add("G4StemPiecesPercent", typeof(string));
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "alert('" + ex.Message + "');", true);
        }
    }










    //Download log files 

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
            lblMessage.Text = ex.Message;
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            download(Server.MapPath("LOGFILES\\"), errfile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    public void Binding()
    {
        //Crop
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }


            string query;
            SqlDataAdapter sda;
            DataTable dt = new DataTable();

            query = "select distinct[Crop] from [dbo].[GPIL_StemReports]";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            sda.Dispose();
            ddCrop.DataSource = dt;
            ddCrop.DataBind();
            ddCrop.DataTextField = "Crop";
           
            ddCrop.DataBind();
            ddCrop.Items.Insert(0, new ListItem("--Select--", "0"));


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
        //Scrap Variety
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }


            string query;
            SqlDataAdapter sda;
            DataTable dt = new DataTable();

            query = " select distinct[Variety] from [dbo].[GPIL_StemReports]";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            ddVariety.DataSource = dt;
            ddVariety.DataBind();
            sda.Dispose();
            ddVariety.DataTextField = "Variety";
            ddVariety.DataBind();
            ddVariety.Items.Insert(0, new ListItem("--Select--", "0"));


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

            query = "  select distinct[Grade] from [dbo].[GPIL_StemReports]";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            ddGrade.DataSource = dt;
            ddGrade.DataBind();
            sda.Dispose();
            ddGrade.DataTextField = "Grade";
            ddGrade.DataBind();
            ddGrade.Items.Insert(0, new ListItem("--Select--", "0"));


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
        //lamia grade
        try
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }


            string query;
            SqlDataAdapter sda;
            DataTable dt = new DataTable();

            query = "  select distinct[LamiaGrade] from [dbo].[GPIL_StemReports]";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            sda.Dispose();
            ddlamiaGrade.DataSource = dt;
            ddlamiaGrade.DataBind();
            ddlamiaGrade.DataTextField = "LamiaGrade";
            ddlamiaGrade.DataBind();
            ddlamiaGrade.Items.Insert(0, new ListItem("--Select--", "0"));


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dt.Clear();
            
            GridViewSample.DataSource = null;
            GridViewSample.DataBind();

            string query = "SELECT [Crop],[Variety],[LamiaGrade],[Grade],[Date]=(Convert(varchar,[Date],103)),[Time]=(Convert(varchar,[Time],108)),[CaseNo],[TotalLength]";
             query += ",[NoofStemPieces] ,[AvgStemLength],[StemWeight],[StemRotapG3_32],[StemRotapG3_32Percent]";
	         query +=" ,[StemRotapL3_32],[StemRotapL3_32Percent],[SandnDust]";
             query +=" ,[SandnDustPercent],[NoofTotalStemInPieces],[NoofL1_2Stems],[L1_2StemsPercent]";
             query += " ,[[L1_2_4Stems],[NoofL1_2toG4StemsPercent],[NoofG4Stems],[NoofG4StemPercent]";
             query += "FROM [dbo].[GPIL_StemReports] where   ";
             
            if (txt_Report_Date.Text != string.Empty && txt_Report_Date0.Text != string.Empty)
            {
                query += " [Date] between CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',103) and CONVERT(DATETIME,'" + txt_Report_Date0.Text + " 23:59:59',103) and";
            }
            
            if (ddCrop.SelectedItem.ToString() != "--Select--")
            {
                query += " [Crop]='" + ddCrop.SelectedItem.ToString() + "' and";
            }
            
            if (ddVariety.SelectedItem.ToString() != "--Select--")
            {
                query += " [Variety]='" + ddVariety.SelectedItem.ToString() + "' and";
            }
            
            if (ddGrade.SelectedItem.ToString() != "--Select--")
            {
                query += " [Grade]='" + ddGrade.SelectedItem.ToString() + "' and";
            }
            if (ddlamiaGrade.SelectedItem.ToString() != "--Select--")
            {
                query += " [LamiaGrade]='" + ddlamiaGrade.SelectedItem.ToString() + "' and";
            }
            query=query.Substring(0,query.Length-3);

            SqlCommand csm = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(csm);
            sda.SelectCommand.CommandTimeout = 0;
            sda.Fill(dt);

          //Crop
            // For Crystal reports 

            //ReportDocument rd = new ReportDocument();
            //rd.Load(Server.MapPath("~/Reports/RptChemistryReport.rpt"));

            //rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
            //rd.SetDataSource(dt);
            //CrystalReportViewer1.ReportSource = rd;
            //CrystalReportViewer1.RefreshReport();
            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "No Data Available";
            }
            for (int z = 0; z < dt.Rows.Count; z++)
            {
                Crop = Convert.ToString(dt.Rows[z][0].ToString());
                Variety = Convert.ToString(dt.Rows[z][1].ToString());
                LamiaGrade = Convert.ToString(dt.Rows[z][2].ToString());
                Grade = Convert.ToString(dt.Rows[z][3].ToString());
                Date = Convert.ToString(dt.Rows[z][4].ToString());
                TimeStem = Convert.ToString(dt.Rows[z][5].ToString());
                CaseNo = Convert.ToString(dt.Rows[z][6].ToString());
                TotalLength = Convert.ToString(dt.Rows[z][7].ToString());
                NoofStemPieces = Convert.ToString(dt.Rows[z][8].ToString());
                AvgStemLength = Convert.ToString(dt.Rows[z][9].ToString());
                StemWeight = Convert.ToString(dt.Rows[z][10].ToString());
                G3_32 = Convert.ToString(dt.Rows[z][11].ToString());
                G3_32Percent = Convert.ToString(dt.Rows[z][12].ToString());
                L3_32 = Convert.ToString(dt.Rows[z][13].ToString());
                L3_32Percent = Convert.ToString(dt.Rows[z][14].ToString());
                SandnDust = Convert.ToString(dt.Rows[z][15].ToString());
                SandnDustPercent = Convert.ToString(dt.Rows[z][16].ToString());
                NoofTotalStemPieces = Convert.ToString(dt.Rows[z][17].ToString());
                NoofL1_2StemPieces = Convert.ToString(dt.Rows[z][18].ToString());
                L1_2StemPiecesPercent = Convert.ToString(dt.Rows[z][19].ToString());
                NoofG1_2L4StemPieces = Convert.ToString(dt.Rows[z][20].ToString());
                G1_2to4StemPiecesPercent = Convert.ToString(dt.Rows[z][21].ToString());
                NoofG4StemPieces = Convert.ToString(dt.Rows[z][22].ToString());
                G4StemPiecesPercent = Convert.ToString(dt.Rows[z][23].ToString());


                //csm.Dispose();
                DataRow NewRow = dg.NewRow();
                NewRow[0] = Crop;
                NewRow[1] = Variety;
                NewRow[2] = LamiaGrade;
                NewRow[3] = Grade;
                NewRow[4] = Date;
                NewRow[5] = CaseNo;
                NewRow[6] = TimeStem;
                NewRow[7] = TotalLength;
                NewRow[8] = NoofStemPieces;
                NewRow[9] = AvgStemLength;
                NewRow[10] = StemWeight;
                NewRow[11] = G3_32;
                NewRow[12] = G3_32Percent;
                NewRow[13] = L3_32;
                NewRow[14] = L3_32Percent;
                NewRow[15] = SandnDust;
                NewRow[16] = SandnDustPercent;
                NewRow[17] = NoofTotalStemPieces;
                NewRow[18] = NoofL1_2StemPieces;
                NewRow[19] = L1_2StemPiecesPercent;
                NewRow[20] = NoofG1_2L4StemPieces;
                NewRow[21] = G1_2to4StemPiecesPercent;
                NewRow[22] = NoofG4StemPieces;
                NewRow[23] = G4StemPiecesPercent;
                //k = 0;   
                dg.Rows.Add(NewRow);
                // Alert.Show(dt.Rows.Count.ToString()); 
            }
            this.GridViewSample.Visible = true;
            GridViewSample.DataSource = dg;
            GridViewSample.DataBind();
            GridViewSample.EditIndex = -1;
        }
        catch (Exception es)
        {
            lblMessage.Visible = true;
            lblMessage.Text = es.ToString();
        }
    }
    public void viewrpt()
    {
    
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmHomePage.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Stem Length Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            
            //To Export all pages
            GridViewSample.AllowPaging = false;


            foreach (TableCell cell in GridViewSample.HeaderRow.Cells)
            {
                cell.BackColor = GridViewSample.HeaderStyle.BackColor;
            }
            //foreach (GridViewRow row in GridViewSample.Rows)
            //{

            //    foreach (TableCell cell in row.Cells)
            //    {
            //        if (row.RowIndex % 2 == 0)
            //        {
            //            cell.BackColor = GridViewSample.AlternatingRowStyle.BackColor;
            //        }
            //        else
            //        {
            //            //cell.BackColor = GridViewSample.RowStyle.BackColor;
            //        }
            //        cell.CssClass = "textmode";
            //    }
            //}
            GridViewSample.RenderControl(hw);
            string headerTable = @"<Table><tr><td/><td/><td/><td/><td/><td/><td/><td><b>STEM DEGRADATION REPORT</td></tr><tr><td>";
            if(txt_Report_Date.Text!="")
                headerTable += "From Date : " + txt_Report_Date.Text + "</b></td><td/><td/><td/><td>";
            if(txt_Report_Date0.Text!="")
                headerTable += "To Date : " + txt_Report_Date0.Text + "</td><td/><td/><td/><td>";
            if (ddCrop.SelectedItem.ToString() != "--Select--")
                headerTable += "Crop : " + ddCrop.SelectedItem.ToString() + "</td><td/><td/><td/><td>";
            if(ddGrade.SelectedItem.ToString()!="--Select--")
                headerTable += "Grade : " + ddGrade.SelectedItem.ToString() + "</td><td/><td/><td/><td>";
            if (ddlamiaGrade.SelectedItem.ToString() != "--Select--")
                headerTable += "Lamina Grade : " + ddlamiaGrade.SelectedItem.ToString() + "</td><td/><td/><td/><td>";
            if (ddVariety.SelectedItem.ToString() != "--Select--")
                headerTable += "Variety : " + ddVariety.SelectedItem.ToString() + "</td><td><br><br><br><br>";


            headerTable += "</b></td></tr></Table><br><br><br><br>";
            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Write(headerTable);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //verifies that the control is rendered
    }
}
