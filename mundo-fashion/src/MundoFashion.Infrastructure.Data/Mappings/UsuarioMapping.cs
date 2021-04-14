using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasMany(u => u.Empresas)
                .WithOne(e => e.Usuario)
                .HasForeignKey(u => u.UsuarioId);

            builder.HasOne(u => u.Servico)
                .WithOne(s => s.Usuario)
                .HasForeignKey<ServicoEstampa>(x => x.UsuarioId);

            builder.ToTable("Usuarios");
        }
    }
}
