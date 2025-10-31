using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class UsuarioRepository(AlfabetizaContexto contexto) : IUsuarioRepository
{
    private readonly AlfabetizaContexto _contexto = contexto;

    public async Task<Usuario> AdicionarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Add(usuario);
        await _contexto.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> AtualizarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
        await _contexto.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> BuscarPorEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email)) return null;

        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await _contexto.Usuarios.SingleOrDefaultAsync(e => e.Email.ToLower() == normalizedEmail);
    }

    public async Task<Usuario?> BuscarPorIdAsync(int id)
    {
        return await _contexto.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Usuario>> BuscarEducadorPorNomeAsync(string nome)
    {
        return await _contexto.Usuarios
            .Where(u => u.Role == "educador" && u.Nome.Contains(nome))
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> ListarTodosAlunosAsync()
    {
        return await _contexto.Usuarios
            .Where(u => u.Role == "aluno")
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> ListarTodosAsync()
    {
        return await _contexto.Usuarios.ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> ListarTodosEducadoresAsync()
    {
        return await _contexto.Usuarios
            .Where(u => u.Role == "educador")
            .ToListAsync();
    }

    public async Task<bool> RemoverAsync(Usuario usuario)
    {
        _contexto.Usuarios.Remove(usuario);
        await _contexto.SaveChangesAsync();
        return true;
    }
}