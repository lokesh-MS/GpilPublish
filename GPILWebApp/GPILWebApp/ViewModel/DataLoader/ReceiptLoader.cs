using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class ReceiptLoader
    {
        //select SHIPMENT_NO,GPIL_BALE_NUMBER,UNLOADING_DATETIME,'V' AS INS_STS from [Sheet1$]
        public string SHIPMENT_NO { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string UNLOADING_DATETIME { get; set; }
        public string INS_STS { get; set; }
        
    }

    public class ListReceipt
    {
        public List<ReceiptLoader> Receipts { get; set; }
    }
}