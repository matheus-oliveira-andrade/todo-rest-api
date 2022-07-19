using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Todo.Application.Common;
using Todo.Application.Queries;
using Todo.Application.QueryHandlers;
using Todo.Infrastructure.Interfaces;
using Xunit;

namespace Todo.Application.Tests.QueryHandlers
{
    public class GetTodoByIdQueryHandlerTests
    {
        private readonly GetTodoByIdQueryHandler _handler;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        public GetTodoByIdQueryHandlerTests()
        {
            _todoRepositoryMock = new Mock<ITodoRepository>();
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            var loggerMock = new Mock<ILogger<GetTodoByIdQueryHandler>>();

            _handler = new GetTodoByIdQueryHandler(
                loggerMock.Object, _todoRepositoryMock.Object, mappingConfig.CreateMapper());
        }

        [Fact]
        public async Task Handle_ShouldReturnTodo_WhenTodoIdExist()
        {
            // Arrange
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Entities.Todo());
            var query = new GetTodoByIdQuery(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNotFoundTodoId()
        {
            // Arrange
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(Domain.Entities.Todo));
            var query = new GetTodoByIdQuery(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}