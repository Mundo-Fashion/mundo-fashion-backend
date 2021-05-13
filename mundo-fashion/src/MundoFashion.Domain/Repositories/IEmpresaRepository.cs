using MundoFashion.Core.Data.Repository;
using MundoFashion.Domain.Servicos;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Domain.Repositories
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        void AdicionarServico(ServicoEstampa servico);
        void RemoverServico(ServicoEstampa servico);
        void AtualizarServico(ServicoEstampa servico);
        Task<ServicoEstampa> ObterServico(Guid id);
        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao solicitacao);
        Task<Solicitacao> ObterSolicitacaoPorId(Guid id);
        void AdicionarEmpresa(Empresa empresa);
        void AtualizarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresaPorId(Guid id);
    }
}
