using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class InvoiceVerificationAuction
    {

        //<th>ORGN_CODE</th>          <th>TOTAL_BALES</th>       <th>TOTAL_QTY</th>
        //        <th>TOTAL_PURC_VAL</th>           <th>SERVICE_TB_TAX_VAL</th>         <th>SERVICE_CHARGE_VAL</th>
        //        <th>PATTA_CHARGE</th>           <th>INVOICE_VAL</th>                <th>Invoice_No</th>
        //        <th>Invoice_Date</th>


        public int SNO { get; set; }
        public string ORGN_CODE { get; set; }
        public string TOTAL_BALES { get; set; }
        public string TOTAL_QTY { get; set; }
        public string TOTAL_PURC_VAL { get; set; }
        public string SERVICE_TB_TAX_VAL { get; set; }
        public string SERVICE_CHARGE_VAL { get; set; }
        public string PATTA_CHARGE { get; set; }
        public string INVOICE_VAL { get; set; }
        public string Invoice_No { get; set; }
        public string Invoice_Date { get; set; }
    }
    public class ListInvoiceVerificationAuction
    {
        public List<InvoiceVerificationAuction> InvoiceVerificationAuctions { get; set; }
    }
}