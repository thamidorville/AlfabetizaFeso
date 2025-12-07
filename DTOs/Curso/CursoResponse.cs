namespace AlfabetizaFeso.Api.DTOs.Curso;

public class CursoResponse
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public required string Status { get; set; }
    public int EducadorId { get; set; }
    public string? NomeEducador { get; set; }
    public int TotalAulas { get; set; }
    public int TotalInscricoes { get; set; }
}