using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.Api.Controllers;
using Todo.Application.Commands;
using Todo.Application.Queries;
using Todo.Application.ViewModels;
using Xunit;

namespace Todo.Api.Tests.Controllers
{
    public class TodoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            
            _controller = new TodoController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnTodos_WhenIsCalled()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TodoViewModel> { new() });

            // Act
            var response = await _controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>().Value.As<List<TodoViewModel>>().Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmpty_WhenNotFindTodos()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TodoViewModel>());

            // Act
            var response = await _controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>().Value.As<List<TodoViewModel>>().Should().BeEmpty();
        }

        [Fact]
        public async Task GetById_ShouldReturnATodo_WhenFindTodoWithId()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TodoViewModel { Id = todoId});

            // Act
            var response = await _controller.GetById(todoId);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>().Value.As<TodoViewModel>().Should().NotBeNull();
            response.Result.As<OkObjectResult>().Value.As<TodoViewModel>().Id.Should().Be(todoId);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotFindTodoId()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(TodoViewModel));

            // Act
            var response = await _controller.GetById(Guid.NewGuid());

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }

        [Fact]
        public async Task Add_ShouldReturnOK_WhenTodoParametersIsValid()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<AddTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var response = await _controller.Add("Xxxx xx xxxx", "X xxxx xxxxx x xxx", new List<string>());

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Add_ShouldReturnBadRequest_WhenTodoParametersIsInvalid()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<AddTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var response = await _controller.Add("", "Xxxx xx xxxxx", new List<string>());

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task MarkAsDone_ShouldReturnOk_WhenIsValidTodoId()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<MarkTodoAsDoneCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var response = await _controller.MarkAsDone(Guid.NewGuid());

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task MarkAsDone_ShouldReturnBadRequest_WhenIsInvalidTodoId()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<MarkTodoAsDoneCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var response = await _controller.MarkAsDone(Guid.Empty);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }
    }
}