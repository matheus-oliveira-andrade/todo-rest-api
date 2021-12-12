using System;
using System.Collections.Generic;

namespace Todo.API.Domain
{
    public class Todo : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TodoStatus Status { get; private set; }
        public List<string> Tags { get; private set; }

        public Todo(string title, string description, TodoStatus status, List<string> tags)
        {
            Title = title;
            Description = description;
            Status = status;
            Tags = tags;

            base.Id = Guid.NewGuid();
            base.CreatedAt = DateTime.Now;
            base.ModifiedAt = DateTime.Now;
        }
    }
}
