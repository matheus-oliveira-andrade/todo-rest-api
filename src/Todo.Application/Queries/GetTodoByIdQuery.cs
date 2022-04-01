using System;
using MediatR;
using Todo.Application.ViewModels;

namespace Todo.Application.Queries
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
