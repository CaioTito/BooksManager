using BooksManager.Application.Commands.Lendings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.API.Controllers
{
    [ApiController]
    [Route("api/lending")]
    public class LendingController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateLendingCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }
    }
}
