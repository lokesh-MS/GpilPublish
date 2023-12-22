using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.DataLoader
{
    public class PackedCaseDispatchLoader
    {

        //select SHIPMENT_NO, SENDER_ORG_CODE, RECEIVER_ORG_CODE, SEND_BY, SENDER_TRUCK_NO, LR_NO, DRIVER_NAME, DRIVING_LICENCE_NO, TRANSPORT_CODE, FRIEGHT_CHARGES, CASE_NUMBER,
        //GRADE, LR_DATE, DISPATCH_TYPE, AWB_NO, IS_LP5_NOTE, REMARKS,'V' AS INS_STS from[Sheet1$]
        public string SHIPMENT_NO { get; set; }
        public string SENDER_ORG_CODE { get; set; }
        public string RECEIVER_ORG_CODE { get; set; }
        public string SEND_BY { get; set; }
        public string SENDER_TRUCK_NO { get; set; }
        public string LR_NO { get; set; }
        public string DRIVER_NAME { get; set; }
        public string DRIVING_LICENCE_NO { get; set; }
        public string TRANSPORT_CODE { get; set; }
        public string FRIEGHT_CHARGES { get; set; }
        public string CASE_NUMBER { get; set; }
        public string GRADE { get; set; }
        public string LR_DATE { get; set; }
        public string DISPATCH_TYPE { get; set; }
        public string AWB_NO { get; set; }
        public string IS_LP5_NOTE { get; set; }
        public string REMARKS { get; set; }
        public string INS_STS { get; set; }
    }

    public class ListPackedCaseDispatchLoader
    {
        public List<PackedCaseDispatchLoader> PackedCaseDispatchs { get; set; }
    }
}