using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class PackedCaseReceipt
    {
        //select SHIPMENT_NO, CASE_NUMBER as GPIL_BALE_NUMBER,RECEIPT_DATE AS UNLOADING_DATETIME,'V' AS INS_STS from[Sheet1$]
        public string SHIPMENT_NO { get; set; }
        public string CASE_NUMBER { get; set; }
        public string RECEIPT_DATE { get; set; }
        public string INS_STS { get; set; }
        
    }

    public class ListPackedCaseReceipt
    {
        public List<PackedCaseReceipt> PackedCaseReceipts { get; set; }
    }
}