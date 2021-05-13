using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Constants;
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
    [Authorize]
    public class UsuarioController : ApiControllerBase
    {
        private readonly UsuarioServices _usuarioServices;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ICloudStorage _cloudStorage;
        public UsuarioController(INotificationHandler<Notificacao> notificacoes, UsuarioServices usuarioServices, IMapper mapper, IUsuarioRepository usuarioRepository, ICloudStorage cloudStorage) : base(notificacoes)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _cloudStorage = cloudStorage;
        }

        [HttpPost]
        [Route("criar-usuario")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CriarUsuario(UsuarioModel usuario)
        {
            await _usuarioServices.CriarUsuario(usuario.Nome, usuario.Email, usuario.Password)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Usuário criado");
        }

        [HttpPost]
        [Route("criar-empresa")]
        [Authorize(Roles = Roles.CLIENTE)]
        public async Task<ActionResult<string>> CriarEmpresa(EmpresaModel empresa)
        {
            await _usuarioServices.CriarEmpresa(UsuarioId, new Empresa(empresa.Nome, empresa.Cnpj))
                                  .ConfigureAwait(false); 

            return RespostaCustomizada($"Empresa '{empresa.Nome}' criada.");
        }

        [HttpPost]
        [Route("criar-servico-usuario")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CriarServicoUsuario(ServicoEstampaModel servico)
        {
            ServicoEstampa novoServico = _mapper.Map<ServicoEstampa>(servico);

            await _usuarioServices.CriarServicoUsuario(UsuarioId, novoServico)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço criado.");
        }

        [HttpGet]
        [Route("obter-usuario/{id:guid}")]
        public async Task<ActionResult<UsuarioModel>> ObterUsuario(Guid id)
        {
            return _mapper.Map<UsuarioModel>(await _usuarioRepository.ObterUsuarioPorId(id).ConfigureAwait(false));
        }

        [HttpPut]
        [Route("atualizar-servico-usuario")]
        [Authorize(Roles = Roles.CLIENTE_PRESTADOR)]
        public async Task<ActionResult<string>> AtualizarServicoUsuario([FromBody]ServicoEstampaModel servico)
        {
            ServicoEstampa servicoAtualizado = _mapper.Map<ServicoEstampa>(servico);

            await _usuarioServices.AtualizarServicoUsuario(UsuarioId, servicoAtualizado)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço atualizado com sucesso.");
        }

        [HttpPut]
        [Route("inativar-servico-usuario/{usuarioId:guid}")]
        public async Task<ActionResult<string>> InativarServicoUsuario(Guid usuarioId)
        {
            await _usuarioServices.InativarServicoUsuario(usuarioId)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço removido com sucesso.");
        }

        [HttpPost]
        [Route("criar-solicitacao-usuario")]
        public async Task<ActionResult<string>> CriarSolicitacaoUsuario(SolicitacaoModel solicitacao)
        {
            Solicitacao novaSolicitacao = _mapper.Map<Solicitacao>(solicitacao);

            foreach (IFormFile imagem in solicitacao.Detalhes.ImagensUpload)
            {
                string nomeImagem = $"{Guid.NewGuid()}_{imagem.FileName}";
                novaSolicitacao.Detalhes.AdicionarImagem(await _cloudStorage.UploadFileAsync(imagem, nomeImagem).ConfigureAwait(false));
            }

            await _usuarioServices.AdicionarSolicitacaoUsuario(UsuarioId, novaSolicitacao)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Solicitação criada com sucesso.");
        }


        //[HttpPut]
        //[Route("atualizar-solicitacao-usuario/{solicitacaoId:guid}")]
        //[Authorize]
        //public async Task<ActionResult<string>> AtualizarSolicitacaoUsuario(Guid solicitacaoId, SolicitacaoModel solicitacao)
        //{
        //    Solicitacao solicitacaoAtualizada = _mapper.Map<Solicitacao>(solicitacao);

        //    await _usuarioServices.AtualizarSolicitacaoUsuario(UsuarioId, solicitacaoAtualizada)
        //                          .ConfigureAwait(false);

        //    return RespostaCustomizada("Solicitação atualizada com sucesso.");
        //}
    }
}
