using FluentValidation;

namespace MundoFashion.Domain.Validacoes
{
    public class DetalhesSolicitacaoValidator : AbstractValidator<DetalhesSolicitacao>
    {
        public DetalhesSolicitacaoValidator()
        {
            RuleFor(d => d.TipoEstampa)
                .IsInEnum()
                .Unless(x => x.TipoEstampa == 0);

            RuleFor(d => d.Tecnica)
                .IsInEnum()
                .Unless(x => x.Tecnica == 0);


            RuleFor(d => d.TecnicaEstamparia)
                .IsInEnum()
                .Unless(x => x.TecnicaEstamparia == 0);

            RuleFor(d => d.Nicho)
                .IsInEnum()
                .Unless(x => x.Nicho == 0);

            RuleFor(d => d.TipoRapport)
                .IsInEnum()
                .Unless(x => x.TipoRapport == 0);
        }
    }
}
