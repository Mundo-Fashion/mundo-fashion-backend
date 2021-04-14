using FluentValidation;
using FluentValidation.Results;
using MundoFashion.Core;
using MundoFashion.Core.Notifications;
using System;

namespace MundoFashion.Application.Services.Base
{
    public abstract class BaseServices
    {
        private readonly Notificador _notificador;

        protected BaseServices(Notificador notificador)
        {
            _notificador = notificador;
        }

        public async void Notificar(string message)
            => await _notificador.Notificar(new Notificacao(message));

        public void Notificar(ValidationResult validationResult)
        {
            foreach (ValidationFailure validation in validationResult.Errors)
                Notificar(validation.ErrorMessage);
        }

        public bool Validar<TEntity, TValidator>(TEntity entidade) where TEntity : Entity
                                                                   where TValidator : AbstractValidator<TEntity>
        {
            AbstractValidator<TEntity> validador = Activator.CreateInstance<TValidator>();
            ValidationResult resultadoValidacao = validador.Validate(entidade);

            if (!resultadoValidacao.IsValid)
                Notificar(resultadoValidacao);

            return resultadoValidacao.IsValid;
        }
    }
}
