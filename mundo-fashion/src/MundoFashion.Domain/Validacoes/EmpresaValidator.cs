using FluentValidation;

namespace MundoFashion.Domain.Validacoes
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty();

            RuleFor(e => e.Cnpj)
                .NotEmpty();
        }
    }
}
