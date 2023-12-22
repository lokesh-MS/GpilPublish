using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPILWebApp.Models;
using System.Web.Mvc;

namespace GPILWebApp.ViewModel
{
    public class LDReport
    {
        GREEN_LEAF_TRACEABILITYEntities db = new GREEN_LEAF_TRACEABILITYEntities();
        public List<GPIL_ORGN_MASTER> OrgnMasteres { get; set; }
        public List<GPIL_VARIETY_MASTER> VarietyMaster { get; set; }
        public List<GPIL_CROP_MASTER> CropMaster { get; set; }
        public List<GPIL_FARMER_MASTER> FarmerMaster { get; set; }
        public List<GPIL_TAP_FARM_PURCHS_DTLS> PurchaseDetails { get; set; }
        public GPIL_TAP_FARM_PURCHS_HDR PurchaseHeader { get; set; }
        IEnumerable<SelectListItem> OrgnMaster { get; set; }
        public List<SelectListItem> ItemGroup { get; set; }
        public int? ItemGroupcode { get; set; }
        public int? ItemGroupName { get; set; }


        //public LDReport()
        //{
        //    OrgnMaster = db.GPIL_ORGN_MASTER.Select(t=> select new SelectListItem
        //    {
        //        Text = t.name + " - " + t.Price + " TL",
        //        Value = t.Id.ToString()
        //    }).ToList();
        //}

    }
}