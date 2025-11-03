using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface ICursoRepository
{
    Task<IEnumerable<Curso>> ListarTodosAsync();
    Task<IEnumerable<Curso>> ListarPorEducadorAsync(int educadorId);
    Task<Curso?> BuscarPorIdAsync(int id);
    Task<Curso> AdicionarAsync(Curso curso);
    Task<Curso> AtualizarAsync(Curso curso);
    Task RemoverAsync(Curso curso);
}