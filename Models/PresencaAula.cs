namespace AlfabetizaFeso.Api.Models;

public class PresencaAula
{
    public int Id { get; set; }
    public int InscricaoId { get; set; }
    public Inscricao? Inscricao { get; set; }
    public int AulaId { get; set; }
    public Aula? Aula { get; set; }
    public bool Presente { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}