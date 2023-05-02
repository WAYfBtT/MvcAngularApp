using DAL.DataContext;
using DAL.Entities.Abstract;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity
        where TKey : struct
    {
        protected readonly UrlShortenerDataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(UrlShortenerDataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity) => _dbSet.Add(entity);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.AnyAsync(predicate);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);
        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id)
                ?? throw new ArgumentException($"Entity with such an id not found.", nameof(id));
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take)
            => await _dbSet.Skip(skip).Take(take).ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
            => await _dbSet.FindAsync(id);

        public void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Update(entity);
        }
    }
}
