
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
    
public partial class GPIL_CROP_TRANS_HDR
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public GPIL_CROP_TRANS_HDR()
    {

        this.GPIL_CROP_TRANS_DTLS = new HashSet<GPIL_CROP_TRANS_DTLS>();

    }


    public int SNO { get; set; }

    public string BATCH_NO { get; set; }

    public string ORGN_CODE { get; set; }

    public string CLASSIFIER_NAME { get; set; }

    public string RECIPE_CODE { get; set; }

    public System.DateTime DATE_OF_OPERATION { get; set; }

    public int TOT_NO_OF_BALES { get; set; }

    public string CREATED_BY { get; set; }

    public System.DateTime CREATED_DATE { get; set; }

    public string LAST_UPDATED_BY { get; set; }

    public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }

    public string STATUS { get; set; }

    public string FLAG { get; set; }

    public byte[] LASTUPDATE { get; set; }

    public string REMARKS { get; set; }

    public string TEMP_REF { get; set; }

    public string ATTRIBUTE1 { get; set; }

    public string ATTRIBUTE2 { get; set; }

    public string ATTRIBUTE3 { get; set; }

    public string ATTRIBUTE4 { get; set; }

    public string ATTRIBUTE5 { get; set; }

    public string IS_PACKED_TRANSFER { get; set; }

    public string PACKED_TYPE { get; set; }

    public string WMS_STATUS { get; set; }

    public string WMS_FLAG { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CROP_TRANS_DTLS> GPIL_CROP_TRANS_DTLS { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER1 { get; set; }

    public virtual GPIL_ORGN_MASTER GPIL_ORGN_MASTER { get; set; }

    public virtual GPIL_OPERATION_RECIPE GPIL_OPERATION_RECIPE { get; set; }

}

}
