using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Todo.Application.CommandHandlers;
using Todo.Application.Commands;
using Todo.Infrastructure.Interfaces;
using Xunit;

namespace Todo.Application.Tests.CommandHandlers
{
    public class AddTodoCommandHandlerTests
    {
        private readonly AddTodoCommandHandler _handler;

        public AddTodoCommandHandlerTests()
        {
            var todoProviderMock = new Mock<ITodoRepository>();
            var loggerMock = new Mock<ILogger<AddTodoCommandHandler>>();

            _handler = new AddTodoCommandHandler(loggerMock.Object, todoProviderMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenValidCommandIsPassed()
        {
            // Arrange
            var command = new AddTodoCommand("Xxxx", "Xxxxx xx xxxx", new List<string>());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenInValidCommandIsPassed()
        {
            // Arrange
            var command = new AddTodoCommand("", "Xxxxx xx xxxx", new List<string>());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
}