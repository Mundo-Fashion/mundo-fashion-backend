using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    public class MensagemMapping : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.Solicitacao)
                .WithMany(s => s.Mensagens)
                .HasForeignKey(m => m.SolicitacaoId);

            builder.ToTable("MensagensSolicitacao");
        }
    }
}
