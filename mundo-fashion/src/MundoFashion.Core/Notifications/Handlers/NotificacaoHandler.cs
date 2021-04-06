using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MundoFashion.Core.Notifications.Handlers
{
    public class NotificacaoHandler : INotificationHandler<Notificacao>
    {
        private readonly List<Notificacao> _notificacoes;
        public IReadOnlyList<Notificacao> Notificacoes => _notificacoes.AsReadOnly();
        public bool PossuiNotificaoes => _notificacoes.Count > 0;

        public NotificacaoHandler()
        {
            _notificacoes = new List<Notificacao>();
        }

        public Task Handle(Notificacao notification, CancellationToken cancellationToken)
        {
            _notificacoes.Add(notification);
            return Task.CompletedTask;
        }
    }
}
