using MediatR;
using System;
using Todo.API.MediatR.Validators;

namespace Todo.API.MediatR.Commands
{
    public class MarkTodoAsDoneCommand : Command, IRequest<bool>
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
