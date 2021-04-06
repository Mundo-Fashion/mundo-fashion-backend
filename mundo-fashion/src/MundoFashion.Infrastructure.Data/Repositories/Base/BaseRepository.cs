using Microsoft.EntityFrameworkCore;
using MundoFashion.Core;
using MundoFashion.Core.Data.Repository;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Infrastructure.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly MundoFashionContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(MundoFashionContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(new TEntity { Id = id });
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
