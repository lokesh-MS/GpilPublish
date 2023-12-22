
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
    
public partial class GPIL_CROP_TRANS_DTLS
{

    public int SNO { get; set; }

    public string BATCH_NO { get; set; }

    public string DETAIL_ID { get; set; }

    public string TRANSFER_TYPE { get; set; }

    public string OLD_BALE_NUMBER { get; set; }

    public string OLD_CROP { get; set; }

    public string OLD_VARIETY { get; set; }

    public string OLD_GRADE { get; set; }

    public Nullable<double> MARKED_WT { get; set; }

    public string FROM_SUBINVENTORY_CODE { get; set; }

    public string NEW_BALE_NUMBER { get; set; }

    public string NEW_CROP { get; set; }

    public string NEW_VARIETY { get; set; }

    public string NEW_GRADE { get; set; }

    public string TO_SUBINVENTORY_CODE { get; set; }

    public string CREATED_BY { get; set; }

    public System.DateTime CREATED_DATE { get; set; }

    public string LAST_UPDATED_BY { get; set; }

    public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }

    public string STATUS { get; set; }

    public string HEADER_STATUS { get; set; }

    public string FLAG { get; set; }

    public byte[] LASTUPDATE { get; set; }

    public string REMARKS { get; set; }

    public string ATTRIBUTE1 { get; set; }

    public string ATTRIBUTE2 { get; set; }

    public string ATTRIBUTE3 { get; set; }

    public string ATTRIBUTE4 { get; set; }

    public string ATTRIBUTE5 { get; set; }



    public virtual GPIL_CROP_MASTER GPIL_CROP_MASTER { get; set; }

    public virtual GPIL_CROP_TRANS_HDR GPIL_CROP_TRANS_HDR { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }

    public virtual GPIL_SUBINVENTORY GPIL_SUBINVENTORY { get; set; }

    public virtual GPIL_STOCK GPIL_STOCK { get; set; }

    public virtual GPIL_STOCK GPIL_STOCK1 { get; set; }

    public virtual GPIL_ITEM_MASTER GPIL_ITEM_MASTER { get; set; }

    public virtual GPIL_VARIETY_MASTER GPIL_VARIETY_MASTER { get; set; }

    public virtual GPIL_SUBINVENTORY GPIL_SUBINVENTORY1 { get; set; }

}

}
