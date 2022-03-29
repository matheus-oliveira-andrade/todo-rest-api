using System;
using System.Collections.Generic;
using Todo.API.Domain;

namespace Todo.Application.ViewModels
{
    public class TodoViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public List<string> Tags { get; set; }
        public Guid Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
