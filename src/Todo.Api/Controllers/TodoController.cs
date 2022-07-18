using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Commands;
using Todo.Application.Queries;
using Todo.Application.ViewModels;

namespace Todo.Api.Controllers
{
    [ApiController]
    [ApiVersion(version: "1.0")]
    [Route("/api/v{version:apiVersion}/todo")]
    public class TodoController : Controller
    {
        private readonly ISender _mediator;

        public TodoController(ISender mediator)
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

            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult> Add(string title, string description, List<string> tags)
        {
            var success = await _mediator.Send(new AddTodoCommand(title, description, tags));
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPut("done")]
        public async Task<ActionResult> MarkAsDone(Guid id)
        {
            var success = await _mediator.Send(new MarkTodoAsDoneCommand(id));
            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}