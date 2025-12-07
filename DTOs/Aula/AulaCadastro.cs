using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Aula;

public class AulaCadastro
{
    [Required]
    public required string Titulo { get; set; }

    [Required]
    public required string Descricao { get; set; }

    [Required]
    public DateTime DataInicio { get; set; }

    [Required]
    public DateTime DataFinal { get; set; }

    public string? LinkAula { get; set; }
}