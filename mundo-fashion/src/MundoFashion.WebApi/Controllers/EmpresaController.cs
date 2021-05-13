using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Storage;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{
    public class EmpresaController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICloudStorage _cloudStorage;
        private readonly EmpresaServices _empresaServices;

        public EmpresaController(INotificationHandler<Notificacao> notificacoes, IMapper mapper, IEmpresaRepository empresaRepository, EmpresaServices empresaServices, ICloudStorage cloudStorage) : base(notificacoes)
        {
            _mapper = mapper;
            _empresaServices = empresaServices;
            _cloudStorage = cloudStorage;
        }

        [HttpPost]
        [Route("criar-solicitacao-empresa")]
        public async Task<ActionResult<string>> CriarSolicitacaoEmpresa([FromForm] SolicitacaoModel solicitacao)
        {
            Solicitacao novaSolicitacao = _mapper.Map<Solicitacao>(solicitacao);

            foreach (IFormFile imagem in solicitacao.Detalhes.ImagensUpload)
            {
                string nomeImagem = $"{Guid.NewGuid()}_{imagem.FileName}";
                novaSolicitacao.Detalhes.AdicionarImagem(await _cloudStorage.UploadFileAsync(imagem, nomeImagem).ConfigureAwait(false));
            }

            await _empresaServices.AdicionarSolicitacaoEmpresa(solicitacao.EmpresaId.GetValueOrDefault(), novaSolicitacao)
                                   .ConfigureAwait(false);

            return RespostaCustomizada("Solicitação criada com sucesso.");
        }

        [HttpPost]
        [Route("criar-servico-empresa/{empresaId:guid}")]
        public async Task<ActionResult<string>> CriarServicoEmpresa(Guid empresaId, ServicoEstampaModel servico)
        {
            ServicoEstampa novoServico = _mapper.Map<ServicoEstampa>(servico);

            await _empresaServices.CriarServicoEmpresa(empresaId, novoServico).ConfigureAwait(false);

            return RespostaCustomizada("Serviço criado com sucesso.");
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
