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

            RuleFor(u => u.Senha)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Senha));

            RuleFor(u => u.Role)
                .NotEmpty();

            RuleFor(u => u.Cpf)
                .NotEmpty();    
        }
    }
}
