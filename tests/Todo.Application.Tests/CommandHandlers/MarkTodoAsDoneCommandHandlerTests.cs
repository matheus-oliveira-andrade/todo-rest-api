using System;
using System.Threading;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.CommandHandlers;
using Todo.Application.Commands;
using Todo.Application.Services;
using Todo.Application.Tests.CommandHandlers.Fixtures;
using Xunit;

namespace Todo.Application.Tests.CommandHandlers
{
    public class MarkTodoAsDoneCommandHandlerTests : IClassFixture<MarkTodoAsDoneCommandHandlerFixture>
    {
        private readonly MarkTodoAsDoneCommandHandler _commandHandler;
        private readonly Mock<ITodoProvider> _todoProviderMock;
        private readonly MarkTodoAsDoneCommandHandlerFixture _fixture;

        public MarkTodoAsDoneCommandHandlerTests(MarkTodoAsDoneCommandHandlerFixture fixture)
        {
            _fixture = fixture;

            var autoMocker = new AutoMocker();

            _commandHandler = autoMocker.CreateInstance<MarkTodoAsDoneCommandHandler>();
            _todoProviderMock = autoMocker.GetMock<ITodoProvider>();
        }

        [Fact]
        public async void Handle_Processed_ReturnTrue()
        {
            // Arrange
            var command = new MarkTodoAsDoneCommand(Guid.NewGuid());

            _todoProviderMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(_fixture.GetPendingTodo());

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void Handle_AlwaysDone_ReturnTrue()
        {
            // Arrange
            var command = new MarkTodoAsDoneCommand(Guid.NewGuid());

            _todoProviderMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(_fixture.GetDoneTodo());

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void Handle_NotFound_ReturnTrue()
        {
            // Arrange
            var command = new MarkTodoAsDoneCommand(Guid.NewGuid());

            _todoProviderMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(default(Domain.Todo));

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
}