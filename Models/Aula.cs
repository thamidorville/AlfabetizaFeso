namespace AlfabetizaFeso.Api.Models;

public class Aula
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }

    // Relacionamentos
    public int CursoId { get; set; }
    public Curso? Curso { get; set; }

    public int EducadorId { get; set; }
    public Usuario? Educador { get; set; }

    // Auditoria
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}