﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Amazonia
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class amazoniaEntities : DbContext
    {
        public amazoniaEntities()
            : base("name=amazoniaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleLine> SaleLines { get; set; }
    }
}
