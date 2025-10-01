using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> ListarTodosAsync();
    Task<Aluno?> BuscarPorIdAsync(int id);
    Task<Aluno> AdicionarAsync(Aluno aluno);
    Task<Aluno> AtualizarAsync(Aluno aluno);
    Task<bool> RemoverAsync(int id);
}
