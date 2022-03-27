using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Commands;
using Todo.Application.Mapping;
using Todo.Application.Services;

namespace Todo.Application.CommandHandlers
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, bool>
    {
        private readonly ITodoProvider _todoProvider;

        public AddTodoCommandHandler(ITodoProvider todoProvider)
        {
            _todoProvider = todoProvider;
        }

        public async Task<bool> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return false;
            }

            await _todoProvider.Add(request.ToDomain());

            return true;
        }
    }
}
