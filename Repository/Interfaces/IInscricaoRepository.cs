using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IInscricaoRepository
{
    Task<IEnumerable<Inscricao>> ObterInscricoesPorAlunoIdAsync(int id);
    Task<IEnumerable<Inscricao>> ObterInscricoesPorAulaIdAsync(int id);
    Task<Inscricao?> ObterInscricaoAsync(int alunoId, int aulaId);
    Task<Inscricao> AdicionarAsync(Inscricao inscricao);
    Task<bool> RemoverAsync(int alunoId, int aulaId);
}
