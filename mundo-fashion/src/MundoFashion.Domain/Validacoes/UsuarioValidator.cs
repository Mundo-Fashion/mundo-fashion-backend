using FluentValidation;

namespace MundoFashion.Domain.Validacoes
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty();

            RuleFor(u => u.Password)
                .NotEmpty();

            RuleFor(u => u.Role)
                .NotEmpty();
        }
    }
}
