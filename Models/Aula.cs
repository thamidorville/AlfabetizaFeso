namespace AlfabetizaFeso.Api.Models;

public class Aula
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string  Descricao { get; set; }
    public DateTime DataInicioUtc { get; set; }
    public DateTime DataFinalUtc { get; set; }

    public int EducadorId { get; set; }
    public Educador? Educador { get; set; }

    public ICollection<Inscricao> Inscricoes { get; set; } = [];
}
