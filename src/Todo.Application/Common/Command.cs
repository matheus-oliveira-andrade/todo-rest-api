using FluentValidation.Results;
using MediatR;

namespace Todo.Application.Common
{
    public abstract class Command : IRequest<bool>
    {
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool IsValid();
    }
}
