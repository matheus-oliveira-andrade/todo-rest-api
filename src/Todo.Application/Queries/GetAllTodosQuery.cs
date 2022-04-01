using System.Collections.Generic;
using MediatR;
using Todo.Application.ViewModels;

namespace Todo.Application.Queries
{
    public class GetAllTodosQuery : IRequest<List<TodoViewModel>>
    {
    }
}
