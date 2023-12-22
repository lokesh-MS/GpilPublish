using GPILWebApp.Models;
using GPILWebApp.ViewModel;
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
    public class InvoiceVerifyController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;


        public InvoiceVerifyController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: InvoiceVerify
        public ActionResult Index()
        {
            ViewBag.GPIL_TAP_FARM_PURCHS_HDR = (from s in _context.GPIL_TAP_FARM_PURCHS_HDR where s.STATUS == "P" && s.PURCHASE_TYPE == "TAP PURCHASE" select new { s.PURCH_DOC_NO }).Distinct();
            return View();
        }
        string lblMessage = string.Empty;
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;
        public JsonResult InvoiceVerification(string poNumber)
        {

            string servicetaxid;
            double servicetax = 0.00;
            string tbtaxid;
            double tbtax = 0.00;
            string strsql;
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                strsql = " SELECT TAX_ID,RATE AS TBTAX FROM GPIL_SERVICE_CHARGE_MASTER(NOLOCK) WHERE TAX_TYPE='OTHERS' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);

                if (ds1.Rows.Count > 0)
                {
                    tbtaxid = Convert.ToString(ds1.Rows[0][0]);
                    tbtax = Convert.ToDouble(ds1.Rows[0][1].ToString());
                }
                ds1.Clear();

                strsql = " SELECT TAX_ID,RATE AS SERVICECHARGE FROM GPIL_SERVICE_CHARGE_MASTER(NOLOCK) WHERE TAX_TYPE='GST1' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    servicetaxid = Convert.ToString(ds1.Rows[0][0]);
                    servicetax = Convert.ToDouble(ds1.Rows[0][1]);
                }
                ds1.Clear();


                strsql = "select 'FALSE' as Approved, h.PURCH_DOC_NO,H.ORGN_CODE,COUNT(CASE WHEN D.REJE_STATUS = 'OK' THEN 1 ELSE NULL END) AS TOTAL_BALES,ROUND(SUM(D.NET_WT),2) AS TOTAL_QTY,ROUND(SUM(D.NET_WT*RATE),2) AS TOTAL_PURC_VAL,";
                strsql = strsql + " ROUND(((SUM(D.NET_WT*RATE)* " + tbtax.ToString() + ")/100),2) AS SERVICE_TB_TAX_VAL,ROUND(((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100),2)SERVICE_CHARGE_VAL,";
                strsql = strsql + " SUM(PATTA_CHARGE) AS PATTA_CHARGE, ROUND((SUM(D.NET_WT*RATE))+(((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))+((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100)+(SUM(PATTA_CHARGE)),2) AS INVOICE_VAL ";//((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(serviceSBCtax) + " /100)+(((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100)* " + Convert.ToString(servicetax2per) + " /100)+(((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100)* " + Convert.ToString(servicetax1per) + " /100)
                strsql = strsql + " from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O WHERE H.HEADER_ID=D.HEADER_ID AND H.PURCHASE_TYPE='TAP PURCHASE'  AND D.STATUS='Y' AND  H.PURCH_DOC_NO='" + poNumber.ToString() + "' and H.STATUS='P' and H.ORGN_CODE=O.ORGN_CODE and O.STATUS='Y' and O.ORGN_ADDRESS5<>'Karnataka' GROUP BY D.HEADER_ID ,H.ORGN_CODE, h.PURCH_DOC_NO ";
                strsql = strsql + " order by H.ORGN_CODE";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                //var data = ds1;

                if (ds1.Rows.Count > 0)
                {

                    json = JsonConvert.SerializeObject(ds1);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Error: NO RECORD FOUND IN 1ST GRID!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }

                //string json = JsonConvert.SerializeObject(data);
                //var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;


            }
            catch (Exception ex)
            {
                data = "Error: " + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            //return Json(ds);
        }


        public JsonResult InvoiceVerifyInd(string orgnCodeList, string poNumberList)
        {
            Session["poNumber"] = poNumberList;
            Session["orgnCode"] = orgnCodeList;

            return Json(new { result = "Redirect", url = Url.Action("InvoiceVerifyIndex", "InvoiceVerify") });
        }

        public ActionResult InvoiceVerifyIndex()
        {
            ViewBag.Title = "InvoiceVerifyIndex";
            ViewBag.poNumber = (string)Session["poNumber"];
            ViewBag.orgnCode = (string)Session["orgnCode"];

            //fromDate = (string)Session["FromDate"];

            DataSet ds = new DataSet();
            VerificationManagement ldMgt = new VerificationManagement();
            try
            {
                ds = ldMgt.GetInvoiceVerify(ViewBag.poNumber, ViewBag.orgnCode);
                var data = ds;
                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return View(jsonResult);
            }
            catch (Exception ex)
            {
                data = "Error: " + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            //return View("InvoiceVerifyIndex");
        }


        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        [HttpPost]
        public ActionResult CheckBaleAvailability(string Baledata)
        {
            //HeaderID
            //var TodayHeaderId = TempData["hd"].ToString().Substring(0, 3);
            var TodayHeaderId = (string)Session["orgnCode"];

            var H_ID = Baledata.ToString().Substring(4, 3);
            System.Threading.Thread.Sleep(200);
            var usr = db.GPIL_STOCK.Where(x => x.GPIL_BALE_NUMBER == Baledata).SingleOrDefault();
            if (TodayHeaderId != H_ID)
            {
                data = "error: BaleNumber Formate is Wrong";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

            else if (usr != null)
            {

                data = "Error: BaleNumber Already Exist";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //if (Baledata == null)
                //{
                //    return Json(0);
                //}
                //PPDManagement ppdMgt = new PPDManagement();
                //DataTable dtclstr = new DataTable();
                //string query = "";
                ////query = " SELECT SNO,FARM_CODE,FARM_CATEGORY,FARM_NAME,FARM_FATHER_NAME,VILLAGE_CODE,SOIL_TYPE,FARM_ADDRESS1,FARM_ADDRESS2,FARM_ADDRESS3,FARM_ADDRESS4,FARM_ADDRESS5,FARM_ADDRESS6,";
                ////query = query + " COUNTRY,PIN_CODE,TEL_NO,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,STATUS,FLAG,LASTUPDATE,LOAN_AMOUNT,ALERT_FLAG,ALERT_MSG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5 from GPIL_FARMER_MASTER where FARM_CODE='" + Farmerdata + "' ";


                //query = "  SELECT FM.SNO,FM.FARM_CODE,FM.FARM_CATEGORY,FM.FARM_NAME,FM.FARM_FATHER_NAME,FM.VILLAGE_CODE,FM.SOIL_TYPE,";
                //query = query + "FM.FARM_ADDRESS1,FM.FARM_ADDRESS2,FM.FARM_ADDRESS3,FM.FARM_ADDRESS4,FM.FARM_ADDRESS5,FM.FARM_ADDRESS6,";
                //query = query + " FM.COUNTRY,FM.PIN_CODE,FM.TEL_NO,FM.MOBILE_NO,FM.EMAIL_ID,FM.BANK_ACCOUNT_NO,FM.BANK_NAME,FM.BRANCH_NAME,";
                //query = query + "FM.IFSC_CODE,FM.CREATED_BY,FM.CREATED_DATE,FM.LAST_UPDATED_BY,FM.LAST_UPDATED_DATE,FM.STATUS,FM.FLAG,FM.LASTUPDATE,";
                //query = query + "FM.LOAN_AMOUNT,FM.ALERT_FLAG,FM.ALERT_MSG,FM.ATTRIBUTE1,FM.ATTRIBUTE2,FM.ATTRIBUTE3 as A3,FM.ATTRIBUTE4,FM.ATTRIBUTE5,";
                //query = query + "FCH.CROP,FCH.VARIETY,FCH.ATTRIBUTE1 as A1,FCH.ATTRIBUTE2 as A2,FCH.ATTRIBUTE3,FCH.ATTRIBUTE4 as A4,FCH.ATTRIBUTE5 as A5";
                //query = query + " from GPIL_FARMER_MASTER FM Join[dbo].[GPIL_FARMER_CROP_HISTORY]  FCH on FM.FARM_CODE = FCH.FARM_CODE";
                //query = query + " where FM.FARM_CODE = '" + Baledata + "'";



                //dtclstr = ppdMgt.GetQueryResult(query);
                //string json = JsonConvert.SerializeObject(dtclstr);
                //return Json(json, JsonRequestBehavior.AllowGet);
                //return Json(0);
            }
            else
            {
                data = "Success: You Can Use This Bale Number";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            //return Json(0);
        }


        // GET: Verification/Create
        public ActionResult Create(string HEADER_ID)
        {
            ViewBag.HEADER_ID = HEADER_ID;
            //TempData["hd"] = HEADER_ID;
            Session["HEADER_ID"] = HEADER_ID;
            return View();
        }

        string purdoc;
        string orgcd;
        string strsql;
        double tbtax;
        double servicetax;
        double servicetax1per;
        double servicetax2per;
        double value;
        double tbtaxval;
        double servicetaxval;
        double servicetax1perval;
        double servicetax2perval;
        double tottax;

        string tbtaxid;
        string servicetaxid;
        string servicetax1perid;
        string servicetax2perid;

        string tablename;
        string temptablename;
        string strbalerejtype;
        string strbalerej;
        string sttrsqrt;
        public static string headerid;
        // POST: Verification/Create

        [HttpPost]
        public ActionResult Create(GPIL_TAP_FARM_PURCHS_DTLS gPIL_TAP_FARM_PURCHS_DTLS)
        {


            try
            {

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    VerificationManagement ldMgt = new VerificationManagement();

                    string HEADER_ID = gPIL_TAP_FARM_PURCHS_DTLS.HEADER_ID;
                    string GPIL_BALE_NUMBER = gPIL_TAP_FARM_PURCHS_DTLS.GPIL_BALE_NUMBER;
                    string TB_LOT_NO = (gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO; ///(gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO;  /* "";*
                    //string TBGR_NO = gPIL_TAP_FARM_PURCHS_DTLS.TBGR_NO;
                    string orgcd = ((string)Session["orgnCode"] == null) ? "" : (string)Session["orgnCode"];
                    string TBGR_NO = (gPIL_TAP_FARM_PURCHS_DTLS.TBGR_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TBGR_NO;
                    string TB_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.TB_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_GRADE;
                    double NET_WT = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.NET_WT);


                    double RATE = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.RATE);
                    string BUYER_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE;
                    string rejetype = gPIL_TAP_FARM_PURCHS_DTLS.REJE_TYPE;
                    double PATTA_CHARGE = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.PATTA_CHARGE);

                    //DataTable dtTB_LOT_NO = new DataTable();
                    //string strQry = "select distinct TB_LOT_NO from GPIL_TAP_FARM_PURCHS_DTLS where FARMER_CODE='" + FARMER_CODE.ToString() + "'";

                    //dtTB_LOT_NO = ldMgt.GetQueryResult(strQry);
                    //if (dtTB_LOT_NO.Rows.Count > 0)
                    //{
                    //    TB_LOT_NO = dtTB_LOT_NO.Rows[0][0].ToString();
                    //}
                    string CREATED_BY = Session["userID"].ToString();
                    DateTime CREATED_DATE = DateTime.Now;





                    string rejests;
                    string status;
                    if (rejetype == "NONE")
                    {
                        rejests = "OK";
                        rejetype = "NONE";
                    }
                    else
                    {
                        rejests = "RJ";

                    }
                    if (rejests.Trim() == "RJ")
                    {
                        status = "N";
                    }
                    else
                    {
                        status = "Y";
                    }

                    value = Convert.ToDouble(NET_WT) * Convert.ToDouble(RATE);
                    tbtaxval = ((value * tbtax) / 100);
                    servicetaxval = ((tbtaxval * servicetax) / 100);
                    servicetax1perval = ((servicetaxval * servicetax1per) / 100);
                    servicetax2perval = ((servicetaxval * servicetax2per) / 100);
                    tottax = tbtaxval + servicetaxval + servicetax1perval + servicetax2perval;



                    strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,TB_LOT_NO,TBGR_NO,TB_GRADE,BUYER_GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRICE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                    strsql = strsql + "Values('" + GPIL_BALE_NUMBER.ToString().Trim() + "','FW','" + TB_LOT_NO.ToString().Trim() + "','" + TBGR_NO.ToString().Trim() + "','" + TB_GRADE.ToString().Trim() + "','" + BUYER_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','LOC1','" + orgcd.Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','" + RATE.ToString().Trim() + "','G','N','" + HEADER_ID.ToString() + "','" + status + "','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','TAP','LOC1','" + orgcd + "')";
                    ldMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[TBGR_NO],[TB_GRADE],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[PATTA_CHARGE],[SERVICE_CHARGE],[SERVICE_CHARGE_AMT],[SERVICE_TAX],[SERVICE_TAX_AMT],[CREATED_BY],[CREATED_DATE] ,[SH_ED_TAX],[ED_CESS_TAX])";
                    if (rejetype == "NONE")
                    {
                        strsql = strsql + "Values('" + HEADER_ID.ToString() + "','" + GPIL_BALE_NUMBER.ToString().Trim() + "','" + TB_LOT_NO.ToString().Trim() + "','" + TBGR_NO.ToString().Trim() + "','" + TB_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + RATE.ToString().Trim() + "','" + value + "','" + BUYER_GRADE.ToString().Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','FW','" + rejests + "','" + rejetype.ToString() + "','" + status + "','N','" + PATTA_CHARGE.ToString() + "','" + tbtaxid + "','" + tbtaxval + "','" + servicetaxid + "','" + tottax + "','" + Session["userID"].ToString() + "',GETDATE(),'" + servicetax1perid + "','" + servicetax2perid + "')";
                        //strsql = strsql + "Values('" + HEADER_ID.ToString() + "','" + GPIL_BALE_NUMBER.ToString().Trim() + "','" + TB_LOT_NO.ToString().Trim() + "','" + TBGR_NO.ToString().Trim() + "','" + TB_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + RATE.ToString().Trim() + "','" + value + "','" + BUYER_GRADE.ToString().Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','FW','" + rejests + "',NULL,'" + status + "','N','" + PATTA_CHARGE.ToString() + "','" + tbtaxid + "','" + tbtaxval + "','" + servicetaxid + "','" + tottax + "','" + Session["userID"].ToString() + "',GETDATE(),'" + servicetax1perid + "','" + servicetax2perid + "')";
                    }
                    else
                    {
                        strsql = strsql + "Values('" + HEADER_ID.ToString() + "','" + GPIL_BALE_NUMBER.ToString().Trim() + "','" + TB_LOT_NO.ToString().Trim() + "','" + TBGR_NO.ToString().Trim() + "','" + TB_GRADE.ToString().Trim() + "','" + NET_WT.ToString().Trim() + "','" + RATE.ToString().Trim() + "','" + value + "','" + BUYER_GRADE.ToString().Trim() + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(0, 2) + "','" + GPIL_BALE_NUMBER.ToString().Trim().Substring(2, 2) + "','FW','" + rejests + "','" + rejetype.ToString() + "','" + status + "','N','" + PATTA_CHARGE.ToString() + "','" + tbtaxid + "','" + tbtaxval + "','" + servicetaxid + "','" + tottax + "','" + Session["userID"].ToString() + "',GETDATE(),'" + servicetax1perid + "','" + servicetax2perid + "')";
                    }
                    ldMgt.UpdateUsingExecuteNonQuery(strsql);

                    ModelState.Clear();
                    //return RedirectToAction("InvoiceVerifyIndex");
                    TempData["SuccessMessage"] = "Successfully Created!!!";
                }
                //return RedirectToAction("InvoiceVerifyIndex");

                return View();
            }
            catch
            {
                return View();
            }
        }




        [HttpPost]
        public JsonResult UpdateInvoiceVerification(GPIL_TAP_FARM_PURCHS_DTLS gPIL_TAP_FARM_PURCHS_DTLS)
        {
            try
            {
                VerificationManagement ldMgt = new VerificationManagement();
                string HEADER_ID = gPIL_TAP_FARM_PURCHS_DTLS.HEADER_ID;
                string GPIL_BALE_NUMBER = gPIL_TAP_FARM_PURCHS_DTLS.GPIL_BALE_NUMBER;
                string TB_LOT_NO = (gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_LOT_NO;
                string orgcd = ((string)Session["orgnCode"] == null) ? "" : (string)Session["orgnCode"];
                string TBGR_NO = (gPIL_TAP_FARM_PURCHS_DTLS.TBGR_NO == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TBGR_NO;
                string TB_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.TB_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.TB_GRADE;
                double NET_WT = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.NET_WT);
                double RATE = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.RATE);
                string BUYER_GRADE = (gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE == null) ? "" : gPIL_TAP_FARM_PURCHS_DTLS.BUYER_GRADE;
                string rejetype = gPIL_TAP_FARM_PURCHS_DTLS.REJE_TYPE;
                double PATTA_CHARGE = Convert.ToDouble(gPIL_TAP_FARM_PURCHS_DTLS.PATTA_CHARGE);




                DataTable ds1 = new DataTable();

                string strsql = "";
                strsql = " SELECT TAX_ID,RATE AS TBTAX FROM GPIL_SERVICE_CHARGE_MASTER WHERE TAX_TYPE='OTHERS' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    tbtaxid = Convert.ToString(ds1.Rows[0][0]);
                    tbtax = Convert.ToDouble(ds1.Rows[0][1].ToString());
                }
                ds1.Clear();

                strsql = " SELECT TAX_ID,RATE AS SERVICECHARGE FROM GPIL_SERVICE_CHARGE_MASTER WHERE TAX_TYPE='SERVICE' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    servicetaxid = Convert.ToString(ds1.Rows[0][0]);
                    servicetax = Convert.ToDouble(ds1.Rows[0][1].ToString());
                }
                ds1.Clear();

                strsql = "SELECT TAX_ID,RATE AS SERVICESHEDCHEES FROM GPIL_SERVICE_CHARGE_MASTER WHERE TAX_TYPE='SERVICE SH EDUCATION CESS' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    servicetax1perid = Convert.ToString(ds1.Rows[0][0]);
                    servicetax1per = Convert.ToDouble(ds1.Rows[0][1].ToString());
                }
                ds1.Clear();

                strsql = "SELECT TAX_ID,RATE AS SERVICETAXEDCHEES FROM GPIL_SERVICE_CHARGE_MASTER WHERE TAX_TYPE='SERVICE TAX-EDUCATION CESS' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    servicetax2perid = Convert.ToString(ds1.Rows[0][0]);
                    servicetax2per = Convert.ToDouble(ds1.Rows[0][1].ToString());
                }
                ds1.Clear();

                value = Convert.ToDouble(NET_WT.ToString()) * Convert.ToDouble(RATE.ToString());
                tbtaxval = ((value * tbtax) / 100);
                servicetaxval = ((tbtaxval * servicetax) / 100);
                servicetax1perval = ((servicetaxval * servicetax1per) / 100);
                servicetax2perval = ((servicetaxval * servicetax2per) / 100);
                tottax = tbtaxval + servicetaxval + servicetax1perval + servicetax2perval;
                string sttrsqrtstk;
                string sttrsqrt;

                if (rejetype != null)
                {
                    if (rejetype != "NONE")
                    {
                        strbalerejtype = rejetype.ToString();
                        strbalerej = "RJ";
                        //sttrsqrt = "update GPIL_TAP_FARM_PURCHS_DTLS set BUYER_GRADE='" + byrgrade.Text + "',PATTA_CHARGE='" + pattacharge.Text + "',STATUS='N',REJE_TYPE='" + strbalerejtype + "',REJE_STATUS='" + strbalerej + "',VALUE='" + value + "',SERVICE_TAX_AMT='" + tottax + "',SERVICE_CHARGE_AMT='" + tbtaxval + "',NET_WT='" + netwt.Text + "',RATE='" + rate.Text + "' where GPIL_BALE_NUMBER='" + GPILBALENO.Text.Trim() + "'";
                        //sttrsqrtstk = "update GPIL_STOCK set BUYER_GRADE='" + byrgrade.Text + "',MARKED_WT='" + netwt.Text + "',CURR_WT='" + netwt.Text + "',PRICE='" + rate.Text + "',STATUS='N' where GPIL_BALE_NUMBER='" + GPILBALENO.Text.Trim() + "'";

                        sttrsqrt = "update GPIL_TAP_FARM_PURCHS_DTLS set PATTA_CHARGE='" + PATTA_CHARGE.ToString() + "',STATUS='N',REJE_TYPE='" + strbalerejtype + "',REJE_STATUS='" + strbalerej + "',VALUE='" + value + "',SERVICE_TAX_AMT='" + tottax + "',SERVICE_CHARGE_AMT='" + tbtaxval + "',NET_WT='" + NET_WT.ToString() + "',RATE='" + RATE.ToString() + "' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString().Trim() + "'";
                        sttrsqrtstk = "update GPIL_STOCK set MARKED_WT='" + NET_WT.ToString() + "',CURR_WT='" + NET_WT.ToString() + "',PRICE='" + RATE.ToString() + "',STATUS='N' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString().Trim() + "'";

                    }
                    else
                    {
                        strbalerej = "OK";
                        strbalerejtype = "NULL";
                        //sttrsqrt = "update GPIL_TAP_FARM_PURCHS_DTLS set BUYER_GRADE='" + byrgrade.Text + "',PATTA_CHARGE='" + pattacharge.Text + "',STATUS='Y',REJE_TYPE=NULL,REJE_STATUS='OK',VALUE='" + value + "',SERVICE_TAX_AMT='" + tottax + "',SERVICE_CHARGE_AMT='" + tbtaxval + "',NET_WT='" + netwt.Text + "',RATE='" + rate.Text + "' where GPIL_BALE_NUMBER='" + GPILBALENO.Text.Trim() + "'";
                        //sttrsqrtstk = "update GPIL_STOCK set BUYER_GRADE='" + byrgrade.Text + "',MARKED_WT='" + netwt.Text + "',CURR_WT='" + netwt.Text + "',PRICE='" + rate.Text + "',STATUS='Y' where GPIL_BALE_NUMBER='" + GPILBALENO.Text.Trim() + "'";

                        sttrsqrt = "update GPIL_TAP_FARM_PURCHS_DTLS set PATTA_CHARGE='" + PATTA_CHARGE.ToString() + "',STATUS='Y',REJE_TYPE=NULL,REJE_STATUS='OK',VALUE='" + value + "',SERVICE_TAX_AMT='" + tottax + "',SERVICE_CHARGE_AMT='" + tbtaxval + "',NET_WT='" + NET_WT.ToString() + "',RATE='" + RATE.ToString() + "' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString().Trim() + "'";
                        sttrsqrtstk = "update GPIL_STOCK set MARKED_WT='" + NET_WT.ToString() + "',CURR_WT='" + NET_WT.ToString() + "',PRICE='" + RATE.ToString() + "',STATUS='Y' where GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString().Trim() + "'";

                    }
                    ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                    ldMgt.UpdateUsingExecuteNonQuery(sttrsqrtstk);


                    sttrsqrt = "select PROCESS_NAME,PROCESS_REF_ID from GPIL_PROCESS_ORDER_CAPTURE WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString() + "'";
                    ds1 = ldMgt.GetQueryResult(sttrsqrt);
                    //ds1.Clear();
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
                        if (ds1.Rows[s]["PROCESS_NAME"].ToString() != "TAPPURCHASE")
                        {
                            DataTable dt = new DataTable();
                            sttrsqrt = "update " + tablename + " set MARKED_WT='" + Convert.ToDouble(NET_WT.ToString()) + "' WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString() + "'";
                            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                            sttrsqrt = "update " + temptablename + " set MARKED_WT='" + Convert.ToDouble(NET_WT.ToString()) + "' WHERE GPIL_BALE_NUMBER='" + GPIL_BALE_NUMBER.ToString() + "'";
                            ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
                        }
                    }


                    data = "Success: The Value Inserted Successfully!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Error: The Rejected Should not be null";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                

              

                //return RedirectToAction("InvoiceVerifyIndex");
                //return View("InvoiceAuctionVerifyIndex");
                //return new EmptyResult();
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
        public ActionResult DeleteInvoiceVerification(string GPIL_BALE_NUMBER)
        {
            string sttrsqrt;
            VerificationManagement ldMgt = new VerificationManagement();
            List<string> lst = new List<string>();

            sttrsqrt = "delete FROM GPIL_TAP_FARM_PURCHS_DTLS where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            //ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            lst.Add(sttrsqrt);
            sttrsqrt = "delete FROM GPIL_PROCESS_ORDER_CAPTURE where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            //ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            lst.Add(sttrsqrt); 
            sttrsqrt = "delete FROM gpil_stock where gpil_bale_number='" + GPIL_BALE_NUMBER + "'";
            // ldMgt.UpdateUsingExecuteNonQuery(sttrsqrt);
            lst.Add(sttrsqrt);

           bool b= ldMgt.UpdateUsingExecuteNonQueryList(lst);

            if(b)
            {
                return new EmptyResult();
            }
            else
            {
                return null;
            }

            
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
        MasterManagement MstrMgt = new MasterManagement();
        [HttpPost]
        public JsonResult InvoiceVerificationVerified(ListInvoiceVerification LFR, string poNumber)
        {
            List<string> lstQry = new List<string>();
            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            VerificationManagement ldMgt = new VerificationManagement();
            DataTable dtclstr = ToDataTable(LFR.InvoiceVerifications);

            for (int i = 0; i < dtclstr.Rows.Count; i++)
            {
                string ORGN_CODE = dtclstr.Rows[i]["ORGN_CODE"].ToString();
                strsql = "update GPIL_TAP_FARM_PURCHS_HDR set STATUS='N' where PURCH_DOC_NO='" + poNumber + "' and ORGN_CODE='" + ORGN_CODE + "' ";
                lstQry.Add(strsql);
            }

            bool b = MstrMgt.UpdateUsingExecuteNonQueryList(lstQry);
            //bool b = ldMgt.UpdateUsingExecuteNonQuery(strsql);

            if (b)
            {
                data = "Success: The Value Updated Successfully!!";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                data = "Error: Error While Updating Data!!";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            //return Json(strsql);

            return Json(new { result = "Redirect", url = Url.Action("Index", "InvoiceVerify") });
        }



        public JsonResult InvoiceVerificationAuction(string poNumber)
        {
            VerificationManagement ldMgt = new VerificationManagement();
            DataTable ds1 = new DataTable();
            try
            {
                string servicetaxid;
                double servicetax = 0.00;
                string tbtaxid;
                double tbtax = 0.00;

                strsql = " SELECT TAX_ID,RATE AS TBTAX FROM GPIL_SERVICE_CHARGE_MASTER(NOLOCK) WHERE TAX_TYPE='OTHERS' AND STATUS='Y'";

                ds1 = ldMgt.GetQueryResult(strsql);

                if (ds1.Rows.Count > 0)
                {
                    tbtaxid = Convert.ToString(ds1.Rows[0][0]);
                    tbtax = Convert.ToDouble(ds1.Rows[0][1].ToString());
                }
                ds1.Clear();

                strsql = " SELECT TAX_ID,RATE AS SERVICECHARGE FROM GPIL_SERVICE_CHARGE_MASTER(NOLOCK) WHERE TAX_TYPE='GST1' AND STATUS='Y'";
                ds1 = ldMgt.GetQueryResult(strsql);
                if (ds1.Rows.Count > 0)
                {
                    servicetaxid = Convert.ToString(ds1.Rows[0][0]);
                    servicetax = Convert.ToDouble(ds1.Rows[0][1]);
                }

                strsql = "select 'FALSE' as Approved, h.PURCH_DOC_NO,H.ORGN_CODE,COUNT(*) AS TOTAL_BALES,ROUND(SUM(D.NET_WT),2) AS TOTAL_QTY,ROUND(SUM(D.NET_WT*RATE),2) AS TOTAL_PURC_VAL,";
                strsql = strsql + " ROUND(((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100),2) AS SERVICE_TB_TAX_VAL,ROUND(((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100),2)SERVICE_CHARGE_VAL,";
                strsql = strsql + " SUM(PATTA_CHARGE) AS PATTA_CHARGE, ROUND((SUM(D.NET_WT*RATE))+(((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))+((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100)+(SUM(PATTA_CHARGE)),2) AS INVOICE_VAL ";//((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(serviceSBCtax) + " /100)+(((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100)* " + Convert.ToString(servicetax2per) + " /100)+(((((SUM(D.NET_WT*RATE)* " + Convert.ToString(tbtax) + ")/100))* " + Convert.ToString(servicetax) + " /100)* " + Convert.ToString(servicetax1per) + " /100)
                strsql = strsql + " ,H.Invoice_No,CONVERT(char(10),H.Invoice_Date,105)Invoice_Date";
                strsql = strsql + " from GPIL_TAP_FARM_PURCHS_DTLS(NOLOCK) D,GPIL_TAP_FARM_PURCHS_HDR(NOLOCK) H,GPIL_ORGN_MASTER(NOLOCK) O WHERE H.HEADER_ID=D.HEADER_ID AND D.STATUS='Y' AND  H.PURCH_DOC_NO='" + poNumber + "' and H.STATUS='P' and H.ORGN_CODE=O.ORGN_CODE and O.STATUS='Y' and O.ORGN_ADDRESS5='Karnataka'  GROUP BY D.HEADER_ID,H.ORGN_CODE,H.PURCH_DOC_NO,H.Invoice_No,H.Invoice_Date ";
                strsql = strsql + " order by H.ORGN_CODE";


                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;

                string json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;



            }
            catch (Exception ex)
            {
                data = "Error: " + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

            return Json(ds1);
        }

        public JsonResult InvoiceAuctionVerify(string orgnCodeList, string poNumberList)
        {
            Session["poNumber"] = poNumberList;
            Session["orgnCode"] = orgnCodeList;

            return Json(new { result = "Redirect", url = Url.Action("InvoiceVerifyIndex", "InvoiceVerify") });
        }




        [HttpPost]
        public JsonResult UpdateInvoiceVerificationAuction(InvoiceVerificationAuction LIV, string poNumber)
        {
            try
            {
                // ViewBag.poNumber = (string)Session["poNumber"];
                VerificationManagement ldMgt = new VerificationManagement();


                string ORGN_CODE = LIV.ORGN_CODE;
                string TOTAL_BALES = LIV.TOTAL_BALES;
                string TOTAL_QTY = LIV.TOTAL_QTY;
                string TOTAL_PURC_VAL = LIV.TOTAL_PURC_VAL;
                string SERVICE_TB_TAX_VAL = LIV.SERVICE_TB_TAX_VAL;
                string SERVICE_CHARGE_VAL = LIV.SERVICE_CHARGE_VAL;
                string PATTA_CHARGE = LIV.PATTA_CHARGE;
                string INVOICE_VAL = LIV.INVOICE_VAL;
                string Invoice_No = (LIV.Invoice_No == null) ? "" : LIV.Invoice_No;
                string Invoice_Date = (LIV.Invoice_Date == null) ? "" : LIV.Invoice_Date;

                if (!string.IsNullOrEmpty(Invoice_No.Trim()) & !string.IsNullOrEmpty(Invoice_Date.Trim()))
                {
                    strsql = "update GPIL_TAP_FARM_PURCHS_HDR set INVOICE_NO='" + Invoice_No + "',INVOICE_DATE=convert(datetime,'" + Invoice_Date + "') where PURCH_DOC_NO='" + poNumber + "' and ORGN_CODE='" + ORGN_CODE + "' ";
                    //ClsConnection.closeDB();
                    //ClsConnection.connectDB();
                    //SqlCommand cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
                    //cmd.CommandTimeout = 0;
                    //cmd.ExecuteNonQuery();
                }



                bool b = MstrMgt.UpdateUsingExecuteNonQuery(strsql);
                if (b)
                {
                    data = "Success: The Value Updated Successfully!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    data = "Error: Error While Updating Data!!";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }



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

        //[HttpPost]
        //public JsonResult UpdateInvoiceVerificationAuction(ListInvoiceVerificationAuction LIV, string poNumber)
        //{

        //    string lblMessage = string.Empty;
        //    string data = String.Empty, json = String.Empty;
        //    JsonResult jsonResult;
        //    VerificationManagement ldMgt = new VerificationManagement();
        //    DataTable dtclstr = ToDataTable(LIV.InvoiceVerificationAuctions);
        //    try
        //    {
        //        //<th>ORGN_CODE</th>          <th>TOTAL_BALES</th>       <th>TOTAL_QTY</th>
        //        //        <th>TOTAL_PURC_VAL</th>           <th>SERVICE_TB_TAX_VAL</th>         <th>SERVICE_CHARGE_VAL</th>
        //        //        <th>PATTA_CHARGE</th>           <th>INVOICE_VAL</th>                <th>Invoice_No</th>
        //        //        <th>Invoice_Date</th>
        //        for (int i = 0; i < dtclstr.Rows.Count; i++)
        //        {
        //            string ORGN_CODE = dtclstr.Rows[i]["ORGN_CODE"].ToString();
        //            string TOTAL_BALES = dtclstr.Rows[i]["TOTAL_BALES"].ToString();
        //            string TOTAL_QTY = dtclstr.Rows[i]["TOTAL_QTY"].ToString();
        //            string TOTAL_PURC_VAL = dtclstr.Rows[i]["TOTAL_PURC_VAL"].ToString();
        //            string SERVICE_TB_TAX_VAL = dtclstr.Rows[i]["SERVICE_TB_TAX_VAL"].ToString();
        //            string SERVICE_CHARGE_VAL = dtclstr.Rows[i]["SERVICE_CHARGE_VAL"].ToString();
        //            string PATTA_CHARGE = dtclstr.Rows[i]["PATTA_CHARGE"].ToString();
        //            string INVOICE_VAL = dtclstr.Rows[i]["INVOICE_VAL"].ToString();
        //            string Invoice_No = dtclstr.Rows[i]["Invoice_No"].ToString();
        //            string Invoice_Date = dtclstr.Rows[i]["Invoice_Date"].ToString();

        //            if (!string.IsNullOrEmpty(Invoice_No.Trim()) & !string.IsNullOrEmpty(Invoice_Date.Trim()))
        //            {
        //                strsql = "update GPIL_TAP_FARM_PURCHS_HDR set INVOICE_NO='" + Invoice_No + "',INVOICE_DATE=convert(datetime,'" + Invoice_Date + "') where PURCH_DOC_NO='" + poNumber + "' and ORGN_CODE='" + ORGN_CODE + "' ";
        //                //ClsConnection.closeDB();
        //                //ClsConnection.connectDB();
        //                //SqlCommand cmd = new SqlCommand(strsql, ClsConnection.SqlCon);
        //                //cmd.CommandTimeout = 0;
        //                //cmd.ExecuteNonQuery();
        //            }


        //        }

        //        bool b = MstrMgt.UpdateUsingExecuteNonQuery(strsql);
        //        if (b)
        //        {
        //            data = "Success: The Value Updated Successfully!!";
        //            json = JsonConvert.SerializeObject(data);
        //            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //            jsonResult.MaxJsonLength = int.MaxValue;
        //            return jsonResult;
        //        }
        //        else
        //        {
        //            data = "Error: Error While Updating Data!!";
        //            json = JsonConvert.SerializeObject(data);
        //            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
        //            jsonResult.MaxJsonLength = int.MaxValue;
        //            return jsonResult;
        //        }
        //    }
        //    catch (Exception e)

        //    {


        //    }


        //    return null;
        //}

    }
}