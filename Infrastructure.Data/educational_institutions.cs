//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class educational_institutions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public educational_institutions()
        {
            this.snies = new HashSet<snies>();
            this.investigators = new HashSet<investigators>();
        }
    
        public int educational_institution_id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> user_id_modified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<snies> snies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<investigators> investigators { get; set; }
    }
}
