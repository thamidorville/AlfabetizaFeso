using AlfabetizaFeso.Api.Services.Interfaces;
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


    [HttpPost("aluno/{alunoId}/aula/{aulaId}")]
    public async Task<IActionResult> Inscrever(int alunoId, int aulaId)
    {
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


    [HttpDelete("aluno/{alunoId}/aula/{aulaId}")]
    public async Task<IActionResult> Cancelar(int alunoId, int aulaId)
    {
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
}