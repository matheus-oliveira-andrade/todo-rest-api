using FluentValidation;
using Todo.Application.Commands;

namespace Todo.Application.Validators
{
    public class AddTodoValidation : AbstractValidator<AddTodoCommand>
    {
        public AddTodoValidation()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(c => c.Title).MinimumLength(3).WithMessage("Title must have minimum 3 characteres");
        }
    }
}