using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoFashion.Domain.Validacoes
{
    public class SolicitacaoValidator : AbstractValidator<Solicitacao>
    {
        public SolicitacaoValidator()
        {
            RuleFor(s => s.Status)
                .IsInEnum();

            RuleFor(s => s.Detalhes)
                .SetValidator(new DetalhesSolicitacaoValidator());
        }
    }
}
