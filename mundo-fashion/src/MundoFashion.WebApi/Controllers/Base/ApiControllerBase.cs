using MediatR;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Notifications.Handlers;
using MundoFashion.WebApi.Controllers.Base.Responses;
using System.Linq;
using System.Net;

namespace MundoFashion.WebApi.Controllers.Base
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly NotificacaoHandler _notificacoes;

        protected ApiControllerBase(INotificationHandler<Notificacao> notificacoes)
        {
            _notificacoes = (NotificacaoHandler)notificacoes;
        }

        public IActionResult RespostaCustomizada(object resultado = null)
        {
            if (_notificacoes.PossuiNotificaoes)
                return BadRequest(new CustomResponse(HttpStatusCode.BadRequest, resultado)
                    .AdicionarErros(_notificacoes.Notificacoes.Select(notificacao => notificacao.Mensagem)));

            if (resultado == null)
                return NoContent();

            return Ok(new CustomResponse(HttpStatusCode.OK, resultado));
        }
    }
}
