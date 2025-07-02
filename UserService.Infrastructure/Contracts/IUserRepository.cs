
using UserService.Domain.Aggregates.UsersAggregates;

namespace UserService.Infrastructure.Contracts
{
    public interface IUserRepository : IRepository<Users, int>
    {
        Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);
        Task<Users?> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken); 
        Task<IEnumerable<Users>> GetUsers(CancellationToken cancellationToken);
        public Task<bool> UsernameTakenByAnotherAccountAsync(string username, Guid accountId, CancellationToken cancellationToken);

    }
}
