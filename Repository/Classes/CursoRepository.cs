using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class CursoRepository(AlfabetizaContexto context) : ICursoRepository
{
    private readonly AlfabetizaContexto _context = context;

    public async Task<IEnumerable<Curso>> ListarTodosAsync()
    {
        return await _context.Cursos
            .Include(c => c.Educador)
            .Include(c => c.Aulas)
            .Include(c => c.Inscricoes)
            .ToListAsync();
    }

    public async Task<IEnumerable<Curso>> ListarPorEducadorAsync(int educadorId)
    {
        return await _context.Cursos
            .Include(c => c.Educador)
            .Include(c => c.Aulas)
            .Include(c => c.Inscricoes)
            .Where(c => c.EducadorId == educadorId)
            .ToListAsync();
    }

    public async Task<Curso?> BuscarPorIdAsync(int id)
    {
        return await _context.Cursos
            .Include(c => c.Educador)
            .Include(c => c.Aulas)
            .Include(c => c.Inscricoes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Curso> AdicionarAsync(Curso curso)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
        return curso;
    }

    public async Task<Curso> AtualizarAsync(Curso curso)
    {
        _context.Cursos.Update(curso);
        await _context.SaveChangesAsync();
        return curso;
    }

    public async Task RemoverAsync(Curso curso)
    {
        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
    }
}