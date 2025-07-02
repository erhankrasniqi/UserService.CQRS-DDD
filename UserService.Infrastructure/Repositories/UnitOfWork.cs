using Microsoft.EntityFrameworkCore;
using SharedKernel; 
using UserService.Infrastructure.Contracts;
using UserService.Infrastructure.Database;

namespace UserService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WitsDbContext _dbContext;

        public UnitOfWork(WitsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken = default)
        {
            SetDefaultProperties(_dbContext);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void SetDefaultProperties(WitsDbContext dbContext)
        {
            var modifiedItems = dbContext.ChangeTracker
                .Entries<IEntity<int>>()
                .Where(entity => entity.State == EntityState.Modified);

            var newItems = dbContext.ChangeTracker
                .Entries<IEntity<int>>()
                .Where(entity => entity.State == EntityState.Added);

            foreach (var item in modifiedItems)
            {
                item.Entity.SetModifiedOn(DateTime.UtcNow);
            }

            foreach (var item in newItems)
            {
                item.Entity.SetCreatedOn(DateTime.UtcNow);
            }
        }
    }
}
