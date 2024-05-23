using BooksManager.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }
    }
}
