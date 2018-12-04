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
    
    public partial class investigators
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public investigators()
        {
            this.commissions = new HashSet<commissions>();
            this.interest_areas = new HashSet<interest_areas>();
            this.concepts = new HashSet<concepts>();
        }
    
        public int investigator_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string last_name { get; set; }
        public string second_last_name { get; set; }
        public Nullable<int> gender_id { get; set; }
        public string mobile_phone { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public Nullable<int> institution_id { get; set; }
        public Nullable<int> investigation_group_id { get; set; }
        public Nullable<int> program_id { get; set; }
        public Nullable<int> academic_level_id { get; set; }
    
        public virtual academic_levels academic_levels { get; set; }
        public virtual genders genders { get; set; }
        public virtual institutions institutions { get; set; }
        public virtual investigation_groups investigation_groups { get; set; }
        public virtual programs programs { get; set; }
        public virtual users users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commissions> commissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<interest_areas> interest_areas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concepts> concepts { get; set; }
    }
}
