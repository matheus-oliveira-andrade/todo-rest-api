using FluentAssertions;
using Todo.Domain.Enums;
using Xunit;

namespace Todo.Domain.Tests.Entities;

public class TodoTests
{
    [Fact]
    public void MarkAsDone_ShouldSetStatusToDone_WhenTodoStatusIsPending()
    {
        // Arrange
        var todo = new Domain.Entities.Todo
        {
            Title = "My title",
            Description = "This is my long description",
            Status = TodoStatus.Done,
            Tags = new List<string>
            {
                "For tomorrow",
                "Critical"
            }
        };

        // Act
        todo.MarkAsDone();

        // Arrange
        todo.Status.Should().Be(TodoStatus.Done);
    }

    [Fact]
    public void IsDone_ShouldReturnTrue_WhenTodoStatusDone()
    {
        // Arrange
        var todo = new Domain.Entities.Todo
        {
            Title = "My title",
            Description = "This is my long description",
            Status = TodoStatus.Done,
            Tags = new List<string>
            {
                "For tomorrow",
                "Critical"
            }
        };

        // Act
        var result = todo.IsDone();

        // Arrange
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsDone_ShouldReturnFalse_WhenTodoStatusNotIsDone()
    {
        // Arrange
        var todo = new Domain.Entities.Todo
        {
            Title = "My title",
            Description = "This is my long description",
            Status = TodoStatus.Pending,
            Tags = new List<string>
            {
                "For tomorrow",
                "Critical"
            }
        };
     
        // Act
        var result = todo.IsDone();
     
        // Arrange
        result.Should().BeFalse();
    }
}