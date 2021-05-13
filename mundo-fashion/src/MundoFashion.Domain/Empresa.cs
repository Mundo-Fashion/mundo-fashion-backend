using MundoFashion.Core;
using MundoFashion.Core.Interfaces;
using MundoFashion.Domain.Servicos;
using System;
using System.Collections.Generic;

namespace MundoFashion.Domain
{
    public class Empresa : ActivableEntity, IAggregateRoot
    {
        private readonly List<Solicitacao> _solicitacoes;

        public string Nome { get; private set; }
        public string Cnpj { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; set; }

        public Guid ServicoId { get; private set; }
        public ServicoEstampa Servico { get; set; }

        public IReadOnlyCollection<Solicitacao> Solicitacoes => _solicitacoes.AsReadOnly();
        public Empresa(string nome, string cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
            _solicitacoes = new List<Solicitacao>();
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            solicitacao.AssociarEmpresa(Id);
            _solicitacoes.Add(solicitacao);
        }

        public void AdicionarServico(ServicoEstampa servico)
        {
            servico.AssociarEmpresa(Id);
            ServicoId = servico.Id;
        }

        internal void AssociarUsuario(Guid usuarioId)
        {
            UsuarioId = usuarioId;
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
