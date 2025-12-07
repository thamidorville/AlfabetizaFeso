using AlfabetizaFeso.Api.DTOs.Aula;
using AlfabetizaFeso.Api.Models;

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
            DataInicio = aula.DataInicio,
            DataFinal = aula.DataFinal,
            CursoId = aula.CursoId,
            NomeCurso = aula.Curso?.Nome,
            LinkAula = aula.LinkAula
        };
    }

    public static Aula ToEntity(this AulaCadastro aulaCadastro, int cursoId)
    {
        return new Aula
        {
            Titulo = aulaCadastro.Titulo,
            Descricao = aulaCadastro.Descricao,
            DataInicio = aulaCadastro.DataInicio,
            DataFinal = aulaCadastro.DataFinal,
            CursoId = cursoId,
            LinkAula = aulaCadastro.LinkAula
        };
    }
}