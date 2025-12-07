using AlfabetizaFeso.Api.DTOs.Inscricao;
using AlfabetizaFeso.Api.DTOs.Aula;
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
    private readonly IPresencaAulaService _presencaAulaService;
    private readonly IAulaService _aulaService;

    public InscricaoController(
        IInscricaoService inscricaoService,
        IPresencaAulaService presencaAulaService,
        IAulaService aulaService)
    {
        _inscricaoService = inscricaoService;
        _presencaAulaService = presencaAulaService;
        _aulaService = aulaService;
    }

    [HttpPost("curso/{cursoId}")]
    [Authorize(Roles = "aluno")]
    public async Task<IActionResult> Inscrever(int cursoId)
    {
        if (User.GetUserId() is not int alunoId)
            return Unauthorized();

        try
        {
            var ok = await _inscricaoService.AdicionarAsync(alunoId, cursoId);
            if (!ok) return BadRequest();
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("curso/{cursoId}")]
    [Authorize(Roles = "aluno")]
    public async Task<IActionResult> Cancelar(int cursoId)
    {
        if (User.GetUserId() is not int alunoId)
            return Unauthorized();

        var removido = await _inscricaoService.RemoverAsync(alunoId, cursoId);
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
    [Authorize(Roles = "aluno")]
    public async Task<IActionResult> ListarMinhasAulas()
    {
        if (User.GetUserId() is not int alunoId)
            return Unauthorized();

        var aulas = await _inscricaoService.ObterAulasInscritasPorAlunoIdAsync(alunoId);
        return Ok(aulas);
    }

    [HttpGet("aluno/{alunoId}/inscricoes")]
    [Authorize]
    public async Task<IActionResult> ListarInscricoesPorAluno(int alunoId)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (userId != alunoId && !User.IsInRole("educador"))
            return Forbid();

        var inscricoes = await _inscricaoService.ObterInscricoesPorAlunoIdAsync(alunoId);
        return Ok(inscricoes);
    }

    [HttpGet("aluno/{alunoId}/curso/{cursoId}")]
    [Authorize]
    public async Task<IActionResult> ObterInscricao(int alunoId, int cursoId)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (userId != alunoId && !User.IsInRole("educador"))
            return Forbid();

        var inscricao = await _inscricaoService.ObterInscricaoAsync(alunoId, cursoId);
        if (inscricao == null) return NotFound();
        return Ok(inscricao);
    }

    // Retorna as aulas em que o aluno foi marcado como presente
    [HttpGet("aluno/{alunoId}/aulas-presentes")]
    [Authorize]
    public async Task<IActionResult> ListarAulasPresentes(int alunoId)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (userId != alunoId && !User.IsInRole("educador"))
            return Forbid();

        var inscricoes = await _inscricaoService.ObterInscricoesPorAlunoIdAsync(alunoId);
        var aulaIds = new HashSet<int>();

        foreach (var inscricao in inscricoes)
        {
            var presencas = await _presencaAulaService.ObterPorInscricaoIdAsync(inscricao.Id);
            foreach (var p in presencas.Where(p => p.Presente))
            {
                aulaIds.Add(p.AulaId);
            }
        }

        var aulasPresentes = new List<AulaResponse>();
        foreach (var aulaId in aulaIds)
        {
            var aula = await _aulaService.BuscarPorIdAsync(aulaId);
            if (aula != null)
                aulasPresentes.Add(aula);
        }

        return Ok(aulasPresentes);
    }

    // Retorna a soma da carga horária (em horas) das aulas presentes do aluno
    [HttpGet("aluno/{alunoId}/presencas/carga-horaria")]
    [Authorize]
    public async Task<IActionResult> ObterCargaHorariaPresenca(int alunoId)
    {
        if (User.GetUserId() is not int userId)
            return Unauthorized();

        if (userId != alunoId && !User.IsInRole("educador"))
            return Forbid();

        var inscricoes = await _inscricaoService.ObterInscricoesPorAlunoIdAsync(alunoId);
        var aulaIds = new HashSet<int>();

        foreach (var inscricao in inscricoes)
        {
            var presencas = await _presencaAulaService.ObterPorInscricaoIdAsync(inscricao.Id);
            foreach (var p in presencas.Where(p => p.Presente))
            {
                aulaIds.Add(p.AulaId);
            }
        }

        double totalHoras = 0.0;
        foreach (var aulaId in aulaIds)
        {
            var aula = await _aulaService.BuscarPorIdAsync(aulaId);
            if (aula == null) continue;

            totalHoras += (aula.DataFinal - aula.DataInicio).TotalHours;
        }

        return Ok(new { totalHoras = Math.Round(totalHoras, 2) });
    }
}