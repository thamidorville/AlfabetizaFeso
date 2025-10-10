using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfabetizaFeso.Api.Repositories
{
    public class EducadorRepository : IEducadorRepository
    {
        private readonly AlfabetizaContexto _contexto;

        public EducadorRepository(AlfabetizaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Educador>> ListarTodosAsync()
        {
            return await _contexto.Educadores.ToListAsync();
        }

        public async Task<Educador?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Educadores.FindAsync(id);
        }

        public async Task<Educador?> BuscarPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            var normalized = email.Trim().ToLowerInvariant();
            return await _contexto.Educadores.SingleOrDefaultAsync(e => e.Email.ToLower() == normalized);
        }

        public async Task<Educador> AdicionarAsync(Educador educador)
        {
            _contexto.Educadores.Add(educador);
            await _contexto.SaveChangesAsync();
            return educador;
        }

        public async Task<Educador> AtualizarAsync(Educador educador)
        {
            _contexto.Educadores.Update(educador);
            await _contexto.SaveChangesAsync();
            return educador;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var educador = await _contexto.Educadores.FindAsync(id);
            if (educador == null)
                return false;

            _contexto.Educadores.Remove(educador);
            await _contexto.SaveChangesAsync();
            return true;
        }
    }
}