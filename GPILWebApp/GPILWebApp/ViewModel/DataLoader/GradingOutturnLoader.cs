using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class GradingOutturnLoader
    {

        //string query = "select BATCH_NO,BALE_TYPE,PRODUCT_TYPE,GPIL_BALE_NUMBER,GRADE,WEIGHT,SUBINVENTORY_CODE,NO_OF_GRADERS,'V' AS INS_STS from [Sheet1$]";


        public string BATCH_NO { get; set; }
        public string BALE_TYPE { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public string GRADE { get; set; }
        public string WEIGHT { get; set; }
        public string SUBINVENTORY_CODE { get; set; }

        public string NO_OF_GRADERS { get; set; }
        public string INS_STS { get; set; }
        
    }

    public class ListGradingOutturnLoader
    {
        public List<GradingOutturnLoader> GradingOutturns { get; set; }
    }
}