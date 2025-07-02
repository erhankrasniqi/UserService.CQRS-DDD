using MediatR;
using UserService.Application.ReadModels.User;
using UserService.Application.Responses;

namespace UserService.Application.Features.Users.Queries
{
    public class UsersListQuery : IRequest<GeneralResponse<IEnumerable<UserReadModel>>>
    {
        //
    }
}