using GPILWebApp.ViewModel;
using GPILWebApp.ViewModel.DataLoader;
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
    public class TapPurchaseLoaderController : Controller
    {
        // GET: TapPurchaseLoader
        public ActionResult Index()
        {
            return View();
        }

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
            purchasedata(generation);
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



        public bool Tapvalidate(ref DataSet purdata, ref DataTable dtclstr, ref string TAPerror)
        {
            int i = 0;
            DataLoaderManagement dlMgt = new DataLoaderManagement();

            string strsql = string.Empty;
            TAPerror = "Error :";
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


                        if (baleno1.Substring(0, 2) != crop1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + " Bale Number  and Corp Year MisMatch BaleNumber--" + baleno1;
                        }
                        else if (baleno1.Substring(2, 2) != variety1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + " Bale Number  and Variety MisMatch BaleNumber--" + baleno1;
                        }
                        else if (baleno1.Substring(4, 3) != orgcd1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + " Bale Number  and Orginization MisMatch BaleNumber--" + baleno1;
                        }
                        else if (tbgrade1.Substring(0, 2) != crop1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TB Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                        }
                        else if (tbgrade1.Substring(2, 2) != variety1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TB Grade and Variety MisMatch BaleNumber--" + baleno1;
                        }
                        else if (buyergrade1.Substring(0, 2) != crop1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Buyer Grade and Crop Year MisMatch BaleNumber--" + baleno1;
                        }
                        else if (buyergrade1.Substring(2, 2) != variety1)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Buyer Grade and Variety MisMatch BaleNumber--" + baleno1;
                        }

                        else if (baleno1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;

                        }
                        else if (tblot1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Lot Number is Empty for BaleNumber--" + baleno1;
                        }
                        else if (tbgrno1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TBGR number is Empty for BaleNumber--" + baleno1;
                        }
                        else if (tbgrade1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "TB Grade is Empty for BaleNumber--" + baleno1;
                        }
                        else if (buyergrade1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Buyer Grade is Empty for BaleNumber--" + baleno1;
                        }
                        else if (netwt1 == "" || Convert.ToDouble(netwt1) == 0)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Weight is Empty for BaleNumber--" + baleno1;
                        }
                        else if (Convert.ToDouble(netwt1) > 150)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Weight is more than 150 for BaleNumber--" + baleno1;
                        }
                        else if (rate1 == "" || Convert.ToDouble(rate1) == 0)
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Rate is Empty for BaleNumber--" + baleno1;
                        }
                        else if (rejests1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Rejection Status is Empty for BaleNumber--" + baleno1;
                        }
                        else if (pattacharge1 == "")
                        {
                            Tapupdate(baleno1, "N", ref dtclstr);
                            i = i + 1;
                            TAPerror = TAPerror + Environment.NewLine + "Patta Charge is Empty for BaleNumber--" + baleno1;
                        }
                        else
                        {

                            strsql = "select * from GPIL_ITEM_MASTER (NOLOCK) where ITEM_CODE ='" + tbgrade1.Trim() + "'";
                            DataTable ds1 = new DataTable();
                            ds1 = dlMgt.GetQueryResult(strsql);
                            if (ds1.Rows.Count == 0)
                            {
                                strsql = "select * from GPIL_STOCK (NOLOCK) where GPIL_BALE_NUMBER='" + baleno1.Trim() + "'";
                                DataTable ds2 = new DataTable();
                                ds2 = dlMgt.GetQueryResult(strsql);
                                if (ds2.Rows.Count == 0)
                                {
                                    Tapupdate(baleno1, "N", ref dtclstr);
                                    i = i + 1;
                                    TAPerror = TAPerror + Environment.NewLine + "Bale Already Purchased BaleNumber--" + baleno1;
                                }
                                else
                                {
                                    DataTable ds3 = new DataTable();
                                    ds3 = dlMgt.GetQueryResult(strsql);
                                    if (ds3.Rows.Count == 0)
                                    {
                                        Tapupdate(baleno1, "Y", ref dtclstr);
                                    }
                                    else
                                    {
                                        Tapupdate(baleno1, "N", ref dtclstr);
                                        i = i + 1;
                                        TAPerror = TAPerror + Environment.NewLine + "Buyer Grade Does not exit in master BaleNumber--" + baleno1;
                                    }

                                }

                            }
                            else
                            {
                                Tapupdate(baleno1, "N", ref dtclstr);
                                i = i + 1;
                                TAPerror = TAPerror + Environment.NewLine + "TB Grade Does not exit in master BaleNumber--" + baleno1;
                            }
                        }


                    }

                }
                if (i == 0)
                {
                    TAPerror = TAPerror + " NO ERROR";
                    //Errorlog err = new Errorlog();
                    //err.WriteErrorLog(TAPerror, "TAPPURCHASE_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                    return true;
                }
                else
                {
                    //Errorlog err = new Errorlog();
                    //errfile = err.WriteErrorLog(TAPerror, "TAPPURCHASE_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                    return false;
                }

            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
                //TAPerror = TAPerror + Environment.NewLine + ex.Message;
                //Errorlog err = new Errorlog();
                //errfile = err.WriteErrorLog(TAPerror, "TAPPURCHASE_" + Propertycls.EMPCODE, Server.MapPath("LOGFILES\\"));
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + ex.Message + "');", true);
                return false;

            }
            finally
            {
            }
        }


        public void Tapupdate(string gpilbaleno, string flg, ref DataTable dtclstr)
        {
            try
            {
                DataRow[] rows = dtclstr.Select("GPIL_BALE_NUMBER ='" + gpilbaleno + "'");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["INS_STS"] = flg;
                        dtclstr.AcceptChanges();
                        row.SetModified();
                    }
                }

                //GridViewSample.EditIndex = -1;
                //LoadData();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }



        public void purchasedata(Generation generation)
        {
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

                DataSet purdata = new DataSet();
                for (int s = 0; s < orgdata.Rows.Count; s++)
                {
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
                    if (Tapvalidate(ref purdata, ref dtclstr, ref TAPerror))
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

                            dlMgt.UpdateUsingExecuteNonQuery(strsql);

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

                                dlMgt.UpdateUsingExecuteNonQuery(strsql);

                                strsql = "INSERT INTO [GPIL_TAP_FARM_PURCHS_DTLS]([HEADER_ID],[GPIL_BALE_NUMBER],[TB_LOT_NO],[TBGR_NO],[TB_GRADE],[NET_WT],[RATE],[VALUE],[BUYER_GRADE] ,[CROP],[VARIETY],[SUBINVENTORY_CODE],[REJE_STATUS],[REJE_TYPE],[STATUS],[HEADER_STATUS] ,[PATTA_CHARGE],[SERVICE_CHARGE],[SERVICE_CHARGE_AMT],[SERVICE_TAX],[SERVICE_TAX_AMT],[CREATED_BY],[CREATED_DATE] ,[SH_ED_TAX],[ED_CESS_TAX])";
                                if (rejetype == "NONE")
                                {
                                    strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "',NULL,'" + status + "','N','" + pattacharge + "','" + servicechargeid + "',ROUND('" + servicechargeamt + "',2),'" + servicetaxid + "',ROUND('" + totalservicetaxamt + "',2),'" + Session["userID"].ToString() + "',GETDATE(),'" + servicechargeedshid + "','" + servicechargeshcessid + "')";
                                }
                                else
                                {
                                    strsql = strsql + "Values('" + headerid + "','" + baleno.Trim() + "','" + tblot.Trim() + "','" + tbgrno.Trim() + "','" + tbgrade.Trim() + "','" + netwt.Trim() + "','" + rate.Trim() + "','" + totalprice + "','" + buyergrade.Trim() + "','" + crop + "','" + variety + "','FW','" + rejests + "','" + rejetype + "','" + status + "','N','" + pattacharge + "','" + servicechargeid + "','" + servicechargeamt + "','" + servicetaxid + "','" + totalservicetaxamt + "','" + Session["userID"].ToString() + "',GETDATE(),'" + servicechargeedshid + "','" + servicechargeshcessid + "')";
                                }

                                dlMgt.UpdateUsingExecuteNonQuery(strsql);

                                strsql = "INSERT INTO GPIL_PROCESS_ORDER_CAPTURE(PROCESS_ID,GPIL_BALE_NUMBER,PROCESS_NAME,PROCESS_REF_ID,WEIGHT,ORGN_CODE,PROCESS_DATE,WEIGHMENT_TRACE)";
                                strsql = strsql + "Values('PC" + orgcd + DateTime.Now.ToString("yyyyMMddhhmmss") + h + "','" + baleno.Trim() + "','TAP Purchase','" + headerid.Trim() + "','" + netwt.Trim() + "','" + orgcd.Trim() + "','" + Convert.ToDateTime(dateofpurch) + "','N')";

                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }

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




    }
}