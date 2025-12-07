using AlfabetizaFeso.Api.DTOs.PresencaAula;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Mappings;

public static class PresencaAulaMapping
{
    public static PresencaAulaResponse ToDto(this PresencaAula presenca)
    {
        return new PresencaAulaResponse
        {
            Id = presenca.Id,
            InscricaoId = presenca.InscricaoId,
            AulaId = presenca.AulaId,
            Presente = presenca.Presente,
            DataCriacao = presenca.DataCriacao
        };
    }

    public static PresencaAula ToEntity(this PresencaAulaRequest req)
    {
        return new PresencaAula
        {
            InscricaoId = req.InscricaoId,
            AulaId = req.AulaId,
            Presente = req.Presente,
            DataCriacao = DateTime.UtcNow
        };
    }
}