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

    public partial class GPIL_SOIL_MASTER
    {
        public int SNO { get; set; }
        [Required(ErrorMessage = "SOIL TYPE IS REQUIRED")]
        public string SOIL_TYPE { get; set; }
        [Required(ErrorMessage = "SOIL NAME IS REQUIRED")]
        public string SOIL_NAME { get; set; }
        public string SOIL_DESC { get; set; }
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
    
        public virtual GPIL_USER_MASTER GPIL_USER_MASTER { get; set; }
        public virtual GPIL_USER_MASTER GPIL_USER_MASTER1 { get; set; }
        public virtual GPIL_USER_MASTER GPIL_USER_MASTER2 { get; set; }
        public virtual GPIL_USER_MASTER GPIL_USER_MASTER3 { get; set; }
    }
    public class ListSoilMaster
    {
        public List<GPIL_SOIL_MASTER> SoilMasters { get; set; }
    }
}
