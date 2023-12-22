using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class FarmerBankDetailsLoader
    {
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string FARMER_CODE { get; set; }
        public string FARMER_NAME { get; set; }
        public string FARMER_FATHER_NAME { get; set; }

        public string MOBILE_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string BANK_ACCOUNT_NO { get; set; }
        public string BANK_NAME { get; set; }
        public string BRANCH_NAME { get; set; }

        public string IFSC_CODE { get; set; }
        public string INS_STS { get; set; }
        
    }

    public class ListFarmerBankDetails
    {
        public List<FarmerBankDetailsLoader> FarmerBanks { get; set; }
    }
}