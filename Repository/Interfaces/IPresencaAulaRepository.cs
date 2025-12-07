using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Repository.Interfaces;

public interface IPresencaAulaRepository
{
    Task<PresencaAula> AdicionarAsync(PresencaAula presenca);
    Task<PresencaAula?> ObterPorInscricaoEAulaAsync(int inscricaoId, int aulaId);
    Task<IEnumerable<PresencaAula>> ObterPorAulaIdAsync(int aulaId);
    Task<IEnumerable<PresencaAula>> ObterPorInscricaoIdAsync(int inscricaoId);
    Task<bool> RemoverAsync(int id);
}