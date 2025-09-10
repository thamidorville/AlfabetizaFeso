using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfabetizaFeso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducadorController : ControllerBase
    {
        private readonly IEducadorService _educadorService;

        public EducadorController(IEducadorService educadorService)
        {
            _educadorService = educadorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Educador>>> GetAll()
        {
            var educadores = await _educadorService.ListarTodosAsync();
            return Ok(educadores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Educador>> GetById(int id)
        {
            var educador = await _educadorService.BuscarPorIdAsync(id);
            if (educador == null)
                return NotFound();
            return Ok(educador);
        }

        [HttpPost]
        public async Task<ActionResult<Educador>> Create(Educador educador)
        {
            var novoEducador = await _educadorService.AdicionarAsync(educador);
            return CreatedAtAction(nameof(GetById), new { id = novoEducador.Id }, novoEducador);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Educador>> Update(int id, Educador educador)
        {
            if (id != educador.Id)
                return BadRequest();

            var educadorAtualizado = await _educadorService.AtualizarAsync(educador);
            return Ok(educadorAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _educadorService.RemoverAsync(id);
            if (!removido)
                return NotFound();
            return NoContent();
        }
    }
}

