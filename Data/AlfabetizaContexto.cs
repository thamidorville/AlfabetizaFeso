using Microsoft.EntityFrameworkCore;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Data
{
    public class AlfabetizaContexto : DbContext
    {
        public AlfabetizaContexto(DbContextOptions<AlfabetizaContexto> options) : base(options)
        {
        }

        public DbSet<Educador> Educadores { get; set; }
        public DbSet<Aula> Aulas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Educador>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Inscricao>()
                .HasKey(i => new { i.AlunoId, i.AulaId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
