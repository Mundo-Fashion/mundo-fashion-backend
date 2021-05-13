using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Infrastructure.Data
{
    public class MundoFashionContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<ServicoEstampa> Servicos { get; private set; }
        public DbSet<Solicitacao> Solicitacoes { get; private set; }
        public DbSet<DetalhesSolicitacao> DetalhesSolicitacoes { get; private set; }
        public DbSet<Proposta> Propostas { get; private set; }
        public DbSet<Mensagem> Mensagens { get; private set; }
        public MundoFashionContext(DbContextOptions<MundoFashionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MundoFashionContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
