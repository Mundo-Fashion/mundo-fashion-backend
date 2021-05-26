using MundoFashion.Core.Data.Repository;
using MundoFashion.Domain.Servicos;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void AdicionarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(Guid id);
        Task<Usuario> ObterUsuarioCompletoPorId(Guid id);

        Task<Usuario> ObterUsuarioPorIdComEmpresaes(Guid id);
        Task<Usuario> ObterUsuarioPorIdComSolicitacoes(Guid id);
        Task<bool> UsuarioExiste(string username);
        Task<Usuario> ObterUsuarioPorUserNameSenha(string username, string senha);
        void AdicionarEmpresa(Empresa empresa);
        void AtualizarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresaPorId(Guid id);
        void AdicionarServico(ServicoEstampa servico);
        void AtualizarServico(ServicoEstampa servico);
        Task<ServicoEstampa> ObterServico(Guid id);
        void AdicionarSolicitacao(Solicitacao solicitacao);
        void AtualizarSolicitacao(Solicitacao solicitacao);
        Task<Solicitacao> ObterSolicitacaoPorId(Guid id);

    }
}
