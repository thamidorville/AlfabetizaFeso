using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.DTOs.Inscricao;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IInscricaoService
{
    Task<IEnumerable<AulaResponse>> ObterAulasInscritasPorAlunoIdAsync(int id);
    Task<IEnumerable<UsuarioResponse>> ObterAlunosInscritosPorCursoIdAsync(int cursoId);
    Task<IEnumerable<UsuarioResponse>> ObterAlunosInscritosPorAulaIdAsync(int aulaId);
    Task<IEnumerable<InscricaoResponse>> ObterInscricoesPorAlunoIdAsync(int alunoId);
    Task<InscricaoResponse?> ObterInscricaoAsync(int alunoId, int cursoId);
    Task<bool> AdicionarAsync(int alunoId, int cursoId);
    Task<bool> RemoverAsync(int alunoId, int cursoId);
}