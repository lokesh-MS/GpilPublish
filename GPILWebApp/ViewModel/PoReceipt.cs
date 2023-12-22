using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class PoReceipt
    {
        public string Crop { get; set; }
        public string ItemCode { get; set; }
        [Required(ErrorMessage = "Po Number is required")]
        public string PoNum { get; set; }
        public string Supplier { get; set; }
        public string Invoice { get; set; }
        public string InvoiceDate { get; set; }
        public string Qty { get; set; }
        public string Amt { get; set; }
        public string TotAmt { get; set; }
        public string CGSTPercent { get; set; }
        public string CGSTValue { get; set; }
        public string IGSTPercent { get; set; }
        public string IGSTValue { get; set; }
        public string TotalAmount { get; set; }
    }

    public class ListPoReceipt
    {
        public List<PoReceipt> PoReceipts { get; set; }
    }
}