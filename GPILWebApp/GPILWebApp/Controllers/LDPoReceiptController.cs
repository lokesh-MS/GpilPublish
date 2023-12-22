using GPI;
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
    public class LDPoReceiptController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        // GET: LDPoReceipt
        public ActionResult Index()
        {
            return View();
        }

        // GET: LDPoReceipt/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LDPoReceipt/Create
        public ActionResult Create()
        {
            //ViewBag.GPIL_Leaf_Item_Master = (from c in db.GPIL_Leaf_Item_Master select new { c.Crop }).Distinct();
            //ViewBag.GPIL_ITEM_MASTER = (from c in db.GPIL_ITEM_MASTER select new { c.ITEM_CODE }).Distinct();
            //ViewBag.GPIL_SUPPLIER_MASTER = (from c in db.GPIL_SUPPLIER_MASTER select new { c.SUPP_NAME }).Distinct();
            return View();
        }

        [HttpGet]
        // GET: SupplierVerification/SupplierCode
        public ActionResult GetCropDetails()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement ldMgt = new CommonManagement();
            try
            {

                strsql = "SELECT Distinct[Crop] FROM [dbo].[GPIL_Leaf_Item_Master]";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
            }

            return Json(ds);
        }
        [HttpGet]
        public ActionResult GetItemDetails()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement ldMgt = new CommonManagement();
            try
            {

                strsql = "SELECT [ItemCode],[ItemName] FROM [dbo].[GPIL_Leaf_Item_Master]";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
            }

            return Json(ds);
        }

        [HttpGet]
        public ActionResult GetSupplierDetails()
        {
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            string strsql;
            string json = "";
            CommonManagement ldMgt = new CommonManagement();
            try
            {

                strsql = "SELECT [SUPP_NAME] FROM [dbo].[GPIL_SUPPLIER_MASTER]";
                ds1 = ldMgt.GetQueryResult(strsql);
                ds1.TableName = "Table";
                var data = ds1;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
            }

            return Json(ds);
        }

        //, string crop
        //GetQuantity = "select [Supplier] from [dbo].[GPIL_Leaf_PO_Header] where [PoNo] ='" + poNumber + "' and [Crop]='" + crop + "'";
        //        dt = cMgt.GetQueryResult(GetQuantity);
        [HttpPost]
        public ActionResult CheckPOAvailability(string poNumber, string itemCode, string invoiceNumber)
        {
            DataTable dt = new DataTable();
            CommonManagement cMgt = new CommonManagement();
            System.Threading.Thread.Sleep(200);
            string GetQuantity = "Select Count(*) from [dbo].[GPIL_Leaf_PO_Details] where   [PoNo]='" + poNumber + "' and [ItemCode]='" + itemCode + "' and [InvoiceNo]='" + invoiceNumber + "'";
            dt = cMgt.GetQueryResult(GetQuantity);
            if (dt.Rows.Count != 0)
            {
                if (poNumber == null && itemCode == null && invoiceNumber == null)
                {
                    return Json(0);
                }
                MasterManagement MstrMgt = new MasterManagement();
                DataTable dtclstr = new DataTable();
                GetQuantity = "select [Qty],[UnitPrice],[UnitTotal],[CGSTPercent],[CGSTAmt],[IGSTPercent],[IGStAmt],[Total],[DatePo]  from [dbo].[GPIL_Leaf_PO_Details] where  [PoNo]='" + poNumber + "' and [ItemCode]='" + itemCode + "' and [InvoiceNo]='" + invoiceNumber + "'";
                dt = cMgt.GetQueryResult(GetQuantity);
                string json = JsonConvert.SerializeObject(dt);
                return Json(json, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0);
            }
        }


        [HttpPost]
        public ActionResult GetSupplierDetails(string poNumber, string itemCode, string invoiceNumber)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strsql;
            string json = "";
            CommonManagement cMgt = new CommonManagement();
            try
            {

                strsql = "SELECT [SUPP_NAME] FROM [dbo].[GPIL_SUPPLIER_MASTER]";
                dt = cMgt.GetQueryResult(strsql);
                dt.TableName = "Table";
                var data = dt;
                json = JsonConvert.SerializeObject(data);

                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
            }

            return Json(ds);
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
        // POST: LDPoReceipt/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreatePoReceipt(ListPoReceipt LPR)
        {
            DataTable dt = new DataTable();
            CommonManagement cMgt = new CommonManagement();
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                List<string> lstQry = new List<string>();
                DataTable dtGridLst = ToDataTable(LPR.PoReceipts);
                string strQry = "";
                for (int s = 0; s < dtGridLst.Rows.Count; s++)
                {
                    string Crop = dtGridLst.Rows[s]["Crop"].ToString();
                    string ItemCode = dtGridLst.Rows[s]["ItemCode"].ToString();
                    string PoNum = dtGridLst.Rows[s]["PoNo"].ToString();
                    string Supplier = dtGridLst.Rows[s]["Supplier"].ToString();
                    string Invoice = dtGridLst.Rows[s]["InvoiceNo"].ToString();
                    string InvoiceDate = dtGridLst.Rows[s]["DatePo"].ToString();
                    string Qty = dtGridLst.Rows[s]["Qty"].ToString();
                    string Amt = dtGridLst.Rows[s]["UnitPrice"].ToString();
                    string TotAmt = dtGridLst.Rows[s]["UnitTotal"].ToString();                    
                    string CGSTPercent = dtGridLst.Rows[s]["CGSTPercent"].ToString();
                    string CGSTValue = dtGridLst.Rows[s]["CGSTAmt"].ToString();
                    string IGSTPercent = dtGridLst.Rows[s]["IGSTPercent"].ToString();
                    string IGSTValue = dtGridLst.Rows[s]["IGStAmt"].ToString();
                    string TotalAmount = dtGridLst.Rows[s]["Total"].ToString();

                   
                    string query = "";
                    query = " Select  Count(*) from [dbo].[GPIL_Leaf_Po_Details]  where  [PoNo]='" + PoNum + "' and [ItemCode]='" + ItemCode + "'";
                    dt = cMgt.GetQueryResult(query);
                    if (dt.Columns.Contains("ErrorMessage"))
                    {
                        data = "Error: " + dt.Rows[0]["ErrorMessage"].ToString();
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    if (dt.Rows.Count == 0)
                    {

                        GPIL_ITEM_MASTER obj = null;
                        obj = new GPIL_ITEM_MASTER();

                        //strQry = "INSERT INTO [dbo].[GPIL_ITEM_MASTER] ([ITEM_CODE],[ITEM_CODE_GROUP],[ITEM_GROUP],[ITEM_TYPE],[ITEM_DESC],[CROP],[VARIETY],[COST_CATEGORY],[ORGN_TYPE],[CREATED_BY],[CREATED_DATE],[ATTRIBUTE3],[STATUS]) ";
                        //strQry = strQry + "VALUES('" + ITEM_CODE + "','" + ITEM_CODE_GROUP + "','" + ITEM_GROUP + "','" + ITEM_TYPE + "','" + ITEM_DESC + "','" + CROP + "','" + VARIETY + "','" + COST_CATEGORY + "','" + ORGN_TYPE + "','" + Session["userID"].ToString() + "',getdate(),'" + ATTRIBUTE3 + "','" + STATUS + "')";
                        lstQry.Add(strQry);

                    }
                    else
                    {

                        //data = "Error: Item Code " + ITEM_CODE + " is already exist So please check and import";
                        json = JsonConvert.SerializeObject(data);
                        jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                }


            }
            catch
            {
                return View();
            }
            return RedirectToAction("Create");
        }

        // GET: LDPoReceipt/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LDPoReceipt/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LDPoReceipt/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LDPoReceipt/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
