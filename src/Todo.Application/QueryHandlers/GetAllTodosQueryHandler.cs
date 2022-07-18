using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Todo.Application.Queries;
using Todo.Application.ViewModels;
using Todo.Infrastructure.Interfaces;

namespace Todo.Application.QueryHandlers
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoViewModel>>
    {
        private readonly ILogger<GetAllTodosQueryHandler> _logger;
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        
        public GetAllTodosQueryHandler(
            ILogger<GetAllTodosQueryHandler> logger,
            ITodoRepository todoRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<List<TodoViewModel>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all todos");
            
            var todos = await _todoRepository.GetAllAsync();

            return todos
                .AsQueryable()
                .ProjectTo<TodoViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
