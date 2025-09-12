using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class AulaRepository(AlfabetizaContexto contexto) : IAulaRepository
{
    private readonly AlfabetizaContexto _contexto = contexto;

    public async Task<Aula> AdicionarAsync(Aula aula)
    {
        _contexto.Aulas.Add(aula);
        await _contexto.SaveChangesAsync();

        return aula;
    }

    public async Task<Aula> AtualizarAsync(Aula aula)
    {
        _contexto.Update(aula);
        await _contexto.SaveChangesAsync();

        return aula;
    }

    public async Task<Aula?> BuscarPorIdAsync(int id)
    {
        return await _contexto.Aulas.FindAsync(id);
    }

    public async Task<IEnumerable<Aula>> ListarPorEducadorId(int educadorId)
    {
        var aulas = await _contexto.Aulas
            .Include(a => a.Educador)
            .Where(a => a.EducadorId == educadorId)
            .ToListAsync();

        return aulas;
    }

    public async Task<IEnumerable<Aula>> ListarTodosAsync()
    {
        return await _contexto.Aulas.ToListAsync();
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var aula = await _contexto.Aulas.FindAsync(id);
        if (aula == null)
            return false;

        _contexto.Aulas.Remove(aula);
        await _contexto.SaveChangesAsync();
        return true;
    }
}
