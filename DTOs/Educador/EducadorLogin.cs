using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.Educador
{
    public class EducadorLogin
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}