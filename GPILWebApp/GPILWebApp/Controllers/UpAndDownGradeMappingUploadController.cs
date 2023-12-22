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
    public class UpAndDownGradeMappingUploadController : Controller
    {
        // GET: UpAndDownGradeMappingUpload
        public ActionResult UpAndDownGradeMappingUploadIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFDFromExcel(HttpPostedFileBase postedFile)
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
            return View("FactoryDispatchLoaderIndex");
        }


        [HttpPost]
        public JsonResult UpAndDownGradeMappingUploadComplete(ListUpAndDownGradeMappingUpload UADGM)
        {


            DataLoaderManagement dlMgt = new DataLoaderManagement();

            //DataTable dtclstr = ToDataTable(UADGM.UpAndDownGradeMappingUploads);

            //var od = from s in UADGM.UpAndDownGradeMappingUploads
            //         group s by new { s.CROP } into newgroup
            //         select new
            //         {
            //             CROP = newgroup.Key.CROP

            //         };
            //var ods = od.ToList();
            //DataTable orgdata = new DataTable();
            //orgdata.Columns.Add("CROP");


            //DataRow row = null;

            //foreach (var rowObj in ods)
            //{
            //    row = orgdata.NewRow();
            //    orgdata.Rows.Add(rowObj.CROP);
            //}

            DataTable dtclstr = ToDataTable(UADGM.UpAndDownGradeMappingUploads);


            ////////////////////////////////////////////////VALIDATE//////////////////////////////////




            int i = 0;
            thresisserr = "Error :";
            try
            {

                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {
                    string strCrop = dtclstr.Rows[d]["CROP"].ToString();
                    string strVariety = dtclstr.Rows[d]["VARIETY"].ToString();


                    string strBuyerGrdGrp = dtclstr.Rows[d]["BUYER_GRADE_GRP"].ToString();
                    string strClassifierGrdGrp = dtclstr.Rows[d]["CLASSIFIER_GRADE_GRP"].ToString();
                    string strPairType = dtclstr.Rows[d]["PAIR_TYPE"].ToString().ToUpper();

                    if (strPairType != "U" && strPairType != "E" && strPairType != "D")
                    {

                        data = "Error: Invalid Pair Type";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //update(strCrop, strVariety, strBuyerGrdGrp, strClassifierGrdGrp, "N");
                        //i = i + 1;
                        //thresisserr = "Invalid Pair Type";
                    }
                    else
                    {
                        //update(strCrop, strVariety, strBuyerGrdGrp, strClassifierGrdGrp, "Y");
                    }

                }
                //if (i == 0)
                //{

                //    data = "Success: NO ERROR";
                //    json = JsonConvert.SerializeObject(data);
                //    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //    jsonResult.MaxJsonLength = int.MaxValue;
                //    return jsonResult;
                //    //thresisserr = thresisserr + " NO ERROR";
                //    ////Errorlog err = new Errorlog();
                //    //// err.WriteErrorLog(thresisserr, "BUYER_VS_CLASSIFIER_GRD_MAS_ERR" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                //    //return true;
                //}
                //else
                //{
                //    // Errorlog err = new Errorlog();
                //    // errfile = err.WriteErrorLog(thresisserr, "BUYER_VS_CLASSIFIER_GRD_MAS_ERR" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                //    //return false;
                //}


            }
            catch (Exception ex)
            {

                data = "Error: " + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //lblMessage.Text = ex.Message;
                //thresisserr = thresisserr + Environment.NewLine + ex.Message;
                //Errorlog err = new Errorlog();
                //errfile = err.WriteErrorLog(thresisserr, "BUYER_VS_CLASSIFIER_GRD_MAS_ERR" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                //return false;
            }
            finally
            {

                // purdata.Clear();

            }




            //////////////////////////////////////////////////////////////////////////////////////////

            //insertinfo

            //if (UpAndDownvalidate())
            //{
            temprefno = string.Empty;
            //SqlTransaction trx = ClsConnection.SqlCon.BeginTransaction();
            List<string> lstQry = new List<string>();
            try
            {
                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {
                    string strBuyerGradeGrp, strClassifierGrdGrp, strPairCode;
                    //  string strQuery;
                    string strCrop, strVariety;


                    strCrop = dtclstr.Rows[d]["CROP"].ToString();
                    strVariety = dtclstr.Rows[d]["VARIETY"].ToString();

                    strBuyerGradeGrp = dtclstr.Rows[d]["BUYER_GRADE_GRP"].ToString();
                    strClassifierGrdGrp = dtclstr.Rows[d]["CLASSIFIER_GRADE_GRP"].ToString();
                    strPairCode = dtclstr.Rows[d]["PAIR_TYPE"].ToString().ToUpper();


                    strsql = "SELECT BUYER_GRADE_GRP FROM GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER(NOLOCK) WHERE BUYER_GRADE_GRP='" + strBuyerGradeGrp + "' AND CLASSIFIER_GRADE_GRP='" + strClassifierGrdGrp + "' AND ATTRIBUTE1='" + strCrop + "' AND ATTRIBUTE2='" + strVariety + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    //cmd1 = new SqlCommand(strsql, ClsConnection.SqlCon);
                    //cmd1.CommandTimeout = 0;
                    //cmd1.Transaction = trx;

                    //strrs = cmd1.ExecuteReader();
                    if (ds1.Columns.Contains("ErrorMessage"))
                    {
                        data = "Error: " + ds1.Rows[0]["ErrorMessage"].ToString();
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    // if (dtclstr.Rows.Count == 0)

                    if (ds1.Rows.Count > 0)
                    {
                        strQuery = "UPDATE GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER SET PAIR_TYPE='" + strPairCode + "',STATUS='Y',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE  BUYER_GRADE_GRP='" + strBuyerGradeGrp + "' AND CLASSIFIER_GRADE_GRP='" + strClassifierGrdGrp + "' AND ATTRIBUTE1='" + strCrop + "' AND ATTRIBUTE2='" + strVariety + "'";

                    }
                    else
                    {
                        strQuery = "INSERT INTO GPIL_BUYER_VS_CLASSIFIER_GRD_MASTER (PAIR_CODE,BUYER_GRADE_GRP,CLASSIFIER_GRADE_GRP,PAIR_TYPE,CREATED_BY,CREATED_DATE,STATUS,ATTRIBUTE1,ATTRIBUTE2) VALUES ('" + strCrop + strVariety + "-" + strBuyerGradeGrp + "-" + strClassifierGrdGrp + "','" + strBuyerGradeGrp + "','" + strClassifierGrdGrp + "','" + strPairCode + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','" + strCrop + "','" + strVariety + "')";
                    }


                    lstQry.Add(strQuery);


                }
                bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);
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
                    return Json("Error: Please Check Your Excel and Upload!!!!" + JsonRequestBehavior.AllowGet);
                }

                //trx.Commit();
                //clrgridview();
                //dtclstr.Clear();
                //trx.Dispose();
                //lblMessage.Text = "DONE";
                //Errorlog err = new Errorlog();
                //errfile = err.WriteErrorLog("UpandDown", "GradeMappingUpload" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                //btndwnerr.Visible = true;
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                //trx.Rollback();
                //trx.Dispose();
                //return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);

            }
        }
        string strQuery;
        string temprefno;
        string strsql;

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
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;
        MasterManagement MstrMgt = new MasterManagement();
        //[HttpPost]
        //public JsonResult UpAndDownGradeMappingdata(ListUpAndDownGradeMappingUpload UADGM)
        //{
            //finally
            //{
            //    //cleardataset();
            //    //trx.Dispose();
            //}
            // }
            //else
            //{
            //    //cleardataset();
            //    //lblMessage.Text = "Error In Data Which Have Provoded Please verify red color rows";
            //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Error In Data Which Have Provoded Please verify red color rows');", true);
            //    //btndwnerr.Visible = true;

            //}
            //return View();

        //}

        string errfile;
        string thresisserr;
        // DataTable dtclstr = new DataTable();

        //public bool UpAndDownvalidate()
        //{
        //    int i = 0;
        //    thresisserr = "Error :";
        //    try
        //    {

        //        for (int d = 0; d < dtclstr.Rows.Count; d++)
        //        {
        //            string strCrop = dtclstr.Rows[d]["CROP"].ToString();
        //            string strVariety = dtclstr.Rows[d]["VARIETY"].ToString();


        //            string strBuyerGrdGrp = dtclstr.Rows[d]["BUYER_GRADE_GRP"].ToString();
        //            string strClassifierGrdGrp = dtclstr.Rows[d]["CLASSIFIER_GRADE_GRP"].ToString();
        //            string strPairType = dtclstr.Rows[d]["PAIR_TYPE"].ToString().ToUpper();

        //            if (strPairType != "U" && strPairType != "E" && strPairType != "D")
        //            {
        //                update(strCrop, strVariety, strBuyerGrdGrp, strClassifierGrdGrp, "N");
        //                i = i + 1;
        //                thresisserr = "Invalid Pair Type";
        //            }
        //            else
        //            {
        //                update(strCrop, strVariety, strBuyerGrdGrp, strClassifierGrdGrp, "Y");
        //            }

        //        }
        //        if (i == 0)
        //        {
        //            thresisserr = thresisserr + " NO ERROR";
        //            //Errorlog err = new Errorlog();
        //            // err.WriteErrorLog(thresisserr, "BUYER_VS_CLASSIFIER_GRD_MAS_ERR" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
        //            return true;
        //        }
        //        else
        //        {
        //            // Errorlog err = new Errorlog();
        //            // errfile = err.WriteErrorLog(thresisserr, "BUYER_VS_CLASSIFIER_GRD_MAS_ERR" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
        //            return false;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //lblMessage.Text = ex.Message;
        //        //thresisserr = thresisserr + Environment.NewLine + ex.Message;
        //        //Errorlog err = new Errorlog();
        //        //errfile = err.WriteErrorLog(thresisserr, "BUYER_VS_CLASSIFIER_GRD_MAS_ERR" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
        //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
        //        return false;
        //    }
        //    finally
        //    {

        //        // purdata.Clear();

        //    }
        //}




        //public void update(string inParamStrCrop, string inParamStrVariety, string inParamStrBuyerGradeGrp, string inParamStrClassifierGradeGrp, string flg)
        //{
        //    try
        //    {
        //        DataRow[] rows = dtclstr.Select("BUYER_GRADE_GRP='" + inParamStrBuyerGradeGrp + "' AND CLASSIFIER_GRADE_GRP='" + inParamStrClassifierGradeGrp + "' AND CROP='" + inParamStrCrop + "' AND VARIETY='" + inParamStrVariety + "'");
        //        if (rows.Length > 0)
        //        {
        //            foreach (DataRow row in rows)
        //            {
        //                row["INS_STS"] = flg;
        //                dtclstr.AcceptChanges();
        //                row.SetModified();
        //            }
        //        }


        //        //GridViewSample.EditIndex = -1;
        //        //LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblMessage.Text = ex.Message;
        //    }
        //}









    }
}