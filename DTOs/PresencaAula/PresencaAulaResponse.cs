namespace AlfabetizaFeso.Api.DTOs.PresencaAula;

public class PresencaAulaResponse
{
    public int Id { get; set; }
    public int InscricaoId { get; set; }
    public int AulaId { get; set; }
    public bool Presente { get; set; }
    public DateTime DataCriacao { get; set; }
}