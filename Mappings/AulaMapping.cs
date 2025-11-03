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

    public static Aula ToEntity(this AulaCadastro aulaCadastro, int educadorId)
    {
        return new Aula
        {
            Titulo = aulaCadastro.Titulo,
            Descricao = aulaCadastro.Descricao,
            EducadorId = educadorId,
            DataInicioUtc = aulaCadastro.DataInicio.ToUniversalTime(),
            DataFinalUtc = aulaCadastro.DataFinal.ToUniversalTime()
        };
    }

    public static Aula ToEntity(this AulaCadastro aulaCadastro, int aulaId, int educadorId)
    {
        return new Aula
        {
            Id = aulaId,
            Titulo = aulaCadastro.Titulo,
            Descricao = aulaCadastro.Descricao,
            EducadorId = educadorId,
            DataInicioUtc = aulaCadastro.DataInicio.ToUniversalTime(),
            DataFinalUtc = aulaCadastro.DataFinal.ToUniversalTime()
        };
    }
}