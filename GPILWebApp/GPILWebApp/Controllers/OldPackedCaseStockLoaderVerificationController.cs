using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static GPILWebApp.ViewModel.Verificationn.OldPackedCaseStockLoader;
using System.Data;
using GPILWebApp.ViewModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;


namespace GPILWebApp.Controllers
{
    public class OldPackedCaseStockLoaderVerificationController : Controller
    {
        // GET: OldPackedCaseStockLoaderVerification
        public ActionResult OldPackedCaseStockLoaderIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportOPSFromExcel(HttpPostedFileBase postedFile)
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
            return View("FarmerpPurchaseLoaderIndex");
        }


        string lblMessage = string.Empty;
        string data = String.Empty;
        JsonResult jsonResult;
        string json = String.Empty;
        DataSet purdata = new DataSet();
        VerificationManagement vMgt = new VerificationManagement();
        public static string errfile;
        string retVal = string.Empty;
        [HttpPost]
        public JsonResult OldPackedStockComplete(ListOldPackedStock LOPS)
        {
            //OldPackedStockInsert(LOPS);
            //return null;
            try
            {
                //string Farmererror = string.Empty;
                String strsql = string.Empty;
                //DataLoaderManagement vMgt = new DataLoaderManagement();
                //SELECT CROP,VARIETY,GPIL_BALE_NUMBER,GRADE,MARKED_WT,ORGN_CODE,'V' AS INS_STS from [Sheet1$]";

                DataTable dtclstr = ToDataTable1(LOPS.OldPackedStocks);

                DataRow[] drows = dtclstr.Select();
                for (int i = 0; i < drows.Length; i++)
                {
                    dtclstr.Rows[i]["INS_STS"] = "V";
                }

                dtclstr.AcceptChanges();

                // DataTable FLdata = new DataTable();


                //PUtchaseData

                for (int s = 0; s < 1; s++)
                {
                    string tablename = "OLD_STOCK";
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("MARKED_WT");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");


                    DataRow[] purrows = dtclstr.Select("GRADE LIKE 'L%'");
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow row in purrows)
                        {
                            purdata.Tables[s].ImportRow(row);
                        }
                    }

                }


                //Insert info
                if (OldPackedStockValidate())
                {
                    //SqlTransaction trx = ClsConnection.SqlCon.BeginTransaction();
                    try
                    {
                        string varStockQuery = "Begin tran ";
                        for (int h = 0; h < purdata.Tables[0].Rows.Count; h++)
                        {
                            int cnt = 0;


                            string varStrCrop = purdata.Tables[0].Rows[h]["CROP"].ToString();
                            string varStrVariety = purdata.Tables[0].Rows[h]["VARIETY"].ToString();
                            string varStrCase = purdata.Tables[0].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            string varStrGrade = purdata.Tables[0].Rows[h]["GRADE"].ToString();
                            string varStrQty = purdata.Tables[0].Rows[h]["MARKED_WT"].ToString();
                            string varStrOrg = purdata.Tables[0].Rows[h]["ORGN_CODE"].ToString();

                            varStockQuery = varStockQuery + " INSERT INTO GPIL_STOCK (GPIL_BALE_NUMBER,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,CURR_LOCN,ORIGN_ORGN_CODE,CURR_ORGN_CODE,CROP,VARIETY,SUBINVENTORY_CODE,CREATED_BY,CREATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,PRODUCT_TYPE,PROCESS_STATUS,STATUS,WMS_STATUS,FUMIGATION_STATUS) ";
                            varStockQuery = varStockQuery + " VALUES ('" + varStrCase.Trim() + "','" + varStrGrade.Trim() + "','" + varStrQty.Trim() + "','" + varStrQty.Trim() + "','LOC1','LOC1','" + varStrOrg.Trim() + "','" + varStrOrg.Trim() + "','" + varStrCrop.Trim() + "','" + varStrVariety.Trim() + "','FGD','5655',GETDATE(),'N','OPCS','PC','N','Y','Y','FUM') ";



                        }
                        varStockQuery = varStockQuery + " commit tran ";
                        bool b = vMgt.UpdateUsingExecuteNonQuery(varStockQuery);
                        //trx.Commit();
                        //clrgridview();
                        //objDTNewStock.Clear();
                        //trx.Dispose();
                        if (b)
                        {
                            lblMessage = "Success: Old Packed Case Stock are added successfully" + retVal;
                        }
                        else
                        {
                            lblMessage = "Error: Old Packed Case Stock are not added" + retVal;
                        }
                        //lblMessage = "Success: Old Packed Case Stock are added successfully" + retVal;
                        //Errorlog err = new Errorlog();
                        //errfile = err.WriteErrorLog(retVal, "OLDPKS_STK_SUCESS_" + Session["userID"].ToString(), Server.MapPath("LOGFILES\\"));
                        //btndwnerr.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblMessage = ex.Message;
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                        //trx.Rollback();
                        //trx.Dispose();
                    }
                    finally
                    {
                        //cleardataset();
                        //trx.Dispose();
                    }
                }

