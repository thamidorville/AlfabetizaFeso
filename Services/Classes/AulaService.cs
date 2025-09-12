using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Repositories;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class AulaService(IAulaRepository aulaRepo) : IAulaService
{
    private readonly IAulaRepository _aulaRepo = aulaRepo;

    public async Task<AulaResponse> AdicionarAsync(AulaRequest aulaRequest)
    {
        var aula = aulaRequest.ToEntity();
        var aulaAdicionada = await _aulaRepo.AdicionarAsync(aula);
        
        return aulaAdicionada.ToDto();
    }

    public Task<AulaResponse> AtualizarAsync(AulaRequest aulaRequest, int id)
    {
        throw new NotImplementedException();
    }

    public Task<AulaResponse?> BuscarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AulaResponse>> ListarPorEducadorIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AulaResponse>> ListarTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoverAsync(int id)
    {
        throw new NotImplementedException();
    }
}
