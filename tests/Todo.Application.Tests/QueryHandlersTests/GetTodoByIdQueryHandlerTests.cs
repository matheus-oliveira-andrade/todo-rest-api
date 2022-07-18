using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.Queries;
using Todo.Application.QueryHandlers;
using Todo.Infrastructure.Interfaces;
using Xunit;

namespace Todo.Application.Tests.QueryHandlersTests
{
    public class GetTodoByIdQueryHandlerTests
    {
        private readonly GetTodoByIdQueryHandler _queryHandler;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        public GetTodoByIdQueryHandlerTests()
        {
            var autoMock = new AutoMocker();

            _todoRepositoryMock = autoMock.GetMock<ITodoRepository>();
            _queryHandler = autoMock.CreateInstance<GetTodoByIdQueryHandler>();
        }

        [Fact]
        public async Task Handle_Success_CanFindData()
        {
            // Arrange
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Entities.Todo());
            var query = new GetTodoByIdQuery(Guid.NewGuid());

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Handle_Null_CantFindData()
        {
            // Arrange
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(default(Domain.Entities.Todo));
            var query = new GetTodoByIdQuery(Guid.NewGuid());

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}