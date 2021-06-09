using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MundoFashion.Application.Services;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Storage;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;
using MundoFashion.WebApi.Models.Usuario;
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
        public UsuarioController(INotificationHandler<Notificacao> notificacoes, UsuarioServices usuarioServices, IMapper mapper, IUsuarioRepository usuarioRepository, ICloudStorage cloudStorage, IMemoryCache cache) : base(notificacoes)
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
            await _usuarioServices.CriarUsuario(usuario.Nome, usuario.Cpf, usuario.Email, usuario.Senha)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Usuário criado");
        }

        [HttpPost]
        [Route("criar-servico-usuario")]
        public async Task<ActionResult<string>> CriarServicoUsuario([FromForm] ServicoEstampaModel servico)
        {
            ServicoEstampa novoServico = new ServicoEstampa(servico.TipoEstampa,
                                                            servico.TipoTecnica,
                                                            servico.TipoTecnicaEstamparia,
                                                            servico.TipoNicho,
                                                            servico.TipoRapport);

            foreach (IFormFile imagem in servico.ImagensUpload)
            {
                string nomeImagem = $"{Guid.NewGuid()}_{imagem.FileName}";
                novoServico.AdicionarImagem(await _cloudStorage.UploadFileAsync(imagem, nomeImagem).ConfigureAwait(false));
            }

            await _usuarioServices.CriarServicoUsuario(UsuarioId, novoServico)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço criado.");
        }

        [HttpGet]
        [Route("obter-usuario/{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioModel>> ObterUsuario(Guid id)
        {
            return _mapper.Map<UsuarioModel>(await _usuarioRepository.ObterUsuarioCompletoPorId(id).ConfigureAwait(false));
        }

        [HttpPut]
        [Route("atualizar-servico-usuario")]
        [Authorize(Roles = Roles.CLIENTE_PRESTADOR)]
        public async Task<ActionResult<string>> AtualizarServicoUsuario([FromBody] ServicoEstampaModel servico)
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
    }
}
