using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlfabetizaFeso.Api.Services.Classes;

public class UsuarioService(
    IUsuarioRepository usuarioRepository,
    IPasswordHasher<Usuario> hasher,
    IConfiguration configuration) : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IPasswordHasher<Usuario> _hasher = hasher;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<UsuarioResponse>> ListarTodosAsync()
    {
        var usuarios = await _usuarioRepository.ListarTodosAsync();
        return usuarios
            .Select(u => u.ToDto()!)
            .ToList();
    }

    public async Task<UsuarioResponse> BuscarPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        return usuario.ToDto()!;
    }

    public async Task<UsuarioResponse> AdicionarAsync(EducadorRequest educadorRequest)
    {
        if (educadorRequest.Senha != educadorRequest.ConfirmarSenha)
            throw new Exception("As senhas não conferem");

        var usuario = educadorRequest.ToEntity();

        // Normalizar email
        usuario.Email = usuario.Email.Trim().ToLowerInvariant();

        // Checar duplicado
        var existente = await _usuarioRepository.BuscarPorEmailAsync(usuario.Email);
        if (existente != null)
            throw new InvalidOperationException("Email já cadastrado");

        // Hash da senha antes de salvar
        usuario.SenhaHash = _hasher.HashPassword(usuario, educadorRequest.Senha);
        var adicionado = await _usuarioRepository.AdicionarAsync(usuario);
        return adicionado.ToDto()!;
    }

    public async Task<UsuarioResponse> AdicionarAsync(AlunoRequest alunoRequest)
    {
        if (alunoRequest.Senha != alunoRequest.ConfirmarSenha)
            throw new InvalidOperationException("As senhas não conferem");

        var usuario = alunoRequest.ToEntity();

        // Normalizar email
        usuario.Email = usuario.Email.Trim().ToLowerInvariant();

        // Checar duplicado
        var existente = await _usuarioRepository.BuscarPorEmailAsync(usuario.Email);
        if (existente != null)
            throw new InvalidOperationException("Email já cadastrado");

        // Hash da senha antes de salvar
        usuario.SenhaHash = _hasher.HashPassword(usuario, alunoRequest.Senha);
        var adicionado = await _usuarioRepository.AdicionarAsync(usuario);
        return adicionado.ToDto()!;
    }

    public async Task<(UsuarioResponse?, string?)> AuthenticateAsync(UsuarioLogin usuarioLogin)
    {
        var usuario = await _usuarioRepository.BuscarPorEmailAsync(usuarioLogin.Email);
        if (usuario == null) return (null, null);

        var result = _hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, usuarioLogin.Senha);
        if (result == PasswordVerificationResult.Failed) return (null, null);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Email),
            // Role já está armazenada em Usuario.Role
            new(ClaimTypes.Role, usuario.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var usuarioResponse = usuario.ToDto();
        return (usuarioResponse, tokenString);
    }

    public async Task<bool> AlterarSenhaAsync(int id, SenhaRequest senha)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario is null) return false;

        var result = _hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha.SenhaAntiga);
        if (result == PasswordVerificationResult.Failed) return false;

        if (senha.SenhaNova != senha.ConfirmarSenha)
            throw new InvalidOperationException("As senhas não conferem");

        usuario.SenhaHash = _hasher.HashPassword(usuario, senha.SenhaNova);
        await _usuarioRepository.AtualizarAsync(usuario);

        return true;
    }

    public async Task<UsuarioResponse> AtualizarAsync(EducadorUpdateRequest educadorRequest, int id)
    {
        var usuarioExistente = await _usuarioRepository.BuscarPorIdAsync(id);

        if (usuarioExistente is null || !string.Equals(usuarioExistente.Role, "educador", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("O usuário informado não é um educador.");

        // Se o email mudou, normalizar e checar duplicado
        var novoEmail = educadorRequest.Email.Trim().ToLowerInvariant();
        if (!string.Equals(usuarioExistente.Email, novoEmail, StringComparison.OrdinalIgnoreCase))
        {
            var outro = await _usuarioRepository.BuscarPorEmailAsync(novoEmail);
            if (outro != null && outro.Id != id)
                throw new InvalidOperationException("Email já cadastrado por outro usuário");
            usuarioExistente.Email = novoEmail;
        }

        usuarioExistente.UpdateFrom(educadorRequest);
        usuarioExistente.Role = "educador"; // garantir role
        var atualizado = await _usuarioRepository.AtualizarAsync(usuarioExistente);
        return atualizado.ToDto()!;
    }

    public async Task<UsuarioResponse> AtualizarAsync(AlunoUpdateRequest alunoRequest, int id)
    {
        var usuarioExistente = await _usuarioRepository.BuscarPorIdAsync(id);

        if (usuarioExistente is null || !string.Equals(usuarioExistente.Role, "aluno", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("O usuário informado não é um aluno.");

        // Se o email mudou, normalizar e checar duplicado
        var novoEmail = alunoRequest.Email.Trim().ToLowerInvariant();
        if (!string.Equals(usuarioExistente.Email, novoEmail, StringComparison.OrdinalIgnoreCase))
        {
            var outroUsuario = await _usuarioRepository.BuscarPorEmailAsync(novoEmail);
            if (outroUsuario != null && outroUsuario.Id != id)
                throw new InvalidOperationException("Email já cadastrado por outro usuário.");

            usuarioExistente.Email = novoEmail;
        }

        usuarioExistente.UpdateFrom(alunoRequest);
        usuarioExistente.Role = "aluno"; // garantir role
        await _usuarioRepository.AtualizarAsync(usuarioExistente);
        return usuarioExistente.ToDto()!;
    }


    public async Task<bool> RemoverAsync(int id)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null) return false;

        await _usuarioRepository.RemoverAsync(usuario);
        return true;
    }
}