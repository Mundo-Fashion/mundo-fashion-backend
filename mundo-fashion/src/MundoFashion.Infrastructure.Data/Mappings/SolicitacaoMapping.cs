using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    public class SolicitacaoMapping : IEntityTypeConfiguration<Solicitacao>
    {
        public void Configure(EntityTypeBuilder<Solicitacao> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Detalhes)
                .WithOne(d => d.Solicitacao)
                .HasForeignKey<DetalhesSolicitacao>(s => s.SolicitacaoId);

            builder.HasOne(s => s.Proposta)
                .WithOne(p => p.Solicitacao)
                .HasForeignKey<Proposta>(p => p.SolicitacaoId);

            builder.Property(s => s.Codigo)
                .ValueGeneratedOnAdd();

            builder.ToTable("Solicitacoes");
        }
    }
}