                else
                {
                    //cleardataset();

                    lblMessage = "Error: In Data Which Have Provided Please verify red color rows";
                    //btndwnerr.Visible = true;
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error In Data Which Have Provoded Please verify red color rows');", true);

                }
                if (lblMessage.Length > 0)
                {
                    data = lblMessage;
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Success";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }

            }
            catch (Exception ex)
            {

            }
            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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

       
        //string lblMessage = string.Empty;

        public void OldPackedStockInsert(ListOldPackedStock LOPS)
        {
            
           
        }


        DataTable dtclstr = new DataTable();


        public static string errordata = string.Empty;
        public bool OldPackedStockValidate()
        {
            int i = 0;
            errordata = "Error :";

            try
            {
                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string currorg = "";

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {

                        string varStrCrop = purdata.Tables[d].Rows[h]["CROP"].ToString();
                        string varStrVariety = purdata.Tables[d].Rows[h]["VARIETY"].ToString();
                        string varStrCase = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string varStrGrade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string varStrQty = purdata.Tables[d].Rows[h]["MARKED_WT"].ToString();
                        string varStrOrg = purdata.Tables[d].Rows[h]["ORGN_CODE"].ToString();

                        string strQuery0 = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + varStrCase + "'";
                        string strQuery1 = "select * from tCaseDetails (NOLOCK) where CaseBarcode='" + varStrCase + "'";
                        string strQuery2 = "select * from GPIL_ITEM_MASTER (NOLOCK) where ITEM_CODE='" + varStrGrade + "'";
                        string strQuery3 = "select * from mLocations (NOLOCK) where LocCode='" + varStrOrg + "' and LocType in ('PSW','GLT','REDRYING')";
                        string strQuery4 = "select * from GPIL_CROP_MASTER (NOLOCK) where CROP='" + varStrCrop + "'";
                        string strQuery5 = "select * from GPIL_VARIETY_MASTER (NOLOCK) where VARIETY='" + varStrVariety + "'";


                        DataTable ds1 = new DataTable();
                        ds1 = vMgt.GetQueryResult(strQuery0);
                        DataTable ds2 = new DataTable();
                        ds2 = vMgt.GetQueryResult(strQuery1);
                        DataTable ds3 = new DataTable();
                        ds3 = vMgt.GetQueryResult(strQuery2);
                        DataTable ds4 = new DataTable();
                        ds4 = vMgt.GetQueryResult(strQuery3);
                        DataTable ds5 = new DataTable();
                        ds5 = vMgt.GetQueryResult(strQuery4);
                        DataTable ds6 = new DataTable();
                        ds6 = vMgt.GetQueryResult(strQuery5);

                        if (varStrCase == "" || varStrCase.Trim().Length != 31)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " is in-valid";
                        }
                        else if (ds1.Rows.Count != 0)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " is already exist in Traceability Stock";
                        }
                        else if (ds2.Rows.Count != 0)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " is not yet Available in WMS Stock";
                        }
                        else if (ds3.Rows.Count != 0)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Grade (" + varStrGrade + ") not yet Available in Item Master";
                        }
                        else if (varStrGrade != "LF" + varStrCase.Substring(27, 4) + varStrCase.Substring(2, 4))
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Grade (" + varStrGrade + ") not yet match with Case Number";
                        }
                        else if (ds4.Rows.Count != 0)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Stock Organization (" + varStrOrg + ") not yet available / not be GLT/PSW Type in Locaation Master";
                        }
                        else if (ds5.Rows.Count != 0)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Crop (" + varStrCrop + ") not yet available in Crop Master";
                        }
                        else if (ds6.Rows.Count != 0)
                        {
                            OldPackedStockUpdate(varStrCase, "N");
                            i = i + 1;
                            errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Variety (" + varStrVariety + ") not yet available in Variety Master";
                        }
                        //else if (varStrQty.Substring(0,3) != varStrCase.Substring(18, 3))
                        //{
                        //    update(varStrCase, "N");
                        //    i = i + 1;
                        //    errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Nett Qty (" + varStrQty + ") not yet match with Case Number";
                        //}
                        else
                        {
                            OldPackedStockUpdate(varStrCase, "Y");
                        }
                    }

                }

                if (i == 0)
                {
                    /*errordata = errordata + " No Errors";
                    Errorlog err = new Errorlog();
                    errfile=err.WriteErrorLog(errordata, "Dispatch_Error_"+Propertycls.EMPCODE+"", Server.MapPath("LOGFILES\\"));
                    download(Server.MapPath("LOGFILES\\"), errfile);*/
                    return true;
                }
                else
                {
                    //Errorlog err = new Errorlog();
                    //errfile = err.WriteErrorLog(errordata, "Dispatch_Error_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                    return false;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
            }
        }


        public void OldPackedStockUpdate(string gpilbaleno, string flg)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER ='" + gpilbaleno + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["INS_STS"] = flg;
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }

            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }



    }


    public static class ClsConnection
    {
        //public ClsConnection()
        //{
        public static SqlConnection SqlCon = new SqlConnection();

        public static string connection;
        public static string UserName;
        public static string ExcelFile = "";
        public static string strConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
        public static string DBNAME = ConfigurationManager.AppSettings["DBname"].ToString();
        public static string USRID = ConfigurationManager.AppSettings["usrid"].ToString();
        public static string PASSWORD = ConfigurationManager.AppSettings["password"].ToString();
        public static string SERVERIP = ConfigurationManager.AppSettings["IP"].ToString();

        public static DataSet dsPerm = new DataSet();
        public static DataTable dtMain = new DataTable();
        public static DataTable dtWeekHdr = new DataTable();
        public static DataTable dtWeekDtl = new DataTable();

        public static void connectDB()
        {
            if (ClsConnection.SqlCon.State != ConnectionState.Open)
            {
                connection = ConfigurationManager.ConnectionStrings["conStr"].ToString();
                ClsConnection.SqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
                ClsConnection.SqlCon.Open();
            }
        }
        public static void closeDB()
        {
            if (ClsConnection.SqlCon.State == ConnectionState.Open)
            {
                ClsConnection.SqlCon.Close();
            }
        }

        public static string ExcelFileUpload
        {
            get
            {
                return ExcelFile;
            }
            set
            {
                ExcelFile = value;
            }
        }
        //}
    }


    public class Errorlog
    {
        public Errorlog()
        { }
        public string WriteErrorLog(string LogMessage, string file, string path)
        {
            bool Status = false;
            //string LogDirectory = ConfigurationManager.AppSettings["LogDirectory"].ToString();
            string LogDirectory = path;
            DateTime CurrentDateTime = DateTime.Now;
            string CurrentDateTimeString = CurrentDateTime.ToString();
            CheckCreateLogDirectory(LogDirectory);
            string logLine = BuildLogLine(CurrentDateTime, LogMessage);
            string logfile = "Log_" + file + LogFileName(DateTime.Now) + ".txt";
            LogDirectory = (LogDirectory + "Log_" + file + LogFileName(DateTime.Now) + ".txt");

            lock (typeof(Errorlog))
            {
                StreamWriter oStreamWriter = null;
                try
                {
                    oStreamWriter = new StreamWriter(LogDirectory, true);
                    oStreamWriter.WriteLine(logLine);
                    Status = true;
                }
                catch

                {

                }
                finally
                {
                    if (oStreamWriter != null)
                    {
                        oStreamWriter.Close();
                    }
                }
            }
            return logfile;
        }


        private bool CheckCreateLogDirectory(string LogPath)
        {
            bool loggingDirectoryExists = false;
            DirectoryInfo oDirectoryInfo = new DirectoryInfo(LogPath);
            if (oDirectoryInfo.Exists)
            {
                loggingDirectoryExists = true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(LogPath);
                    loggingDirectoryExists = true;
                }
                catch
                {
                    // Logging failure
                }
            }
            return loggingDirectoryExists;
        }


        private string BuildLogLine(DateTime CurrentDateTime, string LogMessage)
        {
            StringBuilder loglineStringBuilder = new StringBuilder();
            loglineStringBuilder.Append(LogFileEntryDateTime(CurrentDateTime));
            loglineStringBuilder.Append(" \t");
            loglineStringBuilder.Append(LogMessage);
            return loglineStringBuilder.ToString();
        }


        public string LogFileEntryDateTime(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }


        private string LogFileName(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd_MM_yyyy");
        }


    }




}