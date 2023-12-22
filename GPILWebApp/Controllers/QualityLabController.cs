using GPI;
using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class QualityLabController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _data;
        public QualityLabController()
        {
            _data = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _data.Dispose();
        }
        // GET: QualityLab
        public ActionResult ChemistryDataIndex()
        {
            return View();
        }

        //public ActionResult Create()
        //{
        //    ViewBag.GPIL_Chemical_Target = (from s in _data.GPIL_Chemical_Targets select new { s.Crop }).Distinct();
        //    ViewBag.GPIL_Chemical_Targets = (from s in _data.GPIL_Chemical_Targets select new { s.Variety }).Distinct();
        //    ViewBag.GPIL_Chemical_Targetss = (from s in _data.GPIL_Chemical_Targets select new { s.Grade }).Distinct();
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "DOP,Crop,Variety,Grade,Mark,SourceOrganisation,Product,Dom_Exp,Type,From_Run_No,To_Run_No,NIC,TRS,CL,MoisturePercent")] GPIL_Chemistry_Reports ChReports)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _data.GPIL_Chemistry_Reports.Add(ChReports);
        //        _data.SaveChanges();
        //        return RedirectToAction("ChemistryDataIndex");
        //    }

            

        //    return View(ChReports);
        //}

        public ActionResult ManualIndex()
        {
            ViewBag.GPIL_Chemical_Target = (from s in _data.GPIL_Chemical_Targets select new { s.Crop }).Distinct();
            ViewBag.GPIL_Chemical_Targets = (from s in _data.GPIL_Chemical_Targets select new { s.Variety }).Distinct();
            ViewBag.GPIL_Chemical_Targetss = (from s in _data.GPIL_Chemical_Targets select new { s.Grade }).Distinct();
            return View();
        }

        CommonManagement cMgt = new CommonManagement();
        DataTable dt = new DataTable();

        [HttpPost]
        public ActionResult InsertQualityData(string dateOfTime, string crop, string type, string grade, string variety, string exportType, string mark, string product, string sourceOrgn, string fromNo, string toNo, string trs, string cl, string nic, string moisturePer)
        {
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                if (moisturePer != "")
                {
                    int i;

                    string querycheck = "select Count(*) from GPIL_Chemistry_Reports where dop=convert(varchar,'" + dateOfTime + " 00:00:00',103) and Crop='" + crop + "' and Variety='" + variety + "' and Grade='" + grade + "' and From_Run_No=" + Convert.ToInt32(fromNo) + " and To_Run_No=" + Convert.ToInt32(toNo) + "";
                    dt = cMgt.GetQueryResult(querycheck);
                    if (dt.Rows.Count != 0)
                    {
                        try
                        {
                            //if(
                            string query = "INSERT INTO [dbo].[GPIL_Chemistry_Reports]([DOP],[Crop],[Variety],[Grade],[Mark] ,[SourceOrganisation],[Product],[Dom_Exp] ,[Type] ,[From_Run_No],[To_Run_No] ,[NIC],[TRS],[CL],[MoisturePercent])";
                            query += "VALUES(CONVERT(varchar,'" + dateOfTime + "',103),'" + crop + "','" + variety + "','" + grade + "','" + mark + "','" + sourceOrgn + "','" + product + "','" + exportType + "','" + type + "',";
                            query += "" + Convert.ToInt32(fromNo) + "," + Convert.ToInt32(toNo) + "," + Convert.ToDouble(nic) + "," + Convert.ToDouble(trs) + "," + Convert.ToDouble(cl) + "," + Convert.ToDouble(moisturePer) + ")";

                            cMgt.UpdateUsingExecuteNonQuery(query);

                            
                            lblMessage = "Success: Updated";
                            
                        }
                        catch (Exception ex)
                        {
                            
                            lblMessage = ex.ToString();
                        }
                    }
                    else
                    {
                        lblMessage = "Error: The Values already exists in Database";
                        //Alert.Show("The Values already exists in Database");
                    }
                }
                else
                {
                   
                    lblMessage = "Error: Check the NIC and Moisture values";
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
            return View("ChemistryDataIndex");
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
        public ActionResult QualityLabLoaderInsert(ListQualityLab LQL)
        {
            // int z = 0;           
            String strsql = string.Empty;
            DataTable dtclstr = ToDataTable1(LQL.QualityLabs);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            bool result = false;
            try
            {

                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {
                    try
                    {

                        string strCrop, strGrade, strVariety, strDop, strMark, strProduct, strExportType;
                        string strType, strSourceOrg, strSRunNo, strERunNo, strNic;
                        string strTrs, strCl, strMoisture;

                        strCrop = dtclstr.Rows[i]["Crop"].ToString();
                        strGrade = dtclstr.Rows[i]["Grade"].ToString();

                        strVariety = dtclstr.Rows[i]["Variety"].ToString();
                        strDop = dtclstr.Rows[i]["DOP"].ToString();
                        strMark = dtclstr.Rows[i]["Mark"].ToString();
                        strProduct = dtclstr.Rows[i]["Product"].ToString();

                        strExportType = dtclstr.Rows[i]["ExportType"].ToString();
                        strType = dtclstr.Rows[i]["Type"].ToString();
                        strSourceOrg = dtclstr.Rows[i]["SourceOrg"].ToString();
                        strSRunNo = dtclstr.Rows[i]["SRunNo"].ToString();

                        strERunNo = dtclstr.Rows[i]["ERunNo"].ToString();
                        strNic = dtclstr.Rows[i]["NIC"].ToString();
                        strTrs = dtclstr.Rows[i]["TRS"].ToString();

                        strCl = dtclstr.Rows[i]["CL"].ToString();
                        strMoisture = dtclstr.Rows[i]["Moisture"].ToString();

                        //'" + strFarmerCode + "'
                        string query = "INSERT INTO [dbo].[GPIL_Chemistry_Reports]([DOP],[Crop],[Variety],[Grade],[Mark] ,[SourceOrganisation],[Product],[Dom_Exp] ,[Type] ,[From_Run_No],[To_Run_No] ,[NIC],[TRS],[CL],[MoisturePercent])";
                        query += "VALUES('" + strDop + "','" + strCrop + "','" + strVariety + "','" + strGrade + "','" + strMark + "','" + strSourceOrg + "','" + strProduct + "','" + strExportType + "','" + strType + "',";
                        query += "" + Convert.ToInt32(strSRunNo) + "," + Convert.ToInt32(strERunNo) + "," + Convert.ToDouble(strNic) + "," + Convert.ToDouble(strTrs) + "," + Convert.ToDouble(strCl) + "," + Convert.ToDouble(strMoisture) + ")";
                        result = cMgt.UpdateUsingExecuteNonQuery(query);
                        //z = 0;
                    }
                    catch (Exception ex)
                    {
                        //z = 1;
                    }

                }
                if (result)
                {


                    lblMessage = "Success: Sucessfully Inserted";

                    data = "Success";
                    //json = JsonConvert.SerializeObject(data);
                    //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    //jsonResult.MaxJsonLength = int.MaxValue;
                    //return jsonResult;
                    //lblMessage.Visible = true; 
                    //GridViewSample.DataSource = null;
                    //GridViewSample.DataBind();

                }
                else
                {
                    //tran.Rollback();
                    lblMessage = "Error: while Inserting";
                    //lblMessage.Visible = true;
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
                lblMessage = (ex.ToString());
                //lblMessage.Visible = true;
            }
            return View("ChemistryDataIndex");
        }


    }
}