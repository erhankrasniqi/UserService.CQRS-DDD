using MediatR;
using SharedKernel;
using UserService.Application.ReadModels.User; 
using UserService.Application.Responses; 
using UserService.Infrastructure.Contracts;

namespace UserService.Application.Features.Users.Queries
{
    public class UsersListQueryHandler : IRequestHandler<UsersListQuery, GeneralResponse<IEnumerable<UserReadModel>>>
    {
        private readonly IUserRepository _userRepository;

        public UsersListQueryHandler(IUserRepository customerRepository)
        {
            _userRepository = customerRepository;
        }

        public async Task<GeneralResponse<IEnumerable<UserReadModel>>> Handle(UsersListQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Aggregates.UsersAggregates.Users> users =
              (await _userRepository.GetUsers(cancellationToken: cancellationToken))
              .Where(u => u.Status == EntityStatus.Active)
              .ToList();

            IEnumerable<UserReadModel> readModel = [];

            if (users.Any())
            {
                readModel = users.Select(x => { 
                    return new UserReadModel
                    {
                        Id = x.Id, 
                        AccountId = x.AccountId,
                        Username = x.Username,
                        TenantId = x.TenantId,
                         
                    };
                }).ToList();

            }

            return new GeneralResponse<IEnumerable<UserReadModel>>
            {
                Success = true,
                Message = "User list.",
                Result = readModel
            };
        }

    }
}
