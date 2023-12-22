
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
    
public partial class mLocation
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public mLocation()
    {

        this.BC_Batch_Header = new HashSet<BC_Batch_Header>();

        this.BC_InterGrade = new HashSet<BC_InterGrade>();

        this.tDispatchNotes = new HashSet<tDispatchNote>();

        this.tDispatchNotes1 = new HashSet<tDispatchNote>();

        this.mStacks = new HashSet<mStack>();

    }


    public string LocCode { get; set; }

    public string LocName { get; set; }

    public string LocType { get; set; }

    public Nullable<bool> Active { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PinCode { get; set; }

    public string ContactNo { get; set; }

    public string Email { get; set; }

    public string CreatedBy { get; set; }

    public Nullable<System.DateTime> CreatedOn { get; set; }

    public string ModifiedBy { get; set; }

    public Nullable<System.DateTime> ModifiedOn { get; set; }

    public System.Guid rowguid { get; set; }

    public byte[] LASTUPDATE { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<BC_Batch_Header> BC_Batch_Header { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<BC_InterGrade> BC_InterGrade { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tDispatchNote> tDispatchNotes { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tDispatchNote> tDispatchNotes1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<mStack> mStacks { get; set; }

    public virtual tMaxLP5 tMaxLP5 { get; set; }

}

}
