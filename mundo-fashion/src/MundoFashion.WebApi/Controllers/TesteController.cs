using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MundoFashion.Core.Notifications;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Controllers.Base.Responses;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ApiControllerBase
    {
        private readonly ILogger<TesteController> _logger;
        private readonly Notificador _notificador;

        public TesteController(ILogger<TesteController> logger, INotificationHandler<Notificacao> notificationHandler, Notificador notificador) : base(notificationHandler)
        {
            _logger = logger;
            _notificador = notificador;
        }

        [HttpGet]
        [Route("nocontent-request")]
        public IActionResult GetNoContent()
        {
            return RespostaCustomizada(null);
        }

        [HttpGet]
        [Route("success-request")]
        public IActionResult GetSuccess()
        {
            return RespostaCustomizada(new { Teste = "SUCESSO" });
        }

        [HttpGet]
        [Route("bad-request")]
        public async Task<IActionResult> GetBadRequest()
        {
            await _notificador.Notificar(new Notificacao("Deu pau irmão"));
            await _notificador.Notificar(new Notificacao("Deu pau irmão 2"));
            await _notificador.Notificar(new Notificacao("Deu pau irmão 3"));

            return RespostaCustomizada();
        }
    }
}
