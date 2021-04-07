using MundoFashion.Core.Notifications;

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
    }
}
