using AlfabetizaFeso.Api.DTOs.Educador;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Mappings;

public static class EducadorMapping
{
    public static EducadorResponse ToDto(this Educador educador)
    {
        return new EducadorResponse
        {
            Id = educador.Id,
            Nome = educador.Nome,
            Descricao = educador.Descricao,
            Email = educador.Email,
            Especialidade = educador.Especialidade,
            Telefone = educador.Telefone
        };
    }

    public static Educador ToEntity(this EducadorRequest educadorRequest)
    {
        return new Educador
        {
            Nome = educadorRequest.Nome,
            Descricao = educadorRequest.Descricao,
            Email = educadorRequest.Email,
            Especialidade = educadorRequest.Especialidade,
            Telefone = educadorRequest.Telefone
        };
    }

    // overload do metodo ToEntity para retornar entidade com id.
    // util para quando for atualizar a entidade.
    public static Educador ToEntity(this EducadorRequest educadorRequest, int id)
    {
        return new Educador
        {
            Id = id,
            Nome = educadorRequest.Nome,
            Descricao = educadorRequest.Descricao,
            Email = educadorRequest.Email,
            Especialidade = educadorRequest.Especialidade,
            Telefone = educadorRequest.Telefone
        };
    }
}
