using MediatR;
using System.Threading.Tasks;

namespace MundoFashion.Core.Notifications
{
    public class Notificador
    {
        private readonly IMediator _mediator;

        public Notificador(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Notificar(Notificacao notificacao)
        {
            await _mediator.Publish(notificacao);
        }
    }
}
