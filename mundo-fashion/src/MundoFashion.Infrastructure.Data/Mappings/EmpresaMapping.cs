using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Usuario)
                .WithMany(u => u.Empresas)
                .HasForeignKey(e => e.UsuarioId);

            builder.HasOne(e => e.Servico)
                .WithOne(s => s.Empresa)
                .HasForeignKey<ServicoEstampa>(x => x.EmpresaId);

            builder.HasMany(u => u.Solicitacoes)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId);

            builder.ToTable("Empresas");
        }
    }
}
