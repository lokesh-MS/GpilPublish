using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class SupplierVerification
    {

        public SupplierVerification()
        {
            this.GPIL_SUPP_PURCHS_HDR = new List<SelectListItem>();
            this.SupplierCodes = new List<SelectListItem>();
           
        }
        public List<SelectListItem> GPIL_SUPP_PURCHS_HDR { get; set; }
        public List<SelectListItem> SupplierCodes { get; set; }

        public int HEADER_ID { get; set; }
        public int SUPP_CODE { get; set; }
    }
}