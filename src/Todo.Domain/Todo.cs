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

        public Todo(string title, string description, TodoStatus status, List<string> tags, Guid? id = null, DateTime? createdAt = null, DateTime? modifiedAt = null)
        {
            Title = title;
            Description = description;
            Status = status;
            Tags = tags;
            Id = id ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTime.Now;
            ModifiedAt = modifiedAt ?? DateTime.Now;
        }
        
        public void MarkAsDone()
        {
            Status = TodoStatus.Done;
        }
    }
}
