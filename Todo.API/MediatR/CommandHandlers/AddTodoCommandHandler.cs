using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.API.Mapping;
using Todo.API.MediatR.Commands;
using Todo.API.Providers;

namespace Todo.API.MediatR.CommandHandlers
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
