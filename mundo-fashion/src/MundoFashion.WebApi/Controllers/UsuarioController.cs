using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Application.Services;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.WebApi.Controllers.Base;
using MundoFashion.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ApiControllerBase
    {
        private readonly UsuarioServices _usuarioServices;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioController(INotificationHandler<Notificacao> notificacoes, UsuarioServices usuarioServices, IMapper mapper, IUsuarioRepository usuarioRepository) : base(notificacoes)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("criar-usuario")]
        public async Task<ActionResult<string>> CriarUsuario(UsuarioModel usuario)
        {
            await _usuarioServices.CriarUsuario(usuario.Username, usuario.Password)
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
        public async Task<ActionResult<string>> CriarServicoPrestador(ServicoEstampaModel servico)
        {
            ServicoEstampa novoServico = _mapper.Map<ServicoEstampa>(servico);

            await _usuarioServices.CriarServicoUsuario(UsuarioId, novoServico)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço criado.");
        }

        [HttpGet]
        [Route("obter-usuario/{id:guid}")]
        [Authorize]
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

        [HttpDelete]
        [Route("remover-servico-usuario/{usuarioId:guid}")]
        [Authorize]
        public async Task<ActionResult<string>> RemoverServicoUsuario(Guid usuarioId)
        {
            await _usuarioServices.RemoverServicoUsuario(usuarioId)
                                  .ConfigureAwait(false);

            return RespostaCustomizada("Serviço removido com sucesso.");
        }
    }
}
