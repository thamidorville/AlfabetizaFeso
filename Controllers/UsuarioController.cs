using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Extensions;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        var usuarios = await _usuarioService.ListarTodosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponse>> GetById(int id)
    {
        var usuario = await _usuarioService.BuscarPorIdAsync(id);
        if (usuario == null)
            return NotFound();
        return Ok(usuario);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UsuarioResponse>> GetMyUser()
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        var usuario = await _usuarioService.BuscarPorIdAsync(userId);
        if (usuario == null)
            return NotFound();
        return Ok(usuario);
    }

    [HttpGet("educadores")]
    public async Task<ActionResult<IEnumerable<EducadorLista>>> GetEducadores()
    {
        var educadores = await _usuarioService.ListarEducadoresAsync();
        return Ok(educadores);
    }

    [HttpGet("alunos")]
    public async Task<ActionResult<IEnumerable<AlunoLista>>> GetAlunos()
    {
        var alunos = await _usuarioService.ListarAlunosAsync();
        return Ok(alunos);
    }

    [HttpPost("educador")]
    public async Task<ActionResult<UsuarioResponse>> CreateEducador(EducadorCadastro educadorCadastro)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var novo = await _usuarioService.AdicionarAsync(educadorCadastro);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Email já cadastrado") || ex.Message.Contains("senhas"))
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("aluno")]
    public async Task<ActionResult<UsuarioResponse>> CreateAluno(AlunoCadastro alunoCadastro)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var novo = await _usuarioService.AdicionarAsync(alunoCadastro);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Email já cadastrado") || ex.Message.Contains("senhas"))
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLogin login)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (usuario, token) = await _usuarioService.AuthenticateAsync(login);
        if (usuario == null || token == null) return Unauthorized();

        return Ok(new { usuario, token });
    }

    [HttpPut("educador")]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<UsuarioResponse>> UpdateEducador(EducadorEditar educadorEditar)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var atualizado = await _usuarioService.AtualizarAsync(educadorEditar, userId);
            return Ok(atualizado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("aluno")]
    [Authorize(Roles = "aluno")]
    public async Task<ActionResult<UsuarioResponse>> UpdateAluno(AlunoEditar alunoEditar)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var atualizado = await _usuarioService.AtualizarAsync(alunoEditar, userId);
            return Ok(atualizado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("alterar-senha")]
    [Authorize]
    public async Task<IActionResult> AlterarSenha(SenhaEditar senha)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var ok = await _usuarioService.AlterarSenhaAsync(userId, senha);
            if (!ok) return BadRequest("Senha atual inválida ou usuário não encontrado.");
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete()]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        var removido = await _usuarioService.RemoverAsync(userId);
        if (!removido) return NotFound();
        return NoContent();
    }
}