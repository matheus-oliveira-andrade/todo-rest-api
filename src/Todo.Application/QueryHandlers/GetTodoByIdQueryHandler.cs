using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Mappings;
using Todo.Application.Queries;
using Todo.Application.ViewModels;
using Todo.Infrastructure.Interfaces;

namespace Todo.Application.QueryHandlers
{
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoViewModel>
    {
        private readonly ITodoRepository _todoRepository;        

        public GetTodoByIdQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;            
        }

        public async Task<TodoViewModel> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetById(query.Id);

            return todo?.ToViewModel();
        }
    }
}
