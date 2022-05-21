using System.Collections.Generic;

namespace Todo.Application.ViewModels
{
    public class AddTodoViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}