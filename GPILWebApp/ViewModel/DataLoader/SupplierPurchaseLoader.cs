using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class SupplierPurchaseLoader
    {
        //GPIL_BALE_NUMBER,BUYER_GRADE,NET_WEIGHT,SUBINVENTORY_CODE,SUPP_CODE,RECEV_ORG_CODE,BUYER_CODE,LP4_NUMBER,CROP,VARIETY,'V' AS INS_STS
        public string GPIL_BALE_NUMBER { get; set; }
        public string BUYER_GRADE { get; set; }
        public string NET_WEIGHT { get; set; }
        public string SUBINVENTORY_CODE { get; set; }
        public string SUPP_CODE { get; set; }
        public string RECEV_ORG_CODE { get; set; }
        public string BUYER_CODE { get; set; }
        public string LP4_NUMBER { get; set; }
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string INS_STS { get; set; }
    }

    public class ListSupplierPurchase
    {
        public List<SupplierPurchaseLoader> SupplierPurchases { get; set; }
    }
}