using MediatR;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Core.Extensions.Pagination.Models;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Controllers.Base;

namespace MundoFashion.WebApi.Controllers
{
    public class ServicoController : ApiControllerBase
    {
        private readonly IServicoRepository _servicoRepository;
        public ServicoController(INotificationHandler<Notificacao> notificacoes, IServicoRepository servicoRepository) : base(notificacoes)
        {
            _servicoRepository = servicoRepository;
        }

        [HttpGet]
        [Route("obter-servicos")]
        public ActionResult<PagedModel<ServicoEstampa>> ObterServicos(int pagina, int limitePorPagina)
        {
            return RespostaCustomizada(_servicoRepository.GetByPage(pagina, limitePorPagina));
        }
    }
}
