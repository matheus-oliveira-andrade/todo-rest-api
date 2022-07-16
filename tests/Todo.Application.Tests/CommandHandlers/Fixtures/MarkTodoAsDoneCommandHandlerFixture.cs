using System.Collections.Generic;
using Todo.Domain.Enums;

namespace Todo.Application.Tests.CommandHandlers.Fixtures
{
    public class MarkTodoAsDoneCommandHandlerFixture
    {
        public Domain.Entities.Todo GetPendingTodo()
        {
            return new Domain.Entities.Todo("Title",
                "Description",
                TodoStatus.Pending,
                new List<string>());
        }
        
        public Domain.Entities.Todo GetDoneTodo()
        {
            return new Domain.Entities.Todo("Title",
                "Description",
                TodoStatus.Done,
                new List<string>());
        }
    }
}