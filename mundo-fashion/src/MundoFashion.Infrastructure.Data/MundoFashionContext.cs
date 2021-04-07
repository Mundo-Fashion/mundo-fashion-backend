using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain;

namespace MundoFashion.Infrastructure.Data
{
    public class MundoFashionContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public MundoFashionContext(DbContextOptions<MundoFashionContext> options) : base(options) { }
    }
}
