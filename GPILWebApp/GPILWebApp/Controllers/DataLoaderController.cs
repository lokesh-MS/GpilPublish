using GPILWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.DataLoader;
using System.Data;
using System.ComponentModel;
using static GPILWebApp.ViewModel.DataLoader.FarmerPurchaseLoader;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Data.Entity;

namespace GPILWebApp.Controllers
{
    public class DataLoaderController : Controller
    {


        private GREEN_LEAF_TRACEABILITYEntities _context;

        string strsql;
        public DataLoaderController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: DataLoader
        //public ActionResult TapPurchase()
        //{
        //    return View();
        //}

        /// <summary>
        /// TAP PURCHASE
        /// </summary>
        /// <returns></returns>
        public ActionResult TapPurchaseLoaderIndex()
        {
            return View();
        }

        //public ActionResult TapPurchaseLoaderButtonIndex()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult TapPurchaseComplete(Generation generation)
        {
            //purchasedata(generation);
            //return null;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            List<string> lstQry = new List<string>();

            try
            {
                string TAPerror = string.Empty;
                double servicecharge = 0.00, servicetax = 0.00, servicechargeedsh = 0.00, servicechargeshcess = 0.00, servicechargeamt = 0.00, servicetaxamt = 0.00, servicechargeedshamt = 0.00, servicechargeshcessamt = 0.00;
                string servicechargeid = string.Empty, servicetaxid = string.Empty, servicechargeedshid = string.Empty, servicechargeshcessid = string.Empty, strsql = string.Empty;

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(generation.TapPurchases);
                var od = from s in generation.TapPurchases
                         group s by new { s.ORGN_CODE, s.PURCH_DOC_NO } into newgroup
                         select new
                         {
                             ORGN_CODE = newgroup.Key.ORGN_CODE,
                             PURCH_DOC_NO = newgroup.Key.PURCH_DOC_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("ORGN_CODE");
                orgdata.Columns.Add("PURCH_DOC_NO");
                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.ORGN_CODE, rowObj.PURCH_DOC_NO);
                }
                List<string> qryStringCol = new List<string>();
                
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    DataSet purdata = new DataSet();
                    string tablename = orgdata.Rows[s]["ORGN_CODE"].ToString() + orgdata.Rows[s]["PURCH_DOC_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("TB_LOT_NO");
                    purdata.Tables[s].Columns.Add("TBGR_NO");
                    purdata.Tables[s].Columns.Add("TB_GRADE");
                    purdata.Tables[s].Columns.Add("NET_WT");
                    purdata.Tables[s].Columns.Add("RATE");
                    purdata.Tables[s].Columns.Add("BUYER_GRADE");
                    purdata.Tables[s].Columns.Add("REJE_STATUS");
                    purdata.Tables[s].Columns.Add("REJE_TYPE");
                    purdata.Tables[s].Columns.Add("PATTA_CHARGE");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("BUYER_CODE");
                    purdata.Tables[s].Columns.Add("PURCH_DOC_NO");
                    purdata.Tables[s].Columns.Add("PURCHASE_DATE");
                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");

                    string orgcd = orgdata.Rows[s]["ORGN_CODE"].ToString();
                    string purdoc = orgdata.Rows[s]["PURCH_DOC_NO"].ToString();
                    DataRow[] purrows = dtclstr.Select("ORGN_CODE ='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "' AND PURCH_DOC_NO ='" + orgdata.Rows[s]["PURCH_DOC_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow row1 in purrows)
                        {
                            purdata.Tables[s].ImportRow(row1);
                        }
                    }
                    //////////////////////////validate//////////////////////////


                    try
                    {

                        for (int d = 0; d < purdata.Tables.Count; d++)
                        {
                            string tblname = purdata.Tables[d].TableName;
                            int rowcount = purdata.Tables[d].Rows.Count;
                            if (d == 13)
                            {
                                //lblMessage.Text = "ADS";
                            }
                            string headerid1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                            string orgcd1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                            string byrcd1 = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                            string purchdoc1 = purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                            string dateofpurch1 = purdata.Tables[d].Rows[0]["PURCHASE_DATE"].ToString();
                            string crop1 = purdata.Tables[d].Rows[0]["CROP"].ToString();
                            string variety1 = purdata.Tables[d].Rows[0]["VARIETY"].ToString();
                            for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                            {
                                string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                                string tblot1 = purdata.Tables[d].Rows[h]["TB_LOT_NO"].ToString();
                                string tbgrno1 = purdata.Tables[d].Rows[h]["TBGR_NO"].ToString();
                                string tbgrade1 = purdata.Tables[d].Rows[h]["TB_GRADE"].ToString();
                                string netwt1 = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                                string rate1 = purdata.Tables[d].Rows[h]["RATE"].ToString();
                                string buyergrade1 = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                                string rejests1 = purdata.Tables[d].Rows[h]["REJE_STATUS"].ToString();
                                string rejetype1 = purdata.Tables[d].Rows[h]["REJE_TYPE"].ToString();
                                string pattacharge1 = purdata.Tables[d].Rows[h]["PATTA_CHARGE"].ToString();

                                string rejests;
                                if (rejetype1 == "NONE")
                                {
                                    rejests = "OK";
                                }
                                else
                                {
                                    rejests = "RJ";

                                }


                                if (baleno1.Substring(0, 2) != crop1)
                                {
                                    data = "Error:  Bale Number  and Corp Year MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (baleno1.Substring(2, 2) != variety1)
                                {
                                    data = "Error: Bale Number  and Variety MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (baleno1.Substring(4, 3) != orgcd1)
                                {
                                    data = "Error:  Bale Number  and Orginization MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tbgrade1.Substring(0, 2) != crop1)
                                {
                                    data = "Error:  TB Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tbgrade1.Substring(2, 2) != variety1)
                                {
                                    data = "Error: TB Grade and Variety MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }
                                else if (buyergrade1.Substring(0, 2) != crop1)
                                {
                                    data = "Error: Buyer Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (buyergrade1.Substring(2, 2) != variety1)
                                {
                                    data = "Error: Buyer Grade and Variety MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }

                                else if (baleno1 == "")
                                {
                                    data = "Error: Bale number should not be empty--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;


                                }
                                else if (tblot1 == "")
                                {
                                    data = "Error: Lot Number is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tbgrno1 == "")
                                {
                                    data = "Error: TBGR number is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tbgrade1 == "")
                                {
                                    data = "Error: TB Grade is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (buyergrade1 == "")
                                {
                                    data = "Error: Buyer Grade is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }
                                else if (netwt1 == "" || Convert.ToDouble(netwt1) == 0)
                                {
                                    data = "Error: Weight is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (Convert.ToDouble(netwt1) > 150)
                                {
                                    data = "Error: Weight is more than 150 for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (rate1 == "" || Convert.ToDouble(rate1) == 0)
                                {
                                    data = "Error: Rate is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (rejests == "")
                                {
                                    data = "Error: Rejection Status is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (pattacharge1 == "")
                                {
                                    data = "Error: Patta Charge is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {

                                    strsql = "select * from GPIL_ITEM_MASTER (NOLOCK) where ITEM_CODE ='" + tbgrade1.Trim() + "'";
                                    DataTable ds1 = new DataTable();
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {
                                        strsql = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + baleno1.Trim() + "'";
                                        DataTable ds2 = new DataTable();
                                        ds2 = dlMgt.GetQueryResult(strsql);
                                        if (ds2.Rows.Count > 0)
                                        {
                                            data = "Error: Bale Already Purchased BaleNumber--" + baleno1;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                        else
                                        {
                                            strsql = "select * from GPIL_ITEM_MASTER (NOLOCK) where ITEM_CODE ='" + buyergrade1.Trim() + "'";
                                            DataTable ds3 = new DataTable();
                                            ds3 = dlMgt.GetQueryResult(strsql);
                                            if (ds3.Rows.Count == 0)
                                            {
                                                data = "Error: Buyer Grade Does not exit in master BaleNumber--" + baleno1;
                                                json = JsonConvert.SerializeObject(data);
                                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                                jsonResult.MaxJsonLength = int.MaxValue;
                                                return jsonResult;
                                            }

                                        }

                                    }
                                    else
                                    {
                                        data = "Error: TB Grade Does not exit in master BaleNumber--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                    }
                                }


                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        data = "Error:" + ex.Message;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    finally
                    {
                    }

                    /////////////////////////////////////////////////////////

                    //if (Tapvalidate(ref purdata, ref dtclstr, ref TAPerror))
                    //{


                    
                    using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            for (int d = 0; d < purdata.Tables.Count; d++)
                            {
                                string headerid = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                                string orgcd1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                                string byrcd = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                                string purchdoc = purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                                string dateofpurch = purdata.Tables[d].Rows[0]["PURCHASE_DATE"].ToString();
                                string crop = purdata.Tables[d].Rows[0]["CROP"].ToString();
                                string variety = purdata.Tables[d].Rows[0]["VARIETY"].ToString();

                                strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_HDR]([HEADER_ID],[ORGN_CODE],[PURCHASE_TYPE],[BUYER_CODE],[PURCH_DOC_NO],[DATE_OF_PURCH],[CROP],[VARIETY],[CREATED_BY],[CREATION_DATE],[STATUS])";
                                strsql = strsql + " VALUES('" + headerid + "','" + orgcd1 + "','TAP PURCHASE','" + byrcd + "','" + purchdoc + "','" + Convert.ToDateTime(dateofpurch).ToString("yyyy-MM-dd") + "','" + crop + "','" + variety + "','" + Session["userID"].ToString() + "',GETDATE(),'P')";
                                // strsql = strsql + " VALUES('" + headerid + "','" + orgcd + "','TAP PURCHASE','" + byrcd + "','" + purchdoc + "',convert(datetime,'"+Convert.ToDateTime(dateofpurch)+"',105),'" + crop + "','" + variety + "','"+Propertycls.EMPCODE+"',GETDATE(),'P')";
                                lstQry.Add(strsql);
                                //dlMgt.UpdateUsingExecuteNonQueryList(strsql);
                                // qryStringCol.Add(strsql);

                                for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                                {
                                    string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                                    string tblot = purdata.Tables[d].Rows[h]["TB_LOT_NO"].ToString();
                                    string tbgrno = purdata.Tables[d].Rows[h]["TBGR_NO"].ToString();
                                    string tbgrade = purdata.Tables[d].Rows[h]["TB_GRADE"].ToString();
                                    string netwt = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                                    string rate = purdata.Tables[d].Rows[h]["RATE"].ToString();
                                    string buyergrade = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                                    string rejetype = purdata.Tables[d].Rows[h]["REJE_TYPE"].ToString();
                                    string pattacharge = purdata.Tables[d].Rows[h]["PATTA_CHARGE"].ToString();
                                    string status;
                                    string rejests;
                                    if (rejetype == "NONE")
                                    {
                                        rejests = "OK";
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
                                    //
                                    strsql = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='SERVICE'";
                                    DataTable ds1 = new DataTable();
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {
                                        servicetaxid = ds1.Rows[0]["TAX_ID"].ToString();
                                        servicetax = Convert.ToDouble(ds1.Rows[0]["RATE"]);

                                    }
                                    ds1.Clear();

                                    strsql = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='OTHERS' order by SNO";
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {
                                        servicechargeid = ds1.Rows[0]["TAX_ID"].ToString();
                                        servicecharge = Convert.ToDouble(ds1.Rows[0]["RATE"]);
                                    }
                                    ds1.Clear();


                                    strsql = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='SERVICE SH EDUCATION CESS' order by SNO";
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {
                                        servicechargeedshid = ds1.Rows[0]["TAX_ID"].ToString();
                                        servicechargeedsh = Convert.ToDouble(ds1.Rows[0]["RATE"]);
                                    }
                                    ds1.Clear();
                                    strsql = "SELECT TAX_ID,RATE,TAX_NAME,TAX_TYPE FROM GPIL_SERVICE_CHARGE_MASTER (NOLOCK) WHERE STATUS='Y' and TAX_TYPE='SERVICE TAX-EDUCATION CESS' order by SNO";
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {
                                        servicechargeshcessid = ds1.Rows[0]["TAX_ID"].ToString();
                                        servicechargeshcess = Convert.ToDouble(ds1.Rows[0]["RATE"]);
                                    }
                                    //
                                    double totalprice;
                                    double totalservicetaxamt;
                                    totalprice = Convert.ToDouble(netwt) * Convert.ToDouble(rate);
                                    servicechargeamt = (totalprice * servicecharge) / 100;
                                    servicetaxamt = (servicechargeamt * servicetax) / 100;
                                    servicechargeedshamt = (servicetaxamt * servicechargeedsh) / 100;
                                    servicechargeshcessamt = (servicetaxamt * servicechargeshcess) / 100;
                                    totalservicetaxamt = servicetaxamt + servicechargeamt + servicechargeedshamt + servicechargeshcessamt;

                                    strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,TB_LOT_NO,TBGR_NO,TB_GRADE,BUYER_GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRICE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                                    strsql = strsql + "Values('" + baleno.Trim() + "','FW','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + buyergrade.Trim() + "','" + netwt.Trim() + "','" + netwt.Trim() + "','LOC1','" + orgcd.Trim() + "','" + crop + "','" + variety + "','" + rate.Trim() + "','G','N','" + headerid + "','" + status + "','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','TAP','LOC1','" + orgcd + "')";

                                    // dlMgt.UpdateUsingExecuteNonQuery(strsql);
                                    lstQry.Add(strsql);

                                    strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[TBGR_NO],[TB_GRADE],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[PATTA_CHARGE],[SERVICE_CHARGE],[SERVICE_CHARGE_AMT],[SERVICE_TAX],[SERVICE_TAX_AMT],[CREATED_BY],[CREATED_DATE] ,[SH_ED_TAX],[ED_CESS_TAX])";
                                    if (rejetype == "NONE")
                                    {
                                        strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "',NULL,'" + status + "','N','" + pattacharge + "','" + servicechargeid + "',ROUND('" + servicechargeamt + "',2),'" + servicetaxid + "',ROUND('" + totalservicetaxamt + "',2),'" + Session["userID"].ToString() + "',GETDATE(),'" + servicechargeedshid + "','" + servicechargeshcessid + "')";
                                    }
                                    else
                                    {
                                        strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "','" + rejetype + "','" + status + "','N','" + pattacharge + "','" + servicechargeid + "','" + servicechargeamt + "','" + servicetaxid + "','" + totalservicetaxamt + "','" + Session["userID"].ToString() + "',GETDATE(),'" + servicechargeedshid + "','" + servicechargeshcessid + "')";
                                    }

                                    // dlMgt.UpdateUsingExecuteNonQuery(strsql);
                                    lstQry.Add(strsql);

                                    strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                                    strsql = strsql + "Values('PC" + orgcd + DateTime.Now.ToString("yyyyMMddhhmmss") + h + "','" + baleno.Trim() + "','TAP Purchase','" + headerid.Trim() + "','" + netwt.Trim() + "','" + orgcd.Trim() + "','" + Convert.ToDateTime(dateofpurch).ToString("yyyy-MM-dd") + "','N')";
                                    // dlMgt.UpdateUsingExecuteNonQuery(strsql);
                                    lstQry.Add(strsql);
                                }
                            }
                            // transaction.Commit();
                          
                           
                        }
                        catch (Exception ex)
                        {
                           
                            data = "Error: " + ex.Message;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }


                    }
                   
                }
            }
            catch (Exception ex)
            {
                data = "Error:" + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

            bool b = dlMgt.UpdateUsingExecuteNonQueryList(lstQry);
            if (b)
            {
                data = "Success: Data Inserted SucessFully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }



            //data = "Success: Data Uploaded Successfully";
            //json = JsonConvert.SerializeObject(data);
            //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
           return null;
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
            return View("TapPurchaseLoaderIndex");
        }


        /// <summary>
        /// FARMER PURCHASE
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerpPurchaseLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFPFromExcel(HttpPostedFileBase postedFile)
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


        [HttpPost]
        public JsonResult FarmerPurchaseComplete(ListFarmerPurchase LFP)
        {
            //Farmerpurchasedata(LFP);
            //return null;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;


            try
            {

                string Farmererror = string.Empty;
                String strsql = string.Empty;
                DataLoaderManagement dlMgt = new DataLoaderManagement();

                DataTable dtclstr = ToDataTable1(LFP.FarmerPurchases);
                var od = from s in LFP.FarmerPurchases
                         group s by new { s.ORGN_CODE, s.PURCH_DOC_NO } into newgroup
                         select new
                         {
                             ORGN_CODE = newgroup.Key.ORGN_CODE,
                             PURCH_DOC_NO = newgroup.Key.PURCH_DOC_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("ORGN_CODE");
                orgdata.Columns.Add("PURCH_DOC_NO");
                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.ORGN_CODE, rowObj.PURCH_DOC_NO);
                }


                //PUtchaseData
                DataSet purdata = new DataSet();
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["ORGN_CODE"].ToString() + orgdata.Rows[s]["PURCH_DOC_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("LOT_NO");
                    purdata.Tables[s].Columns.Add("FARMER_CODE");
                    purdata.Tables[s].Columns.Add("FORM_GRADE");
                    purdata.Tables[s].Columns.Add("NET_WT");
                    purdata.Tables[s].Columns.Add("RATE");
                    purdata.Tables[s].Columns.Add("BUYER_GRADE");
                    purdata.Tables[s].Columns.Add("REJE_STATUS");
                    purdata.Tables[s].Columns.Add("REJE_TYPE");
                    //purdata.Tables[s].Columns.Add("PATTA_CHARGE");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("BUYER_CODE");
                    purdata.Tables[s].Columns.Add("PURCH_DOC_NO");
                    purdata.Tables[s].Columns.Add("PURCHASE_DATE");
                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");

                    string orgcd = orgdata.Rows[s]["ORGN_CODE"].ToString();
                    string purdoc = orgdata.Rows[s]["PURCH_DOC_NO"].ToString();
                    DataRow[] purrows = dtclstr.Select("ORGN_CODE ='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "' AND PURCH_DOC_NO ='" + orgdata.Rows[s]["PURCH_DOC_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow row1 in purrows)
                        {
                            purdata.Tables[s].ImportRow(row1);
                        }
                    }

                    ////Validation

                    try
                    {

                        for (int d = 0; d < purdata.Tables.Count; d++)
                        {
                            string tblname = purdata.Tables[d].TableName;
                            int rowcount = purdata.Tables[d].Rows.Count;
                            if (d == 13)
                            {
                                //lblMessage.Text = "ADS";
                            }
                            string headerid1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                            string orgcd1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                            string byrcd1 = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                            string purchdoc1 = purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                            string dateofpurch1 = purdata.Tables[d].Rows[0]["PURCHASE_DATE"].ToString();
                            string crop1 = purdata.Tables[d].Rows[0]["CROP"].ToString();
                            string variety1 = purdata.Tables[d].Rows[0]["VARIETY"].ToString();



                            for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                            {
                                string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                                string tblot1 = purdata.Tables[d].Rows[h]["LOT_NO"].ToString();
                                string tbgrno1 = purdata.Tables[d].Rows[h]["FARMER_CODE"].ToString();
                                string tbgrade1 = purdata.Tables[d].Rows[h]["FORM_GRADE"].ToString();
                                string netwt1 = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                                string rate1 = purdata.Tables[d].Rows[h]["RATE"].ToString();
                                string buyergrade1 = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                                string rejests1 = purdata.Tables[d].Rows[h]["REJE_STATUS"].ToString();
                                string rejetype1 = purdata.Tables[d].Rows[h]["REJE_TYPE"].ToString();
                                //string pattacharge1 = purdata.Tables[d].Rows[h]["PATTA_CHARGE"].ToString();


                                if (baleno1.Substring(0, 2) != crop1)
                                {
                                    data = "Error:  Bale Number  and Corp Year MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (baleno1.Substring(2, 2) != variety1)
                                {
                                    data = "Error: Bale Number  and Variety MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }
                                else if (buyergrade1.Substring(0, 2) != crop1)
                                {
                                    data = "Error: Buyer Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }
                                else if (buyergrade1.Substring(2, 2) != variety1)
                                {
                                    data = "Error:  Buyer Grade and Variety MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }
                                else if (baleno1 == "")
                                {
                                    data = "Error:  BaleNumber is not empty --" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tblot1 == "")
                                {
                                    data = "Error: Lot Number is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tbgrno1 == "")
                                {
                                    data = "Error: Farmer Code is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (tbgrade1 == "")
                                {
                                    data = "Error: TB Grade is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (netwt1 == "" || Convert.ToDouble(netwt1) == 0)
                                {
                                    data = "Error: Weight is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (rate1 == "" || Convert.ToDouble(rate1) == 0)
                                {
                                    data = "Error: Rate is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (buyergrade1 == "")
                                {
                                    data = "Error: Buyer Grade is Empty for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                //else if (rejests1 == "")
                                //{
                                //    Farmerupdate(baleno1, "N", ref dtclstr);
                                //    i = i + 1;
                                //    Farmererror = Farmererror + Environment.NewLine + "Rejection Status is Empty for BaleNumber--" + baleno1;
                                //}

                                else
                                {

                                    strsql = "select * from GPIL_ITEM_MASTER (NOLOCK) where ITEM_CODE ='" + tbgrade1.Trim() + "'";
                                    DataTable ds1 = new DataTable();
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {
                                        strsql = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + baleno1.Trim() + "'";
                                        DataTable ds2 = new DataTable();
                                        ds2 = dlMgt.GetQueryResult(strsql);
                                        if (ds2.Rows.Count > 0)
                                        {
                                            data = "Error: Bale Already Purchased BaleNumber--" + baleno1;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                        else
                                        {
                                            DataTable ds3 = new DataTable();
                                            ds3 = dlMgt.GetQueryResult(strsql);
                                            if (ds3.Rows.Count == 0)
                                            {

                                            }
                                            else
                                            {
                                                data = "Error: Buyer Grade Does not exit in master BaleNumber--" + baleno1;
                                                json = JsonConvert.SerializeObject(data);
                                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                                jsonResult.MaxJsonLength = int.MaxValue;
                                                return jsonResult;
                                            }
                                        }
                                    }
                                    //else
                                    //{
                                    //    Farmerupdate(baleno1, "N", ref dtclstr);
                                    //    i = i + 1;
                                    //    Farmererror = Farmererror + Environment.NewLine + "TB Grade Does not exit in master BaleNumber--" + baleno1;
                                    //}
                                }


                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        data = "Error: " + ex.Message;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    finally
                    {
                    }


                    //InsertInfo
                    List<string> lstString = new List<string>();


                    for (int d = 0; d < purdata.Tables.Count; d++)
                    {
                        string headerid = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                        string orgcd1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                        string byrcd = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                        string purchdoc = purdata.Tables[d].Rows[0]["PURCH_DOC_NO"].ToString();
                        string dateofpurch = purdata.Tables[d].Rows[0]["PURCHASE_DATE"].ToString();
                        string crop = purdata.Tables[d].Rows[0]["CROP"].ToString();
                        string variety = purdata.Tables[d].Rows[0]["VARIETY"].ToString();

                        strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_HDR]([HEADER_ID],[ORGN_CODE],[PURCHASE_TYPE],[BUYER_CODE],[PURCH_DOC_NO],[DATE_OF_PURCH],[CROP],[VARIETY],[CREATED_BY],[CREATION_DATE],[STATUS])";
                        strsql = strsql + " VALUES('" + headerid + "','" + orgcd1 + "','SUNDRY PURCHASE','" + byrcd + "','" + purchdoc + "','" + Convert.ToDateTime(dateofpurch).ToString("yyyy-MM-dd") + "','" + crop + "','" + variety + "','" + Session["userID"].ToString() + "',GETDATE(),'P')";
                        // strsql = strsql + " VALUES('" + headerid + "','" + orgcd + "','TAP PURCHASE','" + byrcd + "','" + purchdoc + "',convert(datetime,'"+Convert.ToDateTime(dateofpurch)+"',105),'" + crop + "','" + variety + "','"+Propertycls.EMPCODE+"',GETDATE(),'P')";

                        // dlMgt.UpdateUsingExecuteNonQueryList(strsql);

                        lstString.Add(strsql);
                        strsql = "";

                        strsql = " insert into[dbo].[GPIL_CLASSIFICATION_HDR_TEMP] ( BATCH_NO,ORGN_CODE,CLASSIFIER_NAME,RECIPE_CODE, ";
                        strsql = strsql + " CLASSIFICATION_DATE,REASONING_CODE,CREATED_BY,CREATED_DATE,STATUS) ";
                        strsql = strsql + " values('" + headerid + "', '" + orgcd1 + "', '" + Session["userID"].ToString() + "' ,'CLASSIFICATION',  ";
                        strsql = strsql + " getdate(),0, '" + Session["userID"].ToString() + "', getdate(),'Y') ";

                        // dlMgt.UpdateUsingExecuteNonQueryList(strsql);

                        lstString.Add(strsql);


                        for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                        {
                            string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            string tblot = purdata.Tables[d].Rows[h]["LOT_NO"].ToString();
                            string tbgrno = purdata.Tables[d].Rows[h]["FARMER_CODE"].ToString();
                            string tbgrade = purdata.Tables[d].Rows[h]["FORM_GRADE"].ToString();
                            string netwt = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                            string rate = purdata.Tables[d].Rows[h]["RATE"].ToString();
                            string buyergrade = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                            string rejetype = purdata.Tables[d].Rows[h]["REJE_TYPE"].ToString();
                            //string pattacharge = purdata.Tables[d].Rows[h]["PATTA_CHARGE"].ToString();
                            string status;
                            string rejests;
                            if (rejetype == "NONE")
                            {
                                rejests = "OK";
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


                            //
                            double totalprice;
                            //double totalservicetaxamt;
                            totalprice = Convert.ToDouble(netwt) * Convert.ToDouble(rate);
                            //servicechargeamt = (totalprice * servicecharge) / 100;
                            //servicetaxamt = (servicechargeamt * servicetax) / 100;
                            //servicechargeedshamt = (servicetaxamt * servicechargeedsh) / 100;
                            //servicechargeshcessamt = (servicetaxamt * servicechargeshcess) / 100;
                            //totalservicetaxamt = servicetaxamt + servicechargeamt + servicechargeedshamt + servicechargeshcessamt;

                            strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,TB_LOT_NO,TBGR_NO,TB_GRADE,BUYER_GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRICE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE,GRADE)";
                            strsql = strsql + "Values('" + baleno.Trim() + "','FW','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + buyergrade.Trim() + "','" + netwt.Trim() + "','" + netwt.Trim() + "','LOC1','" + orgcd.Trim() + "','" + crop + "','" + variety + "','" + rate.Trim() + "','G','N','" + headerid + "','" + status + "','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','TAP','LOC1','" + orgcd + "','" + tbgrade.Trim() + "')";

                            // dlMgt.UpdateUsingExecuteNonQuery(strsql);
                            lstString.Add(strsql);

                            strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[FARMER_CODE],[TB_GRADE],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[CREATED_BY],[CREATED_DATE],[ATTRIBUTE2])";
                            if (rejetype == "NONE")
                            {
                                strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "',NULL,'" + status + "','N','" + Session["userID"].ToString() + "',GETDATE(),'" + tbgrade.Trim() + "')";
                            }
                            else
                            {
                                strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "','" + rejetype + "','" + status + "','N','" + Session["userID"].ToString() + "',GETDATE(),'" + tbgrade.Trim() + "')";
                            }

                            //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                            lstString.Add(strsql);
                            strsql = "";
                            strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                            strsql = strsql + "Values('PC" + orgcd + DateTime.Now.ToString("yyyyMMddhhmmss") + h + "','" + baleno.Trim() + "','Farmer Purchase','" + headerid.Trim() + "','" + netwt.Trim() + "','" + orgcd.Trim() + "','" + Convert.ToDateTime(dateofpurch).ToString("yyyy-MM-dd") + "','N')";

                            //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                            lstString.Add(strsql);
                        }
                    }

                    dlMgt.UpdateUsingExecuteNonQueryList(lstString);
                }
                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
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
        /// <summary>
        /// Farmer Loan Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerLoanLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFLFromExcel(HttpPostedFileBase postedFile)
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
            return View("FarmerLoanLoaderIndex");
        }


        [HttpPost]
        public JsonResult FarmerLoanComplete(ListFarmerLoan LFL)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            DataTable dtclstr = new DataTable();
            DataLoaderManagement dlMgt = new DataLoaderManagement();
            try
            {
                dtclstr = ToDataTable1(LFL.FarmerLoans);
                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {
                    string strCrop = dtclstr.Rows[d]["CROP"].ToString();
                    string strVariety = dtclstr.Rows[d]["VARIETY"].ToString();


                    string strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                    string strLoanAmount = dtclstr.Rows[d]["LOAN_AMOUNT"].ToString();

                    string strQuery = "SELECT F.FARM_CODE FROM GPIL_FARMER_MASTER(NOLOCK) F,GPIL_FARMER_CROP_HISTORY(NOLOCK) FC,GPIL_VARIETY_SEASON_MASTER(NOLOCK) VS WHERE F.FARM_CODE=FC.FARM_CODE AND VS.VARIETY=FC.VARIETY AND VS.CROP=FC.CROP AND FC.CROP='" + strCrop + "' AND FC.VARIETY='" + strVariety + "' AND FC.FARM_CODE='" + strFarmerCode + "'";

                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strQuery);
                    if (ds1.Rows.Count != 0)
                    {

                        data = "Error:  Farmer Code (" + strFarmerCode + ") may doesn't exist/Invalid Crop & Variety/Crop not match with current Crop as against Variety";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }
                    else
                    {
                        try
                        {
                            float f;

                            if (float.TryParse(strLoanAmount, out f) == false)
                            {
                                data = "Error:  Invalid Loan Amount as against Farmer Code (" + strFarmerCode + ")";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                        }
                        catch (Exception ex)
                        {
                            data = "Error: Invalid Loan Amount as against Farmer Code (" + strFarmerCode + ")";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                data = "Error: Exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            finally
            {
            }

            try
            {

                string Farmererror = string.Empty;
                String strsql = string.Empty;

                DataTable FLdata = new DataTable();


                //PUtchaseData
                for (int d = 0; d < dtclstr.Rows.Count; d++)
                {
                    string strFarmerCode, strLoanAmount;

                    string strCrop, strVariety;
                    string lvarStrTotalLoan, lvarStrTotalBalance;


                    strCrop = dtclstr.Rows[d]["CROP"].ToString();
                    strVariety = dtclstr.Rows[d]["VARIETY"].ToString();

                    strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                    strLoanAmount = dtclstr.Rows[d]["LOAN_AMOUNT"].ToString();



                    strsql = "SELECT FARM_CODE,LOAN_AMOUNT,BALANCE_AMOUNT FROM GPIL_FARMER_CROP_HISTORY(NOLOCK) WHERE FARM_CODE='" + strFarmerCode + "' AND CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";
                    //SqlDataReader strrs;
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);

                    if (ds1.Rows.Count > 0)
                    {
                        lvarStrTotalLoan = Convert.ToDouble(ds1.Rows[0][1]).ToString(); //ds1.GetDouble(1).ToString();
                        lvarStrTotalBalance = Convert.ToDouble(ds1.Rows[0][2]).ToString();
                    }
                    else
                    {
                        lvarStrTotalLoan = "0";
                        lvarStrTotalBalance = "0";
                    }

                    ds1.Dispose();
                    //ds1.Close();


                    string lvarStrFinalLoan, lvarStrFinalBalance;

                    lvarStrFinalLoan = Convert.ToString(Convert.ToDouble(lvarStrTotalLoan) + Convert.ToDouble(strLoanAmount));
                    lvarStrFinalBalance = Convert.ToString(Convert.ToDouble(lvarStrTotalBalance) + Convert.ToDouble(strLoanAmount));


                    string lvarLoanTransactionQuery = "INSERT INTO [GPIL_FARMER_LOAN_TRANSACTIONS] ([TRAN_ID],[CROP],[VARIETY],[FARM_CODE],[CURR_LOAN_AMOUNT],[CREDIT_AMOUNT],[DEBIT_AMOUNT],[FINAL_LOAN_AMOUNT],[REMARKS],[STATUS],[CREATED_BY],[CREATED_DATE]) VALUES ('" + DateTime.Now.ToString("yyyyMMddHHmmss") + d.ToString().PadLeft(4, '0') + "','" + strCrop + "','" + strVariety + "','" + strFarmerCode + "','" + lvarStrTotalBalance + "','" + strLoanAmount + "','0','" + lvarStrFinalBalance + "','CREDIT AMOUNT','Y','" + Session["userID"].ToString() + "',GETDATE())";
                    string lvarFarmerMasterUpdateQuery = "UPDATE GPIL_FARMER_MASTER SET ATTRIBUTE4='" + lvarStrFinalLoan + "',LOAN_AMOUNT='" + lvarStrFinalBalance + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "'";
                    string lvarFarmerCropHisUpdateQuery = "UPDATE GPIL_FARMER_CROP_HISTORY SET LOAN_AMOUNT='" + lvarStrFinalLoan + "',BALANCE_AMOUNT='" + lvarStrFinalBalance + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "' AND CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";
                    dlMgt.UpdateUsingExecuteNonQuery(lvarLoanTransactionQuery);
                    dlMgt.UpdateUsingExecuteNonQuery(lvarFarmerMasterUpdateQuery);
                    dlMgt.UpdateUsingExecuteNonQuery(lvarFarmerCropHisUpdateQuery);
                }


                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        /// <summary>
        /// Farmer Bank Details Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmerBankDetailsLoaderIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportFBFromExcel(HttpPostedFileBase postedFile)
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
            return View("FarmerBankDetailsLoaderIndex");
        }

        DataLoaderManagement dlMgt = new DataLoaderManagement();
        [HttpPost]
        public JsonResult FarmerBankComplete(ListFarmerBankDetails LFB)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            //////////////////VALIDATE//////////////////////


            DataTable dtclstr = ToDataTable1(LFB.FarmerBanks);
            for (int d = 0; d < dtclstr.Rows.Count; d++)
            {
                string strCrop = dtclstr.Rows[d]["CROP"].ToString();
                string strVariety = dtclstr.Rows[d]["VARIETY"].ToString();


                string strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();

                string lvarStrFarmerName, lvarStrFarmerFatherName, lvarStrMobileNo, lvarStrEmailId, lvarStrAccountNo, lvarStrBankName, lvarStrBranchName, lvarStrIFSCCode;


                lvarStrFarmerName = dtclstr.Rows[d]["FARMER_NAME"].ToString();
                lvarStrFarmerFatherName = dtclstr.Rows[d]["FARMER_FATHER_NAME"].ToString();
                lvarStrMobileNo = dtclstr.Rows[d]["MOBILE_NO"].ToString();
                lvarStrEmailId = dtclstr.Rows[d]["EMAIL_ID"].ToString();
                lvarStrAccountNo = dtclstr.Rows[d]["BANK_ACCOUNT_NO"].ToString();
                lvarStrBankName = dtclstr.Rows[d]["BANK_NAME"].ToString();
                lvarStrBranchName = dtclstr.Rows[d]["BRANCH_NAME"].ToString();
                lvarStrIFSCCode = dtclstr.Rows[d]["IFSC_CODE"].ToString();


                string strQuery1 = "SELECT FARM_CODE FROM GPIL_FARMER_MASTER WHERE FARM_CODE='" + strFarmerCode + "'";
                string strQuery2 = "SELECT * FROM GPIL_VARIETY_MASTER WHERE VARIETY='" + strVariety + "'";
                string strQuery3 = "SELECT * FROM GPIL_VARIETY_SEASON_MASTER WHERE CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";

                DataTable ds1 = new DataTable();
                ds1 = dlMgt.GetQueryResult(strQuery1);
                DataTable ds2 = new DataTable();
                ds2 = dlMgt.GetQueryResult(strQuery2);
                DataTable ds3 = new DataTable();
                ds3 = dlMgt.GetQueryResult(strQuery3);

                if (ds1.Rows.Count == 0)
                {
                    data = "Error:  Farmer Code (" + strFarmerCode + ") doesn't exist in master";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }
                else if (ds2.Rows.Count == 0)
                {


                    data = "Error: Variety (" + strVariety + ")  doesn't exist in master; Farmer Code (" + strFarmerCode + ")";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (ds3.Rows.Count == 0)
                {


                    data = "Error: Crop (" + strCrop + ")  is not match with current crop of given variety (" + strVariety + ") ; Farmer Code (" + strFarmerCode + ")";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrFarmerName.Trim().Length == 0 || lvarStrFarmerName.Length > 50)
                {


                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Farmer Name (" + lvarStrFarmerName + ") must required with maximum length of 50 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrFarmerFatherName.Trim().Length == 0 || lvarStrFarmerFatherName.Length > 50)
                {
                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Farmer Father Name (" + lvarStrFarmerFatherName + ") must required with maximum length of 50 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrMobileNo.Length > 11)
                {

                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Mobile No (" + lvarStrMobileNo + ") length should not exist 11 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrEmailId.Length > 50)
                {


                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Email Id (" + lvarStrEmailId + ") length should not exist 50 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrAccountNo.Trim().Length == 0 || lvarStrAccountNo.Length > 25)
                {
                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Account No (" + lvarStrAccountNo + ") must required with maximum length of 25 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;


                }
                else if (lvarStrBankName.Trim().Length == 0 || lvarStrBankName.Length > 50)
                {
                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Bank Name (" + lvarStrBankName + ") must required with maximum length of 50 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrBranchName.Trim().Length == 0 || lvarStrBranchName.Length > 50)
                {
                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : Branch Name (" + lvarStrBranchName + ") must required with maximum length of 50 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (lvarStrIFSCCode.Trim().Length == 0 || lvarStrIFSCCode.Length > 11)
                {

                    data = "Error: Crop (" + strCrop + ") || Variety (" + strVariety + ")||Farmer Code (" + strFarmerCode + ") : IFSC Code (" + lvarStrIFSCCode + ") must required with maximum length of 11 characters";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }



            }
            try
            {

                string Farmererror = string.Empty;
                String strsql = string.Empty;
                DataLoaderManagement dlMgt = new DataLoaderManagement();




                DataTable FLdata = new DataTable();
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        for (int d = 0; d < dtclstr.Rows.Count; d++)
                        {
                            string strFarmerCode, lvarStrFarmerName, lvarStrFarmerFatherName, lvarStrMobileNo, lvarStrEmailId, lvarStrAccountNo, lvarStrBankName, lvarStrBranchName, lvarStrIFSCCode;
                            string strQuery;
                            string strCrop, strVariety;


                            strCrop = dtclstr.Rows[d]["CROP"].ToString();
                            strVariety = dtclstr.Rows[d]["VARIETY"].ToString();

                            strFarmerCode = dtclstr.Rows[d]["FARMER_CODE"].ToString();
                            lvarStrFarmerName = dtclstr.Rows[d]["FARMER_NAME"].ToString();
                            lvarStrFarmerFatherName = dtclstr.Rows[d]["FARMER_FATHER_NAME"].ToString();
                            lvarStrMobileNo = dtclstr.Rows[d]["MOBILE_NO"].ToString();
                            lvarStrEmailId = dtclstr.Rows[d]["EMAIL_ID"].ToString();
                            lvarStrAccountNo = dtclstr.Rows[d]["BANK_ACCOUNT_NO"].ToString();
                            lvarStrBankName = dtclstr.Rows[d]["BANK_NAME"].ToString();
                            lvarStrBranchName = dtclstr.Rows[d]["BRANCH_NAME"].ToString();
                            lvarStrIFSCCode = dtclstr.Rows[d]["IFSC_CODE"].ToString();

                            string lvarFarmerMasterUpdateQuery = "";
                            string lvarFarmerCropHisUpdateQuery = "";


                            strsql = "SELECT FARM_CODE FROM GPIL_FARMER_CROP_HISTORY (NOLOCK) WHERE FARM_CODE='" + strFarmerCode + "' AND CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";

                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {
                                lvarFarmerMasterUpdateQuery = "UPDATE GPIL_FARMER_MASTER SET FARM_NAME='" + lvarStrFarmerName + "',FARM_FATHER_NAME='" + lvarStrFarmerFatherName + "',MOBILE_NO='" + lvarStrMobileNo + "',EMAIL_ID='" + lvarStrEmailId + "',BANK_ACCOUNT_NO='" + lvarStrAccountNo + "',BANK_NAME='" + lvarStrBankName + "',BRANCH_NAME='" + lvarStrBranchName + "',IFSC_CODE='" + lvarStrIFSCCode + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "'";
                                lvarFarmerCropHisUpdateQuery = "UPDATE GPIL_FARMER_CROP_HISTORY SET MOBILE_NO='" + lvarStrMobileNo + "',EMAIL_ID='" + lvarStrEmailId + "',BANK_ACCOUNT_NO='" + lvarStrAccountNo + "',BANK_NAME='" + lvarStrBankName + "',BRANCH_NAME='" + lvarStrBranchName + "',IFSC_CODE='" + lvarStrIFSCCode + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "' AND CROP='" + strCrop + "' AND VARIETY='" + strVariety + "'";

                            }
                            else
                            {
                                lvarFarmerMasterUpdateQuery = "UPDATE GPIL_FARMER_MASTER SET FARM_NAME='" + lvarStrFarmerName + "',FARM_FATHER_NAME='" + lvarStrFarmerFatherName + "',MOBILE_NO='" + lvarStrMobileNo + "',EMAIL_ID='" + lvarStrEmailId + "',BANK_ACCOUNT_NO='" + lvarStrAccountNo + "',BANK_NAME='" + lvarStrBankName + "',BRANCH_NAME='" + lvarStrBranchName + "',IFSC_CODE='" + lvarStrIFSCCode + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE FARM_CODE='" + strFarmerCode + "'";
                                lvarFarmerCropHisUpdateQuery = "INSERT INTO GPIL_FARMER_CROP_HISTORY (HIS_CODE,FARM_CODE,CROP,VARIETY,MOBILE_NO,EMAIL_ID,BANK_ACCOUNT_NO,BANK_NAME,BRANCH_NAME,IFSC_CODE,CREATED_BY,CREATED_DATE,STATUS,LOAN_AMOUNT,BALANCE_AMOUNT) VALUES ('" + strCrop + strVariety + strFarmerCode + "','" + strFarmerCode + "','" + strCrop + "','" + strVariety + "','" + lvarStrMobileNo + "','" + lvarStrEmailId + "','" + lvarStrAccountNo + "','" + lvarStrBankName + "','" + lvarStrBranchName + "','" + lvarStrIFSCCode + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','0','0') ";

                            }


                            ds1.Dispose();


                            dlMgt.UpdateUsingExecuteNonQuery(lvarFarmerMasterUpdateQuery);
                            dlMgt.UpdateUsingExecuteNonQuery(lvarFarmerCropHisUpdateQuery);
                            transaction.Commit();

                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        data = "Error: " + ex.Message;
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                }
                data = "Success: Data Uploaded Successfully";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
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


        }



        /// <summary>
        /// Supplier Purchase Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierPurchaseLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportSPFromExcel(HttpPostedFileBase postedFile)
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
            return View("SupplierPurchaseLoaderIndex");
        }


        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;
        [HttpPost]
        public JsonResult SupplierPurchaseComplete(ListSupplierPurchase LSP)
        {
            try
            {
                string strsql = string.Empty;

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LSP.SupplierPurchases);
                var od = from s in LSP.SupplierPurchases
                         group s by new { s.SUPP_CODE, s.RECEV_ORG_CODE, s.LP4_NUMBER } into newgroup
                         select new
                         {
                             SUPP_CODE = newgroup.Key.SUPP_CODE,
                             RECEV_ORG_CODE = newgroup.Key.RECEV_ORG_CODE,
                             LP4_NUMBER = newgroup.Key.LP4_NUMBER

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("SUPP_CODE");
                orgdata.Columns.Add("RECEV_ORG_CODE");
                orgdata.Columns.Add("LP4_NUMBER");
                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SUPP_CODE, rowObj.RECEV_ORG_CODE, rowObj.LP4_NUMBER);
                }

                //Purchase Data
                DataSet purdata = new DataSet();
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SUPP_CODE"].ToString() + orgdata.Rows[s]["RECEV_ORG_CODE"].ToString() + orgdata.Rows[s]["LP4_NUMBER"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("BUYER_GRADE");
                    purdata.Tables[s].Columns.Add("NET_WEIGHT");
                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");
                    purdata.Tables[s].Columns.Add("SUPP_CODE");
                    purdata.Tables[s].Columns.Add("RECEV_ORG_CODE");
                    purdata.Tables[s].Columns.Add("BUYER_CODE");
                    purdata.Tables[s].Columns.Add("LP4_NUMBER");
                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");

                    string suppCode = orgdata.Rows[s]["SUPP_CODE"].ToString();
                    string recOrgnCode = orgdata.Rows[s]["RECEV_ORG_CODE"].ToString();
                    string lp4Number = orgdata.Rows[s]["LP4_NUMBER"].ToString();
                    DataRow[] purrows = dtclstr.Select("RECEV_ORG_CODE ='" + orgdata.Rows[s]["RECEV_ORG_CODE"].ToString() + "' AND LP4_NUMBER ='" + orgdata.Rows[s]["LP4_NUMBER"].ToString() + "' AND SUPP_CODE ='" + orgdata.Rows[s]["SUPP_CODE"].ToString() + "'");

                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow row1 in purrows)
                        {
                            purdata.Tables[s].ImportRow(row1);
                        }
                    }


                    //Insert Info

                    for (int d = 0; d < purdata.Tables.Count; d++)
                    {
                        string crop1 = purdata.Tables[d].Rows[0]["CROP"].ToString();
                        string variety1 = purdata.Tables[d].Rows[0]["VARIETY"].ToString();
                        string suppcode1 = purdata.Tables[d].Rows[0]["SUPP_CODE"].ToString();

                        for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                        {
                            string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            string buyergrade1 = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                            string netwt1 = purdata.Tables[d].Rows[h]["NET_WEIGHT"].ToString();
                            string subinvcode1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();


                            if (baleno1 == "")
                            {
                                //SupllierPurchaseupdate(baleno1, "N");
                                //i = i + 1;

                            }
                            else if (netwt1 == "" || Convert.ToDouble(netwt1) == 0)
                            {

                                data = "Error: Weight is Empty for BaleNumber-- " + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            else if (buyergrade1 == "")
                            {

                                data = "Error: Buyer Grade is Empty for BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            else if (subinvcode1 == "")
                            {

                                data = "Error: Sub-Inventory Code is Empty for BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            else
                            {
                                strsql = "select * from GPIL_ITEM_MASTER where ITEM_CODE ='" + buyergrade1.Trim() + "'";
                                DataTable ds1 = new DataTable();
                                ds1 = dlMgt.GetQueryResult(strsql);
                                if (ds1.Rows.Count > 0)
                                {

                                    strsql = "select * from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno1.Trim() + "'";
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count > 0)
                                    {

                                        data = "Error: Bale Already Purchased BaleNumber--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                    }
                                    else
                                    {
                                        //SupllierPurchaseupdate(baleno1, "Y");
                                    }

                                }
                                else
                                {


                                    data = "Error: Buyer Grade Does not exit in master BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                            }


                        }

                    }


                    for (int d = 0; d < purdata.Tables.Count; d++)
                    {
                        int cnt = 0;
                        /// Generating Header Id
                        string baleno1 = purdata.Tables[d].Rows[0]["GPIL_BALE_NUMBER"].ToString();
                        string temphdr = purdata.Tables[d].Rows[0]["RECEV_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["LP4_NUMBER"].ToString();
                        strsql = "select * from GPIL_SUPP_PURCHS_HDR where HEADER_ID like '%" + temphdr + "%'";
                        DataTable ds1 = new DataTable();
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            //arStrTotalLoan = Convert.ToDouble(ds1.Rows[0][1]).ToString();
                            cnt = Convert.ToInt32(ds1.Rows[0][1]) + 1;
                        }
                        //strrs.Close();
                        //cmd.Dispose();
                        string headerid;
                        if (Convert.ToString(cnt).Length == 1)
                        {
                            headerid = purdata.Tables[d].Rows[0]["SUPP_CODE"].ToString() + purdata.Tables[d].Rows[0]["RECEV_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["LP4_NUMBER"].ToString() + "0" + cnt;
                        }
                        else
                        {
                            headerid = purdata.Tables[d].Rows[0]["SUPP_CODE"].ToString() + purdata.Tables[d].Rows[0]["RECEV_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["LP4_NUMBER"].ToString() + cnt;
                        }


                        /// Retriving Supplier Information
                        string suppcode = purdata.Tables[d].Rows[0]["SUPP_CODE"].ToString();
                        string supperpcode;
                        string suppname;
                        strsql = "select SUPP_CODE,SUPP_NAME,SITE_NAME from GPIL_SUPPLIER_MASTER where GPIL_SUPP_CODE='" + suppcode + "'";
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            supperpcode = ds1.Rows[0][0].ToString();
                            suppname = ds1.Rows[0][1].ToString() + " " + ds1.Rows[0][2].ToString();


                            suppname = Regex.Replace(suppname, @"[^0-9a-zA-Z]+", "");
                        }
                        else
                        {
                            data = "Error: Supplier Code Doesn't Match with Master Data--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                            //throw new Exception("Supplier Code Doesn't Match with Master Data");
                        }

                        string orgcd = purdata.Tables[d].Rows[0]["RECEV_ORG_CODE"].ToString();
                        string byrcd = purdata.Tables[d].Rows[0]["BUYER_CODE"].ToString();
                        string purchdoc = purdata.Tables[d].Rows[0]["LP4_NUMBER"].ToString();
                        string crop = purdata.Tables[d].Rows[0]["CROP"].ToString();
                        string variety = purdata.Tables[d].Rows[0]["VARIETY"].ToString();

                        strsql = "INSERT INTO [GPIL_SUPP_PURCHS_HDR]([HEADER_ID],[SUPP_CODE],[SITE_NAME],[BUYER_CODE],[RECEV_ORGN_CODE],[LP4_NUMBER],[CROP],[VARIETY],[CREATED_BY],[CREATED_DATE],[STATUS],[LP4_DATE])";
                        strsql = strsql + " VALUES('" + headerid + "','" + supperpcode + "','" + suppname + "','" + byrcd + "','" + orgcd + "','" + purchdoc + "','" + crop + "','" + variety + "','" + Session["userID"].ToString() + "',GETDATE(),'P',GETDATE())";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                        {
                            string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            string netwt = purdata.Tables[d].Rows[h]["NET_WEIGHT"].ToString();
                            string buyergrade = purdata.Tables[d].Rows[h]["BUYER_GRADE"].ToString();
                            string subinvcode = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();


                            strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,BUYER_GRADE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                            strsql = strsql + "Values('" + baleno.Trim() + "','" + subinvcode.Trim() + "','" + buyergrade.Trim() + "','" + buyergrade.Trim() + "','" + netwt.Trim() + "','" + netwt.Trim() + "','LOC1','" + orgcd.Trim() + "','" + crop + "','" + variety + "','G','N','" + headerid + "','Y','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','SUPPLIER','LOC1','" + orgcd + "')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);

                            strsql = "INSERT INTO [GPIL_SUPP_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[GRADE],[NET_WEIGHT],[SEC_QTY],[SUBINVENTORY_CODE],[CROP],[VARIETY],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                            strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + buyergrade.Trim() + "','" + netwt.Trim() + "','1','" + subinvcode.Trim() + "','" + crop + "','" + variety + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);

                            strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                            strsql = strsql + "Values('PC" + orgcd + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','Supp Purchase','" + headerid.Trim() + "','" + netwt.Trim() + "','" + orgcd.Trim() + "',GETDATE(),'N')";
                            //strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "','" + Convert.ToDateTime(loadtime) + "','N')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);


                        }

                    }

                    //}

                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
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

        }


        DataTable dtclstr = new DataTable();



        /// <summary> 
        /// Dispatch Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult DispatchLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportDFromExcel(HttpPostedFileBase postedFile)
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
            return View("DispatchLoaderIndex");
        }


        [HttpPost]
        public JsonResult DispatchComplete(ListDispatch LD)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            try
            {
                //Purchase Data
                string strsql = string.Empty, retVal = string.Empty;
                DataLoaderManagement dlMgt = new DataLoaderManagement();

                DataTable orgdata = new DataTable();
                DataTable dtclstr = ToDataTable(LD.Dispatchs);
                var od = from s in LD.Dispatchs
                         group s by new { s.SHIPMENT_NO } into newgroup
                         select new
                         {
                             SHIPMENT_NO = newgroup.Key.SHIPMENT_NO


                         };
                var ods = od.ToList();

                orgdata.Columns.Add("SHIPMENT_NO");

                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SHIPMENT_NO);
                }

                //Purchase Data
                DataSet purdata = new DataSet();
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SHIPMENT_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("SHIPMENT_NO");
                    purdata.Tables[s].Columns.Add("SENDER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("RECEIVER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("SEND_BY");
                    purdata.Tables[s].Columns.Add("SENDER_TRUCK_NO");
                    purdata.Tables[s].Columns.Add("RC_NO");
                    purdata.Tables[s].Columns.Add("DRIVER_NAME");
                    purdata.Tables[s].Columns.Add("DRIVING_LICENCE_NO");
                    purdata.Tables[s].Columns.Add("TRANSPORT_NAME");
                    purdata.Tables[s].Columns.Add("FRIEGHT_CHARGES");

                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");

                    purdata.Tables[s].Columns.Add("FROM_SUBINVENTORY_CODE");
                    purdata.Tables[s].Columns.Add("TO_SUBINVENTORY_CODE");

                    purdata.Tables[s].Columns.Add("LOADING_DATETIME", typeof(DateTime));


                    DataRow[] purrows = dtclstr.Select("SHIPMENT_NO ='" + orgdata.Rows[s]["SHIPMENT_NO"].ToString() + "'");

                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }
                /////////

                double mrkdwt;
                string processts;
                string errordata = string.Empty;
                int i = 0;
                errordata = "Error :";
                try
                {

                    for (int d = 0; d < purdata.Tables.Count; d++)
                    {
                        for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                        {

                            string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            string subinventorycd1 = purdata.Tables[d].Rows[h]["FROM_SUBINVENTORY_CODE"].ToString();
                            string currorg = purdata.Tables[d].Rows[h]["SENDER_ORG_CODE"].ToString();


                            if (baleno1 == "")
                            {
                                data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            else
                            {
                                string orgntype = "";
                                strsql = "Select ORGN_TYPE from GPIL_ORGN_MASTER where ORGN_CODE='" + currorg + "'";
                                DataTable ds1 = new DataTable();
                                ds1 = dlMgt.GetQueryResult(strsql);
                                if (ds1.Rows.Count > 0)
                                {
                                    orgntype = Convert.ToString(ds1.Rows[0][0]);
                                }

                                strsql = "select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno1 + "' and CURR_ORGN_CODE='" + currorg + "' AND STATUS='Y'";
                                ds1 = dlMgt.GetQueryResult(strsql);

                                if (ds1.Rows.Count > 0)
                                {
                                    string srksubcd;
                                    mrkdwt = Convert.ToDouble(ds1.Rows[0][0]);
                                    processts = Convert.ToString(ds1.Rows[0][1]);
                                    srksubcd = Convert.ToString(ds1.Rows[0][2]);
                                    if (processts == "Y")
                                    {
                                        data = "Error: Bale Is using in another process Bale Number--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                    }
                                    if (srksubcd != subinventorycd1)
                                    {
                                        data = "Error: Subinventory Code Mismatch--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;

                                    }
                                }
                                else
                                {
                                    data = "Error: BaleNumber Does not exists Bale Number--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }


                            }
                        }

                    }

                }
                catch (Exception ex)
                {

                    data = "Error: " + ex.Message;
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                finally
                {

                    // purdata.Clear();

                }

                List<string> lstQry = new List<string>();

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    string temphdr = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string sendorg = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString();
                    string recevorg = purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString();
                    string sendby = purdata.Tables[d].Rows[0]["SEND_BY"].ToString();
                    string truckno = purdata.Tables[d].Rows[0]["SENDER_TRUCK_NO"].ToString();
                    string rcno = purdata.Tables[d].Rows[0]["RC_NO"].ToString();
                    string drivername = purdata.Tables[d].Rows[0]["DRIVER_NAME"].ToString();
                    string licenceno = purdata.Tables[d].Rows[0]["DRIVING_LICENCE_NO"].ToString();
                    string transportname = purdata.Tables[d].Rows[0]["TRANSPORT_NAME"].ToString();
                    string frightchrgs = purdata.Tables[d].Rows[0]["FRIEGHT_CHARGES"].ToString();
                    DateTime currentD = Convert.ToDateTime(purdata.Tables[d].Rows[0]["LOADING_DATETIME"]);
                    string senddate = purdata.Tables[d].Rows[0]["LOADING_DATETIME"].ToString();// currentD.ToString("MM/dd/yyyy HH:mm:ss ", CultureInfo.InvariantCulture);
                    string sndorgty = "";
                    string rcvorgtyp = "";

                    DateTime dt = Convert.ToDateTime(senddate);
                    senddate = dt.ToString("yyyy-MM-dd");


                    strsql = "Select O.ORGN_TYPE,O2.ORGN_TYPE FROM GPIL_ORGN_MASTER O,GPIL_ORGN_MASTER O2 WHERE O.ORGN_CODE='" + sendorg + "' AND O2.ORGN_CODE='" + recevorg + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {
                        sndorgty = Convert.ToString(ds1.Rows[0][0]);
                        rcvorgtyp = Convert.ToString(ds1.Rows[0][1]);
                    }
                    if (LD.chkPSW)
                    {
                        strsql = "INSERT INTO [GPIL_SHIPMENT_HDR_TEMP]([SHIPMENT_NO],[SENDER_ORGN_CODE],[RECEIVER_ORGN_CODE],[SENDER_DATE],[SENT_BY],[SENDER_TRUCK_NO] ,[RC_NO],[DRIVER_NAME],[DRIVING_LICENCE_NO],[TRANSPORT_NAME],[FRIEGHT_CHARGES],[UOM],[CREATED_BY],[CREATED_DATE],[STATUS],[ATTRIBUTE2],[ATTRIBUTE3],TOT_NO_OF_BALES,IS_WMS_SHIPMENT)";
                        strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "','" + senddate + "','" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','" + frightchrgs + "','KG','" + Session["userID"].ToString() + "',GETDATE(),'INT','" + sndorgty + "','" + rcvorgtyp + "'," + purdata.Tables[d].Rows.Count + ",'Y')";
                    }
                    else
                    {
                        strsql = "INSERT INTO [GPIL_SHIPMENT_HDR_TEMP]([SHIPMENT_NO],[SENDER_ORGN_CODE],[RECEIVER_ORGN_CODE],[SENDER_DATE],[SENT_BY],[SENDER_TRUCK_NO] ,[RC_NO],[DRIVER_NAME],[DRIVING_LICENCE_NO],[TRANSPORT_NAME],[FRIEGHT_CHARGES],[UOM],[CREATED_BY],[CREATED_DATE],[STATUS],[ATTRIBUTE2],[ATTRIBUTE3],TOT_NO_OF_BALES)";
                        strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "','" + senddate + "','" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','" + frightchrgs + "','KG','" + Session["userID"].ToString() + "',GETDATE(),'INT','" + sndorgty + "','" + rcvorgtyp + "'," + purdata.Tables[d].Rows.Count + ")";
                    }

                    lstQry.Add(strsql);
                    //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        // string diswt = purdata.Tables[d].Rows[h]["DISPATCH_WT"].ToString();
                        string fromsubcode = purdata.Tables[d].Rows[h]["FROM_SUBINVENTORY_CODE"].ToString();
                        string tosubcode = purdata.Tables[d].Rows[h]["TO_SUBINVENTORY_CODE"].ToString();
                        // string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string loadtime = purdata.Tables[d].Rows[h]["LOADING_DATETIME"].ToString();
                        string markedwt = "0";
                        string diswt = "0";
                        string grade = string.Empty;
                        string orgntype = string.Empty;

                        strsql = "Select ORGN_TYPE from GPIL_ORGN_MASTER where ORGN_CODE='" + sendorg + "'";

                        ds1 = dlMgt.GetQueryResult(strsql);

                        if (ds1.Rows.Count > 0)
                        {
                            orgntype = Convert.ToString(ds1.Rows[0][0]);
                        }


                        if (orgntype == "TAP")
                        {
                            strsql = "select MARKED_WT,PROCESS_STATUS,CURR_WT,BUYER_GRADE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        }
                        else
                        {
                            strsql = "select MARKED_WT,PROCESS_STATUS,CURR_WT,GRADE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        }

                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            markedwt = Convert.ToString(ds1.Rows[0][0]);
                            diswt = Convert.ToString(ds1.Rows[0][2]);
                            grade = Convert.ToString(ds1.Rows[0][3]);
                        }




                        
                        DateTime dtSDate = Convert.ToDateTime(loadtime);
                        loadtime = dtSDate.ToString("yyyy-MM-dd");
                        
                        


                        strsql = "INSERT INTO [GPIL_SHIPMENT_DTLS_TEMP]([SHIPMENT_NO],[DETAIL_ID],[GPIL_BALE_NUMBER],[MARKED_WT],[DISPATCH_WEIGHT],[FROM_SUBINVENTORY_CODE],[TO_SUBINVENTORY_CODE],[LOADING_DATETIME],[STATUS],[HEADER_STATUS],[CREATED_BY],[CREATED_DATE],[GRADE])";
                        strsql = strsql + "Values('" + temphdr + "','SH" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','" + markedwt.Trim() + "','" + diswt.Trim() + "','" + fromsubcode.Trim() + "','" + tosubcode.Trim() + "','" + Convert.ToDateTime(loadtime).ToString("yyyy-MM-dd HH:mm:ss") + "','INT','N','" + Session["userID"].ToString() + "',GETDATE(),'" + grade.Trim() + "')";
                        //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        lstQry.Add(strsql);

                        strsql = "update GPIL_STOCK set STATUS='INT',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + diswt + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        lstQry.Add(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";

                        strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "','" + Convert.ToDateTime(loadtime).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        lstQry.Add(strsql);


                    }

                   bool b1= dlMgt.UpdateUsingExecuteNonQueryList(lstQry);

                    if (b1)
                    {

                        string SPName = "GPIL_SP_DISPATCH";
                        List<SqlParameter> parameters = new List<SqlParameter>();
                        SqlParameter[] pram = new SqlParameter[3];
                        pram[0] = (new SqlParameter("@DISPATCHNO", SqlDbType.NVarChar, 50));
                        pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                        pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                        pram[0].Value = temphdr;
                        pram[1].Direction = ParameterDirection.Output;
                        pram[2].Direction = ParameterDirection.Output;
                        for (int ii = 0; ii < pram.Length; ii++)
                        {
                            parameters.Add(pram[ii]);
                        }
                        //retVal = "/" + retVal + "||" + dlMgt.SP_ExecuteNonQuery(parameters, SPName);
                        VerificationManagement vMgt = new VerificationManagement();
                        vMgt.SP_ExecuteNonQuery(parameters, SPName);
                        if (Convert.ToString(parameters[1].Value) == "1")
                        {
                            retVal = "Success: " + Convert.ToString(parameters[2].Value);
                        }
                        else
                        {
                            retVal = "Error: " + Convert.ToString(parameters[2].Value);
                        }

                       

                    }


                    else
                    {
                        data = "Please check the dispatch main procedure";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    //return retVal;

                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
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
        }



        /// <summary>
        /// Receipt Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportRFromExcel(HttpPostedFileBase postedFile)
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
            return View("ReceiptLoaderIndex");
        }

        [HttpPost]
        public JsonResult ReceiptComplete(ListReceipt LR)
        {
            //Receiptdata(LR);

            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;

            string recerror = string.Empty;
            int i = 0;
            recerror = "Error :";
            DataSet purdata = new DataSet();




            try
            {
                DataLoaderManagement dlMgt = new DataLoaderManagement();

                DataTable orgdata = new DataTable();
                DataTable dtclstr = ToDataTable(LR.Receipts);
                var od = from s in LR.Receipts
                         group s by new { s.SHIPMENT_NO } into newgroup
                         select new
                         {
                             SHIPMENT_NO = newgroup.Key.SHIPMENT_NO

                         };
                var ods = od.ToList();

                orgdata.Columns.Add("SHIPMENT_NO");

                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SHIPMENT_NO);
                }


                //Purchase Data

                // DataSet purdata = new DataSet();
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SHIPMENT_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("SHIPMENT_NO");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    // purdata.Tables[s].Columns.Add("RECEIPT_WT");
                    purdata.Tables[s].Columns.Add("UNLOADING_DATETIME");


                    DataRow[] purrows = dtclstr.Select("SHIPMENT_NO ='" + orgdata.Rows[s]["SHIPMENT_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }

                //Insert Info
                //-----------------valiadtion-----------
                try
                {

                    for (int d = 0; d < purdata.Tables.Count; d++)
                    {
                        string shipno1 = purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString();

                        for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                        {
                            string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                            if (baleno1 == "")
                            {
                                data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }

                            else
                            {
                                strsql = "select D.GPIL_BALE_NUMBER,D.MARKED_WT from GPIL_SHIPMENT_DTLS D,GPIL_SHIPMENT_HDR H WHERE D.SHIPMENT_NO=H.SHIPMENT_NO AND H.SHIPMENT_NO='" + shipno1 + "' AND D.GPIL_BALE_NUMBER='" + baleno1 + "' AND H.STATUS='INT'  AND D.STATUS='INT'";
                                DataTable ds1 = new DataTable();
                                ds1 = dlMgt.GetQueryResult(strsql);
                                if (ds1.Rows.Count == 0)
                                {
                                    data = "Error: Batch Does not Esists in this shipment no for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    data = "Error: " + ex.Message;
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                finally
                {

                    // purdata.Clear();

                }



                List<string> lstQry = new List<string>();


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    //int cnt = 0;
                    string recvorg = "TT";


                    /// Generating Header Id
                    strsql = "select RECEIVER_ORGN_CODE FROM GPIL_SHIPMENT_HDR WHERE SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);

                    if (ds1.Rows.Count > 0)
                    {
                        recvorg = Convert.ToString(ds1.Rows[0][0]);
                    }


                    string SHIPNO = purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString();
                    strsql = "UPDATE GPIL_SHIPMENT_HDR SET STATUS='C' WHERE SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";
                    //dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    lstQry.Add(strsql);


                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();

                        // string loadtime = purdata.Tables[d].Rows[h]["UNLOADING_DATETIME"].ToString();
                        string recevwt = "0";

                        strsql = "select DISPATCH_WEIGHT from GPIL_SHIPMENT_DTLS where GPIL_BALE_NUMBER ='" + baleno + "' and SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            recevwt = Convert.ToString(ds1.Rows[0][0]);
                        }




                        strsql = "update GPIL_SHIPMENT_DTLS set RECEIPT_WEIGHT='" + recevwt + "',STATUS='RCV',HEADER_STATUS='N',UNLOADING_DATETIME=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno + "' and SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";

                        //dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        lstQry.Add(strsql);




                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + recvorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','RECEPIT','" + SHIPNO.Trim() + "','" + recevwt.Trim() + "','" + recvorg.Trim() + "',GETDATE(),'N')";

                        // dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        lstQry.Add(strsql);

                    }

                    bool b1 = dlMgt.UpdateUsingExecuteNonQueryList(lstQry);

                    if (b1)
                    {
                        string SPName = "GPIL_SP_RECEIPT";
                        List<SqlParameter> parameters = new List<SqlParameter>();
                        SqlParameter[] pram = new SqlParameter[4];
                        pram[0] = (new SqlParameter("@RECEIVEDSHIPMENTNO", SqlDbType.NVarChar, 50));
                        pram[1] = (new SqlParameter("@RECEIVEDBY", SqlDbType.NVarChar, 20));
                        pram[2] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                        pram[3] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                        pram[0].Value = SHIPNO;
                        pram[1].Value = Session["userID"].ToString();
                        pram[2].Direction = ParameterDirection.Output;
                        pram[3].Direction = ParameterDirection.Output;
                        for (int ii = 0; ii < pram.Length; ii++)
                        {
                            parameters.Add(pram[ii]);
                        }
                        string retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);
                    }
                    else
                    {
                        data = "Error: Please check the input values";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
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
        }
        /// <summary>
        /// Sales Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesTransactionLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportSTFromExcel(HttpPostedFileBase postedFile)
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
            return View("SalesTransactionLoaderIndex");
        }


        [HttpPost]
        public JsonResult SalesTransactionComplete(ListSales LS)
        {

            try
            {
                string Farmererror = string.Empty;
                String strsql = string.Empty;
                DataLoaderManagement dlMgt = new DataLoaderManagement();

                DataTable orgdata = new DataTable();
                DataTable dtclstr = ToDataTable(LS.SalesTransactions);
                var od = from s in LS.SalesTransactions
                         group s by new { s.SHIPMENT_NO } into newgroup
                         select new
                         {
                             SHIPMENT_NO = newgroup.Key.SHIPMENT_NO


                         };
                var ods = od.ToList();

                orgdata.Columns.Add("SHIPMENT_NO");

                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SHIPMENT_NO);
                }

                //PurchaseData

                DataSet purdata = new DataSet();
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SHIPMENT_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("SHIPMENT_NO");
                    purdata.Tables[s].Columns.Add("SENDER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("RECEIVER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("SEND_BY");
                    purdata.Tables[s].Columns.Add("SENDER_TRUCK_NO");
                    purdata.Tables[s].Columns.Add("RC_NO");
                    purdata.Tables[s].Columns.Add("DRIVER_NAME");
                    purdata.Tables[s].Columns.Add("DRIVING_LICENCE_NO");
                    purdata.Tables[s].Columns.Add("TRANSPORT_NAME");

                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");



                    // purdata.Tables[s].Columns.Add("DISPATCH_WT");
                    // purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("LOADING_DATETIME");


                    DataRow[] purrows = dtclstr.Select("SHIPMENT_NO ='" + orgdata.Rows[s]["SHIPMENT_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }



                //----------Validation----------

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();

                        //GRADE,SUBINVENTORY_CODE
                        string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string subinventory = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();


                        //  string buyergrade1 = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        //string netwt1 = purdata.Tables[d].Rows[h]["DISPATCH_WT"].ToString();
                        //string subinventorycd1 = purdata.Tables[d].Rows[h]["FROM_SUBINVENTORY_CODE"].ToString();
                        string currorg = purdata.Tables[d].Rows[h]["SENDER_ORG_CODE"].ToString();


                        if (baleno1 == "")
                        {

                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {
                            string orgntype = "";
                            strsql = "Select ORGN_TYPE from GPIL_ORGN_MASTER where ORGN_CODE='" + currorg + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {
                                orgntype = Convert.ToString(ds1.Rows[0][0]);
                            }


                            strsql = "select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE,GRADE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno1 + "' and CURR_ORGN_CODE='" + currorg + "' AND STATUS='Y'";

                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {
                                string srksubcd, strGrade;
                                mrkdwt = Convert.ToDouble(ds1.Rows[0][0]);
                                processts = Convert.ToString(ds1.Rows[0][1]);
                                srksubcd = Convert.ToString(ds1.Rows[0][2]);
                                strGrade = Convert.ToString(ds1.Rows[0][3]);

                                if (processts == "Y")
                                {

                                    data = "Error: Bale Is using in another process Bale Number--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }

                                if (grade != strGrade)
                                {

                                    data = "Error: Grade is not match with inventory, Bale Number--" + baleno1 + " & Inventory Grade --" + strGrade.ToString();
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }

                                if (subinventory != srksubcd)
                                {

                                    data = "Error: Sub-Inventory is not match with inventory, Bale Number--" + baleno1 + " & Current Sub-Inventory --" + srksubcd.ToString();
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {

                                }


                            }
                            else
                            {

                                data = "Error: BaleNumber Does not exists Bale Number--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }


                        }
                    }

                }



                //------------Validation----------
                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string sendorg = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString();
                    string recevorg = purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString();
                    string sendby = purdata.Tables[d].Rows[0]["SEND_BY"].ToString();
                    string truckno = purdata.Tables[d].Rows[0]["SENDER_TRUCK_NO"].ToString();
                    string rcno = purdata.Tables[d].Rows[0]["RC_NO"].ToString();
                    string drivername = purdata.Tables[d].Rows[0]["DRIVER_NAME"].ToString();
                    string licenceno = purdata.Tables[d].Rows[0]["DRIVING_LICENCE_NO"].ToString();
                    string transportname = purdata.Tables[d].Rows[0]["TRANSPORT_NAME"].ToString();
                    string senddate = purdata.Tables[d].Rows[0]["LOADING_DATETIME"].ToString();
                    string sndorgty = "";
                    string rcvorgtyp = "";


                    strsql = "Select O.ORGN_TYPE,O2.ORGN_TYPE FROM GPIL_ORGN_MASTER O,GPIL_ORGN_MASTER O2 WHERE O.ORGN_CODE='" + sendorg + "' AND O2.ORGN_CODE='" + recevorg + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {

                        //lvarStrTotalLoan = Convert.ToDouble(ds1.Rows[0][1]).ToString();
                        sndorgty = Convert.ToString(ds1.Rows[0][0]);
                        rcvorgtyp = Convert.ToString(ds1.Rows[0][1]);
                    }


                    strsql = "INSERT INTO [GPIL_SO_RESERVATION_HDR_TEMP]([SHIPMENT_NO],[SENDER_ORGN_CODE],[RECEIVER_ORGN_CODE],[SENDER_DATE],[SENT_BY],[SENDER_TRUCK_NO] ,[RC_NO],[DRIVER_NAME],[DRIVING_LICENCE_NO],[TRANSPORT_NAME],[FRIEGHT_CHARGES],[UOM],[CREATED_BY],[CREATED_DATE],[STATUS],[ATTRIBUTE2],[ATTRIBUTE3],TOT_NO_OF_BALES)";
                    strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "','" + Convert.ToDateTime(senddate) + "','" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','0','KG','" + Session["userID"].ToString() + "',GETDATE(),'INT','" + sndorgty + "','" + rcvorgtyp + "'," + purdata.Tables[d].Rows.Count + ")";
                    //strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "',GETDATE(),'" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','" + frightchrgs + "','KG','"+Propertycls.EMPCODE+"',GETDATE(),'INT')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        // string diswt = purdata.Tables[d].Rows[h]["DISPATCH_WT"].ToString();
                        string fromsubcode = ""; // purdata.Tables[d].Rows[h]["FROM_SUBINVENTORY_CODE"].ToString();
                        string tosubcode = ""; // purdata.Tables[d].Rows[h]["TO_SUBINVENTORY_CODE"].ToString();
                                               // string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                                               // string loadtime = purdata.Tables[d].Rows[h]["LOADING_DATETIME"].ToString();
                        string loadtime = purdata.Tables[d].Rows[h]["LOADING_DATETIME"].ToString();
                        string markedwt = "0";
                        string diswt = "0";
                        string grade = string.Empty;
                        string orgntype = string.Empty;

                        strsql = "Select ORGN_TYPE from GPIL_ORGN_MASTER where ORGN_CODE='" + sendorg + "'";

                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            orgntype = Convert.ToString(ds1.Rows[0][0]);
                        }


                        if (orgntype == "TAP")
                        {
                            strsql = "select MARKED_WT,PROCESS_STATUS,CURR_WT,BUYER_GRADE,SUBINVENTORY_CODE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        }
                        else
                        {
                            strsql = "select MARKED_WT,PROCESS_STATUS,CURR_WT,GRADE,SUBINVENTORY_CODE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        }

                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            markedwt = Convert.ToString(ds1.Rows[0][0]);
                            diswt = Convert.ToString(ds1.Rows[0][2]);
                            grade = Convert.ToString(ds1.Rows[0][3]);
                            fromsubcode = Convert.ToString(ds1.Rows[0][4]);
                            tosubcode = fromsubcode;
                        }


                        strsql = "INSERT INTO [GPIL_SO_RESERVATION_DTLS_TEMP]([SHIPMENT_NO],[DETAIL_ID],[GPIL_BALE_NUMBER],[MARKED_WT],[DISPATCH_WEIGHT],[FROM_SUBINVENTORY_CODE],[TO_SUBINVENTORY_CODE],[LOADING_DATETIME],[STATUS],[HEADER_STATUS],[CREATED_BY],[CREATED_DATE],[GRADE])";

                        strsql = strsql + "Values('" + temphdr + "','SH" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','" + markedwt.Trim() + "','" + diswt.Trim() + "','" + fromsubcode.Trim() + "','" + tosubcode.Trim() + "','" + Convert.ToDateTime(loadtime) + "','INT','N','" + Session["userID"].ToString() + "',GETDATE(),'" + grade.Trim() + "')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set ATTRIBUTE2='" + sendorg + "',ATTRIBUTE3='" + recevorg + "',STATUS='INT',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + diswt + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";

                        strsql = strsql + "Values('SA" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','SALES','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "','" + Convert.ToDateTime(loadtime).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    }




                    string SPName = "GPIL_SP_SALES";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[3];
                    pram[0] = (new SqlParameter("@DISPATCHNO", SqlDbType.NVarChar, 50));
                    pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                    pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                    pram[0].Value = temphdr;
                    pram[1].Direction = ParameterDirection.Output;
                    pram[2].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    string retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);




                }
                //}

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                data = "Error :" + ex.Message;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }


        }


        /// <summary>
        /// Classification Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportClsFromExcel(HttpPostedFileBase postedFile)
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
            return View("ClassificationLoaderIndex");
        }

        DataTable orgdata = new DataTable();
        DataSet purdata = new DataSet();
        double mrkdwt;
        string processts;
        [HttpPost]
        public JsonResult ClassificationComplete(ListClassificationLoader LC)
        {
            string retVal = string.Empty;

            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LC.Classifications);
                var od = from s in LC.Classifications
                         group s by new { s.ORGN_CODE, s.BATCH_NO } into newgroup
                         select new
                         {
                             ORGN_CODE = newgroup.Key.ORGN_CODE,
                             BATCH_NO = newgroup.Key.BATCH_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("ORGN_CODE");
                orgdata.Columns.Add("BATCH_NO");
                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.ORGN_CODE, rowObj.BATCH_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["ORGN_CODE"].ToString() + orgdata.Rows[s]["BATCH_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("CLASSIFIER_CODE");
                    purdata.Tables[s].Columns.Add("CLASSIFICATION_DATE");
                    purdata.Tables[s].Columns.Add("RECIPE_CODE");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("CLASSIFICATION_GRADE");


                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "' AND ORGN_CODE='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow row1 in purrows)
                        {
                            purdata.Tables[s].ImportRow(row1);
                        }
                    }

                }

                //------Validation-----------
                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string orgcode1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        // string issuegrade1 = purdata.Tables[d].Rows[h]["ISSUED_GRADE"].ToString();
                        string classifygrade1 = purdata.Tables[d].Rows[h]["CLASSIFICATION_GRADE"].ToString();
                        // string wtbefclassify1 = purdata.Tables[d].Rows[h]["WEIGHT_BEFORE_CLASSIFICATION"].ToString();
                        //  string wtaftclassify1 = purdata.Tables[d].Rows[h]["WEIGHT_AFTER_CLASSIFICATION"].ToString();


                        if (baleno1.Substring(0, 2) != classifygrade1.Substring(0, 2))
                        {

                            data = "Error: Classified Grade Crop Year  and BaleNumber Crop year MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1.Substring(2, 2) != classifygrade1.Substring(2, 2))
                        {

                            data = "Error: Classified Grade Variety and BaleNumber Variety MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1 == "")
                        {

                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (classifygrade1 == "")
                        {

                            data = "Error: Classified Grade is empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {
                            strsql = "select MARKED_WT,PROCESS_STATUS from GPIL_STOCK where GRADE IS NULL AND GPIL_BALE_NUMBER='" + baleno1 + "' AND CURR_ORGN_CODE='" + orgcode1 + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {
                                mrkdwt = Convert.ToDouble(ds1.Rows[0][0]);
                                processts = Convert.ToString(ds1.Rows[0][1]);
                                if (processts == "Y")
                                {

                                    data = "Error: Bale Is been using for anoter process BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {

                                    strsql = "select * from GPIL_ITEM_MASTER where ITEM_CODE ='" + classifygrade1.Trim() + "'";
                                    ds1 = dlMgt.GetQueryResult(strsql);
                                    if (ds1.Rows.Count != 0)
                                    {

                                    }
                                    else
                                    {

                                        data = "Error: Classified Grade Doesnot Exist in master BaleNumber--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                    }

                                }

                            }
                            else
                            {

                                data = "Error: Bale Is not available in current orginization BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }


                        }


                    }

                }
                //-----------Validation----------

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + "CL" + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string orgcode = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string classifiercd = purdata.Tables[d].Rows[0]["CLASSIFIER_CODE"].ToString();
                    string classdate = purdata.Tables[d].Rows[0]["CLASSIFICATION_DATE"].ToString();

                    string recipecode = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    strsql = "INSERT INTO [GPIL_CLASSIFICATION_HDR_TEMP]([BATCH_NO],[ORGN_CODE],[CLASSIFIER_NAME],[CLASSIFICATION_DATE],[REASONING_CODE],[RECIPE_CODE],[CREATED_BY],[CREATED_DATE],[STATUS])";
                    strsql = strsql + " VALUES('" + temphdr + "','" + orgcode + "','" + classifiercd + "','" + Convert.ToDateTime(classdate) + "','0','" + recipecode + "','" + Session["userID"].ToString() + "',GETDATE(),'C')";

                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string classifygrade = purdata.Tables[d].Rows[h]["CLASSIFICATION_GRADE"].ToString();
                        string issuegrade = string.Empty;
                        string markedwt = "0";
                        string wtbefclassify = "0";
                        strsql = "select MARKED_WT,PROCESS_STATUS,BUYER_GRADE,CURR_WT from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        DataTable ds1 = new DataTable();
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count != 0)
                        {
                            markedwt = Convert.ToString(ds1.Rows[0][0]);
                            issuegrade = Convert.ToString(ds1.Rows[0][2]);
                            wtbefclassify = Convert.ToString(ds1.Rows[0][3]);
                        }

                        string dtlid = "CL" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + h;

                        strsql = "INSERT INTO [GPIL_CLASSIFICATION_DTLS_TEMP]([BATCH_NO],[DETAIL_ID],[GPIL_BALE_NUMBER],[ISSUED_GRADE],[CLASSIFICATION_GRADE],[MARKED_WT],[WEIGHT_BEFORE_CLASSIFY],[WEIGHT_AFTER_CLASSIFICATION],[FROM_SUBINVENTORY_CODE],[TO_SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                        strsql = strsql + "Values('" + temphdr + "','CL" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','" + issuegrade.Trim() + "','" + classifygrade.Trim() + "','" + markedwt.Trim() + "','" + wtbefclassify.Trim() + "','" + wtbefclassify.Trim() + "','CL','CL','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set GRADE='" + classifygrade + "',PROCESS_STATUS='N',BATCH_NO='" + temphdr + "',CURR_WT='" + wtbefclassify + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','CLASSIFICATION','" + temphdr.Trim() + "','" + wtbefclassify.Trim() + "','" + orgcode.Trim() + "','" + Convert.ToDateTime(classdate).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";

                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    }



                    string SPName = "Classificationprocess";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[1];
                    pram[0] = (new SqlParameter("@sts", SqlDbType.VarChar, 50));
                    pram[0].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);
                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }

        /// <summary>
        /// GRADING ISSUE LOADER
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingIssueLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportGIFromExcel(HttpPostedFileBase postedFile)
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
            return View("GradingIssueLoaderIndex");
        }

        string temprefno = string.Empty;
        string grdisserror = string.Empty;
        [HttpPost]
        public JsonResult GradingIssueComplete(ListGradingIssueLoader LG)
        {


            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LG.GradingIssues);
                var od = from s in LG.GradingIssues
                         group s by new { s.ORGN_CODE, s.BATCH_NO } into newgroup
                         select new
                         {
                             ORGN_CODE = newgroup.Key.ORGN_CODE,
                             BATCH_NO = newgroup.Key.BATCH_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("ORGN_CODE");
                orgdata.Columns.Add("BATCH_NO");
                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.ORGN_CODE, rowObj.BATCH_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["ORGN_CODE"].ToString() + orgdata.Rows[s]["BATCH_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("SUPERVISOR_CODE");
                    purdata.Tables[s].Columns.Add("RECIPE_CODE");
                    purdata.Tables[s].Columns.Add("DATE_OF_OPERATION");
                    purdata.Tables[s].Columns.Add("ISSUED_GRADE");
                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");

                    //purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("ASCERTAIN_WEIGHT");
                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");

                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "' AND ORGN_CODE='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }



                //InsertData


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string orgcode1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string supervisorcd1 = purdata.Tables[d].Rows[0]["SUPERVISOR_CODE"].ToString();
                    string recipecode1 = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    string dateoroper1 = purdata.Tables[d].Rows[0]["DATE_OF_OPERATION"].ToString();
                    string issuegrd1 = purdata.Tables[d].Rows[0]["ISSUED_GRADE"].ToString();
                    string crop1 = purdata.Tables[d].Rows[0]["CROP"].ToString();
                    string variety1 = purdata.Tables[d].Rows[0]["VARIETY"].ToString();


                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string ascertainwt1 = purdata.Tables[d].Rows[h]["ASCERTAIN_WEIGHT"].ToString();
                        string subinvcd1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();

                        if (baleno1.Substring(0, 2) != crop1)
                        {

                            data = "Error: BaleNumber Crop year MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1.Substring(2, 2) != variety1)
                        {
                            data = "Error: BaleNumber Variety MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        if (issuegrd1.Substring(0, 2) != crop1)
                        {
                            data = "Error: Issue Grade Crop Year MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (issuegrd1.Substring(2, 2) != variety1)
                        {
                            data = "Error: Issue Grade Variety MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }

                        else if (baleno1 == "")
                        {
                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                            //GradingIssueUpdate(baleno1, "N");
                            //i = i + 1;
                        }

                        else
                        {

                            strsql = "select MARKED_WT,PROCESS_STATUS,GRADE from GPIL_STOCK where GRADE IS NOT NULL AND GPIL_BALE_NUMBER='" + baleno1 + "' AND CURR_ORGN_CODE='" + orgcode1 + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {
                                mrkdwt = Convert.ToDouble(ds1.Rows[0][0]);
                                processts = Convert.ToString(ds1.Rows[0][1]);
                                if (Convert.ToString(ds1.Rows[0][1]).Trim() == issuegrd1.Trim())
                                {

                                    data = "Error: Issue Grade and Bale Grade MisMatch BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else if (processts == "Y")
                                {
                                    data = "Error: Bale is already been using by another process BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {

                                    if (recipecode1 != "BITS CLEANING" && recipecode1 != "DAMAGE CLEANING")
                                    {
                                        strsql = "SELECT * FROM GPIL_CLASSIFY_SUFFIX where RECIPE_CODE='" + recipecode1 + "' and SUFFIX_CODE='" + issuegrd1.Substring(issuegrd1.IndexOf('-') + 1) + "'";
                                        ds1 = dlMgt.GetQueryResult(strsql);
                                        if (ds1.Rows.Count > 0)
                                        {
                                            //GradingIssueUpdate(baleno1, "Y");
                                        }
                                        else
                                        {
                                            data = "Error: Issue Grade And Recipe code MisMatch BaleNumber--" + baleno1;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;
                                        }
                                    }
                                    else
                                    {
                                        //GradingIssueUpdate(baleno1, "Y");
                                        //i = i + 1;
                                    }

                                }

                            }
                            else
                            {
                                data = "Error: Bale Is not available in current orginization/Not Yet Classified BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }
                        }


                    }

                }

                //---------------Validation---------

                temprefno = string.Empty;
                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + "GR" + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string orgcode = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string supervisercd = purdata.Tables[d].Rows[0]["SUPERVISOR_CODE"].ToString();
                    string recipecode = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    string dateofoper = purdata.Tables[d].Rows[0]["DATE_OF_OPERATION"].ToString();
                    string issuegrd = purdata.Tables[d].Rows[0]["ISSUED_GRADE"].ToString();
                    string crop = purdata.Tables[d].Rows[0]["CROP"].ToString();
                    string variety = purdata.Tables[d].Rows[0]["VARIETY"].ToString();

                    strsql = "INSERT INTO [GPIL_GRADING_HDR_TEMP]([BATCH_NO],[ORGN_CODE],[SUPERVISOR_NAME],[RECIPE_CODE],[DATE_OF_OPERATION],[ISSUED_GRADE],[CROP],[VARIETY],[CREATED_BY],[CREATED_DATE],[STATUS])";
                    strsql = strsql + " VALUES('" + temphdr + "','" + orgcode + "','" + supervisercd + "','" + recipecode.Trim() + "','" + Convert.ToDateTime(dateofoper).ToString("yyyy-MM-dd") + "','" + issuegrd + "','" + crop + "','" + variety + "','" + Session["userID"].ToString() + "',GETDATE(),'I')";

                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    temprefno = temprefno + Environment.NewLine + "Batch No- " + temphdr + " / Grade- " + issuegrd + " / Recipe- " + recipecode;
                    int h = 0;
                    double totqty = 0;
                    for (h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        //baleno = "1520P56001766";
                        string ascertainwt1 = purdata.Tables[d].Rows[h]["ASCERTAIN_WEIGHT"].ToString();
                        string subinvcd1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();
                        issuegrd = purdata.Tables[d].Rows[h]["ISSUED_GRADE"].ToString();
                        string grade1 = string.Empty;
                        string markedwt = "0";
                        strsql = "select MARKED_WT,PROCESS_STATUS,GRADE,CURR_WT from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        DataTable ds1 = new DataTable();
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            markedwt = Convert.ToString(ds1.Rows[0][0]);
                            grade1 = Convert.ToString(ds1.Rows[0][2]);

                        }
                        totqty = totqty + Convert.ToDouble(markedwt);


                        if (grade1 == issuegrd)
                        {
                        }
                        else
                        {

                            data = "Error: Issue Grade and Bale Grade Does not Match";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }

                        strsql = "INSERT INTO [GPIL_GRADING_DTLS_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT] ,[SUBINVENTORY_CODE],[CREATED_BY] ,[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                        strsql = strsql + "Values('" + temphdr + "','GR" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','IPB','G','" + baleno.Trim() + "','" + grade1.Trim() + "','" + markedwt.Trim() + "','" + ascertainwt1.Trim() + "','" + subinvcd1.Trim() + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','I')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set STATUS='N',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + ascertainwt1 + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','GRADING','" + temphdr.Trim() + "','" + ascertainwt1.Trim() + "','" + orgcode.Trim() + "','" + Convert.ToDateTime(dateofoper).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    }
                    strsql = "update GPIL_GRADING_HDR_TEMP set TOT_ISSUE_BALES='" + h + "',TOT_ISSUE_QTY='" + totqty + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE BATCH_NO='" + temphdr.Trim() + "'";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);


                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }





        /// <summary>
        /// GRADING OUTTURN LOADER
        /// </summary>
        /// <returns></returns>
        public ActionResult GradingOutturnLoaderIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportGUFromExcel(HttpPostedFileBase postedFile)
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
            return View("GradingOutturnLoaderIndex");
        }



        [HttpPost]

        public JsonResult GradingOutturnComplete(ListGradingOutturnLoader LGO)
        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LGO.GradingOutturns);
                var od = from s in LGO.GradingOutturns
                         group s by new { s.BATCH_NO } into newgroup
                         select new
                         {
                             BATCH_NO = newgroup.Key.BATCH_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("BATCH_NO");
                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.BATCH_NO);
                }



                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["BATCH_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("BALE_TYPE");
                    purdata.Tables[s].Columns.Add("PRODUCT_TYPE");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("WEIGHT");
                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");
                    purdata.Tables[s].Columns.Add("NO_OF_GRADERS");

                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }
                //---------------Vaildation-----------
                //InsertData

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string batchno1 = purdata.Tables[d].Rows[0]["BATCH_NO"].ToString();
                    string noofgraders1 = purdata.Tables[d].Rows[0]["NO_OF_GRADERS"].ToString();

                    string crop1 = string.Empty;
                    string variety1 = string.Empty;
                    string orgcode1 = string.Empty;
                    double totissueqty1 = 0;

                    strsql = "select CROP,VARIETY,ORGN_CODE,TOT_ISSUE_QTY from GPIL_GRADING_HDR_TEMP where BATCH_NO='" + batchno1 + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {
                        crop1 = Convert.ToString(ds1.Rows[0][0]);
                        variety1 = Convert.ToString(ds1.Rows[0][1]);
                        orgcode1 = Convert.ToString(ds1.Rows[0][2]);
                        totissueqty1 = Convert.ToDouble(ds1.Rows[0][3]);
                    }



                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string baletype1 = purdata.Tables[d].Rows[h]["BALE_TYPE"].ToString();
                        string producttype1 = purdata.Tables[d].Rows[h]["PRODUCT_TYPE"].ToString();
                        string grade1 = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string weight1 = purdata.Tables[d].Rows[h]["WEIGHT"].ToString();
                        string subinvcd1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();



                        if (baleno1.Substring(0, 2) != crop1)
                        {

                            data = "Error: Bale Number  and Corp Year MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1.Substring(2, 2) != variety1)
                        {

                            data = "Error: Bale Number  and Variety MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        if (grade1.Substring(0, 2) != crop1)
                        {

                            data = "Error: Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (grade1.Substring(2, 2) != variety1)
                        {

                            data = "Error: Grade and Variety MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1.Substring(4, 3) != orgcode1)
                        {

                            data = "Error: Bale Number  and Orginization MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1 == "")
                        {

                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (weight1 == "")
                        {

                            data = "Error: Weight is Empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (grade1 == "")
                        {

                            data = "Error: Grade is Empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {

                            strsql = "select * from GPIL_GRADING_HDR_TEMP where STATUS='I' and BATCH_NO='" + batchno1 + "'";
                            DataTable ds2 = new DataTable();
                            ds2 = dlMgt.GetQueryResult(strsql);
                            if (ds2.Rows.Count > 0)
                            {

                                strsql = "SELECT * FROM GPIL_STOCK WHERE GPIL_BALE_NUMBER='" + baleno1.Trim() + "'";
                                DataTable ds3 = new DataTable();
                                ds3 = dlMgt.GetQueryResult(strsql);
                                if (ds3.Rows.Count > 0)
                                {

                                    data = "Error: Bale Already Exists in stock BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {

                                    strsql = "select * from GPIL_ITEM_MASTER where ITEM_CODE ='" + grade1.Trim() + "'";
                                    DataTable ds4 = new DataTable();
                                    ds4 = dlMgt.GetQueryResult(strsql);
                                    if (ds4.Rows.Count > 0)
                                    {

                                    }
                                    else
                                    {

                                        data = "Error: Grade does not exists for BaleNumber--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;
                                    }

                                }

                            }
                            else
                            {

                                data = "Error: Batch Already Closed for BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }


                        }


                    }


                }






                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id

                    string batchno = purdata.Tables[d].Rows[0]["BATCH_NO"].ToString();
                    string noofgraders = purdata.Tables[d].Rows[0]["NO_OF_GRADERS"].ToString();

                    string crop = string.Empty;
                    string variety = string.Empty;
                    string orgcode = string.Empty;
                    double totissueqty = 0;

                    strsql = "select CROP,VARIETY,ORGN_CODE,TOT_ISSUE_QTY from GPIL_GRADING_HDR_TEMP where BATCH_NO='" + batchno + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {
                        crop = Convert.ToString(ds1.Rows[0][0]);
                        variety = Convert.ToString(ds1.Rows[0][1]);
                        orgcode = Convert.ToString(ds1.Rows[0][2]);
                        totissueqty = Convert.ToDouble(ds1.Rows[0][3]);
                    }

                    int h = 0;
                    double totqty = totissueqty;
                    double totoutqty = 0;
                    for (h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {

                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string baletype = purdata.Tables[d].Rows[h]["BALE_TYPE"].ToString();
                        string producttype = purdata.Tables[d].Rows[h]["PRODUCT_TYPE"].ToString();
                        string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string weight = purdata.Tables[d].Rows[h]["WEIGHT"].ToString();
                        string subinvcd = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();
                        totoutqty = 0;
                        totqty = totqty - Convert.ToDouble(weight);
                        if (totqty < 0)
                        {
                            //throw new Exception("Output Quantity Is More Than Issued Quantity");
                            data = "Error: Output Quantity Is More Than Issued Quantity";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        totoutqty = totoutqty + Convert.ToDouble(weight);

                        strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                        strsql = strsql + "Values('" + baleno.Trim() + "','" + subinvcd + "','" + grade + "','" + weight.Trim() + "','" + weight.Trim() + "','LOC1','" + orgcode.Trim() + "','" + crop + "','" + variety + "','" + producttype + "','N','" + batchno + "','Y','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','PPD','LOC1','" + orgcode + "')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                        strsql = "INSERT INTO [GPIL_GRADING_DTLS_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT] ,[SUBINVENTORY_CODE],[CREATED_BY] ,[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                        strsql = strsql + "Values('" + batchno + "','GR" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baletype + "','" + producttype + "','" + baleno.Trim() + "','" + grade.Trim() + "','" + weight.Trim() + "','" + weight.Trim() + "','" + subinvcd.Trim() + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','I')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','GRADING','" + batchno.Trim() + "','" + weight.Trim() + "','" + orgcode.Trim() + "',GETDATE(),'N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }

                    strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                    strsql = strsql + "Values('" + batchno.Trim() + "','BYP','" + crop + variety + "LOSS','" + totqty + "','" + totqty + "','LOC1','" + orgcode.Trim() + "','" + crop + "','" + variety + "','BP','N','" + batchno + "','Y','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','PPD','LOC1','" + orgcode + "')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "INSERT INTO [GPIL_GRADING_DTLS_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT] ,[SUBINVENTORY_CODE],[CREATED_BY] ,[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                    strsql = strsql + "Values('" + batchno + "','GR" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','OPB','BP','" + batchno.Trim() + "','" + crop + variety + "LOSS','" + totqty + "','" + totqty + "','BYP','" + Session["userID"].ToString() + "',GETDATE(),'Y','I')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                    strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + batchno.Trim() + "','GRADING','" + batchno.Trim() + "','" + totqty + "','" + orgcode.Trim() + "',GETDATE(),'N')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "update GPIL_GRADING_HDR_TEMP set STATUS='C', TOT_OUTPUT_BALES='" + h + "',TOT_OUTPUT_QTY='" + totoutqty + "',NO_OF_GRADERS='" + noofgraders + "',AVG_BALES_GRADER='" + totoutqty / Convert.ToDouble(noofgraders) + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE BATCH_NO='" + batchno.Trim() + "'";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    string SPName = "GradingIssueProcess";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[1];
                    pram[0] = (new SqlParameter("@sts", SqlDbType.VarChar, 50));
                    pram[0].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);
                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }




        /// <summary>
        /// GRADE TRANSFER LOADER
        /// </summary>
        /// <returns></returns>
        public ActionResult GradeTransferLoaderIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportGTFromExcel(HttpPostedFileBase postedFile)
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
            return View("GradeTransferLoaderIndex");
        }



        [HttpPost]
        public JsonResult GradeTransferComplete(ListGradingTransferLoader LGT)
        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LGT.GradingTransfers);
                var od = from s in LGT.GradingTransfers
                         group s by new { s.BATCH_NO, s.ORGN_CODE } into newgroup
                         select new
                         {
                             BATCH_NO = newgroup.Key.BATCH_NO,
                             ORGN_CODE = newgroup.Key.ORGN_CODE

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("BATCH_NO");
                orgdata.Columns.Add("ORGN_CODE");

                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.BATCH_NO, rowObj.ORGN_CODE);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["BATCH_NO"].ToString() + orgdata.Rows[s]["ORGN_CODE"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("CLASSIFIER_CODE");
                    purdata.Tables[s].Columns.Add("GRADE_TRANSFER_DATE");
                    purdata.Tables[s].Columns.Add("RECIPE_CODE");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    // purdata.Tables[s].Columns.Add("ISSUED_GRADE");
                    purdata.Tables[s].Columns.Add("TRANSFERED_GRADE");
                    //   purdata.Tables[s].Columns.Add("ASCERTAIN_WT");


                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "' AND ORGN_CODE='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    // lblMessage.Text = "one";
                    // int len=purrows.Length;
                    //lblMessage.Text = len.ToString();

                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }


                //---------Validation-----------
                //InsertData
                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string orgcode1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {

                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        // string issuegrade1 = purdata.Tables[d].Rows[h]["ISSUED_GRADE"].ToString();
                        string classifygrade1 = purdata.Tables[d].Rows[h]["TRANSFERED_GRADE"].ToString();
                        // string wtbefclassify1 = purdata.Tables[d].Rows[h]["ASCERTAIN_WT"].ToString();
                        // string wtaftclassify1 = purdata.Tables[d].Rows[h]["WEIGHT_AFTER_CLASSIFICATION"].ToString();


                        if (baleno1.Substring(0, 2) != classifygrade1.Substring(0, 2) && classifygrade1.Substring(0, 1) != "L")
                        {

                            data = "Error: Classified Grade Crop Year  and BaleNumber Crop year MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1.Substring(2, 2) != classifygrade1.Substring(2, 2) && classifygrade1.Substring(0, 1) != "L")
                        {

                            data = "Error: Classified Grade Variety and BaleNumber Variety MisMatch BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (baleno1 == "")
                        {
                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (classifygrade1 == "")
                        {

                            data = "Error: Classified Grade is Empty BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {
                            strsql = "select MARKED_WT,PROCESS_STATUS from GPIL_STOCK where GRADE IS NOT NULL AND GPIL_BALE_NUMBER='" + baleno1 + "' AND CURR_ORGN_CODE='" + orgcode1 + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);

                            if (ds1.Rows.Count > 0)
                            {
                                mrkdwt = Convert.ToDouble(ds1.Rows[0][0]);
                                processts = Convert.ToString(ds1.Rows[0][1]);
                                if (processts == "Y")
                                {

                                    data = "Error: Bale Is been using for anoter process BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {
                                    //GradeTransferUpdate(baleno1, "Y");
                                }

                            }
                            else
                            {
                                data = "Error: Bale Is not available in current orginization/Not Yet Classified BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }


                        }


                    }

                }



                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + "GT" + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string orgcode = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string classifiercd = purdata.Tables[d].Rows[0]["CLASSIFIER_CODE"].ToString();
                    string classdate = purdata.Tables[d].Rows[0]["GRADE_TRANSFER_DATE"].ToString();
                    string recipecode = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();



                    strsql = "INSERT INTO [GPIL_CLASSIFICATION_HDR_TEMP]([BATCH_NO],[ORGN_CODE],[CLASSIFIER_NAME],[CLASSIFICATION_DATE],[REASONING_CODE],[RECIPE_CODE],[CREATED_BY],[CREATED_DATE],[STATUS])";
                    strsql = strsql + " VALUES('" + temphdr + "','" + orgcode + "','" + classifiercd + "','" + Convert.ToDateTime(classdate) + "','1','" + recipecode + "','" + Session["userID"].ToString() + "',GETDATE(),'C')";

                    dlMgt.UpdateUsingExecuteNonQuery(strsql);



                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {


                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();

                        string classifygrade = purdata.Tables[d].Rows[h]["TRANSFERED_GRADE"].ToString();

                        string currwt = "0";
                        string subcd = "C";
                        string issuegrade = string.Empty;

                        string markedwt = "0";
                        strsql = "select MARKED_WT,PROCESS_STATUS,CURR_WT,SUBINVENTORY_CODE,GRADE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        DataTable ds1 = new DataTable();
                        ds1 = dlMgt.GetQueryResult(strsql);

                        if (ds1.Rows.Count >= 0)
                        {
                            markedwt = Convert.ToString(ds1.Rows[0][0]);
                            currwt = Convert.ToString(ds1.Rows[0][2]);
                            subcd = Convert.ToString(ds1.Rows[0][3]);
                            issuegrade = Convert.ToString(ds1.Rows[0][4]);
                        }

                        strsql = "INSERT INTO [GPIL_CLASSIFICATION_DTLS_TEMP]([BATCH_NO],[DETAIL_ID],[GPIL_BALE_NUMBER],[ISSUED_GRADE],[CLASSIFICATION_GRADE],[MARKED_WT],[WEIGHT_BEFORE_CLASSIFY],[WEIGHT_AFTER_CLASSIFICATION],[FROM_SUBINVENTORY_CODE],[TO_SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                        strsql = strsql + "Values('" + temphdr + "','GT" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','" + issuegrade.Trim() + "','" + classifygrade.Trim() + "','" + markedwt.Trim() + "','" + currwt.Trim() + "','" + currwt.Trim() + "','" + subcd + "','" + subcd + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set GRADE='" + classifygrade + "',PROCESS_STATUS='N',STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + currwt + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','GRADETRANSFER','" + temphdr.Trim() + "','" + currwt.Trim() + "','" + orgcode.Trim() + "','" + Convert.ToDateTime(classdate).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        //strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','GRADETRANSFER','" + temphdr.Trim() + "','" + currwt.Trim() + "','" + orgcode.Trim() + "',GETDATE(),'N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }



                    string SPName = "Classificationprocess";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[1];
                    pram[0] = (new SqlParameter("@sts", SqlDbType.VarChar, 50));
                    pram[0].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);




                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }


        string gdtrserror = string.Empty;


        /// <summary>
        /// Crop Transfer Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult CropTransferLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportCTFromExcel(HttpPostedFileBase postedFile)
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
            return View("CropTransferLoaderIndex");
        }


        [HttpPost]
        public JsonResult CropTransferComplete(ListCropTransferLoader LCT)
        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LCT.CropTransfers);
                var od = from s in LCT.CropTransfers
                         group s by new { s.BATCH_NO, s.ORGN_CODE } into newgroup
                         select new
                         {
                             BATCH_NO = newgroup.Key.BATCH_NO,
                             ORGN_CODE = newgroup.Key.ORGN_CODE

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("BATCH_NO");
                orgdata.Columns.Add("ORGN_CODE");

                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.BATCH_NO, rowObj.ORGN_CODE);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["BATCH_NO"].ToString() + orgdata.Rows[s]["ORGN_CODE"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("CLASSIFIER_CODE");

                    purdata.Tables[s].Columns.Add("RECIPE_CODE");
                    purdata.Tables[s].Columns.Add("DATE_OF_OPERATION");

                    purdata.Tables[s].Columns.Add("OLD_BALE_NUMBER");
                    // purdata.Tables[s].Columns.Add("OLD_GRADE");
                    purdata.Tables[s].Columns.Add("NEW_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("NEW_GRADE");
                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");
                    //  purdata.Tables[s].Columns.Add("WEIGHT");

                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "' AND ORGN_CODE='" + orgdata.Rows[s]["ORGN_CODE"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }

                //-------Validation-------
                for (int d = 0; d < purdata.Tables.Count; d++)
                {

                    string orgcode1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string classifiercd1 = purdata.Tables[d].Rows[0]["CLASSIFIER_CODE"].ToString();
                    string recipecode1 = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    string classdate1 = purdata.Tables[d].Rows[0]["DATE_OF_OPERATION"].ToString();


                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["OLD_BALE_NUMBER"].ToString();

                        string newbaleno1 = purdata.Tables[d].Rows[h]["NEW_BALE_NUMBER"].ToString();
                        string classifygrade1 = purdata.Tables[d].Rows[h]["NEW_GRADE"].ToString();
                        string subinvcd1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();

                        if (baleno1.Length != 13 && baleno1.Length != 31 && baleno1.Length != 14)
                        {
                            data = "Error: Invalid Old Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;

                        }
                        else if (newbaleno1.Length != 13 && newbaleno1.Length != 31)
                        {
                            data = "Error: Invalid New Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;

                        }
                        else if (baleno1 == newbaleno1)
                        {
                            data = "Error: Old Bale number and New bale number should not be same--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;


                        }
                        else if (baleno1 == "")
                        {
                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                            //CropTransferUpdate(baleno1, "N");
                            //i = i + 1;
                        }
                        else if (newbaleno1 == "")
                        {
                            data = "Error: Classified Grade is empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;

                        }
                        else if (classifygrade1 == "")
                        {
                            data = "Error: Classified Grade is empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;

                        }
                        else
                        {

                            strsql = "Select ITEM_CODE from GPIL_ITEM_MASTER where ITEM_CODE ='" + classifygrade1 + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {

                                strsql = "select MARKED_WT,PROCESS_STATUS from GPIL_STOCK where GRADE IS NOT NULL AND GPIL_BALE_NUMBER='" + baleno1 + "' AND CURR_ORGN_CODE='" + orgcode1 + "'";
                                DataTable ds2 = new DataTable();
                                ds2 = dlMgt.GetQueryResult(strsql);
                                if (ds2.Rows.Count > 0)
                                {
                                    mrkdwt = Convert.ToDouble(ds2.Rows[0][0]);
                                    processts = Convert.ToString(ds2.Rows[0][1]);
                                    if (processts == "Y")
                                    {
                                        data = "Error: Bale Is been using for anoter process BaleNumber--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;

                                    }
                                    else
                                    {

                                        strsql = "SELECT * FROM GPIL_STOCK WHERE GPIL_BALE_NUMBER='" + newbaleno1.Trim() + "'";
                                        DataTable ds3 = new DataTable();
                                        ds3 = dlMgt.GetQueryResult(strsql);
                                        if (ds3.Rows.Count > 0)
                                        {
                                            data = "Error: New BaleNumber is already exists in stock BaleNumber--" + baleno1;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;

                                        }
                                        else
                                        {

                                        }
                                    }

                                }
                                else
                                {
                                    data = "Error: Bale Is not available in current orginization/Not Yet Classified BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }

                            }
                            else
                            {


                                data = "Error: Grade not available in Item_Master--" + classifygrade1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                        }


                    }

                }
                //InsertData


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + "CT" + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string orgcode = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string classifiercd = purdata.Tables[d].Rows[0]["CLASSIFIER_CODE"].ToString();
                    string recipecode = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    string classdate = purdata.Tables[d].Rows[0]["DATE_OF_OPERATION"].ToString();

                    strsql = "INSERT INTO [GPIL_CROP_TRANS_HDR_TEMP]([BATCH_NO],[ORGN_CODE],[CLASSIFIER_NAME],[RECIPE_CODE],[DATE_OF_OPERATION],[TOT_NO_OF_BALES],[CREATED_BY],[CREATED_DATE],[STATUS])";
                    strsql = strsql + " VALUES('" + temphdr + "','" + orgcode + "','" + classifiercd + "','" + recipecode + "','" + Convert.ToDateTime(classdate).ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + Session["userID"].ToString() + "',GETDATE(),'C')";

                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["OLD_BALE_NUMBER"].ToString();

                        string newbaleno = purdata.Tables[d].Rows[h]["NEW_BALE_NUMBER"].ToString();
                        string classifygrade = purdata.Tables[d].Rows[h]["NEW_GRADE"].ToString();
                        string subinvcd = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();

                        string transfertype = "CVGT";
                        string issuegrade = string.Empty;
                        string weight = string.Empty;

                        string newCrop = string.Empty;
                        string oldCrop = string.Empty;
                        string newVariety = string.Empty;
                        string oldVariety = string.Empty;

                        if (baleno.Length == 13)
                        {
                            oldCrop = baleno.Substring(0, 2);
                            oldVariety = baleno.Substring(2, 2);
                        }
                        else if (baleno.Length == 14)
                        {
                            oldCrop = baleno.Substring(0, 2);
                            oldVariety = baleno.Substring(2, 2);
                        }
                        else
                        {
                            oldVariety = baleno.Substring(baleno.Length - 2, 2);
                            string sqlCrop = "SELECT CROP FROM GPIL_CROP_MASTER WHERE ATTRIBUTE1='" + baleno.Substring(0, 1) + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                        }
                        if (newbaleno.Length == 13)
                        {
                            newCrop = newbaleno.Substring(0, 2);
                            newVariety = newbaleno.Substring(2, 2);
                        }
                        else
                        {
                            newVariety = newbaleno.Substring(baleno.Length - 2, 2);
                            string sqlCropNew = "SELECT CROP FROM GPIL_CROP_MASTER WHERE ATTRIBUTE1='" + newbaleno.Substring(0, 1) + "'";
                            DataTable ds2 = new DataTable();
                            ds2 = dlMgt.GetQueryResult(strsql);
                        }


                        string markedwt = "0";
                        strsql = "select MARKED_WT,PROCESS_STATUS,GRADE,CURR_WT from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        DataTable ds3 = new DataTable();
                        ds3 = dlMgt.GetQueryResult(strsql);
                        if (ds3.Rows.Count > 0)
                        {
                            markedwt = Convert.ToString(ds3.Rows[0][0]);
                            issuegrade = Convert.ToString(ds3.Rows[0][2]);
                            weight = Convert.ToString(ds3.Rows[0][3]);
                        }


                        strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                        strsql = strsql + "Values('" + newbaleno.Trim() + "','" + subinvcd + "','" + classifygrade + "','" + markedwt.Trim() + "','" + markedwt.Trim() + "','LOC1','" + orgcode.Trim() + "','" + newCrop + "','" + newVariety + "','G','N','" + temphdr + "','Y','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','PPD','LOC1','" + orgcode + "')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO [GPIL_CROP_TRANS_DTLS_TEMP]([BATCH_NO],[DETAIL_ID] ,[TRANSFER_TYPE],[OLD_BALE_NUMBER],[OLD_CROP],[OLD_VARIETY],[OLD_GRADE],[MARKED_WT],[FROM_SUBINVENTORY_CODE],[NEW_BALE_NUMBER] ,[NEW_CROP],[NEW_VARIETY],[NEW_GRADE] ,[TO_SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                        strsql = strsql + "Values('" + temphdr + "','CT" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + transfertype + "','" + baleno.Trim() + "','" + oldCrop + "','" + oldVariety + "','" + issuegrade.Trim() + "','" + markedwt.Trim() + "','" + subinvcd.Trim() + "','" + newbaleno.Trim() + "','" + newCrop + "','" + newVariety + "','" + classifygrade.Trim() + "','" + subinvcd.Trim() + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set STATUS='N',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + weight + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','CROPTRANSFER','" + temphdr.Trim() + "','" + weight.Trim() + "','" + orgcode.Trim() + "','" + Convert.ToDateTime(classdate).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC1" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + newbaleno.Trim() + "','CROPTRANSFER','" + temphdr.Trim() + "','" + markedwt.Trim() + "','" + orgcode.Trim() + Convert.ToDateTime(classdate).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }
                    string SPName = "CROPTRANSFERPROCESS";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[1];
                    pram[0] = (new SqlParameter("@sts", SqlDbType.VarChar, 50));
                    pram[0].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);



                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                data = "Error: Exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }


        }


        string cptrerror = string.Empty;



        /// <summary>
        /// Threshing Issue Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreshingIssueLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportTIFromExcel(HttpPostedFileBase postedFile)
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
            return View("ThreshingIssueLoaderIndex");
        }


        [HttpPost]
        public JsonResult ThreshingIssueComplete(ListThreshingIssueLoader LTI)
        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LTI.ThreshingIssues);
                var od = from s in LTI.ThreshingIssues
                         group s by new { s.BATCH_NO } into newgroup
                         select new
                         {
                             BATCH_NO = newgroup.Key.BATCH_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("BATCH_NO");


                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.BATCH_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["BATCH_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("ORGN_CODE");
                    purdata.Tables[s].Columns.Add("RECIPE_CODE");
                    purdata.Tables[s].Columns.Add("SHIFT");
                    purdata.Tables[s].Columns.Add("SHIFT_INCHARGE");

                    purdata.Tables[s].Columns.Add("DATE_OF_OPERATION");

                    purdata.Tables[s].Columns.Add("CROP");
                    purdata.Tables[s].Columns.Add("VARIETY");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");


                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");
                    purdata.Tables[s].Columns.Add("ASCERTAIN_WEIGHT");

                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "'");

                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }

                List<string> qryStringCol = new List<string>();

                //--------Validate-------

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string orgcode1 = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string recipecode1 = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    string shift1 = purdata.Tables[d].Rows[0]["SHIFT"].ToString();
                    string supervisorcd1 = purdata.Tables[d].Rows[0]["SHIFT_INCHARGE"].ToString();

                    string dateoroper1 = purdata.Tables[d].Rows[0]["DATE_OF_OPERATION"].ToString();
                    string crop1 = purdata.Tables[d].Rows[0]["CROP"].ToString();
                    string variety1 = purdata.Tables[d].Rows[0]["VARIETY"].ToString();




                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        //  string grade1 = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string ascertainwt1 = purdata.Tables[d].Rows[h]["ASCERTAIN_WEIGHT"].ToString();
                        string subinvcd1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();




                        if (baleno1 == "")
                        {

                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {
                            DataTable ds = new DataTable();
                            strsql = "select MARKED_WT,PROCESS_STATUS,GRADE from GPIL_STOCK where GRADE IS NOT NULL AND GPIL_BALE_NUMBER='" + baleno1 + "' AND CURR_ORGN_CODE='" + orgcode1 + "'";
                            ds = dlMgt.GetQueryResult(strsql);
                            if (ds.Rows.Count > 0)
                            {
                                mrkdwt = Convert.ToDouble(ds.Rows[0][0].ToString());
                                processts = ds.Rows[0][0].ToString();

                                if (processts == "Y")
                                {

                                    data = "Error: Bale is already been using by another process for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {
                                    try
                                    {
                                        if (mrkdwt >= Convert.ToDouble(ascertainwt1))
                                        {
                                            //ThreshingIssueUpdate(baleno1, "Y");
                                        }
                                        else
                                        {

                                            data = "Error: Ascertain wt should not exist with marked wt BaleNumber--" + baleno1;
                                            json = JsonConvert.SerializeObject(data);
                                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                            jsonResult.MaxJsonLength = int.MaxValue;
                                            return jsonResult;

                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        data = "Error: Error in Ascertain wt " + ex.Message.ToString() + " BaleNumber--" + baleno1;
                                        json = JsonConvert.SerializeObject(data);
                                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                        jsonResult.MaxJsonLength = int.MaxValue;
                                        return jsonResult;

                                    }

                                }

                            }
                            else
                            {

                                data = "Error: Bale Is not available in current orginization/Not Yet Classified BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }


                        }


                    }

                }





                //InsertData


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString() + "TH" + DateTime.Now.ToString("yyyyMMddHHmmss") + d;
                    string orgcode = purdata.Tables[d].Rows[0]["ORGN_CODE"].ToString();
                    string recipecode = purdata.Tables[d].Rows[0]["RECIPE_CODE"].ToString();
                    string shift = purdata.Tables[d].Rows[0]["SHIFT"].ToString();
                    string supervisercd = purdata.Tables[d].Rows[0]["SHIFT_INCHARGE"].ToString();

                    DateTime dtdateofoper = Convert.ToDateTime(purdata.Tables[d].Rows[0]["DATE_OF_OPERATION"].ToString());

                    string dateofoper = dtdateofoper.ToString("yyyy-MM-dd").Trim();
                    string crop = purdata.Tables[d].Rows[0]["CROP"].ToString();
                    string variety = purdata.Tables[d].Rows[0]["VARIETY"].ToString();

                    //+ Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','TAP','LOC1','" + orgcd + "')";
                    strsql = "INSERT INTO [GPIL_THRESH_RECON_HDR_TEMP]([BATCH_NO],[ORGN_CODE],[RECIPE_CODE],[SHIFT],[SHIFT_INCHARGE],[DATE_OF_OPERATION],[CROP],[VARIETY],[CREATED_BY],[CREATED_DATE],[STATUS])";
                    strsql = strsql + " VALUES('" + temphdr + "','" + orgcode + "','" + recipecode + "','" + shift + "','" + supervisercd + "','" + Convert.ToDateTime(dateofoper).ToString("yyyy-MM-dd") + "','" + crop + "','" + variety + "','" + Session["userID"].ToString() + "',GETDATE(),'I')";
                    qryStringCol.Add(strsql);



                    temprefno = temprefno + Environment.NewLine + "Batch No- " + temphdr;
                    int h = 0;
                    double totqty = 0;
                    double ascertqty = 0;
                    for (h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();

                        string ascertainwt1 = purdata.Tables[d].Rows[h]["ASCERTAIN_WEIGHT"].ToString();

                        string subinvcd1 = "";
                        string markedwt = "0";
                        string grade1 = string.Empty;


                        strsql = "select MARKED_WT,PROCESS_STATUS,CURR_WT,GRADE,SUBINVENTORY_CODE,STATUS from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";
                        DataTable ds1 = new DataTable();
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            if (ds1.Rows[0][5].ToString() == "N")
                            {
                                data = "Error: Bale Number " + baleno + " Already Issued.";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;


                            }
                            markedwt = ds1.Rows[0][0].ToString();
                            grade1 = ds1.Rows[0][3].ToString();

                            subinvcd1 = ds1.Rows[0][4].ToString();
                        }



                        totqty = totqty + Convert.ToDouble(markedwt);
                        ascertqty = ascertqty + Convert.ToDouble(ascertainwt1);


                        strsql = "INSERT INTO [GPIL_THRESH_RECON_DTLS_1_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT],[SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                        strsql = strsql + "Values('" + temphdr + "','TH" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','IPB','GT','" + baleno.Trim() + "','" + grade1.Trim() + "','" + markedwt.Trim() + "','" + ascertainwt1.Trim() + "','" + subinvcd1.Trim() + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                        qryStringCol.Add(strsql);


                        strsql = "update GPIL_STOCK set STATUS='N',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + ascertainwt1 + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        qryStringCol.Add(strsql);


                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";

                        strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','THRESHING','" + temphdr.Trim() + "','" + ascertainwt1.Trim() + "','" + orgcode.Trim() + "','" + dateofoper + "','N')";
                        qryStringCol.Add(strsql);


                    }
                    strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                    strsql = strsql + "Values('S" + temphdr.Trim() + "','BYP','" + crop + variety + "SLOSS','" + Math.Round((totqty - ascertqty), 1) + "','" + Math.Round((totqty - ascertqty), 1) + "','LOC1','" + orgcode.Trim() + "','" + crop + "','" + variety + "','SLOSS','N','" + temphdr + "','N','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','GLT','LOC1','" + orgcode + "')";
                    qryStringCol.Add(strsql);


                    strsql = "INSERT INTO [GPIL_THRESH_RECON_DTLS_1_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT],[SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                    strsql = strsql + "Values('" + temphdr + "','THS" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','OPB','SLOSS','S" + temphdr.Trim() + "','" + crop + variety + "SLOSS','" + Math.Round((totqty - ascertqty), 1) + "','" + Math.Round((totqty - ascertqty), 1) + "','BYP','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                    qryStringCol.Add(strsql);


                    strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";

                    strsql = strsql + "Values('PCS" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','S" + temphdr.Trim() + "','GRADING','" + temphdr.Trim() + "','" + Math.Round((totqty - ascertqty), 1) + "','" + orgcode.Trim() + "','" + dateofoper + "','N')";
                    qryStringCol.Add(strsql);



                    strsql = "update GPIL_THRESH_RECON_HDR_TEMP set TOT_ISSUE_BALES='" + h + "',TOT_ISSUE_MARKED_QTY='" + totqty + "',TOT_ISSUE_ASCERTAIN_QTY='" + ascertqty + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE BATCH_NO='" + temphdr.Trim() + "'";
                    qryStringCol.Add(strsql);

                    temprefno = temprefno + Environment.NewLine + "Batch No- " + temphdr + " / Recipe - " + recipecode + " / Issued Qty - " + ascertqty;

                }
                //bool b = false;
                //if (qryStringCol.Count > 0)
                //{
                //    GPIWebApp.DataServerSync.Instance.TransactionInsert(qryStringCol);
                //}
                bool b = dlMgt.UpdateUsingExecuteNonQueryList(qryStringCol);
                if (b)
                {
                    data = "Succuss: Data Inserted SucessFully";
                    json = JsonConvert.SerializeObject(data);
                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {

                }
                // bool b = dlMgt.Transaction(qryStringCol);

                //data = "Success: Data Uploaded Successfully";
                //json = JsonConvert.SerializeObject(data);
                //jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            return null;
        }


        string thresisserr = string.Empty;


        /// <summary>
        /// Threshing Outturn Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreshingOutturnLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportTOFromExcel(HttpPostedFileBase postedFile)
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
            return View("ThreshingOutturnLoaderIndex");
        }

        [HttpPost]
        public JsonResult ThreshingOutturnComplete(ListThreshingOutturnLoader LTO)

        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LTO.ThreshingOutturns);
                var od = from s in LTO.ThreshingOutturns
                         group s by new { s.BATCH_NO } into newgroup
                         select new
                         {
                             BATCH_NO = newgroup.Key.BATCH_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("BATCH_NO");


                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.BATCH_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["BATCH_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("BATCH_NO");
                    purdata.Tables[s].Columns.Add("BALE_TYPE");
                    purdata.Tables[s].Columns.Add("PRODUCT_TYPE");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("NET_WT");
                    purdata.Tables[s].Columns.Add("TARE_WT");
                    purdata.Tables[s].Columns.Add("SUBINVENTORY_CODE");


                    DataRow[] purrows = dtclstr.Select("BATCH_NO ='" + orgdata.Rows[s]["BATCH_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }

                //-----------Validation-----------

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string batchno1 = purdata.Tables[d].Rows[0]["BATCH_NO"].ToString();

                    string crop1 = string.Empty;
                    string variety1 = string.Empty;
                    string orgcode1 = string.Empty;
                    double totissueqty1 = 0;

                    strsql = "select CROP,VARIETY,ORGN_CODE from GPIL_THRESH_RECON_HDR_TEMP where BATCH_NO='" + batchno1 + "' and STATUS='I' ";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);

                    if (ds1.Rows.Count > 0)
                    {
                        crop1 = Convert.ToString(ds1.Rows[0][0]);
                        variety1 = Convert.ToString(ds1.Rows[0][1]);
                        orgcode1 = Convert.ToString(ds1.Rows[0][2]);

                    }
                    else
                    {
                        data = "Error: No Batch Found In Respective Table Or Batch Is Closed";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;

                    }

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string baletype1 = purdata.Tables[d].Rows[h]["BALE_TYPE"].ToString();
                        string producttype1 = purdata.Tables[d].Rows[h]["PRODUCT_TYPE"].ToString();
                        string grade1 = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string weight1 = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                        string tarewt1 = purdata.Tables[d].Rows[h]["TARE_WT"].ToString();
                        string subinvcd1 = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();

                        if (baleno1 == "")
                        {
                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;

                        }
                        else if (weight1 == "")
                        {

                            data = "Error: Weight is Empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else if (grade1 == "")
                        {

                            data = "Error: Grade is Empty for BaleNumber--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {

                            strsql = "SELECT * FROM GPIL_STOCK WHERE GPIL_BALE_NUMBER='" + baleno1.Trim() + "'";
                            DataTable ds2 = new DataTable();
                            ds2 = dlMgt.GetQueryResult(strsql);

                            if (ds2.Rows.Count > 0)
                            {

                                data = "Error: Bale/Case Already Exists in stock BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            else
                            {

                                strsql = "select * from GPIL_ITEM_MASTER where ITEM_CODE ='" + grade1.Trim() + "'";
                                DataTable ds3 = new DataTable();
                                ds3 = dlMgt.GetQueryResult(strsql);
                                if (ds3.Rows.Count > 0)
                                {
                                    //ThreshingOutturnUpdate(baleno1, "Y");
                                }
                                else
                                {

                                    data = "Error: Grade does not exists for BaleNumber--" + baleno1;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }


                            }


                        }


                    }

                }

                //InsertData


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id

                    string batchno = purdata.Tables[d].Rows[0]["BATCH_NO"].ToString();

                    string crop = string.Empty;
                    string variety = string.Empty;
                    string orgcode = string.Empty;
                    double totissuemrktqty = 0;
                    double totissueascertqty = 0;
                    double eleqty = 0;
                    double gtqty = 0;
                    double pcqty = 0;
                    double bpqty = 0;
                    double sloss = 0;
                    int elebls = 0;
                    int gtbls = 0;
                    int pcbls = 0;
                    int bpbls = 0;

                    strsql = "select CROP,VARIETY,ORGN_CODE,TOT_ISSUE_MARKED_QTY,TOT_ISSUE_ASCERTAIN_QTY from GPIL_THRESH_RECON_HDR_TEMP where BATCH_NO='" + batchno + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {
                        crop = Convert.ToString(ds1.Rows[0][0]);
                        variety = Convert.ToString(ds1.Rows[0][1]);
                        orgcode = Convert.ToString(ds1.Rows[0][2]);
                        totissuemrktqty = Convert.ToDouble(ds1.Rows[0][3]);
                        totissueascertqty = Convert.ToDouble(ds1.Rows[0][4]);
                    }

                    sloss = totissuemrktqty - totissueascertqty;
                    int h = 0;
                    double totqty = totissueascertqty;
                    double totoutqty = 0;
                    for (h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        string baletype = purdata.Tables[d].Rows[h]["BALE_TYPE"].ToString();
                        string producttype = purdata.Tables[d].Rows[h]["PRODUCT_TYPE"].ToString();
                        string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string weight = purdata.Tables[d].Rows[h]["NET_WT"].ToString();
                        string tarewt = purdata.Tables[d].Rows[h]["TARE_WT"].ToString();
                        string subinvcd = purdata.Tables[d].Rows[h]["SUBINVENTORY_CODE"].ToString();




                        totoutqty = totoutqty + Convert.ToDouble(weight);

                        if (producttype == "PC")
                        {
                            pcqty = pcqty + Convert.ToDouble(weight);// -Convert.ToDouble(tarewt);
                            pcbls = pcbls + 1;
                        }
                        else if (producttype == "BP")
                        {
                            bpqty = bpqty + Convert.ToDouble(weight);
                            bpbls = bpbls + 1;
                        }
                        else if (producttype == "GT")
                        {
                            gtqty = gtqty + Convert.ToDouble(weight);
                            gtbls = gtbls + 1;
                        }
                        else
                        {
                            eleqty = eleqty + Convert.ToDouble(weight);
                            elebls = elebls + 1;
                        }

                        if (producttype == "PC")
                        {
                            strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                            strsql = strsql + "Values('" + baleno.Trim() + "','" + subinvcd + "','" + grade + "','" + (Convert.ToDouble(weight.Trim())) + "','" + (Convert.ToDouble(weight.Trim())) + "','LOC1','" + orgcode.Trim() + "','" + crop + "','" + variety + "','" + producttype + "','N','" + batchno + "','Y','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','GLT','LOC1','" + orgcode + "')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);

                            strsql = "INSERT INTO [GPIL_THRESH_RECON_DTLS_2_TEMP]([BATCH_NO],[CASE_NUMBER],[PACKED_GRADE],[CROP],[VARIETY],[SUBINVENTORY_CODE],[GROSS_WT],[TARE_WT],[NET_WT],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                            strsql = strsql + "Values('" + batchno + "','" + baleno.Trim() + "','" + grade.Trim() + "','" + crop.Trim() + "','" + variety.Trim() + "','" + subinvcd.Trim() + "','" + (Convert.ToDouble(weight.Trim()) + Convert.ToDouble(tarewt.Trim())) + "','" + tarewt.Trim() + "','" + weight.Trim() + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);


                            strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                            strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','THRESHING','" + batchno.Trim() + "','" + (Convert.ToDouble(weight.Trim())) + "','" + orgcode.Trim() + "',GETDATE(),'N')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        }

                        else
                        {
                            strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                            strsql = strsql + "Values('" + baleno.Trim() + "','" + subinvcd + "','" + grade + "','" + weight.Trim() + "','" + weight.Trim() + "','LOC1','" + orgcode.Trim() + "','" + crop + "','" + variety + "','" + producttype + "','N','" + batchno + "','Y','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','GLT','LOC1','" + orgcode + "')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);

                            strsql = "INSERT INTO [GPIL_THRESH_RECON_DTLS_1_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT],[SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                            strsql = strsql + "Values('" + batchno + "','TH" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baletype + "','" + producttype + "','" + baleno.Trim() + "','" + grade.Trim() + "','" + weight.Trim() + "','" + weight.Trim() + "','" + subinvcd.Trim() + "','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);


                            strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                            strsql = strsql + "Values('PC" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','GRADING','" + batchno.Trim() + "','" + weight.Trim() + "','" + orgcode.Trim() + "',GETDATE(),'N')";
                            dlMgt.UpdateUsingExecuteNonQuery(strsql);
                        }

                    }
                    double tottloss = Math.Round((totissuemrktqty - gtqty - eleqty) - (pcqty + bpqty + sloss), 1);
                    strsql = "insert into GPIL_STOCK(GPIL_BALE_NUMBER,SUBINVENTORY_CODE,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CROP,VARIETY,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,CREATED_BY,CREATED_DATE,LAST_UPDATED_BY,LAST_UPDATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,CURR_LOCN,CURR_ORGN_CODE)";
                    strsql = strsql + "Values('T" + batchno.Trim() + "','BYP','" + crop + variety + "LOSS','" + tottloss + "','" + tottloss + "','LOC1','" + orgcode.Trim() + "','" + crop + "','" + variety + "','LOSS','N','" + batchno + "','N','" + Session["userID"].ToString() + "',GETDATE(),'" + Session["userID"].ToString() + "',GETDATE(),'N','GLT','LOC1','" + orgcode + "')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "INSERT INTO [GPIL_THRESH_RECON_DTLS_1_TEMP]([BATCH_NO],[DETAIL_ID],[BALE_TYPE],[PRODUCT_TYPE],[GPIL_BALE_NUMBER],[GRADE],[MARKED_WT],[ASCERTAIN_WT],[SUBINVENTORY_CODE],[CREATED_BY],[CREATED_DATE],[STATUS],[HEADER_STATUS])";
                    strsql = strsql + "Values('" + batchno + "','THL" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','OPB','LOSS','T" + batchno.Trim() + "','" + crop + variety + "LOSS','" + tottloss + "','" + tottloss + "','BYP','" + Session["userID"].ToString() + "',GETDATE(),'Y','N')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                    strsql = strsql + "Values('PCL" + orgcode + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','T" + batchno.Trim() + "','GRADING','" + batchno.Trim() + "','" + tottloss + "','" + orgcode.Trim() + "',GETDATE(),'N')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    strsql = "update GPIL_THRESH_RECON_HDR_TEMP set STATUS='C',TOT_PRODUCT_CASE='" + pcbls + "',TOT_PRODUCT_QTY='" + pcqty + "',TOT_BYPRODUCT_BALES='" + bpbls + "',TOT_BYPRODUCT_QTY='" + bpqty + "',TOT_ELIMINATE_BALES='" + elebls + "',TOT_ELIMINATE_QTY='" + eleqty + "',TOT_GRADTRANS_BALES='" + gtbls + "',TOT_GRADTRANS_QTY='" + gtqty + "',TOT_INPUT_QTY='" + totissuemrktqty + "',TOT_OUTPUT_QTY='" + totissuemrktqty + "' ,LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE BATCH_NO='" + batchno.Trim() + "'";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);




                }

                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }





        /// <summary>
        /// Packed Case Dispatch Loader
        /// </summary>
        /// <returns></returns>

        public ActionResult PackedCaseDispatchLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportPCDFromExcel(HttpPostedFileBase postedFile)
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
            return View("PackedCaseDispatchLoaderIndex");
        }

        [HttpPost]
        public JsonResult PackedCaseDispatchComplete(ListPackedCaseDispatchLoader LPCD)
        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LPCD.PackedCaseDispatchs);
                var od = from s in LPCD.PackedCaseDispatchs
                         group s by new { s.SHIPMENT_NO } into newgroup
                         select new
                         {
                             SHIPMENT_NO = newgroup.Key.SHIPMENT_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("SHIPMENT_NO");


                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SHIPMENT_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SHIPMENT_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("SHIPMENT_NO");
                    purdata.Tables[s].Columns.Add("SENDER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("RECEIVER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("SEND_BY");
                    purdata.Tables[s].Columns.Add("SENDER_TRUCK_NO");
                    purdata.Tables[s].Columns.Add("LR_NO");
                    purdata.Tables[s].Columns.Add("DRIVER_NAME");
                    purdata.Tables[s].Columns.Add("DRIVING_LICENCE_NO");
                    purdata.Tables[s].Columns.Add("TRANSPORT_CODE");
                    purdata.Tables[s].Columns.Add("FRIEGHT_CHARGES");

                    purdata.Tables[s].Columns.Add("CASE_NUMBER");
                    // purdata.Tables[s].Columns.Add("DISPATCH_WT");
                    purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("LR_DATE");
                    purdata.Tables[s].Columns.Add("DISPATCH_TYPE");
                    purdata.Tables[s].Columns.Add("AWB_NO");
                    purdata.Tables[s].Columns.Add("IS_LP5_NOTE");
                    purdata.Tables[s].Columns.Add("REMARKS");


                    DataRow[] purrows = dtclstr.Select("SHIPMENT_NO ='" + orgdata.Rows[s]["SHIPMENT_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }

                //-----------Validation----------

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string currorg = "";

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();
                        string strGrade = purdata.Tables[d].Rows[h]["GRADE"].ToString();

                        if (h == 0)
                        {
                            string shipmentno = purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString();

                            currorg = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString();
                            string strToOrg = purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString();
                            string strPicker = purdata.Tables[d].Rows[0]["SEND_BY"].ToString();
                            string strLRNo = purdata.Tables[d].Rows[0]["LR_NO"].ToString();
                            string strTransportCode = purdata.Tables[d].Rows[0]["TRANSPORT_CODE"].ToString();

                            string strDispacthType = purdata.Tables[d].Rows[0]["DISPATCH_TYPE"].ToString();
                            string strAWBNo = purdata.Tables[d].Rows[0]["AWB_NO"].ToString();
                            string strIsLP5Note = purdata.Tables[d].Rows[0]["IS_LP5_NOTE"].ToString();
                            string strRemarks = purdata.Tables[d].Rows[0]["REMARKS"].ToString();
                            bool bolIsHeaderError = false;
                            string strHdrError = "";

                            string strQuery1 = "select * from mLocations(NOLOCK) where LocCode='" + currorg + "' and LocType in ('PSW','GLT','REDRYING')";
                            string strQuery2 = "select * from mLocations(NOLOCK) where LocCode='" + strToOrg + "' and LocType='PSW'";
                            string strQuery3 = "select * from mUsers(NOLOCK) where UserName='" + strPicker + "' and LocCode='" + currorg + "'";
                            string strQuery4 = "select * from mTransporters(NOLOCK) WHERE TransporterCode='" + strTransportCode + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strQuery1);
                            DataTable ds2 = new DataTable();
                            ds2 = dlMgt.GetQueryResult(strQuery2);
                            DataTable ds3 = new DataTable();
                            ds3 = dlMgt.GetQueryResult(strQuery3);
                            DataTable ds4 = new DataTable();
                            ds4 = dlMgt.GetQueryResult(strQuery4);



                            if (ds1.Rows.Count == 0)
                            {

                                data = "Error:  Sender Location Type must be GLT's/PSW's Location ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }
                            if (ds2.Rows.Count == 0)
                            {

                                data = "Error:  To Location Type must be PSW's Location ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }

                            if (ds4.Rows.Count == 0)
                            {

                                data = "Error:  Transporter Code does't exist ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }
                            if (strLRNo.Length > 30)
                            {

                                data = "Error:  LR No should not exist more than 30 characters ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            if (strDispacthType != "E" && strDispacthType != "F")
                            {

                                data = "Error:  Dispatch Type must be E or F---(" + baleno1 + ") ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            if (strAWBNo.Length > 40)
                            {

                                data = "Error:  ASW No should not exist more than 40 characters ----(" + baleno1 + ") ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            if (strIsLP5Note != "0" && strIsLP5Note != "1")
                            {

                                data = "Error:  LP5 Note should be 0 or 1--- (" + baleno1 + ") ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            if (strRemarks.Length > 50)
                            {

                                data = "Error:   Remarks should not exist more than 50 characters---- (" + baleno1 + ") ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            if (bolIsHeaderError == true)
                            {

                                data = "Error:  Shipment no : " + shipmentno + " " + strHdrError;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                        }


                        if (baleno1 == "" || baleno1.Trim().Length != 31)
                        {

                            data = "Error:  Case Number : " + baleno1 + " is in-valid";
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {
                            bool bolIsDetailError = false;

                            strsql = "select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE,WMS_STATUS,FUMIGATION_STATUS,GRADE from GPIL_STOCK(NOLOCK) where GPIL_BALE_NUMBER='" + baleno1 + "' and CURR_ORGN_CODE='" + currorg + "' AND STATUS='Y'";
                            // }
                            DataTable ds5 = new DataTable();
                            ds5 = dlMgt.GetQueryResult(strsql);
                            if (ds5.Rows.Count > 0)
                            {
                                string srksubcd;
                                mrkdwt = Convert.ToDouble(ds5.Rows[0][0]);
                                processts = Convert.ToString(ds5.Rows[0][1]);
                                srksubcd = Convert.ToString(ds5.Rows[0][2]);

                                string strWMSStatus = Convert.ToString(ds5.Rows[0][3]);
                                string strFumigationStatus = Convert.ToString(ds5.Rows[0][4]);
                                string strCaseGrade = Convert.ToString(ds5.Rows[0][5]);

                                string strDtlsError = "";

                                if (processts == "Y")
                                {

                                    data = "Error:  is using in another process ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                if (strWMSStatus != "Y")
                                {
                                    data = "Error:  is not yet moved to WMS ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;

                                }
                                if (strFumigationStatus.Trim() == "FUM-U")
                                {

                                    data = "Error:  is in under Fumigation ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }

                                if (strCaseGrade != strGrade)
                                {

                                    data = "Error:  not match with stock grade (" + strCaseGrade + ") ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }


                                if (bolIsDetailError == true)
                                {

                                    data = "Error: Case Number : " + baleno1 + " " + strDtlsError;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {
                                    //PackedCaseDispatchUpdate(baleno1, "Y");
                                }


                            }
                            else
                            {

                                data = "Error: Case Number : " + baleno1 + " does not exists";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }


                        }
                    }

                }

                //InsertData


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    string temphdr = "PCD" + purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    string sendorg = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString();
                    string recevorg = purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString();
                    string sendby = purdata.Tables[d].Rows[0]["SEND_BY"].ToString();
                    string truckno = purdata.Tables[d].Rows[0]["SENDER_TRUCK_NO"].ToString();
                    string rcno = purdata.Tables[d].Rows[0]["LR_NO"].ToString();
                    string drivername = purdata.Tables[d].Rows[0]["DRIVER_NAME"].ToString();
                    string licenceno = purdata.Tables[d].Rows[0]["DRIVING_LICENCE_NO"].ToString();
                    string transportname = purdata.Tables[d].Rows[0]["TRANSPORT_CODE"].ToString();
                    string frightchrgs = purdata.Tables[d].Rows[0]["FRIEGHT_CHARGES"].ToString();
                    string senddate = purdata.Tables[d].Rows[0]["LR_DATE"].ToString();
                    string sndorgty = "";
                    string rcvorgtyp = "";

                    string strDispacthType = purdata.Tables[d].Rows[0]["DISPATCH_TYPE"].ToString();
                    string strAWBNo = purdata.Tables[d].Rows[0]["AWB_NO"].ToString();
                    string strIsLP5Note = purdata.Tables[d].Rows[0]["IS_LP5_NOTE"].ToString();
                    string strRemarks = purdata.Tables[d].Rows[0]["REMARKS"].ToString();


                    strsql = "Select O.ORGN_TYPE,O2.ORGN_TYPE FROM GPIL_ORGN_MASTER(NOLOCK) O,GPIL_ORGN_MASTER(NOLOCK) O2 WHERE O.ORGN_CODE='" + sendorg + "' AND O2.ORGN_CODE='" + recevorg + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);

                    if (ds1.Rows.Count > 0)
                    {
                        sndorgty = Convert.ToString(ds1.Rows[0][0]);
                        rcvorgtyp = Convert.ToString(ds1.Rows[0][1]);
                    }


                    strsql = "INSERT INTO [GPIL_SHIPMENT_HDR_TEMP]([SHIPMENT_NO],[SENDER_ORGN_CODE],[RECEIVER_ORGN_CODE],[SENDER_DATE],[SENT_BY],[SENDER_TRUCK_NO] ,[RC_NO],[DRIVER_NAME],[DRIVING_LICENCE_NO],[TRANSPORT_NAME],[FRIEGHT_CHARGES],[UOM],[CREATED_BY],[CREATED_DATE],[STATUS],[ATTRIBUTE2],[ATTRIBUTE3],[ATTRIBUTE4],[ATTRIBUTE5],[IS_WMS_SHIPMENT],TOT_NO_OF_BALES)";
                    strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "','" + Convert.ToDateTime(senddate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','" + frightchrgs + "','KG','" + Session["userID"].ToString() + "',GETDATE(),'INT','" + sndorgty + "','" + rcvorgtyp + "','" + strDispacthType + "|" + strAWBNo + "|" + strIsLP5Note + "','" + strRemarks + "','Y'," + purdata.Tables[d].Rows.Count + ")";

                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();
                        // string diswt = purdata.Tables[d].Rows[h]["DISPATCH_WT"].ToString();
                        string fromsubcode = "";//purdata.Tables[d].Rows[h]["FROM_SUBINVENTORY_CODE"].ToString();
                        string tosubcode = "";// purdata.Tables[d].Rows[h]["TO_SUBINVENTORY_CODE"].ToString();
                                              // string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string loadtime = purdata.Tables[d].Rows[h]["LR_DATE"].ToString();
                        string markedwt = "0";
                        string diswt = "0";
                        string grade = string.Empty;
                        string orgntype = string.Empty;

                        strsql = "Select ORGN_TYPE from GPIL_ORGN_MASTER(NOLOCK) where ORGN_CODE='" + sendorg + "'";
                        DataTable ds2 = new DataTable();
                        ds2 = dlMgt.GetQueryResult(strsql);

                        if (ds2.Rows.Count > 0)
                        {
                            orgntype = Convert.ToString(ds2.Rows[0][0]);
                        }



                        strsql = "select MARKED_WT,PROCESS_STATUS,MARKED_WT,GRADE,SUBINVENTORY_CODE from GPIL_STOCK(NOLOCK) where GPIL_BALE_NUMBER='" + baleno + "'";


                        DataTable ds3 = new DataTable();
                        ds3 = dlMgt.GetQueryResult(strsql);
                        if (ds3.Rows.Count > 0)
                        {
                            markedwt = Convert.ToString(ds3.Rows[0][0]);
                            diswt = Convert.ToString(ds3.Rows[0][2]);
                            grade = Convert.ToString(ds3.Rows[0][3]);
                            fromsubcode = Convert.ToString(ds3.Rows[0][4]);
                            tosubcode = Convert.ToString(ds3.Rows[0][4]);
                        }


                        strsql = "INSERT INTO [GPIL_SHIPMENT_DTLS_TEMP]([SHIPMENT_NO],[DETAIL_ID],[GPIL_BALE_NUMBER],[MARKED_WT],[DISPATCH_WEIGHT],[FROM_SUBINVENTORY_CODE],[TO_SUBINVENTORY_CODE],[LOADING_DATETIME],[STATUS],[HEADER_STATUS],[CREATED_BY],[CREATED_DATE],[GRADE])";
                        strsql = strsql + "Values('" + temphdr + "','SH" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','" + markedwt.Trim() + "','" + diswt.Trim() + "','" + fromsubcode.Trim() + "','" + tosubcode.Trim() + "','" + Convert.ToDateTime(senddate).ToString("yyyy-MM-dd HH:mm:ss") + "','INT','N','" + Session["userID"].ToString() + "',GETDATE(),'" + grade.Trim() + "')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                        strsql = "update GPIL_STOCK set STATUS='INT',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + diswt + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        //strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") +d+ h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "',GETDATE(),'N')";
                        strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "','" + Convert.ToDateTime(senddate).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }


                    string SPName = "GPIL_SP_DISPATCH";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[3];
                    pram[0] = (new SqlParameter("@DISPATCHNO", SqlDbType.NVarChar, 50));
                    pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                    pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                    pram[0].Value = temphdr;
                    pram[1].Direction = ParameterDirection.Output;
                    pram[2].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = "/" + retVal + "||" + dlMgt.SP_ExecuteNonQuery(parameters, SPName);



                }


                data = "Success :DONE where shipmentno are" + retVal;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;


            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }



        /// <summary>
        /// Packed Case Receipt
        /// </summary>
        /// <returns></returns>
        public ActionResult PackedCaseReceiptLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportPCRFromExcel(HttpPostedFileBase postedFile)
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
            return View("PackedCaseReceiptLoaderIndex");
        }

        [HttpPost]
        public JsonResult PackedCaseReceiptComplete(ListPackedCaseReceipt LPCR)

        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LPCR.PackedCaseReceipts);
                var od = from s in LPCR.PackedCaseReceipts
                         group s by new { s.SHIPMENT_NO } into newgroup
                         select new
                         {
                             SHIPMENT_NO = newgroup.Key.SHIPMENT_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("SHIPMENT_NO");


                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SHIPMENT_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SHIPMENT_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("SHIPMENT_NO");
                    purdata.Tables[s].Columns.Add("GPIL_BALE_NUMBER");
                    // purdata.Tables[s].Columns.Add("RECEIPT_WT");
                    purdata.Tables[s].Columns.Add("UNLOADING_DATETIME");


                    DataRow[] purrows = dtclstr.Select("SHIPMENT_NO ='" + orgdata.Rows[s]["SHIPMENT_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }

                //----------Validation------------

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string shipno1 = purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString();

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        //   string netwt1 = purdata.Tables[d].Rows[h]["RECEIPT_WT"].ToString();



                        if (baleno1 == "")
                        {

                            data = "Error: Bale Number is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }

                        if (shipno1 == "")
                        {

                            data = "Error: Shipment No is Empty  Bale Number--" + baleno1;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }

                        else
                        {
                            strsql = "select D.GPIL_BALE_NUMBER,D.MARKED_WT from GPIL_SHIPMENT_DTLS(NOLOCK) D,GPIL_SHIPMENT_HDR(NOLOCK) H WHERE D.SHIPMENT_NO=H.SHIPMENT_NO AND H.SHIPMENT_NO='" + shipno1 + "' AND D.GPIL_BALE_NUMBER='" + baleno1 + "' AND H.STATUS='INT'  AND D.STATUS='INT'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);

                            if (ds1.Rows.Count > 0)
                            {

                                //PackedCaseReceiptUpdate(baleno1, "Y");

                            }
                            else
                            {

                                data = "Error: Batch Does not Esists in this shipment no for BaleNumber--" + baleno1;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }


                        }


                    }



                }

                //InsertData

                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    string recvorg = "TT";

                    /// Generating Header Id
                    strsql = "select RECEIVER_ORGN_CODE FROM GPIL_SHIPMENT_HDR(NOLOCK) WHERE SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {
                        recvorg = Convert.ToString(ds1.Rows[0][0]);
                    }


                    string SHIPNO = purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString();
                    strsql = "UPDATE GPIL_SHIPMENT_HDR SET STATUS='C',WMS_FLAG='Y' WHERE SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["GPIL_BALE_NUMBER"].ToString();
                        //string recevwt = purdata.Tables[d].Rows[h]["RECEIPT_WT"].ToString();
                        string loadtime = purdata.Tables[d].Rows[h]["UNLOADING_DATETIME"].ToString();
                        string recevwt = "0";

                        strsql = "select DISPATCH_WEIGHT from GPIL_SHIPMENT_DTLS(NOLOCK) where GPIL_BALE_NUMBER ='" + baleno + "' and SHIPMENT_NO='" + purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString() + "'";
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            recevwt = Convert.ToString(ds1.Rows[0][0]);
                        }


                        //strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "',GETDATE(),'" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','" + frightchrgs + "','KG','"+Session["userID"].ToString()+"',GETDATE(),'INT')";

                        strsql = "update GPIL_SHIPMENT_DTLS set RECEIPT_WEIGHT='" + recevwt + "',STATUS='RCV',HEADER_STATUS='N',UNLOADING_DATETIME= '" + Convert.ToDateTime(loadtime).ToString("yyyy - MM - dd HH: mm: ss") + "' WHERE GPIL_BALE_NUMBER='" + baleno + "'";

                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        strsql = strsql + "Values('PC" + recvorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','RECEPIT','" + SHIPNO.Trim() + "','" + recevwt.Trim() + "','" + recvorg.Trim() + "',GETDATE(),'N')";
                        //strsql = strsql + "Values('PC" + recvorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','RECEPIT','" + SHIPNO.Trim() + "','" + recevwt.Trim() + "','" + recvorg.Trim() + "',GETDATE(),'N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }

                    string SPName = "GPIL_SP_RECEIPT";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[4];
                    pram[0] = (new SqlParameter("@RECEIVEDSHIPMENTNO", SqlDbType.NVarChar, 50));
                    pram[1] = (new SqlParameter("@RECEIVEDSHIPMENTNO", SqlDbType.NVarChar, 20));
                    pram[2] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                    pram[3] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                    pram[0].Value = SHIPNO;
                    pram[1].Value = Session["userID"].ToString();
                    pram[2].Direction = ParameterDirection.Output;
                    pram[3].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);



                }


                data = "Success: Data Uploaded Successfully";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;



            }
            catch (Exception ex)
            {
                data = "Error: Exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }




        /// <summary>
        /// Fumigation Report
        /// </summary>
        /// <returns></returns>
        public ActionResult FumigationReportLoaderIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFRFromExcel(HttpPostedFileBase postedFile)
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
            return View("FumigationReportLoaderIndex");
        }

        [HttpPost]
        public JsonResult FumigationReportComplete(ListFumigationReport LFR)

        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LFR.FumigationReports);
                var od = from s in LFR.FumigationReports
                         group s by new { s.FUMIGATION_BATCH } into newgroup
                         select new
                         {
                             FUMIGATION_BATCH = newgroup.Key.FUMIGATION_BATCH

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("FUMIGATION_BATCH");


                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.FUMIGATION_BATCH);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
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
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }


                //--------------Validation---------

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

                            string strQuery1 = "select * from mLocations where LocCode='" + strOrgn + "' and LocType='PSW'";
                            //string strQuery2 = "select * from mUsers where UserName='" + strPicker + "' and LocCode='" + strOrgn + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strQuery1);

                            if (ds1.Rows.Count == 0)
                            {

                                data = "Error: Fumigation Location Type must be PSW's Location  :Batch no--" + strFumBatch;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }


                            int intRunDays = 0;
                            try
                            {
                                intRunDays = Convert.ToInt32(strRunDays.Trim());
                            }
                            catch (Exception ex)
                            {

                                data = "Error: Fumigation Run Period must be numberic  :Batch no--" + strFumBatch;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            try
                            {
                                DateTime objDateTime = Convert.ToDateTime(strStartingDate);
                            }
                            catch (Exception ex)
                            {

                                data = "Error: Fumigation Starting Date must be datetime feild  :Batch no--" + strFumBatch;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }



                            int intExpDays = 0;
                            try
                            {
                                intExpDays = Convert.ToInt32(strExpDays.Trim());
                            }
                            catch (Exception ex)
                            {

                                data = "Error: Fumigation Expiry Period must be numberic  :Batch no--" + strFumBatch;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            if (intExpDays != 0 && intRunDays != 0 && intExpDays > intRunDays)
                            {
                                //valid
                            }
                            else
                            {

                                data = "Error: Fumigation Expiry Period must be greater than Run Preiod and both should not be zero  :Batch no--" + strFumBatch;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }


                            if (strRemarks.Length > 50)
                            {

                                data = "Error: Remarks should not exist more than 50 characters  :Batch no--" + strFumBatch;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            if (bolIsHeaderError == true)
                            {

                            }

                        }


                        if (strCaseNo == "" || strCaseNo.Trim().Length != 31)
                        {
                            data = "Error: Case Number is In-Valid  Case Number--" + strCaseNo;
                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;

                        }
                        else
                        {
                            bool bolIsDetailError = false;

                            strsql = "select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE,WMS_STATUS,FUMIGATION_STATUS,GRADE from GPIL_STOCK where GPIL_BALE_NUMBER='" + strCaseNo + "' and CURR_ORGN_CODE='" + strOrgn + "' AND STATUS='Y'";
                            // }
                            DataTable ds2 = new DataTable();
                            ds2 = dlMgt.GetQueryResult(strsql);

                            if (ds2.Rows.Count > 0)
                            {
                                string srksubcd;
                                mrkdwt = Convert.ToDouble(ds2.Rows[0][0]);
                                processts = Convert.ToString(ds2.Rows[0][1]);
                                srksubcd = Convert.ToString(ds2.Rows[0][2]);

                                string strWMSStatus = Convert.ToString(ds2.Rows[0][3]);
                                string strFumigationStatus = Convert.ToString(ds2.Rows[0][4]);
                                string strCaseGrade = Convert.ToString(ds2.Rows[0][5]);

                                if (processts == "Y")
                                {

                                    data = "Error: Case Is using in another process Case Number--" + strCaseNo;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                if (strWMSStatus != "Y")
                                {

                                    data = "Error: Case number not yet moved to WMS; Case Number--" + strCaseNo;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                if (strFumigationStatus.Trim() == "FUM-U")
                                {

                                    data = "Error: Case is already in under Fumigation; Case Number--" + strCaseNo;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }



                            }
                            else
                            {

                                data = "Error: Case Number Does not exists Case Number--" + strCaseNo;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }


                        }
                    }

                }



                //InsertData
                // "','" + Convert.ToDateTime(senddate).ToString("yyyy-MM-dd HH:mm:ss") + "'

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
                    strFumEndDate = Convert.ToDateTime(strFumStartDate).AddDays(Convert.ToInt32(strRunPriod)).ToString();
                    strFumExpDate = Convert.ToDateTime(strFumStartDate).AddDays(intAddDays).ToString();


                    strsql = "INSERT INTO [GPIL_FUMIGATION_HDR_TEMP] ([BATCH_NO],[ORGN_CODE],[FUMIGATION_STARTING_DATE],[FUMIGATION_ENDING_DATE] ,[FUMIGATION_DAYS_FOR_RUNPREIOD] ,[FUMIGATION_DAYS_FOR_EXPIRY] ,[CREATED_BY] ,[CREATED_DATE],[STATUS],[REMARKS])";
                    strsql = strsql + " VALUES('" + temphdr + "','" + strOrgn + "',CONVERT(varchar,'" + strFumStartDate + "',105),CONVERT(varchar,'" + strFumEndDate + "',105) ,'" + strRunPriod + "' ,'" + strExpiryPreiod + "' ,'" + strFumBy + "',GETDATE(),'Y','" + strRemarks + "')";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string strCaseNo = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();


                        strsql = "INSERT INTO [GPIL_FUMIGATION_DTLS_TEMP] ([DETAIL_ID],[BATCH_NO],[CASE_NUMBER],[FUMIGATION_STARTING_DATE],[FUMIGATION_ENDING_DATE],[FUMIGATION_EXPIRY_DATE],[CREATED_BY],[CREATED_DATE],[STATUS],[FUM_STATUS])";
                        strsql = strsql + " VALUES ('FUM" + strOrgn + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + temphdr + "','" + strCaseNo + "',CONVERT(varchar,'" + strFumStartDate + "',105),CONVERT(varchar,'" + strFumEndDate + "',105),CONVERT(varchar,'" + strFumExpDate + "',105),'" + strFumBy + "',GETDATE(),'Y','FUM-U')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set FUMIGATION_STATUS='FUM-U',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',LAST_UPDATED_BY='" + strFumBy + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + strCaseNo.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }

                    strsql = "UPDATE GPIL_FUMIGATION_HDR_TEMP SET STATUS='C' WHERE BATCH_NO='" + temphdr + "'";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);



                    string SPName = "FUMIGATION_BATCH";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[2];
                    pram[0] = (new SqlParameter("@refNo", SqlDbType.VarChar, 50));
                    pram[1] = (new SqlParameter("@status", SqlDbType.VarChar, 50));
                    pram[0].Value = temphdr;
                    pram[1].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);


                }



                data = "Success :DONE where Fumigation are" + retVal;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;



            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }



        /// <summary>
        /// Factory Dispatch Loader
        /// </summary>
        /// <returns></returns>
        public ActionResult FactoryDispatchLoaderIndex()
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
        public JsonResult FactoryDispatchComplete(ListFactoryDispatch LFD)

        {

            string retVal = string.Empty;
            try
            {

                DataLoaderManagement dlMgt = new DataLoaderManagement();
                DataTable dtclstr = ToDataTable(LFD.FactoryDispatchs);
                var od = from s in LFD.FactoryDispatchs
                         group s by new { s.SHIPMENT_NO } into newgroup
                         select new
                         {
                             SHIPMENT_NO = newgroup.Key.SHIPMENT_NO

                         };
                var ods = od.ToList();
                DataTable orgdata = new DataTable();
                orgdata.Columns.Add("SHIPMENT_NO");


                DataRow row = null;

                foreach (var rowObj in ods)
                {
                    row = orgdata.NewRow();
                    orgdata.Rows.Add(rowObj.SHIPMENT_NO);
                }



                //PurchaseData

                DataSet purdata = new DataSet();

                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
                    string tablename = orgdata.Rows[s]["SHIPMENT_NO"].ToString();
                    purdata.Tables.Add(tablename);

                    purdata.Tables[s].Columns.Add("SHIPMENT_NO");
                    purdata.Tables[s].Columns.Add("SENDER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("RECEIVER_ORG_CODE");
                    purdata.Tables[s].Columns.Add("SEND_BY");
                    purdata.Tables[s].Columns.Add("SENDER_TRUCK_NO");
                    purdata.Tables[s].Columns.Add("LR_NO");
                    purdata.Tables[s].Columns.Add("DRIVER_NAME");
                    purdata.Tables[s].Columns.Add("DRIVING_LICENCE_NO");
                    purdata.Tables[s].Columns.Add("TRANSPORT_CODE");
                    purdata.Tables[s].Columns.Add("FRIEGHT_CHARGES");

                    purdata.Tables[s].Columns.Add("CASE_NUMBER");
                    // purdata.Tables[s].Columns.Add("DISPATCH_WT");
                    purdata.Tables[s].Columns.Add("GRADE");
                    purdata.Tables[s].Columns.Add("LR_DATE");
                    purdata.Tables[s].Columns.Add("DISPATCH_TYPE");
                    purdata.Tables[s].Columns.Add("AWB_NO");
                    purdata.Tables[s].Columns.Add("IS_LP5_NOTE");
                    purdata.Tables[s].Columns.Add("REMARKS");


                    DataRow[] purrows = dtclstr.Select("SHIPMENT_NO ='" + orgdata.Rows[s]["SHIPMENT_NO"].ToString() + "'");
                    // DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER =" + Custid);
                    if (purrows.Length > 0)
                    {
                        foreach (DataRow r in purrows)
                        {
                            purdata.Tables[s].ImportRow(r);
                        }
                    }

                }


                //-----------Validation----------
                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    string currorg = "";

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno1 = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();
                        string strGrade = purdata.Tables[d].Rows[h]["GRADE"].ToString();

                        if (h == 0)
                        {
                            string shipmentno = purdata.Tables[d].Rows[0]["SHIPMENT_NO"].ToString();

                            currorg = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString();
                            string strToOrg = purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString();
                            string strPicker = purdata.Tables[d].Rows[0]["SEND_BY"].ToString();
                            string strLRNo = purdata.Tables[d].Rows[0]["LR_NO"].ToString();
                            string strTransportCode = purdata.Tables[d].Rows[0]["TRANSPORT_CODE"].ToString();

                            string strDispacthType = purdata.Tables[d].Rows[0]["DISPATCH_TYPE"].ToString();
                            string strAWBNo = purdata.Tables[d].Rows[0]["AWB_NO"].ToString();
                            string strIsLP5Note = purdata.Tables[d].Rows[0]["IS_LP5_NOTE"].ToString();
                            string strRemarks = purdata.Tables[d].Rows[0]["REMARKS"].ToString();
                            bool bolIsHeaderError = false;

                            string strHdrError = "";

                            string strQuery1 = "select * from mLocations where LocCode='" + currorg + "' and LocType='PSW'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strQuery1);
                            string strQuery2 = "select * from mLocations where LocCode='" + strToOrg + "' and LocType='FACTORY'";
                            DataTable ds2 = new DataTable();
                            ds2 = dlMgt.GetQueryResult(strQuery2);
                            string strQuery3 = "select * from mUsers where UserName='" + strPicker + "' and LocCode='" + currorg + "'";
                            DataTable ds3 = new DataTable();
                            ds3 = dlMgt.GetQueryResult(strQuery3);
                            string strQuery4 = "select * from mTransporters WHERE TransporterCode='" + strTransportCode + "'";
                            DataTable ds4 = new DataTable();
                            ds4 = dlMgt.GetQueryResult(strQuery4);

                            if (ds1.Rows.Count == 0)
                            {
                                data = "Error: Sender Location Type must be PSW's Location ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }

                            if (ds2.Rows.Count == 0)
                            {

                                data = "Error: To Location Type must be Factory's Location ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }
                            if (ds3.Rows.Count == 0)
                            {

                                data = "Error: Sender By (Picker code) doesn't exist ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }
                            if (ds4.Rows.Count == 0)
                            {

                                data = "Error: Transporter Code doesn't exist ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }
                            if (strLRNo.Length > 30)
                            {

                                data = "Error: LR No should not exist more than 30 characters ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            if (strDispacthType != "E" && strDispacthType != "F")
                            {

                                data = "Error: Dispatch Type must be E or F ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            if (strAWBNo.Length > 40)
                            {

                                data = "Error: ASW No should not exist more than 40 characters ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }
                            if (strIsLP5Note != "0" && strIsLP5Note != "1")
                            {

                                data = "Error: LP5 Note should be 0 or 1 ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            if (strRemarks.Length > 50)
                            {

                                data = "Error: Remarks should not exist more than 50 characters ||";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                            if (bolIsHeaderError == true)
                            {

                                data = "Error: Shipment no : " + shipmentno + " " + strHdrError;
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;
                            }

                        }



                        if (baleno1 == "" || baleno1.Trim().Length != 31)
                        {

                            json = JsonConvert.SerializeObject(data);
                            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                            jsonResult.MaxJsonLength = int.MaxValue;
                            return jsonResult;
                        }
                        else
                        {
                            bool bolIsDetailError = false;

                            strsql = "select MARKED_WT,PROCESS_STATUS,SUBINVENTORY_CODE,WMS_STATUS,FUMIGATION_STATUS,GRADE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno1 + "' and CURR_ORGN_CODE='" + currorg + "' AND STATUS='Y'";

                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count > 0)
                            {
                                string srksubcd;
                                mrkdwt = Convert.ToDouble(ds1.Rows[0][0]);
                                processts = Convert.ToString(ds1.Rows[0][1]);
                                srksubcd = Convert.ToString(ds1.Rows[0][2]);

                                string strWMSStatus = Convert.ToString(ds1.Rows[0][3]);
                                string strFumigationStatus = Convert.ToString(ds1.Rows[0][4]);
                                string strCaseGrade = Convert.ToString(ds1.Rows[0][5]);

                                string strErrMsg = "";

                                if (processts == "Y")
                                {

                                    data = "Error:  is using in another process || ";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                if (strWMSStatus != "Y")
                                {

                                    data = "Error: is not yet moved to WMS ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                if (strFumigationStatus.Trim() != "FUM")
                                {

                                    data = "Error: is not Fumigated or may be in Under Fumigation ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }

                                if (strCaseGrade != strGrade)
                                {

                                    data = "Error: is not match with stock grade (" + strCaseGrade + ") ||";
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }

                                string sCaseBarcode = baleno1;
                                string sCropYearCode = sCaseBarcode.Substring(0, 1);
                                string sCaseCode = sCaseBarcode.Substring(26, 1);
                                string sGradeCode = sCaseBarcode.Substring(2, 4);


                                if (bolIsDetailError == true)
                                {

                                    data = "Error: Case Number : " + baleno1 + " " + strErrMsg;
                                    json = JsonConvert.SerializeObject(data);
                                    jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                    jsonResult.MaxJsonLength = int.MaxValue;
                                    return jsonResult;
                                }
                                else
                                {
                                    //FactoryDispatchUpdate(baleno1, "Y");
                                }



                            }
                            else
                            {

                                data = "Error: Case Number : " + baleno1 + " does not exists";
                                json = JsonConvert.SerializeObject(data);
                                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                                jsonResult.MaxJsonLength = int.MaxValue;
                                return jsonResult;

                            }


                        }
                    }

                }

                //InsertData

                string sendorg;
                string temphdr;


                for (int d = 0; d < purdata.Tables.Count; d++)
                {
                    int cnt = 0;
                    /// Generating Header Id
                    temphdr = "PCD" + purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString() + purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + d;
                    sendorg = purdata.Tables[d].Rows[0]["SENDER_ORG_CODE"].ToString();
                    string recevorg = purdata.Tables[d].Rows[0]["RECEIVER_ORG_CODE"].ToString();
                    string sendby = purdata.Tables[d].Rows[0]["SEND_BY"].ToString();
                    string truckno = purdata.Tables[d].Rows[0]["SENDER_TRUCK_NO"].ToString();
                    string rcno = purdata.Tables[d].Rows[0]["LR_NO"].ToString();
                    string drivername = purdata.Tables[d].Rows[0]["DRIVER_NAME"].ToString();
                    string licenceno = purdata.Tables[d].Rows[0]["DRIVING_LICENCE_NO"].ToString();
                    string transportname = purdata.Tables[d].Rows[0]["TRANSPORT_CODE"].ToString();
                    string frightchrgs = purdata.Tables[d].Rows[0]["FRIEGHT_CHARGES"].ToString();
                    string senddate = purdata.Tables[d].Rows[0]["LR_DATE"].ToString();
                    string sndorgty = "";
                    string rcvorgtyp = "";

                    string strDispacthType = purdata.Tables[d].Rows[0]["DISPATCH_TYPE"].ToString();
                    string strAWBNo = purdata.Tables[d].Rows[0]["AWB_NO"].ToString();
                    string strIsLP5Note = purdata.Tables[d].Rows[0]["IS_LP5_NOTE"].ToString();
                    string strRemarks = purdata.Tables[d].Rows[0]["REMARKS"].ToString();


                    strsql = "Select O.ORGN_TYPE,O2.ORGN_TYPE FROM GPIL_ORGN_MASTER O,GPIL_ORGN_MASTER O2 WHERE O.ORGN_CODE='" + sendorg + "' AND O2.ORGN_CODE='" + recevorg + "'";
                    DataTable ds1 = new DataTable();
                    ds1 = dlMgt.GetQueryResult(strsql);
                    if (ds1.Rows.Count > 0)
                    {
                        sndorgty = Convert.ToString(ds1.Rows[0][0]);
                        rcvorgtyp = Convert.ToString(ds1.Rows[0][1]);
                    }
                    //Convert.ToDateTime(dateofpurch).ToString("yyyy-MM-dd")

                    strsql = "INSERT INTO [GPIL_SHIPMENT_HDR_TEMP]([SHIPMENT_NO],[SENDER_ORGN_CODE],[RECEIVER_ORGN_CODE],[SENDER_DATE],[SENT_BY],[SENDER_TRUCK_NO] ,[RC_NO],[DRIVER_NAME],[DRIVING_LICENCE_NO],[TRANSPORT_NAME],[FRIEGHT_CHARGES],[UOM],[CREATED_BY],[CREATED_DATE],[STATUS],[ATTRIBUTE2],[ATTRIBUTE3],[ATTRIBUTE4],[ATTRIBUTE5],[IS_WMS_SHIPMENT],TOT_NO_OF_BALES)";
                    strsql = strsql + " VALUES('" + temphdr + "','" + sendorg + "','" + recevorg + "','" + Convert.ToDateTime(senddate).ToString("yyyy-MM-dd HH:mm:ss") + "','" + sendby + "','" + truckno + "','" + rcno + "','" + drivername + "','" + licenceno + "','" + transportname + "','" + frightchrgs + "','KG','" + Session["userID"].ToString() + "',GETDATE(),'INT','" + sndorgty + "','" + rcvorgtyp + "','" + strDispacthType + "|" + strAWBNo + "|" + strIsLP5Note + "','" + strRemarks + "','F'," + purdata.Tables[d].Rows.Count + ")";
                    dlMgt.UpdateUsingExecuteNonQuery(strsql);

                    for (int h = 0; h < purdata.Tables[d].Rows.Count; h++)
                    {
                        string baleno = purdata.Tables[d].Rows[h]["CASE_NUMBER"].ToString();
                        // string diswt = purdata.Tables[d].Rows[h]["DISPATCH_WT"].ToString();
                        string fromsubcode = "";//purdata.Tables[d].Rows[h]["FROM_SUBINVENTORY_CODE"].ToString();
                        string tosubcode = "";// purdata.Tables[d].Rows[h]["TO_SUBINVENTORY_CODE"].ToString();
                                              // string grade = purdata.Tables[d].Rows[h]["GRADE"].ToString();
                        string loadtime = purdata.Tables[d].Rows[h]["LR_DATE"].ToString();
                        string markedwt = "0";
                        string diswt = "0";
                        string grade = string.Empty;
                        string orgntype = string.Empty;

                        strsql = "Select ORGN_TYPE from GPIL_ORGN_MASTER where ORGN_CODE='" + sendorg + "'";
                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            orgntype = Convert.ToString(ds1.Rows[0][0]);
                        }



                        strsql = "select MARKED_WT,PROCESS_STATUS,MARKED_WT,GRADE,SUBINVENTORY_CODE from GPIL_STOCK where GPIL_BALE_NUMBER='" + baleno + "'";


                        ds1 = dlMgt.GetQueryResult(strsql);
                        if (ds1.Rows.Count > 0)
                        {
                            markedwt = Convert.ToString(ds1.Rows[0][0]);
                            diswt = Convert.ToString(ds1.Rows[0][2]);
                            grade = Convert.ToString(ds1.Rows[0][3]);
                            fromsubcode = Convert.ToString(ds1.Rows[0][4]);
                            tosubcode = Convert.ToString(ds1.Rows[0][4]);
                        }


                        strsql = "INSERT INTO [GPIL_SHIPMENT_DTLS_TEMP]([SHIPMENT_NO],[DETAIL_ID],[GPIL_BALE_NUMBER],[MARKED_WT],[DISPATCH_WEIGHT],[FROM_SUBINVENTORY_CODE],[TO_SUBINVENTORY_CODE],[LOADING_DATETIME],[STATUS],[HEADER_STATUS],[CREATED_BY],[CREATED_DATE],[GRADE])";
                        strsql = strsql + "Values('" + temphdr + "','SH" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','" + markedwt.Trim() + "','" + diswt.Trim() + "','" + fromsubcode.Trim() + "','" + tosubcode.Trim() + "','" + Convert.ToDateTime(loadtime).ToString("yyyy-MM-dd HH:mm:ss") + "','INT','N','" + Session["userID"].ToString() + "',GETDATE(),'" + grade.Trim() + "')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "update GPIL_STOCK set STATUS='INT',PROCESS_STATUS='Y',BATCH_NO='" + temphdr + "',CURR_WT='" + diswt + "',LAST_UPDATED_BY='" + Session["userID"].ToString() + "',LAST_UPDATED_DATE=GETDATE() WHERE GPIL_BALE_NUMBER='" + baleno.Trim() + "'";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);

                        strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                        //strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") +d+ h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "',GETDATE(),'N')";
                        strsql = strsql + "Values('PC" + sendorg + DateTime.Now.ToString("yyyyMMddhhmmss") + d + h + "','" + baleno.Trim() + "','DISPATCH','" + temphdr.Trim() + "','" + diswt.Trim() + "','" + sendorg.Trim() + "','" + Convert.ToDateTime(loadtime).ToString("yyyy-MM-dd HH:mm:ss") + "','N')";
                        dlMgt.UpdateUsingExecuteNonQuery(strsql);


                    }



                    string SPName = "GPIL_SP_DISPATCH";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter[] pram = new SqlParameter[2];
                    pram[0] = (new SqlParameter("@DISPATCHNO", SqlDbType.NVarChar, 50));
                    pram[1] = (new SqlParameter("@RESULT", SqlDbType.NVarChar, 1));
                    pram[2] = (new SqlParameter("@OUTPUT", SqlDbType.NVarChar, 1000));
                    pram[0].Value = temphdr;
                    pram[1].Direction = ParameterDirection.Output;
                    pram[2].Direction = ParameterDirection.Output;
                    for (int i = 0; i < pram.Length; i++)
                    {
                        parameters.Add(pram[i]);
                    }
                    retVal = dlMgt.SP_ExecuteNonQuery(parameters, SPName);



                }



                data = "Success :DONE where shipmentno are" + retVal;
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;




            }
            catch (Exception ex)
            {
                data = "exception";
                json = JsonConvert.SerializeObject(data);
                jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }




    }
}