using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GPILWebApp
{
    public partial class GLTLaminaMoisture : System.Web.UI.Page
    {
        //testDataContext test;
        DataTable objDataTable = new DataTable();
        public static DataTable dt = new DataTable();
        public static string errfile;


        bool varBolIsException = false;
        string varStrErrorMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString());
        //public void dataBind()
        //{


        //    //Crop
        //    try
        //    {
        //        //if (con.State != ConnectionState.Open)
        //        //{
        //        //    con.Open();
        //        //}


        //        string query;
        //        SqlDataAdapter sda;
        //        DataTable dt = new DataTable();

        //        query = "SELECT[CROP_YEAR] FROM [dbo].[GPIL_CROP_MASTER]";
        //        sda = new SqlDataAdapter(query, con);
        //        sda.Fill(dt);
        //        ddCrop.DataSource = dt;
        //        //ddCrop.DataBind();
        //        ddCrop.DataTextField = "CROP_YEAR";
        //        ddCrop.DataBind();
        //        ddCrop.Items.Insert(0, new ListItem("--Select--", "0"));
        //        sda.Dispose();


        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.ToString();
        //    }
        //    ////Variety
        //    try
        //    {
        //        //if (con.State != ConnectionState.Open)
        //        //{
        //        //    con.Open();
        //        //}


        //        string query;
        //        SqlDataAdapter sda;
        //        DataTable dt = new DataTable();

        //        query = "SELECT [VARIETY_TYPE]  FROM [dbo].[GPIL_VARIETY_MASTER]";
        //        sda = new SqlDataAdapter(query, con);
        //        sda.Fill(dt);
        //        ddType.DataSource = dt;
        //        ddType.DataBind();
        //        ddType.DataTextField = "VARIETY_TYPE";
        //        ddType.DataBind();
        //        sda.Dispose();


        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.ToString();
        //    }
        //    ////Grade
        //    try
        //    {
        //        //if (con.State != ConnectionState.Open)
        //        //{
        //        //    con.Open();
        //        //}


        //        string query;
        //        SqlDataAdapter sda;
        //        DataTable dt = new DataTable();

        //        query = "SELECT [ITEM_CODE] FROM [dbo].[GPIL_ITEM_MASTER] where [ITEM_CODE] like 'L%'";
        //        sda = new SqlDataAdapter(query, con);
        //        sda.Fill(dt);
        //        sda.Dispose();
        //        ddGrade.DataSource = dt;
        //        //ddGrade.DataBind();
        //        ddGrade.DataTextField = "ITEM_CODE";
        //        ddGrade.DataBind();
        //        ddGrade.Items.Insert(0, new ListItem("--Select--", "0"));



        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.ToString();
        //    }
        //}
        protected void txtSampleTIme_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txttimeIN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtResults_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Complete_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {

        }
    }
    //[Flags]
    //public enum ConnectionState
    //{
    //    Closed = 0,
    //    Open = 1,
    //    Connecting = 2,
    //    Executing = 4,
    //    Fetching = 8,
    //    Broken = 16,
    //}
}