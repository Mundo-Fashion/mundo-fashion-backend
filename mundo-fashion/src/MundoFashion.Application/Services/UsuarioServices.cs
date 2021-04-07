using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Data.Repository;
using MundoFashion.Core.Dto;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MundoFashion.Application.Services
{
    public class UsuarioServices : BaseServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository, Notificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task CriarUsuario(string username, string senha, string role = Roles.CLIENTE)
        {
            if (await _usuarioRepository.UsuarioExiste(username))
            {
                Notificar("Usuário já cadastrado");
                return;
            }

            _usuarioRepository.AdicionarUsuario(new Usuario(username, senha, role));
            await _usuarioRepository.Commit();
        }

        public async Task CriarEmpresa(Guid usuarioId, Empresa empresa)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            usuario.AdicionarEmpresa(empresa);
            //verificar se é valido
            
            _usuarioRepository.AdicionarEmpresa(empresa);
            await _usuarioRepository.Commit();
        }
    }
}
