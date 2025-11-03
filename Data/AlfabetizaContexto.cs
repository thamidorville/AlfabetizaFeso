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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relacionamentos
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

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Educador)
                .WithMany()
                .HasForeignKey(a => a.EducadorId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<Inscricao>()
                .HasOne(i => i.Aula)
                .WithMany()
                .HasForeignKey(i => i.AulaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Constraint: deve ter OU CursoId OU AulaId, mas não ambos
            modelBuilder.Entity<Inscricao>()
                .ToTable(t => t.HasCheckConstraint("CK_Inscricao_CursoOuAula", 
                    "(\"CursoId\" IS NOT NULL AND \"AulaId\" IS NULL) OR (\"CursoId\" IS NULL AND \"AulaId\" IS NOT NULL)"));
        }
    }
}
