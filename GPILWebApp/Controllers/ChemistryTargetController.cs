using GPI;
using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.ComponentModel;
using GPILWebApp.ViewModel;

namespace GPILWebApp.Controllers
{
    public class ChemistryTargetController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _data;
        public ChemistryTargetController()
        {
            _data = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _data.Dispose();
        }

        // GET: ChemistryTarget
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.GPIL_CROP_MASTERs = (from s in _data.GPIL_CROP_MASTER select new { s.CROP_YEAR }).Distinct();
            ViewBag.GPIL_VARIETY_MASTERs = (from s in _data.GPIL_VARIETY_MASTER select new { s.VARIETY_TYPE }).Distinct();
            ViewBag.GPIL_ITEM_MASTERs = (from s in _data.GPIL_ITEM_MASTER select new { s.ITEM_CODE }).Distinct();
            return View();
        }
        DataTable dt = new DataTable();
        CommonManagement cMgt = new CommonManagement();

        string lblMessage = string.Empty;
        string data = String.Empty;
        JsonResult jsonResult;
        [HttpGet]
        public ActionResult GetUSLAvgValue(string lslvalue, string uslValue, string avg1)
        {
            string json = String.Empty;
            string lblMessage = String.Empty;
            string averageValue = String.Empty;

            if (Convert.ToDouble(lslvalue) < Convert.ToDouble(uslValue))
            {
                avg1 = Convert.ToString((Convert.ToDouble(lslvalue) + Convert.ToDouble(uslValue)) / 2);
                averageValue = avg1;
            }
            else
            {
                lblMessage = "Error: LSL value higher than USL value";
            }

            if (lblMessage.Length > 0)
            {
                var errorData = new { Message = lblMessage };
                json = JsonConvert.SerializeObject(errorData);
            }
            else
            {
                var successData = new { Message = "Success", AverageValue = averageValue };
                json = JsonConvert.SerializeObject(successData);
            }

            // Use a different variable name to avoid conflict with the field
            var jsonResultResponse = Json(json, JsonRequestBehavior.AllowGet);
            jsonResultResponse.MaxJsonLength = int.MaxValue;
            return jsonResultResponse;
        }


        //public ActionResult GetMoiUslValue(string moiLsl, string moiUsl, string ave2)
        //{
        //    string json = String.Empty;

        //    if (Convert.ToDouble(moiLsl) < Convert.ToDouble(moiUsl))
        //    {

        //        ave2 = Convert.ToString((Convert.ToDouble(moiLsl) + Convert.ToDouble(moiUsl)) / 2);
        //    }
        //    else
        //    {
        //        lblMessage = "Error: Moisture USL value higher than UCL value";
        //    }
        //    if (lblMessage.Length > 0)
        //    {
        //        data = lblMessage;
        //        json = JsonConvert.SerializeObject(data);
        //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    else
        //    {
        //        data = "Success";
        //        json = JsonConvert.SerializeObject(data);
        //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;

        //    }
        //}

        public ActionResult GetMoiUslValue(string moiLsl, string moiUsl, string ave2)
        {
            string json = String.Empty;
            string lblMessage = String.Empty;
            string averageValue = String.Empty;

            if (Convert.ToDouble(moiLsl) < Convert.ToDouble(moiUsl))
            {

                ave2 = Convert.ToString((Convert.ToDouble(moiLsl) + Convert.ToDouble(moiUsl)) / 2);
                averageValue = ave2;

            }
            else
            {
                lblMessage = "Error: Moisture USL value higher than UCL value";
            }
            if (lblMessage.Length > 0)
            {
                var errorData = new { Message = lblMessage };
                json = JsonConvert.SerializeObject(errorData);
            }
            else
            {
                var successData = new { Message = "Success", AverageValue = averageValue };
                json = JsonConvert.SerializeObject(successData);
            }
            var jsonResultResponse = Json(json, JsonRequestBehavior.AllowGet);
            jsonResultResponse.MaxJsonLength = int.MaxValue;
            return jsonResultResponse;
        }




        //Crop, Variety, Grade, Mark, Lsl, Usl, Ave, Lcl, Ucl, MoiLsl, MoiUsl, Ave2
        [HttpPost]
        public ActionResult InsertChemistryTargetData(string crop, string variety, string grade, string mark, string lsl, string usl, string ave, string lcl, string ucl, string moiLsl, string moiUsl, string ave2)
        {
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                int i;
                if (crop != "" && variety != "" && grade != "")
                {
                    if (ave != "" && lsl != "" && ave2 != "" && usl != "" && lcl != "" && ucl != "" && moiLsl != "" && moiUsl != "")
                    {

                        string query1 = "select Count(*) from [dbo].[GPIL_Chemical_Targets]  where Crop='" + crop + "' and Variety='" + variety + "' and Grade='" + grade + "'";
                        dt = cMgt.GetQueryResult(query1);
                        //SqlCommand sqlcmd1 = new SqlCommand(query1, con);
                        //i = Convert.ToInt32(dt);
                        //sqlcmd1.Dispose();
                        //if (i == 0)
                        if (dt.Rows.Count != 0)
                        {
                            string query = "INSERT INTO [dbo].[GPIL_Chemical_Targets] ([Crop] ,[Variety] ,[Grade] ,[Mark],[LSL] ,[AVE] ,[USL] ,[LCL],[AVEC],[UCL]   ,[LSLMoisture] ,[USLMoisture])  VALUES ";
                            query += " ('" + crop + "','" + variety + "','" + grade + "','" + mark + "'," + lsl + "";
                            query += "," + ave + "," + usl + "," + lcl + "," + ave2 + "," + ucl + "," + moiLsl + "," + moiUsl + ")";

                            cMgt.UpdateUsingExecuteNonQuery(query);

                            lblMessage = "Success: Updated Sucessfully";
                            //cleartexts();
                        }
                        else
                        {

                            lblMessage = "Error: The Value already exists in Database";
                        }


                    }
                    else
                    {

                        lblMessage = "Error: Fill Up all the Fields before clicking the Save button";
                    }
                }
                else
                {
                    lblMessage = "Error: Please Check the Crop, Grade, Variety values !!!";
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
                lblMessage = ex.ToString();
                //lblMessage.Visible = true;
            }
            return View();


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
            return View("Index");
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


        [HttpPost]
        public JsonResult ChemistryTargetLoaderComplete(ListChemistryTarget LCT)
        {
            int z = 0;
            String strsql = string.Empty;
            String query = string.Empty;
            DataTable dtclstr = ToDataTable1(LCT.ChemistryTargets);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {
                    try
                    {

                        string strCrop, strGrade, strVariety, strMark, strLSL, strASL, strUSL;
                        string strLCL, strALCL, strUCL, strMoistureL, strMoistureU;

                        strCrop = dtclstr.Rows[i]["Crop"].ToString();
                        strGrade = dtclstr.Rows[i]["Grade"].ToString();

                        strVariety = dtclstr.Rows[i]["Variety"].ToString();
                        strMark = dtclstr.Rows[i]["Mark"].ToString();
                        strLSL = dtclstr.Rows[i]["LSL"].ToString();
                        strASL = dtclstr.Rows[i]["ASL"].ToString();

                        strUSL = dtclstr.Rows[i]["USL"].ToString();
                        strLCL = dtclstr.Rows[i]["LCL"].ToString();
                        strALCL = dtclstr.Rows[i]["ALCL"].ToString();
                        strUCL = dtclstr.Rows[i]["UCL"].ToString();

                        strMoistureL = dtclstr.Rows[i]["MoistureL"].ToString();
                        strMoistureU = dtclstr.Rows[i]["MoistureU"].ToString();

                        query = "INSERT INTO [dbo].[GPIL_Chemical_Targets] ([Crop] ,[Variety] ,[Grade] ,[Mark],[LSL] ,[AVE] ,[USL] ,[LCL],[AVEC],[UCL]   ,[LSLMoisture] ,[USLMoisture])  VALUES ";
                        query += " ('" + strCrop + "','" + strVariety + "','" + strGrade + "','" + strMark + "'," + Convert.ToDouble(strLSL) + "";
                        query += "," + Convert.ToDouble(strASL) + "," + Convert.ToDouble(strUSL) + "," + Convert.ToDouble(strLCL) + "," + Convert.ToDouble(strALCL) + "," + Convert.ToDouble(strUCL) + " ";
                        query += "," + Convert.ToDouble(strMoistureL) + "," + strMoistureU + ")";
                        cMgt.UpdateUsingExecuteNonQuery(query);
                        z = 0;
                    }

                    catch (Exception ex)
                    {
                        z = 1;
                    }
                }
                if (z == 1)
                {
                    //tran.Rollback();
                    lblMessage = "Error: Error while Inserting";



                }
                else
                {

                    //tran.Commit();
                    lblMessage = "Success: Sucessfully Inserted";


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
                lblMessage = ex.ToString();

            }
            return null;
        }
        public ActionResult ChemistryTargetLoaderInsert(ListChemistryTarget LCT)
        {
            
            return View("Index");

        }
    }
}