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
    
    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            this.concept_debate_speakers = new HashSet<concept_debate_speakers>();
            this.consultations = new HashSet<consultations>();
            this.debate_speakers = new HashSet<debate_speakers>();
            this.investigators = new HashSet<investigators>();
            this.notifications = new HashSet<notifications>();
            this.user_institutions = new HashSet<user_institutions>();
        }
    
        public int id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_pass { get; set; }
        public Nullable<int> document_type_id { get; set; }
        public string doc_nro { get; set; }
        public Nullable<int> nationality_id { get; set; }
        public string contact_name { get; set; }
        public string phone { get; set; }
        public Nullable<int> address_country_id { get; set; }
        public Nullable<int> address_municipality_id { get; set; }
        public string address { get; set; }
        public Nullable<int> user_role_id { get; set; }
        public Nullable<int> user_status_id { get; set; }
        public Nullable<byte> is_super_admin { get; set; }
        public Nullable<System.DateTime> user_date_last_login { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<int> user_id_created { get; set; }
        public Nullable<int> user_id_modified { get; set; }
        public string user_code_activate { get; set; }
        public string user_code_recover { get; set; }
        public string avatar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<concept_debate_speakers> concept_debate_speakers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<consultations> consultations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<debate_speakers> debate_speakers { get; set; }
        public virtual document_types document_types { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<investigators> investigators { get; set; }
        public virtual municipalities municipalities { get; set; }
        public virtual nationalities nationalities { get; set; }
        public virtual nationalities nationalities1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notifications> notifications { get; set; }
        public virtual roles roles { get; set; }
        public virtual user_status user_status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_institutions> user_institutions { get; set; }
    }
}
