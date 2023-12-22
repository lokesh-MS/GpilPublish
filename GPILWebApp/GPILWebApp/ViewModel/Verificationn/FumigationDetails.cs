using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class FumigationDetails
    {

        public string GPIL_BALE_NUMBER { get; set; }
        public string ORGN_CODE { get; set; }
        public string FUM_DATE { get; set; }

        public string INS_STS { get; set; }
       
    }

    public class ListFumigationDetails
    {
        public List<FumigationDetails> FumigDtls { get; set; }
    }
}