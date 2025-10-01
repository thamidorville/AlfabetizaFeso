using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlfabetizaFeso.Api.Repository.Classes;

public class AlunoRepository(AlfabetizaContexto context) : IAlunoRepository
{
    private readonly AlfabetizaContexto _context = context;
    private readonly DbSet<Aluno> _alunoDbSet = context.Alunos;

    public async Task<Aluno> AdicionarAsync(Aluno aluno)
    {
        _alunoDbSet.Add(aluno);
        await _context.SaveChangesAsync();
        return aluno;
    }

    public async Task<Aluno> AtualizarAsync(Aluno aluno)
    {
        _alunoDbSet.Update(aluno);
        await _context.SaveChangesAsync();

        return aluno;
    }

    public async Task<Aluno?> BuscarPorIdAsync(int id)
    {
        var aluno = await _alunoDbSet.FindAsync(id);

        return aluno;
    }

    public async Task<IEnumerable<Aluno>> ListarTodosAsync()
    {
        var alunos = await _alunoDbSet.ToListAsync();
        
        return alunos;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var alunos = await _alunoDbSet.FindAsync(id);
        if (alunos == null)
            return false;

        _alunoDbSet.Remove(alunos);
        await _context.SaveChangesAsync();
        return true;
    }
}
