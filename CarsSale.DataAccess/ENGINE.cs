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
    
    public partial class ENGINE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENGINE()
        {
            this.ENGINE_FUEL = new HashSet<ENGINE_FUEL>();
            this.VEHICLs = new HashSet<VEHICL>();
        }
    
        public int ID { get; set; }
        public int VOLUME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENGINE_FUEL> ENGINE_FUEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEHICL> VEHICLs { get; set; }
    }
}
