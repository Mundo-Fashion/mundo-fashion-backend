﻿using MundoFashion.Core;
using MundoFashion.Core.Interfaces;
using System.Collections.Generic;

namespace MundoFashion.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        private readonly List<Empresa> _empresas;

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public IReadOnlyCollection<Empresa> Empresas { get; private set; }

        public Usuario(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
            _empresas = new List<Empresa>();
        }

        public Usuario() { }

        public void AdicionarEmpresa(Empresa empresa)
        {
            empresa.AssociarUsuario(Id);
            _empresas.Add(empresa);
        }

        public void setRole(string newRole)
        {
            Role = newRole;
        }
    }
}
