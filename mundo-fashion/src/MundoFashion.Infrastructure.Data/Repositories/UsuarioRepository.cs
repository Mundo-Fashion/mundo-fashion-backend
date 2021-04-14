using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
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
            return await _context.Usuarios
                .Include(u => u.Servico).SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<Usuario> ObterUsuarioPorIdComEmpresaes(Guid id)
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

        public void AdicionarEmpresa(Empresa Empresa)
        {
            _context.Empresas.Add(Empresa);
        }

        public void RemoverEmpresa(Empresa Empresa)
        {
            _context.Empresas.Remove(Empresa);
        }

        public void AtualizarEmpresa(Empresa Empresa)
        {
            _context.Empresas.Update(Empresa);
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid id)
        {
            return await _context.Empresas
                .Include(e => e.Servico).SingleOrDefaultAsync(e => e.Id == id);
        }

        public void AdicionarServico(ServicoEstampa servico)
        {
            _context.Servicos.Add(servico);
        }

        public void AtualizarServico(ServicoEstampa servico)
        {
            _context.Servicos.Update(servico);
        }

        public async Task<ServicoEstampa> ObterServico(Guid id)
        {
            return await _context.Servicos.SingleOrDefaultAsync(s => s.Id == id);
        }

        public void RemoverServico(ServicoEstampa servico)
        {
            _context.Servicos.Remove(servico);
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
