using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;
using System.Collections.Generic;

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

            builder.Property(s => s.Imagens)
                .HasColumnType("text[]");

            builder.ToTable("DetalhesSolicitacoes");
        }
    }
}
