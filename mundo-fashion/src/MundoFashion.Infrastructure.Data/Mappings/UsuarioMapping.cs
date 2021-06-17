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

            builder.HasOne(u => u.Servico)
                .WithOne(s => s.Prestador)
                .HasForeignKey<ServicoEstampa>(x => x.PrestadorId);

            builder.Property(u => u.AvatarLink)
                .HasDefaultValue("http://projeto-mundofashion-bucket.storage.googleapis.com/DefaultProfile.jpg");

            builder.ToTable("Usuarios");
        }
    }
}
