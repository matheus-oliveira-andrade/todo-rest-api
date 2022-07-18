using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Interfaces
{
    public interface ITodoRepository
    {
        public Task AddAsync(Domain.Entities.Todo todo);
        Task UpdateAsync(Domain.Entities.Todo todo);

        public Task<Domain.Entities.Todo> GetByIdAsync(Guid id);
        public Task<List<Domain.Entities.Todo>> GetAllAsync();
    }
}
