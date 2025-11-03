using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Usuario;

public class SenhaEditar
{
    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
    public required string SenhaAntiga { get; set; }

    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
    public required string SenhaNova { get; set; }

    [Required]
    [Compare("SenhaNova")]
    public required string ConfirmarSenha { get; set; } 
}
