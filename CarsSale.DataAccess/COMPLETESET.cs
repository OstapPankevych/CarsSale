//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarsSale.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class COMPLETESET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMPLETESET()
        {
            this.VEHICLs = new HashSet<VEHICL>();
        }
    
        public int ID { get; set; }
        public int ENGINE_ID { get; set; }
        public int TRANSMISSION_ID { get; set; }
    
        public virtual ENGINE ENGINE { get; set; }
        public virtual TRANSMISSION TRANSMISSION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEHICL> VEHICLs { get; set; }
    }
}
