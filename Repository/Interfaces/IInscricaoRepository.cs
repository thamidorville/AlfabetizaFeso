using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IInscricaoRepository
{
    Task<IEnumerable<Inscricao>> ObterInscricoesPorAlunoIdAsync(int id);
    Task<IEnumerable<Inscricao>> ObterInscricoesPorCursoIdAsync(int cursoId);
    Task<Inscricao?> ObterInscricaoAsync(int alunoId, int cursoId);
    Task<Inscricao?> BuscarPorIdAsync(int id);
    Task<Inscricao> AdicionarAsync(Inscricao inscricao);
    Task<bool> RemoverAsync(int alunoId, int cursoId);
}
