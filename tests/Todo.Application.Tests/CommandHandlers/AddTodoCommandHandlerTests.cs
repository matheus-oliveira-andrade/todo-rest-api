using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.CommandHandlers;
using Todo.Application.Commands;
using Todo.Infrastructure.Interfaces;
using Xunit;

namespace Todo.Application.Tests.CommandHandlers
{
    public class AddTodoCommandHandlerTests
    {
        private readonly AddTodoCommandHandler _commandHandler;
        private readonly Mock<ITodoRepository> _todoProviderMock;

        public AddTodoCommandHandlerTests()
        {
            var autoMocker = new AutoMocker();
            _commandHandler = autoMocker.CreateInstance<AddTodoCommandHandler>();
            _todoProviderMock = autoMocker.GetMock<ITodoRepository>();
        }

        [Fact]
        public async Task Handle_AddedWithSuccess_ReturnTrue()
        {
            // Arrange
            _todoProviderMock.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Todo>()));
            var command = new AddTodoCommand("Xxxx", "Xxxxx xx xxxx", new List<string>());

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_InvalidCommand_ReturnFalse()
        {
            // Arrange
            var command = new AddTodoCommand("", "Xxxxx xx xxxx", new List<string>());

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
}