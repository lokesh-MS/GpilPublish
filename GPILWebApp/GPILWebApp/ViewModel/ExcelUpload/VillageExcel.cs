using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.ExcelUpload
{
    public class VillageExcel
    {
        private string villageCode;
        private string villageName;
        private string clusterCode;
        private string status;

        public string VillageCode
        {
            get
            {
                return villageCode;
            }

            set
            {
                villageCode = value;
            }
        }

        public string VillageName
        {
            get
            {
                return villageName;
            }

            set
            {
                villageName = value;
            }
        }

        public string ClusterCode
        {
            get
            {
                return clusterCode;
            }

            set
            {
                clusterCode = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}