using AlfabetizaFeso.Api.DTOs.Aluno;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlfabetizaFeso.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunoController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoResponse>>> GetAll()
    {
        var alunos = await _alunoService.ListarTodosAsync();
        return Ok(alunos);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoResponse>> GetById(int id)
    {
        var aluno = await _alunoService.BuscarPorIdAsync(id);
        if (aluno == null)
            return NotFound();
        return Ok(aluno);
    }


    [HttpPost]
    public async Task<ActionResult<AlunoResponse>> Create(AlunoRequest alunoRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var novoAluno = await _alunoService.AdicionarAsync(alunoRequest);
        return CreatedAtAction(nameof(GetById), new { id = novoAluno.Id }, novoAluno);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<AlunoResponse>> Update(int id, AlunoRequest alunoRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var alunoAtualizado = await _alunoService.AtualizarAsync(alunoRequest, id);
        if (alunoAtualizado == null)
            return NotFound();

        return Ok(alunoAtualizado);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _alunoService.RemoverAsync(id);
        if (!removido)
            return NotFound();
        return NoContent();
    }
}
