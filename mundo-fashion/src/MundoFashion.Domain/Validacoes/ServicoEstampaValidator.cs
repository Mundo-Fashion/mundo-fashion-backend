using FluentValidation;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Domain.Validacoes
{
    public class ServicoEstampaValidator : AbstractValidator<ServicoEstampa>
    {
        public ServicoEstampaValidator()
        {
            RuleFor(d => d.TipoEstampa)
               .IsInEnum();

            RuleFor(d => d.Tecnica)
                .IsInEnum();

            RuleFor(d => d.TecnicaEstamparia)
                .IsInEnum();

            RuleFor(d => d.Nicho)
                .IsInEnum();

            RuleFor(d => d.TipoRapport)
                .IsInEnum();
        }
    }
}
