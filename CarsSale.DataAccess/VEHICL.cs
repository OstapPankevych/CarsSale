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
    
    public partial class VEHICL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VEHICL()
        {
            this.ADVERTISEMENTs = new HashSet<ADVERTISEMENT>();
        }
    
        public int ID { get; set; }
        public int BRAND_ID { get; set; }
        public int VEHICL_TYPE_ID { get; set; }
        public int COMPLETESET_ID { get; set; }
        public int BODY_TYPE_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADVERTISEMENT> ADVERTISEMENTs { get; set; }
        public virtual BODY_TYPE BODY_TYPE { get; set; }
        public virtual BRAND BRAND { get; set; }
        public virtual COMPLETESET COMPLETESET { get; set; }
        public virtual VEHICL_TYPE VEHICL_TYPE { get; set; }
    }
}