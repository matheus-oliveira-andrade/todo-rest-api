using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.API.Data;
using Todo.API.MediatR.Queries;

namespace Todo.API.MediatR.QueryHandlers
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<Domain.Todo>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<Domain.Todo>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            return await _todoRepository.GetAll();
        }
    }
}
