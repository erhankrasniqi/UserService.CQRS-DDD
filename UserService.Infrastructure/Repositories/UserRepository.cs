
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.UsersAggregates;
using UserService.Infrastructure.Contracts;
using UserService.Infrastructure.Database;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<Users, int>, IUserRepository
    {
        private readonly WitsDbContext _dbContext;

        public UserRepository(WitsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

         

        public async Task<IEnumerable<Users>> GetUsers(CancellationToken cancellationToken)
        {
            return await _dbContext.Users  
            .ToListAsync(cancellationToken);
        }
         
        public async Task<Users?> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.AccountId == accountId, cancellationToken);
        }

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.Username.ToLower() == username.ToLower(), cancellationToken);
        }
        public async Task<bool> UsernameTakenByAnotherAccountAsync(string username, Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.Username == username && u.AccountId != accountId, cancellationToken);
        }

    }
}