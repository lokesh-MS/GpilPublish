using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class FarmerPurchaseCrossCheck
    {
        public int SNO { get; set; }
        public string HEADER_ID { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string Date { get; set; }
        public string TB_LOT_NO { get; set; }
        public string FARMER_CODE { get; set; }
        public string BUYER_GRADE { get; set; }
        public string DTL_CLASS_GRADE { get; set; }
        public string NET_WT { get; set; }
        public string STOCK_GRADE { get; set; }
        public string STOCK_NET_WT { get; set; }
        
    }
    public class ListFarmerPurchaseCrossCheck
    {
        public List<FarmerPurchaseCrossCheck> FarmerPurchaseCrossChecks { get; set; }
    }
}