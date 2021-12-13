using MediatR;
using System.Collections.Generic;
using Todo.API.ViewModels;

namespace Todo.API.MediatR.Queries
{
    public class GetAllTodosQuery : IRequest<List<TodoViewModel>>
    {
        public GetAllTodosQuery()
        {
        }
    }
}
