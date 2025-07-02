using Microsoft.EntityFrameworkCore.Query;
using SharedKernel;
using System.Linq.Expressions;

namespace UserService.Infrastructure.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes = null,
            CancellationToken cancellationToken = default);
        Task<TEntity> GetById(TKey id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes = null, CancellationToken cancellationToken = default);
        Task Add(TEntity entity, CancellationToken cancellationToken = default);
        Task Update(TEntity entity, CancellationToken cancellationToken = default);
        Task Delete(TEntity entity, CancellationToken cancellationToken = default);
    }
}
