namespace AlfabetizaFeso.Api.Models;

public class Aula
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }

    public int CursoId { get; set; }
    public Curso? Curso { get; set; }

    public string? LinkAula { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}