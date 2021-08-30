using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<InfoContacto> InfoContacto { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personas>()
                .HasMany(e => e.InfoContacto)
                .WithRequired(e => e.Personas)
                .WillCascadeOnDelete(false);
        }
    }
}
