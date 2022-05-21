using System;
using System.Collections.Generic;
using Todo.Domain;

namespace Todo.Data.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set;}
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public List<string> Tags { get; set; }
    }
}