using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Todo.Application.Commands;
using Todo.Infrastructure.Interfaces;

namespace Todo.Application.CommandHandlers
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, bool>
    {
        private readonly ILogger<AddTodoCommandHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public AddTodoCommandHandler(
            ILogger<AddTodoCommandHandler> logger,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding new todo {@Request}", request);
            
            if (!request.IsValid())
                return false;

            var todo = new Domain.Entities.Todo(
                request.Title,
                request.Description,
                request.Status,
                request.Tags);
            
            await _todoRepository.AddAsync(todo);

            return true;
        }
    }
}