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
    
        public virtual DbSet<permissions> permissions { get; set; }
        public virtual DbSet<role_permissions> role_permissions { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<user_status> user_status { get; set; }
        public virtual DbSet<document_types> document_types { get; set; }
        public virtual DbSet<institutions> institutions { get; set; }
        public virtual DbSet<nationalities> nationalities { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<genders> genders { get; set; }
        public virtual DbSet<investigation_groups> investigation_groups { get; set; }
        public virtual DbSet<investigators> investigators { get; set; }
    }
}
