using AlfabetizaFeso.Api.DTOs.Aula;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IAulaService
{
    Task<IEnumerable<AulaResponse>> ListarPorCursoAsync(int cursoId);
    Task<AulaResponse?> BuscarPorIdAsync(int id);
    Task<AulaResponse> AdicionarAsync(AulaCadastro aulaCadastro, int cursoId, int educadorId);
    Task<AulaResponse> AtualizarAsync(int id, AulaCadastro aulaCadastro, int cursoId, int educadorId);
    Task<bool> RemoverAsync(int id, int cursoId, int educadorId);
}