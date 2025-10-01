using AlfabetizaFeso.Api.DTOs.Aluno;
using AlfabetizaFeso.Api.Models;
using System.Runtime.CompilerServices;

namespace AlfabetizaFeso.Api.Mappings;

public class AlunoMapping
{
    public static AlunoResponse ToDto(this Aluno aluno)
    {
        return new AlunoResponse
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Email = aluno.Email,
            Telefone = aluno.Telefone,
            Descricao = aluno.Descricao
        };
    }

    public static Aluno ToEntity(this AlunoRequest alunoRequest)
    {
        return new Aluno
        {
            Nome = alunoRequest.Nome,
            Email = alunoRequest.Email,
            Telefone = alunoRequest.Telefone,
            Descricao = alunoRequest.Descricao
        };
    }

    public static Aluno ToEntity(this AlunoRequest alunoRequest, int id)
    {
        return new Aluno
        {
            Id = id,
            Nome = alunoRequest.Nome,
            Email = alunoRequest.Email,
            Telefone = alunoRequest.Telefone,
            Descricao = alunoRequest.Descricao
        };
    }
}
