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
            this.concepts = new HashSet<concepts>();
            this.investigators_commissions = new HashSet<investigators_commissions>();
            this.investigators_interest_areas = new HashSet<investigators_interest_areas>();
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
        public Nullable<int> educational_institution_id { get; set; }
        public Nullable<int> program_id { get; set; }
        public Nullable<int> education_level_id { get; set; }
        public string CVLAC { get; set; }
        public Nullable<double> points { get; set; }
        public Nullable<int> position { get; set; }
        public Nullable<int> total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concepts> concepts { get; set; }
        public virtual education_levels education_levels { get; set; }
        public virtual educational_institutions educational_institutions { get; set; }
        public virtual genders genders { get; set; }
        public virtual institutions institutions { get; set; }
        public virtual investigation_groups investigation_groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<investigators_commissions> investigators_commissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<investigators_interest_areas> investigators_interest_areas { get; set; }
        public virtual programs programs { get; set; }
        public virtual users users { get; set; }
    }
}
