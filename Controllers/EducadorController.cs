using AlfabetizaFeso.Api.DTOs.Educador;
using AlfabetizaFeso.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<EducadorResponse>>> GetAll()
        {
            var educadores = await _educadorService.ListarTodosAsync();
            return Ok(educadores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EducadorResponse>> GetById(int id)
        {
            var educador = await _educadorService.BuscarPorIdAsync(id);
            if (educador == null)
                return NotFound();
            return Ok(educador);
        }

        [HttpPost]
        public async Task<ActionResult<EducadorResponse>> Create(EducadorRequest educadorRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoEducador = await _educadorService.AdicionarAsync(educadorRequest);
            return CreatedAtAction(nameof(GetById), new { id = novoEducador.Id }, novoEducador);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EducadorResponse>> Update(int id, EducadorRequest educadorRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var educadorAtualizado = await _educadorService.AtualizarAsync(educadorRequest, id);
                return Ok(educadorAtualizado);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
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

