using Microsoft.EntityFrameworkCore;
using MundoFashion.Core.Extensions.Pagination;
using MundoFashion.Core.Extensions.Pagination.Models;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Infrastructure.Data.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly MundoFashionContext _context;

        public ServicoRepository(MundoFashionContext context)
        {
            _context = context;
        }

        public PagedModel<ServicoEstampa> GetByPage(int page, int limit)
            => _context.Servicos.AsNoTracking().Paginate(page, limit);
    }
}
