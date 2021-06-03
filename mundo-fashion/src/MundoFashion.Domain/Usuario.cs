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
        private readonly List<Solicitacao> _solicitacoes;

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string Cpf { get; private set; }
        public ServicoEstampa Servico { get; private set; }
        public Guid ServicoId { get; private set; }
        public IReadOnlyCollection<Solicitacao> Solicitacoes => _solicitacoes.AsReadOnly();

        private Usuario()
        {
            _solicitacoes = new List<Solicitacao>();
        }
        public Usuario(string nome, string cpf, string email, string senha, string role)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Password = senha;
            Role = role;
            _solicitacoes = new List<Solicitacao>();
        }

        public void SetarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || Cpf == cpf) return;

            AlterarRole(Roles.CLIENTE_PRESTADOR);
            Cpf = cpf;
        }

        public void AdicionarServico(ServicoEstampa servico)
        {
            AlterarRole(Roles.CLIENTE_PRESTADOR);
            ServicoId = servico.Id;
            servico.AssociarUsuarioPrestador(Id);
        }

        public void AtualizarServico(ServicoEstampa servico)
        {
            if (Servico == servico || servico is null) return;

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
    }
}
