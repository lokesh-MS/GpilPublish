using GPI;
using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class AddOldStockMasterController : Controller
    {
        private GREEN_LEAF_TRACEABILITYEntities _context;


        public AddOldStockMasterController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        // GET: AddOldStockMaster
        public ActionResult AddOldStockIndex()
        {
            //ddlSubInventory.DataSource = (from S in test.GPIL_SUBINVENTORYs where S.STATUS == "Y" select new { S.SUB_INV_CODE }).Distinct();
            ViewBag.GPIL_SUBINVENTORYs = (from S in _context.GPIL_SUBINVENTORY where S.STATUS == "Y" select new { S.SUB_INV_CODE }).Distinct();
            return View();
        }
        CommonManagement cMgt = new CommonManagement();
        DataTable dt = new DataTable();
        string lblMessage = string.Empty;
        string data = String.Empty, json = String.Empty;
        JsonResult jsonResult;

        [HttpGet]
        public JsonResult ValidateBaleNumberOldStockDetails(string strBaleNumber, string strCrop, string strVariety)
        {
            try
            {
                DataTable dt1 = new DataTable();
                string strQry = "SELECT * FROM GPIL_STOCK(NOLOCK) WHERE GPIL_BALE_NUMBER ='" + strBaleNumber + "'";
                dt1 = cMgt.GetQueryResult(strQry);
                DataTable dt2 = new DataTable();
                string strQry1 = "SELECT * FROM GPIL_CROP_MASTER(NOLOCK) WHERE CROP ='" + strCrop + "'";
                dt2 = cMgt.GetQueryResult(strQry1);

                DataTable dt3 = new DataTable();
                string strQry2 = "SELECT* FROM GPIL_VARIETY_MASTER(NOLOCK) WHERE VARIETY = '" + strVariety + "'";
                dt3 = cMgt.GetQueryResult(strQry2);

                DataTable dt4 = new DataTable();
                string strQry3 = "SELECT * FROM GPIL_ORGN_MASTER(NOLOCK) WHERE ORGN_CODE = '" + strBaleNumber.Substring(4, 3) + "'";
                dt4 = cMgt.GetQueryResult(strQry3);

                if (strBaleNumber.Length != 13)
                {
                    lblMessage = "Error: Please Select Valid Bale Number";
                }
                else if (dt1.Rows.Count > 0)
                {

                    lblMessage = "Error: Bale Number is alreay exist, Please Enter New Bale Number";

                }
                else if (strCrop != strBaleNumber.Substring(0, 2))
                {
                    lblMessage = "Error: Crop is not exist, Please Enter Valid Bale Number";

                }
                else if (strVariety != strBaleNumber.Substring(2, 2))
                {
                    lblMessage = "Error: Variety is not exist, Please Enter Valid Bale Number";

                }
                else if (dt2.Rows.Count == 0)
                {
                    lblMessage = "Error: Crop is not exist, Please Enter Valid Bale Number";

                }
                else if (dt3.Rows.Count == 0)
                {
                    lblMessage = "Error: Variety is not exist, Please Enter Valid Bale Number";

                }
                else if (dt4.Rows.Count == 0)
                {
                    lblMessage = "Error: Organization is not exist, Please Enter Valid Bale Number";

                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
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

        [HttpGet]
        public JsonResult ValidateGradeOldStockDetails(string strGrade, string strBaleNumber, string strCrop, string strVariety)
        {
            try
            {
                DataTable dt1 = new DataTable();
                string strQry = "SELECT * FROM GPIL_ITEM_MASTER(NOLOCK) WHERE ITEM_CODE ='" + strGrade + "'";
                dt1 = cMgt.GetQueryResult(strQry);
               
                if (strGrade.Length <= 4)
                {
                    lblMessage = "Error: Please Enter Valid Grade";
                }
                else if (strGrade.ToUpper().Substring(0, 1) != "L" && strCrop != strGrade.Substring(0, 2))
                {
                    lblMessage = "Error: Crop of Bale Number & Grade are not matching , Please Enter Valid Grade";

                }
                else if (strGrade.Trim().ToUpper().Substring(0, 1) != "L" && strVariety != strGrade.Substring(2, 2))
                {
                    lblMessage = "Error: Variety of Bale Number & Grade are not matching , Please Enter Valid Grade";

                }
                else if (dt1.Rows.Count == 0)
                {
                    lblMessage = "Error: Grade is not exist, Please Enter Valid Bale Number";

                }

            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
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

        //BaleNumber, Crop, Variety, Grade, MarkedWeight, SunInventory

        [HttpPost]
        public JsonResult InsertOldStockDetails(string strBaleNumber, string strCrop, string strVariety, string strGrade, string strMarkedWeight, string strSubinventory)
        {
            if (strMarkedWeight.Length == 0)
            {
                lblMessage = "Error: Please Enter Marked Weight";


            }
            //else if (Double.TryParse(txtMarkedWeight.Text, out dOutput) == false)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Validation Failed!", "alert('Please Enter Marked Weight');", true);
            //    txtMarkedWeight.Focus();
            //    return false;

            //}
            else if (strSubinventory.Length <= 0)
            {
                lblMessage = "Error: Please Select Sub-Inventory";

            }
            else
            {


                string strProductType = "G";

                if (strGrade.Substring(0, 1).ToUpper() == "L")
                {
                    strProductType = "PC";
                }
                string strQuery = "INSERT INTO GPIL_STOCK (GPIL_BALE_NUMBER,GRADE,MARKED_WT,CURR_WT,ORIGN_LOCN,ORIGN_ORGN_CODE,CURR_LOCN,CURR_ORGN_CODE,CROP,VARIETY,SUBINVENTORY_CODE,CREATED_BY,CREATED_DATE,WEIGHT_FLAG,BALE_CARD_TYPE,PRODUCT_TYPE,PROCESS_STATUS,BATCH_NO,STATUS,ATTRIBUTE5) VALUES ('" + strBaleNumber.ToUpper() + "','" + strGrade.ToUpper() + "','" + Convert.ToDouble(strMarkedWeight).ToString("0.0") + "','" + Convert.ToDouble(strMarkedWeight).ToString("0.0") + "','LOC1','" + strBaleNumber.ToUpper().Substring(4, 3) + "','LOC1','" + strBaleNumber.ToUpper().Substring(4, 3) + "','" + strBaleNumber.ToUpper().Substring(0, 2) + "','" + strBaleNumber.ToUpper().Substring(2, 2) + "','" + strSubinventory + "','5653',GETDATE(),'N','GLT','" + strProductType + "','N','MANUAL','Y','MANUAL')";
                bool b = cMgt.UpdateUsingExecuteNonQuery(strQuery);

                if (b)
                {
                    lblMessage = "Success: Inserted SucessFully";
                }
                else
                {
                    lblMessage = "Error: Inserted SucessFully";
                }
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




        //[HttpGet]
        //public ActionResult Validation(string strGrade)
        //{


        //    string query = "";
        //    query = "SELECT * FROM GPIL_ORGN_MASTER(NOLOCK) WHERE ORGN_CODE ='" + strGrade + "'";
        //    dtclstr = cMgt.GetQueryResult(query);
        //    string json = JsonConvert.SerializeObject(dtclstr);
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //    //return Json(new { result = "Redirect", url = Url.Action("FarmerPurchasePendingBalesIndex", "LDD") });

        //}
    }
}