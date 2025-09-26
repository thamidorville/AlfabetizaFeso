namespace AlfabetizaFeso.Api.DTOs.Aula;

public class AulaRequest
{
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public int EducadorId { get; set; }
}
