using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;

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

            builder.ToTable("Usuarios");
        }
    }
}
