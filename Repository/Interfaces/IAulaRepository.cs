using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IAulaRepository
{
    Task<IEnumerable<Aula>> ListarPorCursoAsync(int cursoId);
    Task<Aula?> BuscarPorIdAsync(int id);
    Task<Aula> AdicionarAsync(Aula aula);
    Task<Aula> AtualizarAsync(Aula aula);
    Task RemoverAsync(Aula aula);
}