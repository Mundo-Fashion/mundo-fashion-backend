using MundoFashion.Core.Data.Repository;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Domain.Repositories
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao solicitacao);
        Task<Solicitacao> ObterSolicitacaoPorId(Guid id);
        void AdicionarDetalhesSolicitacao(DetalhesSolicitacao detalhes);
        void AtualizarDetalhesSolicitacao(DetalhesSolicitacao detalhes);
        Task<DetalhesSolicitacao> ObterDetalhesSolicitacaoPorId(Guid id);
        void AdicionarProposta(Proposta proposta);
        void AtualizarProposta(Proposta proposta);
        Task<Proposta> ObterPropostaPorId(Guid id);
    }
}
