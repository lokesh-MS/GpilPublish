using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class InvoiceVerification
    {
        public int SNO { get; set; }
        public string ORGN_CODE { get; set; }
    }
    public class ListInvoiceVerification
    {
        public List<InvoiceVerification> InvoiceVerifications { get; set; }
    }
}