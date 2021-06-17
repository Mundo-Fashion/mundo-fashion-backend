using Microsoft.EntityFrameworkCore;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoFashion.Infrastructure.Data.Repositories
{
    public class SolicitacaoRepository : ISolicitacaoRepository
    {
        private readonly MundoFashionContext _context;

        public SolicitacaoRepository(MundoFashionContext context)
        {
            _context = context;
        }

        public void AdicionarDetalhesSolicitacao(DetalhesSolicitacao detalhes)
        {
            _context.DetalhesSolicitacoes.Add(detalhes);
        }

        public void AdicionarProposta(Proposta proposta)
        {
            _context.Propostas.Add(proposta);
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
        }

        public void AtualizarDetalhesSolicitacao(DetalhesSolicitacao detalhes)
        {
            _context.DetalhesSolicitacoes.Update(detalhes);
        }

        public void AtualizarProposta(Proposta proposta)
        {
            _context.Propostas.Update(proposta);
        }

        public void AtualizarSolicitacao(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Update(solicitacao);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<DetalhesSolicitacao> ObterDetalhesSolicitacaoPorId(Guid id)
        {
            return await _context.DetalhesSolicitacoes.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Proposta> ObterPropostaPorId(Guid id)
        {
            return await _context.Propostas.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Solicitacao> ObterSolicitacaoPorId(Guid id)
        {
            return await _context.Solicitacoes
                .Include(s => s.Tomador)
                .Include(s => s.Proposta)
                .Include(s => s.Servico)
                .ThenInclude(s => s.Prestador)
                .Include(s => s.Detalhes)
                .Include(s => s.Mensagens)
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        public void AdicionarMensagem(Mensagem mensagem)
        {
            _context.Mensagens.Add(mensagem);
        }
        public Task<List<Solicitacao>> ObterSolicitacoes(Func<Solicitacao, bool> predicate)
        {
            return Task.FromResult<List<Solicitacao>>(_context.Solicitacoes
                                                               .Include(s => s.Servico)
                                                               .ThenInclude(s => s.Prestador)
                                                               .Include(s => s.Tomador)
                                                               .Include(s => s.Detalhes)
                                                               .Where(predicate).ToList());
        }
        
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
