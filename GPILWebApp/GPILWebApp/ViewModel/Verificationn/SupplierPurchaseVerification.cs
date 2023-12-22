using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class SupplierPurchaseVerification
    {
        public string SNO { get; set; }
        public string HEADER_ID { get; set; }
        public string LP4_NUMBER { get; set; }
        public string SUPP_CODE { get; set; }

    }

    public class ListSupplierVerification
    {
        public List<SupplierPurchaseVerification> SupplierVerifications { get; set; }
    }
}