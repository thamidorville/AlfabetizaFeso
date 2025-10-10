using System.Collections.Generic;

namespace AlfabetizaFeso.Api.Models
{
    public class Educador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty; // pequena bio/apresentação

        public ICollection<Aula> AulasMinistradas { get; set; } = new List<Aula>();
    }
}