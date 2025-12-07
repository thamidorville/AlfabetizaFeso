namespace AlfabetizaFeso.Api.DTOs.Inscricao;

public class InscricaoResponse
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int CursoId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime DataInscricao { get; set; }
    public string? NomeCurso { get; set; }
}