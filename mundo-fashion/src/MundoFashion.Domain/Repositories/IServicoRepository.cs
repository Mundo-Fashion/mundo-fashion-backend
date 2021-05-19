using MundoFashion.Core.Extensions.Pagination.Models;
using MundoFashion.Domain.Servicos;

namespace MundoFashion.Domain.Repositories
{
    public interface IServicoRepository
    {
        PagedModel<ServicoEstampa> GetByPage(int page, int limit);
    }
}
