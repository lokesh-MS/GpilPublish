using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.RDLCMODELS.LD
{
    public class FarmerPurchase
    {
        //        FARM_NAME FARM_FATHER_NAME HEADER_ID DATE_OF_PURCH VILLAGE_NAME FARMER_CODE TB_LOT_NO
        
        public string FARM_NAME { get; set; }
        public string FARM_FATHER_NAME { get; set; }
        public string HEADER_ID { get; set; }
        public string DATE_OF_PURCH { get; set; }
        public string VILLAGE_NAME { get; set; }
        public string FARMER_CODE { get; set; }
        public string TB_LOT_NO { get; set; }

        //GPIL_BALE_NUMBER BANK_ACCOUNT_NO IFSC_CODE BANK_NAME BRANCH_NAME FREIGHT_CHARGE LOAN_AMOUNT
        
        public string GPIL_BALE_NUMBER { get; set; }
        public string BANK_ACCOUNT_NO { get; set; }
        public string IFSC_CODE { get; set; }
        public string BANK_NAME { get; set; }
        public string BRANCH_NAME { get; set; }
        public string FREIGHT_CHARGE { get; set; }
        public string LOAN_AMOUNT { get; set; }
        //ALERT_MSG ORGN_CODE CROP VARIETY
        public string ALERT_MSG { get; set; }
        public string ORGN_CODE { get; set; }
        public string CROP { get; set; }
        public string VARIETY { get; set; }




    }
}