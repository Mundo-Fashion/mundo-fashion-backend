using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Dto;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using System.Threading.Tasks;

namespace MundoFashion.Application.Services
{
    public class AutenticacaoServices : BaseServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenServices _tokenServices;
        public AutenticacaoServices(Notificador notificador, IUsuarioRepository usuarioRepository, TokenServices tokenServices) : base(notificador)
        {
            _tokenServices = tokenServices;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<TokenDto> Logar(string email, string senha)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuarioPorUserNameSenha(email, senha);

            if (usuario is null)
            {
                Notificar($"Usuario '{email}' não encontrado.");
                return null;
            }

            string token = _tokenServices.GenerateToken(usuario);

            return new TokenDto(new UsuarioDto(usuario.Id, usuario.Nome, usuario.Email, usuario.Role, usuario.CreatedAt), token);
        }
    }
}
