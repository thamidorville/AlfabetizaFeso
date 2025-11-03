namespace AlfabetizaFeso.Api.DTOs.Aula;

public class AulaCadastro
{
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
}
