using System;
using System.Collections.Generic;
using Todo.Domain.Enums;

namespace Todo.Domain.Entities
{
    public class Todo : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public List<string> Tags { get; set; }

        public Todo()
        {
        }

        public Todo(string title, string description, TodoStatus status, List<string> tags, Guid? id = null, DateTime? createdAt = null, DateTime? modifiedAt = null) 
            : base(id, modifiedAt, createdAt)
        {
            Title = title;
            Description = description;
            Status = status;
            Tags = tags;
        }

        public void MarkAsDone() => Status = TodoStatus.Done;

        public bool IsDone() => Status == TodoStatus.Done;
    }
}