using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Usuario;

public class AlunoRequest
{
    [Required]
    public required string Nome { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; } 

    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
    public required string Senha { get; set; } 

    [Required]
    [Compare("Senha")]
    public required string ConfirmarSenha { get; set; } 

    [Required]
    [Phone]
    public required string Telefone { get; set; } 

    [StringLength(240)]
    public string? Descricao { get; set; } 

}
