using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Mappings;

public static class UsuarioMapping
{
    // ====== CRIAÇÃO ======
    public static Usuario ToEntity(this EducadorCadastro req)
    {
        return new Usuario
        {
            Nome = req.Nome,
            Email = req.Email,
            Telefone = req.Telefone,
            Descricao = req.Descricao,
            Especialidade = req.Especialidade,
            Role = "educador"
            // SenhaHash será setada pelo serviço antes de persistir
        };
    }

    public static Usuario ToEntity(this AlunoCadastro req)
    {
        return new Usuario
        {
            Nome = req.Nome,
            Email = req.Email,
            Telefone = req.Telefone,
            Descricao = req.Descricao,
            Role = "aluno"
            // SenhaHash será setada pelo serviço antes de persistir
        };
    }

    // ====== ATUALIZAÇÃO ======
    public static void UpdateFrom(this Usuario target, EducadorEditar src)
    {
        // Email é tratado explicitamente no serviço (normalização/verificação)
        target.Nome = src.Nome;
        target.Telefone = src.Telefone;
        target.Descricao = src.Descricao;
        target.Especialidade = src.Especialidade;
    }

    public static void UpdateFrom(this Usuario target, AlunoEditar src)
    {
        // Email é tratado explicitamente no serviço (normalização/verificação)
        target.Nome = src.Nome;
        target.Telefone = src.Telefone;
        target.Descricao = src.Descricao;
    }

    // ====== LEITURA ======
    public static UsuarioResponse? ToDto(this Usuario? usuario)
    {
        if (usuario is null) return null;

        var dto = new UsuarioResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Descricao = usuario.Descricao,
            Especialidade = usuario.Especialidade,
            Role = usuario.Role
        };

        return dto;
    }
}