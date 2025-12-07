namespace AlfabetizaFeso.Api.Models;

public class Curso
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public required string Status { get; set; } = "ativo"; // ativo, inativo, concluido

    // Relacionamento com Educador
    public int EducadorId { get; set; }
    public Usuario? Educador { get; set; }

    // Relacionamentos
    public ICollection<Aula> Aulas { get; set; } = new List<Aula>();
    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();

    // Auditoria
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}