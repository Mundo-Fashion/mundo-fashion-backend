using MundoFashion.Core;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Interfaces;
using MundoFashion.Domain.Servicos;
using System;
using System.Collections.Generic;

namespace MundoFashion.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        private readonly List<Empresa> _empresas;
        private readonly List<Solicitacao> _solicitacoes;

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string Cpf { get; private set; }
        public IReadOnlyCollection<Empresa> Empresas => _empresas.AsReadOnly();
        public ServicoEstampa Servico { get; private set; }
        public Guid ServicoId { get; private set; }
        public IReadOnlyCollection<Solicitacao> Solicitacoes => _solicitacoes.AsReadOnly();
        public Usuario(string nome, string email, string password, string role)
        {
            Nome = nome;
            Email = email;
            Password = password;
            Role = role;
            _empresas = new List<Empresa>();
            _solicitacoes = new List<Solicitacao>();
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            solicitacao.AssociarUsuario(Id);
            _solicitacoes.Add(solicitacao);
        }

        public void SetarCpf(string cpf)
        {
            AlterarRole(Roles.CLIENTE_PRESTADOR);
            Cpf = cpf;
        }

        public void AdicionarEmpresa(Empresa empresa)
        {
            empresa.AssociarUsuario(Id);
            AlterarRole(Roles.CLIENTE_PRESTADOR);
            _empresas.Add(empresa);
        }

        public void AdicionarServico(ServicoEstampa servico)
        {
            AlterarRole(Roles.CLIENTE_PRESTADOR);
            ServicoId = servico.Id;
            servico.AssociarUsuario(Id);
        }

        public void AtualizarServico(ServicoEstampa servico)
        {
            Servico.AtualizarTipoEstampa(servico.TipoEstampa);
            Servico.AtualizarTipoTecnicaEstampa(servico.Tecnica);
            Servico.AtualizarTipoTecnicaEstamparia(servico.TecnicaEstamparia);
            Servico.AtualizarTipoNicho(servico.Nicho);
            Servico.AtualizarTipoRapport(servico.TipoRapport);
        }

        public void AlterarRole(string novaRole)
        {
            if (!Role.Equals(novaRole))
                Role = novaRole;
        }

        public void InativarServico()
        {
            if (PossuiServico())
                Servico.Inativate();
        }

        public bool PossuiServico()
            => !ServicoId.Equals(Guid.Empty);

        public void AtualizarSolicitacao(Solicitacao solicitacao)
        {
            throw new NotImplementedException();
        }
    }
}
