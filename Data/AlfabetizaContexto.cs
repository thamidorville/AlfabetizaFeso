using Microsoft.EntityFrameworkCore;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Data
{
    public class AlfabetizaContexto : DbContext
    {
        public AlfabetizaContexto(DbContextOptions<AlfabetizaContexto> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Aula> Aulas { get; set; } = null!;
        public DbSet<Inscricao> Inscricoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inscricao>()
                .HasKey(i => new { i.AlunoId, i.AulaId });
        }
    }
}
