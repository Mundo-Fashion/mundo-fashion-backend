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

        public void AtualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            return await _context.Usuarios.AsNoTracking()
                .Include(u => u.Servico)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> ObterUsuarioCompletoPorId(Guid id)
        {
            return await _context.Usuarios.AsNoTracking()
                .Include(u => u.Servico)
                .Include(u => u.Solicitacoes)
                .ThenInclude(s => s.Detalhes)
                .SingleOrDefaultAsync(u => u.Id == id);
        }
        

        public async Task<bool> UsuarioExiste(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email.Equals(email));
        }

        public async Task<Usuario> ObterUsuarioPorUserNameSenha(string email, string senha)
        {
            return await _context.Usuarios.Where(u => u.Email.Equals(email) && u.Password.Equals(senha)).SingleOrDefaultAsync();
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
