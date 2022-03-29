using Todo.Application.Commands;
using Todo.Application.ViewModels;

namespace Todo.Application.Mapping
{
    public static class TodoMapping
    {

        public static API.Domain.Todo ToDomain(this AddTodoCommand command)
        {
            return new API.Domain.Todo(command.Title,
                command.Description,
                command.Status,
                command.Tags);
        }

        public static TodoViewModel ToViewModel(this API.Domain.Todo todo)
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
