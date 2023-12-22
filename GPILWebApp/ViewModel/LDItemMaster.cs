using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class LDItemMaster
    {
        public string Crop { get; set; }
        public string Item { get; set; }
        public string ItemCode { get; set; }
        public string UOM { get; set; }
    }
    public class ListLDItemMaster
    {
        public List<LDItemMaster> LDItemMasters { get; set; }
    }
}