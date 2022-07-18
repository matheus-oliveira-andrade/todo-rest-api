using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Todo.Application.Queries;
using Todo.Application.ViewModels;
using Todo.Infrastructure.Interfaces;

namespace Todo.Application.QueryHandlers
{
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoViewModel>
    {
        private readonly ILogger<GetTodoByIdQueryHandler> _logger;
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodoByIdQueryHandler(
            ILogger<GetTodoByIdQueryHandler> logger,
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _logger = logger;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoViewModel> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting todo with id {Id}", query.Id);
            
            var todo = await _todoRepository.GetByIdAsync(query.Id);

            return _mapper.Map<TodoViewModel>(todo);
        }
    }
}
