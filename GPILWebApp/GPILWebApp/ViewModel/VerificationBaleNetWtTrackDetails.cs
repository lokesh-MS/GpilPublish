using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class VerificationBaleNetWtTrackDetails
    {
        
        public int SNO { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string ORGN_CODE { get; set; }
        public string PROCESS { get; set; }
        public string REPORT_REF { get; set; }
        public string CREATED_DATE { get; set; }
      //  public string SQL_STATUS { get; set; }
        public string ERP_STATUS { get; set; }
        public string SHIPMENT_WT { get; set; }

        public string STOCK_WT { get; set; }
        
       // public Nullable<double> SHIPMENT_WT { get; set; }
      //  public Nullable<double> STOCK_WT { get; set; }

        
    }
    public class ListBaleNetWtTrackDetails
    {
        public List<VerificationBaleNetWtTrackDetails> BaleNetWtTracks { get; set; }
    }

}