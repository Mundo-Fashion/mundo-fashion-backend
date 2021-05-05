using MundoFashion.Core;
using MundoFashion.Core.Enums.Solicitacao;
using MundoFashion.Core.Interfaces;
using MundoFashion.Domain.Servicos;
using System;

namespace MundoFashion.Domain
{
    public class Solicitacao : Entity, IAggregateRoot
    {
        public Solicitacao(DetalhesSolicitacao detalhes)
        { 
            Status = StatusSolicitacao.Solicitado;
            detalhes.AssociarSolicitacao(Id);
        }

        public StatusSolicitacao Status { get; private set; }
        public DetalhesSolicitacao Detalhes { get; private set; }
        public bool Aceita { get; private set; }
        public Guid DetalhesId { get; private set; }

        public Proposta Proposta { get; private set; }
        public Guid? PropostaId { get; private set; }

        public Usuario Usuario { get; private set; }
        public Guid? UsuarioId { get; private set; }

        public Empresa Empresa { get; set; }
        public Guid? EmpresaId { get; private set; }

        public ServicoEstampa Servico { get; set; }
        public Guid ServicoId { get; private set; }

        internal void AssociarServico(Guid servicoId)
            => ServicoId = servicoId;

        internal void AssociarEmpresa(Guid empresaId)
            => EmpresaId = empresaId;

        internal void AssociarUsuario(Guid usuarioId)
            => UsuarioId = usuarioId;

        public void AdicionarProposta(Proposta proposta)
        {
            proposta.AssociarSolicitacao(Id);
            Status = StatusSolicitacao.AnalisandoProposta;
        }

        public bool PossuiProposta()
            => !PropostaId.Equals(Guid.Empty);

        public void AtualizarValorProposta(double valorProposta)
            => Proposta?.AtualizarValor(valorProposta);

        public void IniciarNegociacao()
           => Status = StatusSolicitacao.EmNegociacao;

        public void Cancelar()
           => Status = StatusSolicitacao.Cancelada;

        public void Negociado()
           => Status = StatusSolicitacao.Negociado;

        public void Pagar()
           => Status = StatusSolicitacao.Pago;

        public void Entregue()
           => Status = StatusSolicitacao.Entregue;

        public void AceitarSolicitacao()
            => Aceita = true;
    }
}
