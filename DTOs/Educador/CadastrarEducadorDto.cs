using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Educador
{
    public class CadastrarEducadorDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Especialidade { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}