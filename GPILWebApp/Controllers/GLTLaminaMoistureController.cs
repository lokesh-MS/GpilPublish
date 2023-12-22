using GPI;
using GPILWebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class GLTLaminaMoistureController : Controller
    {
        // GET: GLTLaminaMoisture


        DataSet ds = new DataSet();
        DataTable ds1 = new DataTable();
        string strsql;
        string json = "";
        CommonManagement ldMgt = new CommonManagement();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
       
        public ActionResult GetCrop()
        {
            
            try
            {

                strsql = "SELECT[CROP_YEAR] FROM [dbo].[GPIL_CROP_MASTER]";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }
        [HttpGet]
       
        public ActionResult GetGrade()
        {
           
            try
            {

                strsql = "SELECT [ITEM_CODE] FROM [dbo].[GPIL_ITEM_MASTER] where [ITEM_CODE] like 'L%'";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }
        [HttpGet]
       
        public ActionResult GetType()
        {
            
            try
            {

                strsql = "SELECT [VARIETY_TYPE]  FROM [dbo].[GPIL_VARIETY_MASTER]";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }


        CommonManagement cMgt = new CommonManagement();
        DataTable dt = new DataTable();
        //Crop, Variety, StripGrade, ScrapGrade, Date, SampleTime,                                                                                                                 RunNo, CaseRunNo, AccCaseNo, MoistureRsltl, CaseTemp
        [HttpPost]
        public JsonResult InsertLaminaMoisture(string crop, string grade, string type, string fromDate, string sampleTime, string runNo, string runCaseNo, string timeIn, string timeOut, string result, string packedTemp, string startTime, string stopTime)
        {
            //crop, grade, type, fromDate, sampleTime, runNo, runCaseNo, timeIn, timeOut, result, packedTemp, startTime, stopTime
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            string query = "";
            try
            {
                if (crop != "" && grade != "" && type != "" && fromDate != "" && sampleTime != "" && runNo != "" && runCaseNo != "" && timeIn != "" && timeOut != "" && result != "" && packedTemp != "" && startTime != "" && stopTime != "")
                {
                    string Strqry1 = "Select *  FROM [dbo].[GPIL_LamiaGrade] where [Type]='" + type + "' and [Grade]='" + grade + "' and [GradeCode]='" + grade + "' and [RunNo]='" + runNo + "' and [RunCaseNo]='" + runCaseNo + "'";

                    dt = cMgt.GetQueryResult(Strqry1);


                    if (dt.Rows.Count == 0)
                    {
                        query = "INSERT INTO [dbo].[GPIL_LamiaGrade]([Crop],[Type],[GradeCode],[Grade],[Date],[SampleTime],[RunNo],[RunCaseNo],[TimeIn],[TimeOut],[Results],[AfterCF],[PackedTemp],[GrindingStartTIme],[GrindingEndTIme])";
                        query += "VALUES('" + crop + "','" + type + "','" + grade + "','" + grade + "','" + fromDate + "','" + sampleTime + "','" + runNo + "','" + runCaseNo + "',";
                        query += "'" + timeIn + "','" + timeOut + "','" + result + "','" + result + "','" + packedTemp + "','" + startTime + "','" + stopTime + "')";
                    }
                    else
                    {
                        data = "Error: The Value Already Exists!!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }


                    bool b = cMgt.UpdateUsingExecuteNonQuery(query);

                    if (b)
                    {
                        data = "Success: The Value Inserted Successfully!!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else
                    {
                        data = "Error: Error While Inserting Data!!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
                else
                {
                    data = "Error: Please check all the values are entered...";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }

                

            }
            catch (Exception ex)
            {
                data = "Error: " + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //turn Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);

            }


        }







        public ActionResult ExcelIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase postedFile)
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


        public static DataTable ToDataTable<T>(IList<T> data)
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




        string lblMessage = string.Empty;
        string data = String.Empty;
        JsonResult jsonResult;
        //string json = String.Empty;
        DataSet purdata = new DataSet();        
        public static string errfile;
        string retVal = string.Empty;
        [HttpPost]
        public JsonResult LaminaMoistureComplete(ListGLTLaminaMoisture LGLM)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;


            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LGLM.GLTLaminaMoistures);
                string query = "";
                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string Crop = dtGridLst.Rows[s]["Crop"].ToString();
                    string Type = dtGridLst.Rows[s]["Type"].ToString();
                    string Grade = dtGridLst.Rows[s]["Grade"].ToString();
                    string Date = dtGridLst.Rows[s]["Date"].ToString();
                    string Sample_Time = dtGridLst.Rows[s]["SampleTime"].ToString();
                    string Run_No = dtGridLst.Rows[s]["RunNo"].ToString();
                    string Run_Case = dtGridLst.Rows[s]["RunCaseNo"].ToString();
                    string Time_In = dtGridLst.Rows[s]["TimeIn"].ToString();
                    string Time_Out = dtGridLst.Rows[s]["TimeOut"].ToString();
                    string Results = dtGridLst.Rows[s]["Results"].ToString();
                    string Packed_Temp = dtGridLst.Rows[s]["PackedTemp"].ToString();
                    string Grinding_Start_Time = dtGridLst.Rows[s]["GrindingStartTIme"].ToString();
                    string Grinding_Stop_Time = dtGridLst.Rows[s]["GrindingEndTIme"].ToString();

                   
                    query = "INSERT INTO [dbo].[GPIL_LamiaGrade]([Crop],[Type],[GradeCode],[Grade],[Date],[SampleTime],[RunNo],[RunCaseNo],[TimeIn],[TimeOut],[Results],[PackedTemp],[GrindingStartTIme],[GrindingEndTIme])";
                    query += "VALUES('" + Crop + "','" + Type + "','NULL','" + Grade + "',(select convert(varchar,'" + Date + "',103))";
                    query += ",'" + Sample_Time + "','" + Run_No + "','" + Run_Case + "',";
                    query += "'" + Time_In + "','" + Time_Out + "','" + Results + "','" + Packed_Temp + "','" + Grinding_Start_Time + "','" + Grinding_Stop_Time + "')";
                    lstQry.Add(query);
                }
               
                bool b = cMgt.UpdateUsingExecuteNonQueryList(lstQry);
                //bool b = GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);
                if (b)
                {
                    data = "Succuss";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    return Json("Error: Please check the excel sheet", JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }


    }
}