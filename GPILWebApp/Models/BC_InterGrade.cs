
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
    
public partial class BC_InterGrade
{

    public string BatchNo { get; set; }

    public string OldGrade { get; set; }

    public string NewGrade { get; set; }

    public string OldCaseBarCode { get; set; }

    public string NewCaseBarCode { get; set; }

    public Nullable<decimal> NetWeight { get; set; }

    public string LocCode { get; set; }

    public Nullable<System.DateTime> TransactionOn { get; set; }

    public string Status { get; set; }

    public string Read_Flag_Prodn { get; set; }

    public Nullable<System.DateTime> Read_dt_Prodn { get; set; }

    public string Error_Prodn { get; set; }

    public string Attribute1_Prodn { get; set; }

    public Nullable<int> Attribute2_Prodn { get; set; }

    public string Read_Flag_Test { get; set; }

    public Nullable<System.DateTime> Read_dt_Test { get; set; }

    public string Error_Test { get; set; }

    public string Attribute1_test { get; set; }

    public Nullable<int> Attribute2_test { get; set; }

    public System.Guid rowguid { get; set; }



    public virtual mGrade mGrade { get; set; }

    public virtual mGrade mGrade1 { get; set; }

    public virtual mLocation mLocation { get; set; }

}

}