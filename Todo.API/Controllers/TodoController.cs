using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.API.MediatR.Commands;
using Todo.API.MediatR.Queries;

namespace Todo.API.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {

        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var todos = await _mediator.Send(new GetAllTodosQuery());

            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string title, string description, List<string> tags)
        {
            var addTodoCommand = new AddTodoCommand(title, description, tags);

            bool success = await _mediator.Send(addTodoCommand);
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}
