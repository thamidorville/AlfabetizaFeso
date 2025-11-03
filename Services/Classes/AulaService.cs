using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class AulaService(IAulaRepository aulaRepo, IUsuarioRepository usuarioRepo) : IAulaService
{
    private readonly IAulaRepository _aulaRepo = aulaRepo;
    private readonly IUsuarioRepository _usuarioRepo = usuarioRepo;

    public async Task<AulaResponse> AdicionarAsync(AulaCadastro aulaCadastro, int educadorId)
    {
        await VerificaExistenciaEducadorAsync(educadorId);

        var aula = aulaCadastro.ToEntity(educadorId);
        var aulaAdicionada = await _aulaRepo.AdicionarAsync(aula);

        return aulaAdicionada.ToDto();
    }

    public async Task<AulaResponse> AtualizarAsync(AulaCadastro aulaCadastro, int aulaId, int educadorId)
    {
        await VerificaExistenciaEducadorAsync(educadorId);

        var aula = aulaCadastro.ToEntity(aulaId, educadorId);
        var aulaAtualizada = await _aulaRepo.AtualizarAsync(aula);

        return aulaAtualizada.ToDto();
    }

    public async Task<AulaResponse?> BuscarPorIdAsync(int id)
    {
        var aula = await _aulaRepo.BuscarPorIdAsync(id);

        if (aula != null)
            return aula.ToDto();

        return null;
    }

    public async Task<IEnumerable<AulaResponse>> ListarPorEducadorIdAsync(int educadorId)
    {
        await VerificaExistenciaEducadorAsync(educadorId);

        var aulas = await _aulaRepo.ListarPorEducadorId(educadorId);
        List<AulaResponse> aulasResponse = aulas
            .Select(a => a.ToDto())
            .ToList();

        return aulasResponse;
    }

    public async Task<IEnumerable<AulaResponse>> ListarTodosAsync()
    {
        var aulas = await _aulaRepo.ListarTodosAsync();
        List<AulaResponse> aulasResponse = aulas
            .Select(a => a.ToDto())
            .ToList();

        return aulasResponse;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        return await _aulaRepo.RemoverAsync(id);
    }

    private async Task VerificaExistenciaEducadorAsync(int educadorId)
    {
        var educador = await _usuarioRepo.BuscarPorIdAsync(educadorId);
        if (educador == null)
        {
            throw new KeyNotFoundException("Educador não encontrado.");
        }
    }
}