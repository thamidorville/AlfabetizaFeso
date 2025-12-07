using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class PresencaAulaRepository(AlfabetizaContexto context) : IPresencaAulaRepository
{
    private readonly AlfabetizaContexto _context = context;
    private readonly DbSet<PresencaAula> _presencaDb = context.PresencasAula;

    public async Task<PresencaAula> AdicionarAsync(PresencaAula presenca)
    {
        _presencaDb.Add(presenca);
        await _context.SaveChangesAsync();
        return presenca;
    }

    public async Task<PresencaAula?> ObterPorInscricaoEAulaAsync(int inscricaoId, int aulaId)
    {
        return await _presencaDb
            .FirstOrDefaultAsync(p => p.InscricaoId == inscricaoId && p.AulaId == aulaId);
    }

    public async Task<IEnumerable<PresencaAula>> ObterPorAulaIdAsync(int aulaId)
    {
        return await _presencaDb
            .Where(p => p.AulaId == aulaId)
            .Include(p => p.Inscricao)
            .ToListAsync();
    }

    public async Task<IEnumerable<PresencaAula>> ObterPorInscricaoIdAsync(int inscricaoId)
    {
        return await _presencaDb
            .Where(p => p.InscricaoId == inscricaoId)
            .Include(p => p.Aula)
            .ToListAsync();
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var entity = await _presencaDb.FindAsync(id);
        if (entity == null) return false;
        _presencaDb.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}