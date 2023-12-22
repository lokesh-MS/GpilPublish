
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
    using System.ComponentModel.DataAnnotations;

    public partial class GPIL_TAP_FARM_PURCHS_DTLS
{

   public int SNO { get; set; }
     [Required (ErrorMessage = "HEADER_ID IS REQUIRED")]
 public string HEADER_ID { get; set; }
     [Required(ErrorMessage = "BALE_NUMBER IS REQUIRED")]
 public string GPIL_BALE_NUMBER { get; set; }
     [Required(ErrorMessage = "TB_LOT_NO IS REQUIRED")]
 public string TB_LOT_NO { get; set; }
     [Required(ErrorMessage = "TBGR_NO IS REQUIRED")]
 public string TBGR_NO { get; set; }
     [Required(ErrorMessage = "TB_GRADE IS REQUIRED")]
 public string TB_GRADE { get; set; }
     [Required(ErrorMessage = "NET_WT IS REQUIRED")]
     public Nullable<double> NET_WT { get; set; }
     [Required(ErrorMessage = "RATE IS REQUIRED")]
     public Nullable<double> RATE { get; set; }
 public Nullable<double> VALUE { get; set; }
     [Required(ErrorMessage = "BUYER_GRADE IS REQUIRED")]
     public string BUYER_GRADE { get; set; }
 public string CROP { get; set; }
 public string VARIETY { get; set; }
 public string SUBINVENTORY_CODE { get; set; }
 public string FARMER_CODE { get; set; }
 public string REJE_STATUS { get; set; }
 public string REJE_TYPE { get; set; }
 public Nullable<double> QR_QUANTITY { get; set; }
 public Nullable<double> PR_RATE { get; set; }
 public string REMARKS { get; set; }
 public string STATUS { get; set; }
 public string HEADER_STATUS { get; set; }
 public string FLAG { get; set; }
     [Required(ErrorMessage = "PATTA_CHARGE IS REQUIRED")]
     public Nullable<double> PATTA_CHARGE { get; set; }
 public Nullable<double> SERVICE_CHARGE { get; set; }
 public Nullable<double> SERVICE_CHARGE_AMT { get; set; }
 public Nullable<double> SERVICE_TAX { get; set; }
 public Nullable<double> SERVICE_TAX_AMT { get; set; }
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
 public string SH_ED_TAX { get; set; }
 public string ED_CESS_TAX { get; set; }



    public virtual GPIL_CROP_MASTER GPIL_CROP_MASTER { get; set; }

    public virtual GPIL_ITEM_MASTER GPIL_ITEM_MASTER { get; set; }

    public virtual GPIL_STOCK GPIL_STOCK { get; set; }

    public virtual GPIL_SUBINVENTORY GPIL_SUBINVENTORY { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }

    public virtual GPIL_TAP_FARM_PURCHS_HDR GPIL_TAP_FARM_PURCHS_HDR { get; set; }

    public virtual GPIL_VARIETY_MASTER GPIL_VARIETY_MASTER { get; set; }

}

}