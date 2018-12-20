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
    
    public partial class draft_laws
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public draft_laws()
        {
            this.concepts = new HashSet<concepts>();
            this.debate_speakers = new HashSet<debate_speakers>();
        }
    
        public int draft_law_id { get; set; }
        public int draft_law_number { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string origin { get; set; }
        public Nullable<System.DateTime> date_presentation { get; set; }
        public Nullable<int> commission_id { get; set; }
        public string debate_speaker { get; set; }
        public string debate_speaker2 { get; set; }
        public string status { get; set; }
        public string status_comment { get; set; }
        public Nullable<int> interest_area_id { get; set; }
        public string initiative { get; set; }
        public string summary { get; set; }
        public string link { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> user_id_modified { get; set; }
        public string dev_name { get; set; }
    
        public virtual commissions commissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concepts> concepts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<debate_speakers> debate_speakers { get; set; }
        public virtual interest_areas interest_areas { get; set; }
    }
}