using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Todo.Application.Mappings;
using Todo.Application.Queries;
using Todo.Application.QueryHandlers;
using Todo.Application.Tests.Mappings.Fixtures;
using Todo.Data;
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
            _todoRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync((new TodoMapperFixture().GenerateRandomTodos(100)));
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
            _todoRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(default(List<Todo.Data.Models.Todo>));
            var query = new GetAllTodosQuery();

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}