
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
    
public partial class GPIL_SHIPMENT_HDR
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public GPIL_SHIPMENT_HDR()
    {

        this.GPIL_SHIP_MISS = new HashSet<GPIL_SHIP_MISS>();

        this.GPIL_SHIP_MISS1 = new HashSet<GPIL_SHIP_MISS>();

    }


    public int SNO { get; set; }

    public string SHIPMENT_NO { get; set; }

    public string WAYBILL { get; set; }

    public string SENDER_ORGN_CODE { get; set; }

    public string RECEIVER_ORGN_CODE { get; set; }

    public Nullable<int> SENDER_NO { get; set; }

    public Nullable<System.DateTime> SENDER_DATE { get; set; }

    public string SENT_BY { get; set; }

    public string SENDER_TRUCK_NO { get; set; }

    public string RECEIVER_TRUCK_NO { get; set; }

    public string RC_NO { get; set; }

    public string DRIVER_NAME { get; set; }

    public string DRIVING_LICENCE_NO { get; set; }

    public string TRANSPORT_NAME { get; set; }

    public string REDIRECT_STATUS { get; set; }

    public string REDIRECT_SHIPMENT_NO { get; set; }

    public Nullable<double> FRIEGHT_CHARGES { get; set; }

    public Nullable<int> TOT_NO_OF_BALES { get; set; }

    public Nullable<double> TOT_WEIGHT { get; set; }

    public string UOM { get; set; }

    public Nullable<int> RECEIVER_NO { get; set; }

    public Nullable<System.DateTime> RECEIVED_DATE { get; set; }

    public string RECEIVED_BY { get; set; }

    public Nullable<int> TOT_RECEIVED_BALES { get; set; }

    public string CREATED_BY { get; set; }

    public System.DateTime CREATED_DATE { get; set; }

    public string LAST_UPDATED_BY { get; set; }

    public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }

    public byte[] LASTUPDATE { get; set; }

    public string STATUS { get; set; }

    public string FLAG { get; set; }

    public string TEMP_REF { get; set; }

    public string ATTRIBUTE1 { get; set; }

    public string ATTRIBUTE2 { get; set; }

    public string ATTRIBUTE3 { get; set; }

    public string ATTRIBUTE4 { get; set; }

    public string ATTRIBUTE5 { get; set; }

    public string RECEV_WEIGH_TYPE { get; set; }

    public string IS_WMS_SHIPMENT { get; set; }

    public Nullable<int> PICKLIST_NO { get; set; }

    public Nullable<int> LP5_NO { get; set; }

    public string WMS_STATUS { get; set; }

    public string WMS_FLAG { get; set; }



    public virtual GPIL_ORGN_MASTER GPIL_ORGN_MASTER { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SHIP_MISS> GPIL_SHIP_MISS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SHIP_MISS> GPIL_SHIP_MISS1 { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER1 { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER2 { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER3 { get; set; }

}

}
