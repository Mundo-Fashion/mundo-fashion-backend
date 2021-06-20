using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Core.Extensions.Pagination.Models;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;

namespace MundoFashion.WebApi.Controllers
{   
    public class ServicoController : ApiControllerBase
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public ServicoController(INotificationHandler<Notificacao> notificacoes, IServicoRepository servicoRepository, IUsuarioRepository usuarioRepository, IMapper mapper) : base(notificacoes)
        {
            _servicoRepository = servicoRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("obter-servicos")]
        public ActionResult<PagedModel<ServicoEstampa>> ObterServicos(int pagina, int limitePorPagina)
        {
            return RespostaCustomizada(_servicoRepository.GetByPage(pagina, limitePorPagina));
        }

        [HttpGet]
        [Route("obter-servico/{servicoId:guid}")]
        public async Task<ActionResult<ServicoEstampaModel>> ObterServico(Guid servicoId)
        {
            ServicoEstampa servico = await _usuarioRepository.ObterServico(servicoId).ConfigureAwait(false);

            return RespostaCustomizada(_mapper.Map<ServicoEstampaModel>(servico));
        }
    }
}
