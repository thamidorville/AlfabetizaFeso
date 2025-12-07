using AlfabetizaFeso.Api.DTOs.PresencaAula;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresencaAulaController : ControllerBase
{
    private readonly IPresencaAulaService _presencaAulaService;

    public PresencaAulaController(IPresencaAulaService presencaAulaService)
    {
        _presencaAulaService = presencaAulaService;
    }

    [HttpPost]
    [Authorize(Roles = "educador")]
    public async Task<IActionResult> Marcar(PresencaAulaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var criado = await _presencaAulaService.AdicionarAsync(request);
            return CreatedAtAction(nameof(GetByInscricaoEAula), new { inscricaoId = criado.InscricaoId, aulaId = criado.AulaId }, criado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("inscricao/{inscricaoId}/aula/{aulaId}")]
    [Authorize]
    public async Task<IActionResult> GetByInscricaoEAula(int inscricaoId, int aulaId)
    {
        var presenca = await _presencaAulaService.ObterPorInscricaoEAulaAsync(inscricaoId, aulaId);
        if (presenca == null) return NotFound();
        return Ok(presenca);
    }

    [HttpGet("aula/{aulaId}")]
    [Authorize(Roles = "educador")]
    public async Task<IActionResult> GetByAula(int aulaId)
    {
        var lista = await _presencaAulaService.ObterPorAulaIdAsync(aulaId);
        return Ok(lista);
    }

    [HttpGet("inscricao/{inscricaoId}")]
    [Authorize]
    public async Task<IActionResult> GetByInscricao(int inscricaoId)
    {
        var lista = await _presencaAulaService.ObterPorInscricaoIdAsync(inscricaoId);
        return Ok(lista);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "educador")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _presencaAulaService.RemoverAsync(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}