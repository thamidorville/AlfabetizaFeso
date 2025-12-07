namespace AlfabetizaFeso.Api.Models;

public class Inscricao
{
    public int Id { get; set; }
    public DateTime DataInscricao { get; set; } = DateTime.UtcNow;
    public required string Status { get; set; } = "ativa";

    public int AlunoId { get; set; }
    public Usuario? Aluno { get; set; }

    public int CursoId { get; set; }
    public Curso? Curso { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
