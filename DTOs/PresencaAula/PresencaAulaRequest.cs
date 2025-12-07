using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.DTOs.PresencaAula;

public class PresencaAulaRequest
{
    [Required]
    public int InscricaoId { get; set; }

    [Required]
    public int AulaId { get; set; }

    public required bool Presente { get; set; }
}