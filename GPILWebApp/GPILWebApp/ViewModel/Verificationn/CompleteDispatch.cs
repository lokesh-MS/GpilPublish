using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel.Verificationn
{
    public class CompleteDispatch
    {
        public int SNo { get; set; }
        public string SHIPMENT_NO { get; set; }
        public string SENDER_ORGN_CODE { get; set; }
        public string SENDER_TRUCK_NO { get; set; }
        public string SENDER_DATE { get; set; }
        public string FLAG { get; set; }
       
    }
    public class ListCompleteDispatch
    {
        public List<CompleteDispatch> CompleteDispatchs { get; set; }
    }
}