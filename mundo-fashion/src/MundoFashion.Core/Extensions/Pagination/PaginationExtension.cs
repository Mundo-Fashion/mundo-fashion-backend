using MundoFashion.Core.Extensions.Pagination.Models;
using System;
using System.Linq;

namespace MundoFashion.Core.Extensions.Pagination
{
    public static class PaginationExtension
    {
        public static PagedModel<TModel> Paginate<TModel>(
         this IQueryable<TModel> query,
         int page,
         int limit)
         where TModel : class
        {

            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            var totalItemsCountTask = query.Count();

            var startRow = (page - 1) * limit;
            paged.Items = query
                       .Skip(startRow)
                       .Take(limit)
                       .ToList();

            paged.TotalItems = totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}
