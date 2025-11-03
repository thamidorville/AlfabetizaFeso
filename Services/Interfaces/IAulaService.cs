using AlfabetizaFeso.Api.DTOs.Aula;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IAulaService
{
    Task<IEnumerable<AulaResponse>> ListarTodosAsync();
    Task<IEnumerable<AulaResponse>> ListarPorEducadorIdAsync(int educadorId);
    Task<AulaResponse?> BuscarPorIdAsync(int id);
    Task<AulaResponse> AdicionarAsync(AulaCadastro aulaCadastro, int educadorId);
    Task<AulaResponse> AtualizarAsync(AulaCadastro aulaCadastro, int aulaId, int educadorId);
    Task<bool> RemoverAsync(int id);
    //Task VerificaExistenciaEducadorAsync(int educadorId);
}
