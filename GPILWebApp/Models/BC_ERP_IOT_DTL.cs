
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
    
public partial class BC_ERP_IOT_DTL
{

    public Nullable<int> TRANSACTION_ID { get; set; }

    public string ITEM_CODE { get; set; }

    public string FROM_INV_ORG { get; set; }

    public string TO_INV_ORG { get; set; }

    public string FROM_SUBINV { get; set; }

    public string TO_SUBINV { get; set; }

    public Nullable<decimal> FREIGHT_COST_PER_UNIT { get; set; }

    public Nullable<decimal> TRX_QTY { get; set; }

    public Nullable<System.DateTime> TRX_DATE { get; set; }

    public string TRX_REF { get; set; }

    public string FREIGHT_CODE { get; set; }

    public string SHIP_NUM { get; set; }

    public string WAYBILL { get; set; }

    public string ACCT_CODE { get; set; }

    public string LOT_NUM { get; set; }

    public string READ_FLAG { get; set; }

    public string ERROR_DESC { get; set; }

    public System.Guid rowguid { get; set; }

}

}
