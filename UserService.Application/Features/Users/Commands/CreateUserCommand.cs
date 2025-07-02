using MediatR; 
using UserService.Application.Responses;

namespace UserService.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<GeneralResponse<int>>
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string TenantId { get; set; }
    } 
}
