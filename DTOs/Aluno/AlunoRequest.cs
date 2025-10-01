using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Aluno;

public class AlunoRequest
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Telefone { get; set; } = string.Empty;

    [StringLength(240)]
    public string Descricao { get; set; } = string.Empty;
}