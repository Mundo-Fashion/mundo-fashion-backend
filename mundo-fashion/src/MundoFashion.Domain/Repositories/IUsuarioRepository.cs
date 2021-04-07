using MundoFashion.Core.Data.Repository;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void AdicionarUsuario(Usuario usuario);
        void RemoverUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(Guid id);
        Task<Usuario> ObterUsuarioPorIdComEmpresas(Guid id);
        Task<bool> UsuarioExiste(string username);
        Task<Usuario> ObterUsuarioPorUserNameSenha(string username, string senha);
        void AdicionarEmpresa(Empresa empresa);
        void RemoverEmpresa(Empresa empresa);
        void AtualizarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresaPorId(Guid id);

    }
}
