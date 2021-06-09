using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.WebApi.Controllers.Base;

namespace MundoFashion.WebApi.Controllers
{
    public class AlexaController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IUsuarioRepository _usuarioRepository;
        public AlexaController(INotificationHandler<Notificacao> notificacoes, IMemoryCache cache, IUsuarioRepository usuarioRepository) : base(notificacoes)
        {
            _cache = cache;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("registrar-alexa")]
        public async Task<ActionResult<string>> RegistrarAlexa(string alexaUserId, string passcode)
        {
            if (string.IsNullOrWhiteSpace(alexaUserId) || string.IsNullOrWhiteSpace(passcode))
                return BadRequest("As informações enviadas não podem ser vazia, verifique o código e tente novamente.");

            Guid usuarioId = _cache.Get<Guid>(passcode);  

            if (usuarioId == Guid.Empty)
                return BadRequest("Código informado inválido ou expirado, tente novamente ou solicite um novo código.");  

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId).ConfigureAwait(false);   

            usuario.AtivarSuporteAlexa();
            usuario.AssociarAlexaUserId(alexaUserId);

            _cache.Remove(passcode);

            _usuarioRepository.AtualizarUsuario(usuario);  
            await _usuarioRepository.Commit().ConfigureAwait(false);

            return RespostaCustomizada("Alexa cadastrada com sucesso.");
        }

        [HttpGet]
        [Route("verificar-usuario-registrado")]
        public async Task<ActionResult<bool>> VerificarUsuarioRegistrado(string alexaUserId)
        {
            if (string.IsNullOrWhiteSpace(alexaUserId))
                return BadRequest("As informações enviadas não podem ser vazia, entre em contato com a administração do time notes.");

            return RespostaCustomizada(await _usuarioRepository.ObterUsuarioPorAlexaUserId(alexaUserId).ConfigureAwait(false) != null);
        }

        [HttpGet]
        [Route("obter-passcode-cadastrar-alexa")]
        public async Task<ActionResult<string>> ObterPasscodeCadastrarAlexa()
        {
            string passcode = GetPasscode();

            _cache.Set<Guid>(passcode, Guid.NewGuid(), TimeSpan.FromMinutes(1));

            return await Task.FromResult(passcode);
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