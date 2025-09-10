using AlfabetizaFeso.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfabetizaFeso.Api.Services
{
    public interface IEducadorService
    {
        Task<IEnumerable<Educador>> ListarTodosAsync();
        Task<Educador> BuscarPorIdAsync(int id);
        Task<Educador> AdicionarAsync(Educador educador);
        Task<Educador> AtualizarAsync(Educador educador);
        Task<bool> RemoverAsync(int id);
    }
}