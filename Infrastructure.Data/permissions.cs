//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class permissions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public permissions()
        {
            this.role_permissions = new HashSet<role_permissions>();
        }
    
        public int id_permission { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public Nullable<int> is_only_for_super_admin { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> user_id_modified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<role_permissions> role_permissions { get; set; }
    }
}
