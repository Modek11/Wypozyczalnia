//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wypozyczalnia
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class WypozyczalniaEntities : DbContext
    {
        public WypozyczalniaEntities()
            : base("name=WypozyczalniaEntities")
        {
        }

    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<KlasyPojazdow> KlasyPojazdow { get; set; }
        public virtual DbSet<Samochody> Samochody { get; set; }
        public virtual DbSet<Uzytkownicy> Uzytkownicy { get; set; }
        public virtual DbSet<Wypozyczone> Wypozyczone { get; set; }
    }
}
