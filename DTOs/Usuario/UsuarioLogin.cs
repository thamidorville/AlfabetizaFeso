using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Usuario;

public class UsuarioLogin
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Senha { get; set; }
}
