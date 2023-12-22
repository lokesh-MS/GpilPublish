using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class GLTLaminaMoisture
    {

        public string Crop { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Grade { get; set; }
        public string SampleTime { get; set; }
        public string RunNo { get; set; }
        public string RunCaseNo { get; set; }


        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Results { get; set; }
        public string PackedTemp { get; set; }
        public string GrindingStartTIme { get; set; }
        public string GrindingEndTIme { get; set; }

    }

    public class ListGLTLaminaMoisture
    {
        public List<GLTLaminaMoisture> GLTLaminaMoistures { get; set; }
    }
}