using MediatR; 
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        protected readonly IMediator Mediator;

    public DefaultController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
}