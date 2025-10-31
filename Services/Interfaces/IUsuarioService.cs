using AlfabetizaFeso.Api.DTOs.Usuario;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponse>> ListarTodosAsync();
    Task<UsuarioResponse> BuscarPorIdAsync(int id);
    Task<UsuarioResponse> AdicionarAsync(EducadorRequest educadorRequest);
    Task<UsuarioResponse> AdicionarAsync(AlunoRequest educadorRequest);
    Task<(UsuarioResponse?, string?)> AuthenticateAsync(UsuarioLogin usuarioLogin);
    Task<bool> AlterarSenhaAsync(int id, SenhaRequest senha);
    Task<UsuarioResponse> AtualizarAsync(EducadorUpdateRequest educadorRequest, int id);
    Task<UsuarioResponse> AtualizarAsync(AlunoUpdateRequest alunoRequest, int id);
    Task<bool> RemoverAsync(int id);
}
