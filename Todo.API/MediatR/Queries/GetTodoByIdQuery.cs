using MediatR;
using System;
using Todo.API.ViewModels;

namespace Todo.API.MediatR.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoViewModel>
    {
        public Guid Id { get; private set; }

        public GetTodoByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
