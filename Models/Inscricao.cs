namespace AlfabetizaFeso.Api.Models;

public class Inscricao
{
    public int Id { get; set; }
    public DateTime DataInscricao { get; set; } = DateTime.UtcNow;
    public required string Status { get; set; } = "ativa"; // ativa, cancelada, concluida
    public bool? Presenca { get; set; } // null=nao marcado, true=presente, false=ausente

    // Relacionamentos
    public int AlunoId { get; set; }
    public Usuario? Aluno { get; set; }

    // Polimórfico: OU curso OU aula
    public int? CursoId { get; set; }
    public Curso? Curso { get; set; }

    public int? AulaId { get; set; }
    public Aula? Aula { get; set; }

    // Auditoria
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
