using GPILWebApp.ViewModel.Verificationn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.ComponentModel;
using GPILWebApp.ViewModel;

namespace GPILWebApp.Controllers
{
    public class FumigationDetailsLoaderController : Controller
    {
        // GET: FumigationDetailsLoader
        public ActionResult FumigationDetailsLoaderIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportFDLFromExcel(HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null && postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
                {
                    ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
                }
                else
                {
                    string filePath = string.Empty;
                    string path = Server.MapPath("~/ExcelUploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

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
                                    connExcel.Close();
                                }
                            }
                        }
                        string json = JsonConvert.SerializeObject(dt);
                        return Json(json, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                }
            }
           return View();
        }



        [HttpPost]
        public JsonResult OldPackedStockComplete(ListFumigationDetails LFDS)
        {
            //FumigationPackedStockInsert(LFDS);
            return null;
        }

        public static DataTable ToDataTable1<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        DataSet purdata = new DataSet();
        VerificationManagement vMgt = new VerificationManagement();
        public static string errordata = string.Empty;
        string lblMessage = string.Empty;
        string retVal = string.Empty;
        public static string errfile;

        //public void FumigationPackedStockInsert(ListFumigationDetails LFDS)
        //{

        //    try
        //    {
        //        string Farmererror = string.Empty;
        //        String strsql = string.Empty;
        //        //DataLoaderManagement vMgt = new DataLoaderManagement();
        //        //SELECT CROP,VARIETY,GPIL_BALE_NUMBER,GRADE,MARKED_WT,ORGN_CODE,'V' AS INS_STS from [Sheet1$]";

        //        DataTable dtclstr = ToDataTable1(LFDS.FumigDtls);

        //        DataRow[] drows = dtclstr.Select();
        //        for (int i = 0; i < drows.Length; i++)
        //        {
        //            dtclstr.Rows[i]["INS_STS"] = "V";
        //        }

        //        dtclstr.AcceptChanges();

        //        DataTable FLdata = new DataTable();


        //        //PUtchaseData

               
        //        for (int s = 0; s < 1; s++)
        //        {
        //            string tablename = "OLD_STOCK";
        //            purdata.Tables.Add(tablename);

        //            purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
        //            purdata.Tables[s].Columns.Add("ORGN_CODE");
        //            purdata.Tables[s].Columns.Add("FUM_DATE");
                   


        //            DataRow[] purrows = dtclstr.Select("GPIL_BALE_NUMBER LIKE '%F%'");
        //            if (purrows.Length > 0)
        //            {
        //                foreach (DataRow row in purrows)
        //                {
        //                    purdata.Tables[s].ImportRow(row);
        //                }
        //            }

        //        }


        //        //Insert info
        //        if (OldPackedStockValidate())
        //        {
        //            SqlTransaction trx = ClsConnection.SqlCon.BeginTransaction();
        //            try
        //            {

        //                for (int h = 0; h < purdata.Tables[0].Rows.Count; h++)
        //                {
        //                    int cnt = 0;


        //                    string varStrCase = purdata.Tables[0].Rows[h]["GPIL_BALE_NUMBER"].ToString();
        //                    string varStrOrg = purdata.Tables[0].Rows[h]["ORGN_CODE"].ToString();
        //                    string varStrFumDate = purdata.Tables[0].Rows[h]["FUM_DATE"].ToString().Replace("FUM", "");

        //                    if (FuncBoolIsExistWithTranx("SELECT * FROM GPIL_STOCK (NOLOCK) WHERE GPIL_BALE_NUMBER='" + varStrCase + "' AND CURR_ORGN_CODE='" + varStrOrg + "'", trx) == true)
        //                    {
        //                        string sCaseBarcode = varStrCase;
        //                        string sCropYearCode = sCaseBarcode.Substring(0, 1);
        //                        string sGradeCode = sCaseBarcode.Substring(2, 4);
        //                        string sCaseCode = sCaseBarcode.Substring(26, 1);
        //                        int iCaseNo = Convert.ToInt32(sCaseBarcode.Substring(9, 5));
        //                        string varStrChemQuery = "SELECT top 1 NIC, TRS, CL FROM mChemicalDetails (NOLOCK) WHERE CropYearCode='" + sCropYearCode + "' And GradeName in (select GradeName from mGrades where GradeCode='" + sGradeCode + "') AND CaseCode = '" + sCaseCode + "' AND RangeFrom <= " + iCaseNo + " AND RangeTo >= " + iCaseNo + " Order By Updatedon desc";

        //                        DataTable ds1 = new DataTable();
        //                        ds1 = vMgt.GetQueryResult(strsql);

        //                        string varWMSStockUpdation = "";


        //                        if (ds1.Rows.Count > 0)
        //                        {
        //                            varWMSStockUpdation = "UPDATE tCaseDetails SET NIC='" + ds1.Rows[0]["NIC"].ToString() + "',TRS='" + ds1.Rows[0]["TRS"].ToString() + "',CL='" + ds1.Rows[0]["CL"].ToString() + "',LocCode='" + varStrOrg + "',StackID='0001',Status='5',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";

        //                        }
        //                        else
        //                        {
        //                            varWMSStockUpdation = "UPDATE tCaseDetails SET LocCode='" + varStrOrg + "',StackID='0001',Status='5',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";
        //                            varWMSStockUpdation = "UPDATE tCaseDetails SET LocCode='" + varStrOrg + "',StackID='0001',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";

        //                        }


        //                        vMgt.UpdateUsingExecuteNonQuery(varWMSStockUpdation);

        //                        string varGLTSStockUpdation = "UPDATE GPIL_STOCK SET FUMIGATION_STARTING_DATE=convert(datetime,'" + varStrFumDate + "',105),FUMIGATION_ENDING_DATE=convert(datetime,'" + varStrFumDate + "',105)+7,FUMIGATION_EXPIRY_DATE=convert(datetime,'" + varStrFumDate + "',105)+67 WHERE GPIL_BALE_NUMBER='" + varStrCase + "'";
        //                        vMgt.UpdateUsingExecuteNonQuery(varGLTSStockUpdation);

        //                    }

        //                }

        //                lblMessage = "Success: Fumigation Details & WMS Stock are updated successfully" + retVal;
        //                Errorlog err = new Errorlog();
        //                errfile = err.WriteErrorLog(retVal, "OLDPKS_STK_SUCESS_" + Session["userID"].ToString(), Server.MapPath("LOGFILES\\"));
        //                btndwnerr.Visible = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                lblMessage = ex.Message;
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
        //                trx.Rollback();
        //                trx.Dispose();
        //            }
        //            finally
        //            {
        //                cleardataset();
        //                trx.Dispose();
        //            }
        //        }

        //        else
        //        {
        //            cleardataset();

        //            lblMessage = "Error: In Data Which Have Provided Please verify red color rows";
        //            btndwnerr.Visible = true;
        //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error In Data Which Have Provoded Please verify red color rows');", true);

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


    }



  
}