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
    
        public virtual DbSet<Account> account { get; set; }
        public virtual DbSet<Character> character { get; set; }
        public virtual DbSet<characterfamily> CharacterFamily { get; set; }
        public virtual DbSet<family> family { get; set; }
        public virtual DbSet<familyhistory> familyhistory { get; set; }
        public virtual DbSet<friend> friend { get; set; }
        public virtual DbSet<inventory> inventory { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<skill> listskill { get; set; }
        public virtual DbSet<miniland> miniland { get; set; }
        public virtual DbSet<monster> monsters { get; set; }
        public virtual DbSet<npc> npcs { get; set; }
        public virtual DbSet<partner> partner { get; set; }
        public virtual DbSet<pet> pet { get; set; }
        public virtual DbSet<portal> portals { get; set; }
        public virtual DbSet<rune> runes { get; set; }
        public virtual DbSet<shop> shop { get; set; }
        public virtual DbSet<warehouse> warehouse { get; set; }
        public virtual DbSet<action> actionSet1 { get; set; }
        public virtual DbSet<ConnectionLog> connectionlog { get; set; }
        public virtual DbSet<Map> MapSet { get; set; }
    }
}
