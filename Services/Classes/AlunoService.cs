using AlfabetizaFeso.Api.DTOs.Aluno;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Repositories;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;

    public async Task<AlunoResponse> AdicionarAsync(AlunoRequest alunoRequest)
    {
        var aluno = alunoRequest.ToEntity();
        var alunoAdicionado = await _alunoRepository.AdicionarAsync(aluno);

        return alunoAdicionado.ToDto();
    }

    public async Task<AlunoResponse> AtualizarAsync(AlunoRequest alunoRequest, int id)
    {
        var aluno = alunoRequest.ToEntity(id);
        var alunoAdicionado = await _alunoRepository.AtualizarAsync(aluno);

        return alunoAdicionado.ToDto();
    }

    public async Task<AlunoResponse?> BuscarPorIdAsync(int id)
    {
        var aluno = await _alunoRepository.BuscarPorIdAsync(id);
        if (aluno is null)
            return null;

        return aluno.ToDto();
    }

    public async Task<IEnumerable<AlunoResponse>> ListarTodosAsync()
    {
        var alunos = await _alunoRepository.ListarTodosAsync();

        List<AlunoResponse> alunosResponse = alunos
            .Select(a => a.ToDto())
            .ToList();

        return alunosResponse;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        return await _alunoRepository.RemoverAsync(id);
    }
}
