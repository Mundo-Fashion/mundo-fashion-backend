using MundoFashion.Core;
using MundoFashion.Core.Enums.Servicos.Estampa;
using System;
using System.Collections.Generic;

namespace MundoFashion.Domain
{
    public class DetalhesSolicitacao : Entity
    {
        private readonly List<string> _imagens;

        private DetalhesSolicitacao() { }

        public DetalhesSolicitacao(int tipoEstampa = 0,
                    int tecnica = 0,
                    int tecnicaEstamparia = 0,
                    int nicho = 0,
                    int tipoRapport = 0,
                    string observacoes = "") :
                    this((TipoEstampa)tipoEstampa, (TipoTecnicaEstampa)tecnica, (TipoTecnicaEstamparia)tecnicaEstamparia,
                    (TipoNicho)nicho, (TipoRapport)tipoRapport, observacoes)
        { }

        public DetalhesSolicitacao(TipoEstampa tipoEstampa = 0,
            TipoTecnicaEstampa tecnica = 0,
            TipoTecnicaEstamparia tecnicaEstamparia = 0,
            TipoNicho nicho = 0,
            TipoRapport tipoRapport = 0,
            string observacoes = "")
        {
            TipoEstampa = tipoEstampa;
            Tecnica = tecnica;
            TecnicaEstamparia = tecnicaEstamparia;
            Nicho = nicho;
            TipoRapport = tipoRapport;
            Observacoes = observacoes;

            _imagens = new List<string>();
        }

        public string[] Imagens { get; private set; }
        public TipoEstampa TipoEstampa { get; private set; }
        public TipoTecnicaEstampa Tecnica { get; private set; }
        public TipoTecnicaEstamparia TecnicaEstamparia { get; private set; }
        public TipoNicho Nicho { get; private set; }
        public TipoRapport TipoRapport { get; private set; }
        public string Observacoes { get; private set; }
        public Solicitacao Solicitacao { get; private set; }
        public Guid SolicitacaoId { get; private set; }

        internal void AssociarSolicitacao(Guid solicitacaoId)
           => SolicitacaoId = solicitacaoId;

        public void AtualizarTipoEstampa(TipoEstampa tipoEstampa)
           => TipoEstampa = tipoEstampa;

        public void AtualizarTipoTecnicaEstampa(TipoTecnicaEstampa tecnica)
            => Tecnica = tecnica;

        public void AtualizarTipoTecnicaEstamparia(TipoTecnicaEstamparia tipoTecnicaEstamparia)
            => TecnicaEstamparia = tipoTecnicaEstamparia;

        public void AtualizarTipoNicho(TipoNicho tipoNicho)
            => Nicho = tipoNicho;

        public void AtualizarTipoRapport(TipoRapport tipoRapport)
            => TipoRapport = tipoRapport;

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

        public void AtualizarObservacao(string observacao)
          => Observacoes = observacao;
    }
}
