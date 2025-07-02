﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using SharedKernel; 
using System.Linq.Expressions; 
using UserService.Infrastructure.Database;

namespace UserService.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly WitsDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        protected GenericRepository(WitsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet;

            if (includes != null && includes.Any())
            {
                foreach (Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include in includes)
                {
                    queryable = include(queryable);
                }
            }

            IEnumerable<TEntity> result = (predicate == null)
                ?
                await queryable.ToListAsync(cancellationToken).ConfigureAwait(false)
                :
                await queryable.Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<TEntity> GetById(TKey id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet;

            if (includes != null && includes.Any())
            {
                foreach (Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include in includes)
                {
                    queryable = include(queryable);
                }
            }

            return await queryable.SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken).ConfigureAwait(false);
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);

            await Task.CompletedTask;
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
             
        }
    }
}
