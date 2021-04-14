using MundoFashion.Core;
using System;

namespace MundoFashion.Domain.Servicos
{
    public class Servico : Entity
    {
        public Usuario? Usuario { get; set; }
        public Guid? UsuarioId { get; private set; }
        public Empresa? Empresa { get; set; }
        public Guid? EmpresaId { get; private set; }

        internal void AssociarUsuario(Guid id)
           => UsuarioId = id;
        internal void AssociarEmpresa(Guid id)
            => EmpresaId = id;
    }
}
