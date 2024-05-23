using BooksManager.Application.Commands.Books;
using BooksManager.Application.Queries.Books;
using BooksManager.Application.ViewModels;
using BooksManager.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BooksManager.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="command">Book data</param>
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(BookViewModel))]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        /// <summary>
        /// Returns all books
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<BookViewModel>))]
        [Authorize(Roles = "Administrator, Customer")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBooksQuery();

            var books = await mediator.Send(query);

            return Ok(books);
        }

        /// <summary>
        /// Returns book according to Id
        /// </summary>
        /// <param name="id">Book Id</param>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BookViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResult))]
        [Authorize(Roles = "Administrator, Customer")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetBookByIdQuery(id);

            var book = await mediator.Send(query);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}
