using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    public class PropostaMapping : IEntityTypeConfiguration<Proposta>
    {
        public void Configure(EntityTypeBuilder<Proposta> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Solicitacao)
                .WithOne(s => s.Proposta)
                .HasForeignKey<Solicitacao>(s => s.PropostaId);

            builder.ToTable("Propostas");
        }
    }
}
