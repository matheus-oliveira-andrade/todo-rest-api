using System;
using System.Collections.Generic;
using Todo.API.Domain;
using Todo.API.MediatR.Validators;

namespace Todo.API.MediatR.Commands
{
    public class AddTodoCommand : Command
    {
        public string Title { get; private set; }
        public string Desciption { get; private set; }
        public List<string> Tags { get; private set; }
        public TodoStatus Status { get; private set; }

        public AddTodoCommand(string title, string desciption, List<string> tags)
        {
            Title = title;
            Desciption = desciption;
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
