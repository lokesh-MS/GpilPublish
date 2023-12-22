using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class FarmerPurchaseLoader
    {

        public string GPIL_BALE_NUMBER { get; set; }
        public string LOT_NO { get; set; }
        public string FARMER_CODE { get; set; }
        public string FORM_GRADE { get; set; }
        public string NET_WT { get; set; }
        public string RATE { get; set; }
        public string BUYER_GRADE { get; set; }
        public string REJE_STATUS { get; set; }
        public string REJE_TYPE { get; set; }
        public string ORGN_CODE { get; set; }
        public string BUYER_CODE { get; set; }
        public string PURCH_DOC_NO { get; set; }
        public string PURCHASE_DATE { get; set; }
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        
        public string INS_STS { get; set; }

        public class ListFarmerPurchase
        {
            public List<FarmerPurchaseLoader> FarmerPurchases { get; set; }
        }
    }
}