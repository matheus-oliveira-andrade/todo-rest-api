using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Todo.Application.CommandHandlers;
using Todo.Application.Commands;
using Todo.Domain.Enums;
using Todo.Infrastructure.Interfaces;
using Xunit;

namespace Todo.Application.Tests.CommandHandlers
{
    public class MarkTodoAsDoneCommandHandlerTests
    {
        private readonly MarkTodoAsDoneCommandHandler _handler;
        private readonly Mock<ITodoRepository> _todoProviderMock;

        public MarkTodoAsDoneCommandHandlerTests()
        {
            _todoProviderMock = new Mock<ITodoRepository>();
            var loggerMock = new Mock<ILogger<MarkTodoAsDoneCommandHandler>>();

            _handler = new MarkTodoAsDoneCommandHandler(loggerMock.Object, _todoProviderMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenValidCommandIsPassed()
        {
            // Arrange
            var command = new MarkTodoAsDoneCommand(Guid.NewGuid());

            _todoProviderMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Domain.Entities.Todo("Title",
                    "Description",
                    TodoStatus.Pending,
                    new List<string>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenTodoAlreadyMarkAsDone()
        {
            // Arrange
            var command = new MarkTodoAsDoneCommand(Guid.NewGuid());

            _todoProviderMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Domain.Entities.Todo("Title",
                    "Description",
                    TodoStatus.Done,
                    new List<string>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenTodoIdIsNotFound()
        {
            // Arrange
            var command = new MarkTodoAsDoneCommand(Guid.NewGuid());

            _todoProviderMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(Domain.Entities.Todo));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
}