using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class InscricaoRepository(AlfabetizaContexto context) : IInscricaoRepository
{
    private readonly AlfabetizaContexto _context = context;
    private readonly DbSet<Inscricao> _inscricaoDbSet = context.Inscricoes;

    public async Task<Inscricao> AdicionarAsync(Inscricao inscricao)
    {
        _inscricaoDbSet.Add(inscricao);
        await _context.SaveChangesAsync();

        return inscricao;
    }

    public async Task<Inscricao?> ObterInscricaoAsync(int alunoId, int cursoId)
    {
        var inscricao = await _inscricaoDbSet
            .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.CursoId == cursoId);

        return inscricao;
    }

    public async Task<Inscricao?> BuscarPorIdAsync(int id)
    {
        return await _inscricaoDbSet
            .Include(i => i.Aluno)
            .Include(i => i.Curso)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Inscricao>> ObterInscricoesPorAlunoIdAsync(int id)
    {
        var inscricoes = await _inscricaoDbSet
            .Where(i => i.AlunoId == id)
            .Include(i => i.Curso)
            .ToListAsync();

        return inscricoes;
    }

    public async Task<IEnumerable<Inscricao>> ObterInscricoesPorCursoIdAsync(int cursoId)
    {
        var inscricoes = await _inscricaoDbSet
            .Where(i => i.CursoId == cursoId)
            .Include(i => i.Aluno)
            .ToListAsync();

        return inscricoes;
    }

    public async Task<bool> RemoverAsync(int alunoId, int cursoId)
    {
        var inscricao = await _inscricaoDbSet
            .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.CursoId == cursoId);
        if (inscricao == null)
            return false;

        _inscricaoDbSet.Remove(inscricao);
        await _context.SaveChangesAsync();
        return true;
    }
}
