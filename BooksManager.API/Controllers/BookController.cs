using BooksManager.Application.Commands.Books;
using BooksManager.Application.Queries.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBooksQuery();

            var books = await mediator.Send(query);

            return Ok(books);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetBookByIdQuery(id);

            var book = await mediator.Send(query);

            return Ok(book);
        }
    }
}
