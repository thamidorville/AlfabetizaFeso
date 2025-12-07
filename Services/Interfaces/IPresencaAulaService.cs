using AlfabetizaFeso.Api.DTOs.PresencaAula;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IPresencaAulaService
{
    Task<PresencaAulaResponse> AdicionarAsync(PresencaAulaRequest request);
    Task<PresencaAulaResponse?> ObterPorInscricaoEAulaAsync(int inscricaoId, int aulaId);
    Task<IEnumerable<PresencaAulaResponse>> ObterPorAulaIdAsync(int aulaId);
    Task<IEnumerable<PresencaAulaResponse>> ObterPorInscricaoIdAsync(int inscricaoId);
    Task<bool> RemoverAsync(int id);
}