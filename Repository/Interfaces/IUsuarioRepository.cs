using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ListarTodosAsync();
    Task<IEnumerable<Usuario>> ListarTodosAlunosAsync();
    Task<IEnumerable<Usuario>> ListarTodosEducadoresAsync();
    Task<IEnumerable<Usuario>> BuscarEducadorPorNomeAsync(string nome);
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task<Usuario> AdicionarAsync(Usuario usuario);
    Task<Usuario> AtualizarAsync(Usuario usuario);
    Task<bool> RemoverAsync(Usuario usuario);
}