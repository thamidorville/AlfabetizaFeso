using Microsoft.EntityFrameworkCore;
using AlfabetizaFeso.Api.Models;

namespace AlfabetizaFeso.Api.Data
{
    public class AlfabetizaContexto(DbContextOptions<AlfabetizaContexto> options) : DbContext(options)
    {
        public DbSet<Educador> Educadores { get; set; }
        public DbSet<Aula> Aulas { get; set; }
    }
}
