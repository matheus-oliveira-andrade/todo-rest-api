using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.Queries;
using Todo.Application.QueryHandlers;
using Todo.Application.Tests.Mappings.Fixtures;
using Todo.Infrastructure.Interfaces;
using Xunit;

namespace Todo.Application.Tests.QueryHandlersTests
{
    public class GetAllTodosQueryHandlerTests
    {
        private readonly GetAllTodosQueryHandler _queryHandler;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        public GetAllTodosQueryHandlerTests()
        {
            var autoMock = new AutoMocker();

            _todoRepositoryMock = autoMock.GetMock<ITodoRepository>();
            _queryHandler = autoMock.CreateInstance<GetAllTodosQueryHandler>();
        }

        [Fact]
        public async Task Handle_Success_CanFindData()
        {
            // Arrange
            _todoRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new TodoMapperFixture().GenerateRandomTodos(100));
            var query = new GetAllTodosQuery();

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task Handle_Null_CantFindData()
        {
            // Arrange
            _todoRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(default(List<Domain.Entities.Todo>));
            var query = new GetAllTodosQuery();

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}