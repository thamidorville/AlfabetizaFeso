using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfabetizaFeso.Api.Services
{
    public class EducadorService : IEducadorService
    {
        private readonly IEducadorRepository _educadorRepository;

        public EducadorService(IEducadorRepository educadorRepository)
        {
            _educadorRepository = educadorRepository;
        }

        public async Task<IEnumerable<Educador>> ListarTodosAsync()
        {
            return await _educadorRepository.ListarTodosAsync();
        }

        public async Task<Educador> BuscarPorIdAsync(int id)
        {
            return await _educadorRepository.BuscarPorIdAsync(id);
        }

        public async Task<Educador> AdicionarAsync(Educador educador)
        {
            return await _educadorRepository.AdicionarAsync(educador);
        }

        public async Task<Educador> AtualizarAsync(Educador educador)
        {
            return await _educadorRepository.AtualizarAsync(educador);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            return await _educadorRepository.RemoverAsync(id);
        }
    }
}