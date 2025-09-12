using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Models;
using System.Runtime.CompilerServices;

namespace AlfabetizaFeso.Api.Mappings;

public static class AulaMapping
{
    public static AulaResponse ToDto(this Aula aula)
    {
        return new AulaResponse
        {
            Id = aula.Id,
            Titulo = aula.Titulo,
            Descricao = aula.Descricao,
            EducadorId = aula.EducadorId,
            DataInicioUtc = aula.DataInicioUtc,
            DataFinalUtc = aula.DataFinalUtc
        };
    }

    public static Aula ToEntity(this AulaRequest aulaRequest)
    {
        return new Aula
        {
            Titulo = aulaRequest.Titulo,
            Descricao = aulaRequest.Descricao,
            EducadorId = aulaRequest.EducadorId,
            DataInicioUtc = aulaRequest.DataInicio.ToUniversalTime(),
            DataFinalUtc = aulaRequest.DataFinal.ToUniversalTime(), 
        };
    }
}
