namespace AlfabetizaFeso.Api.DTOs.Aula;

public class AulaResponse
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public int CursoId { get; set; }
    public int EducadorId { get; set; }
    public string? NomeCurso { get; set; }
    public string? NomeEducador { get; set; }
}
