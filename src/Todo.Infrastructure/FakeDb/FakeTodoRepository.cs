using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Enums;
using Todo.Infrastructure.Interfaces;

namespace Todo.Infrastructure.FakeDb;

public class FakeTodoRepository : ITodoRepository
{
    private List<Domain.Entities.Todo> _todos;

    public FakeTodoRepository()
    {
        _todos = new List<Domain.Entities.Todo>
        {
            new("Todo test 1",
                "This is only a fake todo",
                TodoStatus.Pending,
                new List<string>()),
            new("Todo test 2",
                "This is only a fake todo",
                TodoStatus.Pending,
                new List<string>()),
            new("Todo test 3",
                "This is only a fake todo",
                TodoStatus.Pending,
                new List<string>())
        };
    }

    public Task AddAsync(Domain.Entities.Todo todo)
    {
        _todos.Add(todo);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Domain.Entities.Todo todo)
    {
        var updatedTodo = todo;

        var oldTodo = _todos.First(x => x.Id == todo.Id);

        oldTodo = updatedTodo;

        return Task.CompletedTask;
    }

    public Task<Domain.Entities.Todo> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_todos.FirstOrDefault(x => x.Id == id));
    }

    public Task<List<Domain.Entities.Todo>> GetAllAsync()
    {
        return Task.FromResult(_todos);
    }
}