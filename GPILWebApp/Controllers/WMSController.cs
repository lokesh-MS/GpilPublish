using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp.Models;
using System.Web.Mvc;
//using GPILWebApp.ViewModel;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data.Entity.SqlServer;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using GPILWebApp.ViewModel;
using GPI;
using System.Web.UI;
using System.Text;
using System.Text.RegularExpressions;
using GPILWebApp.ViewModel.WMS;
using System.ComponentModel;

namespace GPILWebApp.Controllers
{
    public class WMSController : Controller
    {
        // GET: WMS

        private GREEN_LEAF_TRACEABILITYEntities _data;
        CommonManagement CMgt = new CommonManagement();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public WMSController()
        {
            _data = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _data.Dispose();
        }

        /// <summary>
        /// VIEW PRINT ALLOCATION
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewPrintAllocationIndex()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.ORGN_TYPE == "WH" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View("ViewPrintAllocationIndex");
        }
        clsDBActions oDBActions = new clsDBActions();
        [HttpGet]

        //public ActionResult FumigationLoaction()
        //{
        //    string sQuery = "";
        //    try
        //    {

        //        if (clsSettings.strLocCode == clsSettings.sOngoleLoc)
        //        {
        //            sQuery = "Select LocCode, LocName from mLocations Where Active='True' And LocType<>'FACTORY' Order By LocCode";
        //        }
        //        else
        //        {
        //            sQuery = "Select LocCode, LocName from mLocations Where LocCode='" + clsSettings.strLocCode + "'";
        //        }
        //        //oDBActions.FillComboBox(cboLocCode, sQuery, false);
        //        //cboLocCode.SelectedIndex = cboLocCode.Items.IndexOf(cboLocCode.Items.FindByText(clsSettings.strLocCode));
        //        //BindGrid(clsSettings.strLocCode);



        //        //string strsql = "SELECT LocCode,LocCode FROM mLocations(NOLOCK) WHERE LocType='PSW'";
        //        //dt = CMgt.GetQueryResult(strsql);
        //        //dt.TableName = "Table";
        //        //var data = dt;
        //        //string json = JsonConvert.SerializeObject(data);
        //        //return Json(json, JsonRequestBehavior.AllowGet);
        //        //var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        //jsonResult.MaxJsonLength = int.MaxValue;
        //    }
        //    catch
        //    {

        //    }
        //    return Json(dt);
        //}

        //[HttpGet]

        //public JsonResult WMSDataStatus(string FromDate, string ToDate, string LP5)
        //{
        //    DataSet ds = new DataSet();
        //    WMSManagement wmsMgt = new WMSManagement();
        //    try
        //    {
        //        ds = wmsMgt.GetWMSDataStatus(FromDate, ToDate, LP5);
        //        var data = ds;
        //        string json = JsonConvert.SerializeObject(data);
        //        var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    { }
        //    return Json(ds);
        //}

