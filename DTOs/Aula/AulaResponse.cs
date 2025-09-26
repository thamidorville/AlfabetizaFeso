namespace AlfabetizaFeso.Api.DTOs.Aula;

public class AulaResponse
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicioUtc { get; set; }
    public DateTime DataFinalUtc { get; set; }
    public int EducadorId { get; set; }
}
