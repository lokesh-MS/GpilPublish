
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

    public partial class GPIL_SUBINVENTORY
    {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public GPIL_SUBINVENTORY()
    {

        this.GPIL_CLASSIFICATION_DTLS = new HashSet<GPIL_CLASSIFICATION_DTLS>();

        this.GPIL_CLASSIFICATION_DTLS_TEMP = new HashSet<GPIL_CLASSIFICATION_DTLS_TEMP>();

        this.GPIL_CROP_TRANS_DTLS = new HashSet<GPIL_CROP_TRANS_DTLS>();

        this.GPIL_CROP_TRANS_DTLS1 = new HashSet<GPIL_CROP_TRANS_DTLS>();

        this.GPIL_CROP_TRANS_DTLS_TEMP = new HashSet<GPIL_CROP_TRANS_DTLS_TEMP>();

        this.GPIL_CROP_TRANS_DTLS_TEMP1 = new HashSet<GPIL_CROP_TRANS_DTLS_TEMP>();

        this.GPIL_GRADING_DTLS = new HashSet<GPIL_GRADING_DTLS>();

        this.GPIL_GRADING_DTLS_TEMP = new HashSet<GPIL_GRADING_DTLS_TEMP>();

        this.GPIL_SHIPMENT_DTLS = new HashSet<GPIL_SHIPMENT_DTLS>();

        this.GPIL_SHIPMENT_DTLS_TEMP = new HashSet<GPIL_SHIPMENT_DTLS_TEMP>();

        this.GPIL_SO_RESERVATION_DTLS = new HashSet<GPIL_SO_RESERVATION_DTLS>();

        this.GPIL_SO_RESERVATION_DTLS_TEMP = new HashSet<GPIL_SO_RESERVATION_DTLS_TEMP>();

        this.GPIL_SUPP_PURCHS_DTLS = new HashSet<GPIL_SUPP_PURCHS_DTLS>();

        this.GPIL_TAP_FARM_PURCHS_DTLS = new HashSet<GPIL_TAP_FARM_PURCHS_DTLS>();

        this.GPIL_THRESH_RECON_DTLS_1 = new HashSet<GPIL_THRESH_RECON_DTLS_1>();

        this.GPIL_THRESH_RECON_DTLS_1_TEMP = new HashSet<GPIL_THRESH_RECON_DTLS_1_TEMP>();

    }


        //public int SNO { get; set; }

        //public string SUB_INV_CODE { get; set; }

        //public string SUB_INV_DESC { get; set; }

        //public string CREATED_BY { get; set; }

        //public System.DateTime CREATED_DATE { get; set; }

        //public string LAST_UPDATED_BY { get; set; }

        //public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }

        //public string STATUS { get; set; }

        //public string FLAG { get; set; }

        //public byte[] LASTUPDATE { get; set; }

        //public string ATTRIBUTE1 { get; set; }

        //public string ATTRIBUTE2 { get; set; }

        //public string ATTRIBUTE3 { get; set; }

        //public string ATTRIBUTE4 { get; set; }

        //public string ATTRIBUTE5 { get; set; }
        public int SNO { get; set; }
        [Required(ErrorMessage = "SUB_INV_CODE IS REQUIRED")]
        public string SUB_INV_CODE { get; set; }
        [Required(ErrorMessage = "SUB_INV_DESC IS REQUIRED")]
        public string SUB_INV_DESC { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
        [Required(ErrorMessage = "STATUS IS REQUIRED")]
        public string STATUS { get; set; }
        public string FLAG { get; set; }
        public byte[] LASTUPDATE { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CLASSIFICATION_DTLS> GPIL_CLASSIFICATION_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CLASSIFICATION_DTLS_TEMP> GPIL_CLASSIFICATION_DTLS_TEMP { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CROP_TRANS_DTLS> GPIL_CROP_TRANS_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CROP_TRANS_DTLS> GPIL_CROP_TRANS_DTLS1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CROP_TRANS_DTLS_TEMP> GPIL_CROP_TRANS_DTLS_TEMP { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_CROP_TRANS_DTLS_TEMP> GPIL_CROP_TRANS_DTLS_TEMP1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_GRADING_DTLS> GPIL_GRADING_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_GRADING_DTLS_TEMP> GPIL_GRADING_DTLS_TEMP { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SHIPMENT_DTLS> GPIL_SHIPMENT_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SHIPMENT_DTLS_TEMP> GPIL_SHIPMENT_DTLS_TEMP { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SO_RESERVATION_DTLS> GPIL_SO_RESERVATION_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SO_RESERVATION_DTLS_TEMP> GPIL_SO_RESERVATION_DTLS_TEMP { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }

    public virtual GPIL_USER_MASTER GPIL_USER_MASTER1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_SUPP_PURCHS_DTLS> GPIL_SUPP_PURCHS_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_TAP_FARM_PURCHS_DTLS> GPIL_TAP_FARM_PURCHS_DTLS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_THRESH_RECON_DTLS_1> GPIL_THRESH_RECON_DTLS_1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GPIL_THRESH_RECON_DTLS_1_TEMP> GPIL_THRESH_RECON_DTLS_1_TEMP { get; set; }

}

    public class ListSubInventory
    {
        public List<GPIL_SUBINVENTORY> SubInventoryMasters { get; set; }
    }
}
