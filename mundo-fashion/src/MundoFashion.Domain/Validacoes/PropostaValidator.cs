using FluentValidation;

namespace MundoFashion.Domain.Validacoes
{
    public class PropostaValidator : AbstractValidator<Proposta>
    {
        public PropostaValidator()
        {
            RuleFor(p => p.Valor)
                .GreaterThanOrEqualTo(0);            
        }
    }
}
