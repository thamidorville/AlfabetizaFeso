using AlfabetizaFeso.Api.DTOs.Aluno;
using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IInscricaoService
{
    Task<IEnumerable<AulaResponse>> ObterAulasInscritasPorAlunoIdAsync(int id);
    Task<IEnumerable<AlunoResponse>> ObterAlunosInscritosPorAulaIdAsync(int id);
    Task<Inscricao> AdicionarAsync(int alunoId, int aulaId);
    Task<bool> RemoverAsync(int alunoId, int aulaId);
}
