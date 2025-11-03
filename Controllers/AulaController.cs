using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Extensions;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AulaController : ControllerBase
{
    private readonly IAulaService _aulaService;

    public AulaController(IAulaService aulaService)
    {
        _aulaService = aulaService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AulaResponse>>> GetAll()
    {
        var aulas = await _aulaService.ListarTodosAsync();
        return Ok(aulas);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<AulaResponse>> GetById(int id)
    {
        var aula = await _aulaService.BuscarPorIdAsync(id);
        if (aula == null)
            return NotFound();
        return Ok(aula);
    }

    [HttpGet("educador/{educadorId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AulaResponse>>> GetByEducadorId(int educadorId)
    {
        try
        {
            var aulas = await _aulaService.ListarPorEducadorIdAsync(educadorId);
            return Ok(aulas);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("minhas-aulas")]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<IEnumerable<AulaResponse>>> GetByMyId()
    {
        if(User.GetUserId() is not int educadorId)
            return Unauthorized();

        try
        {
            var aulas = await _aulaService.ListarPorEducadorIdAsync(educadorId);
            return Ok(aulas);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<AulaResponse>> Create(AulaCadastro aulaCadastro)
    {
        if (User.GetUserId() is not int educadorId)
            return Unauthorized();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var novaAula = await _aulaService.AdicionarAsync(aulaCadastro, educadorId);
            return CreatedAtAction(nameof(GetById), new { id = novaAula.Id }, novaAula);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<ActionResult<AulaResponse>> Update(int aulaId, AulaCadastro aulaCadastro)
    {
        if(User.GetUserId() is not int educadorId)
            return Unauthorized();


        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aulaAtualizada = await _aulaService.AtualizarAsync(aulaCadastro, aulaId, educadorId);
            return Ok(aulaAtualizada);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _aulaService.RemoverAsync(id);
        if (!removido)
            return NotFound();
        return NoContent();
    }
}
