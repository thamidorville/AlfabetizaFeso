using AlfabetizaFeso.Api.DTOs.Curso;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Services.Classes;

public class CursoService(ICursoRepository cursoRepository) : ICursoService
{
    private readonly ICursoRepository _cursoRepository = cursoRepository;

    public async Task<IEnumerable<CursoResponse>> ListarTodosAsync()
    {
        var cursos = await _cursoRepository.ListarTodosAsync();
        return cursos.Select(ToDto);
    }

    public async Task<IEnumerable<CursoResponse>> ListarPorEducadorAsync(int educadorId)
    {
        var cursos = await _cursoRepository.ListarPorEducadorAsync(educadorId);
        return cursos.Select(ToDto);
    }

    public async Task<CursoResponse?> BuscarPorIdAsync(int id)
    {
        var curso = await _cursoRepository.BuscarPorIdAsync(id);
        return curso != null ? ToDto(curso) : null;
    }

    public async Task<CursoResponse> AdicionarAsync(CursoCadastro cursoCadastro, int educadorId)
    {
        var curso = new Curso
        {
            Nome = cursoCadastro.Nome,
            Descricao = cursoCadastro.Descricao,
            CargaHoraria = cursoCadastro.CargaHoraria,
            DataInicio = cursoCadastro.DataInicio,
            DataFim = cursoCadastro.DataFim,
            EducadorId = educadorId,
            Status = "ativo"
        };

        var adicionado = await _cursoRepository.AdicionarAsync(curso);
        return ToDto(adicionado);
    }

    public async Task<CursoResponse> AtualizarAsync(int id, CursoCadastro cursoCadastro, int educadorId)
    {
        var curso = await _cursoRepository.BuscarPorIdAsync(id);
        if (curso == null || curso.EducadorId != educadorId)
            throw new InvalidOperationException("Curso não encontrado ou você não tem permissão para editá-lo");

        curso.Nome = cursoCadastro.Nome;
        curso.Descricao = cursoCadastro.Descricao;
        curso.CargaHoraria = cursoCadastro.CargaHoraria;
        curso.DataInicio = cursoCadastro.DataInicio;
        curso.DataFim = cursoCadastro.DataFim;

        var atualizado = await _cursoRepository.AtualizarAsync(curso);
        return ToDto(atualizado);
    }

    public async Task<bool> RemoverAsync(int id, int educadorId)
    {
        var curso = await _cursoRepository.BuscarPorIdAsync(id);
        if (curso == null || curso.EducadorId != educadorId)
            return false;

        await _cursoRepository.RemoverAsync(curso);
        return true;
    }

    private static CursoResponse ToDto(Curso curso)
    {
        return new CursoResponse
        {
            Id = curso.Id,
            Nome = curso.Nome,
            Descricao = curso.Descricao,
            CargaHoraria = curso.CargaHoraria,
            DataInicio = curso.DataInicio,
            DataFim = curso.DataFim,
            Status = curso.Status,
            EducadorId = curso.EducadorId,
            NomeEducador = curso.Educador?.Nome,
            TotalAulas = curso.Aulas?.Count ?? 0,
            TotalInscricoes = curso.Inscricoes?.Count ?? 0
        };
    }
}