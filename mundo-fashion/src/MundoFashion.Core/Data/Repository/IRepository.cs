using MundoFashion.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Core.Data.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        Task<bool> Commit();
    }
}
