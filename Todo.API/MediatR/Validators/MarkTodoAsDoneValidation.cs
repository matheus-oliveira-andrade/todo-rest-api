using FluentValidation;
using Todo.API.MediatR.Commands;

namespace Todo.API.MediatR.Validators
{
    public class MarkTodoAsDoneValidation : AbstractValidator<MarkTodoAsDoneCommand>
    {
        public MarkTodoAsDoneValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
