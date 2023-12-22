using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.Verificationn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.Controllers
{
    public class SupplierVerificationController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;


        public SupplierVerificationController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: SupplierVerification
        public ActionResult SupplierVerificationIndex()
        {

            ViewBag.GPIL_SUPP_PURCHS_HDR = (from s in _context.GPIL_SUPP_PURCHS_HDR where s.STATUS == "P" select new { s.LP4_NUMBER }).Distinct();
            return View();

        }



        [HttpGet]
        // GET: SupplierVerification/SupplierCode
        public ActionResult SupplierCode(string supplierName)
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {

                strsql = "SELECT H.SUPP_CODE, (H.SUPP_CODE + ' - ' + H.SITE_NAME) AS SUPPLIER FROM GPIL_SUPP_PURCHS_HDR H WITH(NOLOCK) WHERE  H.LP4_NUMBER='" + supplierName + "' AND H.STATUS='P' GROUP BY H.SUPP_CODE,H.SITE_NAME ORDER BY H.SUPP_CODE";
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
        // GET: SupplierVerification/View Button
        public JsonResult SupplierVerification(string poNumber, string supplierCode)
        {
            Session["frmpurcdoc"] = poNumber;
            string strsql;
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                if (poNumber.Length == 0)
                {
                    strsql = "SELECT D.HEADER_ID,H.LP4_NUMBER,H.SUPP_CODE,H.SITE_NAME,H.RECEV_ORGN_CODE,H.CROP,H.VARIETY,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(NET_WEIGHT) AS QUANTITY FROM GPIL_SUPP_PURCHS_DTLS D, GPIL_SUPP_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND H.LP4_NUMBER='00000' AND H.SUPP_CODE='0000' AND H.STATUS='P' GROUP BY D.HEADER_ID,H.LP4_NUMBER,H.SUPP_CODE,H.SITE_NAME,H.RECEV_ORGN_CODE,H.CROP,H.VARIETY ORDER BY H.SUPP_CODE,H.SITE_NAME,H.RECEV_ORGN_CODE,H.CROP,H.VARIETY";
                }
                else
                {
                    strsql = "SELECT D.HEADER_ID,H.LP4_NUMBER,H.SUPP_CODE,H.SITE_NAME,H.RECEV_ORGN_CODE,H.CROP,H.VARIETY,COUNT(D.GPIL_BALE_NUMBER) AS BALES,SUM(NET_WEIGHT) AS QUANTITY FROM GPIL_SUPP_PURCHS_DTLS D, GPIL_SUPP_PURCHS_HDR H WHERE H.HEADER_ID=D.HEADER_ID AND H.LP4_NUMBER='" + poNumber + "' AND H.SUPP_CODE='" + supplierCode + "' AND H.STATUS='P' GROUP BY D.HEADER_ID,H.LP4_NUMBER,H.SUPP_CODE,H.SITE_NAME,H.RECEV_ORGN_CODE,H.CROP,H.VARIETY ORDER BY H.SUPP_CODE,H.SITE_NAME,H.RECEV_ORGN_CODE,H.CROP,H.VARIETY";
                }
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;

                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


                //Session["frmPrintid"] = poNumber;


            }
            catch (Exception ex)
            { }
            return Json(ds);
        }



        // Verify Button Event start
        public JsonResult SupplierPurchaseBW(string headerIDList, string lp4NumberList, string supplierCodeList)
        {
            Session["HEADER_ID"] = headerIDList;
            Session["lp4Number"] = lp4NumberList;
            Session["supplierCode"] = supplierCodeList;


            return Json(new { result = "Redirect", url = Url.Action("SupplierVerifyIndex", "SupplierVerification") });
        }

        public ActionResult SupplierVerifyIndex()
        {
            ViewBag.Title = "SupplierVerifyIndex";

            ViewBag.HEADER_ID = (string)Session["HEADER_ID"];
            //Session["HEADER_ID"] = HEADER_ID;
            //ViewBag.lp4Number = (string)Session["lp4Number"];
            //ViewBag.supplierCode = (string)Session["supplierCode"];

            DataSet ds = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                ds = ldMgt.GetSupplierPurchaseVerify(ViewBag.HEADER_ID);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return View(jsonResult);
            }
            catch (Exception ex)
            { }
            return View("SupplierVerifyIndex");
        }
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;
        //UPDATE EVENT METHOD
        [HttpPost]
        public ActionResult UpdateSupplierPurchase(Supplier_PURCHS_DTLS gPIL_TAP_FARM_PURCHS_DTLS)
        {

            //string orgcd = ((string)Session["orgnCode"] == null) ? "" : (string)Session["orgnCode"];
            try
            {
                VerificationManagement ldMgt = new VerificationManagement();
                string HEADER_ID = gPIL_TAP_FARM_PURCHS_DTLS.HEADER_ID;
                string GPIL_BALE_NUMBER = gPIL_TAP_FARM_PURCHS_DTLS.GPIL_BALE_NUMBER;
                string GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.GRADE;
                string NET_WEIGHT = (gPIL_TAP_FARM_PURCHS_DTLS.NET_WEIGHT == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.NET_WEIGHT;

                string tablename = string.Empty, temptablename = string.Empty;
                string sttrsqrtstk;
                double dFreight = 0;
                string sAttribute4 = string.Empty;
                //sAttribute4 = RATE.ToString();
                string sttrsqrt = "update GPIL_SUPP_PURCHS_DTLS set GRADE='" + GRADE + "',NET_WEIGHT='" + NET_WEIGHT + "' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";

                bool b = ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                if (b)
                {

                    sttrsqrt = "select PROCESS_NAME,PROCESS_REF_ID from GPIL_PROCESS_ORDER_CAPTURE WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = ldMgt.GetQueryResult(sttrsqrt);

                    for (int s = 0; s < ds1.Rows.Count; s++)
                    {
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "DISPATCH")
                        {
                            tablename = "GPIL_SHIPMENT_DTLS";
                            temptablename = "GPIL_SHIPMENT_DTLS_TEMP";
                        }
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "CLASSIFICATION" || ds1.Rows[s]["PROCESS_NAME"].ToString() == "GRADETRANSFER")
                        {
                            tablename = "GPIL_CLASSIFICATION_DTLS";
                            temptablename = "GPIL_CLASSIFICATION_DTLS_TEMP";
                        }
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "CROPTRANSFER")
                        {
                            tablename = "GPIL_CROP_TRANS_DTLS";
                            temptablename = "GPIL_CROP_TRANS_DTLS_TEMP";
                        }
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "GRADING")
                        {
                            tablename = "GPIL_GRADING_DTLS";
                            temptablename = "GPIL_GRADING_DTLS_TEMP";
                        }
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() == "THRESHING")
                        {
                            tablename = "GPIL_THRESH_RECON_DTLS_1";
                            temptablename = "GPIL_THRESH_RECON_DTLS_1_TEMP";
                        }
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() != "TAPPURCHASE" && ds1.Rows[s]["PROCESS_NAME"].ToString() != "FARMERPURCHASE" && ds1.Rows[s]["PROCESS_NAME"].ToString() != "SUPPLIERPURCHASE")
                        {
                            sttrsqrt = "update " + tablename + " set MARKED_WT='" + Convert.ToDouble(NET_WEIGHT) + "' WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                            sttrsqrt = "update " + temptablename + " set MARKED_WT='" + Convert.ToDouble(NET_WEIGHT) + "' WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER + "'";
                            b = ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                        }

                    }
                }
                else
                {
                    data = "Error: Error While Inserting Please Check Data";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
               



                return new EmptyResult();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        [HttpPost]
        public ActionResult DeleteSupplierPurchase(string GPIL_BALE_NUMBER)
        {
            string sttrsqrt;
            VerificationManagement ldMgt = new VerificationManagement();
            sttrsqrt = "delete FROM GPIL_SUPP_PURCHS_DTLS where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            sttrsqrt = "delete FROM gpil_stock where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);

            return new EmptyResult();
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

        // Verified button Event
        MasterManagement MstrMgt = new MasterManagement();
        [HttpPost]
        public JsonResult SupplierPurchaseVerified(ListSupplierVerification LSV)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;



            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LSV.SupplierVerifications);
                //string strQry = "";

                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {

                    //SupplierPurchaseVerification.SNO = row.find("td:eq(0)")[0].innerText;
                    //SupplierPurchaseVerification.HEADER_ID = row.find("td:eq(1)")[0].innerText;
                    //SupplierPurchaseVerification.LP4_NUMBER = row.find("td:eq(2)")[0].innerText;
                    //SupplierPurchaseVerification.SUPP_CODE = row.find("td:eq(3)")[0].innerText;


                    string HEADER_ID = dtGridLst.Rows[s]["HEADER_ID"].ToString();
                    string LP4_NUMBER = dtGridLst.Rows[s]["LP4_NUMBER"].ToString();
                    string SUPP_CODE = dtGridLst.Rows[s]["SUPP_CODE"].ToString();


                    DataTable dtclstr = new DataTable();
                    string query = "";


                    query = "update GPIL_SUPP_PURCHS_HDR set STATUS='N' where  HEADER_ID='" + HEADER_ID + "' ";
                    lstQry.Add(query);

                  
                }
                bool b = false;

                //bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);
                //GPIWebApp.DataServerSync.Instance.TransactionInsert(lstQry);

                if (b)
                {
                    data = "Success: Supplier Purchase Verified SucessFully";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {

                    data = "Error: ERROR WHILE INSERTING!!!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json("Error: ERROR WHILE INSERTING!!!!" + JsonRequestBehavior.AllowGet);
                }
                //data = "Succuss";
                //json = JsonConvert.SerializeObject(data);
                //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;


            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

            //}

        }






        //[HttpPost]
        //public JsonResult SupplierPurchaseVerified(string poNumber, string supplierName)
        //{
        //    if (poNumber == "")
        //    {
        //        data = "Error: Please Select Purchase Order Number";
        //        json = JsonConvert.SerializeObject(data);
        //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }

        //    if (supplierName == "")
        //    {
        //        data = "Error: Please Select Supplier Name";
        //        json = JsonConvert.SerializeObject(data);
        //        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }




        //}


    }

    public class Supplier_PURCHS_DTLS
    {
        public int SNO { get; set; }
        public string HEADER_ID { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string GRADE { get; set; }
        public string NET_WEIGHT { get; set; }
    }
}