        /// <summary>
        /// FUMIGATION RECEIPT
        /// </summary>
        /// <returns></returns>
        public ActionResult FumigationReceiptIndex()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.ORGN_TYPE == "WH" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }



        [HttpGet]
        public ActionResult GetFumigationReceiptGrid(string orgnCode)
        {

            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT D.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENDER_DATE,H.RECEIVED_DATE,D.GRADE,COUNT(D.GPIL_BALE_NUMBER) AS CASES,SUM(MARKED_WT) AS QUANTITY,(D.SHIPMENT_NO + D.GRADE) AS SHIPGRADEGROUP FROM GPIL_SHIPMENT_DTLS(NOLOCK) D, GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO  AND H.RECEIVER_ORGN_CODE='" + orgnCode + "' AND H.STATUS='N' AND H.WMS_FLAG='Y' AND H.IS_WMS_SHIPMENT='Y' GROUP BY D.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENDER_DATE,H.RECEIVED_DATE,D.GRADE ORDER BY D.SHIPMENT_NO,D.GRADE";
            dtclstr = CMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }


        public JsonResult GetFumigationReceiptGridValues(string shipmentNumber, string packedgrade)
        {
            Session["SHIPMENT_NO"] = shipmentNumber;
            Session["GRADE"] = packedgrade;

            string strsql = "select * from GPIL_SHIPMENT_DTLS(NOLOCK) where ISNULL(ATTRIBUTE1,'')<>'Y' and SHIPMENT_NO='" + shipmentNumber + "' and GRADE='" + packedgrade + "'";
            dt = CMgt.GetQueryResult(strsql);
            string json = JsonConvert.SerializeObject(dt);
            return Json(json, JsonRequestBehavior.AllowGet);


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
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;
        bool bDtlGrd = false;
        string sDtlShipmentNos = string.Empty;
        string sDtlBaleNos = string.Empty;
        string fromGrade = "";
        public static DataSet purdata = new DataSet();
        [HttpPost]
        public JsonResult StartFumigateComplete(ListFumigationReceipt LFR)
        {
            fromGrade = (string)Session["GRADE"];
            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LFR.FumigationReceipts);


                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string FUMIGATION_BATCH = dtGridLst.Rows[s]["FUMIGATION_BATCH"].ToString();
                    string CASE_NUMBER = dtGridLst.Rows[s]["CASE_NUMBER"].ToString();
                    string ORGN_CODE = dtGridLst.Rows[s]["ORGN_CODE"].ToString();
                    string FUMIGATION_DAYS_FOR_RUNPREIOD = dtGridLst.Rows[s]["FUMIGATION_DAYS_FOR_RUNPREIOD"].ToString();
                    string FUMIGATION_DAYS_FOR_EXPIRY = dtGridLst.Rows[s]["FUMIGATION_DAYS_FOR_EXPIRY"].ToString();
                    string FUMIGATED_BY = Session["userID"].ToString();
                    //string FUMIGATION_STARTING_DATE = getdate().ToString();
                    //////////      ///////////////////////////
                    if (ORGN_CODE == null)
                    {

                        data = "Error: Please select the Location code for viewing ...";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    if (FUMIGATION_DAYS_FOR_RUNPREIOD.Length == 0)
                    {
                        data = "Error: Please enter the Run Period (in days)";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    if (FUMIGATION_DAYS_FOR_EXPIRY.Length == 0)
                    {
                        data = "Error: Please enter the Expiry Period (in days)";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    try
                    {
                        if (Convert.ToInt32(FUMIGATION_DAYS_FOR_EXPIRY) != 0 && Convert.ToInt32(FUMIGATION_DAYS_FOR_RUNPREIOD) != 0 && Convert.ToInt32(FUMIGATION_DAYS_FOR_EXPIRY) <= Convert.ToInt32(FUMIGATION_DAYS_FOR_RUNPREIOD))
                        {
                            data = "Error: Fumigation Expiry Period must be Greater than Run Period and Both Should not be Zero";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                    }
                    catch (Exception)
                    {
                        data = "Error: Fumigation Expiry Period must be Greater than Run Period and Both Should not be Zero";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }


                    //string strHeadeIDs = "";
                    //string strHeaderIDWithGrades = "";
                    //string sGrade = string.Empty;
                    //bool bolIsChecked = false;


                    //////////////////////////////////////////////////////////////
                    CommonManagement CMgt = new CommonManagement();
                    DataTable dtclstr = new DataTable();
                    DataTable dt1 = new DataTable();
                    DataTable orgdata = new DataTable();

                    try
                    {
                        string sSql = string.Empty;
                        if (!string.IsNullOrEmpty(FUMIGATION_BATCH))
                        {


                            sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH,H.RECEIVER_ORGN_CODE AS ORGN_CODE,GPIL_BALE_NUMBER AS CASE_NUMBER,";
                            sSql = sSql + " H.RECEIVED_BY AS FUMIGATED_BY,GETDATE () AS FUMIGATION_STARTING_DATE,'" + FUMIGATION_DAYS_FOR_RUNPREIOD + "' AS FUMIGATION_DAYS_FOR_RUNPREIOD,'";
                            sSql = sSql + FUMIGATION_DAYS_FOR_EXPIRY + "' AS FUMIGATION_DAYS_FOR_EXPIRY,'DIRECT-FUM' AS REMARKS FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H";
                            sSql = sSql + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND H.SHIPMENT_NO ='" + FUMIGATION_BATCH + "' and D.GRADE IN '" + fromGrade + "')";
                            //sSql = sSql + sGradeNos + ")";

                            dtclstr = CMgt.GetQueryResult(sSql);
                            //SqlDataAdapter objSqlDataAdapter1 = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                            //objSqlDataAdapter1.SelectCommand.CommandTimeout = 0;
                            //dtclstr.Clear();
                            //objSqlDataAdapter1.Fill(dtclstr);
                        }

                        if (!string.IsNullOrEmpty(FUMIGATION_BATCH))
                        {
                            DataTable dt = new DataTable();
                            sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH,H.RECEIVER_ORGN_CODE AS ORGN_CODE,GPIL_BALE_NUMBER AS CASE_NUMBER,";
                            sSql = sSql + " H.RECEIVED_BY AS FUMIGATED_BY,GETDATE () AS FUMIGATION_STARTING_DATE,'" + FUMIGATION_DAYS_FOR_RUNPREIOD + "' AS FUMIGATION_DAYS_FOR_RUNPREIOD,'";
                            sSql = sSql + FUMIGATION_DAYS_FOR_EXPIRY + "' AS FUMIGATION_DAYS_FOR_EXPIRY,'DIRECT-FUM' AS REMARKS FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H";
                            sSql = sSql + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND H.SHIPMENT_NO ='" + FUMIGATION_BATCH + "' ";
                            sSql = sSql + " AND D.GPIL_BALE_NUMBER ='" + CASE_NUMBER + "'";

                            dt1 = CMgt.GetQueryResult(sSql);
                            //SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                            //da.SelectCommand.CommandTimeout = 0;
                            //da.Fill(dt);
                            if (dt1.Rows.Count > 0)
                            {
                                dtclstr.Merge(dt1);
                            }
                        }

                        if (!string.IsNullOrEmpty(FUMIGATION_BATCH))
                        {
                            //string strbatch = "SELECT DISTINCT '1' AS FUMIGATION_BATCH FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
                            sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND";
                            sSql = sSql + " H.SHIPMENT_NO ='" + FUMIGATION_BATCH + "' and D.GRADE ='" + fromGrade + "'";
                            orgdata = CMgt.GetQueryResult(sSql);
                            //SqlDataAdapter objSqlDataAdapter2 = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                            //objSqlDataAdapter2.SelectCommand.CommandTimeout = 0;
                            //orgdata.Clear();
                            //objSqlDataAdapter2.Fill(orgdata);
                        }
                        if (!string.IsNullOrEmpty(FUMIGATION_BATCH))
                        {
                            DataTable dt = new DataTable();
                            sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND";
                            sSql = sSql + " H.SHIPMENT_NO ='" + FUMIGATION_BATCH + "' and D.GPIL_BALE_NUMBER ='" + CASE_NUMBER + "'";
                            dt = CMgt.GetQueryResult(sSql);
                            //SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
                            //da.SelectCommand.CommandTimeout = 0;
                            //da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                orgdata.Merge(dt);
                            }
                        }

                        //////////////////purchasedata();//////////////////////////


                        if (orgdata.Rows.Count > 0)
                        {

                            purdata.Tables.Clear();

                            string tablename = orgdata.Rows[s]["FUMIGATION_BATCH"].ToString();
                            purdata.Tables.Add(tablename);

                            purdata.Tables[s].Columns.Add("FUMIGATION_BATCH");
                            purdata.Tables[s].Columns.Add("ORGN_CODE");
                            purdata.Tables[s].Columns.Add("CASE_NUMBER");
                            purdata.Tables[s].Columns.Add("FUMIGATED_BY");
                            purdata.Tables[s].Columns.Add("FUMIGATION_STARTING_DATE");
                            purdata.Tables[s].Columns.Add("FUMIGATION_DAYS_FOR_RUNPREIOD");
                            purdata.Tables[s].Columns.Add("FUMIGATION_DAYS_FOR_EXPIRY");
                            purdata.Tables[s].Columns.Add("REMARKS");


                            DataRow[] purrows = dtclstr.Select("FUMIGATION_BATCH ='" + orgdata.Rows[s]["FUMIGATION_BATCH"].ToString() + "'");
                            // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                            if (purrows.Length > 0)
                            {
                                foreach (DataRow row in purrows)
                                {
                                    purdata.Tables[s].ImportRow(row);
                                }
                            }



                            ///////////////////////////////////////////////////////////

                            int i = 0;
                            ///////////////VALIDATE////////////////////////

                            for (int d = 0; d < purdata.Tables.Count; d++)
                            {
                                string strOrgn = "";

                                for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                                {
                                    string strCaseNo = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();

                                    if (h == 0)
                                    {
                                        string strFumBatch = purdata.Tables[d].Rows[0]["FUMIGATION_BATCH"].ToString();

                                        strOrgn = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                                        string strPicker = purdata.Tables[d].Rows[0]["FUMIGATED_BY"].ToString();

                                        string strStartingDate = purdata.Tables[d].Rows[0]["FUMIGATION_STARTING_DATE"].ToString();
                                        string strRunDays = purdata.Tables[d].Rows[0]["FUMIGATION_DAYS_FOR_RUNPREIOD"].ToString();
                                        string strExpDays = purdata.Tables[d].Rows[0]["FUMIGATION_DAYS_FOR_EXPIRY"].ToString();

                                        string strRemarks = purdata.Tables[d].Rows[0]["REMARKS"].ToString();
                                        string baleno1 = purdata.Tables[d].Rows[0]["CASE_NUMBER"].ToString();


                                        bool bolIsHeaderError = false;

                                        string strQuery1 = "select * from mLocations(NOLOCK) where LocCode='" + strOrgn + "' and LocType='PSW'";
                                        string strQuery2 = "select * from mUsers(NOLOCK) where UserName='" + strPicker + "' and LocCode='" + strOrgn + "'";

                                        dt = CMgt.GetQueryResult(strQuery1);
                                        if (dt.Rows.Count == 0)
                                        {
                                            data = "Error: Fumigation Location Type must be PSW's Location  :Batch no--" + strFumBatch;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;

                                            //bolIsHeaderError = true;
                                            //errordata = errordata + Environment.NewLine + "Fumigation Location Type must be PSW's Location  :Batch no--" + strFumBatch;

                                        }
                                        //if (FuncBoolIsExist(strQuery2) == false)
                                        //{
                                        //    bolIsHeaderError = true;
                                        //    errordata = errordata + Environment.NewLine + "Fumigated By (Fumigater code) doesn't exist :Batch no--" + strFumBatch;

                                        //}

                                        int intRunDays = 0;
                                        try
                                        {
                                            intRunDays = Convert.ToInt32(strRunDays.Trim());
                                        }
                                        catch (Exception ex)
                                        {
                                            data = "Error: Fumigation Run Period must be Numberic  :Batch no--" + strFumBatch;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                            //bolIsHeaderError = true;
                                            //errordata = errordata + Environment.NewLine + "Fumigation Run Period must be Numberic  :Batch no--" + strFumBatch;

                                        }

                                        try
                                        {
                                            DateTime objDateTime = Convert.ToDateTime(strStartingDate);
                                        }
                                        catch (Exception ex)
                                        {
                                            data = "Error: Fumigation Starting Date must be Datetime Field  :Batch no--" + strFumBatch;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                            //bolIsHeaderError = true;
                                            //errordata = errordata + Environment.NewLine + "Fumigation Starting Date must be Datetime Field  :Batch no--" + strFumBatch;

                                        }



                                        int intExpDays = 0;
                                        try
                                        {
                                            intExpDays = Convert.ToInt32(strExpDays.Trim());
                                        }
                                        catch (Exception ex)
                                        {
                                            data = "Error: Fumigation Expiry Period must be Numberic  :Batch no--" + strFumBatch;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                            //bolIsHeaderError = true;
                                            //errordata = errordata + Environment.NewLine + "Fumigation Expiry Period must be Numberic  :Batch no--" + strFumBatch;

                                        }

                                        if (intExpDays != 0 && intRunDays != 0 && intExpDays > intRunDays)
                                        {
                                            //valid
                                        }
                                        else
                                        {
                                            data = "Error: Fumigation Expiry Period must be Greater than Run Preiod and Both Should not be Zero  :Batch no--" + strFumBatch;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                            //bolIsHeaderError = true;
                                            //errordata = errordata + Environment.NewLine + "Fumigation Expiry Period must be Greater than Run Preiod and Both Should not be Zero  :Batch no--" + strFumBatch;

                                        }


                                        if (strRemarks.Length > 50)
                                        {
                                            data = "Error: Remarks should not Exist more than 50 Characters  :Batch no--" + strFumBatch;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                            //bolIsHeaderError = true;
                                            //errordata = errordata + Environment.NewLine + "Remarks should not Exist more than 50 Characters  :Batch no--" + strFumBatch;
                                        }

                                        if (bolIsHeaderError == true)
                                        {
                                            i = i + 1;
                                        }

                                    }


                                    if (strCaseNo == "" || strCaseNo.Trim().Length != 31)
                                    {
                                        i = i + 1;
                                        data = "Error: Case Number is In-Valid Case Number--" + strCaseNo;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                        //errordata = errordata + Environment.NewLine + "Case Number is In-Valid Case Number--" + strCaseNo;
                                    }
                                    else
                                    {
                                        bool bolIsDetailError = false;

                                        string strsql = "select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE,WMS_STATUS,FUMIGATION_STATUS,GRADE from GPIL_STOCK(NOLOCK) where";
                                        strsql = strsql + " GPIL_BALE_NUMBER='" + strCaseNo + "' and CURR_ORGN_CODE='" + strOrgn + "' AND STATUS='Y' ";
                                        if (!string.IsNullOrEmpty(FUMIGATION_BATCH))
                                        {
                                            strsql = strsql + " UNION ALL select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE,WMS_STATUS,FUMIGATION_STATUS,GRADE from GPIL_STOCK(NOLOCK)";
                                            strsql = strsql + " where GPIL_BALE_NUMBER ='" + CASE_NUMBER + "' and CURR_ORGN_CODE='" + strOrgn + "' AND STATUS='Y'";

                                            strsql = "SELECT DISTINCT * FROM(" + strsql + ")T1";
                                        }
                                        dt = CMgt.GetQueryResult(strsql);
                                        //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                                        //cmd.CommandTimeout = 0;
                                        //strrs = cmd.ExecuteReader();
                                        if (dt.Rows.Count >= 0)
                                        {
                                            string srksubcd;
                                            double mrkdwt;
                                            string processts;
                                            mrkdwt = Convert.ToDouble(dt.Rows[0][0]);
                                            processts = Convert.ToString(dt.Rows[0][1]);
                                            srksubcd = Convert.ToString(dt.Rows[0][2]);

                                            string strWMSStatus = Convert.ToString(dt.Rows[0][3]);
                                            string strFumigationStatus = Convert.ToString(dt.Rows[0][4]);
                                            string strCaseGrade = Convert.ToString(dt.Rows[0][5]);

                                            if (processts == "Y")
                                            {

                                                data = "Error: Case Is using in Another Process Case Number--" + strCaseNo;
                                                json = JsonConvert.SerializeObject(data);
                                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                                jsonResult.MaxJsonLength = int.MaxValue;
                                                return jsonResult;
                                                //bolIsDetailError = true;
                                                //errordata = errordata + Environment.NewLine + "Case Is using in Another Process Case Number--" + strCaseNo;
                                            }
                                            if (strWMSStatus != "Y")
                                            {
                                                data = "Error: Case Number not Yet Moved to WMS; Case Number--" + strCaseNo;
                                                json = JsonConvert.SerializeObject(data);
                                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                                jsonResult.MaxJsonLength = int.MaxValue;
                                                return jsonResult;
                                                //bolIsDetailError = true;
                                                //errordata = errordata + Environment.NewLine + "Case Number not Yet Moved to WMS; Case Number--" + strCaseNo;
                                            }
                                            if (strFumigationStatus.Trim() == "FUM-U")
                                            {
                                                data = "Error: Case is Already in Under Fumigation; Case Number--" + strCaseNo;
                                                json = JsonConvert.SerializeObject(data);
                                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                                jsonResult.MaxJsonLength = int.MaxValue;
                                                return jsonResult;
                                                //bolIsDetailError = true;
                                                //errordata = errordata + Environment.NewLine + "Case is Already in Under Fumigation; Case Number--" + strCaseNo;
                                            }



                                            if (bolIsDetailError == true)
                                            {
                                                i = i + 1;
                                            }

                                            //strrs.Close();
                                            //cmd.Dispose();

                                        }
                                        else
                                        {
                                            i = i + 1;
                                            data = "Error: Case Number Does Not Exists Case Number--" + strCaseNo;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                            //errordata = errordata + Environment.NewLine + "Case Number Does Not Exists Case Number--" + strCaseNo;
                                            //strrs.Close();
                                            //cmd.Dispose();
                                        }


                                    }
                                }

                            }
                            if (i == 0)
                            {

                                //return true;
                            }
                            else
                            {
                                //Errorlog err = new Errorlog();
                                //errfile = err.WriteErrorLog(errordata, "Dispatch_Error_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                                //return false;
                            }

                            ///////////////////////////////////////////////




                            ///////////////////////////////////////INSERT/////////////////////


                            string retVal = "";
                            SqlTransaction trx = ClsConnection.SqlCon.BeginTransaction();
                            try
                            {

                                for (int d = 0; d < purdata.Tables.Count; d++)
                                {
                                    int cnt = 0;
                                    /// Generating Header Id
                                    string temphdr = "FUM" + purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                                    string strOrgn = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                                    string strFumBy = purdata.Tables[d].Rows[0]["FUMIGATED_BY"].ToString();
                                    string strFumStartDate = purdata.Tables[d].Rows[0]["FUMIGATION_STARTING_DATE"].ToString();
                                    string strRunPriod = purdata.Tables[d].Rows[0]["FUMIGATION_DAYS_FOR_RUNPREIOD"].ToString();
                                    string strExpiryPreiod = purdata.Tables[d].Rows[0]["FUMIGATION_DAYS_FOR_EXPIRY"].ToString();
                                    string strRemarks = purdata.Tables[d].Rows[0]["REMARKS"].ToString();

                                    string strFumEndDate = "";
                                    string strFumExpDate = "";

                                    int intAddDays = Convert.ToInt32(strRunPriod) + Convert.ToInt32(strExpiryPreiod);
                                    //strFumEndDate = Convert.ToDateTime(strFumStartDate).AddDays(Convert.ToInt32(strRunPriod)).ToString();
                                    //strFumExpDate = Convert.ToDateTime(strFumStartDate).AddDays(intAddDays).ToString();

                                    strFumEndDate = Convert.ToDateTime(DateTime.Now.ToString()).AddDays(Convert.ToInt32(strRunPriod)).ToString();
                                    strFumExpDate = Convert.ToDateTime(DateTime.Now.ToString()).AddDays(intAddDays).ToString();


                                    //strsql = "INSERT INTO [GPIL_FUMIGATION_HDR_TEMP] ([BATCH_NO],[ORGN_CODE],[FUMIGATION_STARTING_DATE],[FUMIGATION_ENDING_DATE] ,[FUMIGATION_DAYS_FOR_RUNPREIOD] ,[FUMIGATION_DAYS_FOR_EXPIRY] ,[CREATED_BY] ,[CREATED_DATE],[STATUS],[REMARKS])";
                                    //strsql = strsql + " VALUES('" + temphdr + "','" + strOrgn + "',CONVERT(DATETIME,'" + strFumStartDate + "',105),CONVERT(DATETIME,'" + strFumEndDate + "',105) ,'" + strRunPriod + "' ,'" + strExpiryPreiod + "' ,'" + strFumBy + "' ,CONVERT(DATETIME,'" + strFumStartDate + "',105),'Y','" + strRemarks + "')";

                                    string strsql = "INSERT INTO [GPIL_FUMIGATION_HDR_TEMP] ([BATCH_NO],[ORGN_CODE],[FUMIGATION_STARTING_DATE],[FUMIGATION_ENDING_DATE] ,[FUMIGATION_DAYS_FOR_RUNPREIOD] ,[FUMIGATION_DAYS_FOR_EXPIRY] ,[CREATED_BY] ,[CREATED_DATE],[STATUS],[REMARKS])";
                                    strsql = strsql + " VALUES('" + temphdr + "','" + strOrgn + "',getdate(),getdate()+" + strRunPriod + " ,'" + strRunPriod + "' ,'" + strExpiryPreiod + "' ,'" + strFumBy + "' ,getdate(),'Y','" + strRemarks + "')";

                                    CMgt.UpdateUsingExecuteNonQuery(strsql);
                                    //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                                    //cmd.CommandTimeout = 0;
                                    //cmd.Transaction = trx;
                                    //cmd.ExecuteNonQuery();
                                    //cmd.Dispose();

                                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                                    {
                                        string strCaseNo = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();



                                        //strsql = "INSERT INTO [GPIL_FUMIGATION_DTLS_TEMP] ([DETAIL_ID],[BATCH_NO],[CASE_NUMBER],[FUMIGATION_STARTING_DATE],[FUMIGATION_ENDING_DATE],[FUMIGATION_EXPIRY_DATE],[CREATED_BY],[CREATED_DATE],[STATUS],[FUM_STATUS])";
                                        //strsql = strsql + " VALUES ('FUM" + strOrgn + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + temphdr + "','" + strCaseNo + "',CONVERT(DATETIME,'" + strFumStartDate + "',105),CONVERT(DATETIME,'" + strFumEndDate + "',105),CONVERT(DATETIME,'" + strFumExpDate + "',105),'" + strFumBy + "',CONVERT(DATETIME,'" + strFumStartDate + "',105),'Y','FUM-U')";

                                        strsql = "INSERT INTO [GPIL_FUMIGATION_DTLS_TEMP] ([DETAIL_ID],[BATCH_NO],[CASE_NUMBER],[FUMIGATION_STARTING_DATE],[FUMIGATION_ENDING_DATE],[FUMIGATION_EXPIRY_DATE],[CREATED_BY],[CREATED_DATE],[STATUS],[FUM_STATUS])";
                                        strsql = strsql + " VALUES ('FUM" + strOrgn + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + temphdr + "','" + strCaseNo + "',getdate(),getdate()+" + strRunPriod + ",getdate()+" + intAddDays.ToString() + ",'" + strFumBy + "',getdate(),'Y','FUM-U')";
                                        CMgt.UpdateUsingExecuteNonQuery(strsql);
                                        //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                                        //cmd.CommandTimeout = 0;
                                        //cmd.Transaction = trx;
                                        //cmd.ExecuteNonQuery();
                                        //cmd.Dispose();


                                        strsql = "update GPIL_STOCK set FUMIGATION_STATUS='FUM-U',FUMIGATION_STARTING_DATE=getdate(),FUMIGATION_ENDING_DATE=getdate()+";
                                        strsql = strsql + strRunPriod + ",FUMIGATION_EXPIRY_DATE=getdate()+" + intAddDays.ToString() + ",PROCESS_STATUS='Y',BATCH_NO='";
                                        strsql = strsql + temphdr + "',LAST_UPDATED_BY='" + strFumBy + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + strCaseNo.Trim() + "'";
                                        CMgt.UpdateUsingExecuteNonQuery(strsql);
                                        //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                                        //cmd.CommandTimeout = 0;
                                        //cmd.Transaction = trx;
                                        //cmd.ExecuteNonQuery();
                                        //cmd.Dispose();


                                    }

                                    strsql = "UPDATE GPIL_FUMIGATION_HDR_TEMP SET STATUS='C' WHERE BATCH_NO='" + temphdr + "'";
                                    CMgt.UpdateUsingExecuteNonQuery(strsql);
                                    //cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                                    //cmd.CommandTimeout = 0;
                                    //cmd.Transaction = trx;
                                    //cmd.ExecuteNonQuery();
                                    //cmd.Dispose();


                                    string SPName = "FUMIGATION_BATCH";
                                    List<SqlParameter> parameters = new List<SqlParameter>();
                                    SqlParameter[] pram = new SqlParameter[3];
                                    pram[0] = (new SqlParameter("@refNo", SqlDbType.NVarChar, 50));
                                    pram[1] = (new SqlParameter("@status", SqlDbType.NVarChar, 50));

                                    pram[0].Value = temphdr;
                                    pram[1].Direction = ParameterDirection.Output;

                                    for (int k = 0; k < pram.Length; k++)
                                    {
                                        parameters.Add(pram[k]);
                                    }
                                    VerificationManagement vMgt = new VerificationManagement();
                                    vMgt.SP_ExecuteNonQuery(parameters, SPName);
                                    if (Convert.ToString(parameters[1].Value) == "1")
                                    {
                                        retVal = "Success: " + Convert.ToString(parameters[1].Value);
                                    }
                                    else
                                    {
                                        retVal = "Error: " + Convert.ToString(parameters[1].Value);
                                    }
                                    //return retVal;

                                    //cmd = new SqlCommand();
                                    //cmd.CommandTimeout = 0;
                                    //cmd.Connection = ClsConnection.SqlCon;
                                    //ClsConnection.connectDB();
                                    //cmd.Transaction = trx;
                                    //cmd.CommandType = CommandType.StoredProcedure;
                                    //cmd.CommandText = "FUMIGATION_BATCH";
                                    //cmd.Parameters.Add(new SqlParameter("@refNo", SqlDbType.VarChar, 50));
                                    //cmd.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                                    //cmd.Parameters["@refNo"].Value = temphdr;
                                    //cmd.ExecuteNonQuery();
                                    //retVal = "/" + retVal + cmd.Parameters["@status"].Value.ToString();

                                }
                                //trx.Commit();
                                //dtclstr.Clear();
                                //trx.Dispose();

                                data = "Error: DONE where Fumigation are" + retVal;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                                //lblMessage.Text = "DONE where Fumigation are" + retVal;
                                //Errorlog err = new Errorlog();
                                //errfile = err.WriteErrorLog(retVal, "Fumigation_SUCESS_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                                //btndwnerr.Visible = true;
                                //return true;
                            }
                            catch (Exception ex)
                            {

                                data = "Error: " + ex.Message;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                                //lblMessage.Text = ex.Message;
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                                //trx.Rollback();
                                //trx.Dispose();
                                //return false;
                            }
                            finally
                            {
                                trx.Dispose();
                            }


                            ////////////////////////////////////////////////////////////////

                            //if (insertinfo() == true)
                            //{
                            string strUpdate = string.Empty;
                            if (!string.IsNullOrEmpty(FUMIGATION_BATCH.Trim()))
                            {
                                //string strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
                                strUpdate = "UPDATE GPIL_SHIPMENT_DTLS SET ATTRIBUTE1='Y' WHERE DETAIL_ID IN (SELECT D.DETAIL_ID FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,";
                                strUpdate = strUpdate + " GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO = '"+ FUMIGATION_BATCH + "' and";
                                strUpdate = strUpdate + " D.GRADE = '" + fromGrade + "')";
                                CMgt.UpdateUsingExecuteNonQuery(strUpdate);
                                //SqlCommand objSqlCommand = new SqlCommand(strUpdate, ClsConnection.SqlCon);
                                //objSqlCommand.CommandTimeout = 0;
                                //objSqlCommand.ExecuteNonQuery();
                                //objSqlCommand.Dispose();

                                //strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
                                strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO = '" + FUMIGATION_BATCH + "' AND SHIPMENT_NO NOT IN (SELECT DISTINCT H.SHIPMENT_NO FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND D.SHIPMENT_NO = '" + FUMIGATION_BATCH + "')";
                                CMgt.UpdateUsingExecuteNonQuery(strUpdate);
                                //SqlCommand objSqlCommand1 = new SqlCommand(strUpdate, ClsConnection.SqlCon);
                                //objSqlCommand1.CommandTimeout = 0;
                                //objSqlCommand1.ExecuteNonQuery();
                                //objSqlCommand1.Dispose();
                            }

                            if (bDtlGrd)
                            {
                                strUpdate = "UPDATE GPIL_SHIPMENT_DTLS SET ATTRIBUTE1='Y' WHERE SHIPMENT_NO=" + FUMIGATION_BATCH + " and GPIL_BALE_NUMBER = '" + CASE_NUMBER + "'";

                                CMgt.UpdateUsingExecuteNonQuery(strUpdate);
                                //SqlCommand cmd = new SqlCommand(strUpdate, ClsConnection.SqlCon);
                                //cmd.CommandTimeout = 0;
                                //cmd.ExecuteNonQuery();
                                //cmd.Dispose();

                              

                                strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO =" + FUMIGATION_BATCH + " AND SHIPMENT_NO NOT IN (SELECT DISTINCT H.SHIPMENT_NO FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND D.SHIPMENT_NO =" + FUMIGATION_BATCH + ")";
                                CMgt.UpdateUsingExecuteNonQuery(strUpdate);
                                //SqlCommand objSqlCommand1 = new SqlCommand(strUpdate, ClsConnection.SqlCon);
                                //objSqlCommand1.CommandTimeout = 0;
                                //objSqlCommand1.ExecuteNonQuery();
                                //objSqlCommand1.Dispose();
                            }
                            //return true;
                          

                        }
                        
                    }
                    catch (Exception ex)
                    {
                        //return false;
                    }

                    ////////////////////////////////////////////////////////////////////
                    
                }


                data = "Success: ";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;




            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        //string lblMessage = string.Empty;
        //string data = String.Empty;
        //JsonResult jsonResult;
        //public JsonResult GetTable2Details(string shipmentNumber, string baleNumber)
        //{
        //    Session["ShipmentNumber"] = shipmentNumber;
        //    Session["BaleNumber"] = baleNumber;
        //    string json = "";
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}

        //string shpNumbr = "";
        //string grade = "";
        //string baleNumber = "";
        //string json = string.Empty;

        //public JsonResult GetFumigationStart(string orgnCode, string fumigationPeriod, string expiory)
        //{
        //    Session["OrgnCode"] = orgnCode;
        //    Session["FumigationPeriod"] = fumigationPeriod;
        //    Session["Expiory"] = expiory;




        //    shpNumbr = (string)Session["SHIPMENT_NO"];
        //    grade = (string)Session["GRADE"];
        //    baleNumber = (string)Session["BaleNumber"];
        //    string labelmsg = string.Empty;
        //    if (orgnCode == "")
        //    {
        //        labelmsg = "Error: Please select the Location code for viewing ...";
        //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "alert('Please select the Location code for viewing ...');", true);
        //        //return;
        //    }
        //    else if (fumigationPeriod.Length == 0)
        //    {
        //        labelmsg = "Error: Please enter the Run Period (in days)";
        //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "alert('Please enter the Run Period (in days)');", true);
        //        //return;
        //    }
        //    else if (expiory.Length == 0)
        //    {
        //        labelmsg = "Error: Please enter the Expiry Period (in days)";
        //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "alert('Please enter the Expiry Period (in days)');", true);
        //        //return;
        //    }
        //    string sDtlShipmentNos = string.Empty;
        //    string sDtlBaleNos = string.Empty;

        //    string strHeadeIDs = "";
        //    string strHeaderIDWithGrades = "";
        //    string sGrade = string.Empty;
        //    bool bolIsChecked = false;
        //    try
        //    {
        //        if (Convert.ToInt32(expiory) != 0 && Convert.ToInt32(fumigationPeriod) != 0 && Convert.ToInt32(expiory) <= Convert.ToInt32(fumigationPeriod))
        //        {
        //            labelmsg = "Error: Fumigation Expiry Period must be Greater than Run Period and Both Should not be Zero";
        //            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + strMsg + "');", true);
        //            //return;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        labelmsg = "Error: Fumigation Expiry Period must be Greater than Run Period and Both Should not be Zero";
        //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + strMsg + "');", true);
        //        //return;
        //    }
        //    strHeaderIDWithGrades = "'" + shpNumbr + grade + "'";


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
        //        data = "Success: Fumigation Batch has been Created for Selected Shipments - " + Convert.ToString(strHeadeIDs) + " Bales";
        //        json = JsonConvert.SerializeObject(data);
        //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;

        //    }


        //    //labelmsg = "Success: Fumigation Batch has been Created for Selected Shipments - " + Convert.ToString(strHeadeIDs) + " Bales";


        //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Fumigation Receipt", "alert('Fumigation Batch has been Created for Selected Shipments - " + Convert.ToString(strHeadeIDs) + " Bales');", true);
        //    //ddlLocationCode.SelectedIndex = 0;
        //    //txtRunPeriod.Text = "";
        //    //txtExpiryPeriod.Text = "";
        //    //gvbind();



        //    //string json = "";
        //    //return Json(json, JsonRequestBehavior.AllowGet);
        //}
        //DataTable orgdata = new DataTable();

        //public bool FuncBoolCreateFumigationBatch(string inParamStrShipmentNoGrades, string inParamStrShipmentNos, string sGradeNos)
        //{

        //    string strOrgnCode = (string)Session["OrgnCode"];
        //    string strFumigationPeriod = (string)Session["FumigationPeriod"];
        //    string strExpiory = (string)Session["Expiory"];
        //    shpNumbr = (string)Session["SHIPMENT_NO"];
        //    grade = (string)Session["GRADE"];
        //    baleNumber = (string)Session["BaleNumber"];
        //    try
        //    {
        //        DataTable dt12 = new DataTable();
        //        string sSql = string.Empty;
        //        if (!string.IsNullOrEmpty(inParamStrShipmentNos.Trim()))
        //        {
        //            //string strReceivedBales = "SELECT DISTINCT GPIL_BALE_NUMBER FROM GPIL_SHIPMENT_DTLS WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
        //            //string strReceivedBales = "SELECT DISTINCT '1' AS FUMIGATION_BATCH,H.RECEIVER_ORGN_CODE AS ORGN_CODE,GPIL_BALE_NUMBER AS CASE_NUMBER,H.RECEIVED_BY AS FUMIGATED_BY,GETDATE () AS FUMIGATION_STARTING_DATE,'" + txtRunPeriod.Text + "' AS FUMIGATION_DAYS_FOR_RUNPREIOD,'" + txtExpiryPeriod.Text + "' AS FUMIGATION_DAYS_FOR_EXPIRY,'DIRECT-FUM' AS REMARKS FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
        //            //string sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH,H.RECEIVER_ORGN_CODE AS ORGN_CODE,GPIL_BALE_NUMBER AS CASE_NUMBER,";
        //            //sSql = sSql + " H.RECEIVED_BY AS FUMIGATED_BY,GETDATE () AS FUMIGATION_STARTING_DATE,'" + txtRunPeriod.Text + "' AS FUMIGATION_DAYS_FOR_RUNPREIOD,'";
        //            //sSql = sSql + txtExpiryPeriod.Text + "' AS FUMIGATION_DAYS_FOR_EXPIRY,'DIRECT-FUM' AS REMARKS FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H";
        //            //sSql = sSql + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND H.SHIPMENT_NO + D.GRADE IN (";
        //            //sSql = sSql + inParamStrShipmentNoGrades + ")";

        //            sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH,H.RECEIVER_ORGN_CODE AS ORGN_CODE,GPIL_BALE_NUMBER AS CASE_NUMBER,";
        //            sSql = sSql + " H.RECEIVED_BY AS FUMIGATED_BY,GETDATE () AS FUMIGATION_STARTING_DATE,'" + strFumigationPeriod + "' AS FUMIGATION_DAYS_FOR_RUNPREIOD,'";
        //            sSql = sSql + strExpiory + "' AS FUMIGATION_DAYS_FOR_EXPIRY,'DIRECT-FUM' AS REMARKS FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H";
        //            sSql = sSql + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND H.SHIPMENT_NO IN(" + inParamStrShipmentNos + ") and D.GRADE IN (";
        //            sSql = sSql + sGradeNos + ")";

        //            dt12 = CMgt.GetQueryResult(sSql);
        //            //SqlDataAdapter objSqlDataAdapter1 = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
        //            //objSqlDataAdapter1.SelectCommand.CommandTimeout = 0;
        //            //dtclstr.Clear();
        //            //objSqlDataAdapter1.Fill(dtclstr);
        //        }

        //        //if (bDtlGrd)
        //        //{
        //        //    DataTable dt = new DataTable();
        //        //    sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH,H.RECEIVER_ORGN_CODE AS ORGN_CODE,GPIL_BALE_NUMBER AS CASE_NUMBER,";
        //        //    sSql = sSql + " H.RECEIVED_BY AS FUMIGATED_BY,GETDATE () AS FUMIGATION_STARTING_DATE,'" + strFumigationPeriod + "' AS FUMIGATION_DAYS_FOR_RUNPREIOD,'";
        //        //    sSql = sSql + strExpiory + "' AS FUMIGATION_DAYS_FOR_EXPIRY,'DIRECT-FUM' AS REMARKS FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H";
        //        //    sSql = sSql + " WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND H.SHIPMENT_NO IN(" + shpNumbr + ") ";
        //        //    sSql = sSql + " AND D.GPIL_BALE_NUMBER IN(" + baleNumber + ")";
        //        //    SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
        //        //    da.SelectCommand.CommandTimeout = 0;
        //        //    da.Fill(dt);
        //        //    if (dt.Rows.Count > 0)
        //        //    {
        //        //        dt.Merge(dt);
        //        //    }
        //        //}

        //        if (!string.IsNullOrEmpty(inParamStrShipmentNos.Trim()))
        //        {
        //            //string strbatch = "SELECT DISTINCT '1' AS FUMIGATION_BATCH FROM GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
        //            sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND";
        //            sSql = sSql + " H.SHIPMENT_NO IN(" + inParamStrShipmentNos + ") and D.GRADE IN (" + sGradeNos + ")";
        //            orgdata = CMgt.GetQueryResult(sSql);
        //            //SqlDataAdapter objSqlDataAdapter2 = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
        //            //objSqlDataAdapter2.SelectCommand.CommandTimeout = 0;
        //            //orgdata.Clear();
        //            //objSqlDataAdapter2.Fill(orgdata);
        //        }
        //        //if (bDtlGrd)
        //        //{
        //        //    DataTable dt = new DataTable();
        //        //    sSql = "SELECT DISTINCT '1' AS FUMIGATION_BATCH FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND";
        //        //    sSql = sSql + " H.SHIPMENT_NO IN(" + shpNumbr + ") and D.GPIL_BALE_NUMBER IN (" + baleNumber + ")";
        //        //    dt = CMgt.GetQueryResult(sSql);
        //        //    //SqlDataAdapter da = new SqlDataAdapter(sSql, ClsConnection.SqlCon);
        //        //    //da.SelectCommand.CommandTimeout = 0;
        //        //    //da.Fill(dt);
        //        //    if (dt.Rows.Count > 0)
        //        //    {
        //        //        orgdata.Merge(dt);
        //        //    }
        //        //}

        //        DataSet purdata = new DataSet();
        //        //Purchase Data
        //        int s = 0;
        //        //for (int s = 0; s < orgdata.Rows.Count; s++)
        //        if (orgdata.Rows.Count > 0)
        //        {

        //            purdata.Tables.Clear();

        //            string tablename = orgdata.Rows[s]["FUMIGATION_BATCH"].ToString();
        //            purdata.Tables.Add(tablename);

        //            purdata.Tables[s].Columns.Add("FUMIGATION_BATCH");
        //            purdata.Tables[s].Columns.Add("ORGN_CODE");
        //            purdata.Tables[s].Columns.Add("CASE_NUMBER");
        //            purdata.Tables[s].Columns.Add("FUMIGATED_BY");
        //            purdata.Tables[s].Columns.Add("FUMIGATION_STARTING_DATE");
        //            purdata.Tables[s].Columns.Add("FUMIGATION_DAYS_FOR_RUNPREIOD");
        //            purdata.Tables[s].Columns.Add("FUMIGATION_DAYS_FOR_EXPIRY");
        //            purdata.Tables[s].Columns.Add("REMARKS");


        //            DataRow[] purrows = dt12.Select("FUMIGATION_BATCH ='" + orgdata.Rows[s]["FUMIGATION_BATCH"].ToString() + "'");
        //            // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
        //            if (purrows.Length > 0)
        //            {
        //                foreach (DataRow row in purrows)
        //                {
        //                    purdata.Tables[s].ImportRow(row);
        //                }
        //            }

        //        }


        //        //if (insertinfo() == true)
        //        //{
        //        //    string strUpdate = string.Empty;
        //        //    if (!string.IsNullOrEmpty(inParamStrShipmentNos.Trim()))
        //        //    {
        //        //        //string strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
        //        //        strUpdate = "UPDATE GPIL_SHIPMENT_DTLS SET ATTRIBUTE1='Y' WHERE DETAIL_ID IN (SELECT D.DETAIL_ID FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,";
        //        //        strUpdate = strUpdate + " GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.SHIPMENT_NO IN(" + inParamStrShipmentNos + ") and";
        //        //        strUpdate = strUpdate + " D.GRADE IN (" + sGradeNos + "))";
        //        //        SqlCommand objSqlCommand = new SqlCommand(strUpdate, ClsConnection.SqlCon);
        //        //        objSqlCommand.CommandTimeout = 0;
        //        //        objSqlCommand.ExecuteNonQuery();
        //        //        objSqlCommand.Dispose();

        //        //        //strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ")";
        //        //        strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + inParamStrShipmentNos + ") AND SHIPMENT_NO NOT IN (SELECT DISTINCT H.SHIPMENT_NO FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND D.SHIPMENT_NO IN (" + inParamStrShipmentNos + "))";
        //        //        SqlCommand objSqlCommand1 = new SqlCommand(strUpdate, ClsConnection.SqlCon);
        //        //        objSqlCommand1.CommandTimeout = 0;
        //        //        objSqlCommand1.ExecuteNonQuery();
        //        //        objSqlCommand1.Dispose();
        //        //    }

        //        //    if (bDtlGrd)
        //        //    {
        //        //        strUpdate = "UPDATE GPIL_SHIPMENT_DTLS SET ATTRIBUTE1='Y' WHERE SHIPMENT_NO=" + sDtlShipmentNos + " and GPIL_BALE_NUMBER IN(" + sDtlBaleNos + ")";
        //        //        SqlCommand cmd = new SqlCommand(strUpdate, ClsConnection.SqlCon);
        //        //        cmd.CommandTimeout = 0;
        //        //        cmd.ExecuteNonQuery();
        //        //        cmd.Dispose();

        //        //        //strUpdate = "SELECT ATTRIBUTE1 FROM GPIL_SHIPMENT_DTLS(NOLOCK) WHERE SHIPMENT_NO=" + sDtlShipmentNos + " and ISNULL(ATTRIBUTE1,'')<>'Y' --and GPIL_BALE_NUMBER IN(" + sDtlBaleNos + ")";
        //        //        //SqlDataAdapter da = new SqlDataAdapter(strUpdate, ClsConnection.SqlCon);
        //        //        //da.SelectCommand.CommandTimeout = 0;
        //        //        //da.Fill(ds);
        //        //        //if (ds.Tables[0].Rows.Count > 0)
        //        //        //{
        //        //        //    strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='Y' WHERE SHIPMENT_NO IN (" + sDtlShipmentNos + ")";
        //        //        //    SqlCommand cmd1 = new SqlCommand(strUpdate, ClsConnection.SqlCon);
        //        //        //    cmd1.CommandTimeout = 0;
        //        //        //    cmd1.ExecuteNonQuery();
        //        //        //    cmd1.Dispose();
        //        //        //}

        //        //        strUpdate = "UPDATE GPIL_SHIPMENT_HDR SET WMS_FLAG='F' WHERE SHIPMENT_NO IN (" + sDtlShipmentNos + ") AND SHIPMENT_NO NOT IN (SELECT DISTINCT H.SHIPMENT_NO FROM GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND ISNULL(D.ATTRIBUTE1,'')<>'Y' AND D.SHIPMENT_NO IN (" + sDtlShipmentNos + "))";
        //        //        SqlCommand objSqlCommand1 = new SqlCommand(strUpdate, ClsConnection.SqlCon);
        //        //        objSqlCommand1.CommandTimeout = 0;
        //        //        objSqlCommand1.ExecuteNonQuery();
        //        //        objSqlCommand1.Dispose();
        //        //    }
        //        //    return true;
        //        //}
        //        //else
        //        //{
        //        //    return false;
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}





        /// <summary>
        /// FUMIGATION PARTIAL COMPLETE
        /// </summary>
        /// <returns></returns>
        public ActionResult FumigationPartialCompleteIndex()
        {
            return View();
        }


        [HttpGet]

        public ActionResult FumigationLoaction()
        {
            try
            {
                string strsql = "SELECT LocCode,LocCode FROM mLocations(NOLOCK) WHERE LocType='PSW'";
                dt = CMgt.GetQueryResult(strsql);
                dt.TableName = "Table";
                var data = dt;
                string json = JsonConvert.SerializeObject(data);
                return Json(json, JsonRequestBehavior.AllowGet);
                //var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
            }
            catch
            {

            }
            return Json(dt);
        }



        [HttpGet]

        public ActionResult GetFumigationBatch(string fumigationLocation)
        {

            try
            {

                string strsql = "SELECT BATCH_NO,BATCH_NO FROM GPIL_FUMIGATION_HDR(NOLOCK) WHERE ORGN_CODE='" + fumigationLocation + "' AND STATUS IN ('Y','P')";
                dt = CMgt.GetQueryResult(strsql);
                dt.TableName = "Table";
                var data = dt;
                string json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            { }

            return Json(ds);
        }

        [HttpGet]
        public ActionResult FumigationPartialComplete(string fumigationLocation, string fumigationBatch)
        {

            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT D.BATCH_NO,D.CASE_NUMBER,D.FUMIGATION_STARTING_DATE,D.FUMIGATION_ENDING_DATE,D.FUMIGATION_EXPIRY_DATE,'Under Fumigation' AS FUM_STATUS FROM GPIL_FUMIGATION_HDR(NOLOCK) H,GPIL_FUMIGATION_DTLS(NOLOCK) D WHERE H.BATCH_NO=D.BATCH_NO AND H.ORGN_CODE='" + fumigationLocation + "' AND H.BATCH_NO='" + fumigationBatch + "' AND H.STATUS IN ('Y','P') AND D.FUM_STATUS='FUM-U'";
            dtclstr = CMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        public EmptyResult PartialClose(string batchNumber, string caseNumber)
        {
            Session["BatchNumber"] = batchNumber;
            Session["CaseNumber"] = caseNumber;

            return new EmptyResult();
        }


        /// <summary>
        /// UNDER FUMIGATION DETAILS
        /// </summary>
        /// <returns></returns>
        public ActionResult UnderFumigationDetailsIndex()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.ORGN_TYPE == "WH" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }


        [HttpGet]
        public ActionResult UnderFumigationDetails(string orgnCode)
        {

            DataTable dtclstr = new DataTable();
            string strSqlQuery = "";
            strSqlQuery = "SELECT CURR_ORGN_CODE as ORGN_CODE,GPIL_BALE_NUMBER,FUMIGATION_STARTING_DATE as FUMIGATION_START_DATE,FUMIGATION_ENDING_DATE AS FUMIGATION_END_DATE FROM GPIL_STOCK(NOLOCK) S WHERE FUMIGATION_STATUS='FUM-U' AND S.STATUS='Y'";

            if (orgnCode.Length > 0 && orgnCode.Length != -1)
            {
                strSqlQuery = strSqlQuery + " AND CURR_ORGN_CODE='" + orgnCode + "' ";
            }
            strSqlQuery = strSqlQuery + " ORDER BY CURR_ORGN_CODE,GPIL_BALE_NUMBER,FUMIGATION_ENDING_DATE ";
            dtclstr = CMgt.GetQueryResult(strSqlQuery);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// FUMIGATION CASE DETAILS
        /// </summary>
        /// <returns></returns>
        public ActionResult FumigationCaseDetailsIndex()
        {
            ViewBag.GPIL_ITEM_MASTER = (from o in _data.GPIL_ITEM_MASTER where o.ATTRIBUTE2 == "FGD" && o.ITEM_GROUP == "PCG" && o.STATUS == "Y" select new { o.ITEM_CODE }).ToList();
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.ORGN_TYPE == "WH" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }
        //FumigationCase, Grade, ExpiryDay, NoOfCase, ExpiryDate, OrgnCode

        [HttpGet]
        public ActionResult FumigationCaseDetails(string fumigationCase, string grade, string expiryDay, string noOfCase, string expiryDate, string orgnCode)
        {
            string data = String.Empty, json = String.Empty;
            string lblMessage = string.Empty;

            DataTable dtclstr = new DataTable();
            string strSqlQuery = "";
            strSqlQuery = " CURR_ORGN_CODE as ORGN_CODE,GPIL_BALE_NUMBER,GRADE,convert(nvarchar(15),C.FumStartOn,105) AS FUMIGATION_START_DATE,convert(nvarchar(15),C.FumEndOn,105) AS FUMIGATION_END_DATE,convert(nvarchar(15),C.FumExpiredOn,105) AS FUMIGATION_EXPIRED_DATE,datediff(DAY,getdate(),FumExpiredOn)-1 as FUMIGATION_EXPIRES_IN  FROM GPIL_STOCK(NOLOCK) S,tCaseDetails(NOLOCK) c WHERE FUMIGATION_STATUS='FUM' AND S.STATUS='Y' AND C.CaseBarCode=S.GPIL_BALE_NUMBER ";

            if (orgnCode.Length != 0 && orgnCode.Length != -1)
            {
                strSqlQuery = strSqlQuery + " AND CURR_ORGN_CODE='" + orgnCode + "' ";
            }

            if (expiryDate != "" && expiryDate != "")
            {


                strSqlQuery = strSqlQuery + " AND ";

                //strSqlQuery = strSqlQuery + " convert(varchar(12), BC_WALD_INSERT_DT,101) between ";
                //strSqlQuery = strSqlQuery + " convert(varchar(12), '" + txt_Report_Date.Text + "',101) AND ";
                //strSqlQuery = strSqlQuery + " convert(varchar(12), '" + txtToDate.Text + "',101) ";

                if (fumigationCase.Length.ToString() == "G")
                {
                    strSqlQuery = strSqlQuery + " FumExpiredOn > ";
                }
                else
                {
                    strSqlQuery = strSqlQuery + " FumExpiredOn <= ";
                }

                strSqlQuery = strSqlQuery + " convert(varchar, '" + expiryDate + " 00:00:00',105) ";    ////datetime


            }



            if (expiryDay.Trim() != "")
            {
                strSqlQuery = strSqlQuery + " AND ";

                if (fumigationCase.Length.ToString() == "G")
                {
                    strSqlQuery = strSqlQuery + " FumExpiredOn > getdate() + " + expiryDay + "";
                }
                else
                {
                    strSqlQuery = strSqlQuery + " FumExpiredOn <= getdate() - " + expiryDay + "";
                }

            }

            if (grade.Length != 0 && grade.Length != -1)
            {
                strSqlQuery = strSqlQuery + " AND GRADE='" + grade + "' ";
            }

            //if (ddlGrade.SelectedIndex != 0 && ddlGrade.SelectedIndex != -1)
            //{
            if (noOfCase.Trim() != "")
            {
                strSqlQuery = "SELECT top " + noOfCase + " " + strSqlQuery;
            }
            else
            {
                strSqlQuery = "SELECT " + strSqlQuery;
            }

            strSqlQuery = strSqlQuery + " ORDER BY C.FumEndOn,CURR_ORGN_CODE,GRADE,GPIL_BALE_NUMBER ";

            dtclstr = CMgt.GetQueryResult(strSqlQuery);

            json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


            //if (dtclstr.Rows.Count > 0)
            //{
            //    json = JsonConvert.SerializeObject(dtclstr);
            //    return Json(json, JsonRequestBehavior.AllowGet);
            //lblCount.Text = "Total No. of Cases : " + ds.Tables[0].Rows.Count.ToString();
            //}
            // else
            //{
            //ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            //GridViewSamp.DataSource = ds;
            //GridViewSamp.DataBind();
            //int columncount = GridViewSamp.Rows[0].Cells.Count;
            //GridViewSamp.Rows[0].Cells.Clear();
            //GridViewSamp.Rows[0].Cells.Add(new TableCell());
            //GridViewSamp.Rows[0].Cells[0].ColumnSpan = columncount;
            //GridViewSamp.Rows[0].Cells[0].Text = "No Records Found";
            //lblCount.Text = "Total No. of Cases : 0";
            //}



            //return Json(dtclstr);
        }



        /// <summary>
        /// FACTORY DISPATCH COMPLETE
        /// </summary>
        /// <returns></returns>
        public ActionResult FactoryDispatchCompleteIndex()
        {
            ViewBag.GPIL_ORGN_MASTER = (from s in _data.GPIL_ORGN_MASTER where s.ORGN_TYPE == "FACTORY" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult GetTransporterCode()
        {
            string query = "";
            query = "select TransporterCode, (TransporterCode + ' - ' +  TransporterName ) as TransporterName from mTransporters(nolock)";
            dt = CMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTruckNumber(string lRDate, string orgnCode, string transportCode)
        {
            DataTable DT1 = new DataTable();
            string query = "";
            query = "SELECT DISTINCT SENDER_TRUCK_NO FROM GPIL_SHIPMENT_HDR(nolock) WHERE SENDER_DATE BETWEEN CONVERT(DATETIME,'" + lRDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + lRDate + " 23:59:59',105) AND STATUS='INT' AND RECEIVER_ORGN_CODE='" + orgnCode + "' AND TRANSPORT_NAME='" + transportCode + "'";
            DT1 = CMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(DT1);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        //lrDate: LRDate, orgnCode: OrgnCode, transporterCode: TransporterCode, truckNumber: TruckNumber 


        [HttpGet]
        public ActionResult GetFactoryDispatchGrid(string lrDate, string orgnCode, string transporterCode, string truckNumber)
        {

            if (lrDate.Trim().Length == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the LR Date');", true);
                //return;
            }

            if (orgnCode.Length == 0 || orgnCode.Length == -1)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Location code for Factory Dispatch');", true);
                //return;
            }

            if (transporterCode.Length == 0 || transporterCode.Length == -1)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Transporter Code for Factory Dispatch');", true);
                //return;
            }

            if (truckNumber.Length == 0 || truckNumber.Length == -1)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select the Truck Number for Factory Dispatch');", true);
                //return;
            }




            string query = "";
            query = "SELECT D.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENDER_DATE,H.RECEIVED_DATE,COUNT(D.GPIL_BALE_NUMBER) AS CASES,SUM(MARKED_WT) AS QUANTITY FROM GPIL_SHIPMENT_DTLS(nolock) D, GPIL_SHIPMENT_HDR(nolock) H WHERE H.SHIPMENT_NO=D.SHIPMENT_NO AND H.RECEIVER_ORGN_CODE='" + orgnCode + "' AND H.STATUS='INT' AND H.IS_WMS_SHIPMENT='F' AND  H.SENDER_DATE BETWEEN CONVERT(DATETIME,'" + lrDate + " 00:00:00',105) AND CONVERT(DATETIME,'" + lrDate + " 23:59:59',105) AND H.SENDER_TRUCK_NO='" + truckNumber + "' AND H.TRANSPORT_NAME='" + transporterCode + "' GROUP BY D.SHIPMENT_NO,H.SENDER_ORGN_CODE,H.SENDER_TRUCK_NO,H.SENDER_DATE,H.RECEIVED_DATE ORDER BY D.SHIPMENT_NO";
            dt = CMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// WMS DATA STATUS
        /// </summary>
        /// <returns></returns>
        public ActionResult WmsDataStatusIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetLPNumber(string fromDate, string toDate)
        {
            DataTable dtt = new DataTable();
            DateTime dtime = Convert.ToDateTime(fromDate);
            fromDate = dtime.ToString("dd-MM-yyyy");
            DateTime dtim = Convert.ToDateTime(toDate);
            toDate = dtim.ToString("dd-MM-yyyy");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select Distinct BC_WALD_LP5_NO as LP5No,BC_WALD_LP5_NO as LP5No from BC_WMS_LEAF_ASN_D ");
            if (fromDate != "" && toDate != "")
            {
                sb.AppendLine(" Where BC_WALD_INSERT_DT between ");
                sb.AppendLine(" convert(datetime, '" + fromDate + " 00:00:00',105) AND ");
                sb.AppendLine(" convert(datetime, '" + toDate + " 23:59:59',105) ");

                //sb.AppendLine("Where convert(varchar(12), BC_WALD_INSERT_DT,101) between ");
                //sb.AppendLine(" convert(varchar(12), '" + txt_Report_Date.Text + "',101) AND ");
                //sb.AppendLine(" convert(varchar(12), '" + txtToDate.Text + "',101) ");
            }
            sb.AppendLine("Order By BC_WALD_LP5_NO");

            string strsql = sb.ToString();
            dtt = CMgt.GetQueryResult(strsql);
            string json = JsonConvert.SerializeObject(dtt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetWMSDataStatus(string fromDate, string toDate, string lpNumber)
        {
            try
            {
                string strSqlQuery = "Select BC_WALD_LP5_NO as LP5No, BC_WALD_BALE_NO as CaseBarCode,(Case when BC_WALD_READ_FLAG='Y' then 'Uploaded' else 'Pending' end) as Status from BC_WMS_LEAF_ASN_D ";

                if (lpNumber.Length != 0 && lpNumber.Length != -1)
                {
                    strSqlQuery = strSqlQuery + " Where BC_WALD_LP5_NO='" + lpNumber + "' ";
                }

                if (toDate != "" && toDate != "")
                {
                    if (lpNumber.Length != 0 && lpNumber.Length != -1)
                    {
                        strSqlQuery = strSqlQuery + " And ";
                    }
                    else
                    {
                        strSqlQuery = strSqlQuery + " Where ";
                    }
                    //strSqlQuery = strSqlQuery + " convert(varchar(12), BC_WALD_INSERT_DT,101) between ";
                    //strSqlQuery = strSqlQuery + " convert(varchar(12), '" + txt_Report_Date.Text + "',101) AND ";
                    //strSqlQuery = strSqlQuery + " convert(varchar(12), '" + txtToDate.Text + "',101) ";

                    strSqlQuery = strSqlQuery + " BC_WALD_INSERT_DT between ";
                    strSqlQuery = strSqlQuery + " convert(varchar, '" + fromDate + " 00:00:00',105) AND ";
                    strSqlQuery = strSqlQuery + " convert(varchar, '" + toDate + " 23:59:59',105) ";


                }
                strSqlQuery = strSqlQuery + " Order By BC_WALD_LP5_NO,BC_WALD_BALE_NO ";
                dt = CMgt.GetQueryResult(strSqlQuery);
                string json = JsonConvert.SerializeObject(dt);
                return Json(json, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

            }
            return Json(dt);
        }



    }







    public class clsSettings
    {
        #region global variables

        public static string appPrinter = ConfigurationManager.AppSettings["printerName"].ToString();
        public static string sOngoleLoc = ConfigurationManager.AppSettings["LocCode"].ToString();
        public static string sFumRunPeriod = ConfigurationManager.AppSettings["FumRunPeriod"].ToString();
        public static string sFumExpiryPeriod = ConfigurationManager.AppSettings["FumExpiryPeriod"].ToString();
        public static bool bEscapeLoading = Convert.ToBoolean(ConfigurationManager.AppSettings["EscapeLoading"].ToString());


        public static string strLocCode = "";
        public static string strLocName;
        public static string strLocType;
        public static string strUserName;
        public static string strName;
        public static string strUserType;
        public static bool bAccessMstr;
        public static bool bAccessRprt;
        public static bool bAccessTran;
        public static bool bAccessSync;
        public static DateTime dtSysDate;
        //public static StatusStrip mdiStatusStrip;
        public static DataTable dtImportErrors;
        public static DataTable dtSaveErrors;
        public static DataSet dsFumAlertData;
        public static DataSet dsCaseSuggestion;
        public static string strFumAlert = "";
        //public static Form mdiFrm;
        public static string sStackID = "";
        public static string sPrintID = "";
        public static string sPrintLocCode = "";
        public static string sPrintCropYr = "";
        public static string sPrintLotNo = "";
        public static string sPrintGradeCode = "";
        public static string sPrintFromCaseNo = "";
        public static string sPrintToCaseNo = "";
        public static string sPrintCasesCount = "";
        public static string sPrintExistingStock = "";
        public static bool bRePrintBC = false;
        public static string sDateOfPacking = "";

        //clsDBActions oDBAction = new clsDBActions();

        /*
         * VERSION DETAILS
         * 1.00 - Initial Version
         */
        //public static string iVersion = "1.0.2.0";

        #endregion

        #region common general methods

        //public void FlashToolTip(Form oForm, ToolTip oToolTip, System.Windows.Forms.Timer oTimer, StatusStrip oStatusStrip, ToolStripStatusLabel oToolStripStatusLabel)
        //{
        //    clsMasterActions oMasterAction = new clsMasterActions();    // master action class object
        //    StringBuilder sbQuery = new StringBuilder();
        //    try
        //    {
        //        sbQuery.Append("SELECT StackID, CaseBarCode, CaseName, GradeName FROM tCaseDetails INNER JOIN mCases ON mCases.CaseCode = SUBSTRING(CaseBarCode, 27, 1) INNER JOIN mGrades ON mGrades.GradeCode = SUBSTRING(CaseBarCode, 3, 4) WHERE LocCode='" + strLocCode + "' AND Status=1 ORDER BY StackID");
        //        sbQuery.Append("; ");
        //        //sbQuery.Append("SELECT StackID, CaseBarCode, CaseName, GradeName, FumExpiredOn FROM tCaseDetails INNER JOIN mCases ON mCases.CaseCode = SUBSTRING(CaseBarCode, 27, 1) INNER JOIN mGrades ON mGrades.GradeCode = SUBSTRING(CaseBarCode, 3, 4) WHERE LocCode='" + strLocCode + "' AND Status=2 AND CONVERT(VARCHAR, FumExpiredOn, 101)<=CONVERT(VARCHAR, GETDATE()+10, 101) ORDER BY StackID");
        //        sbQuery.Append("SELECT StackID, CaseBarCode, CaseName, GradeName, FumExpiredOn FROM tCaseDetails INNER JOIN mCases ON mCases.CaseCode = SUBSTRING(CaseBarCode, 27, 1) INNER JOIN mGrades ON mGrades.GradeCode = SUBSTRING(CaseBarCode, 3, 4) WHERE LocCode='" + strLocCode + "' AND Status=2 AND DATEDIFF(day, CONVERT(VARCHAR, GETDATE() + 10, 101), CONVERT(VARCHAR, FumExpiredOn, 101)) <= 10 ORDER BY StackID");

        //        dsFumAlertData = oDBAction.GetDataSet(sbQuery.ToString());
        //        if (dsFumAlertData.Tables[0].Rows.Count > 0 && dsFumAlertData.Tables[1].Rows.Count > 0)
        //        {
        //            oTimer.Enabled = true;
        //            strFumAlert = "Pending";
        //            oToolTip.Show("Fumigation is Pending!", oStatusStrip, oToolStripStatusLabel.Bounds.Left, oToolStripStatusLabel.Bounds.Top, 10000);
        //            oToolStripStatusLabel.Image = global::GPIProject.Properties.Resources.th_bullets_balls_red_004;
        //        }
        //        else if (dsFumAlertData.Tables[0].Rows.Count > 0)
        //        {
        //            strFumAlert = "Pending";
        //            oToolTip.Show("Fumigation is Pending!", oStatusStrip, oToolStripStatusLabel.Bounds.Left, oToolStripStatusLabel.Bounds.Top, 10000);
        //            oToolStripStatusLabel.Image = global::GPIProject.Properties.Resources.th_bullets_balls_red_004;
        //        }
        //        else if (dsFumAlertData.Tables[1].Rows.Count > 0)
        //        {
        //            strFumAlert = "Expiry";
        //            oToolTip.Show("Fumigation expired/will" + Environment.NewLine + "expire within 10 days!", oStatusStrip, oToolStripStatusLabel.Bounds.Left, oToolStripStatusLabel.Bounds.Top, 10000);
        //            oToolStripStatusLabel.Image = global::GPIProject.Properties.Resources.th_bullets_balls_red_004;
        //        }
        //        else
        //        {
        //            strFumAlert = "";
        //            oToolStripStatusLabel.Image = global::GPIProject.Properties.Resources.green_bullet1;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        cnnGPI.Close();
        //        MessageBox.Show(ex.Message);
        //        if (ex.Number != -1)
        //            oMasterAction.ErrorLog(oForm, "FlashToolTip", sbQuery.ToString(), ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        cnnGPI.Close();
        //        MessageBox.Show(ex.Message);
        //        oMasterAction.ErrorLog(oForm, "FlashToolTip", "", ex.Message);
        //    }
        //}


        /*
         * Encode the passed string by adding '5' in the ascii value of each character
         */
        public string EncodeString(string strBaseText)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(strBaseText);
            StringBuilder result = new StringBuilder();
            foreach (byte b in asciiBytes)
            {
                result.Append(Convert.ToChar(b + 5));
            }
            return result.ToString();
        }

        /*
         * Only allow to key alphabet on selected control
         * c - key character passed value from control
         * space - to allow space or not with alpha
         */
        public bool Alpha(char c, bool space)
        {
            if (space == true && c.ToString() == " ")
            {
                return true;
            }
            else if (char.IsLetter(c) == true)
            {
                return true;
            }
            return false;
        }

        /*
         * Only allow to key Symbol on selected control
         * c - key character passed value from control
         */
        public bool Symbol(char c)
        {
            if (char.IsDigit(c) == false && char.IsLetter(c) == false)
            {
                return true;
            }
            return false;
        }

        /*
         * Only allow to key numbers on selected control
         * c - key character passed value from control
         * decimalPoint - to allow decimal or not with number
         */
        public bool Num(char c, bool decimalPoint)
        {
            if (decimalPoint == true && c.ToString() == ".")
            {
                return true;
            }
            else if (char.IsDigit(c) == true)
            {
                return true;
            }
            return false;
        }

        /*
         * Only allow to key alphabet on selected control
         * c - key character passed value from control
         * space - to allow space or not with alpha
         */
        public bool AlphaNumeric(char c, bool space)
        {
            if (space == true && c.ToString() == " ")
            {
                return true;
            }
            else if (char.IsLetterOrDigit(c) == true)
            {
                return true;
            }
            return false;
        }

        public void SelectAll(GridView dgv, bool bSelect)
        {
            string strChk = "true";
            if (bSelect == false) strChk = "false";
            for (int iCnt = 0; iCnt < dgv.Rows.Count; iCnt++)
            {
                dgv.Rows[iCnt].Cells[0].Text = strChk;
            }
        }

        public void onCheckBoxClick(GridView dgv, int colIdx, int rowIdx, CheckBox chk)
        {
            if (colIdx == 0 && rowIdx != -1)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string re_value = dgv.Rows[i].Cells[0].Text.ToString();
                    if (re_value.ToLower() == "false")
                    {
                        chk.Checked = false;
                        return;
                    }
                    else if (re_value.ToLower() == "true" && i == dgv.Rows.Count - 1)
                    {
                        chk.Checked = true;
                    }
                }
            }

        }

        public static bool IsValidEmail(string strIn)
        {
            // code source link with pattern description - http://msdn.microsoft.com/en-us/library/01escwtf.aspx
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        #endregion

    }


    public class clsDBActions
    {
        public clsDBActions()
        { }

        public void FillComboBox(DropDownList cbo, string sSqlQuery, bool isSelect)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sSqlQuery, ClsConnection.SqlCon);
                cmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                //if (isSelect)
                //{
                //    DataRow dr = ds.Tables[0].NewRow();
                //    dr[0] = "--Select--";
                //    dr[1] = "";
                //    ds.Tables[0].Rows.InsertAt(dr, 0);
                //}
                //cbo.DataTextField = ds.Tables[0].Columns[0].ToString();
                //cbo.DataValueField = ds.Tables[0].Columns[1].ToString();
                //cbo.DataSource = ds.Tables[0];
                //cmd.Dispose();
                //sda.Dispose();
                //ds.Dispose();

                cbo.DataSource = ds.Tables[0];
                cbo.DataTextField = ds.Tables[0].Columns[0].ToString();
                cbo.DataValueField = ds.Tables[0].Columns[1].ToString();
                cbo.DataBind();
                cbo.Items.Insert(0, "<--Select-->");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void FillStackComboDetail(DropDownList cbo, string sLocCode, bool isSelect)
        {
            string sSQL = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Stack", typeof(String));
                dt.Columns.Add("Detail", typeof(String));
                DataSet dsCombo = new DataSet();
                dsCombo.Tables.Add(dt);
                sSQL = "SELECT StackID, Capacity FROM mStacks(NOLOCK) WHERE LocCode='" + sLocCode + "'";
                DataSet dsStack = GetDataSet(sSQL);
                if (dsStack.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsStack.Tables[0].Rows.Count; i++)
                    {
                        sSQL = "SELECT StackID, Status, COUNT(StackID) AS Cnt FROM tCaseDetails(NOLOCK) WHERE LocCode='" + sLocCode + "' AND StackID='" + dsStack.Tables[0].Rows[i]["StackID"].ToString() + "' AND Status BETWEEN 1 AND 2 GROUP BY StackID, Status";
                        DataSet dsCase = GetDataSet(sSQL);
                        string sStackID = dsStack.Tables[0].Rows[i]["StackID"].ToString();
                        string sSpaceOccupied = "0";
                        string sStatus = "9";
                        if (dsCase.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsCase.Tables[0].Rows.Count; j++)
                            {
                                sSpaceOccupied = Convert.ToString(Convert.ToInt32(sSpaceOccupied) + Convert.ToInt32(dsCase.Tables[0].Rows[j]["Cnt"].ToString()));
                                if (sStatus == "9")
                                { sStatus = dsCase.Tables[0].Rows[j]["Status"].ToString(); }
                                else
                                {
                                    if (Convert.ToInt32(dsCase.Tables[0].Rows[j]["Status"].ToString()) < Convert.ToInt32(sStatus))
                                    { sStatus = dsCase.Tables[0].Rows[j]["Status"].ToString(); }
                                }
                            }
                            DataRow dr = dt.NewRow();
                            dr[0] = sStackID;
                            dr[1] = Convert.ToString(dsStack.Tables[0].Rows[i]["Capacity"].ToString() + "-" + sSpaceOccupied + "-" + sStatus);
                            dsCombo.Tables[0].Rows.InsertAt(dr, dsCombo.Tables[0].Rows.Count);
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = sStackID;
                            dr[1] = Convert.ToString(dsStack.Tables[0].Rows[i]["Capacity"].ToString() + "-" + sSpaceOccupied + "-" + sStatus);
                            dsCombo.Tables[0].Rows.InsertAt(dr, dsCombo.Tables[0].Rows.Count);
                        }
                        dsCase.Dispose();
                    }
                }
                if (isSelect)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "";
                    dsCombo.Tables[0].Rows.InsertAt(dr, 0);
                }
                dsStack.Dispose();
                cbo.DataTextField = dsCombo.Tables[0].Columns[0].ToString();
                cbo.DataValueField = dsCombo.Tables[0].Columns[1].ToString();
                cbo.DataSource = dsCombo.Tables[0];
                dsCombo.Dispose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetLocations(bool bPSW)
        {
            string sQuery = "";
            try
            {

                if (clsSettings.strLocCode == clsSettings.sOngoleLoc)
                {
                    if (bPSW)
                    {
                        sQuery = "Select LocCode, LocName from mLocations(NOLOCK) Where Active='True' And LocType='PSW' Order By LocCode";
                    }
                    else
                    {
                        sQuery = "Select LocCode, LocName from mLocations(NOLOCK) Where Active='True' And LocType<>'FACTORY' Order By LocCode";
                    }
                }
                else
                {
                    sQuery = "Select LocCode, LocName from mLocations(NOLOCK) Where LocCode='" + clsSettings.strLocCode + "'";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sQuery;

        }

        /*
         * This procedure execute query result scalar object
         */
        public object ExecuteScalar(string sSqlQuery)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, ClsConnection.SqlCon);
            cmd.CommandTimeout = 0;
            if (ClsConnection.SqlCon.State == ConnectionState.Closed)
            {
                ClsConnection.SqlCon.Open();
            }
            object obj = cmd.ExecuteScalar();
            ClsConnection.SqlCon.Close();
            return obj;
        }

        public object ExecuteScalar(string sSqlQuery, SqlTransaction trxn)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, ClsConnection.SqlCon, trxn);
            cmd.CommandTimeout = 0;
            object obj = cmd.ExecuteScalar();
            //ClsConnection.SqlCon.Close();
            return obj;
        }



        /*
         * Fetch data from data set
         * strSql - SQL query
         */
        public DataSet GetDataSet(string strSql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                if (ClsConnection.SqlCon.State == ConnectionState.Closed)
                    ClsConnection.SqlCon.Open();

                ds = new DataSet();
                da = new SqlDataAdapter(strSql, ClsConnection.SqlCon);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(ds);
                da.Dispose();

                if (ClsConnection.SqlCon.State == ConnectionState.Open)
                    ClsConnection.SqlCon.Close();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return ds;
        }

        public DataSet GetDataSet(string strSql, SqlTransaction trxn)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {

                ds = new DataSet();
                da = new SqlDataAdapter(strSql, ClsConnection.SqlCon);
                da.SelectCommand.CommandTimeout = 0;
                da.SelectCommand.Transaction = trxn;
                da.Fill(ds);
                da.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /*
         * Fetch data from non query
         * strSql - SQL query
         */
        public int ExecuteNonQuery(string strSql)
        {
            int result = 0;
            try
            {
                if (ClsConnection.SqlCon.State == ConnectionState.Closed)
                    ClsConnection.SqlCon.Open();
                SqlCommand cmd = new SqlCommand(strSql, ClsConnection.SqlCon);
                cmd.CommandTimeout = 0;
                result = cmd.ExecuteNonQuery();
                if (ClsConnection.SqlCon.State == ConnectionState.Open)
                    ClsConnection.SqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int ExecuteNonQuery(string strSql, SqlConnection con)
        {
            int result = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandTimeout = 0;
                result = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int ExecuteNonQuery(string sSqlQuery, SqlConnection cnn, SqlTransaction trxn)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand(sSqlQuery, cnn, trxn);
            cmd.CommandTimeout = 0;
            result = cmd.ExecuteNonQuery();
            return result;
        }

        public DataTable ReturnDataTable(string sSqlQuery)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, ClsConnection.SqlCon);
            cmd.CommandTimeout = 0;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public DataTable ReturnDataTable(string sSqlQuery, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(sSqlQuery, cnn);
            cmd.CommandTimeout = 0;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public DataTable ReturnDataTable(string sSqlQuery, SqlConnection cnn, SqlTransaction trxn)
        {
            SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(sSqlQuery, cnn, trxn));
            sda.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

    }




}