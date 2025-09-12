using AlfabetizaFeso.Api.DTOs.Educador;
using AlfabetizaFeso.Api.Repositories;
using AlfabetizaFeso.Api.Mappings;

namespace AlfabetizaFeso.Api.Services
{
    public class EducadorService : IEducadorService
    {
        private readonly IEducadorRepository _educadorRepository;

        public EducadorService(IEducadorRepository educadorRepository)
        {
            _educadorRepository = educadorRepository;
        }

        public async Task<IEnumerable<EducadorResponse>> ListarTodosAsync()
        {
            var educadores = await _educadorRepository.ListarTodosAsync();
            List<EducadorResponse> educadoresResponse = educadores
                .Select(e => e.ToDto())
                .ToList();

            return educadoresResponse;
        }

        public async Task<EducadorResponse?> BuscarPorIdAsync(int id)
        {
            var educador = await _educadorRepository.BuscarPorIdAsync(id);
            if (educador is null)
                return null;

            return educador.ToDto();
        }

        public async Task<EducadorResponse> AdicionarAsync(EducadorRequest educadorRequest)
        {
            var educador = educadorRequest.ToEntity();
            var educadorAdicionado =  await _educadorRepository.AdicionarAsync(educador);
            return educadorAdicionado.ToDto();
        }

        public async Task<EducadorResponse> AtualizarAsync(EducadorRequest educadorRequest, int id)
        {
            var educador = educadorRequest.ToEntity(id);
            var educadorAtualizado = await _educadorRepository.AtualizarAsync(educador);
            return educadorAtualizado.ToDto();
        }

        public async Task<bool> RemoverAsync(int id)
        {
            return await _educadorRepository.RemoverAsync(id);
        }
    }
}