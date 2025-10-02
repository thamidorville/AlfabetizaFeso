using AlfabetizaFeso.Api.DTOs.Aluno;
using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;


namespace AlfabetizaFeso.Api.Services.Classes;

public class InscricaoService(IInscricaoRepository inscricaoRepository) : IInscricaoService
{
    private readonly IInscricaoRepository _inscricaoRepository = inscricaoRepository;

    public async Task<Inscricao> AdicionarAsync(int alunoId, int aulaId)
    {
        var inscricaoExistente = await _inscricaoRepository.ObterInscricaoAsync(alunoId, aulaId);            

        if (inscricaoExistente != null)
            throw new InvalidOperationException("O aluno já está inscrito nesta aula.");

        var inscricao = new Inscricao
        {
            AlunoId = alunoId,
            AulaId = aulaId,
        };

        await _inscricaoRepository.AdicionarAsync(inscricao);
        return inscricao;
    }


    public async Task<IEnumerable<AlunoResponse>> ObterAlunosInscritosPorAulaIdAsync(int id)
    {
        var inscricoes = await _inscricaoRepository.ObterInscricoesPorAulaIdAsync(id);
        var alunosDto = inscricoes
            .Where(i => i.Aluno != null)
            .Select(i => i.Aluno!)
            .Select(a => a.ToDto())
            .ToList();

        return alunosDto;
    }


    public async Task<IEnumerable<AulaResponse>> ObterAulasInscritasPorAlunoIdAsync(int id)
    {
        var inscricoes = await _inscricaoRepository.ObterInscricoesPorAlunoIdAsync(id);
        var aulasDto = inscricoes
            .Where(i => i.Aula != null)
            .Select(i => i.Aula!)
            .Select(a => a.ToDto())
            .ToList();

        return aulasDto;
    }


    public async Task<bool> RemoverAsync(int alunoId, int aulaId)
    {
        return await _inscricaoRepository.RemoverAsync(alunoId, aulaId);
    }
}
