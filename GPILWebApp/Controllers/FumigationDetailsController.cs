using GPILWebApp.ViewModel.Verificationn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPILWebApp.ViewModel;

namespace GPILWebApp.Controllers
{
    public class FumigationDetailsController : Controller
    {
        // GET: FumigationDetails
        public ActionResult FumigationDetailsIndex()
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
            return View("FumigationDetailsIndex");
        }

        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;
        [HttpPost]
        public JsonResult FumigationComplete(ListFumigationDetails LFD)
        {

            string varWMSStockUpdation = "";
            string varGLTSStockUpdation = "";
            string Farmererror = string.Empty;
            String strsql = string.Empty;
            VerificationManagement VrMgt = new VerificationManagement();

            DataTable dtclstr = ToDataTable1(LFD.FumigDtls);
            //var od = from s in LFD.FumigDtls
            //         group s by new { s.ORGN_CODE, s.PURCH_DOC_NO } into newgroup
            //         select new
            //         {
            //             ORGN_CODE = newgroup.Key.ORGN_CODE,
            //             PURCH_DOC_NO = newgroup.Key.PURCH_DOC_NO

            //         };
            //var ods = od.ToList();
            //DataTable orgdata = new DataTable();
            //orgdata.Columns.Add("ORGN_CODE");
            //orgdata.Columns.Add("PURCH_DOC_NO");
            //DataRow row = null;

            //foreach (var rowObj in ods)
            //{
            //    row = orgdata.NewRow();
            //    orgdata.Rows.Add(rowObj.ORGN_CODE, rowObj.PURCH_DOC_NO);
            //}


            //PurchaseData
            DataSet purdata = new DataSet();
            DataTable dt = new DataTable();
            for (int s = 0; s < 1; s++)
            {
                string tablename = "OLD_STOCK";
                purdata.Tables.Add(tablename);

                purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                purdata.Tables[s].Columns.Add("ORGN_CODE");
                purdata.Tables[s].Columns.Add("FUM_DATE");


                DataRow[] purrows = dtclstr.Select("GPIL_BALE_NUMBER LIKE '%F%'");
                if (purrows.Length > 0)
                {
                    foreach (DataRow rows in purrows)
                    {
                        purdata.Tables[s].ImportRow(rows);
                    }
                }

            }

            /////////////////////////////////////////VALIDATE/////////////////////////////////////////////////


            for (int d = 0; d < purdata.Tables.Count; d++)
            {
                //string currorg = "";

                for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                {


                    string varStrCase = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                    string varStrOrg = purdata.Tables[d].Rows[h]["ORGN_CODE"].ToString();
                    string varStrFumDate = purdata.Tables[d].Rows[h]["FUM_DATE"].ToString().Replace("FUM", "");

                    string strQuery0 = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + varStrCase + "'";
                    string strQuery1 = "select * from tCaseDetails (NOLOCK) where CaseBarcode='" + varStrCase + "'";
                    string strQuery2 = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + varStrCase + "' AND CURR_ORGN_CODE='" + varStrOrg + "'";

                    DataTable ds1 = new DataTable();
                    ds1 = VrMgt.GetQueryResult(strQuery0);
                    DataTable ds2 = new DataTable();
                    ds2 = VrMgt.GetQueryResult(strQuery1);
                    DataTable ds3 = new DataTable();
                    ds3 = VrMgt.GetQueryResult(strQuery2);


                    if (varStrCase == "" || varStrCase.Trim().Length != 31)
                    {
                        data = "Error: Case Number : " + varStrCase + " is in-valid";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //update(varStrCase, "N");
                        //i = i + 1;
                        //errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " is in-valid";
                    }
                    else if (ds1.Rows.Count == 0)
                    {
                        data = "Error: Case Number : " + varStrCase + " is not yet Available in Traceability Stock";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //update(varStrCase, "N");
                        //i = i + 1;
                        //errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " is not yet Available in Traceability Stock";
                    }
                    else if (ds2.Rows.Count == 0)
                    {
                        data = "Error: Case Number : " + varStrCase + " is not yet Available in WMS Stock";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //update(varStrCase, "N");
                        //i = i + 1;
                        //errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " is not yet Available in WMS Stock";
                    }
                    else if (ds3.Rows.Count == 0)
                    {
                        data = "Error: Case Number : " + varStrCase + " Stock not in " + varStrOrg + " Org.";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                        //update(varStrCase, "N");
                        //i = i + 1;
                        //errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Stock not in " + varStrOrg + " Org.";
                    }
                    else
                    {
                        try
                        {
                            string strQuery4 = "SELECT CONVERT(DATE,'" + varStrFumDate.Trim() + "',105)";
                            string strQuery5 = "SELECT (CASE WHEN CONVERT(DATETIME,'" + varStrFumDate + "',105) + 7 >= GETDATE() THEN 0 ELSE 1 END)";
                            DataTable ds4 = new DataTable();
                            ds4 = VrMgt.GetQueryResult(strQuery4);
                            DataTable ds5 = new DataTable();
                            ds5 = VrMgt.GetQueryResult(strQuery5);


                            if (ds4.Rows.Count == 0)
                            {
                                data = "Error: Case Number : " + varStrFumDate + " is not a valid date. Expected Format : dd-MM-yyyy";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                                //update(varStrCase, "N");
                                //i = i + 1;
                                //errordata = errordata + Environment.NewLine + "Case Number : " + varStrFumDate + " is not a valid date. Expected Format : dd-MM-yyyy";

                            }
                            else if (ds5.Rows.Count == 0)
                            {
                                data = "Error: Case Number : " + varStrCase + " Fumigation End Date exceed on today. Fumigation Start On : " + varStrFumDate;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                                //update(varStrCase, "N");
                                //i = i + 1;
                                //errordata = errordata + Environment.NewLine + "Case Number : " + varStrCase + " Fumigation End Date exceed on today. Fumigation Start On : " + varStrFumDate;

                            }
                            else
                            {
                                //update(varStrCase, "Y");
                            }


                        }
                        catch (Exception ex)
                        {
                            data = "Error: Case Number : " + varStrFumDate + " is not a valid date. Expected Format : dd-MM-yyyy";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                            //update(varStrCase, "N");
                            //i = i + 1;
                            //errordata = errordata + Environment.NewLine + "Case Number : " + varStrFumDate + " is not a valid date. Expected Format : dd-MM-yyyy";

                        }

                    }
                }

            }






            ///////////////////////////////////////////////////////////////////////////////////////////////////

            for (int h = 0; h < purdata.Tables[0].Rows.Count; h++)
            {
                int cnt = 0;


                string varStrCase = purdata.Tables[0].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                string varStrOrg = purdata.Tables[0].Rows[h]["ORGN_CODE"].ToString();
                string varStrFumDate = purdata.Tables[0].Rows[h]["FUM_DATE"].ToString().Replace("FUM", "");


                strsql = "SELECT * FROM GPIL_STOCK (NOLOCK) WHERE GPIL_BALE_NUMBER='" + varStrCase + "' AND CURR_ORGN_CODE='" + varStrOrg + "'";
                DataTable ds1 = new DataTable();
                ds1 = VrMgt.GetQueryResult(strsql);


                if (ds1.Rows.Count >= 0)
                {
                    string sCaseBarcode = varStrCase;
                    string sCropYearCode = sCaseBarcode.Substring(0, 1);
                    string sGradeCode = sCaseBarcode.Substring(2, 4);
                    string sCaseCode = sCaseBarcode.Substring(26, 1);
                    int iCaseNo = Convert.ToInt32(sCaseBarcode.Substring(9, 5));
                    string varStrChemQuery = "SELECT top 1 NIC, TRS, CL FROM mChemicalDetails (NOLOCK) WHERE CropYearCode='" + sCropYearCode + "' And GradeName in (select GradeName from mGrades where GradeCode='" + sGradeCode + "') AND CaseCode = '" + sCaseCode + "' AND RangeFrom <= " + iCaseNo + " AND RangeTo >= " + iCaseNo + " Order By Updatedon desc";

                    DataTable objDataTable = new DataTable();
                    objDataTable = VrMgt.GetQueryResult(varStrChemQuery);




                    if (objDataTable.Rows.Count > 0)
                    {
                        varWMSStockUpdation = "UPDATE tCaseDetails SET NIC='" + objDataTable.Rows[0]["NIC"].ToString() + "',TRS='" + objDataTable.Rows[0]["TRS"].ToString() + "',CL='" + objDataTable.Rows[0]["CL"].ToString() + "',LocCode='" + varStrOrg + "',StackID='0001',Status='5',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";

                    }
                    else
                    {
                        varWMSStockUpdation = "UPDATE tCaseDetails SET LocCode='" + varStrOrg + "',StackID='0001',Status='5',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";


                    }




                    varGLTSStockUpdation = "UPDATE GPIL_STOCK SET FUMIGATION_STARTING_DATE=convert(datetime,'" + varStrFumDate + "',105),FUMIGATION_ENDING_DATE=convert(datetime,'" + varStrFumDate + "',105)+7,FUMIGATION_EXPIRY_DATE=convert(datetime,'" + varStrFumDate + "',105)+67 WHERE GPIL_BALE_NUMBER='" + varStrCase + "'";


                }

            }
            bool b = VrMgt.UpdateUsingExecuteNonQuery(varWMSStockUpdation);
            b = VrMgt.UpdateUsingExecuteNonQuery(varGLTSStockUpdation);
            // + retVal
            if (b)
            {
                data = "Succuss: Fumigation Details & WMS Stock are updated successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                return Json("Error: Please check the excel sheet", JsonRequestBehavior.AllowGet);
            }
            //lblMessage.Text = "Fumigation Details & WMS Stock are updated successfully" + retVal;

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

        //public void Fumigationdata(ListFumigationDetails LFD)
        //{
        //    try
        //    {

        //        string Farmererror = string.Empty;
        //        String strsql = string.Empty;
        //        VerificationManagement VrMgt = new VerificationManagement();

        //        DataTable dtclstr = ToDataTable1(LFD.FumigDtls);
        //        //var od = from s in LFD.FumigDtls
        //        //         group s by new { s.ORGN_CODE, s.PURCH_DOC_NO } into newgroup
        //        //         select new
        //        //         {
        //        //             ORGN_CODE = newgroup.Key.ORGN_CODE,
        //        //             PURCH_DOC_NO = newgroup.Key.PURCH_DOC_NO

        //        //         };
        //        //var ods = od.ToList();
        //        //DataTable orgdata = new DataTable();
        //        //orgdata.Columns.Add("ORGN_CODE");
        //        //orgdata.Columns.Add("PURCH_DOC_NO");
        //        //DataRow row = null;

        //        //foreach (var rowObj in ods)
        //        //{
        //        //    row = orgdata.NewRow();
        //        //    orgdata.Rows.Add(rowObj.ORGN_CODE, rowObj.PURCH_DOC_NO);
        //        //}


        //        //PUtchaseData
        //        DataSet purdata = new DataSet();
        //        DataTable dt = new DataTable();
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
        //                foreach (DataRow rows in purrows)
        //                {
        //                    purdata.Tables[s].ImportRow(rows);
        //                }
        //            }

        //        }

        //        for (int h = 0; h < purdata.Tables[0].Rows.Count; h++)
        //        {
        //            int cnt = 0;


        //            string varStrCase = purdata.Tables[0].Rows[h]["GPIL_BALE_NUMBER"].ToString();
        //            string varStrOrg = purdata.Tables[0].Rows[h]["ORGN_CODE"].ToString();
        //            string varStrFumDate = purdata.Tables[0].Rows[h]["FUM_DATE"].ToString().Replace("FUM", "");


        //            strsql = "SELECT * FROM GPIL_STOCK (NOLOCK) WHERE GPIL_BALE_NUMBER='" + varStrCase + "' AND CURR_ORGN_CODE='" + varStrOrg + "'";
        //            DataTable ds1 = new DataTable();
        //            ds1 = VrMgt.GetQueryResult(strsql);


        //            if (ds1.Rows.Count >=0 )
        //            {
        //                string sCaseBarcode = varStrCase;
        //                string sCropYearCode = sCaseBarcode.Substring(0, 1);
        //                string sGradeCode = sCaseBarcode.Substring(2, 4);
        //                string sCaseCode = sCaseBarcode.Substring(26, 1);
        //                int iCaseNo = Convert.ToInt32(sCaseBarcode.Substring(9, 5));
        //                string varStrChemQuery = "SELECT top 1 NIC, TRS, CL FROM mChemicalDetails (NOLOCK) WHERE CropYearCode='" + sCropYearCode + "' And GradeName in (select GradeName from mGrades where GradeCode='" + sGradeCode + "') AND CaseCode = '" + sCaseCode + "' AND RangeFrom <= " + iCaseNo + " AND RangeTo >= " + iCaseNo + " Order By Updatedon desc";

        //                DataTable objDataTable = new DataTable();
        //                objDataTable = VrMgt.GetQueryResult(strsql);

        //                string varWMSStockUpdation = "";


        //                if (objDataTable.Rows.Count > 0)
        //                {
        //                    varWMSStockUpdation = "UPDATE tCaseDetails SET NIC='" + objDataTable.Rows[0]["NIC"].ToString() + "',TRS='" + objDataTable.Rows[0]["TRS"].ToString() + "',CL='" + objDataTable.Rows[0]["CL"].ToString() + "',LocCode='" + varStrOrg + "',StackID='0001',Status='5',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";

        //                }
        //                else
        //                {
        //                    varWMSStockUpdation = "UPDATE tCaseDetails SET LocCode='" + varStrOrg + "',StackID='0001',Status='5',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";
        //                    //varWMSStockUpdation = "UPDATE tCaseDetails SET LocCode='" + varStrOrg + "',StackID='0001',InTime=convert(datetime,'" + varStrFumDate + "',105),PutawayOn=convert(datetime,'" + varStrFumDate + "',105),PutawayBy='gpi',FumStartOn=convert(datetime,'" + varStrFumDate + "',105),FumEndOn=convert(datetime,'" + varStrFumDate + "',105)+7,FumExpiredOn=convert(datetime,'" + varStrFumDate + "',105)+67,FumigatedBy='gpi' WHERE CaseBarcode='" + varStrCase + "'";

        //                }


        //                VrMgt.UpdateUsingExecuteNonQuery(varWMSStockUpdation);

        //                string varGLTSStockUpdation = "UPDATE GPIL_STOCK SET FUMIGATION_STARTING_DATE=convert(datetime,'" + varStrFumDate + "',105),FUMIGATION_ENDING_DATE=convert(datetime,'" + varStrFumDate + "',105)+7,FUMIGATION_EXPIRY_DATE=convert(datetime,'" + varStrFumDate + "',105)+67 WHERE GPIL_BALE_NUMBER='" + varStrCase + "'";
        //                VrMgt.UpdateUsingExecuteNonQuery(varGLTSStockUpdation);

        //            }

        //        }

        //        //lblMessage.Text = "Fumigation Details & WMS Stock are updated successfully" + retVal;
        //        //Errorlog err = new Errorlog();
        //        //errfile = err.WriteErrorLog(retVal, "OLDPKS_STK_SUCESS_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
        //        //btndwnerr.Visible = true;

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}