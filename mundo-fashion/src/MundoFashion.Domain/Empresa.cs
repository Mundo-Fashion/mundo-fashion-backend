using MundoFashion.Core;
using MundoFashion.Core.Interfaces;
using MundoFashion.Domain.Servicos;
using System;

namespace MundoFashion.Domain
{
    public class Empresa : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; set; }

        public Guid ServicoId { get; private set; }
        public ServicoEstampa Servico { get; set; }

        public Empresa(string nome, string cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
        }

        public void AdicionarServico(ServicoEstampa servico)
        {
            servico.AssociarEmpresa(Id);
        }

        internal void AssociarUsuario(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
