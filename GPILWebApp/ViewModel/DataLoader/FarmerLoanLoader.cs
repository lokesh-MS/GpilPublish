using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class FarmerLoanLoader
    {
        public string CROP { get; set; }
        public string VARIETY { get; set; }
        public string FARMER_CODE { get; set; }
        public string LOAN_AMOUNT { get; set; }
        public string INS_STS { get; set; }
    }
    public class ListFarmerLoan
    {
        public List<FarmerLoanLoader> FarmerLoans { get; set; }
    }
}