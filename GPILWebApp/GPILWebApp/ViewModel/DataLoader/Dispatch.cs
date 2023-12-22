using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class Dispatch
    {

        //select SHIPMENT_NO, SENDER_ORG_CODE, RECEIVER_ORG_CODE, SEND_BY, SENDER_TRUCK_NO, RC_NO, DRIVER_NAME, DRIVING_LICENCE_NO,
        //TRANSPORT_NAME, FRIEGHT_CHARGES, GPIL_BALE_NUMBER, FROM_SUBINVENTORY_CODE, TO_SUBINVENTORY_CODE, LOADING_DATETIME,'V' AS INS_STS

        public string SHIPMENT_NO { get; set; }
        public string SENDER_ORG_CODE { get; set; }
        public string RECEIVER_ORG_CODE { get; set; }
        public string SEND_BY { get; set; }
        public string SENDER_TRUCK_NO { get; set; }

        public string RC_NO { get; set; }
        public string DRIVER_NAME { get; set; }
        public string DRIVING_LICENCE_NO { get; set; }
        public string TRANSPORT_NAME { get; set; }
        public string FRIEGHT_CHARGES { get; set; }

        public string GPIL_BALE_NUMBER { get; set; }
        public string FROM_SUBINVENTORY_CODE { get; set; }
        public string TO_SUBINVENTORY_CODE { get; set; }
        public string LOADING_DATETIME { get; set; }
        public string INS_STS { get; set; }



    }

    public class ListDispatch
    {
        public List<Dispatch> Dispatchs { get; set; }
        public bool chkPSW { get; set; }
        
    }
}