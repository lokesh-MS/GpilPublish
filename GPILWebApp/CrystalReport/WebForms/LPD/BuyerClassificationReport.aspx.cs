using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using GPILWebApp.ViewModel;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace GPILWebApp
{
    public partial class BuyerClassificationReport : System.Web.UI.Page
    {

        string strsql;
        ReportDocument CustomerReport = new ReportDocument();
        CrystalReportData crd = new CrystalReportData();
        LPDManagementt lpdMgt = new LPDManagementt();

        string varStrValue = "D.NET_WT * D.RATE";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDropDown();
                }

                else
                {
                    if (rdbcomplete.Checked == true)
                    {
                        viewrpt();
                    }
                    else
                    {
                        viewrpt2();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        private void bindDropDown()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = crd.GetORGN("ORGN", "", "");
                cbxorgcd.DataSource = ds.Tables[0];
                cbxorgcd.DataTextField = "ORGN_CODE1";
                cbxorgcd.DataValueField = "ORGN_CODE";
                cbxorgcd.DataBind();
                cbxorgcd.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds1 = new DataSet();
                ds1 = crd.GetORGN("CROP", "", "");
                cbxcrop.DataSource = ds1.Tables[0];
                cbxcrop.DataTextField = "CROPYEAR";
                cbxcrop.DataValueField = "CROP";
                cbxcrop.DataBind();
                cbxcrop.Items.Insert(0, new ListItem("- Select -", "0"));
                DataSet ds2 = new DataSet();
                ds2 = crd.GetORGN("VARIETY", "", "");
                cbxvariety.DataSource = ds2.Tables[0];
                cbxvariety.DataTextField = "VARIETYNAME";
                cbxvariety.DataValueField = "VARIETY";
                cbxvariety.DataBind();
                cbxvariety.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch (Exception ex)
            {

            }
        }
        public string a, b;
        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (validated())
                {
                    if (rdbcomplete.Checked == true)
                    {
                        viewrpt();
                    }
                    else
                    {
                        viewrpt2();
                    }
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
        }

        public bool validated()
        {
            if (txt_Report_Date.Text == "DD-MM-YYYY" || txt_Report_Date.Text =="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select From Date');", true);
                txt_Report_Date.Focus();
                return false;
            }
            else if (txttodate.Text == "DD-MM-YYYY" || txttodate.Text =="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select To Date');", true);
                txttodate.Focus();
                return false;
            }
            else if (cbxcrop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
                cbxcrop.Focus();
                return false;
            }
            //else if (cbxorgcd.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Crop Year');", true);
            //    cbxcrop.Focus();
            //    return false;
            //}
            else if (cbxvariety.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Select Variety');", true);
                cbxvariety.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void viewrpt()
        {
            a = txt_Report_Date.Text;
            b = txttodate.Text;
            try
            {

                DateTime dt = Convert.ToDateTime(txt_Report_Date.Text);
                txt_Report_Date.Text = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txttodate.Text);
                txttodate.Text = dt1.ToString("dd-MM-yyyy");

                if (cbxvariety.Text != "10" || cbxvariety.Text != "20" || cbxvariety.Text != "30")
                {
                    if (rdoWithFreight.Checked == true)
                    {
                        varStrValue = "D.NET_WT * D.RATE";
                    }
                    else
                    {
                        varStrValue = "D.NET_WT * CONVERT(FLOAT,D.ATTRIBUTE4)";
                    }
                }
                else
                {
                    varStrValue = "D.NET_WT * D.RATE";
                }



                if (cbxorgcd.SelectedIndex == 0)
                {

                    strsql = "SELECT SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)) as BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) as avgpr,H.ORGN_CODE+'-'+O.ORGN_NAME as ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where D.NET_WT <>0 and O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) group by SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)),H.ORGN_CODE+'-'+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME ";
                    //strsql = "SELECT D.BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(AVG(D.RATE),2) as avgpr,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'"+txt_Report_Date.Text+"' AS FROMDATE,'"+txttodate.Text+"' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txt_Report_Date.Text) + "','12:00:00 AM','00:00:00 AM'),105) and CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txttodate.Text) + "','12:00:00 AM','23:59:59 PM'),105) group by D.BUYER_GRADE,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME ";
                }
                else
                {
                    strsql = "SELECT SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)) as BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) as avgpr,H.ORGN_CODE+'-'+O.ORGN_NAME as ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where D.NET_WT <> 0 and O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "'  and H.ORGN_CODE='" + cbxorgcd.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) group by SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)),H.ORGN_CODE+'-'+O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME";

                }
                DataSet ds = new DataSet();
                ds = lpdMgt.GetTabPurchaseSummary(strsql);
                ReportDocument CustomerReport = new ReportDocument();
                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptBuyerClassReport.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);
                BuyerClassReport.Visible = true;
                BuyerClassReport1.Visible = false;
                BuyerClassReport.ReportSource = CustomerReport;
                BuyerClassReport.DataBind();                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            txt_Report_Date.Text = a;
            txttodate.Text = b;
        }


        public void viewrpt2()
        {
            a = txt_Report_Date.Text;
            b = txttodate.Text;
            try
            {
                DateTime dt = Convert.ToDateTime(txt_Report_Date.Text);
                txt_Report_Date.Text = dt.ToString("dd-MM-yyyy");
                DateTime dt1 = Convert.ToDateTime(txttodate.Text);
                txttodate.Text = dt1.ToString("dd-MM-yyyy");

                if (cbxvariety.Text != "10" || cbxvariety.Text!= "20" || cbxvariety.Text!= "30")
                {
                    if (rdoWithFreight.Checked == true)
                    {
                        varStrValue = "D.NET_WT * D.RATE";
                    }
                    else
                    {
                        varStrValue = "D.NET_WT * CONVERT(FLOAT,D.ATTRIBUTE4)";
                    }
                }
                else
                {
                    varStrValue = "D.NET_WT * D.RATE";
                }



                if (cbxorgcd.SelectedIndex == 0)
                {

                    strsql = "SELECT SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)) as BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) as avgpr,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_VARIETY_MASTER V where D.NET_WT <>0 and  D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) group by SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)),H.CROP,H.VARIETY,V.VARIETY_NAME ";
                    //strsql = "SELECT D.BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(AVG(D.RATE),2) as avgpr,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME,'"+txt_Report_Date.Text+"' AS FROMDATE,'"+txttodate.Text+"' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_ORGN_MASTER O,GPIL_VARIETY_MASTER V where O.ORGN_CODE=H.ORGN_CODE AND D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txt_Report_Date.Text) + "','12:00:00 AM','00:00:00 AM'),105) and CONVERT(datetime,REPLACE('" + Convert.ToDateTime(txttodate.Text) + "','12:00:00 AM','23:59:59 PM'),105) group by D.BUYER_GRADE,O.ORGN_NAME,H.CROP,H.VARIETY,V.VARIETY_NAME ";
                }
                else
                {
                    strsql = "SELECT SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)) as BUYER_GRADE,SUM(D.NET_WT) as qty ,ROUND(SUM(" + varStrValue + ")/SUM(D.NET_WT),2) as avgpr,H.CROP,H.VARIETY,V.VARIETY_NAME,'" + txt_Report_Date.Text + "' AS FROMDATE,'" + txttodate.Text + "' AS TODATE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_VARIETY_MASTER V where D.NET_WT <>0 and D.HEADER_ID=H.HEADER_ID AND V.VARIETY=H.VARIETY and D.STATUS!='N' and H.CROP='" + cbxcrop.Text + "' and H.VARIETY='" + cbxvariety.Text + "'  and H.ORGN_CODE='" + cbxorgcd.Text + "' and H.DATE_OF_PURCH between CONVERT(datetime,'" + txt_Report_Date.Text + " 00:00:00 AM',105) and CONVERT(datetime,'" + txttodate.Text + " 23:59:59 PM',105) group by SUBSTRING(D.BUYER_GRADE,5,len(D.BUYER_GRADE)),H.CROP,H.VARIETY,V.VARIETY_NAME";

                }
                DataSet ds = new DataSet();
                ds = lpdMgt.GetTabPurchaseSummary(strsql);


                ReportDocument CustomerReport = new ReportDocument();

                CustomerReport.Load(Server.MapPath("~/CrystalReport/RptBuyerClassReport_Summary.rpt"));
                CustomerReport.SetDataSource(ds.Tables[0].DefaultView);

                BuyerClassReport.Visible = false;
                BuyerClassReport1.Visible = true;

                BuyerClassReport1.ReportSource = CustomerReport;
                BuyerClassReport1.DataBind();

                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
            }
            txt_Report_Date.Text = a;
            txttodate.Text = b;
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuyerClassificationReport.aspx");
        }
    }
}