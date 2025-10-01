namespace AlfabetizaFeso.Api.Models;

public class Inscricao
{
    public int AlunoId { get; set; }
    public required Aluno Aluno { get; set; }

    public int AulaId { get; set; }
    public required Aula Aula { get; set; }

    // Implementar depois, vai exigir uma regra de negocio mais complexa
    //public bool InscricaoConfirmada { get; set; }
    //public bool AlunoPresente { get; set; }
}
