using AlfabetizaFeso.Api.DTOs.Aluno;
using AlfabetizaFeso.Api.DTOs.Educador;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IAlunoService
{
    Task<IEnumerable<AlunoResponse>> ListarTodosAsync();
    Task<AlunoResponse?> BuscarPorIdAsync(int id);
    Task<AlunoResponse> AdicionarAsync(AlunoRequest alunoRequest);
    Task<AlunoResponse> AtualizarAsync(AlunoRequest alunoRequest, int id);
    Task<bool> RemoverAsync(int id);
}
