using MediatR;
using SharedKernel;
using UserService.Application.Responses; 
using UserService.Infrastructure.Contracts;

namespace UserService.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GeneralResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotifier _notifier;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            INotifier notifier
                                              )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork; 
        }

        public async Task<GeneralResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
             
            var usernameTakenByAnotherUser = await _userRepository.UsernameTakenByAnotherAccountAsync(
                request.Username, request.AccountId, cancellationToken);

            if (usernameTakenByAnotherUser)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = "Username is already taken by another account."
                };
            }
             
            var existingUser = await _userRepository.GetByAccountIdAsync(request.AccountId, cancellationToken);
            if (existingUser is not null)
            {
                _userRepository.Delete(existingUser);
            }
             
            var newUser = Domain.Aggregates.UsersAggregates.Users.Create(
                request.AccountId,
                request.Username,
                request.TenantId
            );

            await _userRepository.Add(newUser, cancellationToken);
            await _unitOfWork.Save(cancellationToken);

            string channel = "new-user-creation";

            await newUser.NotifyEvent(_notifier, channel).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "User created successfully.",
                Result = newUser.Id
            };
        }
    }
}
