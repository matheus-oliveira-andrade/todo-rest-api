using FluentValidation.Results;
using MediatR;
using System;

namespace Todo.API.MediatR.Commands
{
    public class Command : IRequest<bool>
    {
        public DateTime ExecutedAt { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid() => throw new NotImplementedException();
    }
}
