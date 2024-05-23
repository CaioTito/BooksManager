using BooksManager.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BooksManager.Application.Queries.Users;
using BooksManager.Application.ViewModels;
using BooksManager.Application.Commands.Lendings;

namespace BooksManager.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Returns user according to Id
        /// </summary>
        /// <param name="id">User Id</param>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResult))]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var getUserByIdQuery = new GetUserByIdQuery(id);

            var user = await mediator.Send(getUserByIdQuery);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="command">user data</param>
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(UserViewModel))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        /// <summary>
        /// Login and returns a Token
        /// </summary>
        /// <param name="login">Email e password</param>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginUserCommand login)
        {
            return Ok(new { token = await mediator.Send(login) });
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">UserId</param>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
