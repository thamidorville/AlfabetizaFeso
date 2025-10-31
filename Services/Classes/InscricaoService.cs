using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class InscricaoService(IInscricaoRepository inscricaoRepository, IUsuarioRepository usuarioRepository, IAulaRepository aulaRepository) : IInscricaoService
{
    private readonly IInscricaoRepository _inscricaoRepository = inscricaoRepository;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IAulaRepository _aulaRepository = aulaRepository;

    public async Task<bool> AdicionarAsync(int alunoId, int aulaId)
    {
        var inscricaoExistente = await _inscricaoRepository.ObterInscricaoAsync(alunoId, aulaId);

        if (inscricaoExistente != null)
            throw new InvalidOperationException("O aluno já está inscrito nesta aula.");

        if (!await AlunoAulaExistem(alunoId, aulaId))
            throw new InvalidOperationException("Aluno ou aula não encontrado");

        var inscricao = new Inscricao
        {
            AlunoId = alunoId,
            AulaId = aulaId,
        };

        await _inscricaoRepository.AdicionarAsync(inscricao);
        return true;
    }

    public async Task<IEnumerable<UsuarioResponse>> ObterAlunosInscritosPorAulaIdAsync(int id)
    {
        var inscricoes = await _inscricaoRepository.ObterInscricoesPorAulaIdAsync(id);
        var alunos = inscricoes
            .Where(i => i.Aluno != null)
            .Select(i => i.Aluno!)
            .ToList();

        return alunos.Select(a => a.ToDto()!).ToList();
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

    private async Task<bool> AlunoAulaExistem(int alunoId, int aulaId)
    {
        var aluno = await _usuarioRepository.BuscarPorIdAsync(alunoId);
            var aula = await _aulaRepository.BuscarPorIdAsync(aulaId);

        return aluno != null && aula != null;
    }
}