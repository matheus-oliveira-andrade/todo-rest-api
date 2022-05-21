using System.Collections.Generic;
using System.Linq;
using Todo.Application.Commands;
using Todo.Application.ViewModels;

namespace Todo.Application.Mappings
{
    public static class TodoMapper
    {
        public static Domain.Todo ToDomain(this AddTodoCommand command)
        {
            if (command == null)
                return null;

            return new Domain.Todo(command.Title,
                command.Description,
                command.Status,
                command.Tags);
        }

        public static TodoViewModel ToViewModel(this Domain.Todo todo)
        {
            if (todo == null)
                return null;

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

        public static Todo.Data.Models.Todo ToPersistence(this Domain.Todo todo)
        {
            if (todo == null)
                return null;

            return new Todo.Data.Models.Todo
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

        public static Domain.Todo FromPersistence(this Todo.Data.Models.Todo todo)
        {
            if (todo == null)
                return null;

            return new Domain.Todo(
                todo.Title,
                todo.Description,
                todo.Status,
                todo.Tags,
                todo.Id,
                todo.CreatedAt,
                todo.ModifiedAt);
        }
        
        public static List<Domain.Todo> FromPersistence(this List<Todo.Data.Models.Todo> todo)
        {
           return todo?.Select(x => x.FromPersistence()).ToList();
        }
    }
}