using AlfabetizaFeso.Api.DTOs.Usuario;

namespace AlfabetizaFeso.Api.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponse>> ListarTodosAsync();
    Task<IEnumerable<EducadorLista>> ListarEducadoresAsync();
    Task<IEnumerable<AlunoLista>> ListarAlunosAsync();
    Task<UsuarioResponse> BuscarPorIdAsync(int id);
    Task<UsuarioResponse> AdicionarAsync(EducadorCadastro educadorCadastro);
    Task<UsuarioResponse> AdicionarAsync(AlunoCadastro alunoCadastro);
    Task<(UsuarioResponse?, string?)> AuthenticateAsync(UsuarioLogin usuarioLogin);
    Task<bool> AlterarSenhaAsync(int id, SenhaEditar senha);
    Task<UsuarioResponse> AtualizarAsync(EducadorEditar educadorEditar, int id);
    Task<UsuarioResponse> AtualizarAsync(AlunoEditar alunoEditar, int id);
    Task<bool> RemoverAsync(int id);
}
