using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPILWebApp
{
    public partial class ChemistryDataLoader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rdbimportFromExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbimportFromExcel.Checked == true)
            {
                idUpload.Visible = true;
                idManual.Visible = false;
                idManualButton.Visible = false;
             //   divGrid.Visible = false;
                divImportSave.Visible = true;
                //lblMessage.Text = "";

            }
        }

        protected void rdbmanualEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbmanualEntry.Checked == true)
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

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}