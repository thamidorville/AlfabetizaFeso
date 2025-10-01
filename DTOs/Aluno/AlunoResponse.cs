namespace AlfabetizaFeso.Api.DTOs.Aluno;

public class AlunoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}