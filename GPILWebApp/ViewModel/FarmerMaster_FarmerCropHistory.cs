using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class FarmerMaster_FarmerCropHistory
    {
        public GPIL_FARMER_MASTER User { get; set; }
        public GPIL_FARMER_CROP_HISTORY Login { get; set; }

        //public IEnumerable<GPIL_FARMER_MASTER> FarmerMasters { get; set; }
        //public IEnumerable<GPIL_FARMER_CROP_HISTORY> FarmerCropHistoryMasters { get; set; }
    }

   
}