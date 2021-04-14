using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Infrastructure.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly MundoFashionContext _context;

        public EmpresaRepository(MundoFashionContext context)
        {
            _context = context;
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
