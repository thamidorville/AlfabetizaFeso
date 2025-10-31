using AlfabetizaFeso.Api.Extensions;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscricaoController : ControllerBase
{
    private readonly IInscricaoService _inscricaoService;

    public InscricaoController(IInscricaoService inscricaoService)
    {
        _inscricaoService = inscricaoService;
    }


    [HttpPost("aula/{aulaId}")]
    [Authorize(Roles = "aluno")]
    public async Task<IActionResult> Inscrever(int aulaId)
    {
        if (User.GetUserId() is not int alunoId)
            return Unauthorized();

        try
        {
            var inscricao = await _inscricaoService.AdicionarAsync(alunoId, aulaId);
            return Ok(inscricao);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("aula/{aulaId}")]
    [Authorize(Roles = "aluno")]
    public async Task<IActionResult> Cancelar(int aulaId)
    {
        if (User.GetUserId() is not int alunoId)
            return Unauthorized();

        var removido = await _inscricaoService.RemoverAsync(alunoId, aulaId);
        if (!removido)
            return NotFound();
        return NoContent();
    }


    [HttpGet("aula/{aulaId}/alunos")]
    public async Task<IActionResult> ListarAlunosPorAula(int aulaId)
    {
        var alunos = await _inscricaoService.ObterAlunosInscritosPorAulaIdAsync(aulaId);
        return Ok(alunos);
    }


    [HttpGet("aluno/{alunoId}/aulas")]
    public async Task<IActionResult> ListarAulasPorAluno(int alunoId)
    {
        var aulas = await _inscricaoService.ObterAulasInscritasPorAlunoIdAsync(alunoId);
        return Ok(aulas);
    }

    [HttpGet("minhas-inscricoes")]
    public async Task<IActionResult> ListarMinhasAulas()
    {
        if (User.GetUserId() is not int alunoId)
            return Unauthorized();

        var aulas = await _inscricaoService.ObterAulasInscritasPorAlunoIdAsync(alunoId);
        return Ok(aulas);
    }
}