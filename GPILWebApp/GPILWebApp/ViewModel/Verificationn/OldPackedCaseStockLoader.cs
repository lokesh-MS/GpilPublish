using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class OldPackedCaseStockLoader
    {
        //@*SELECT CROP, VARIETY, GPIL_BALE_NUMBER, GRADE, MARKED_WT, ORGN_CODE,'V' AS INS_STS from[Sheet1$]";*@
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string GRADE { get; set; }
        public string MARKED_WT { get; set; }
        public string ORGN_CODE { get; set; }
        public string INS_STS { get; set; }

        public class ListOldPackedStock
        {
            public List<OldPackedCaseStockLoader> OldPackedStocks { get; set; }
        }

    }
}