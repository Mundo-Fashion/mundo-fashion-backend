using FluentValidation;

namespace MundoFashion.Domain.Validacoes
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Nome)
                .NotEmpty();

            RuleFor(u => u.Password)
                .NotEmpty();

            RuleFor(u => u.Role)
                .NotEmpty();
        }
    }
}
