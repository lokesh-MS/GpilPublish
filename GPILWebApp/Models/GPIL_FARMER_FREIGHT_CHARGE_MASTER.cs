
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
    
public partial class GPIL_FARMER_FREIGHT_CHARGE_MASTER
{

    public int SNO { get; set; }

    public string FREIGHT_ID { get; set; }

    public string CROP { get; set; }

    public string VARIETY { get; set; }

    public string VILLAGE_CODE { get; set; }

    public string ORGN_CODE { get; set; }

    public double FREIGHT_CHARGE { get; set; }

    public string REMARKS { get; set; }

    public Nullable<double> LAST_FREIGHT_CHARGE { get; set; }

    public string STATUS { get; set; }

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



    public virtual GPIL_CROP_MASTER GPIL_CROP_MASTER { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }

    public virtual GPIL_ORGN_MASTER GPIL_ORGN_MASTER { get; set; }

    public virtual GPIL_VARIETY_MASTER GPIL_VARIETY_MASTER { get; set; }

    public virtual GPIL_VILLAGE_MASTER GPIL_VILLAGE_MASTER { get; set; }

}
    public class ListFarmerFreightChargeMaster
    {
        public List<GPIL_FARMER_FREIGHT_CHARGE_MASTER> FarmerFreightChargeMasters { get; set; }
    }

}
