namespace AlfabetizaFeso.Api.DTOs.Usuario;

public class AlunoLista
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public string? Descricao { get; set; }
}