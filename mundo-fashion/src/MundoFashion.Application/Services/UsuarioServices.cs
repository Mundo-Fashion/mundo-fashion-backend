using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.Domain.Validacoes;
using System;
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

            Usuario novoUsuario = new Usuario(username, senha, role);

            if (!Validar<Usuario, UsuarioValidator>(novoUsuario)) return;

            _usuarioRepository.AdicionarUsuario(novoUsuario);
            await _usuarioRepository.Commit();
        }

        public async Task CriarEmpresa(Guid usuarioId, Empresa empresa)
        {
            if (!Validar<Empresa, EmpresaValidator>(empresa)) return;

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            usuario.AdicionarEmpresa(empresa);

            _usuarioRepository.AdicionarEmpresa(empresa);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit();
        }

        public async Task CriarServicoUsuario(Guid usuarioId, ServicoEstampa servico)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (usuario.PossuiServico())
            {
                Notificar($"O usuário '{usuario.Username}' já possui um serviço cadastrado. Altere-o!");
                return;
            }

            usuario.AdicionarServico(servico);

            _usuarioRepository.AdicionarServico(servico);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit();
        }

        public async Task AtualizarServicoUsuario(Guid usuarioId, ServicoEstampa servicoAtualizado)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (!usuario.PossuiServico())
            {
                Notificar($"O usuário {usuario.Username} não possui serviços cadastrados.");
                return;
            }

            usuario.AtualizarServico(servicoAtualizado);

            _usuarioRepository.AtualizarServico(usuario.Servico);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit();
        }

        public async Task RemoverServicoUsuario(Guid usuarioId)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if(usuario is null)
            {
                Notificar("Usuário não encontrado na base de dados.");
                return;
            }

            usuario.RemoverServico();

            _usuarioRepository.RemoverServico(usuario.Servico);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit();
        }
    }
}
