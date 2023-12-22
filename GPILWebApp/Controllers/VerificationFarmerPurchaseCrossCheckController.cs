using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GPI;
using Newtonsoft.Json;
using System.ComponentModel;
using GPILWebApp.ViewModel.Verificationn;

namespace GPILWebApp.Controllers
{
    public class VerificationFarmerPurchaseCrossCheckController : Controller
    {


        private GREEN_LEAF_TRACEABILITYEntities _context;
        public VerificationFarmerPurchaseCrossCheckController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: VerificationFarmerPurchaseCrossCheck
        public ActionResult Index()
        {
            ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            ViewBag.GPIL_ORGN_MASTER = (from s in _context.GPIL_ORGN_MASTER where s.STATUS == "Y" select new { ORGN_CODE1 = s.ORGN_CODE + " - " + s.ORGN_NAME, s.ORGN_CODE }).ToList();
            return View();
        }


        public JsonResult FarmerPurchaseCrossCheck(string strCrop, string strVariety, string strOrgnCode)
        {
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            string strsql = "";
            DataSet ds = new DataSet();
            CommonManagement cMgt = new CommonManagement();
            string headerid = "";
            string lblMessage = string.Empty;
            DataTable dt = new DataTable();
            DataTable dtclr = new DataTable();

            DataSet dsTemp = new DataSet();

            strsql = "select PH.HEADER_ID,PH.PURCHASE_TYPE,PD.GPIL_BALE_NUMBER,PD.TB_LOT_NO,FARMER_CODE,PD.BUYER_GRADE,PD.ATTRIBUTE2 as DTL_CLASS_GRADE,PD.NET_WT, ST.GRADE AS STOCK_GRADE,ST.MARKED_WT AS STOCK_NET_WT,PH.DATE_OF_PURCH as Date  from GPIL_STOCK ST left outer join GPIL_TAP_FARM_PURCHS_DTLS PD on ST.GPIL_BALE_NUMBER = PD.GPIL_BALE_NUMBER left outer join GPIL_TAP_FARM_PURCHS_HDR PH on PH.HEADER_ID=PD.HEADER_ID where PH.PURCHASE_TYPE in ('TAP PURCHASE' ,'SUNDRY PURCHASE')";
            strsql = strsql + " and PH.ORGN_CODE='" + strOrgnCode + "' and PD.Crop= '" + strCrop + "' and PD.Variety ='" + strVariety + "'";
            strsql = strsql + " and (PD.ATTRIBUTE2 != ST.GRADE Or ISNULL(PD.NET_WT,0)!=ISNULL(ST.MARKED_WT,0)) and   PH.HEADER_ID=ST.BATCH_NO ";
            dtclr = cMgt.GetQueryResult(strsql);
            if (dtclr.Rows.Count > 0)
            {
                dt.Merge(dtclr);
            }

            strsql = "select CH.BATCH_NO as HEADER_ID,CH.RECIPE_CODE,CD.GPIL_BALE_NUMBER, '' as TB_LOT_NO,'' as FARMER_CODE,ISSUED_GRADE as Buyer_Grade,CLASSIFICATION_GRADE as DTL_CLASS_GRADE,cd.MARKED_WT as NET_WT,ST.GRADE AS STOCK_GRADE ,ST.MARKED_WT AS STOCK_NET_WT,CLASSIFICATION_DATE as Date from GPIL_STOCK ST ";
            strsql = strsql + " left outer join GPIL_CLASSIFICATION_DTLS CD on ST.GPIL_BALE_NUMBER = CD.GPIL_BALE_NUMBER left outer join GPIL_CLASSIFICATION_HDR CH on CH.BATCH_NO=CD.BATCH_NO where CH.RECIPE_CODE='CLASSIFICATION' ";
            strsql = strsql + " and CH.ORGN_CODE='" + strOrgnCode + "' and  ST.Crop= '" + strCrop + "' and ST.BATCH_NO ='" + strVariety + "'";
            strsql = strsql + " and (CD.CLASSIFICATION_GRADE != ST.GRADE Or ISNULL(CD.MARKED_WT,0)!=ISNULL(ST.MARKED_WT,0))  and ST.BATCH_NO = CH.BATCH_NO AND  (SUBSTRING(CH.BATCH_NO ,9,2) Not in ('51'))  ";
            dtclr = cMgt.GetQueryResult(strsql);
            if (dtclr.Rows.Count > 0)
            {
                dt.Merge(dtclr);
            }

            if (dt.Rows.Count > 0)
            {
                headerid = dt.Rows[0]["HEADER_ID"].ToString();
                //lblRowNo.Text = ds.Tables[0].Rows.Count.ToString();
                //lblMessage = "Success: DATA";

            }
            else
            {
                //GridViewSamp.Visible = false;
                lblMessage = "Error: No Records found";
                //lblRowNo.Text = "";
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
            json = JsonConvert.SerializeObject(dt);
            jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
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
        public JsonResult VerifyFarmerPurchaseStockComplete(ListFarmerPurchaseCrossCheck LFPCC)
        {
            int z = 0;
            String strsql = string.Empty;
            String query = string.Empty;
            DataTable dtclstr = ToDataTable1(LFPCC.FarmerPurchaseCrossChecks);

            string lblMessage = string.Empty;
            string data = String.Empty, json = String.Empty;
            JsonResult jsonResult;
            try
            {
                for (int i = 0; i < dtclstr.Rows.Count; i++)
                {
                    try
                    {

                        string strHeaderID, strBaleNumber, strDate, strTbLotNumber, strFarmerCode, strBuyerGrade, strClassGrade;
                        string strNetWeight, strStockGrade, strStockNetWeight;

                        strHeaderID = dtclstr.Rows[i]["HEADER_ID"].ToString();
                        strBaleNumber = dtclstr.Rows[i]["GPIL_BALE_NUMBER"].ToString();

                        strDate = dtclstr.Rows[i]["Date"].ToString();
                        strTbLotNumber = dtclstr.Rows[i]["TB_LOT_NO"].ToString();
                        strFarmerCode = dtclstr.Rows[i]["FARMER_CODE"].ToString();
                        strBuyerGrade = dtclstr.Rows[i]["BUYER_GRADE"].ToString();
                        strClassGrade = dtclstr.Rows[i]["DTL_CLASS_GRADE"].ToString();
                        strNetWeight = dtclstr.Rows[i]["NET_WT"].ToString();
                        strStockGrade = dtclstr.Rows[i]["STOCK_GRADE"].ToString();
                        strStockNetWeight = dtclstr.Rows[i]["STOCK_NET_WT"].ToString();

                        

                        //query = "INSERT INTO [dbo].[GPIL_Chemical_Targets] ([Crop] ,[Variety] ,[Grade] ,[Mark],[LSL] ,[AVE] ,[USL] ,[LCL],[AVEC],[UCL]   ,[LSLMoisture] ,[USLMoisture])  VALUES ";
                        //query += " ('" + strCrop + "','" + strVariety + "','" + strGrade + "','" + strMark + "'," + Convert.ToDouble(strLSL) + "";
                        //query += "," + Convert.ToDouble(strASL) + "," + Convert.ToDouble(strUSL) + "," + Convert.ToDouble(strLCL) + "," + Convert.ToDouble(strALCL) + "," + Convert.ToDouble(strUCL) + " ";
                        //query += "," + Convert.ToDouble(strMoistureL) + "," + strMoistureU + ")";
                        //cMgt.UpdateUsingExecuteNonQuery(query);
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


    }
}