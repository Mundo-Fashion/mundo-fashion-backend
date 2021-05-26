﻿using MundoFashion.Application.Services.Base;
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

        public async Task CriarUsuario(string nome, string email, string senha, string role = Roles.CLIENTE)
        {
            if (await _usuarioRepository.UsuarioExiste(email))
            {
                Notificar("Usuário já cadastrado");
                return;
            }

            Usuario novoUsuario = new Usuario(nome, email, senha, role);

            if (!Validar<Usuario, UsuarioValidator>(novoUsuario)) return;

            _usuarioRepository.AdicionarUsuario(novoUsuario);
            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        public async Task CriarEmpresa(Guid usuarioId, Empresa empresa)
        {
            if (!Validar<Empresa, EmpresaValidator>(empresa)) return;

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            usuario.AdicionarEmpresa(empresa);

            _usuarioRepository.AdicionarEmpresa(empresa);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        public async Task CriarServicoUsuario(Guid usuarioId, ServicoEstampa servico)
        {
            if (!Validar<ServicoEstampa, ServicoEstampaValidator>(servico)) return;

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (usuario.PossuiServico())
            {
                Notificar($"O usuário '{usuario.Email}' já possui um serviço cadastrado. Altere-o!");
                return;
            }

            usuario.AdicionarServico(servico);

            _usuarioRepository.AdicionarServico(servico);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        public async Task AtualizarServicoUsuario(Guid usuarioId, ServicoEstampa servicoAtualizado)
        {
            if (!Validar<ServicoEstampa, ServicoEstampaValidator>(servicoAtualizado)) return;

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (!usuario.PossuiServico())
            {
                Notificar($"O usuário {usuario.Email} não possui serviços cadastrados.");
                return;
            }

            usuario.AtualizarServico(servicoAtualizado);

            _usuarioRepository.AtualizarServico(usuario.Servico);
            _usuarioRepository.AtualizarUsuario(usuario);
            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        public async Task InativarServicoUsuario(Guid usuarioId)
        {
            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (usuario is null)
            {
                Notificar("Usuário não encontrado na base de dados.");
                return;
            }

            usuario.InativarServico();

            _usuarioRepository.AtualizarUsuario(usuario);
            _usuarioRepository.AtualizarServico(usuario.Servico);
            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        public async Task AdicionarSolicitacaoUsuario(Guid usuarioId, Solicitacao solicitacao)
        {
            if (!Validar<Solicitacao, SolicitacaoValidator>(solicitacao)) return;

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (usuario is null)
            {
                Notificar("Usuário não encontrado na base de dados.");
                return;
            }

            usuario.AdicionarSolicitacao(solicitacao);

            _usuarioRepository.AdicionarSolicitacao(solicitacao);
            _usuarioRepository.AtualizarUsuario(usuario);

            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        public async Task AtualizarUsuario(Guid usuarioId, Usuario usuarioAtualizado)
        {
            if (!Validar<Usuario, UsuarioValidator>(usuarioAtualizado)) return;

            Usuario usuario = await _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (usuario is null)
            {
                Notificar("Usuário não encontrado na base de dados.");
                return;
            }

            usuario.SetarCpf(usuarioAtualizado.Cpf);

            _usuarioRepository.AtualizarUsuario(usuario);

            await _usuarioRepository.Commit().ConfigureAwait(false);
        }

        //public async Task AtualizarSolicitacaoUsuario(Guid usuarioId, Guid solicitacaoId, Solicitacao solicitacaoAtualizada)
        //{
        //    Usuario usuario = await _usuarioRepository.ObterUsuarioPorIdComSolicitacoes(usuarioId).ConfigureAwait(false);

        //    if (usuario is null)
        //    {
        //        Notificar("Usuário não encontrado na base de dados.");
        //        return;
        //    }

        //    Solicitacao solicitacao = usuario.Solicitacoes.SingleOrDefault(u => u.Id == solicitacaoId);



        //    usuario.AtualizarSolicitacao(solicitacao);

        //    _usuarioRepository.AtualizarUsuario(usuario);
        //    _usuarioRepository.AtualizarSolicitacao(solicitacao);

        //    await _usuarioRepository.Commit().ConfigureAwait(false);
        //}
    }
}
