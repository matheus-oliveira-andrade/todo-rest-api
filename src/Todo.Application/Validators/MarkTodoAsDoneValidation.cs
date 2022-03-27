using FluentValidation;
using Todo.Application.Commands;

namespace Todo.Application.Validators
{
    public class MarkTodoAsDoneValidation : AbstractValidator<MarkTodoAsDoneCommand>
    {
        public MarkTodoAsDoneValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
