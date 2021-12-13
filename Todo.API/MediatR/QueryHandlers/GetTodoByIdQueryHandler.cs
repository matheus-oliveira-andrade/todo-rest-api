using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.API.Data;
using Todo.API.Mapping;
using Todo.API.MediatR.Queries;
using Todo.API.ViewModels;

namespace Todo.API.MediatR.QueryHandlers
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
