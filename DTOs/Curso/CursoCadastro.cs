using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Curso;

public class CursoCadastro
{
    [Required]
    [StringLength(100)]
    public required string Nome { get; set; }

    [Required]
    [StringLength(500)]
    public required string Descricao { get; set; }

    [Required]
    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }
}