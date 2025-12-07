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
        public DbSet<Curso> Cursos { get; set; } = null!;
        public DbSet<Aula> Aulas { get; set; } = null!;
        public DbSet<Inscricao> Inscricoes { get; set; } = null!;
        public DbSet<PresencaAula> PresencasAula { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Educador)
                .WithMany()
                .HasForeignKey(c => c.EducadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Aulas)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscricao>()
                .HasOne(i => i.Aluno)
                .WithMany()
                .HasForeignKey(i => i.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscricao>()
                .HasOne(i => i.Curso)
                .WithMany(c => c.Inscricoes)
                .HasForeignKey(i => i.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PresencaAula>()
                .HasOne(p => p.Inscricao)
                .WithMany()
                .HasForeignKey(p => p.InscricaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PresencaAula>()
                .HasOne(p => p.Aula)
                .WithMany()
                .HasForeignKey(p => p.AulaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
