using System.Collections.Generic;
using Todo.Domain;

namespace Todo.Application.Tests.CommandHandlers.Fixtures
{
    public class MarkTodoAsDoneCommandHandlerFixture
    {
        public Domain.Todo GetPendingTodo()
        {
            return new Domain.Todo("Title",
                "Description",
                TodoStatus.Pending,
                new List<string>());
        }
        
        public Domain.Todo GetDoneTodo()
        {
            return new Domain.Todo("Title",
                "Description",
                TodoStatus.Done,
                new List<string>());
        }
    }
}