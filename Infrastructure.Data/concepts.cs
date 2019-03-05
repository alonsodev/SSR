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
    
    public partial class concepts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public concepts()
        {
            this.concept_debate_speakers = new HashSet<concept_debate_speakers>();
            this.concepts_status_logs = new HashSet<concepts_status_logs>();
            this.concepts_tags = new HashSet<concepts_tags>();
        }
    
        public int concept_id { get; set; }
        public Nullable<int> draft_law_id { get; set; }
        public string summary { get; set; }
        public string concept { get; set; }
        public Nullable<int> investigator_id { get; set; }
        public string bibliography { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> user_id_modified { get; set; }
        public int concept_status_id { get; set; }
        public Nullable<int> reason_reject_id { get; set; }
        public string reason_reject_description { get; set; }
        public Nullable<double> qualification { get; set; }
        public string pdf_path { get; set; }
        public string certification_path { get; set; }
        public Nullable<System.Guid> hash { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concept_debate_speakers> concept_debate_speakers { get; set; }
        public virtual concepts_status concepts_status { get; set; }
        public virtual draft_laws draft_laws { get; set; }
        public virtual investigators investigators { get; set; }
        public virtual reason_rejects reason_rejects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concepts_status_logs> concepts_status_logs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concepts_tags> concepts_tags { get; set; }
    }
}
