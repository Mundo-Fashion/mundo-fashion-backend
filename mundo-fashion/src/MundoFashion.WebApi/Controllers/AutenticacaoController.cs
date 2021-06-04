using MediatR;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Dto;
using MundoFashion.Core.Notifications;
using MundoFashion.WebApi.Controllers.Base;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{
    public class AutenticacaoController : ApiControllerBase
    {
        private readonly AutenticacaoServices _autenticacaoServices;

        public AutenticacaoController(INotificationHandler<Notificacao> notificacoes, AutenticacaoServices autenticacaoServices) : base(notificacoes)
        {
            _autenticacaoServices = autenticacaoServices;
        }

        [HttpPost]
        [Route("logar")]
        public async Task<ActionResult<TokenDto>> Logar(string email, string senha)
        {
            return RespostaCustomizada(await _autenticacaoServices.Logar(email, senha).ConfigureAwait(false));
        }
    }
}
