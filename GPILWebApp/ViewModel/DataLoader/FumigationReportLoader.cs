using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class FumigationReportLoader
    {

        //select FUMIGATION_BATCH,ORGN_CODE,CASE_NUMBER,FUMIGATED_BY,FUMIGATION_STARTING_DATE,FUMIGATION_DAYS_FOR_RUNPREIOD,FUMIGATION_DAYS_FOR_EXPIRY,REMARKS from [Sheet1$]
        public string FUMIGATION_BATCH { get; set; }
        public string ORGN_CODE { get; set; }
        public string CASE_NUMBER { get; set; }
        public string FUMIGATED_BY { get; set; }

        public string FUMIGATION_STARTING_DATE { get; set; }
        public string FUMIGATION_DAYS_FOR_RUNPREIOD { get; set; }
        public string FUMIGATION_DAYS_FOR_EXPIRY { get; set; }
        public string REMARKS { get; set; }
    }
    public class ListFumigationReport
    {
        public List<FumigationReportLoader> FumigationReports { get; set; }
    }
}