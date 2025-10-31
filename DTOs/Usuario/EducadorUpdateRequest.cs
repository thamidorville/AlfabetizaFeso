using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Usuario;

public class EducadorUpdateRequest
{
    [Required]
    public required string Nome { get; set; }

    [Required]
    public required string Especialidade { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [Phone]
    public required string Telefone { get; set; }
    [StringLength(240)]
    public string? Descricao { get; set; }
}
