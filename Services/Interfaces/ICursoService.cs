using AlfabetizaFeso.Api.DTOs.Curso;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface ICursoService
{
    Task<IEnumerable<CursoResponse>> ListarTodosAsync();
    Task<IEnumerable<CursoResponse>> ListarPorEducadorAsync(int educadorId);
    Task<CursoResponse?> BuscarPorIdAsync(int id);
    Task<CursoResponse> AdicionarAsync(CursoCadastro cursoCadastro, int educadorId);
    Task<CursoResponse> AtualizarAsync(int id, CursoCadastro cursoCadastro, int educadorId);
    Task<bool> RemoverAsync(int id, int educadorId);
}