using System;
using FluentValidation.Results;
using MediatR;

namespace Todo.Application.Commands
{
    public class Command : IRequest<bool>
    {
        public DateTime ExecutedAt { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid() => true;
    }
}
