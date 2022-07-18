using System.Collections.Generic;
using Todo.Application.Common;
using Todo.Application.Validations;
using Todo.Domain.Enums;

namespace Todo.Application.Commands
{
    public class AddTodoCommand : Command
    {
        public string Title { get; }
        public string Description { get; }
        public List<string> Tags { get; }
        public TodoStatus Status { get; }

        public AddTodoCommand(string title, string description, List<string> tags)
        {
            Title = title;
            Description = description;
            Tags = tags;
            Status = TodoStatus.Pending;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddTodoValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}