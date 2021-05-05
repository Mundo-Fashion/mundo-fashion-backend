using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{
    public class EmpresaController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmpresaServices _empresaServices;
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaController(INotificationHandler<Notificacao> notificacoes, IMapper mapper, IEmpresaRepository empresaRepository) : base(notificacoes)
        {
            _mapper = mapper;
            _empresaRepository = empresaRepository;
        }

        [HttpPost]
        [Route("criar-solicitacao-empresa")]
        public async Task<ActionResult<string>> CriarSolicitacaoEmpresa(SolicitacaoModel solicitacao)
        {
            Solicitacao novaSolicitacao = _mapper.Map<Solicitacao>(solicitacao);

            await _empresaServices.AdicionarSolicitacaoEmpresa(UsuarioId, novaSolicitacao)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Solicitação criada com sucesso.");
        }

        [HttpPut]
        [Route("inativar-servico-empresa/{usuarioId:guid}")]
        public async Task<ActionResult<string>> InativarServicoEmpresa(Guid empresaId)
        {
            await _empresaServices.InativarServicoEmpresa(empresaId)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço removido com sucesso.");
        }

        [HttpPut]
        [Route("inativar-empresa/{empresaId:guid}")]
        public async Task<ActionResult<string>> InativarEmpresa(Guid empresaId)
        {
            await _empresaServices.InativarEmpresa(empresaId)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Empresa inativada com sucesso.");
        }
    }
}
