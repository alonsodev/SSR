﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SSREntities : DbContext
    {
        public SSREntities()
            : base("name=SSREntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<academic_levels> academic_levels { get; set; }
        public virtual DbSet<bad_languages> bad_languages { get; set; }
        public virtual DbSet<commissions> commissions { get; set; }
        public virtual DbSet<concept_debate_speakers> concept_debate_speakers { get; set; }
        public virtual DbSet<concepts> concepts { get; set; }
        public virtual DbSet<concepts_status> concepts_status { get; set; }
        public virtual DbSet<concepts_status_logs> concepts_status_logs { get; set; }
        public virtual DbSet<concepts_tags> concepts_tags { get; set; }
        public virtual DbSet<configurations> configurations { get; set; }
        public virtual DbSet<consultation_types> consultation_types { get; set; }
        public virtual DbSet<consultations> consultations { get; set; }
        public virtual DbSet<consultations_interest_areas> consultations_interest_areas { get; set; }
        public virtual DbSet<debate_speakers> debate_speakers { get; set; }
        public virtual DbSet<departments> departments { get; set; }
        public virtual DbSet<document_types> document_types { get; set; }
        public virtual DbSet<draft_laws> draft_laws { get; set; }
        public virtual DbSet<education_levels> education_levels { get; set; }
        public virtual DbSet<educational_institutions> educational_institutions { get; set; }
        public virtual DbSet<genders> genders { get; set; }
        public virtual DbSet<initiatives> initiatives { get; set; }
        public virtual DbSet<institutions> institutions { get; set; }
        public virtual DbSet<interest_areas> interest_areas { get; set; }
        public virtual DbSet<investigation_groups> investigation_groups { get; set; }
        public virtual DbSet<investigators> investigators { get; set; }
        public virtual DbSet<investigators_commissions> investigators_commissions { get; set; }
        public virtual DbSet<investigators_interest_areas> investigators_interest_areas { get; set; }
        public virtual DbSet<knowledge_areas> knowledge_areas { get; set; }
        public virtual DbSet<merit_ranges> merit_ranges { get; set; }
        public virtual DbSet<municipalities> municipalities { get; set; }
        public virtual DbSet<nationalities> nationalities { get; set; }
        public virtual DbSet<notifications> notifications { get; set; }
        public virtual DbSet<origins> origins { get; set; }
        public virtual DbSet<periods> periods { get; set; }
        public virtual DbSet<permissions> permissions { get; set; }
        public virtual DbSet<programs> programs { get; set; }
        public virtual DbSet<reason_rejects> reason_rejects { get; set; }
        public virtual DbSet<role_permissions> role_permissions { get; set; }
        public virtual DbSet<snies> snies { get; set; }
        public virtual DbSet<tags> tags { get; set; }
        public virtual DbSet<user_status> user_status { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<user_institutions> user_institutions { get; set; }
        public virtual DbSet<draft_laws_status> draft_laws_status { get; set; }
        public virtual DbSet<roles> roles { get; set; }
    
        public virtual int ActualizarTablasReporte(Nullable<int> period_id)
        {
            var period_idParameter = period_id.HasValue ?
                new ObjectParameter("period_id", period_id) :
                new ObjectParameter("period_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarTablasReporte", period_idParameter);
        }
    
        public virtual int ActualizarNotificacionLeido(string url, Nullable<int> user_id)
        {
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarNotificacionLeido", urlParameter, user_idParameter);
        }
    }
}
