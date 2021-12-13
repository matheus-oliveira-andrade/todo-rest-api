using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.API.MediatR.Commands;
using Todo.API.MediatR.Queries;
using Todo.API.ViewModels;

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
        public async Task<ActionResult<List<TodoViewModel>>> Get()
        {
            var todos = await _mediator.Send(new GetAllTodosQuery());

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoViewModel>> GetById(Guid id)
        {
            var todo = await _mediator.Send(new GetTodoByIdQuery(id));

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult> Add(string title, string description, List<string> tags)
        {
            var addTodoCommand = new AddTodoCommand(title, description, tags);

            bool success = await _mediator.Send(addTodoCommand);
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPost("done")]
        public async Task<ActionResult> MarkAsDone(Guid id)
        {
            bool success = await _mediator.Send(new MarkTodoAsDoneCommand(id));
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}
