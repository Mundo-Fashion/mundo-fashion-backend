using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MundoFashion.Domain;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Infrastructure.Data.Mappings
{
    public class ServicoEstampaMapping : IEntityTypeConfiguration<ServicoEstampa>
    {
        public void Configure(EntityTypeBuilder<ServicoEstampa> builder)
        {
            builder.HasKey(s => s.Id);
            builder.ToTable("Servicos");
        }
    }
}
