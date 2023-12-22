using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class ThreshingIssueLoader
    {
        //select BATCH_NO, ORGN_CODE, RECIPE_CODE, SHIFT, SHIFT_INCHARGE, DATE_OF_OPERATION, CROP, VARIETY, GPIL_BALE_NUMBER, SUBINVENTORY_CODE, ASCERTAIN_WEIGHT,'V' AS INS_STS from[Sheet1$]*@
        public string BATCH_NO { get; set; }
        public string ORGN_CODE { get; set; }
        public string RECIPE_CODE { get; set; }
        public string SHIFT { get; set; }
        public string SHIFT_INCHARGE { get; set; }
        public string DATE_OF_OPERATION { get; set; }
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string SUBINVENTORY_CODE { get; set; }

        public string ASCERTAIN_WEIGHT { get; set; }
        public string INS_STS { get; set; }

    }

    public class ListThreshingIssueLoader
    {
        public List<ThreshingIssueLoader> ThreshingIssues { get; set; }
    }
}