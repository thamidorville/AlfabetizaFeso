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

    public async Task<Inscricao?> ObterInscricaoAsync(int alunoId, int aulaId)
    {
        var inscricao = await _inscricaoDbSet
            .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.AulaId == aulaId);

        return inscricao;
    }

    public async Task<IEnumerable<Inscricao>> ObterInscricoesPorAlunoIdAsync(int id)
    {
        var inscricoes = await _inscricaoDbSet
            .Where(i => i.AlunoId == id)
            .Include(i => i.Aula)
            .ToListAsync();

        return inscricoes;
    }

    public async Task<IEnumerable<Inscricao>> ObterInscricoesPorAulaIdAsync(int id)
    {
        var inscricoes = await _inscricaoDbSet
            .Where(i => i.AulaId == id)
            .Include(i => i.Aluno)
            .ToListAsync();

        return inscricoes;
    }

    public async Task<bool> RemoverAsync(int alunoId, int aulaId)
    {
        var inscricao = await _inscricaoDbSet
            .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.AulaId == aulaId);
        if (inscricao == null)
            return false;

        _inscricaoDbSet.Remove(inscricao);
        await _context.SaveChangesAsync();
        return true;
    }
}
