using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IInscricaoService
{
    Task<IEnumerable<AulaResponse>> ObterAulasInscritasPorAlunoIdAsync(int id);
    Task<IEnumerable<UsuarioResponse>> ObterAlunosInscritosPorAulaIdAsync(int id);
    Task<bool> AdicionarAsync(int alunoId, int aulaId);
    Task<bool> RemoverAsync(int alunoId, int aulaId);
}
