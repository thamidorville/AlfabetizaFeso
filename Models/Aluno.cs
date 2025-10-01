namespace AlfabetizaFeso.Api.Models;

public class Aluno
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Telefone { get; set; }
    public string? Descricao { get; set; }

    public ICollection<Inscricao> Inscricoes { get; set; } = [];
}
