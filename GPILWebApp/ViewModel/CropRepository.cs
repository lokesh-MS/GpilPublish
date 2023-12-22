using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class ClusterRepository
    {

        public virtual string Name { get; set; }
        public virtual List<GPIL_CROP_MASTER> Details { get; protected set; }
        public string CLUSTER_CODE { get; set; }

        public string CLUSTER_NAME { get; set; }

        public string CREATED_BY { get; set; }

        public System.DateTime CREATED_DATE { get; set; }

        public string STATUS { get; set; }
    }
}