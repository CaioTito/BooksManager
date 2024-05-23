using BooksManager.Application.Commands.Lendings;
using BooksManager.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BooksManager.API.Controllers
{
    [ApiController]
    [Route("api/lending")]
    public class LendingController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a new lending
        /// </summary>
        /// <param name="command">Lening data</param>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Guid))]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLendingCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }
    }
}
