using MundoFashion.Core;
using System;
using System.Collections.Generic;

namespace MundoFashion.Domain.Servicos
{
    public class Servico : Entity
    {
        private readonly List<string> _imagens;

        public IReadOnlyCollection<string> Imagens { get; private set; }
        public Usuario? Usuario { get; set; }
        public Guid? UsuarioId { get; private set; }
        public Empresa? Empresa { get; set; }
        public Guid? EmpresaId { get; private set; }
        public Servico()
        {
            _imagens = new List<string>();
        }

        internal void AssociarUsuario(Guid id)
           => UsuarioId = id;
        internal void AssociarEmpresa(Guid id)
            => EmpresaId = id;

        public void AdicionarImagem(string imagem)
        {   
            _imagens.Add(imagem);
        }

        public void RemoverImagem(string imagem)
        {
            _imagens.Remove(imagem);
        }
    }
}
