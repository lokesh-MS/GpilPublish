using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class GradeTransferLoader
    {

        //select BATCH_NO, ORGN_CODE, CLASSIFIER_CODE, GRADE_TRANSFER_DATE, RECIPE_CODE, GPIL_BALE_NUMBER, TRANSFERED_GRADE,'V' AS INS_STS from[Sheet1$]
        public string BATCH_NO { get; set; }
        public string ORGN_CODE { get; set; }
        public string CLASSIFIER_CODE { get; set; }
        public string GRADE_TRANSFER_DATE { get; set; }
        public string RECIPE_CODE { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string TRANSFERED_GRADE { get; set; }
        public string INS_STS { get; set; }

    }

    public class ListGradingTransferLoader
    {
        public List<GradeTransferLoader> GradingTransfers { get; set; }
    }
}