using MundoFashion.Core;
using System;

namespace MundoFashion.Domain
{
    public class Proposta : Entity
    {
        public double Valor { get; private set; }
        public bool Aceita { get; private set; }
        public Solicitacao Solicitacao { get; private set; }
        public Guid SolicitacaoId { get; private set; }

        internal void AssociarSolicitacao(Guid solicitacaoId)
            => SolicitacaoId = solicitacaoId;

        public void AtualizarValor(double valor)
            => Valor = valor;

        public void AceitarProposta()
            => Aceita = true;

        public void RecusarProposta()
            => Aceita = false;
    }
}
