using Todo.API.MediatR.Commands;
using Todo.API.ViewModels;

namespace Todo.API.Mapping
{
    public static class TodoMapping
    {

        public static Domain.Todo ToDomain(this AddTodoCommand command)
        {
            return new Domain.Todo(command.Title,
                                   command.Desciption,
                                   command.Status,
                                   command.Tags);
        }

        public static TodoViewModel ToViewModel(this Domain.Todo todo)
        {
            return new TodoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status,
                Tags = todo.Tags,
                CreatedAt = todo.CreatedAt,
                ModifiedAt = todo.ModifiedAt
            };
        }
    }
}
