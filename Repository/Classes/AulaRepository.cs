using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class AulaRepository(AlfabetizaContexto context) : IAulaRepository
{
    private readonly AlfabetizaContexto _context = context;

    public async Task<IEnumerable<Aula>> ListarPorCursoAsync(int cursoId)
    {
        return await _context.Aulas
            .Include(a => a.Curso)
            .Where(a => a.CursoId == cursoId)
            .OrderBy(a => a.DataInicio)
            .ToListAsync();
    }

    public async Task<Aula?> BuscarPorIdAsync(int id)
    {
        return await _context.Aulas
            .Include(a => a.Curso)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Aula> AdicionarAsync(Aula aula)
    {
        _context.Aulas.Add(aula);
        await _context.SaveChangesAsync();
        return aula;
    }

    public async Task<Aula> AtualizarAsync(Aula aula)
    {
        _context.Aulas.Update(aula);
        await _context.SaveChangesAsync();
        return aula;
    }

    public async Task RemoverAsync(Aula aula)
    {
        _context.Aulas.Remove(aula);
        await _context.SaveChangesAsync();
    }
}