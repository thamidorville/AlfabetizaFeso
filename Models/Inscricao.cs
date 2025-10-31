namespace AlfabetizaFeso.Api.Models;

public class Inscricao
{
    public int AlunoId { get; set; }
    public Usuario? Aluno { get; set; }

    public int AulaId { get; set; }
    public Aula? Aula { get; set; }
}
