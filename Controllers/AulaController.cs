using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Extensions;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/curso/{cursoId}/aula")]
public class AulaController : ControllerBase
{
    private readonly IAulaService _aulaService;
    private readonly ICursoService _cursoService;

    public AulaController(IAulaService aulaService, ICursoService cursoService)
    {
        _aulaService = aulaService;
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AulaResponse>>> GetByCurso(int cursoId)
    {
        var aulas = await _aulaService.ListarPorCursoAsync(cursoId);
        return Ok(aulas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AulaResponse>> GetById(int cursoId, int id)
    {
        var aula = await _aulaService.BuscarPorIdAsync(id);
        if (aula == null || aula.CursoId != cursoId)
            return NotFound();
        return Ok(aula);
    }

    [HttpPost]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<AulaResponse>> Create(int cursoId, AulaCadastro aulaCadastro)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        try
        {
            var nova = await _aulaService.AdicionarAsync(aulaCadastro, cursoId, educadorId);
            return CreatedAtAction(nameof(GetById), new { cursoId, id = nova.Id }, nova);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<AulaResponse>> Update(int cursoId, int id, AulaCadastro aulaCadastro)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        try
        {
            var atualizada = await _aulaService.AtualizarAsync(id, aulaCadastro, cursoId, educadorId);
            return Ok(atualizada);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<IActionResult> Delete(int cursoId, int id)
    {
        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        var removida = await _aulaService.RemoverAsync(id, cursoId, educadorId);
        if (!removida) return NotFound();
        return NoContent();
    }
}