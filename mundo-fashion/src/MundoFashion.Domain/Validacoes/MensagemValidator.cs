using FluentValidation;
using System;

namespace MundoFashion.Domain.Validacoes
{
    public class MensagemValidator : AbstractValidator<Mensagem>
    {
        public MensagemValidator()
        {
            RuleFor(m => m.EmissorId)
                .NotEqual(Guid.Empty);

            RuleFor(m => m.ReceptorId)
                .NotEqual(Guid.Empty);

            RuleFor(m => m.Conteudo)
                .NotEmpty();
        }
    }
}
