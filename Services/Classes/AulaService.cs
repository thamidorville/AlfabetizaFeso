using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class AulaService(IAulaRepository aulaRepository, ICursoRepository cursoRepository) : IAulaService
{
    private readonly IAulaRepository _aulaRepository = aulaRepository;
    private readonly ICursoRepository _cursoRepository = cursoRepository;

    public async Task<IEnumerable<AulaResponse>> ListarPorCursoAsync(int cursoId)
    {
        var aulas = await _aulaRepository.ListarPorCursoAsync(cursoId);
        return aulas.Select(ToDto);
    }

    public async Task<AulaResponse?> BuscarPorIdAsync(int id)
    {
        var aula = await _aulaRepository.BuscarPorIdAsync(id);
        return aula != null ? ToDto(aula) : null;
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
            EducadorId = educadorId
        };

        var adicionada = await _aulaRepository.AdicionarAsync(aula);
        return ToDto(adicionada);
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

        var atualizada = await _aulaRepository.AtualizarAsync(aula);
        return ToDto(atualizada);
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

    private static AulaResponse ToDto(Aula aula)
    {
        return new AulaResponse
        {
            Id = aula.Id,
            Titulo = aula.Titulo,
            Descricao = aula.Descricao,
            DataInicio = aula.DataInicio,
            DataFinal = aula.DataFinal,
            CursoId = aula.CursoId,
            EducadorId = aula.EducadorId,
            NomeCurso = aula.Curso?.Nome,
            NomeEducador = aula.Educador?.Nome
        };
    }
}