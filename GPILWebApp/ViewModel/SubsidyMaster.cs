using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class SubsidyMaster
    {
        public string Crop { get; set; }
        public string ItemCategory { get; set; }
        public string PMI { get; set; }
        public string Farmer { get; set; }
    }



    public class ListSubsidyMaster
    {
        public List<SubsidyMaster> SubsidyMasters { get; set; }
    }

}