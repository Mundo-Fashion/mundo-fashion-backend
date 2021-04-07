using MundoFashion.Core;
using System;

namespace MundoFashion.Domain
{
    public class Empresa : Entity
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; set; }

        public Empresa(string nome, string cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
        }

        internal void AssociarUsuario(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
