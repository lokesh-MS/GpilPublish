using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class QualityLab
    {
        public int SNO { get; set; }
        public string Crop { get; set; }
        public string Grade { get; set; }
        public string Variety { get; set; }
        public string DOP { get; set; }
        public string Mark { get; set; }
        public string Product { get; set; }
        public string ExportType { get; set; }
        public string Type { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string SourceOrg { get; set; }
        public string SRunNo { get; set; }
        public string ERunNo { get; set; }
        public string NIC { get; set; }
        public string TRS { get; set; }
        public string CL { get; set; }
        public string Moisture { get; set; }
    }

    public class ListQualityLab
    {
        public List<QualityLab> QualityLabs { get; set; }
    }
}