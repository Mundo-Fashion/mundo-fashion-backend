using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
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
    }
}
