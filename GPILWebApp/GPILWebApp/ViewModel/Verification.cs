using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class Verification
    {
        public int SNO { get; set; }
        public string CROP { get; set; }
        public string VARIETY { get; set; }

        
        public string PURCH_DOC_NO { get; set; }
        public string ORGN_CODE { get; set; }
        public string Total_Purchased_bale { get; set; }
        public string Total_Purchased_Quantity { get; set; }
        public string Total_Purchased_Value { get; set; }
    }
}