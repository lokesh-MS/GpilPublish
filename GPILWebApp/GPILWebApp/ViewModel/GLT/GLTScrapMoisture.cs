using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class GLTScrapMoisture
    {
        public string Crop { get; set; }
        public string Variety { get; set; }
        public string StripGrade { get; set; }
        public string ScrapGrade { get; set; }
        public string Date { get; set; }
      
        public string Time { get; set; }
        public string RunNo { get; set; }
        public string RunCaseNo { get; set; }
      
        public string AccCaseNo { get; set; }
        public string MoistureResult { get; set; }
        public string CaseTemp { get; set; }
    }
    public class ListGLTScrapMoisture
    {
        public List<GLTScrapMoisture> ScrapMoistures { get; set; }
    }
}