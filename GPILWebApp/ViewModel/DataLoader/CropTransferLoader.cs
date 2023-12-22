using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class CropTransferLoader
    {
        //select BATCH_NO, ORGN_CODE, CLASSIFIER_CODE, RECIPE_CODE, DATE_OF_OPERATION, OLD_BALE_NUMBER, NEW_BALE_NUMBER, NEW_GRADE, SUBINVENTORY_CODE,'V' AS INS_STS from[Sheet1$]
        public string BATCH_NO { get; set; }
        public string ORGN_CODE { get; set; }
        public string CLASSIFIER_CODE { get; set; }
        public string RECIPE_CODE { get; set; }
        public string DATE_OF_OPERATION { get; set; }
        public string OLD_BALE_NUMBER { get; set; }
        public string NEW_BALE_NUMBER { get; set; }
        public string NEW_GRADE { get; set; }
        public string SUBINVENTORY_CODE { get; set; }
        public string INS_STS { get; set; }
    }

    public class ListCropTransferLoader
    {
        public List<CropTransferLoader> CropTransfers { get; set; }
    }
}