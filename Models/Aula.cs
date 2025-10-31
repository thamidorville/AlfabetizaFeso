namespace AlfabetizaFeso.Api.Models;

public class Aula
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicioUtc { get; set; }
    public DateTime DataFinalUtc { get; set; }

    // Agora referencia Usuario como educador (Role == "educador")
    public int EducadorId { get; set; }
    public Usuario? Educador { get; set; }

    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
}