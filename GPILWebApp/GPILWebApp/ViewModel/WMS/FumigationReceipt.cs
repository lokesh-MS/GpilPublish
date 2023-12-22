using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.WMS
{
    public class FumigationReceipt
    {
        public int SNO { get; set; }

        public string FUMIGATION_BATCH { get; set; }
        public string ORGN_CODE { get; set; }
        public string CASE_NUMBER { get; set; }
        public string FUMIGATED_BY { get; set; }

        public string FUMIGATION_STARTING_DATE { get; set; }
        public string FUMIGATION_DAYS_FOR_RUNPREIOD { get; set; }
        public string FUMIGATION_DAYS_FOR_EXPIRY { get; set; }
        public string REMARKS { get; set; }


        //public string ShipmentNumber { get; set; }
        //public string BaleNumber { get; set; }
        //public string OrgnCode { get; set; }
        //public string FumigationPeriod { get; set; }
        //public string Expiory { get; set; }

    }
    public class ListFumigationReceipt
    {
        public List<FumigationReceipt> FumigationReceipts { get; set; }
    }
}