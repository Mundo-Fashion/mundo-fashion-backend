using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    public class DetalhesSolicitacaoMapping : IEntityTypeConfiguration<DetalhesSolicitacao>
    {
        public void Configure(EntityTypeBuilder<DetalhesSolicitacao> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Solicitacao)
                .WithOne(s => s.Detalhes)
                .HasForeignKey<Solicitacao>(s => s.DetalhesId);

            builder.ToTable("DetalhesSolicitacoes");
        }
    }
}
