using MediatR;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Notifications.Handlers;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace MundoFashion.WebApi.Controllers.Base
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly NotificacaoHandler _notificacoes;

        protected Guid UsuarioId => Guid.Parse(User.FindFirst(ClaimTypes.Email).Value);
        protected ApiControllerBase(INotificationHandler<Notificacao> notificacoes)
        {
            _notificacoes = (NotificacaoHandler)notificacoes;
        }

        public ActionResult<TResultado> RespostaCustomizada<TResultado>(TResultado resultado)
        {
            if (_notificacoes.PossuiNotificaoes)
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Errors = _notificacoes.Notificacoes.Select(notificacao => notificacao.Mensagem) });

            return Ok(new { StatusCode = HttpStatusCode.OK, Data = resultado });
        }
    }
}
