using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;
using AlfabetizaFeso.Api.Mappings;

namespace AlfabetizaFeso.Api.Services.Classes;

public class AulaService(IAulaRepository aulaRepository, ICursoRepository cursoRepository) : IAulaService
{
    private readonly IAulaRepository _aulaRepository = aulaRepository;
    private readonly ICursoRepository _cursoRepository = cursoRepository;

    public async Task<IEnumerable<AulaResponse>> ListarPorCursoAsync(int cursoId)
    {
        var aulas = await _aulaRepository.ListarPorCursoAsync(cursoId);
        return aulas.Select(a => a.ToDto());
    }

    public async Task<AulaResponse?> BuscarPorIdAsync(int id)
    {
        var aula = await _aulaRepository.BuscarPorIdAsync(id);
        return aula != null ? aula.ToDto() : null;
    }

    public async Task<AulaResponse> AdicionarAsync(AulaCadastro aulaCadastro, int cursoId, int educadorId)
    {
        var curso = await _cursoRepository.BuscarPorIdAsync(cursoId);
        if (curso == null || curso.EducadorId != educadorId)
            throw new InvalidOperationException("Curso não encontrado ou você não tem permissão para adicionar aulas");

        var aula = new Aula
        {
            Titulo = aulaCadastro.Titulo,
            Descricao = aulaCadastro.Descricao,
            DataInicio = aulaCadastro.DataInicio,
            DataFinal = aulaCadastro.DataFinal,
            CursoId = cursoId,
            LinkAula = aulaCadastro.LinkAula
        };

        var adicionada = await _aulaRepository.AdicionarAsync(aula);
        return adicionada.ToDto();
    }

    public async Task<AulaResponse> AtualizarAsync(int id, AulaCadastro aulaCadastro, int cursoId, int educadorId)
    {
        var aula = await _aulaRepository.BuscarPorIdAsync(id);
        if (aula == null || aula.CursoId != cursoId)
            throw new InvalidOperationException("Aula não encontrada");

        var curso = await _cursoRepository.BuscarPorIdAsync(cursoId);
        if (curso == null || curso.EducadorId != educadorId)
            throw new InvalidOperationException("Você não tem permissão para editar esta aula");

        aula.Titulo = aulaCadastro.Titulo;
        aula.Descricao = aulaCadastro.Descricao;
        aula.DataInicio = aulaCadastro.DataInicio;
        aula.DataFinal = aulaCadastro.DataFinal;
        aula.LinkAula = aulaCadastro.LinkAula;

        var atualizada = await _aulaRepository.AtualizarAsync(aula);
        return atualizada.ToDto();
    }

    public async Task<bool> RemoverAsync(int id, int cursoId, int educadorId)
    {
        var aula = await _aulaRepository.BuscarPorIdAsync(id);
        if (aula == null || aula.CursoId != cursoId)
            return false;

        var curso = await _cursoRepository.BuscarPorIdAsync(cursoId);
        if (curso == null || curso.EducadorId != educadorId)
            return false;

        await _aulaRepository.RemoverAsync(aula);
        return true;
    }

    public async Task<IEnumerable<AulaResponse>> ListarPorEducadorAsync(int educadorId)
    {
        var cursos = await _cursoRepository.ListarPorEducadorAsync(educadorId);

        var aulas = cursos
            .Where(c => c.Aulas != null)
            .SelectMany(c => c.Aulas)
            .GroupBy(a => a.Id)
            .Select(g => g.First())
            .OrderBy(a => a.DataInicio)
            .Select(a => a.ToDto());

        return aulas;
    }
}