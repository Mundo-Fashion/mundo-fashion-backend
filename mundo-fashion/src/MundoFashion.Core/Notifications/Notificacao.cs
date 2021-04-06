using MediatR;

namespace MundoFashion.Core.Notifications
{
    public class Notificacao : INotification
    {
        public string Mensagem { get; private set; }

        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
