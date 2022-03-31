using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.Queries;
using Todo.Application.QueryHandlers;
using Todo.Application.Tests.Mappings.Fixtures;
using Todo.Data;
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
            _todoRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new TodoMapperFixture().GenerateRandomTodos(1).First());
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
            _todoRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(default(Domain.Todo));
            var query = new GetTodoByIdQuery(Guid.NewGuid());

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}