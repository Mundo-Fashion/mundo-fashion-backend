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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ApiControllerBase
    {
        private readonly UsuarioServices _usuarioServices;
        public UsuarioController(INotificationHandler<Notificacao> notificacoes, UsuarioServices usuarioServices) : base(notificacoes)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpPost]
        [Route("criar-usuario")]
        public async Task<ActionResult<string>> CriarUsuario(UsuarioModel usuario)
        {
            await _usuarioServices.CriarUsuario(usuario.Username, usuario.Password);
            return RespostaCustomizada("Usuário criado");
        }

        [HttpPost]
        [Route("criar-empresa")]
        [Authorize(Roles = "cliente")]
        public async Task<ActionResult<string>> CriarEmpresa(EmpresaModel empresa)
        {   
            await _usuarioServices.CriarEmpresa(Guid.Parse(User.FindFirst("sub").Value), new Empresa("AFSolutions", "123123123123132"));
            return RespostaCustomizada($"Empresa '{empresa.Nome}' criada.");
        }
    }
}
