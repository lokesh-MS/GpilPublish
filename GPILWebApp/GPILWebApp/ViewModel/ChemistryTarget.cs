using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class ChemistryTarget
    {
        public int SNO { get; set; }
        public string Crop { get; set; }
        public string Grade { get; set; }
        public string Variety { get; set; }
        public string Mark { get; set; }
        public string LSL { get; set; }
        public string ASL { get; set; }
        public string USL { get; set; }
        public string LCL { get; set; }
        public string ALCL { get; set; }
        public string UCL { get; set; }
        public string MoistureL { get; set; }
        public string MoistureU { get; set; }
    }
    public class ListChemistryTarget
    {
        public List<ChemistryTarget> ChemistryTargets { get; set; }
    }
}