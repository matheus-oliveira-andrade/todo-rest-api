using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Interfaces
{
    public interface ITodoRepository
    {
        public Task Add(Domain.Entities.Todo todo);
        public Task Delete(Guid id);
        Task Update(Domain.Entities.Todo todo);

        public Task<Domain.Entities.Todo> GetById(Guid id);
        public Task<List<Domain.Entities.Todo>> GetAll();
    }
}
