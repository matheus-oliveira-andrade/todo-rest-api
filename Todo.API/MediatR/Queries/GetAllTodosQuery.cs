using MediatR;
using System.Collections.Generic;

namespace Todo.API.MediatR.Queries
{
    public class GetAllTodosQuery : IRequest<List<Domain.Todo>>
    {
        public GetAllTodosQuery()
        {
        }
    }
}
