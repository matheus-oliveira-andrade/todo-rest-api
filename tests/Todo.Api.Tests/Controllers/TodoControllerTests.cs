using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Todo.Api.Controllers;
using Todo.Api.Tests.Controllers.Fixtures;
using Todo.Application.Commands;
using Todo.Application.Queries;
using Todo.Application.ViewModels;
using Xunit;

namespace Todo.Api.Tests.Controllers
{
    public class TodoControllerTests : IClassFixture<TodoControllerFixture>
    {
        private readonly TodoControllerFixture _fixture;
        private readonly TodoController _controller;
        private readonly Mock<IMediator> _mediatorMock;

        public TodoControllerTests(TodoControllerFixture fixture)
        {
            _fixture = fixture;

            var autoMocker = new AutoMocker();

            _mediatorMock = autoMocker.GetMock<IMediator>();
            _controller = autoMocker.CreateInstance<TodoController>();
        }

        [Fact]
        public async void Get_HaveTodos_ReturnsOkResultWithTodos()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.GetTodos());

            // Act
            var response = await _controller.Get();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>().Value.As<List<TodoViewModel>>().Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void Get_DontHaveTodos_ReturnsOKResultWithEmptyList()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TodoViewModel>());

            // Act
            var response = await _controller.Get();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>().Value.As<List<TodoViewModel>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetById_IdExist_ReturnsOKResultWithTodo()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.GetTodo());

            // Act
            var response = await _controller.GetById(Guid.NewGuid());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>().Value.As<TodoViewModel>().Should().NotBeNull();
        }

        [Fact]
        public async void GetById_IdDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(TodoViewModel));

            // Act
            var response = await _controller.GetById(Guid.NewGuid());

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }

        [Fact]
        public async void Add_ValidTodo_ReturnsOk()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var response = await _controller.Add("Xxxx xx xxxx", "X xxxx xxxxx x xxx", new List<string>());

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void Add_NotSuccess_ReturnsBadRequest()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var response = await _controller.Add("", "Xxxx xx xxxxx", new List<string>());

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void MarkAsDone_Success_ReturnsOk()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<MarkTodoAsDoneCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var response = await _controller.MarkAsDone(Guid.NewGuid());

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void MarkAsDone_NotSuccess_ReturnsBadRequest()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<MarkTodoAsDoneCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var response = await _controller.MarkAsDone(Guid.Empty);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }
    }
}