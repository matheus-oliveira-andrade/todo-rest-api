using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.CommandHandlers;
using Todo.Application.Commands;
using Todo.Application.Services;
using Xunit;

namespace Todo.Application.Tests.CommandHandlers
{
    public class AddTodoCommandHandlerTests
    {
        private readonly AddTodoCommandHandler _commandHandler;
        private readonly Mock<ITodoProvider> _todoProviderMock;

        public AddTodoCommandHandlerTests()
        {
            var autoMocker = new AutoMocker();
            _commandHandler = autoMocker.CreateInstance<AddTodoCommandHandler>();
            _todoProviderMock = autoMocker.GetMock<ITodoProvider>();
        }

        [Fact]
        public async void Handle_AddedWithSuccess_ReturnTrue()
        {
            // Arrange
            _todoProviderMock.Setup(x => x.Add(It.IsAny<Domain.Todo>()));
            var command = new AddTodoCommand("Xxxx", "Xxxxx xx xxxx", new List<string>());

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void Handle_InvalidCommand_ReturnFalse()
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