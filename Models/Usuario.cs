using System.ComponentModel.DataAnnotations;

namespace AlfabetizaFeso.Api.Models;

public class Usuario
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public string SenhaHash { get; set; } = null!;
    public required string Telefone { get; set; }
    public string? Descricao { get; set; }

    // Nova propriedade para distinguir aluno/educador: "aluno" | "educador"
    public required string Role { get; set; }

    // Propriedade específica do educador (mantida aqui pois Aluno e Educador foram unificados)
    public string? Especialidade { get; set; }

    // Navegação: aulas ministradas (válida quando Role == "educador")
    public ICollection<Aula> AulasMinistradas { get; set; } = new List<Aula>();
}