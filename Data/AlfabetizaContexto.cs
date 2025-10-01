using Microsoft.EntityFrameworkCore;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Data
{
    public class AlfabetizaContexto(DbContextOptions<AlfabetizaContexto> options) : DbContext(options)
    {
        public DbSet<Educador> Educadores { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define AlunoId e AulaId como PK de Inscricao
            modelBuilder.Entity<Inscricao>()
                .HasKey(i => new { i.AlunoId, i.AulaId });
        }
    }
}
