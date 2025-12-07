using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.DTOs.Inscricao;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class InscricaoService(
    IInscricaoRepository inscricaoRepository,
    IUsuarioRepository usuarioRepository,
    ICursoRepository cursoRepository,
    IAulaRepository aulaRepository) : IInscricaoService
{
    private readonly IInscricaoRepository _inscricaoRepository = inscricaoRepository;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly ICursoRepository _cursoRepository = cursoRepository;
    private readonly IAulaRepository _aulaRepository = aulaRepository;

    public async Task<bool> AdicionarAsync(int alunoId, int cursoId)
    {
        var inscricaoExistente = await _inscricaoRepository.ObterInscricaoAsync(alunoId, cursoId);

        if (inscricaoExistente != null)
            throw new InvalidOperationException("O aluno já está inscrito neste curso.");

        if (!await AlunoCursoExistem(alunoId, cursoId))
            throw new InvalidOperationException("Aluno ou curso não encontrado");

        var inscricao = new Inscricao
        {
            AlunoId = alunoId,
            CursoId = cursoId,
            Status = "ativa"
        };

        await _inscricaoRepository.AdicionarAsync(inscricao);
        return true;
    }

    public async Task<IEnumerable<UsuarioResponse>> ObterAlunosInscritosPorCursoIdAsync(int cursoId)
    {
        var inscricoes = await _inscricaoRepository.ObterInscricoesPorCursoIdAsync(cursoId);
        var alunos = inscricoes
            .Where(i => i.Aluno != null)
            .Select(i => i.Aluno!)
            .ToList();

        return alunos.Select(a => a.ToDto()!).ToList();
    }

    public async Task<IEnumerable<UsuarioResponse>> ObterAlunosInscritosPorAulaIdAsync(int aulaId)
    {
        var aula = await _aulaRepository.BuscarPorIdAsync(aulaId);
        if (aula == null)
            return Enumerable.Empty<UsuarioResponse>();

        var inscricoes = await _inscricaoRepository.ObterInscricoesPorCursoIdAsync(aula.CursoId);
        var alunos = inscricoes
            .Where(i => i.Aluno != null)
            .Select(i => i.Aluno!)
            .ToList();

        return alunos.Select(a => a.ToDto()!).ToList();
    }

    public async Task<IEnumerable<AulaResponse>> ObterAulasInscritasPorAlunoIdAsync(int id)
    {
        var inscricoes = await _inscricaoRepository.ObterInscricoesPorAlunoIdAsync(id);

        var aulasMap = new Dictionary<int, AulaResponse>();

        foreach (var inscricao in inscricoes)
        {
            if (inscricao.CursoId == 0) continue;

            var aulas = await _aulaRepository.ListarPorCursoAsync(inscricao.CursoId);
            foreach (var aula in aulas)
            {
                if (!aulasMap.ContainsKey(aula.Id))
                {
                    var dto = aula.ToDto();
                    aulasMap[aula.Id] = dto;
                }
            }
        }

        return aulasMap.Values;
    }

    public async Task<IEnumerable<InscricaoResponse>> ObterInscricoesPorAlunoIdAsync(int alunoId)
    {
        var inscricoes = await _inscricaoRepository.ObterInscricoesPorAlunoIdAsync(alunoId);
        var lista = inscricoes.Select(i => new InscricaoResponse
        {
            Id = i.Id,
            AlunoId = i.AlunoId,
            CursoId = i.CursoId,
            Status = i.Status,
            DataInscricao = i.DataInscricao,
            NomeCurso = i.Curso?.Nome
        }).ToList();

        return lista;
    }

    public async Task<InscricaoResponse?> ObterInscricaoAsync(int alunoId, int cursoId)
    {
        var inscricao = await _inscricaoRepository.ObterInscricaoAsync(alunoId, cursoId);
        if (inscricao == null) return null;

        return new InscricaoResponse
        {
            Id = inscricao.Id,
            AlunoId = inscricao.AlunoId,
            CursoId = inscricao.CursoId,
            Status = inscricao.Status,
            DataInscricao = inscricao.DataInscricao,
            NomeCurso = inscricao.Curso?.Nome
        };
    }

    public async Task<bool> RemoverAsync(int alunoId, int cursoId)
    {
        return await _inscricaoRepository.RemoverAsync(alunoId, cursoId);
    }

    private async Task<bool> AlunoCursoExistem(int alunoId, int cursoId)
    {
        var aluno = await _usuarioRepository.BuscarPorIdAsync(alunoId);
        var curso = await _cursoRepository.BuscarPorIdAsync(cursoId);

        return aluno != null && curso != null;
    }
}