using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MundoFashion.Application.Services;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Storage;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;
using MundoFashion.WebApi.Models.Mensagem;
using MundoFashion.WebApi.Models.Solicitacao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{
    [Authorize]
    public class SolicitacaoController : ApiControllerBase
    {
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly SolicitacaoServices _solicitacaoServices;
        private readonly ICloudStorage _cloudStorage;
        private readonly IMapper _mapper;
        private readonly ILogger<SolicitacaoController> _logger;
        public SolicitacaoController(INotificationHandler<Notificacao> notificacoes, IMapper mapper, SolicitacaoServices solicitacaoServices, ISolicitacaoRepository solicitacaoRepository, ICloudStorage cloudStorage, ILogger<SolicitacaoController> logger) : base(notificacoes)
        {
            _mapper = mapper;
            _solicitacaoServices = solicitacaoServices;
            _solicitacaoRepository = solicitacaoRepository;
            _cloudStorage = cloudStorage;
            _logger = logger;
        }

        [HttpGet]
        [Route("obter-solicitacoes-prestador/{prestadorId:guid}")]
        public ActionResult<List<SolicitacaoModel>> ObterSolicitacoesPorPrestador(Guid prestadorId)
            => RespostaCustomizada(_mapper.Map<List<SolicitacaoModel>>(_solicitacaoRepository.ObterSolicitacoes(s => s.Servico.PrestadorId == prestadorId)));

        [HttpGet]
        [Route("obter-solicitacoes-tomador/{tomadorId:guid}")]
        public ActionResult<List<SolicitacaoModel>> ObterSolicitacoesPorTomador(Guid tomadorId)
            => RespostaCustomizada(_mapper.Map<List<SolicitacaoModel>>(_solicitacaoRepository.ObterSolicitacoes(s => s.TomadorId == tomadorId)));


        [HttpPost]
        [Route("criar-solicitacao")]
        public async Task<ActionResult<string>> CriarSolicitacao([FromForm] NovaSolicitacaoModel solicitacao)
        {
            DetalhesSolicitacao detalhesSolicitacao = new DetalhesSolicitacao(solicitacao.Detalhes.TipoEstampa, solicitacao.Detalhes.TipoTecnica,
                                                                              solicitacao.Detalhes.TipoTecnicaEstamparia, solicitacao.Detalhes.TipoNicho,
                                                                              solicitacao.Detalhes.TipoRapport, solicitacao.Detalhes.Observacoes);

            Solicitacao novaSolicitacao = new Solicitacao(solicitacao.TomadorServicoId, solicitacao.ServicoId, detalhesSolicitacao);

            _logger.LogInformation($"Quantidade imagens: {solicitacao.Detalhes.ImagensUpload.Count}");

            foreach (Microsoft.AspNetCore.Http.IFormFile imagem in solicitacao.Detalhes.ImagensUpload)
            {
                string nomeImagem = $"{Guid.NewGuid()}_{imagem.FileName}";

                _logger.LogInformation("Vai salvar a imagem");
                var imagemUrl = await _cloudStorage.UploadFileAsync(imagem, nomeImagem).ConfigureAwait(false);

                _logger.LogInformation($"Imagem url: {imagemUrl}");
                _logger.LogInformation("Vai setar na solicitação");
                novaSolicitacao.Detalhes.AdicionarImagem(imagemUrl);
            }

            await _solicitacaoServices.AdicionarSolicitacao(novaSolicitacao, detalhesSolicitacao).ConfigureAwait(false);

            return RespostaCustomizada("Solicitação criada com sucesso!");
        }

        [HttpGet]
        [Route("obter-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<SolicitacaoModel>> ObterSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);
            SolicitacaoModel solicitacaoModel = _mapper.Map<SolicitacaoModel>(solicitacao);

            return RespostaCustomizada(solicitacaoModel);
        }

        [HttpPost]
        [Route("adicionar-proposta/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> AdicionarProposta(Guid solicitacaoId, PropostaModel proposta)
        {
            Proposta novaProposta = new Proposta(proposta.Valor);

            await _solicitacaoServices.AdicionarProposta(solicitacaoId, novaProposta);

            return RespostaCustomizada("Proposta adicionada.");
        }

        [HttpPut]
        [Route("aceitar-proposta/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> AceitarProposta(Guid solicitacaoId)
        {
            await _solicitacaoServices.AceitarProposta(solicitacaoId);

            return RespostaCustomizada("Proposta aceita.");
        }

        [HttpPut]
        [Route("recusar-proposta/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> RecusarProposta(Guid solicitacaoId)
        {
            await _solicitacaoServices.RecusarProposta(solicitacaoId);

            return RespostaCustomizada("Proposta recusada.");
        }

        [HttpPut]
        [Route("atualizar-proposta/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> AtualizarProposta(Guid solicitacaoId, PropostaModel proposta)
        {
            Proposta propostaAtualizada = _mapper.Map<Proposta>(proposta);

            await _solicitacaoServices.AtualizarProposta(solicitacaoId, propostaAtualizada);

            return RespostaCustomizada("Proposta atualizada.");
        }

        [HttpPut]
        [Route("aceitar-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> AceitarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.AceitarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação aceita.");
        }

        [HttpPut]
        [Route("recusar-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> RecusarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.RecusarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação recusada.");
        }

        [HttpPut]
        [Route("pagar-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> PagarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.PagarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação paga.");
        }

        [HttpPut]
        [Route("cancelar-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> CancelarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.CancelarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação cancelada.");
        }

        [HttpPut]
        [Route("entregar-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> EntregarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.EntregarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação entregue.");
        }

        [HttpPut]
        [Route("finalizar-solicitacao/{solicitacaoId:guid}")]
        public async Task<ActionResult<string>> FinalizarSolicitacao(Guid solicitacaoId)
        {
            await _solicitacaoServices.FinalizarSolicitacao(solicitacaoId);

            return RespostaCustomizada("Solicitação finalizada.");
        }

        [HttpPost]
        [Route("adicionar-mensagem-solicitacao")]
        [AllowAnonymous]

        public async Task<ActionResult<string>> AdicionarMensagemSolicitacao(NovaMensagemModel mensagem)
        {
            Mensagem novaMensagem = new Mensagem(mensagem.EmissorId, mensagem.ReceptorId, mensagem.Conteudo);

            await _solicitacaoServices.AdicionarMensagem(mensagem.SolicitacaoId, novaMensagem).ConfigureAwait(false);

            return RespostaCustomizada("Mensagem adicionada com sucesso.");
        }
    }
}
