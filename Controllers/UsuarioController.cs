using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Services.Classes;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege por padrão; endpoints de registro/login liberados com [AllowAnonymous]
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

    [HttpPost("educador")]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioResponse>> CreateEducador(EducadorRequest educadorRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var novo = await _usuarioService.AdicionarAsync(educadorRequest);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Email já cadastrado") || ex.Message.Contains("senhas"))
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("aluno")]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioResponse>> CreateAluno(AlunoRequest alunoRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var novo = await _usuarioService.AdicionarAsync(alunoRequest);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Email já cadastrado") || ex.Message.Contains("senhas"))
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UsuarioLogin login)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (usuario, token) = await _usuarioService.AuthenticateAsync(login);
        if (usuario == null || token == null) return Unauthorized();

        return Ok(new { usuario, token });
    }

    [HttpPut("educador/{id}")]
    public async Task<ActionResult<UsuarioResponse>> UpdateEducador(int id, EducadorUpdateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var atualizado = await _usuarioService.AtualizarAsync(request, id);
            return Ok(atualizado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("aluno/{id}")]
    public async Task<ActionResult<UsuarioResponse>> UpdateAluno(int id, AlunoUpdateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var atualizado = await _usuarioService.AtualizarAsync(request, id);
            return Ok(atualizado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/senha")]
    public async Task<IActionResult> AlterarSenha(int id, SenhaRequest senha)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var ok = await _usuarioService.AlterarSenhaAsync(id, senha);
            if (!ok) return BadRequest("Senha atual inválida ou usuário não encontrado.");
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _usuarioService.RemoverAsync(id);
        if (!removido) return NotFound();
        return NoContent();
    }
}