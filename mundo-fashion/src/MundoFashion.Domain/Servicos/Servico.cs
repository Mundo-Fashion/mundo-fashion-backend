using MundoFashion.Core;
using System;
using System.Collections.Generic;

namespace MundoFashion.Domain.Servicos
{
    public class Servico : ActivableEntity
    {
        private readonly List<string> _imagens;

        public string[] Imagens { get; private set; }
        public Usuario Prestador { get; set; }
        public Guid PrestadorId { get; private set; }
        public Servico()
        {
            _imagens = new List<string>();
        }
        internal void AssociarUsuarioPrestador(Guid id)
           => PrestadorId = id;

        public void AdicionarImagem(string imagem)
        {
            _imagens.Add(imagem);
            Imagens = _imagens.ToArray();
        }

        public void RemoverImagem(string imagem)
        {
            _imagens.Remove(imagem);
            Imagens = _imagens.ToArray();
        }
    }
}
