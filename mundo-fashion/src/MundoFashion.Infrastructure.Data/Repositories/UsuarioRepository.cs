using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MundoFashion.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MundoFashionContext _context;
        public UsuarioRepository(MundoFashionContext context)
        {
            _context = context;
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void RemoverUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<Usuario> ObterUsuarioPorIdComEmpresas(Guid id)
        {
            return await _context.Usuarios.Include(u => u.Empresas).SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UsuarioExiste(string username)
        {
            return await _context.Usuarios.AnyAsync(u => u.Username.Equals(username));
        }

        public async Task<Usuario> ObterUsuarioPorUserNameSenha(string username, string senha)
        {
            return await _context.Usuarios.Where(u => u.Username.Equals(username) && u.Password.Equals(senha)).SingleOrDefaultAsync();
        }

        public void AdicionarEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
        }

        public void RemoverEmpresa(Empresa empresa)
        {
            _context.Empresas.Remove(empresa);
        }

        public void AtualizarEmpresa(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid id)
        {
            return await _context.Empresas.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
