using AlfabetizaFeso.Api.DTOs.Educador;
using AlfabetizaFeso.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AlfabetizaFeso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducadorController : ControllerBase
    {
        private readonly IEducadorService _educadorService;
        private readonly IConfiguration _configuration;

        public EducadorController(IEducadorService educadorService, IConfiguration configuration)
        {
            _educadorService = educadorService;
            _configuration = configuration;
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

        [HttpPost("cadastrar")]
        public async Task<ActionResult<EducadorResponse>> Create(CadastrarEducadorDto cadastrarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // map CadastrarEducadorDto -> EducadorRequest (reusing existing mapping logic)
            var educadorRequest = new EducadorRequest
            {
                Nome = cadastrarDto.Nome,
                Especialidade = cadastrarDto.Especialidade,
                Email = cadastrarDto.Email,
                Password = cadastrarDto.Password,
                ConfirmPassword = cadastrarDto.Password,
                Telefone = cadastrarDto.Telefone,
                Descricao = cadastrarDto.Descricao
            };

            var novoEducador = await _educadorService.AdicionarAsync(educadorRequest);
            return CreatedAtAction(nameof(GetById), new { id = novoEducador.Id }, novoEducador);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(EducadorLogin login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var educador = await _educadorService.AuthenticateAsync(login.Email, login.Password);
            if (educador == null) return Unauthorized();

            // gerar JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, educador.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, educador.Email),
                new Claim("nome", educador.Nome)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString, expires = token.ValidTo });
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

