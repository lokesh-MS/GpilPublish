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
    public class GLTStemMoistureController : Controller
    {
        // GET: GLTStemMoisture
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// DROP DOWN FIELDS
        /// </summary>

        DataSet ds = new DataSet();
        DataTable ds1 = new DataTable();
        string strsql;
        string json = "";
        CommonManagement ldMgt = new CommonManagement();


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
        //crop, variety, stripGrade, stemGrade, fromDate, sampleTime, equipNo, caseRunNo, moistureResult, afterCoreFac, caseTemp
        [HttpPost]

        public JsonResult InsertStemMoisture(string crop, string variety, string stripGrade, string stemGrade, string fromDate, string sampleTime, string equipNo, string caseRunNo, string moistureResult, string afterCoreFac, string caseTemp)
        {
            //crop, grade, type, fromDate, sampleTime, runNo, runCaseNo, timeIn, timeOut, result, packedTemp, startTime, stopTime
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            string queryInsert = "";
            try
            {
                if (crop != "" && variety != "" && stripGrade != "" && stemGrade != "" && fromDate != "" && sampleTime != "" && equipNo != "" && caseRunNo != "" && moistureResult != "" && afterCoreFac != "" && caseTemp != "")
                {
                    string Strqry1 = "Select Count(*) from [dbo].[GPIL_Stem_Moisture] where [Crop]='" + crop + "' and [Variety]='" + variety + "' and [StripGrade]='" + stripGrade + "' and [StemGrade]='" + stemGrade + "' and [RunCaseNo]='" + caseRunNo + "'";
                    dt = cMgt.GetQueryResult(Strqry1);
                    if (dt.Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(dt.Rows[0][0]);

                        if (count == 0)
                        {


                            queryInsert = "INSERT INTO [dbo].[GPIL_Stem_Moisture]([Crop],[Variety],[StripGrade],[StemGrade],[Date],[Time],[EquipNo],[RunCaseNo],[MoistureResult],[AfterCorr],[CaseTemp])";
                            queryInsert += " VALUES ('" + crop + "','" + variety + "','" + stripGrade + "','" + stemGrade + "','" + fromDate + "','" + sampleTime + "'," + Convert.ToInt32(equipNo) + "," + Convert.ToInt32(caseRunNo) + "";
                            queryInsert += "," + Convert.ToDouble(moistureResult) + "," + Convert.ToDouble(afterCoreFac) + "," + Convert.ToDouble(caseTemp) + ")";
                        }
                        else
                        {
                            data = "Error: The Value Already Exists!!";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                    }

                    bool b = cMgt.UpdateUsingExecuteNonQuery(queryInsert);

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
            return View();
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
        public JsonResult StemMoistureComplete(ListGLTStemMoisture LGSMR)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            string check = "";
            string queryInsert = "";



            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LGSMR.StemMoistures);

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string Crop = dtGridLst.Rows[s]["Crop"].ToString();
                    string Variety = dtGridLst.Rows[s]["Variety"].ToString();
                    string StripGrade = dtGridLst.Rows[s]["StripGrade"].ToString();
                    string StemGrade = dtGridLst.Rows[s]["StemGrade"].ToString();
                    string Date = dtGridLst.Rows[s]["Date"].ToString();
                    string RunCaseNo = dtGridLst.Rows[s]["RunCaseNo"].ToString();
                    string Time = dtGridLst.Rows[s]["Time"].ToString();
                    string EquipNo = dtGridLst.Rows[s]["EquipNo"].ToString();
                    string MoistureResult = dtGridLst.Rows[s]["MoistureResult"].ToString();
                    string AfterCorr = dtGridLst.Rows[s]["AfterCorr"].ToString();
                    string CaseTemp = dtGridLst.Rows[s]["CaseTemp"].ToString();

                    DataTable dt1 = new DataTable();
                    check = "Select Count(*) from [dbo].[GPIL_Stem_Moisture] where [Crop]='" + Crop + "' and [Variety]='" + Variety + "' and [StripGrade]='" + StripGrade + "' and [StemGrade]='" + StemGrade + "' and  [RunCaseNo]=" + Convert.ToInt32(RunCaseNo) + "";
                    dt1 = cMgt.GetQueryResult(check);


                    if (dt1.Rows.Count == 0)
                    {
                        queryInsert = "INSERT INTO [dbo].[GPIL_Stem_Moisture]([Crop],[Variety],[StripGrade],[StemGrade],[Date],[Time],[EquipNo],[RunCaseNo],[MoistureResult],[AfterCorr],[CaseTemp])";
                        queryInsert += " VALUES ('" + Crop + "','" + Variety + "','" + StripGrade + "','" + StemGrade + "',(select convert(varchar,'" + Date + "',103)),'" + Time + "'," + Convert.ToInt32(EquipNo) + "," + Convert.ToInt32(RunCaseNo) + "";
                        queryInsert += "," + Convert.ToDouble(MoistureResult) + "," + Convert.ToDouble(AfterCorr) + "," + Convert.ToDouble(CaseTemp) + ")";
                        lstQry.Add(queryInsert);
                    }
                    else
                    {
                        data = "Error: The Value Already Exists!!";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
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