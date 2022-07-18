using System;
using Todo.Application.Common;
using Todo.Application.Validations;

namespace Todo.Application.Commands
{
    public class MarkTodoAsDoneCommand : Command
    {
        public Guid Id { get; private set; }

        public MarkTodoAsDoneCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new MarkTodoAsDoneValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}