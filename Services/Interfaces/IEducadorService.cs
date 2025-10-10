using AlfabetizaFeso.Api.DTOs.Educador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfabetizaFeso.Api.Services
{
    public interface IEducadorService
    {
        Task<IEnumerable<EducadorResponse>> ListarTodosAsync();
        Task<EducadorResponse?> BuscarPorIdAsync(int id);
        Task<EducadorResponse> AdicionarAsync(EducadorRequest educadorRequest);
        Task<EducadorResponse?> AuthenticateAsync(string email, string password);
        Task<EducadorResponse> AtualizarAsync(EducadorRequest educadorRequest, int id);
        Task<bool> RemoverAsync(int id);
    }
}