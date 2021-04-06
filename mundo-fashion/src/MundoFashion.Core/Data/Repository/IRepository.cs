using System;
using System.Threading.Tasks;

namespace MundoFashion.Core.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void Delete(Guid id);
        void Update(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<bool> Save();
    }
}
