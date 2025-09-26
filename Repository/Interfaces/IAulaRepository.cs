using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IAulaRepository
{
    Task<IEnumerable<Aula>> ListarTodosAsync();
    Task<IEnumerable<Aula>> ListarPorEducadorId(int educadorId);
    Task<Aula?> BuscarPorIdAsync(int id);
    Task<Aula> AdicionarAsync(Aula aula);
    Task<Aula> AtualizarAsync(Aula aula);
    Task<bool> RemoverAsync(int id);
}
