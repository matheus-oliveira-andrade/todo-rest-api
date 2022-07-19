using System.Collections.Generic;
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
    public class GetAllTodosQueryHandlerTests
    {
        private readonly GetAllTodosQueryHandler _handler;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        public GetAllTodosQueryHandlerTests()
        {
            _todoRepositoryMock = new Mock<ITodoRepository>();
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            var loggerMock = new Mock<ILogger<GetAllTodosQueryHandler>>();

            _handler = new GetAllTodosQueryHandler(
                loggerMock.Object, _todoRepositoryMock.Object, mappingConfig.CreateMapper());
        }

        [Fact]
        public async Task Handle_ShouldReturnTodos_WhenHasTodosInDb()
        {
            // Arrange
            _todoRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Domain.Entities.Todo> { new() });
            var query = new GetAllTodosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmpty_WhenNotFoundTodos()
        {
            // Arrange
            _todoRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Domain.Entities.Todo>());
            var query = new GetAllTodosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }
    }
}