using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class ThreshingOutturnLoader
    {
        //select BATCH_NO,BALE_TYPE,PRODUCT_TYPE,GPIL_BALE_NUMBER,GRADE,NET_WT,TARE_WT,SUBINVENTORY_CODE,'V' AS INS_STS from [Sheet1$]
        public string BATCH_NO { get; set; }
        public string BALE_TYPE { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string GRADE { get; set; }
        public string NET_WT { get; set; }
        public string TARE_WT { get; set; }
        public string SUBINVENTORY_CODE { get; set; }
        public string INS_STS { get; set; }
        

    }

    public class ListThreshingOutturnLoader
    {
        public List<ThreshingOutturnLoader> ThreshingOutturns { get; set; }
    }
}