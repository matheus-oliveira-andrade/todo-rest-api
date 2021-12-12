using Todo.API.MediatR.Commands;

namespace Todo.API.Mapping
{
    public static class AddTodoMapping
    {

        public static Domain.Todo ToDomain(this AddTodoCommand command)
        {
            return new Domain.Todo(command.Title, 
                                   command.Desciption, 
                                   command.Status, 
                                   command.Tags);
        }
    }
}
