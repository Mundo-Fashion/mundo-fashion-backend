using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Storage;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;
using MundoFashion.WebApi.Models.Solicitacao;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public SolicitacaoController(INotificationHandler<Notificacao> notificacoes, IMapper mapper, SolicitacaoServices solicitacaoServices, ISolicitacaoRepository solicitacaoRepository, ICloudStorage cloudStorage) : base(notificacoes)
        {
            _mapper = mapper;
            _solicitacaoServices = solicitacaoServices;
            _solicitacaoRepository = solicitacaoRepository;
            _cloudStorage = cloudStorage;
        }

        [HttpGet]
        [Route("obter-solicitacoes-prestador/{prestadorId:guid}")]
        public async Task<ActionResult<List<SolicitacaoModel>>> ObterSolicitacoesPorPrestador(Guid prestadorId)
            => RespostaCustomizada(_mapper.Map<List<SolicitacaoModel>>(await _solicitacaoRepository.ObterSolicitacoes(s => s.Servico.PrestadorId == prestadorId)));

        [HttpGet]
        [Route("obter-solicitacoes-tomador/{tomadorId:guid}")]
        public async Task<ActionResult<List<SolicitacaoModel>>> ObterSolicitacoesPorTomador(Guid tomadorId)
            => RespostaCustomizada(_mapper.Map<List<SolicitacaoModel>>(await _solicitacaoRepository.ObterSolicitacoes(s => s.TomadorId == tomadorId)));

        [HttpPost]
        [Route("criar-solicitacao")]
        public async Task<ActionResult<string>> CriarSolicitacao([FromForm] NovaSolicitacaoModel solicitacao)
        {
            DetalhesSolicitacao detalhesSolicitacao = new DetalhesSolicitacao(solicitacao.Detalhes.TipoEstampa, solicitacao.Detalhes.TipoTecnica,
                                                                              solicitacao.Detalhes.TipoTecnicaEstamparia, solicitacao.Detalhes.TipoNicho,
                                                                              solicitacao.Detalhes.TipoRapport, solicitacao.Detalhes.Observacoes);

            Solicitacao novaSolicitacao = new Solicitacao(solicitacao.TomadorServicoId, solicitacao.ServicoId, detalhesSolicitacao);

            foreach (Microsoft.AspNetCore.Http.IFormFile imagem in solicitacao.Detalhes.ImagensUpload)
            {
                string nomeImagem = $"{Guid.NewGuid()}_{imagem.FileName}";
                novaSolicitacao.Detalhes.AdicionarImagem(await _cloudStorage.UploadFileAsync(imagem, nomeImagem).ConfigureAwait(false));
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
        [Route("aceitar-solicitacao/{solicitacaoId:guid}")]
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

        [HttpPost]
        [Route("adicionar-mensagem-solicitacao")]
        [AllowAnonymous]

        public async Task<ActionResult<string>> AdicionarMensagemSolicitacao(MensagemModel mensagem)
        {
            Mensagem novaMensagem = _mapper.Map<Mensagem>(mensagem);

            await _solicitacaoServices.AdicionarMensagem(mensagem.SolicitacaoId, novaMensagem).ConfigureAwait(false);

            return RespostaCustomizada("Mensagem adicionada com sucesso.");
        }
    }
}
