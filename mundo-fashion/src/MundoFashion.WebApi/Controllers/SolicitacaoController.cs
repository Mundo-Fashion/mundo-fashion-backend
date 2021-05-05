using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Authorize]
    public class SolicitacaoController : ApiControllerBase
    {
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly SolicitacaoServices _solicitacaoServices;
        private readonly IMapper _mapper;
        public SolicitacaoController(INotificationHandler<Notificacao> notificacoes, IMapper mapper, SolicitacaoServices solicitacaoServices) : base(notificacoes)
        {
            _mapper = mapper;
            _solicitacaoServices = solicitacaoServices;
        }

        [HttpPost]
        [Route("adicionar-proposta")]
        public async Task<ActionResult<string>> AdicionarProposta(PropostaModel proposta)
        {
            Proposta novaProposta = _mapper.Map<Proposta>(proposta);

            await _solicitacaoServices.AdicionarProposta(proposta.SolicitacaoId, novaProposta);   

            return RespostaCustomizada("Proposta adicionada.");
        }

        [HttpPut]
        [Route("aceitar-proposta/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> AceitarProposta(Guid solicitacaoId)
        {
            await _solicitacaoServices.AceitarProposta(solicitacaoId);

            return RespostaCustomizada("Proposta aceita.");
        }

        [HttpPost]
        [Route("atualizar-proposta")]
        public async Task<ActionResult<string>> AtualizarProposta(PropostaModel proposta)
        {
            Proposta propostaAtualizada = _mapper.Map<Proposta>(proposta);

            await _solicitacaoServices.AtualizarProposta(proposta.SolicitacaoId, propostaAtualizada);

            return RespostaCustomizada("Proposta atualizada.");
        }

        [HttpPut]
        [Route("aceitar-solicitacao/{solicitacaoId:guid")]
        public async Task<ActionResult<string>> AceitarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.AceitarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação aceita.");
        }

        [HttpPut]
        [Route("pagar-solicitacao")]
        public async Task<ActionResult<string>> PagarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            solicitacao.Pagar();

            return RespostaCustomizada("Solicitação paga.");
        }

        [HttpPut]
        [Route("cancelar-solicitacao")]
        public async Task<ActionResult<string>> CancelarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            solicitacao.Cancelar();

            return RespostaCustomizada("Solicitação cancelada.");
        }
    }
}
