using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class ItemMaster
    {
        public int SNO { get; set; }
       
        public string ITEM_CODE { get; set; }
      
        public string ITEM_CODE_GROUP { get; set; }
       
        public string ITEM_GROUP { get; set; }
       
        public string ITEM_TYPE { get; set; }
        
        public string ITEM_DESC { get; set; }
        
        public string CROP { get; set; }
      
        public string VARIETY { get; set; }
     
        public string COST_CATEGORY { get; set; }
      
        public string ORGN_TYPE { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
      
        public string STATUS { get; set; }
        public string FLAG { get; set; }
        public byte[] LASTUPDATE { get; set; }
        public string ATTRIBUTE1 { get; set; }
       
        public string ATTRIBUTE2 { get; set; }
       
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
    }
    public class ListItemMaster
    {
        public List<ItemMaster> ItemMasters { get; set; }
    }
}