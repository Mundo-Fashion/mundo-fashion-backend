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
        Task<bool> UsuarioExiste(string username);
        Task<Usuario> ObterUsuarioPorUserNameSenha(string username, string senha);
        void AdicionarServico(ServicoEstampa servico);
        void AtualizarServico(ServicoEstampa servico);
        Task<ServicoEstampa> ObterServico(Guid id);
        Task<Usuario> ObterUsuarioPorAlexaUserId(string alexaUserId);
    }
}
