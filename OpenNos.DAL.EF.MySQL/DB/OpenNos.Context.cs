﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenNos.DAL.EF.MySQL.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OpenNosContainer : DbContext
    {
        public OpenNosContainer()
            : base("name=OpenNosContainer")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<account> Account { get; set; }
        public virtual DbSet<character> Character { get; set; }
        public virtual DbSet<charafamily> charafamily { get; set; }
        public virtual DbSet<family> family { get; set; }
        public virtual DbSet<familyhistory> familyhistory { get; set; }
        public virtual DbSet<friend> friend { get; set; }
        public virtual DbSet<inventory> inventory { get; set; }
        public virtual DbSet<items> items { get; set; }
        public virtual DbSet<listskill> listskill { get; set; }
        public virtual DbSet<miniland> miniland { get; set; }
        public virtual DbSet<monsters> monsters { get; set; }
        public virtual DbSet<npcs> npcs { get; set; }
        public virtual DbSet<partner> partner { get; set; }
        public virtual DbSet<pet> pet { get; set; }
        public virtual DbSet<portals> portals { get; set; }
        public virtual DbSet<runes> runes { get; set; }
        public virtual DbSet<shop> shop { get; set; }
        public virtual DbSet<warehouse> warehouse { get; set; }
    }
}
