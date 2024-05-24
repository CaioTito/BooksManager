using BooksManager.Application.Commands.Books;
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
        /// <param name="command">Lending data</param>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Guid))]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLendingCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Delete a lending
        /// </summary>
        /// <param name="id">LendingId</param>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteLendingCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
