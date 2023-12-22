using GPILWebApp.Models;
using GPILWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace GPILWebApp.Controllers
{
    public class FarmerWisePurchaseSummaryLDDController : Controller
    {

        private GREEN_LEAF_TRACEABILITYEntities _context;
        public FarmerWisePurchaseSummaryLDDController()
        {
            _context = new GREEN_LEAF_TRACEABILITYEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: FarmerWisePurchaseSummaryLDD
        public ActionResult Index()
        {
            
            //ViewBag.GPIL_CROP_MASTER = (from c in _context.GPIL_CROP_MASTER where c.STATUS == "Y" select new { CROP1 = c.CROP + " - " + c.CROP_YEAR, c.CROP }).ToList();
            //ViewBag.GPIL_VARIETY_MASTER = (from v in _context.GPIL_VARIETY_MASTER where v.STATUS == "Y" select new { VARIETY1 = v.VARIETY + " - " + v.VARIETY_NAME, v.VARIETY }).ToList();
            return View();
        }

        [HttpGet]
        // GET: LD/Crop
        public ActionResult Crop()
        {
            ViewBag.GPIL_CROP_MASTER = (from s in _context.GPIL_CROP_MASTER where s.STATUS == "Y" select new { CROPS = s.CROP + " - " + s.CROP_YEAR, s.CROP }).ToList();
            string json = JsonConvert.SerializeObject(ViewBag.GPIL_CROP_MASTER.ToArray());
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        // GET: LD/Vareities
        public ActionResult Vareities()
        {
            ViewBag.GPIL_CROP_MASTER = (from s in _context.GPIL_VARIETY_MASTER where s.STATUS == "Y" select new { VARIETIES = s.VARIETY + " - " + s.VARIETY_NAME, s.VARIETY }).ToList();
            string json = JsonConvert.SerializeObject(ViewBag.GPIL_CROP_MASTER.ToArray());
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        DataSet ds = new DataSet();
        RDLCReport rdlcReport = new RDLCReport();
        [HttpGet]

        public JsonResult FarmerwisePurchaseSummaryDetails(string strCrop, string strVariety)
        {
            try
            {
                ds = rdlcReport.FarmerPurchaseSummary(strCrop, strVariety);
                var data = ds;
                var json = JsonConvert.SerializeObject(data);
                var jsonResult = Json(json.ToString(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch
            {

            }
            return Json(ds);

        }

        [HttpGet]
        public ActionResult FarmerWisePurchaseSummary(string strCrop, string strVariety)
        {

            LDDManagement lddMgt = new LDDManagement();
            DataTable dtclstr = new DataTable();
            string query = "";
            query = "SELECT ROW_NUMBER() OVER(ORDER BY FARMER_CODE) AS SNO,FARMER_CODE,F.FARM_NAME,F.FARM_FATHER_NAME AS FATHER_NAME,FARM_ADDRESS1 AS VILLAGE,F.BANK_NAME AS BANK_NAME,'AC NO :'+ F.BANK_ACCOUNT_NO AS AccNo ,F.BRANCH_NAME ,F.IFSC_CODE , COUNT(GPIL_BALE_NUMBER) AS BALES,ROUND(SUM(NET_WT),1) AS QUANTITY ,ROUND(SUM(NET_WT*RATE),2) AS TOTAL_VALUE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_FARMER_MASTER F WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='" + strCrop + "' AND H.VARIETY='" + strVariety + "' AND H.STATUS IN ('P','N') AND D.REJE_STATUS='OK' AND H.PURCHASE_TYPE = 'SUNDRY PURCHASE' AND F.FARM_CODE=D.FARMER_CODE GROUP BY FARMER_CODE,F.FARM_NAME,F.FARM_FATHER_NAME,FARM_ADDRESS1,F.BANK_NAME, F.BANK_ACCOUNT_NO  ,F.BRANCH_NAME ,F.IFSC_CODE union all select ROW_NUMBER() OVER(ORDER BY F.FARM_CODE) AS SNO,f.FARM_CODE,F.FARM_NAME,F.FARM_FATHER_NAME AS FATHER_NAME,FARM_ADDRESS1 AS VILLAGE,F.BANK_NAME AS BANK_NAME,'AC NO :'+ F.BANK_ACCOUNT_NO AS AccNo ,F.BRANCH_NAME ,F.IFSC_CODE ,'0' AS BALES,'0' AS QUANTITY ,'0' AS TOTAL_VALUE from  GPIL_FARMER_CROP_HISTORY H,GPIL_FARMER_MASTER F where f.FARM_CODE = H.FARM_CODE and CROP='" + strCrop + "' AND VARIETY='" + strVariety + "' and H.FARM_CODE not in(SELECT FARMER_CODE FROM GPIL_TAP_FARM_PURCHS_DTLS D,GPIL_TAP_FARM_PURCHS_HDR H,GPIL_FARMER_MASTER F WHERE H.HEADER_ID=D.HEADER_ID AND H.CROP='19' AND H.VARIETY='11' AND H.STATUS IN ('P','N') AND D.REJE_STATUS='OK' AND H.PURCHASE_TYPE = 'SUNDRY PURCHASE' AND F.FARM_CODE=D.FARMER_CODE GROUP BY FARMER_CODE,F.FARM_NAME,F.FARM_FATHER_NAME,FARM_ADDRESS1,F.BANK_NAME, F.BANK_ACCOUNT_NO  ,F.BRANCH_NAME ,F.IFSC_CODE)";
            dtclstr = lddMgt.GetQueryResult(query);
            string json = JsonConvert.SerializeObject(dtclstr);
            return Json(json, JsonRequestBehavior.AllowGet);


        }


    }
}