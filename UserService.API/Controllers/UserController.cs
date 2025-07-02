using MediatR;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Features.Users.Queries;
using UserService.Application.ReadModels.User;
using UserService.Application.Responses;

namespace UserService.API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : DefaultController
    {
        public UserController(IMediator mediator)
            : base(mediator)
        {
            //
        }

      

        [HttpGet]
        public async Task<GeneralResponse<IEnumerable<UserReadModel>>> GetUsers()
        {
            return await Mediator.Send(new UsersListQuery()).ConfigureAwait(false);
        } 

        [HttpPost]
        public async Task<GeneralResponse<int>> UserCreate([FromBody] CreateUserCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }
    }
}
