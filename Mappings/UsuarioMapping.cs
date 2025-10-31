using AlfabetizaFeso.Api.DTOs.Usuario;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Mappings;

public static class UsuarioMapping
{
    // ====== CRIAÇÃO ======
    public static Usuario ToEntity(this EducadorRequest req)
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

    public static Usuario ToEntity(this AlunoRequest req)
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

    // ====== ATUALIZAÇÃO (aplica campos do DTO na entidade existente, sem tocar SenhaHash) ======
    public static void UpdateFrom(this Usuario target, EducadorUpdateRequest src)
    {
        // Email é tratado explicitamente no serviço (normalização/verificação)
        target.Nome = src.Nome;
        target.Telefone = src.Telefone;
        target.Descricao = src.Descricao;
        target.Especialidade = src.Especialidade;
    }

    public static void UpdateFrom(this Usuario target, AlunoUpdateRequest src)
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
            Tipo = string.Equals(usuario.Role, "educador", StringComparison.OrdinalIgnoreCase) ? "Educador" : "Aluno"
        };

        return dto;
    }
}