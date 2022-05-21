using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Todo.Api.Tests.Controllers.Fixtures;
using Todo.Application.Commands;
using Todo.Application.Mappings;
using Todo.Application.Tests.Mappings.Fixtures;
using Xunit;

namespace Todo.Application.Tests.Mappings
{
    public class TodoMapperTests
    {
        [Fact]
        public void ToDomain_MapWithSuccess_ShouldReturnDomainTodo()
        {
            // Arrange
            var todo = new AddTodoCommand("", "", new List<string>());

            // Act
            var result = todo.ToDomain();

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(todo.Title);
            result.Description.Should().Be(todo.Description);
            result.Tags.Should().BeEquivalentTo(todo.Tags);
        }

        [Fact]
        public void ToDomain_NotMap_ShouldReturnNull()
        {
            // Arrange & Act
            var result = ((AddTodoCommand) null).ToDomain();

            // Assert
            result.Should().BeNull();
        }
        
        [Fact]
        public void ToViewModel_MapWithSuccess_ShouldReturnTodoViewModel()
        {
            // Arrange
            var todos = new TodoMapperFixture().GenerateRandomTodos(123);

            // Act
            var result = todos.Select(x => x.FromPersistence().ToViewModel());

            // Assert
            result.Select(x => x.Should().BeNull());
        }

        [Fact]
        public void ToViewModel_NotMap_ShouldReturnNull()
        {
            // Arrange & Act
            var result = ((Domain.Todo) null).ToViewModel();

            // Assert
            result.Should().BeNull();
        }
    }
}