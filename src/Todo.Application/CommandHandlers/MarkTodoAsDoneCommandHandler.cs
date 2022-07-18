using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Todo.Application.Commands;
using Todo.Infrastructure.Interfaces;

namespace Todo.Application.CommandHandlers
{
    public class MarkTodoAsDoneCommandHandler : IRequestHandler<MarkTodoAsDoneCommand, bool>
    {
        private readonly ILogger<MarkTodoAsDoneCommandHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public MarkTodoAsDoneCommandHandler(
            ILogger<MarkTodoAsDoneCommandHandler> logger,
            ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(MarkTodoAsDoneCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Marking todo as done {@Request}", request);
            
            if (!request.IsValid())
                return false;

            var todo = await _todoRepository.GetByIdAsync(request.Id);

            if (todo == null || todo.IsDone())
            {
                return false;
            }

            todo.MarkAsDone();

            await _todoRepository.UpdateAsync(todo);

            return true;
        }
    }
}