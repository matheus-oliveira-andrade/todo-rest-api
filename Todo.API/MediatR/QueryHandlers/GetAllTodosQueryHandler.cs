using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.API.Data;
using Todo.API.Mapping;
using Todo.API.MediatR.Queries;
using Todo.API.ViewModels;

namespace Todo.API.MediatR.QueryHandlers
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoViewModel>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoViewModel>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetAll();

            return todos?.Select(t => t.ToViewModel()).ToList();
        }
    }
}
