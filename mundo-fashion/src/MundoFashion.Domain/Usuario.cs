using MundoFashion.Core;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Interfaces;
using MundoFashion.Domain.Servicos;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MundoFashion.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        private readonly List<Empresa> _empresas;

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string Cpf { get; private set; }
        public IReadOnlyCollection<Empresa> Empresas => _empresas.AsReadOnly();
        public ServicoEstampa Servico { get; private set; }
        public Guid ServicoId { get; private set; }
        public Usuario(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
            _empresas = new List<Empresa>();
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

        public void RemoverServico()
        {
            if (PossuiServico())
                ServicoId = Guid.Empty;
        }

        public bool PossuiServico()
            => !ServicoId.Equals(Guid.Empty);
    }
}
