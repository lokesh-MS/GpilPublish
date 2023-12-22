//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GPILWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GPIL_SHIPMENT_DTLS
    {
        public int SNO { get; set; }
        public string SHIPMENT_NO { get; set; }
        public string DETAIL_ID { get; set; }
        public string GPIL_BALE_NUMBER { get; set; }
        public Nullable<double> MARKED_WT { get; set; }
        public Nullable<double> DISPATCH_WEIGHT { get; set; }
        public Nullable<double> RECEIPT_WEIGHT { get; set; }
        public string FROM_SUBINVENTORY_CODE { get; set; }
        public string TO_SUBINVENTORY_CODE { get; set; }
        public string REMARKS { get; set; }
        public System.DateTime LOADING_DATETIME { get; set; }
        public Nullable<System.DateTime> UNLOADING_DATETIME { get; set; }
        public string STATUS { get; set; }
        public string HEADER_STATUS { get; set; }
        public string FLAG { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
        public byte[] LASTUPDATE { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public string WEIGHT_STATUS { get; set; }
        public string GRADE { get; set; }
    
        public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }
        public virtual GPIL_SUBINVENTORY GPIL_SUBINVENTORY { get; set; }
        public virtual GPIL_STOCK GPIL_STOCK { get; set; }
        public virtual GPIL_USER_MASTER GPIL_USER_MASTER1 { get; set; }
    }
}