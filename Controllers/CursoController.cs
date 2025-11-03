using AlfabetizaFeso.Api.DTOs.Curso;
using AlfabetizaFeso.Api.Extensions;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursoController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoResponse>>> GetAll()
    {
        var cursos = await _cursoService.ListarTodosAsync();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CursoResponse>> GetById(int id)
    {
        var curso = await _cursoService.BuscarPorIdAsync(id);
        if (curso == null)
            return NotFound();
        return Ok(curso);
    }

    [HttpGet("educador/{educadorId}")]
    public async Task<ActionResult<IEnumerable<CursoResponse>>> GetByEducador(int educadorId)
    {
        var cursos = await _cursoService.ListarPorEducadorAsync(educadorId);
        return Ok(cursos);
    }

    [HttpGet("meus-cursos")]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<IEnumerable<CursoResponse>>> GetMeusCursos()
    {
        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        var cursos = await _cursoService.ListarPorEducadorAsync(educadorId);
        return Ok(cursos);
    }

    [HttpPost]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<CursoResponse>> Create(CursoCadastro cursoCadastro)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        try
        {
            var novo = await _cursoService.AdicionarAsync(cursoCadastro, educadorId);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<CursoResponse>> Update(int id, CursoCadastro cursoCadastro)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        try
        {
            var atualizado = await _cursoService.AtualizarAsync(id, cursoCadastro, educadorId);
            return Ok(atualizado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<IActionResult> Delete(int id)
    {
        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        var removido = await _cursoService.RemoverAsync(id, educadorId);
        if (!removido) return NotFound();
        return NoContent();
    }
}