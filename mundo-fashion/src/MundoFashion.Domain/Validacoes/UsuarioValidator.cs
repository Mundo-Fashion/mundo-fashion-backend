using FluentValidation;

namespace MundoFashion.Domain.Validacoes
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .When(x => string.IsNullOrWhiteSpace(x.Email));

            RuleFor(u => u.Nome)
                .NotEmpty();

            RuleFor(u => u.Password)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Password));

            RuleFor(u => u.Role)
                .NotEmpty();
        }
    }
}
