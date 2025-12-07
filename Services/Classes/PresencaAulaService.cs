using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlfabetizaFeso.Api.DTOs.PresencaAula;
using AlfabetizaFeso.Api.Mappings;
using AlfabetizaFeso.Api.Models;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services.Interfaces;

namespace AlfabetizaFeso.Api.Services.Classes;

public class PresencaAulaService(
    IPresencaAulaRepository presencaAulaRepository,
    IInscricaoRepository inscricaoRepository,
    IAulaRepository aulaRepository) : IPresencaAulaService
{
    private readonly IPresencaAulaRepository _presencaAulaRepository = presencaAulaRepository;
    private readonly IInscricaoRepository _inscricaoRepository = inscricaoRepository;
    private readonly IAulaRepository _aulaRepository = aulaRepository;

    public async Task<PresencaAulaResponse> AdicionarAsync(PresencaAulaRequest request)
    {
        var inscricao = await _inscricaoRepository.BuscarPorIdAsync(request.InscricaoId);
        if (inscricao == null)
            throw new InvalidOperationException("Inscrição não encontrada");

        var aula = await _aulaRepository!.BuscarPorIdAsync(request.AulaId);
        if (aula == null)
            throw new InvalidOperationException("Aula não encontrada");

        if (inscricao.CursoId != aula.CursoId)
            throw new InvalidOperationException("A aula não pertence ao curso da inscrição");

        var existente = await _presencaAulaRepository.ObterPorInscricaoEAulaAsync(request.InscricaoId, request.AulaId);
        if (existente != null)
            throw new InvalidOperationException("Presença já registrada para esta inscrição e aula");

        var entity = request.ToEntity();
        var adicionado = await _presencaAulaRepository!.AdicionarAsync(entity);
        return adicionado.ToDto();
    }

    public async Task<PresencaAulaResponse?> ObterPorInscricaoEAulaAsync(int inscricaoId, int aulaId)
    {
        var presenca = await _presencaAulaRepository.ObterPorInscricaoEAulaAsync(inscricaoId, aulaId);
        return presenca?.ToDto();
    }

    public async Task<IEnumerable<PresencaAulaResponse>> ObterPorAulaIdAsync(int aulaId)
    {
        var lista = await _presencaAulaRepository.ObterPorAulaIdAsync(aulaId);
        return lista.Select(p => p.ToDto()).ToList();
    }

    public async Task<IEnumerable<PresencaAulaResponse>> ObterPorInscricaoIdAsync(int inscricaoId)
    {
        var lista = await _presencaAulaRepository.ObterPorInscricaoIdAsync(inscricaoId);
        return lista.Select(p => p.ToDto()).ToList();
    }

    public async Task<bool> RemoverAsync(int id)
    {
        return await _presencaAulaRepository.RemoverAsync(id);
    }
}