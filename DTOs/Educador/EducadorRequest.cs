using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Educador;

public class EducadorRequest
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Especialidade { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;


    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get;set; } = string.Empty;


    [Required]
    [Phone]
    public string Telefone { get; set; } = string.Empty;

    [StringLength(240)]
    public string Descricao { get; set; } = string.Empty;
}
