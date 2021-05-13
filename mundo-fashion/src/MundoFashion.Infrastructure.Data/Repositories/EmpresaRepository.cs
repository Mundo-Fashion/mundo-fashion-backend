using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain;
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

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
        }

        public void AtualizarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Update(solicitacao);
        }

        public async Task<Solicitacao> ObterSolicitacaoPorId(Guid id)
        {
            return await _context.Solicitacoes.SingleOrDefaultAsync(s => s.Id == id);
        }

        public void AdicionarEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
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
