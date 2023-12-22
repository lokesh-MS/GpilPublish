using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class VillageMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnManualSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void rdbimport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbimport.Checked == true)
            {
                idUpload.Visible = true;
                idManual.Visible = false;
                idManualButton.Visible = false;
                //divGrid.Visible = false;
                // divImportSave.Visible = true;
                //lblMessage.Text = "";

            }
        }

        protected void rdbmanual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbmanual.Checked == true)
            {
                idManual.Visible = true;
                idManualButton.Visible = true;
                idUpload.Visible = false;
               // divGrid.Visible = false;
                divImportSave.Visible = false;
                //lblMessage.Text = "";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnImportSave_Click(object sender, EventArgs e)
        {

        }
    }
}