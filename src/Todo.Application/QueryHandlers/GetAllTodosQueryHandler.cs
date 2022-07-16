using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Mappings;
using Todo.Application.Queries;
using Todo.Application.ViewModels;
using Todo.Infrastructure.Interfaces;

namespace Todo.Application.QueryHandlers
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
