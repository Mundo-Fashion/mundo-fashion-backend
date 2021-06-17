using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MundoFashion.Core.Notifications;
using MundoFashion.Core.Utils;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.WebApi.Controllers.Base;

namespace MundoFashion.WebApi.Controllers
{
    public class AlexaController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        public AlexaController(INotificationHandler<Notificacao> notificacoes, IMemoryCache cache, IUsuarioRepository usuarioRepository, ISolicitacaoRepository solicitacaoRepository) : base(notificacoes)
        {
            _cache = cache;
            _usuarioRepository = usuarioRepository;
            _solicitacaoRepository = solicitacaoRepository;
        }

        [HttpPost]
        [Route("registrar-alexa")]
        public async Task<ActionResult<string>> RegistrarAlexa(string alexaUserId, string passcode)
        {
            if (string.IsNullOrWhiteSpace(alexaUserId) || string.IsNullOrWhiteSpace(passcode))
                return BadRequest("As informações enviadas não podem ser vazia, verifique o código e tente novamente.");

            Guid usuarioId = _cache.Get<Guid>(passcode);

            if (usuarioId == Guid.Empty)
                return BadRequest("Código informado inválido ou expirado, tente novamente ou solicit novoe um código.");

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId).ConfigureAwait(false);

            if (!usuario.UtilizaSuporteAlexa)
            {
                usuario.AtivarSuporteAlexa();
                usuario.AssociarAlexaUserId(alexaUserId);

                _usuarioRepository.AtualizarUsuario(usuario);
                await _usuarioRepository.Commit().ConfigureAwait(false);

                _cache.Remove(passcode);
                return RespostaCustomizada("Alexa cadastrada com sucesso.");
            }

            _cache.Remove(passcode);
            return BadRequest("Alexa já cadastrada.");

        }

        [HttpGet]
        [Route("verificar-usuario-registrado")]
        public async Task<ActionResult<bool>> VerificarUsuarioRegistrado(string alexaUserId)
        {
            if (string.IsNullOrWhiteSpace(alexaUserId))
                return BadRequest("As informações enviadas não podem ser vazia, entre em contato com a administração do mundo fashion.");

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorAlexaUserId(alexaUserId).ConfigureAwait(false);

            return RespostaCustomizada(usuario != null && usuario.UtilizaSuporteAlexa);
        }

        [HttpGet, Authorize]
        [Route("obter-passcode-cadastrar-alexa")]
        public async Task<ActionResult<string>> ObterPasscodeCadastrarAlexa()
        {
            string passcode = GetPasscode();

            _cache.Set<Guid>(passcode, UsuarioId, TimeSpan.FromMinutes(1));

            return await Task.FromResult(passcode);
        }

        [HttpGet]
        [Route("listar-solicitacoes-usuario-tomador")]
        public async Task<ActionResult<string>> ListarSolicitacoesUsuarioTomador(string alexaUserId)
        {
            if (string.IsNullOrWhiteSpace(alexaUserId))
                BadRequest("As informações enviadas não podem ser vazia, entre em contato com a administração do mundo fashion.");

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorAlexaUserId(alexaUserId).ConfigureAwait(false);
            long[] codigos = (await _solicitacaoRepository.ObterSolicitacoes(s => s.TomadorId == usuario.Id).ConfigureAwait(false)).Select(s => s.Codigo).ToArray();

            return RespostaCustomizada($"Você possui as seguintes solicitações... {string.Join(",", codigos)}");
        }

        [HttpGet]
        [Route("obter-detalhes-solicitacoes")]
        public async Task<ActionResult<string>> ObterDetalhesSolicitacao(string alexaUserId, long codigo)
        {
            if (string.IsNullOrWhiteSpace(alexaUserId) || codigo <= 0)
                BadRequest("As informações enviadas não podem ser vazia, entre em contato com a administração do mundo fashion.");

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorAlexaUserId(alexaUserId).ConfigureAwait(false);
            Solicitacao solicitacao = (await _solicitacaoRepository.ObterSolicitacoes(s => s.TomadorId == usuario.Id && s.Codigo == codigo).ConfigureAwait(false)).SingleOrDefault();

            return RespostaCustomizada($"A solicitação esta com status {EnumUtils.ObterValorEmTexto(solicitacao.Status)}");
        }

        private string GetPasscode()
        {
            string passcode = GeneratePasscode();

            while (_cache.Get<Guid>(passcode) != Guid.Empty)
                passcode = GeneratePasscode();

            return passcode;

            static string GeneratePasscode()
            {
                return new Random().Next(100000, 999999).ToString();
            }
        }
    }
}