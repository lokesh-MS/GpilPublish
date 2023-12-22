using CrystalDecisions.CrystalReports.Engine;
using GPI;
using GPILWebApp.Controllers;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class ChemistryReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        public static string errfile;
        DataTable dt = new DataTable("DS_ChemistryReport");
        //forcrystalRep
        //testDataContext test;
        ReportDocument CustomerReport = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnexportexcel);
            if (!IsPostBack)
            {
               

                try
                {
                    TextBox1.Visible = false;
                    TextBox2.Visible = false;
                    TextBox3.Visible = false;
                    TextBox4.Visible = false;
                    TextBox5.Visible = false;
                    TextBox6.Visible = false;
                    TextBox7.Visible = false;
                    TextBox8.Visible = false;
                    TextBox9.Visible = false;
                    TextBox10.Visible = false;
                    TextBox11.Visible = false;
                    TextBox12.Visible = false;
                    TextBox13.Visible = false;
                    TextBox14.Visible = false;
                    TextBox15.Visible = false;
                    TextBox16.Visible = false;
                    TextBox17.Visible = false;
                    TextBox18.Visible = false;

                    bindDropDown();

                }
                catch (Exception ex)
                { }
            }
            else
            {
                //ReportBales(ddlOrgnCode.SelectedValue.ToString() + DateTime.Now.ToString("yyyyMMdd"), ddlFarmerCode.SelectedValue.ToString());
            }
        }
        string query = string.Empty;
        CommonManagement cMgt = new CommonManagement();
        private void bindDropDown()
        {

            DataTable dt = new DataTable();

            query = "select distinct [Crop] FROM [dbo].[GPIL_Chemistry_Reports]";
            dt = cMgt.GetQueryResult(query);
            ddCrop.DataSource = dt;
            ddCrop.DataBind();
            ddCrop.DataTextField = "Crop";
            ddCrop.DataBind();
            ddCrop.Items.Insert(0, new ListItem("---Select---", "0"));



            DataTable dt1 = new DataTable();

            query = "  select distinct [Variety] FROM [dbo].[GPIL_Chemistry_Reports]";
            dt1 = cMgt.GetQueryResult(query);

            ddVariety.DataSource = dt1;
            ddVariety.DataBind();
            ddVariety.DataTextField = "Variety";
            ddVariety.DataBind();
            ddVariety.Items.Insert(0, new ListItem("---Select---", "0"));


            DataTable dt2 = new DataTable();
            query = "select distinct [Grade] FROM [dbo].[GPIL_Chemistry_Reports]";
            dt2 = cMgt.GetQueryResult(query);
            //sda = new SqlDataAdapter(query, con);
            //sda.Fill(dt);
            ddGrade.DataSource = dt2;
            ddGrade.DataBind();
            ddGrade.DataTextField = "Grade";
            ddGrade.DataBind();
            ddGrade.Items.Insert(0, new ListItem("---Select---", "0"));



        }
        //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString());
        DataTable dt1 = new DataTable("DS_ChemistryReport");
        string CountRows, ACL, MINCL, MAXCL, ATRS, MINTRS, MAXTRS, ANIC, MINNIC, MAXNIC, AMoisture, MINMoisture, MAXMoisture;

        protected void btnexportexcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ChemistryReport" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls");
            Response.ContentType = "application/vnd.xls";
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridViewSample.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        double LCL = 0;
        double AVGLCL = 0;
        double UCL = 0;
        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                string crop, variety, grade, query01;
                double NIC = 0.0;
                con.Open();
                string query = "SELECT [Crop],[Variety],[Grade],[Mark],[SourceOrganisation],[Product],[Dom_Exp],[Type],[From_Run_No],[To_Run_No],[NIC],[TRS],[CL],[MoisturePercent],[DOP]=(Convert(varchar,[DOP],103)) FROM [dbo].[GPIL_Chemistry_Reports] where";
                string querycount = "SELECT total=(Count(*)),ANIC=ROUND(Avg([NIC]),2), ATRS=Round(Avg([TRS]),2),ACL=Round(Avg([CL]),2),AMMOIS=Round(Avg([MoisturePercent]),2),MINTRS=Min([TRS]),MAXTRS=Max([TRS]),MINCL=Min([CL]),MAXCL=Max([CL]),MINMOIS=Min([MoisturePercent]),MAXMOIS=Max([MoisturePercent]),MINNIC=MIN([NIC]),MAXNIC=MAX([NIC]) FROM [dbo].[GPIL_Chemistry_Reports] where ";


                if (txt_Report_Date.Text != string.Empty && txt_Report_Date0.Text != string.Empty)
                {
                    query = query + " [DOP] between CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) and CONVERT(DATETIME,'" + txt_Report_Date0.Text + " 23:59:59',102) and";
                    querycount += " [DOP] between CONVERT(DATETIME,'" + txt_Report_Date.Text + " 00:00:00',102) and CONVERT(DATETIME,'" + txt_Report_Date0.Text + " 23:59:59',102) and";
                }

                if (ddCrop.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Crop]='" + ddCrop.SelectedItem.ToString() + "' and";
                    querycount += " [Crop]='" + ddCrop.SelectedItem.ToString() + "' and";
                }

                if (ddVariety.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Variety]='" + ddVariety.SelectedItem.ToString() + "' and";
                    querycount += " [Variety]='" + ddVariety.SelectedItem.ToString() + "' and";
                }

                if (ddGrade.SelectedItem.ToString() != "---Select---")
                {
                    query += " [Grade]='" + ddGrade.SelectedItem.ToString() + "' and";
                    querycount += " [Grade]='" + ddGrade.SelectedItem.ToString() + "' and";
                }

                query = query.Substring(0, query.Length - 3);
                querycount = querycount.Substring(0, querycount.Length - 3);
                query += " ORDER BY From_Run_No";
                //chemistry report section
                DS_Chemistry_Reports dsc = new DS_Chemistry_Reports();
                SqlCommand command = new SqlCommand(query, con);
                command.CommandTimeout = 0;
                //SqlCommand csm = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dsc, "DS_ChemistryReport");
                DataSet ds = new DataSet();
                sda.Fill(ds);


                DataView view = new DataView(ds.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "Grade", "CROP");

                DataTable dtSummary = new DataTable();
                dtSummary.Columns.Add("Grade", typeof(string));
                dtSummary.Columns.Add("NoOfPacked", typeof(int));
                dtSummary.Columns.Add("SpecificationRange", typeof(string));
                dtSummary.Columns.Add("ControlRange", typeof(string));
                dtSummary.Columns.Add("WithinSpecLimit", typeof(int));
                dtSummary.Columns.Add("WithinSpecPer", typeof(double));
                dtSummary.Columns.Add("WithinControlLimit", typeof(int));
                dtSummary.Columns.Add("WithinControlPer", typeof(double));
                dtSummary.Columns.Add("BelowControlLimit", typeof(int));
                dtSummary.Columns.Add("BelowControlPer", typeof(double));
                dtSummary.Columns.Add("AboveControlLimit", typeof(int));
                dtSummary.Columns.Add("AboveControlPer", typeof(double));
                dtSummary.Columns.Add("AlkaloidPer", typeof(double));
                dtSummary.Columns.Add("TRSPer", typeof(double));
                dtSummary.Columns.Add("CLPer", typeof(double));

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    string dstGrade = distinctValues.Rows[i][0].ToString();
                    string CROP = distinctValues.Rows[i][1].ToString();
                    int totalcount;
                    double lcl, ucl, lsl, usl;
                    lsl = usl = lcl = ucl = 0;
                    SqlCommand cmdGetTarget = new SqlCommand("SELECT lcl,ucl,lsl,usl from gpil_chemical_targets where grade='" + dstGrade + "' and crop='" + CROP + "'", con);
                    SqlDataReader rdTarget = cmdGetTarget.ExecuteReader();
                    if (rdTarget.Read())
                    {
                        lcl = Convert.ToDouble(rdTarget.GetValue(0));
                        ucl = Convert.ToDouble(rdTarget.GetValue(1));
                        lsl = Convert.ToDouble(rdTarget.GetValue(2));
                        usl = Convert.ToDouble(rdTarget.GetValue(3));
                    }
                    rdTarget.Close();
                    rdTarget.Dispose();
                    cmdGetTarget.Dispose();
                    DataRow[] dr = ds.Tables[0].Select("Grade='" + dstGrade + "'");
                    totalcount = dr.Length;
                    int inControl, BelowControl, AboveControl;
                    inControl = BelowControl = AboveControl = 0;
                    for (int cnt = 0; cnt < dr.Length; cnt++)
                    {
                        if (Convert.ToDouble(dr[cnt][10]) > ucl)
                            AboveControl += 1;
                        else if (Convert.ToDouble(dr[cnt][10]) < lcl)
                            BelowControl += 1;
                        else
                            inControl += 1;
                    }

                    double avgNIC, avgTRS, avgCL;
                    avgCL = avgNIC = avgTRS = 0.0;

                    DataRow[] drAvg = ds.Tables[0].Select("GRADE='" + dstGrade + "' AND CROP='" + CROP + "'");

                    for (int cntavg = 0; cntavg < drAvg.Length; cntavg++)
                    {
                        avgCL += Convert.ToDouble(drAvg[cntavg][12]);
                        avgTRS += Convert.ToDouble(drAvg[cntavg][11]);
                        avgNIC += Convert.ToDouble(drAvg[cntavg][10]);

                    }
                    avgCL = System.Math.Round(avgCL / drAvg.Length, 2);
                    avgNIC = System.Math.Round(avgNIC / drAvg.Length, 2);
                    avgTRS = System.Math.Round(avgTRS / drAvg.Length, 2);

                    //var avgNIC = ds.Tables[0].AsEnumerable().Where(x => x["grade"] == dstGrade && x["crop"] == CROP).Average(x => x.Field<double>("NIC"));
                    //var avgTRS = ds.Tables[0].AsEnumerable().Where(x => x["grade"] == dstGrade && x["crop"] == CROP).Average(x => x.Field<double>("TRS"));
                    //var avgCL = ds.Tables[0].AsEnumerable().Where(x => x["grade"] == dstGrade && x["crop"] == CROP).Average(x => x.Field<double>("CL"));

                    DataRow rwSummary = dtSummary.NewRow();
                    rwSummary[0] = dstGrade;
                    rwSummary[1] = totalcount;
                    rwSummary[2] = lsl.ToString() + "-" + usl.ToString();
                    rwSummary[3] = lcl.ToString() + "-" + ucl.ToString();
                    rwSummary[4] = totalcount;
                    rwSummary[5] = 100.0;
                    rwSummary[6] = inControl;
                    rwSummary[7] = Convert.ToDouble((((inControl * 1.0) / (totalcount * 1.0)) * 100.0).ToString("N2"));
                    rwSummary[8] = BelowControl;
                    rwSummary[9] = Convert.ToDouble((((BelowControl * 1.0) / (totalcount * 1.0)) * 100.0).ToString("N2"));
                    rwSummary[10] = AboveControl;
                    rwSummary[11] = Convert.ToDouble((((AboveControl * 1.0) / (totalcount * 1.0)) * 100.0).ToString("N2"));
                    rwSummary[12] = avgNIC;
                    rwSummary[13] = avgTRS;
                    rwSummary[14] = avgCL;
                    dtSummary.Rows.Add(rwSummary);
                }

                GridViewSample.DataSource = dtSummary;
                GridViewSample.DataBind();
                GridViewSample.Visible = true;
                //Alert.Show(dtSummary.Rows.Count.ToString());

                // sda.SelectCommand.CommandTimeout = 0;

                sda.Dispose();
                SqlCommand cmd = new SqlCommand(querycount, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    CountRows = sdr["total"].ToString();
                    ANIC = sdr["ANIC"].ToString();
                    ATRS = sdr["ATRS"].ToString();
                    ACL = sdr["ACL"].ToString();
                    AMoisture = sdr["AMMOIS"].ToString();
                    MINTRS = sdr["MINTRS"].ToString();
                    MAXTRS = sdr["MAXTRS"].ToString();
                    MINCL = sdr["MINCL"].ToString();
                    MAXCL = sdr["MAXCL"].ToString();
                    MINMoisture = sdr["MINMOIS"].ToString();
                    MAXMoisture = sdr["MAXMOIS"].ToString();
                    MINNIC = sdr["MINNIC"].ToString();
                    MAXNIC = sdr["MAXNIC"].ToString();

                    ///To finish


                }
                cmd.Dispose();
                sdr.Dispose();
                TextBox1.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                for (int z = 0; z < ds.Tables[0].Rows.Count; z++)
                {

                    DataRow dtr = ds.Tables[0].Rows[z];
                    crop = Convert.ToString(dtr["Crop"]);
                    variety = Convert.ToString(dtr["Variety"]);
                    grade = Convert.ToString(dtr["Grade"]);
                    NIC = Convert.ToDouble(dtr["NIC"]);

                    query01 = "select [LCL],[AVEC],[UCL] from [dbo].[GPIL_Chemical_Targets] where [Crop]='" + crop + "' and [Variety]='" + variety + "'and [Grade]='" + grade + "'";
                    ///// use inside for loop
                    cmd = new SqlCommand(query01, con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        LCL = Convert.ToDouble(sdr["LCL"].ToString());
                        AVGLCL = Convert.ToDouble(sdr["AVEC"].ToString());
                        UCL = Convert.ToDouble(sdr["UCL"].ToString());


                    }
                    cmd.Dispose();
                    sdr.Dispose();
                    if (NIC < LCL)
                    {
                        TextBox14.Text = Convert.ToString(Convert.ToInt32(TextBox14.Text) + 1);
                    }
                    else if (NIC > UCL)
                    {
                        TextBox15.Text = Convert.ToString(Convert.ToInt32(TextBox15.Text) + 1);
                    }
                    else
                    {
                        TextBox17.Text = Convert.ToString(Convert.ToInt32(TextBox17.Text) + 1);
                    }

                }


                TextBox2.Text = ANIC;
                TextBox3.Text = ATRS;
                TextBox4.Text = ACL;
                TextBox5.Text = AMoisture;
                TextBox6.Text = MINTRS;
                TextBox7.Text = MAXTRS;
                TextBox8.Text = MINCL;
                TextBox9.Text = MAXCL;
                TextBox10.Text = MINMoisture;
                TextBox11.Text = MAXMoisture;
                TextBox12.Text = MINNIC;
                TextBox13.Text = MAXNIC;

                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/Reports/RptChemistryReport.rpt"));

                rd.SetDataSource(ds.Tables[0]);
                rd.SetParameterValue("AVENIC", TextBox2.Text);
                rd.SetParameterValue("AVETRS", TextBox3.Text);
                rd.SetParameterValue("AVECL", TextBox4.Text);
                rd.SetParameterValue("AVGMOISTURE", TextBox5.Text);
                rd.SetParameterValue("MINTRS", TextBox6.Text);
                rd.SetParameterValue("MAXTRS", TextBox7.Text);
                rd.SetParameterValue("MINCL", TextBox8.Text);
                rd.SetParameterValue("MAXCL", TextBox9.Text);
                rd.SetParameterValue("MINMOISTURE", TextBox10.Text);
                rd.SetParameterValue("MAXMOISTURE", TextBox11.Text);
                rd.SetParameterValue("MINNIC", TextBox12.Text);
                rd.SetParameterValue("MAXNIC", TextBox13.Text);
                rd.SetParameterValue("REM", TextBox17.Text);
                rd.SetParameterValue("TOT", TextBox1.Text);
                rd.SetParameterValue("InRange", TextBox15.Text);
                rd.SetParameterValue("LCL", TextBox16.Text);
                rd.SetParameterValue("LLCL", TextBox14.Text);
                rd.SetParameterValue("UCLH", TextBox15.Text);

                if (txt_Report_Date.Text.Trim() == "")
                {
                    rd.SetParameterValue("TXTFROMDATE", "NA");
                }
                else
                {
                    rd.SetParameterValue("TXTFROMDATE", txt_Report_Date.Text.Trim());

                }

                if (txt_Report_Date0.Text.Trim() == "")
                {
                    rd.SetParameterValue("TXTTODATE", "NA");
                }
                else
                {
                    rd.SetParameterValue("TXTTODATE", txt_Report_Date0.Text.Trim());

                }
                if (ddCrop.SelectedItem.ToString() == "---Select---")
                {
                    rd.SetParameterValue("TXTCROP", "NA");
                }
                else
                {
                    rd.SetParameterValue("TXTCROP", ddCrop.SelectedItem.ToString());
                }

                if (ddVariety.SelectedItem.ToString() == "---Select---")
                {
                    rd.SetParameterValue("TXTVARIETY", "NA");
                }
                else
                {
                    rd.SetParameterValue("TXTVARIETY", ddVariety.SelectedItem.ToString());
                }

                if (ddGrade.SelectedItem.ToString() == "---Select---")
                {
                    rd.SetParameterValue("TXTGRADE", "NA");
                }
                else
                {
                    rd.SetParameterValue("TXTGRADE", ddGrade.SelectedItem.ToString());
                }

                rd.SetDatabaseLogon(ClsConnection.USRID, ClsConnection.PASSWORD, ClsConnection.SERVERIP, ClsConnection.DBNAME);
                CrystalReportViewer1.ReportSource = rd;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.SeparatePages = false;
                con.Close();
            }
            catch (Exception es)
            {
                //lblMessage.Visible = true;
                //lblMessage.Text = es.ToString();
            }
            finally
            {
                // Ensure the connection is closed even if an exception occurs
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
                    TextBox15.Text = "0";
            TextBox16.Text = "0";
            TextBox14.Text = "0";
            TextBox17.Text = "0";

        }

        protected void btnclose_Click(object sender, EventArgs e)
        {

        }
    }


  
    

    public partial class DS_Chemistry_Reports : global::System.Data.DataSet
    {

        private GPIL_Chemistry_ReportsDataTable tableGPIL_Chemistry_Reports;

        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DS_Chemistry_Reports()
        {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected DS_Chemistry_Reports(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
                base(info, context, false)
        {
            if ((this.IsBinarySerialized(info, context) == true))
            {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema))
            {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["GPIL_Chemistry_Reports"] != null))
                {
                    base.Tables.Add(new GPIL_Chemistry_ReportsDataTable(ds.Tables["GPIL_Chemistry_Reports"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public GPIL_Chemistry_ReportsDataTable GPIL_Chemistry_Reports
        {
            get
            {
                return this.tableGPIL_Chemistry_Reports;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode
        {
            get
            {
                return this._schemaSerializationMode;
            }
            set
            {
                this._schemaSerializationMode = value;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables
        {
            get
            {
                return base.Tables;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations
        {
            get
            {
                return base.Relations;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet()
        {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone()
        {
            DS_Chemistry_Reports cln = ((DS_Chemistry_Reports)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader)
        {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema))
            {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["GPIL_Chemistry_Reports"] != null))
                {
                    base.Tables.Add(new GPIL_Chemistry_ReportsDataTable(ds.Tables["GPIL_Chemistry_Reports"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                this.ReadXml(reader);
                this.InitVars();
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable()
        {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars()
        {
            this.InitVars(true);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable)
        {
            this.tableGPIL_Chemistry_Reports = ((GPIL_Chemistry_ReportsDataTable)(base.Tables["GPIL_Chemistry_Reports"]));
            if ((initTable == true))
            {
                if ((this.tableGPIL_Chemistry_Reports != null))
                {
                    this.tableGPIL_Chemistry_Reports.InitVars();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass()
        {
            this.DataSetName = "DS_Chemistry_Reports";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/DataSet1.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableGPIL_Chemistry_Reports = new GPIL_Chemistry_ReportsDataTable();
            base.Tables.Add(this.tableGPIL_Chemistry_Reports);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeGPIL_Chemistry_Reports()
        {
            return false;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e)
        {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove))
            {
                this.InitVars();
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs)
        {
            DS_Chemistry_Reports ds = new DS_Chemistry_Reports();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace))
            {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try
                {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext();)
                    {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length))
                        {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length)
                                        && (s1.ReadByte() == s2.ReadByte()));)
                            {
                                ;
                            }
                            if ((s1.Position == s1.Length))
                            {
                                return type;
                            }
                        }
                    }
                }
                finally
                {
                    if ((s1 != null))
                    {
                        s1.Close();
                    }
                    if ((s2 != null))
                    {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }

        public delegate void GPIL_Chemistry_ReportsRowChangeEventHandler(object sender, GPIL_Chemistry_ReportsRowChangeEvent e);

        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class GPIL_Chemistry_ReportsDataTable : global::System.Data.TypedTableBase<GPIL_Chemistry_ReportsRow>
        {

            private global::System.Data.DataColumn columnDOP;

            private global::System.Data.DataColumn columnCrop;

            private global::System.Data.DataColumn columnVariety;

            private global::System.Data.DataColumn columnGrade;

            private global::System.Data.DataColumn columnMark;

            private global::System.Data.DataColumn columnSourceOrganisation;

            private global::System.Data.DataColumn columnProduct;

            private global::System.Data.DataColumn columnDom_Exp;

            private global::System.Data.DataColumn columnType;

            private global::System.Data.DataColumn columnFrom_Run_No;

            private global::System.Data.DataColumn columnTo_Run_No;

            private global::System.Data.DataColumn columnNIC;

            private global::System.Data.DataColumn columnTRS;

            private global::System.Data.DataColumn columnCL;

            private global::System.Data.DataColumn columnMoisturePercent;

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public GPIL_Chemistry_ReportsDataTable()
            {
                this.TableName = "GPIL_Chemistry_Reports";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal GPIL_Chemistry_ReportsDataTable(global::System.Data.DataTable table)
            {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive))
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
                {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace))
                {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected GPIL_Chemistry_ReportsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
                    base(info, context)
            {
                this.InitVars();
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn DOPColumn
            {
                get
                {
                    return this.columnDOP;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CropColumn
            {
                get
                {
                    return this.columnCrop;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn VarietyColumn
            {
                get
                {
                    return this.columnVariety;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn GradeColumn
            {
                get
                {
                    return this.columnGrade;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn MarkColumn
            {
                get
                {
                    return this.columnMark;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn SourceOrganisationColumn
            {
                get
                {
                    return this.columnSourceOrganisation;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn ProductColumn
            {
                get
                {
                    return this.columnProduct;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn Dom_ExpColumn
            {
                get
                {
                    return this.columnDom_Exp;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn TypeColumn
            {
                get
                {
                    return this.columnType;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn From_Run_NoColumn
            {
                get
                {
                    return this.columnFrom_Run_No;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn To_Run_NoColumn
            {
                get
                {
                    return this.columnTo_Run_No;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn NICColumn
            {
                get
                {
                    return this.columnNIC;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn TRSColumn
            {
                get
                {
                    return this.columnTRS;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CLColumn
            {
                get
                {
                    return this.columnCL;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn MoisturePercentColumn
            {
                get
                {
                    return this.columnMoisturePercent;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Browsable(false)]
            public int Count
            {
                get
                {
                    return this.Rows.Count;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public GPIL_Chemistry_ReportsRow this[int index]
            {
                get
                {
                    return ((GPIL_Chemistry_ReportsRow)(this.Rows[index]));
                }
            }

            public event GPIL_Chemistry_ReportsRowChangeEventHandler GPIL_Chemistry_ReportsRowChanging;

            public event GPIL_Chemistry_ReportsRowChangeEventHandler GPIL_Chemistry_ReportsRowChanged;

            public event GPIL_Chemistry_ReportsRowChangeEventHandler GPIL_Chemistry_ReportsRowDeleting;

            public event GPIL_Chemistry_ReportsRowChangeEventHandler GPIL_Chemistry_ReportsRowDeleted;

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddGPIL_Chemistry_ReportsRow(GPIL_Chemistry_ReportsRow row)
            {
                this.Rows.Add(row);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public GPIL_Chemistry_ReportsRow AddGPIL_Chemistry_ReportsRow(System.DateTime DOP, string Crop, string Variety, string Grade, string Mark, string SourceOrganisation, string Product, string Dom_Exp, string Type, int From_Run_No, int To_Run_No, double NIC, double TRS, double CL, double MoisturePercent)
            {
                GPIL_Chemistry_ReportsRow rowGPIL_Chemistry_ReportsRow = ((GPIL_Chemistry_ReportsRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                    DOP,
                    Crop,
                    Variety,
                    Grade,
                    Mark,
                    SourceOrganisation,
                    Product,
                    Dom_Exp,
                    Type,
                    From_Run_No,
                    To_Run_No,
                    NIC,
                    TRS,
                    CL,
                    MoisturePercent};
                rowGPIL_Chemistry_ReportsRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowGPIL_Chemistry_ReportsRow);
                return rowGPIL_Chemistry_ReportsRow;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone()
            {
                GPIL_Chemistry_ReportsDataTable cln = ((GPIL_Chemistry_ReportsDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance()
            {
                return new GPIL_Chemistry_ReportsDataTable();
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars()
            {
                this.columnDOP = base.Columns["DOP"];
                this.columnCrop = base.Columns["Crop"];
                this.columnVariety = base.Columns["Variety"];
                this.columnGrade = base.Columns["Grade"];
                this.columnMark = base.Columns["Mark"];
                this.columnSourceOrganisation = base.Columns["SourceOrganisation"];
                this.columnProduct = base.Columns["Product"];
                this.columnDom_Exp = base.Columns["Dom_Exp"];
                this.columnType = base.Columns["Type"];
                this.columnFrom_Run_No = base.Columns["From_Run_No"];
                this.columnTo_Run_No = base.Columns["To_Run_No"];
                this.columnNIC = base.Columns["NIC"];
                this.columnTRS = base.Columns["TRS"];
                this.columnCL = base.Columns["CL"];
                this.columnMoisturePercent = base.Columns["MoisturePercent"];
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass()
            {
                this.columnDOP = new global::System.Data.DataColumn("DOP", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDOP);
                this.columnCrop = new global::System.Data.DataColumn("Crop", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCrop);
                this.columnVariety = new global::System.Data.DataColumn("Variety", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnVariety);
                this.columnGrade = new global::System.Data.DataColumn("Grade", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnGrade);
                this.columnMark = new global::System.Data.DataColumn("Mark", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnMark);
                this.columnSourceOrganisation = new global::System.Data.DataColumn("SourceOrganisation", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnSourceOrganisation);
                this.columnProduct = new global::System.Data.DataColumn("Product", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnProduct);
                this.columnDom_Exp = new global::System.Data.DataColumn("Dom_Exp", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDom_Exp);
                this.columnType = new global::System.Data.DataColumn("Type", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnType);
                this.columnFrom_Run_No = new global::System.Data.DataColumn("From_Run_No", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnFrom_Run_No);
                this.columnTo_Run_No = new global::System.Data.DataColumn("To_Run_No", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTo_Run_No);
                this.columnNIC = new global::System.Data.DataColumn("NIC", typeof(double), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnNIC);
                this.columnTRS = new global::System.Data.DataColumn("TRS", typeof(double), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTRS);
                this.columnCL = new global::System.Data.DataColumn("CL", typeof(double), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCL);
                this.columnMoisturePercent = new global::System.Data.DataColumn("MoisturePercent", typeof(double), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnMoisturePercent);
                this.columnDOP.AllowDBNull = false;
                this.columnCrop.AllowDBNull = false;
                this.columnCrop.MaxLength = 6;
                this.columnVariety.AllowDBNull = false;
                this.columnVariety.MaxLength = 20;
                this.columnGrade.AllowDBNull = false;
                this.columnGrade.MaxLength = 20;
                this.columnMark.AllowDBNull = false;
                this.columnMark.MaxLength = 6;
                this.columnSourceOrganisation.MaxLength = 30;
                this.columnProduct.AllowDBNull = false;
                this.columnProduct.MaxLength = 10;
                this.columnDom_Exp.AllowDBNull = false;
                this.columnDom_Exp.MaxLength = 20;
                this.columnType.MaxLength = 10;
                this.columnFrom_Run_No.AllowDBNull = false;
                this.columnTo_Run_No.AllowDBNull = false;
                this.columnNIC.AllowDBNull = false;
                this.columnTRS.AllowDBNull = false;
                this.columnCL.AllowDBNull = false;
                this.columnMoisturePercent.AllowDBNull = false;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public GPIL_Chemistry_ReportsRow NewGPIL_Chemistry_ReportsRow()
            {
                return ((GPIL_Chemistry_ReportsRow)(this.NewRow()));
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder)
            {
                return new GPIL_Chemistry_ReportsRow(builder);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType()
            {
                return typeof(GPIL_Chemistry_ReportsRow);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if ((this.GPIL_Chemistry_ReportsRowChanged != null))
                {
                    this.GPIL_Chemistry_ReportsRowChanged(this, new GPIL_Chemistry_ReportsRowChangeEvent(((GPIL_Chemistry_ReportsRow)(e.Row)), e.Action));
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if ((this.GPIL_Chemistry_ReportsRowChanging != null))
                {
                    this.GPIL_Chemistry_ReportsRowChanging(this, new GPIL_Chemistry_ReportsRowChangeEvent(((GPIL_Chemistry_ReportsRow)(e.Row)), e.Action));
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if ((this.GPIL_Chemistry_ReportsRowDeleted != null))
                {
                    this.GPIL_Chemistry_ReportsRowDeleted(this, new GPIL_Chemistry_ReportsRowChangeEvent(((GPIL_Chemistry_ReportsRow)(e.Row)), e.Action));
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if ((this.GPIL_Chemistry_ReportsRowDeleting != null))
                {
                    this.GPIL_Chemistry_ReportsRowDeleting(this, new GPIL_Chemistry_ReportsRowChangeEvent(((GPIL_Chemistry_ReportsRow)(e.Row)), e.Action));
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveGPIL_Chemistry_ReportsRow(GPIL_Chemistry_ReportsRow row)
            {
                this.Rows.Remove(row);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs)
            {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                DS_Chemistry_Reports ds = new DS_Chemistry_Reports();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "GPIL_Chemistry_ReportsDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace))
                {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try
                    {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext();)
                        {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length))
                            {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length)
                                            && (s1.ReadByte() == s2.ReadByte()));)
                                {
                                    ;
                                }
                                if ((s1.Position == s1.Length))
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if ((s1 != null))
                        {
                            s1.Close();
                        }
                        if ((s2 != null))
                        {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }

        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class GPIL_Chemistry_ReportsRow : global::System.Data.DataRow
        {

            private GPIL_Chemistry_ReportsDataTable tableGPIL_Chemistry_Reports;

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal GPIL_Chemistry_ReportsRow(global::System.Data.DataRowBuilder rb) :
                    base(rb)
            {
                this.tableGPIL_Chemistry_Reports = ((GPIL_Chemistry_ReportsDataTable)(this.Table));
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.DateTime DOP
            {
                get
                {
                    return ((global::System.DateTime)(this[this.tableGPIL_Chemistry_Reports.DOPColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.DOPColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Crop
            {
                get
                {
                    return ((string)(this[this.tableGPIL_Chemistry_Reports.CropColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.CropColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Variety
            {
                get
                {
                    return ((string)(this[this.tableGPIL_Chemistry_Reports.VarietyColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.VarietyColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Grade
            {
                get
                {
                    return ((string)(this[this.tableGPIL_Chemistry_Reports.GradeColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.GradeColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Mark
            {
                get
                {
                    return ((string)(this[this.tableGPIL_Chemistry_Reports.MarkColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.MarkColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SourceOrganisation
            {
                get
                {
                    try
                    {
                        return ((string)(this[this.tableGPIL_Chemistry_Reports.SourceOrganisationColumn]));
                    }
                    catch (global::System.InvalidCastException e)
                    {
                        throw new global::System.Data.StrongTypingException("The value for column \'SourceOrganisation\' in table \'GPIL_Chemistry_Reports\' is DB" +
                                "Null.", e);
                    }
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.SourceOrganisationColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Product
            {
                get
                {
                    return ((string)(this[this.tableGPIL_Chemistry_Reports.ProductColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.ProductColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Dom_Exp
            {
                get
                {
                    return ((string)(this[this.tableGPIL_Chemistry_Reports.Dom_ExpColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.Dom_ExpColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Type
            {
                get
                {
                    try
                    {
                        return ((string)(this[this.tableGPIL_Chemistry_Reports.TypeColumn]));
                    }
                    catch (global::System.InvalidCastException e)
                    {
                        throw new global::System.Data.StrongTypingException("The value for column \'Type\' in table \'GPIL_Chemistry_Reports\' is DBNull.", e);
                    }
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.TypeColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int From_Run_No
            {
                get
                {
                    return ((int)(this[this.tableGPIL_Chemistry_Reports.From_Run_NoColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.From_Run_NoColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int To_Run_No
            {
                get
                {
                    return ((int)(this[this.tableGPIL_Chemistry_Reports.To_Run_NoColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.To_Run_NoColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public double NIC
            {
                get
                {
                    return ((double)(this[this.tableGPIL_Chemistry_Reports.NICColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.NICColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public double TRS
            {
                get
                {
                    return ((double)(this[this.tableGPIL_Chemistry_Reports.TRSColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.TRSColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public double CL
            {
                get
                {
                    return ((double)(this[this.tableGPIL_Chemistry_Reports.CLColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.CLColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public double MoisturePercent
            {
                get
                {
                    return ((double)(this[this.tableGPIL_Chemistry_Reports.MoisturePercentColumn]));
                }
                set
                {
                    this[this.tableGPIL_Chemistry_Reports.MoisturePercentColumn] = value;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSourceOrganisationNull()
            {
                return this.IsNull(this.tableGPIL_Chemistry_Reports.SourceOrganisationColumn);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSourceOrganisationNull()
            {
                this[this.tableGPIL_Chemistry_Reports.SourceOrganisationColumn] = global::System.Convert.DBNull;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsTypeNull()
            {
                return this.IsNull(this.tableGPIL_Chemistry_Reports.TypeColumn);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetTypeNull()
            {
                this[this.tableGPIL_Chemistry_Reports.TypeColumn] = global::System.Convert.DBNull;
            }
        }

        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class GPIL_Chemistry_ReportsRowChangeEvent : global::System.EventArgs
        {

            private GPIL_Chemistry_ReportsRow eventRow;

            private global::System.Data.DataRowAction eventAction;

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public GPIL_Chemistry_ReportsRowChangeEvent(GPIL_Chemistry_ReportsRow row, global::System.Data.DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public GPIL_Chemistry_ReportsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }
        }
    }
}