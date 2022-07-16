using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Commands;
using Todo.Application.Services;
using Todo.Domain.Enums;

namespace Todo.Application.CommandHandlers
{
    public class MarkTodoAsDoneCommandHandler : IRequestHandler<MarkTodoAsDoneCommand, bool>
    {
        private readonly ITodoProvider _todoProvider;

        public MarkTodoAsDoneCommandHandler(ITodoProvider todoProvider)
        {
            _todoProvider = todoProvider;
        }

        public async Task<bool> Handle(MarkTodoAsDoneCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return false;
            }

            var todo = await _todoProvider.GetById(request.Id);

            if (todo == null || todo.Status == TodoStatus.Done)
            {
                return false;
            }

            todo.MarkAsDone();

            await _todoProvider.Update(todo);

            return true;
        }
    }
}
